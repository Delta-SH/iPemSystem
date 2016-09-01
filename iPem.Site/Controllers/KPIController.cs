using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class KPIController : Controller {
        
        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebLogger _webLogger;
        private readonly IDictionaryService _dictionaryService;
        private readonly IEnumMethodsService _enumMethodsService;
        private readonly IHisAlmService _hisAlmService;

        #endregion

        #region Ctor

        public KPIController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IDictionaryService dictionaryService,
            IEnumMethodsService enumMethodsService,
            IHisAlmService hisAlmService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._dictionaryService = dictionaryService;
            this._enumMethodsService = enumMethodsService;
            this._hisAlmService = hisAlmService;
        }

        #endregion

        #region Actions

        [Authorize]
        public ActionResult Base(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("base{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Performance(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("performance{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
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
                var models = this.Get500101(parent, types, starttime, endtime);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500101(string parent, string[] types, DateTime starttime, DateTime endtime) {
            try {
                var models = this.Get500101(parent, types, starttime, endtime);
                using(var ms = _excelManager.Export<Model500101>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                var models = this.Get500102(parent, types, starttime, endtime);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500102(string parent, string[] types, DateTime starttime, DateTime endtime) {
            try {
                var models = this.Get500102(parent, types, starttime, endtime);
                using(var ms = _excelManager.Export<Model500102>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model500101> Get500101(string parent, string[] types, DateTime starttime, DateTime endtime) {
            var result = new List<Model500101>();
            if(types == null) types = new string[] { };

            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return result;

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            var alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.whlinterval);
                            var almsInDev = from alm in alarms
                                            group alm by alm.DeviceId into g
                                            select new { Id = g.Key, Alarms = g };

                            var devices = _workContext.RoleDevices;
                            if(types != null && types.Length > 0)
                                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var childDevices = devices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var details = from device in childDevices
                                                  join aid in almsInDev on device.Current.Id equals aid.Id
                                                  select new {
                                                      Device = device.Current,
                                                      Alarms = aid.Alarms
                                                  };

                                    var devCount = childDevices.Count();
                                    var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                    var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                    result.Add(new Model500101 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        devCount = devCount,
                                        almTime = Math.Round(almTime, 2),
                                        cntTime = Math.Round(cntTime, 2),
                                        rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var childDevices = devices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var details = from device in childDevices
                                                  join aid in almsInDev on device.Current.Id equals aid.Id
                                                  select new {
                                                      Device = device.Current,
                                                      Alarms = aid.Alarms
                                                  };

                                    var devCount = devices.Count();
                                    var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                    var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                    result.Add(new Model500101 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        devCount = devCount,
                                        almTime = Math.Round(almTime, 2),
                                        cntTime = Math.Round(cntTime, 2),
                                        rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.whlinterval);
                            var almsInDev = from alm in alarms
                                            group alm by alm.DeviceId into g
                                            select new { Id = g.Key, Alarms = g };

                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var devices = room.Devices;
                                if(types != null && types.Length > 0)
                                    devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

                                var details = from device in devices
                                              join dev in almsInDev on device.Current.Id equals dev.Id
                                              select new {
                                                  Device = device.Current,
                                                  Alarms = dev.Alarms
                                              };

                                var devCount = devices.Count();
                                var almTime = details.SelectMany(a => a.Alarms).Sum(d => d.EndTime.Subtract(d.StartTime).TotalMinutes);
                                var cntTime = devCount * endtime.Subtract(starttime).TotalMinutes;
                                result.Add(new Model500101 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", current.Current.Name, room.Current.Name),
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

            return result;
        }

        private List<Model500102> Get500102(string parent, string[] types, DateTime starttime, DateTime endtime) {
            var result = new List<Model500102>();
            endtime = endtime.AddSeconds(86399);
            if(types == null) types = new string[] { };

            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return result;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            var alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslhulue);
                            var devices = _workContext.RoleDevices;
                            if(types != null && types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

                            var almWdev = from alarm in alarms
                                          join device in devices on alarm.DeviceId equals device.Current.Id
                                          select alarm;

                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var filter = almWdev.Where(a => child.Keys.Contains(a.AreaId));
                                    var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                    var total = filter.Count();
                                    result.Add(new Model500102 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        count = count,
                                        total = total,
                                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var filter = almWdev.Where(a => a.StationId == station.Current.Id);
                                    var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                    var total = filter.Count();
                                    result.Add(new Model500102 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        count = count,
                                        total = total,
                                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslhulue);
                            var devices = _workContext.RoleDevices;
                            if(types != null && types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

                            var almWdev = from alarm in alarms
                                          join device in devices on alarm.DeviceId equals device.Current.Id
                                          select alarm;

                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var filter = almWdev.Where(a => a.RoomId == room.Current.Id);
                                var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= limit.jslguiding);
                                var total = filter.Count();
                                result.Add(new Model500102 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", current.Current.Name, room.Current.Name),
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

            return result;
        }

        #endregion

    }
}