using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MsDomain = iPem.Core.Domain.Master;
using HsDomain = iPem.Core.Domain.History;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using HsSrv = iPem.Services.History;
using RsSrv = iPem.Services.Resource;
using Newtonsoft.Json;

namespace iPem.Site.Controllers {
    public class KPIController : Controller {
        
        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IDictionaryService _msDictionaryService;
        private readonly RsSrv.IEnumMethodsService _rsEnumMethodsService;
        private readonly RsSrv.IStationTypeService _rsStationTypeService;
        private readonly HsSrv.IHisAlmService _hsHisAlmService;

        #endregion

        #region Ctor

        public KPIController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IDictionaryService msDictionaryService,
            RsSrv.IEnumMethodsService rsEnumMethodsService,
            RsSrv.IStationTypeService rsStationTypeService,
            HsSrv.IHisAlmService hsHisAlmService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._msDictionaryService = msDictionaryService;
            this._rsEnumMethodsService = rsEnumMethodsService;
            this._rsStationTypeService = rsStationTypeService;
            this._hsHisAlmService = hsHisAlmService;
        }

        #endregion

        #region Actions

        [Authorize]
        public ActionResult Base(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("base{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Performance(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("performance{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("custom{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [AjaxAuthorize]
        public JsonResult Request500101(int start, int limit, string parent, string[] types, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<Model500101>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500101>()
            };

            try {
                var models = Get500101(parent, types, starttime, endtime);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500101(string parent, string[] types, DateTime starttime, DateTime endtime) {
            try {
                var models = Get500101(parent, types, starttime, endtime);
                using(var ms = _excelManager.Export<Model500101>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500102(int start, int limit, string parent, string[] types, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<Model500102>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500102>()
            };

            try {
                var models = Get500102(parent, types, starttime, endtime);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500102(string parent, string[] types, DateTime starttime, DateTime endtime) {
            try {
                var models = Get500102(parent, types, starttime, endtime);
                using(var ms = _excelManager.Export<Model500102>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model500101> Get500101(string parent, string[] types, DateTime starttime, DateTime endtime) {
            var result = new List<Model500101>();
            endtime = endtime.AddSeconds(86399);
            if(types == null) types = new string[] { };

            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return result;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedAreaAttributes[nodeid];
                            var alarms = _hsHisAlmService.GetHisAlms(starttime, endtime).Where(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.whlinterval);
                            var almsInDev = from alm in alarms
                                            group alm by alm.DeviceId into g
                                            select new { Id = g.Key, Alarms = g };

                            if(current.HasChildren) {
                                #region area children
                                var areaTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Area, "类型").ToList();
                                foreach(var child in current.FirstChildren) {
                                    if(_workContext.AssociatedAreaAttributes.ContainsKey(child.AreaId)) {
                                        var childCurrent = _workContext.AssociatedAreaAttributes[child.AreaId];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.AreaId);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.AreaId);

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Area.AreaId) && (types.Length == 0 || types.Contains(d.Current.DeviceTypeId)));
                                        var details = from device in devices
                                                      join dev in almsInDev on device.Current.Id equals dev.Id
                                                      select new {
                                                          Device = device.Current,
                                                          Alarms = dev.Alarms
                                                      };

                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        var devCount = devices.Count();
                                        var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                        var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                        result.Add(new Model500101 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
                                            devCount = devCount,
                                            almTime = Math.Round(almTime, 2),
                                            cntTime = Math.Round(cntTime, 2),
                                            rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                        });
                                    }
                                }
                                #endregion
                            } else {
                                #region station children
                                var stations = _workContext.AssociatedStations.FindAll(s => s.AreaId == nodeid);
                                var rootMatchs = stations.ToDictionary(k => k.Id, v => v.Name);
                                var roots = new List<RsDomain.Station>();
                                foreach(var sta in stations) {
                                    if(!rootMatchs.ContainsKey(sta.ParentId))
                                        roots.Add(sta);
                                }

                                var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
                                foreach(var root in roots) {
                                    if(_workContext.AssociatedStationAttributes.ContainsKey(root.Id)) {
                                        var childCurrent = _workContext.AssociatedStationAttributes[root.Id];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.Id);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.Id);

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id) && (types.Length == 0 || types.Contains(d.Current.DeviceTypeId)));
                                        var details = from device in devices
                                                      join dev in almsInDev on device.Current.Id equals dev.Id
                                                      select new {
                                                          Device = device.Current,
                                                          Alarms = dev.Alarms
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        var devCount = devices.Count();
                                        var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                        var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                        result.Add(new Model500101 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
                                            devCount = devCount,
                                            almTime = Math.Round(almTime, 2),
                                            cntTime = Math.Round(cntTime, 2),
                                            rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var alarms = _hsHisAlmService.GetHisAlms(starttime, endtime).Where(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.whlinterval);
                            var almsInDev = from alm in alarms
                                            group alm by alm.DeviceId into g
                                            select new { Id = g.Key, Alarms = g };

                            if(current.HasChildren) {
                                #region station children
                                var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
                                foreach(var child in current.FirstChildren) {
                                    if(_workContext.AssociatedStationAttributes.ContainsKey(child.Id)) {
                                        var childCurrent = _workContext.AssociatedStationAttributes[child.Id];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.Id);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.Id);

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id) && (types.Length == 0 || types.Contains(d.Current.DeviceTypeId)));
                                        var details = from device in devices
                                                      join dev in almsInDev on device.Current.Id equals dev.Id
                                                      select new {
                                                          Device = device.Current,
                                                          Alarms = dev.Alarms
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        var devCount = devices.Count();
                                        var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                        var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                        result.Add(new Model500101 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
                                            devCount = devCount,
                                            almTime = Math.Round(almTime, 2),
                                            cntTime = Math.Round(cntTime, 2),
                                            rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                        });
                                    }
                                }
                                #endregion
                            } else {
                                #region room children
                                var rooms = _workContext.AssociatedRooms.FindAll(r => r.StationId == nodeid);
                                foreach(var room in rooms) {
                                    if(_workContext.AssociatedRoomAttributes.ContainsKey(room.Id)) {
                                        var childCurrent = _workContext.AssociatedRoomAttributes[room.Id];

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => d.Current.RoomId == childCurrent.Current.Id && (types.Length == 0 || types.Contains(d.Current.DeviceTypeId)));
                                        var details = from device in devices
                                                      join dev in almsInDev on device.Current.Id equals dev.Id
                                                      select new {
                                                          Device = device.Current,
                                                          Alarms = dev.Alarms
                                                      };

                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        var devCount = devices.Count();
                                        var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                        var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                        result.Add(new Model500101 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
                                            devCount = devCount,
                                            almTime = Math.Round(almTime, 2),
                                            cntTime = Math.Round(cntTime, 2),
                                            rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }

            return result;
        }

        private List<Model500102> Get500102(string parent, string[] types, DateTime starttime, DateTime endtime) {
            var result = new List<Model500102>();
            endtime = endtime.AddSeconds(86399);
            if(types == null) types = new string[] { };

            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return result;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedAreaAttributes[nodeid];
                            var alarms = _hsHisAlmService.GetHisAlms(starttime, endtime).Where(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslhulue);
                            var devices = _workContext.AssociatedDeviceAttributes.Values.AsEnumerable();
                            if(types.Length > 0) devices = devices.Where(d => types.Contains(d.Current.DeviceTypeId));
                            var almWdev = from alarm in alarms
                                          join device in devices on alarm.DeviceId equals device.Current.Id
                                          select new {Alarm = alarm,Device = device};

                            if(current.HasChildren) {
                                #region area children
                                var areaTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Area, "类型").ToList();
                                foreach(var child in current.FirstChildren) {
                                    if(_workContext.AssociatedAreaAttributes.ContainsKey(child.AreaId)) {
                                        var childCurrent = _workContext.AssociatedAreaAttributes[child.AreaId];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.AreaId);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.AreaId);

                                        var filtered = almWdev.Where(a => matchs.Contains(a.Device.Area.AreaId)).Select(a => a.Alarm);
                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        var count = filtered.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                        var total = filtered.Count();
                                        result.Add(new Model500102 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
                                            count = count,
                                            total = total,
                                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                        });
                                    }
                                }
                                #endregion
                            } else {
                                #region station children
                                var stations = _workContext.AssociatedStations.FindAll(s => s.AreaId == nodeid);
                                var rootMatchs = stations.ToDictionary(k => k.Id, v => v.Name);
                                var roots = new List<RsDomain.Station>();
                                foreach(var sta in stations) {
                                    if(!rootMatchs.ContainsKey(sta.ParentId))
                                        roots.Add(sta);
                                }

                                var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
                                foreach(var root in roots) {
                                    if(_workContext.AssociatedStationAttributes.ContainsKey(root.Id)) {
                                        var childCurrent = _workContext.AssociatedStationAttributes[root.Id];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.Id);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.Id);

                                        var filtered = almWdev.Where(a => matchs.Contains(a.Device.Station.Id)).Select(a => a.Alarm);
                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        var count = filtered.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                        var total = filtered.Count();
                                        result.Add(new Model500102 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
                                            count = count,
                                            total = total,
                                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var alarms = _hsHisAlmService.GetHisAlms(starttime, endtime).Where(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslhulue);
                            var devices = _workContext.AssociatedDeviceAttributes.Values.AsEnumerable();
                            if(types.Length > 0) devices = devices.Where(d => types.Contains(d.Current.DeviceTypeId));
                            var almWdev = from alarm in alarms
                                          join device in devices on alarm.DeviceId equals device.Current.Id
                                          select new { Alarm = alarm, Device = device };

                            if(current.HasChildren) {
                                #region station children
                                var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
                                foreach(var child in current.FirstChildren) {
                                    if(_workContext.AssociatedStationAttributes.ContainsKey(child.Id)) {
                                        var childCurrent = _workContext.AssociatedStationAttributes[child.Id];

                                        var matchs = new HashSet<string>();
                                        matchs.Add(childCurrent.Current.Id);
                                        foreach(var cc in childCurrent.Children)
                                            matchs.Add(cc.Id);

                                        var filtered = almWdev.Where(a => matchs.Contains(a.Device.Station.Id)).Select(a => a.Alarm);
                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        var count = filtered.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                        var total = filtered.Count();
                                        result.Add(new Model500102 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
                                            count = count,
                                            total = total,
                                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                        });
                                    }
                                }
                                #endregion
                            } else {
                                #region room children
                                var rooms = _workContext.AssociatedRooms.FindAll(r => r.StationId == nodeid);
                                foreach(var room in rooms) {
                                    if(_workContext.AssociatedRoomAttributes.ContainsKey(room.Id)) {
                                        var childCurrent = _workContext.AssociatedRoomAttributes[room.Id];
                                        var filtered = almWdev.Where(a => a.Device.Room.Id == childCurrent.Current.Id).Select(a => a.Alarm);
                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        var count = filtered.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                        var total = filtered.Count();
                                        result.Add(new Model500102 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
                                            count = count,
                                            total = total,
                                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion

    }
}