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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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

                var fsu = _workContext.AllFsus().Find(f => f.Current.Id == existed.FsuId);
                if (fsu == null) throw new iPemException("未找到FSU");

                var device = _workContext.AllDevices().Find(d => d.Current.Id == existed.DeviceId);
                if (device == null) throw new iPemException("未找到设备");

                var point = _workContext.Points().Find(p => p.Id == existed.PointId);
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
                    AlarmValue = 1,
                    AlarmFlag = EnmFlag.End,
                    AlarmDesc = existed.AlarmDesc,
                    AlarmRemark = "人工结束告警"
                });

                var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
                if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);

                _webLogger.Information(EnmEventType.Other, string.Format("下发结束告警命令[{0}]", existed.Id), null, _workContext.User().Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警结束命令已下发,请稍后查询。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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

                _webLogger.Information(EnmEventType.Other, string.Format("删除告警[{0}]", existed.Id), null, _workContext.User().Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<RedundantAlmModel> GetRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<RedundantAlmModel>>(key);

            var alarms = _aalarmService.GetAllAlarmsInSpan(startDate, endDate).FindAll(a => a.RoomId != "-1");
            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var _keys = Common.SplitKeys(parent);
                if (_keys.Length == 2) {
                    var _type = int.Parse(_keys[0]);
                    var _id = _keys[1];
                    var _nodeType = Enum.IsDefined(typeof(EnmSSH), _type) ? (EnmSSH)_type : EnmSSH.Area;
                    if (_nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == _id);
                        if (current != null) alarms = alarms.FindAll(v => current.Keys.Contains(v.AreaId));
                    } else if (_nodeType == EnmSSH.Station) {
                        alarms = alarms.FindAll(v => v.StationId.Equals(_id));
                    } else if (_nodeType == EnmSSH.Room) {
                        alarms = alarms.FindAll(v => v.RoomId.Equals(_id));
                    } else if (_nodeType == EnmSSH.Device) {
                        alarms = alarms.FindAll(v => v.DeviceId.Equals(_id));
                    }
                }
            }

            if (levels != null && levels.Length > 0) {
                alarms = alarms.FindAll(v => levels.Contains((int)v.AlarmLevel));
            }

            var words = Common.SplitCondition(keyWord);
            if (words.Length > 0 && type == 1) {
                alarms = alarms.FindAll(v => words.Contains(v.Id));
            }

            var _stores = from alarm in alarms
                          join point in _workContext.AL() on alarm.PointId equals point.Id
                          join device in _workContext.AllDevices() on alarm.DeviceId equals device.Current.Id
                          join room in _workContext.AllRooms() on device.Current.RoomId equals room.Current.Id
                          join station in _workContext.AllStations() on room.Current.StationId equals station.Current.Id
                          join area in _workContext.AllAreas() on station.Current.AreaId equals area.Current.Id
                          select new AlmStore<A_AAlarm> {
                              Current = alarm,
                              PointName = point.Name,
                              DeviceName = device.Current.Name,
                              DeviceTypeId = device.Current.Type.Id,
                              SubDeviceTypeId = device.Current.SubType.Id,
                              SubLogicTypeId = device.Current.SubLogicType.Id,
                              RoomName = room.Current.Name,
                              RoomTypeId = room.Current.Type.Id,
                              StationName = station.Current.Name,
                              StationTypeId = station.Current.Type.Id,
                              AreaName = area.Current.Name,
                              AreaFullName = area.ToString(),
                              SubCompany = device.Current.SubCompany ?? ""
                          };

            var result = new List<RedundantAlmModel>();
            var index = 0;
            foreach (var _store in _stores) {
                if (words.Length > 0 && type == 2 && !CommonHelper.ConditionContain(_store.PointName, words))
                    continue;

                result.Add(new RedundantAlmModel {
                    index = ++index,
                    id = _store.Current.Id,
                    level = Common.GetAlarmDisplay(_store.Current.AlarmLevel),
                    time = CommonHelper.DateTimeConverter(_store.Current.AlarmTime),
                    interval = CommonHelper.IntervalConverter(_store.Current.AlarmTime),
                    comment = _store.Current.AlarmDesc,
                    value = _store.Current.AlarmValue.ToString(),
                    supporter = _store.SubCompany,
                    point = _store.PointName,
                    device = _store.DeviceName,
                    room = _store.RoomName,
                    station = _store.StationName,
                    area = _store.AreaFullName,
                    confirmed = Common.GetConfirmDisplay(_store.Current.Confirmed),
                    confirmer = _store.Current.Confirmer,
                    confirmedtime = _store.Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(_store.Current.ConfirmedTime.Value) : "",
                    reservation = string.IsNullOrWhiteSpace(_store.Current.ReservationId) ? "否" : "是",
                    primary = string.IsNullOrWhiteSpace(_store.Current.PrimaryId) ? "否":"是",
                    related = string.IsNullOrWhiteSpace(_store.Current.RelatedId) ? "否" : "是",
                    filter = string.IsNullOrWhiteSpace(_store.Current.FilterId) ? "否" : "是",
                    masked = !_store.Current.Masked ? "否" : "是",
                    reversal = string.IsNullOrWhiteSpace(_store.Current.ReversalId) ? "否" : "是",
                    levelid = (int)_store.Current.AlarmLevel,
                    createdtime = CommonHelper.DateTimeConverter(_store.Current.CreatedTime),
                    background = Common.GetAlarmColor(_store.Current.AlarmLevel)
                });
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<MaskingModel> GetMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.MaskingPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_MaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_MaskingRepository);
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_HashMaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_HashMaskingRepository);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<MaskingModel>>(key);

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
                                    join area in _workContext.Areas() on station.Current.AreaId equals area.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = area.ToString(),
                                        name = station.Current.Name,
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInRoom.Count > 0) {
                    result.AddRange(from masking in maskingsInRoom
                                    join room in _workContext.Rooms() on masking.Id equals room.Current.Id
                                    join area in _workContext.Areas() on room.Current.AreaId equals area.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = area.ToString(),
                                        name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInDevice.Count > 0) {
                    result.AddRange(from masking in maskingsInDevice
                                    join device in _workContext.Devices() on masking.Id equals device.Current.Id
                                    join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = area.ToString(),
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

                        var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                        if (area == null) continue;

                        result.Add(new MaskingModel {
                            index = masking.Time.Ticks,
                            area = area.ToString(),
                            name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                            type = Common.GetMaskTypeDisplay(masking.Type),
                            time = CommonHelper.DateTimeConverter(masking.Time)
                        });
                    }
                }
                #endregion
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length != 2) return result;
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;

                if (nodeType == EnmSSH.Area) {
                    #region Area
                    var current = _workContext.Areas().Find(a => a.Current.Id == id);
                    if (current != null) {
                        if (maskingsInStation.Count > 0) {
                            var stations = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInStation
                                            join station in stations on masking.Id equals station.Current.Id
                                            join area in _workContext.Areas() on station.Current.AreaId equals area.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = area.ToString(),
                                                name = station.Current.Name,
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInRoom.Count > 0) {
                            var rooms = _workContext.Rooms().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInRoom
                                            join room in rooms on masking.Id equals room.Current.Id
                                            join area in _workContext.Areas() on room.Current.AreaId equals area.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = area.ToString(),
                                                name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInDevice.Count > 0) {
                            var devices = _workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInDevice
                                            join device in devices on masking.Id equals device.Current.Id
                                            join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = area.ToString(),
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

                                var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                                if (area == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = area.ToString(),
                                    name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                    type = Common.GetMaskTypeDisplay(masking.Type),
                                    time = CommonHelper.DateTimeConverter(masking.Time)
                                });
                            }
                        }
                    }
                    #endregion
                } else if (nodeType == EnmSSH.Station) {
                    #region Station
                    var current = maskingsInStation.Find(m => m.Id.Equals(id));
                    if (current != null) {
                        var station = _workContext.Stations().Find(s => s.Current.Id.Equals(id));
                        if (station != null) {
                            var area = _workContext.Areas().Find(a => a.Current.Id.Equals(station.Current.AreaId));
                            if (area != null) {
                                result.Add(new MaskingModel {
                                    index = current.Time.Ticks,
                                    area = area.ToString(),
                                    name = station.Current.Name,
                                    type = Common.GetMaskTypeDisplay(current.Type),
                                    time = CommonHelper.DateTimeConverter(current.Time)
                                });
                            }
                        }
                    }

                    if (maskingsInRoom.Count > 0) {
                        var rooms = _workContext.Rooms().FindAll(s => s.Current.StationId.Equals(id));
                        result.AddRange(from masking in maskingsInRoom
                                        join room in rooms on masking.Id equals room.Current.Id
                                        join area in _workContext.Areas() on room.Current.AreaId equals area.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = area.ToString(),
                                            name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(id));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = area.ToString(),
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(id));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                            if (area == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = area.ToString(),
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeType == EnmSSH.Room) {
                    #region Room
                    var current = maskingsInRoom.Find(m => m.Id.Equals(id));
                    if (current != null) {
                        var room = _workContext.Rooms().Find(s => s.Current.Id.Equals(id));
                        if (room != null) {
                            var area = _workContext.Areas().Find(a => a.Current.Id.Equals(room.Current.AreaId));
                            if (area != null) {
                                result.Add(new MaskingModel {
                                    index = current.Time.Ticks,
                                    area = area.ToString(),
                                    name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                    type = Common.GetMaskTypeDisplay(current.Type),
                                    time = CommonHelper.DateTimeConverter(current.Time)
                                });
                            }
                        }
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(id));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = area.ToString(),
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(id));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                            if (area == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = area.ToString(),
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeType == EnmSSH.Device) {
                    #region Device
                    var current = maskingsInDevice.Find(m => m.Id.Equals(id));
                    if (current != null) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(id));
                        if (device != null) {
                            var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                            if (area != null) {
                                result.Add(new MaskingModel {
                                    index = current.Time.Ticks,
                                    area = area.ToString(),
                                    name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                    type = Common.GetMaskTypeDisplay(current.Type),
                                    time = CommonHelper.DateTimeConverter(current.Time)
                                });
                            }
                        }
                    }

                    if (maskingsInPoint.Count > 0) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(id));
                        if (device != null) {
                            foreach (var masking in maskingsInPoint) {
                                var ids = CommonHelper.SplitCondition(masking.Id);
                                if (ids.Length != 2) continue;

                                if (!device.Current.Id.Equals(ids[0])) continue;

                                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                                if (point == null) continue;

                                var area = _workContext.Areas().Find(a => a.Current.Id.Equals(device.Current.AreaId));
                                if (area == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = area.ToString(),
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
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<PointParam> GetPointParams(string parent, int pmtype, string[] points, int[] types, bool cache) {
            var key = string.Format(GlobalCacheKeys.PointParamPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<PointParam>>(key);

            var result = new List<PointParam>();
            var signals = new List<D_Signal>();
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

            if (signals.Count == 0) return result;

            if (points != null && points.Length > 0)
                signals = signals.FindAll(s => points.Contains(s.PointId));

            if (types != null && types.Length > 0)
                signals = signals.FindAll(s => types.Contains((int)s.PointType));

            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var keys = Common.SplitKeys(parent);
                if (keys.Length != 2) return result;
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;

                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id == id);
                    if (current != null) signals = signals.FindAll(s => current.Keys.Contains(s.AreaId));
                } else if (nodeType == EnmSSH.Station) {
                    signals = signals.FindAll(s => s.StationId.Equals(id));
                } else if (nodeType == EnmSSH.Room) {
                    signals = signals.FindAll(s => s.RoomId.Equals(id));
                } else if (nodeType == EnmSSH.Device) {
                    signals = signals.FindAll(s => s.DeviceId.Equals(id));
                }
            }

            var areaKeys = _workContext.Areas().ToDictionary(k => k.Current.Id, v => v.ToString());

            var index = 0;
            foreach (var signal in signals) {
                if (!areaKeys.ContainsKey(signal.AreaId)) continue;

                var current = string.IsNullOrWhiteSpace(signal.Current) ? "--" : signal.Current;
                var normal = string.IsNullOrWhiteSpace(signal.Normal) ? "--" : signal.Normal;

                var target = new PointParam {
                    index = ++index,
                    area = areaKeys[signal.AreaId],
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

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        #endregion

    }
}