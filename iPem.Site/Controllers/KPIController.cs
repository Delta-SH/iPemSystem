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
        public JsonResult Request500101(int start, int limit, string parent, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500101>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500101>()
            };

            try {
                var models = this.Get500101(parent, types, startDate, endDate);
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
        public ActionResult Download500101(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500101(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500101>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500102(int start, int limit, string parent, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500102>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500102>()
            };

            try {
                var models = this.Get500102(parent, types, startDate, endDate);
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
        public ActionResult Download500102(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500102(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500102>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model500101> Get500101(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);
            
            var result = new List<Model500101>();
            if(types == null) types = new string[] { };

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return result;

            if(!string.IsNullOrWhiteSpace(parent)) {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            var devices = _workContext.RoleDevices;
                            if(types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

                            if(current.HasChildren) {
                                #region area children
                                var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.whlHuLue);
                                foreach(var child in current.ChildRoot) {
                                    var childDevices = devices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var childMatchs = childDevices.Select(d => d.Current.Id);
                                    var details = alarms.FindAll(a => childMatchs.Contains(a.DeviceId));

                                    var devCount = childDevices.Count;
                                    var almTime = details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                                    var cntTime = devCount * endDate.Subtract(startDate).TotalSeconds;
                                    result.Add(new Model500101 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        devCount = devCount,
                                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                                        rate = string.Format("{0:P2}", cntTime > 0 ? 1 - almTime / cntTime : 1)
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                var alarms = _hisAlmService.GetAlmsInAreaAsList(id, startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.whlHuLue);
                                foreach(var station in current.Stations) {
                                    var childDevices = devices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var childMatchs = childDevices.Select(d => d.Current.Id);
                                    var details = alarms.FindAll(a => childMatchs.Contains(a.DeviceId));

                                    var devCount = childDevices.Count;
                                    var almTime = details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                                    var cntTime = devCount * endDate.Subtract(startDate).TotalSeconds;
                                    result.Add(new Model500101 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        devCount = devCount,
                                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
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
                            var alarms = _hisAlmService.GetAlmsInStationAsList(id, startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.whlHuLue);
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var childDevices = room.Devices;
                                if(types.Length > 0) childDevices = childDevices.FindAll(d => types.Contains(d.Current.Type.Id));

                                var childMatchs = childDevices.Select(d => d.Current.Id);
                                var details = alarms.FindAll(a => childMatchs.Contains(a.DeviceId));

                                var devCount = childDevices.Count;
                                var almTime = details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                                var cntTime = devCount * endDate.Subtract(startDate).TotalSeconds;
                                result.Add(new Model500101 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", current.Current.Name, room.Current.Name),
                                    devCount = devCount,
                                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
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

        private List<Model500102> Get500102(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);            
            
            var result = new List<Model500102>();
            if(types == null) types = new string[] { };

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return result;

            if(!string.IsNullOrWhiteSpace(parent)) {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            var devices = _workContext.RoleDevices;
                            if(types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));
                            var devMatchs = devices.Select(d => d.Current.Id);

                            if(current.HasChildren) {
                                #region area children
                                var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate).FindAll(a => devMatchs.Contains(a.DeviceId) && a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslHuLue);
                                foreach(var child in current.ChildRoot) {
                                    var filter = alarms.FindAll(a => child.Keys.Contains(a.AreaId));
                                    var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                                    var total = filter.Count;
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
                                var alarms = _hisAlmService.GetAlmsInAreaAsList(id, startDate, endDate).FindAll(a => devMatchs.Contains(a.DeviceId) && a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslHuLue);
                                foreach(var station in current.Stations) {
                                    var filter = alarms.FindAll(a => a.StationId == station.Current.Id);
                                    var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                                    var total = filter.Count;
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
                            var devices = _workContext.RoleDevices;
                            if(types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));
                            var devMatchs = devices.Select(d => d.Current.Id);

                            var alarms = _hisAlmService.GetAlmsInStationAsList(id, startDate, endDate).FindAll(a => devMatchs.Contains(a.DeviceId) && a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslHuLue);
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var filter = alarms.FindAll(a => a.RoomId == room.Current.Id);
                                var count = filter.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                                var total = filter.Count;
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