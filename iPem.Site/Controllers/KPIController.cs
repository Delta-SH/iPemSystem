using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
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
        private readonly IHisElecService _hisElecService;

        #endregion

        #region Ctor

        public KPIController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IDictionaryService dictionaryService,
            IEnumMethodsService enumMethodsService,
            IHisAlmService hisAlmService,
            IHisElecService hisElecService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._dictionaryService = dictionaryService;
            this._enumMethodsService = enumMethodsService;
            this._hisAlmService = hisAlmService;
            this._hisElecService = hisElecService;
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
        public JsonResult Request500103(int start, int limit, string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500103>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500103>()
            };

            try {
                var models = this.Get500103(parent, types, points, startDate, endDate);
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
        public ActionResult Download500103(string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500103(parent, types, points, startDate, endDate);
                using(var ms = _excelManager.Export<Model500103>(models, "温控系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500104(int start, int limit, string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500104>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500104>()
            };

            try {
                var models = this.Get500104(parent, types, points, startDate, endDate);
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
        public ActionResult Download500104(string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500104(parent, types, points, startDate, endDate);
                using(var ms = _excelManager.Export<Model500104>(models, "监控可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500105(int start, int limit, string parent, string[] types, int roadCount, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500105>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500105>()
            };

            try {
                var models = this.Get500105(parent, types, roadCount, startDate, endDate);
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
        public ActionResult Download500105(string parent, string[] types, int roadCount, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500105(parent, types, roadCount, startDate, endDate);
                using(var ms = _excelManager.Export<Model500105>(models, "市电可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500301(int start, int limit, string parent, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxChartModel<List<Model500301>, List<Chart500301>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500301>(),
                chart = new List<Chart500301>()
            };

            try {
                var models = this.Get500301(parent, size, startDate, endDate);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    if(models.Count > 100)
                        models = models.Take(100).ToList();

                    foreach(var model in models) {
                        var names = model.name.Split(new char[]{','});
                        data.chart.Add(new Chart500301 {
                            index = model.index,
                            name = names.Length > 0 ? names[names.Length - 1] : "",
                            kt = model.kt,
                            zm = model.zm,
                            bg = model.bg,
                            sb = model.sb,
                            kgdy = model.kgdy,
                            ups = model.ups,
                            qt = model.qt
                        });
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
        public ActionResult Download500301(string parent, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500301(parent, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500301>(models, "能耗分类统计", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500401(int start, int limit, string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500401>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500401>()
            };

            try {
                var models = this.Get500401(parent, size, types, startDate, endDate);
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
        public ActionResult Download500401(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500401(parent, size, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500401>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500402(int start, int limit, string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500402>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500402>()
            };

            try {
                var models = this.Get500402(parent, size, types, startDate, endDate);
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
        public ActionResult Download500402(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500402(parent, size, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500402>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500403(int start, int limit, string parent, int size, int[] levels, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500403>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500403>()
            };

            try {
                var models = this.Get500403(parent, size, levels, startDate, endDate);
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
        public ActionResult Download500403(string parent, int size, int[] levels, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500403(parent, size, levels, startDate, endDate);
                using(var ms = _excelManager.Export<Model500403>(models, "告警确认及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model500103> Get500103(string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500103>();
            if(points == null || points.Length == 0) return result;
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var stations = _workContext.RoleStations;
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null)
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            foreach(var station in stations) {
                var alarms = _hisAlmService.GetAlmsInStationAsList(station.Current.Id, startDate, endDate).FindAll(a => points.Contains(a.PointId));
                var devices = station.Rooms.SelectMany(r => r.Devices);

                var total = 0;
                foreach(var device in devices) {
                    total += device.Protocol.Points.Select(p => p.Id).Intersect(points).Count();
                }

                var area = _workContext.RoleAreas.Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500103 {
                    index = ++index,
                    name = string.Format("{0},{1}", area == null ? "" : area.ToString(), station.Current.Name),
                    type = station.Current.Type.Name,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    total = total,
                    time = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", total > 0 && cntTime > 0 ? 1 - almTime / (total * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500104> Get500104(string parent, string[] types, string[] points, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500104>();
            if(points == null || points.Length == 0) return result;
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var stations = _workContext.RoleStations;
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null)
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            foreach(var station in stations) {
                var devices = station.Rooms.SelectMany(r => r.Devices).Where(d => d.Protocol.Points.Select(p => p.Id).Intersect(points).Any()).ToList();
                var alarms = _hisAlmService.GetAlmsInStationAsList(station.Current.Id, startDate, endDate).FindAll(a => points.Contains(a.PointId));

                var area = _workContext.RoleAreas.Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500104 {
                    index = ++index,
                    name = string.Format("{0},{1}", area == null ? "" : area.ToString(), station.Current.Name),
                    type = station.Current.Type.Name,
                    devCount = devices.Count,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500105> Get500105(string parent, string[] types, int roadCount, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500105>();
            var rtValues = _workContext.RtValues;
            if(rtValues == null || rtValues.tingDianXinHao == null) return result;
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var stations = _workContext.RoleStations;
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null)
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            foreach(var station in stations) {
                var alarms = _hisAlmService.GetAlmsInStationAsList(station.Current.Id, startDate, endDate).FindAll(a => a.PointId == rtValues.tingDianXinHao);
                var area = _workContext.RoleAreas.Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500105 {
                    index = ++index,
                    name = string.Format("{0},{1}", area == null ? "" : area.ToString(), station.Current.Name),
                    type = station.Current.Type.Name,
                    count = roadCount,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", roadCount > 0 && cntTime > 0 ? 1 - almTime / (roadCount * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500301> Get500301(string parent, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500301>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if(parent == "root") {
                #region root
                if(size == (int)EnmOrganization.Area) {
                    var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Station, startDate, endDate);
                    var roots = _workContext.RoleAreas.FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var children = _workContext.RoleStations.FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                        var categories = energies.FindAll(e => children.Contains(e.Id));
                        result.Add(this.Calculate500301(categories, ++index, root.ToString()));
                    }
                } else if(size == (int)EnmOrganization.Station) {
                    var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Station, startDate, endDate);
                    foreach(var child in _workContext.RoleStations) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.RoleAreas.Find(a => a.Current.Id == child.Current.AreaId);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name)));
                    }
                } else if(size == (int)EnmOrganization.Room) {
                    var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Room, startDate, endDate);
                    foreach(var child in _workContext.RoleRooms) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.RoleAreas.Find(a => a.Current.Id == child.Current.AreaId);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name)));
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(size == (int)EnmOrganization.Area) {
                        var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Station, startDate, endDate);
                        if(current.HasChildren) {
                            foreach(var root in current.ChildRoot) {
                                var children = _workContext.RoleStations.FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                                var categories = energies.FindAll(e => children.Contains(e.Id));
                                result.Add(this.Calculate500301(categories, ++index, root.ToString()));
                            }
                        } else {
                            var children = _workContext.RoleStations.FindAll(s => s.Current.AreaId == current.Current.Id).Select(s => s.Current.Id);
                            var categories = energies.FindAll(e => children.Contains(e.Id));
                            result.Add(this.Calculate500301(categories, ++index, current.ToString()));
                        }
                    } else if(size == (int)EnmOrganization.Station) {
                        var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Station, startDate, endDate);
                        var children = _workContext.RoleStations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == child.Current.AreaId);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", area != null ? area.ToString() : current.ToString(), child.Current.Name)));
                        }
                    } else if(size == (int)EnmOrganization.Room) {
                        var energies = _hisElecService.GetEnergiesAsList(EnmOrganization.Room, startDate, endDate);
                        var children = _workContext.RoleRooms.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == child.Current.AreaId);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", area != null ? area.ToString() : current.ToString(), child.Current.StationName, child.Current.Name)));
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private Model500301 Calculate500301(List<HisElec> categories, int index, string name) {
            var current = new Model500301 {
                index = index,
                name = name,
                kt = categories.FindAll(c => c.FormulaType == EnmFormula.KT).Sum(c => c.Value),
                zm = categories.FindAll(c => c.FormulaType == EnmFormula.ZM).Sum(c => c.Value),
                bg = categories.FindAll(c => c.FormulaType == EnmFormula.BG).Sum(c => c.Value),
                sb = categories.FindAll(c => c.FormulaType == EnmFormula.SB).Sum(c => c.Value),
                kgdy = categories.FindAll(c => c.FormulaType == EnmFormula.KGDY).Sum(c => c.Value),
                ups = categories.FindAll(c => c.FormulaType == EnmFormula.UPS).Sum(c => c.Value),
                qt = categories.FindAll(c => c.FormulaType == EnmFormula.QT).Sum(c => c.Value),
                zl = categories.FindAll(c => c.FormulaType == EnmFormula.ZL).Sum(c => c.Value)
            };

            current.ktrate = string.Format("{0:P2}", current.zl > 0 ? current.kt / current.zl : 0);
            current.zmrate = string.Format("{0:P2}", current.zl > 0 ? current.zm / current.zl : 0);
            current.bgrate = string.Format("{0:P2}", current.zl > 0 ? current.bg / current.zl : 0);
            current.sbrate = string.Format("{0:P2}", current.zl > 0 ? current.sb / current.zl : 0);
            current.kgdyrate = string.Format("{0:P2}", current.zl > 0 ? current.kgdy / current.zl : 0);
            current.upsrate = string.Format("{0:P2}", current.zl > 0 ? current.ups / current.zl : 0);
            current.qtrate = string.Format("{0:P2}", current.zl > 0 ? current.qt / current.zl : 0);
            return current;
        }

        private List<Model500401> Get500401(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);
            
            var result = new List<Model500401>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return result;

            var devices = _workContext.RoleDevices;
            if(types != null && types.Length > 0) 
                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

            var index = 0;
            if(parent == "root") {
                #region root
                var leaies = _workContext.RoleAreas.FindAll(a => a.Current.Type.Id == size);
                var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                foreach(var leaf in leaies) {
                    var childDevices = devices.FindAll(d => leaf.Keys.Contains(d.Current.AreaId));
                    var childDevIds = childDevices.Select(d => d.Current.Id);
                    var childAlarms = alarms.FindAll(a => childDevIds.Contains(a.DeviceId));

                    var devCount = childDevices.Count;
                    var almTime = childAlarms.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                    var cntTime = endDate.Subtract(startDate).TotalSeconds;
                    result.Add(new Model500401 {
                        index = ++index,
                        name = leaf.ToString(),
                        type = leaf.Current.Type.Value,
                        devCount = devCount,
                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                        rate = string.Format("{0:P2}", (devCount > 0 && cntTime > 0) ? 1 - almTime / (devCount * cntTime) : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var leaies = current.Children.FindAll(a => a.Current.Type.Id == size);
                        var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                        foreach(var leaf in leaies) {
                            var childDevices = devices.FindAll(d => leaf.Keys.Contains(d.Current.AreaId));
                            var childDevIds = childDevices.Select(d => d.Current.Id);
                            var childAlarms = alarms.FindAll(a => childDevIds.Contains(a.DeviceId));

                            var devCount = childDevices.Count;
                            var almTime = childAlarms.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                            var cntTime = endDate.Subtract(startDate).TotalSeconds;
                            result.Add(new Model500401 {
                                index = ++index,
                                name = leaf.ToString(),
                                type = leaf.Current.Type.Value,
                                devCount = devCount,
                                almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                                rate = string.Format("{0:P2}", (devCount > 0 && cntTime > 0) ? 1 - almTime / (devCount * cntTime) : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var alarms = _hisAlmService.GetAlmsInAreaAsList(parent, startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                        var childDevices = devices.FindAll(d => d.Current.AreaId == parent);
                        var childDevIds = childDevices.Select(d => d.Current.Id);
                        var childAlarms = alarms.FindAll(a => childDevIds.Contains(a.DeviceId));

                        var devCount = childDevices.Count;
                        var almTime = childAlarms.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                        var cntTime = endDate.Subtract(startDate).TotalSeconds;
                        result.Add(new Model500401 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            devCount = devCount,
                            almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                            cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                            rate = string.Format("{0:P2}", (devCount > 0 && cntTime > 0) ? 1 - almTime / (devCount * cntTime) : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500402> Get500402(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);            
            
            var result = new List<Model500402>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return result;

            var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.jslHuLue);
            if(types != null && types.Length > 0) {
                var devMatchs = _workContext.RoleDevices.FindAll(d => types.Contains(d.Current.Type.Id)).Select(d => d.Current.Id);
                alarms = alarms.FindAll(a => devMatchs.Contains(a.DeviceId));
            }

            var index = 0;
            if(parent == "root") {
                #region root
                var leaies = _workContext.RoleAreas.FindAll(a => a.Current.Type.Id == size);
                foreach(var leaf in leaies) {
                    var childAlarms = alarms.FindAll(a => leaf.Keys.Contains(a.AreaId));
                    var count = childAlarms.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                    var total = childAlarms.Count;
                    result.Add(new Model500402 {
                        index = ++index,
                        name = leaf.ToString(),
                        type = leaf.Current.Type.Value,
                        count = count,
                        total = total,
                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var leaies = current.Children.FindAll(a => a.Current.Type.Id == size);
                        foreach(var leaf in leaies) {
                            var childAlarms = alarms.FindAll(a => leaf.Keys.Contains(a.AreaId));
                            var count = childAlarms.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                            var total = childAlarms.Count;
                            result.Add(new Model500402 {
                                index = ++index,
                                name = leaf.ToString(),
                                type = leaf.Current.Type.Value,
                                count = count,
                                total = total,
                                rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var childAlarms = alarms.FindAll(a => a.AreaId == current.Current.Id);
                        var count = childAlarms.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                        var total = childAlarms.Count;
                        result.Add(new Model500402 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            count = count,
                            total = total,
                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500403> Get500403(string parent, int size, int[] levels, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500403>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return result;

            var alarms = _hisAlmService.GetAllAlmsAsList(startDate, endDate);
            if(levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlmLevel));

            var stores = _workContext.GetHisAlmStore(alarms, startDate, endDate);

            var index = 0;
            if(parent == "root") {
                #region root
                var leaies = _workContext.RoleAreas.FindAll(a => a.Current.Type.Id == size);
                foreach(var leaf in leaies) {
                    var childStores = stores.FindAll(a => leaf.Keys.Contains(a.Current.AreaId));
                    var count = childStores.Count(a => (a.ExtSet1 != null && a.ExtSet1.ConfirmedTime.HasValue ? a.ExtSet1.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
                    var total = childStores.Count;
                    result.Add(new Model500403 {
                        index = ++index,
                        name = leaf.ToString(),
                        type = leaf.Current.Type.Value,
                        count = count,
                        total = total,
                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var leaies = current.Children.FindAll(a => a.Current.Type.Id == size);
                        foreach(var leaf in leaies) {
                            var childStores = stores.FindAll(a => leaf.Keys.Contains(a.Current.AreaId));
                            var count = childStores.Count(a => (a.ExtSet1 != null && a.ExtSet1.ConfirmedTime.HasValue ? a.ExtSet1.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
                            var total = childStores.Count;
                            result.Add(new Model500403 {
                                index = ++index,
                                name = leaf.ToString(),
                                type = leaf.Current.Type.Value,
                                count = count,
                                total = total,
                                rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var childStores = stores.FindAll(a => a.Current.AreaId == current.Current.Id);
                        var count = childStores.Count(a => (a.ExtSet1 != null && a.ExtSet1.ConfirmedTime.HasValue ? a.ExtSet1.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
                        var total = childStores.Count;
                        result.Add(new Model500403 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            count = count,
                            total = total,
                            rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        #endregion

    }
}