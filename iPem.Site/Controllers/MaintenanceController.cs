using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class MaintenanceController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IAAlarmService _aalarmService;
        private readonly ITAlarmService _talarmService;
        private readonly INoteService _noteService;
        private readonly ISignalService _signalService;

        #endregion

        #region Ctor

        public MaintenanceController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IAAlarmService aalarmService,
            ITAlarmService talarmService,
            INoteService noteService,
            ISignalService signalService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._aalarmService = aalarmService;
            this._talarmService = talarmService;
            this._noteService = noteService;
            this._signalService = signalService;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            return View();
        }

        public ActionResult Masking() {
            return View();
        }

        public ActionResult PointParam() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult RequestRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<RedundantAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<RedundantAlmModel>()
            };

            try {
                var stores = this.GetRedundantAlarms(parent, startDate, endDate, levels, type, keyWord, cache);
                if (stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(stores[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache) {
            try {
                var models = this.GetRedundantAlarms(parent, startDate, endDate, levels, type, keyWord, cache);
                using (var ms = _excelManager.Export<RedundantAlmModel>(models, "告警维护管理", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult FinishRedundantAlarms(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _aalarmService.GetAlarm(id);
                if (existed == null) throw new iPemException("告警不存在");

                var fsu = _workContext.Fsus().Find(f => f.Current.Id == existed.FsuId);
                if (fsu == null) throw new iPemException("未找到FSU");

                var device = _workContext.Devices().Find(d => d.Current.Id == existed.DeviceId);
                if (device == null) throw new iPemException("未找到设备");

                var point = _workContext.AL().Find(p => p.Id == existed.PointId);
                if (point == null) throw new iPemException("未找到信号");

                _talarmService.Add(new A_TAlarm {
                    FsuId = fsu.Current.Code,
                    DeviceId = device.Current.Code,
                    PointId = point.Id,
                    SignalId = point.Code,
                    SignalNumber = point.Number,
                    SerialNo = existed.SerialNo,
                    NMAlarmId = existed.NMAlarmId,
                    AlarmTime = DateTime.Now,
                    AlarmLevel = existed.AlarmLevel,
                    AlarmValue = 0,
                    AlarmFlag = EnmFlag.End,
                    AlarmDesc = existed.AlarmDesc,
                    AlarmRemark = "人工结束告警"
                });

                _webLogger.Information(EnmEventType.Other, string.Format("下发结束告警命令[{0}]", JsonConvert.SerializeObject(existed)), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警结束命令已下发,请稍后查询。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteRedundantAlarms(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _aalarmService.GetAlarm(id);
                if (existed == null) throw new iPemException("告警不存在");

                _aalarmService.RemoveAlarms(existed);

                var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
                if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);

                _webLogger.Information(EnmEventType.Other, string.Format("人工删除告警[{0}]", JsonConvert.SerializeObject(existed)), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<MaskingModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<MaskingModel>()
            };

            try {
                var models = this.GetMaskings(parent, types, startDate, endDate, cache);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetMaskings(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<MaskingModel>(models, "告警屏蔽信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestPointParams(string parent, int pmtype, string[] points, int[] types, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<PointParam>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<PointParam>()
            };

            try {
                var models = this.GetPointParams(parent, pmtype, points, types, cache);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadPointParams(string parent, int pmtype, string[] points, int[] types, bool cache) {
            try {
                var models = this.GetPointParams(parent, pmtype, points, types, cache);
                using (var ms = _excelManager.Export<PointParam>(models, "信号参数信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<RedundantAlmModel> GetRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<RedundantAlmModel>(key).ToList();

            var alarms = _aalarmService.GetAllAlarmsInSpan(startDate, endDate).FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) alarms = alarms.FindAll(v => current.Keys.Contains(v.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    alarms = alarms.FindAll(v => v.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    alarms = alarms.FindAll(v => v.RoomId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Device) {
                    alarms = alarms.FindAll(v => v.DeviceId.Equals(nodeKey.Value));
                }
            }

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(v => levels.Contains((int)v.AlarmLevel));

            var words = Common.SplitCondition(keyWord);
            if (words.Length > 0 && type == 1)
                alarms = alarms.FindAll(v => words.Contains(v.SerialNo));

            var devices = _workContext.Devices();
            var points = _workContext.AL();
            var signals = _signalService.GetSimpleSignals(alarms.Select(a => new Kv<string, string>(a.DeviceId, a.PointId)));
            var stores = from alarm in alarms
                         join signal in signals on new { alarm.DeviceId, alarm.PointId } equals new { signal.DeviceId, signal.PointId }
                         join point in points on alarm.PointId equals point.Id
                         join device in devices on alarm.DeviceId equals device.Current.Id
                         select new AlmStore<A_AAlarm> {
                             Current = alarm,
                             PointName = signal.PointName,
                             AlarmName = point.Name,
                             DeviceName = device.Current.Name,
                             DeviceTypeId = device.Current.Type.Id,
                             SubDeviceTypeId = device.Current.SubType.Id,
                             SubLogicTypeId = device.Current.SubLogicType.Id,
                             RoomName = device.Current.RoomName,
                             RoomTypeId = device.Current.RoomTypeId,
                             StationName = device.Current.StationName,
                             StationTypeId = device.Current.StaTypeId,
                             AreaName = device.Current.AreaName,
                             SubCompany = device.Current.SubCompany ?? "--",
                             SubManager = device.Current.SubManager ?? "--"
                         };

            var index = 0;
            var result = new List<RedundantAlmModel>();
            foreach (var store in stores) {
                if (words.Length > 0 && type == 2 && !CommonHelper.ConditionContain(store.AlarmName, words))
                    continue;

                result.Add(new RedundantAlmModel {
                    id = store.Current.Id,
                    index = ++index,
                    serialno = store.Current.SerialNo,
                    level = Common.GetAlarmDisplay(store.Current.AlarmLevel),
                    time = CommonHelper.DateTimeConverter(store.Current.AlarmTime),
                    name = store.AlarmName,
                    nmalarmid = store.Current.NMAlarmId,
                    interval = CommonHelper.IntervalConverter(store.Current.AlarmTime),
                    point = store.PointName,
                    device = store.DeviceName,
                    room = store.RoomName,
                    station = store.StationName,
                    area = store.AreaName,
                    supporter = store.SubCompany,
                    manager = store.SubManager,
                    confirmed = Common.GetConfirmDisplay(store.Current.Confirmed),
                    confirmer = store.Current.Confirmer,
                    confirmedtime = store.Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(store.Current.ConfirmedTime.Value) : "",
                    reservation = string.IsNullOrWhiteSpace(store.Current.ReservationId) ? "否" : "是",
                    primary = string.IsNullOrWhiteSpace(store.Current.PrimaryId) ? "否" : "是",
                    related = string.IsNullOrWhiteSpace(store.Current.RelatedId) ? "否" : "是",
                    filter = string.IsNullOrWhiteSpace(store.Current.FilterId) ? "否" : "是",
                    masked = !store.Current.Masked ? "否" : "是",
                    reversal = string.IsNullOrWhiteSpace(store.Current.ReversalId) ? "否" : "是",
                    levelid = (int)store.Current.AlarmLevel,
                    createdtime = CommonHelper.DateTimeConverter(store.Current.CreatedTime),
                    background = Common.GetAlarmColor(store.Current.AlarmLevel)
                });
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<MaskingModel> GetMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.MaskingPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_MaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_MaskingRepository);
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_HashMaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_HashMaskingRepository);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<MaskingModel>(key).ToList();

            var maskings = _workContext.Maskings().FindAll(m => m.Time >= startDate && m.Time <= endDate);
            if (types != null && types.Length > 0) {
                maskings = maskings.FindAll(m => types.Contains((int)m.Type));
            }

            var result = new List<MaskingModel>();
            var maskingsInStation = maskings.FindAll(m => m.Type == EnmMaskType.Station);
            var maskingsInRoom = maskings.FindAll(m => m.Type == EnmMaskType.Room);
            var maskingsInDevice = maskings.FindAll(m => m.Type == EnmMaskType.Device);
            var maskingsInPoint = maskings.FindAll(m => m.Type == EnmMaskType.Point);

            if (string.IsNullOrWhiteSpace(parent) || parent.Equals("root")) {
                #region Root
                if (maskingsInStation.Count > 0) {
                    result.AddRange(from masking in maskingsInStation
                                    join station in _workContext.Stations() on masking.Id equals station.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = station.Current.AreaName,
                                        name = station.Current.Name,
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInRoom.Count > 0) {
                    result.AddRange(from masking in maskingsInRoom
                                    join room in _workContext.Rooms() on masking.Id equals room.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = room.Current.AreaName,
                                        name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInDevice.Count > 0) {
                    result.AddRange(from masking in maskingsInDevice
                                    join device in _workContext.Devices() on masking.Id equals device.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = device.Current.AreaName,
                                        name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInPoint.Count > 0) {
                    foreach (var masking in maskingsInPoint) {
                        var ids = CommonHelper.SplitCondition(masking.Id);
                        if (ids.Length != 2) continue;

                        var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                        if (point == null) continue;

                        var device = _workContext.Devices().Find(d => d.Current.Id.Equals(ids[0]));
                        if (device == null) continue;

                        result.Add(new MaskingModel {
                            index = masking.Time.Ticks,
                            area = device.Current.AreaName,
                            name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                            type = Common.GetMaskTypeDisplay(masking.Type),
                            time = CommonHelper.DateTimeConverter(masking.Time)
                        });
                    }
                }
                #endregion
            } else {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    #region Area
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        if (maskingsInStation.Count > 0) {
                            var stations = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInStation
                                            join station in stations on masking.Id equals station.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = station.Current.AreaName,
                                                name = station.Current.Name,
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInRoom.Count > 0) {
                            var rooms = _workContext.Rooms().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInRoom
                                            join room in rooms on masking.Id equals room.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = room.Current.AreaName,
                                                name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInDevice.Count > 0) {
                            var devices = _workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInDevice
                                            join device in devices on masking.Id equals device.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = device.Current.AreaName,
                                                name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInPoint.Count > 0) {
                            var devices = _workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            foreach (var masking in maskingsInPoint) {
                                var ids = CommonHelper.SplitCondition(masking.Id);
                                if (ids.Length != 2) continue;

                                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                                if (point == null) continue;

                                var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                                if (device == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = device.Current.AreaName,
                                    name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                    type = Common.GetMaskTypeDisplay(masking.Type),
                                    time = CommonHelper.DateTimeConverter(masking.Time)
                                });
                            }
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Station) {
                    #region Station
                    var current = maskingsInStation.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var station = _workContext.Stations().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (station != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = station.Current.AreaName,
                                name = station.Current.Name,
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInRoom.Count > 0) {
                        var rooms = _workContext.Rooms().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInRoom
                                        join room in rooms on masking.Id equals room.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = room.Current.AreaName,
                                            name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = device.Current.AreaName,
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Room) {
                    #region Room
                    var current = maskingsInRoom.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var room = _workContext.Rooms().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (room != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = room.Current.AreaName,
                                name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = device.Current.AreaName,
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(nodeKey.Value));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Device) {
                    #region Device
                    var current = maskingsInDevice.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (device != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInPoint.Count > 0) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (device != null) {
                            foreach (var masking in maskingsInPoint) {
                                var ids = CommonHelper.SplitCondition(masking.Id);
                                if (ids.Length != 2) continue;

                                if (!device.Current.Id.Equals(ids[0])) continue;

                                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                                if (point == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = device.Current.AreaName,
                                    name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                    type = Common.GetMaskTypeDisplay(masking.Type),
                                    time = CommonHelper.DateTimeConverter(masking.Time)
                                });
                            }
                        }

                    }
                    #endregion
                }
            }

            var index = 0;
            result = result.OrderBy(r => r.index).ToList();
            foreach (var masking in result) {
                masking.index = ++index;
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<PointParam> GetPointParams(string parent, int pmtype, string[] points, int[] types, bool cache) {
            var key = string.Format(GlobalCacheKeys.PointParamPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<PointParam>(key).ToList();

            var result = new List<PointParam>();

            List<D_Signal> signals = null;
            var _pmtype = Enum.IsDefined(typeof(EnmPointParam), pmtype) ? (EnmPointParam)pmtype : EnmPointParam.AbsThreshold;
            if (_pmtype == EnmPointParam.AbsThreshold) {
                signals = _signalService.GetAbsThresholds();
            } else if (_pmtype == EnmPointParam.PerThreshold) {
                signals = _signalService.GetPerThresholds();
            } else if (_pmtype == EnmPointParam.SavedPeriod) {
                signals = _signalService.GetSavedPeriods();
            } else if (_pmtype == EnmPointParam.StorageRefTime) {
                signals = _signalService.GetStorageRefTimes();
            } else if (_pmtype == EnmPointParam.AlarmLimit) {
                signals = _signalService.GetAlarmLimits();
            } else if (_pmtype == EnmPointParam.AlarmLevel) {
                signals = _signalService.GetAlarmLevels();
            } else if (_pmtype == EnmPointParam.AlarmDelay) {
                signals = _signalService.GetAlarmDelays();
            } else if (_pmtype == EnmPointParam.AlarmRecoveryDelay) {
                signals = _signalService.GetAlarmRecoveryDelays();
            } else if (_pmtype == EnmPointParam.AlarmFiltering) {
                signals = _signalService.GetAlarmFilterings();
            } else if (_pmtype == EnmPointParam.AlarmInferior) {
                signals = _signalService.GetAlarmInferiors();
            } else if (_pmtype == EnmPointParam.AlarmConnection) {
                signals = _signalService.GetAlarmConnections();
            } else if (_pmtype == EnmPointParam.AlarmReversal) {
                signals = _signalService.GetAlarmReversals();
            }

            if (signals != null && signals.Count > 0) {
                if (points != null && points.Length > 0)
                    signals = signals.FindAll(s => points.Contains(s.PointId));

                if (types != null && types.Length > 0)
                    signals = signals.FindAll(s => types.Contains((int)s.PointType));

                if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                    var nodeKey = Common.ParseNode(parent);
                    if (nodeKey.Key == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                        if (current != null) signals = signals.FindAll(s => current.Keys.Contains(s.AreaId));
                    } else if (nodeKey.Key == EnmSSH.Station) {
                        signals = signals.FindAll(s => s.StationId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Room) {
                        signals = signals.FindAll(s => s.RoomId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Device) {
                        signals = signals.FindAll(s => s.DeviceId.Equals(nodeKey.Value));
                    }
                }

                var index = 0;
                foreach (var signal in signals) {
                    var current = string.IsNullOrWhiteSpace(signal.Current) ? "--" : signal.Current;
                    var normal = string.IsNullOrWhiteSpace(signal.Normal) ? "--" : signal.Normal;

                    var target = new PointParam {
                        index = ++index,
                        area = signal.AreaName,
                        station = signal.StationName,
                        room = signal.RoomName,
                        device = signal.DeviceName,
                        point = signal.PointName,
                        type = Common.GetPointParamDisplay(_pmtype),
                        current = current,
                        normal = normal,
                        diff = !current.Equals(normal)
                    };

                    if (_pmtype == EnmPointParam.AlarmFiltering
                        || _pmtype == EnmPointParam.AlarmInferior
                        || _pmtype == EnmPointParam.AlarmConnection
                        || _pmtype == EnmPointParam.AlarmReversal) {
                        target.diff = false;
                    }

                    target.background = target.diff ? Color.Red : Color.Transparent;
                    result.Add(target);
                }
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        #endregion

    }
}