using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class ReportController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IReservationService _reservationService;
        private readonly IDictionaryService _dictionaryService;
        private readonly INodeInReservationService _nodesInReservationService;
        private readonly IProjectService _projectService;
        private readonly IBrandService _brandService;
        private readonly IHAlarmService _hisAlarmService;
        private readonly IBatService _batService;
        private readonly IStaticService _staticService;
        private readonly IHMeasureService _measureService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IPointService _pointService;
        private readonly IProductorService _productorService;
        private readonly ISubCompanyService _subCompanyService;
        private readonly ISupplierService _supplierService;

        #endregion

        #region Ctor

        public ReportController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IReservationService reservationService,
            IDictionaryService dictionaryService,
            INodeInReservationService nodesInReservationService,
            IProjectService projectService,
            IBrandService brandService,
            IHAlarmService hisAlarmService,
            IBatService batService,
            IStaticService staticService,
            IHMeasureService measureService,
            IEnumMethodService enumMethodService,
            IPointService pointService,
            IProductorService productorService,
            ISubCompanyService subCompanyService,
            ISupplierService supplierService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._reservationService = reservationService;
            this._dictionaryService = dictionaryService;
            this._nodesInReservationService = nodesInReservationService;
            this._projectService = projectService;
            this._brandService = brandService;
            this._hisAlarmService = hisAlarmService;
            this._batService = batService;
            this._staticService = staticService;
            this._measureService = measureService;
            this._enumMethodService = enumMethodService;
            this._pointService = pointService;
            this._productorService = productorService;
            this._subCompanyService = subCompanyService;
            this._supplierService = supplierService;
        }

        #endregion

        #region Actions

        [Authorize]
        public ActionResult Base(int? id) {
            if(id.HasValue && _workContext.Authorizations.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("base{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult History(int? id) {
            if (id.HasValue && _workContext.Authorizations.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("history{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Chart(int? id) {
            if (id.HasValue && _workContext.Authorizations.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("chart{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if (id.HasValue && _workContext.Authorizations.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("custom{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400101(int start, int limit, string parent, int[] types) {
            var data = new AjaxChartModel<List<Model400101>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400101>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400101(parent, types);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400101(string parent, int[] types) {
            try {
                var models = this.GetBase400101(parent, types);
                using(var ms = _excelManager.Export<Model400101>(models, "区域统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400102(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400102>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400102>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400102(parent, types);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400102(string parent, string[] types) {
            try {
                var models = this.GetBase400102(parent, types);
                using(var ms = _excelManager.Export<Model400102>(models, "站点统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400103(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400103>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400103>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400103(parent, types);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400103(string parent, string[] types) {
            try {
                var models = this.GetBase400103(parent, types);
                using(var ms = _excelManager.Export<Model400103>(models, "机房统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400104(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400104>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400104>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400104(parent, types);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400104(string parent, string[] types) {
            try {
                var models = this.GetBase400104(parent, types);
                using(var ms = _excelManager.Export<Model400104>(models, "设备统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string point, int start, int limit) {
            var data = new AjaxDataModel<List<Model400201>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400201>()
            };

            try {
                var stores = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, point);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400201 {
                            index = i + 1,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            type = Common.GetPointTypeDisplay(stores[i].Point.Type),
                            value = stores[i].Current.Value,
                            unit = string.IsNullOrWhiteSpace(stores[i].Current.SignalDesc) ? Common.GetUnitDisplay(stores[i].Point.Type, stores[i].Current.Value.ToString(), stores[i].Point.UnitState) : stores[i].Current.SignalDesc,
                            status = Common.GetPointStatusDisplay(EnmState.Normal),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.UpdateTime),
                            statusid = (int)EnmState.Normal
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
        public ActionResult DownloadHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string point) {
            try {
                var models = new List<Model400201>();
                var stores = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, point);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400201 {
                            index = i + 1,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            type = Common.GetPointTypeDisplay(stores[i].Point.Type),
                            value = stores[i].Current.Value,
                            unit = string.IsNullOrWhiteSpace(stores[i].Current.SignalDesc) ? Common.GetUnitDisplay(stores[i].Point.Type, stores[i].Current.Value.ToString(), stores[i].Point.UnitState) : stores[i].Current.SignalDesc,
                            status = Common.GetPointStatusDisplay(EnmState.Normal),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.UpdateTime),
                            statusid = (int)EnmState.Normal,
                            background = Common.GetPointColor(EnmState.Normal)
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400201>(models, "历史测值信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400202(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[2]
            };

            try {
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime)
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(parent, stores);
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
        public ActionResult DownloadHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400202>();
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400202 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400202>(models, "历史告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400203(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400203>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400203>(),
                chart = new List<ChartModel>()
            };

            try {
                var stores = this.GetHistory400203(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400203 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }

                    var groups = from store in stores
                                 group store by store.Current.AlarmLevel into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            index = (int)group.Key,
                            name = Common.GetAlarmDisplay(group.Key),
                            value = group.Count
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
        public ActionResult DownloadHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400203>();
                var stores = this.GetHistory400203(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400203 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400203>(models, "历史告警分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400204(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400204>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400204>(),
                chart = new List<ChartModel>()
            };

            try {
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400204 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            deviceType = stores[i].Device.Type.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }

                    var groups = from store in stores
                                 group store by new { store.Device.Type, AlmLevel = store.Current.AlarmLevel } into g
                                 select new {
                                     Type = g.Key.Type,
                                     Level = g.Key.AlmLevel,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            index = (int)group.Level,
                            name = group.Type.Name,
                            value = group.Count
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
        public ActionResult DownloadHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400204>();
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400204 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            deviceType = stores[i].Device.Type.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400204>(models, "设备告警分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400205(int start, int limit, string parent, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<Model400205>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400205>()
            };

            try {
                var models = this.GetHistory400205(parent, starttime, endtime);
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
        public ActionResult DownloadHistory400205(string parent, DateTime starttime, DateTime endtime) {
            try {
                var models = this.GetHistory400205(parent, starttime, endtime);
                using(var ms = _excelManager.Export<Model400205>(models, "工程项目信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400206(int start, int limit, string parent, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<Model400206>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400206>()
            };

            try {
                var models = this.GetHistory400206(parent, starttime, endtime);
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
        public ActionResult DownloadHistory400206(string parent, DateTime starttime, DateTime endtime) {
            try {
                var models = this.GetHistory400206(parent, starttime, endtime);
                using(var ms = _excelManager.Export<Model400206>(models, "工程预约信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400207(string parent, DateTime starttime, DateTime endtime, int start, int limit) {
            var data = new AjaxDataModel<List<Model400207>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400207>()
            };

            try {
                var models = this.GetHistory400207(parent, starttime, endtime);
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
        public ActionResult DownloadHistory400207(string parent, DateTime starttime, DateTime endtime) {
            try {
                var models = this.GetHistory400207(parent, starttime, endtime);
                using(var ms = _excelManager.Export<Model400207>(models, "市电停电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208(string parent, DateTime starttime, DateTime endtime, int start, int limit) {
            var data = new AjaxDataModel<List<Model400208>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400208>()
            };

            try {
                var models = this.GetHistory400208(parent, starttime, endtime);
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
        public ActionResult DownloadHistory400208(string parent, DateTime starttime, DateTime endtime) {
            try {
                var models = this.GetHistory400208(parent, starttime, endtime);
                using(var ms = _excelManager.Export<Model400208>(models, "油机发电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400301(string device, string point, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartModel>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Device) {
                        var curDevice = _workContext.Devices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                var models = _measureService.GetMeasuresInPoint(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(models.Count > 0) {
                                    data.message = "200 Ok";
                                    data.total = models.Count;

                                    for(int i = 0; i < models.Count; i++) {
                                        data.data.Add(new ChartModel {
                                            index = i + 1,
                                            name = CommonHelper.DateTimeConverter(models[i].UpdateTime),
                                            value = double.Parse(models[i].Value.ToString()),
                                            comment = curPoint.UnitState
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400302(string device, string point, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartsModel>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Device) {
                        var curDevice = _workContext.Devices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                var models = _staticService.GetValuesInPoint(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(models.Count > 0) {
                                    data.message = "200 Ok";
                                    data.total = models.Count;

                                    for(int i = 0; i < models.Count; i++) {
                                        var values = new List<ChartModel>();
                                        values.Add(new ChartModel { index = 1, name = CommonHelper.DateTimeConverter(models[i].MaxTime), value = models[i].MaxValue, comment = curPoint.UnitState });
                                        values.Add(new ChartModel { index = 2, name = "--", value = models[i].AvgValue, comment = curPoint.UnitState });
                                        values.Add(new ChartModel { index = 3, name = CommonHelper.DateTimeConverter(models[i].MinTime), value = models[i].MinValue, comment = curPoint.UnitState });

                                        data.data.Add(new ChartsModel {
                                            index = i + 1,
                                            name = CommonHelper.DateTimeConverter(models[i].StartTime),
                                            models = values,
                                            comment = CommonHelper.DateTimeConverter(models[i].EndTime)
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400303(string device, string[] points, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartsModel>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Device) {
                        var curDevice = _workContext.Devices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoints = curDevice.Protocol.Points.FindAll(p => points.Contains(p.Id));
                            for(var i = 0; i < curPoints.Count; i++) {
                                var values = _batService.GetValuesInPoint(curDevice.Current.Id, curPoints[i].Id, starttime, endtime);
                                var models = new List<ChartModel>();
                                for(var k = 0; k < values.Count; k++) {
                                    models.Add(new ChartModel {
                                        index = k + 1,
                                        name = Math.Round(values[k].ValueTime.Subtract(values[k].StartTime).TotalMinutes, 2).ToString(),
                                        value = values[k].Value,
                                        comment = curPoints[i].UnitState
                                    });
                                }

                                data.data.Add(new ChartsModel {
                                    index = i + 1,
                                    name = curPoints[i].Name,
                                    models = models
                                });
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400401(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400401>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400401>(),
                chart = new List<ChartsModel>()
            };

            try {
                var stores = this.GetCustom400401(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, data.chart);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400401 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
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
        public ActionResult DownloadCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400401>();
                var stores = this.GetCustom400401(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, null);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400401 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400401>(models, "超频告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400402(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400402>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400402>(),
                chart = new List<ChartsModel>()
            };

            try {
                var stores = this.GetCustom400402(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, data.chart);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400402 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
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
        public ActionResult DownloadCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400402>();
                var stores = this.GetCustom400402(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, null);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400402 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400402>(models, "超短告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400403(int start, int limit, string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400403>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400403>(),
                chart = new List<ChartsModel>()
            };

            try {
                var stores = this.GetCustom400403(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, data.chart);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400403 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
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
        public ActionResult DownloadCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            try {
                var models = new List<Model400403>();
                var stores = this.GetCustom400403(parent, startDate, endDate, staTypes, roomTypes, devTypes, levels, logicTypes, point, confirm, project, null);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400403 {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.SerialNo,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            levelid = (int)stores[i].Current.AlarmLevel,
                            startDate = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endDate = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            nmid = stores[i].Current.NMAlarmId,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            startValue = string.Format("{0:F2}", stores[i].Current.StartValue),
                            endValue = string.Format("{0:F2}", stores[i].Current.EndValue),
                            comment = stores[i].Current.AlarmDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400403>(models, "超长告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model400101> GetBase400101(string parent, int[] types) {
            var index = 0;
            var result = new List<Model400101>();

            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var areas = _workContext.Areas;
                if(types != null && types.Length > 0) 
                    areas = areas.FindAll(a => types.Contains(a.Current.Type.Id));

                var ordered = areas.OrderBy(a => a.Current.Type.Id);
                foreach(var current in ordered) {
                    result.Add(new Model400101 {
                        index = ++index,
                        id = current.Current.Id,
                        name = current.ToString(),
                        type = current.Current.Type.Value,
                        comment = current.Current.Comment,
                        enabled = current.Current.Enabled
                    });
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if(current != null && current.HasChildren) {
                    var children = current.Children;
                    if(types != null && types.Length > 0)
                        children = children.FindAll(a => types.Contains(a.Current.Type.Id));

                    var ordered = children.OrderBy(a => a.Current.Type.Id);
                    foreach(var child in ordered) {
                        result.Add(new Model400101 {
                            index = ++index,
                            id = child.Current.Id,
                            name = child.ToString(),
                            type = child.Current.Type.Value,
                            comment = child.Current.Comment,
                            enabled = child.Current.Enabled
                        });
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model400102> GetBase400102(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400102>();

            var stations = new List<SSHStation>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                stations = _workContext.Stations;
            } else {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if(current != null)
                    stations = _workContext.Stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            if(types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var loadTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Station, "市电引入方式");
            var powerTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Station, "供电性质");
            var stores = from station in stations
                         join lot in loadTypes on station.Current.CityElecLoadTypeId equals lot.Id into lt1
                         from def1 in lt1.DefaultIfEmpty()
                         join pot in powerTypes on station.Current.SuppPowerTypeId equals pot.Id into lt2
                         from def2 in lt2.DefaultIfEmpty()
                         orderby station.Current.Type.Id
                         select new {
                             Station = station.Current,
                             LoadType = def1 ?? new C_EnumMethod { Name = "未定义", Index = 0 },
                             PowerType = def2 ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                         };

            foreach(var store in stores) {
                result.Add(new Model400102 {
                    index = ++index,
                    id = store.Station.Id,
                    name = store.Station.Name,
                    type = store.Station.Type.Name,
                    longitude = store.Station.Longitude,
                    latitude = store.Station.Latitude,
                    altitude = store.Station.Altitude,
                    cityelecloadtype = store.LoadType.Name,
                    cityeleccap = store.Station.CityElecCap,
                    cityelecload = store.Station.CityElecLoad,
                    contact = store.Station.Contact,
                    lineradiussize = store.Station.LineRadiusSize,
                    linelength = store.Station.LineLength,
                    supppowertype = store.PowerType.Name,
                    traninfo = store.Station.TranInfo,
                    trancontno = store.Station.TranContNo,
                    tranphone = store.Station.TranPhone,
                    comment = store.Station.Comment,
                    enabled = store.Station.Enabled
                });
            }

            return result;
        }

        private List<Model400103> GetBase400103(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400103>();

            var rooms = new List<SSHRoom>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                rooms = _workContext.Rooms;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null)
                            rooms = _workContext.Rooms.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        var current = _workContext.Stations.Find(a => a.Current.Id == id);
                        if(current != null) rooms = current.Rooms;
                    }
                }
            }

            if(types != null && types.Length > 0)
                rooms = rooms.FindAll(s => types.Contains(s.Current.Type.Id));

            var parms = _enumMethodService.GetEnumsByType(EnmMethodType.Room, "产权");
            var stores = from room in rooms
                         join parm in parms on room.Current.PropertyId equals parm.Id into lt
                         from def in lt.DefaultIfEmpty()
                         orderby room.Current.Type.Id, room.Current.Name
                         select new {
                             Room = room.Current,
                             Method = def ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                         };

            foreach(var store in stores) {
                result.Add(new Model400103 {
                    index = ++index,
                    id = store.Room.Id,
                    name = store.Room.Name,
                    type = store.Room.Type.Name,
                    property = store.Method.Name,
                    address = store.Room.Address,
                    floor = store.Room.Floor,
                    length = store.Room.Length,
                    width = store.Room.Width,
                    height = store.Room.Heigth,
                    floorLoad = store.Room.FloorLoad,
                    lineHeigth = store.Room.LineHeigth,
                    square = store.Room.Square,
                    effeSquare = store.Room.EffeSquare,
                    fireFighEuip = store.Room.FireFighEuip,
                    owner = store.Room.Owner,
                    queryPhone = store.Room.QueryPhone,
                    powerSubMain = store.Room.PowerSubMain,
                    tranSubMain = store.Room.TranSubMain,
                    enviSubMain = store.Room.EnviSubMain,
                    fireSubMain = store.Room.FireFighEuip,
                    airSubMain = store.Room.AirSubMain,
                    contact = store.Room.Contact,
                    comment = store.Room.Comment,
                    enabled = store.Room.Enabled
                });
            }

            return result;
        }

        private List<Model400104> GetBase400104(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400104>();

            var devices = new List<SSHDevice>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                devices = _workContext.Devices;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null)
                            devices = _workContext.Devices.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        devices = _workContext.Devices.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms.Find(a => a.Current.Id == id);
                        if(current != null) devices = current.Devices;
                    }
                }
            }

            if(types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

            var productors = _productorService.GetProductors();
            var brands = _brandService.GetBrands();
            var suppliers = _supplierService.GetSuppliers();
            var subCompanys = _subCompanyService.GetCompanies();
            var status = _enumMethodService.GetEnumsByType(EnmMethodType.Device, "使用状态");
            var stores = from device in devices
                        join productor in productors on device.Current.ProdId equals productor.Id into lt1
                        from def1 in lt1.DefaultIfEmpty()
                        join brand in brands on device.Current.BrandId equals brand.Id into lt2
                        from def2 in lt2.DefaultIfEmpty()
                        join supplier in suppliers on device.Current.SuppId equals supplier.Id into lt3
                        from def3 in lt3.DefaultIfEmpty()
                        join company in subCompanys on device.Current.SubCompId equals company.Id into lt4
                        from def4 in lt4.DefaultIfEmpty()
                        join sts in status on device.Current.StatusId equals sts.Id into lt5
                        from def5 in lt5.DefaultIfEmpty()
                        orderby device.Current.Type.Id
                        select new {
                            Device = device.Current,
                            Productor = def1 != null ? def1.Name : string.Empty,
                            Brand = def2 != null ? def2.Name : string.Empty,
                            Supplier = def3 != null ? def3.Name : string.Empty,
                            SubCompany = def4 != null ? def4.Name : string.Empty,
                            Status = def5 ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                        };

            foreach(var store in stores) {
                result.Add(new Model400104 {
                    index = ++index,
                    id = store.Device.Id,
                    name = store.Device.Name,
                    type = store.Device.Type.Name,
                    subType = store.Device.SubType.Name,
                    sysName = store.Device.SysName,
                    sysCode = store.Device.SysCode,
                    model = store.Device.Model,
                    productor = store.Productor,
                    brand = store.Brand,
                    supplier = store.Supplier,
                    subCompany = store.SubCompany,
                    startTime = CommonHelper.DateTimeConverter(store.Device.StartTime),
                    scrapTime = CommonHelper.DateTimeConverter(store.Device.ScrapTime),
                    status = store.Status.Name,
                    contact = store.Device.Contact,
                    comment = store.Device.Comment,
                    enabled = store.Device.Enabled
                });
            }

            return result;
        }

        private List<ValStore<V_HMeasure>> GetHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string point) {
            endtime = endtime.AddSeconds(86399);

            var values = new List<V_HMeasure>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                values = _measureService.GetMeasures(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) values = _measureService.GetMeasures(starttime, endtime).FindAll(s => current.Keys.Contains(s.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        values = _measureService.GetMeasuresInStation(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Room) {
                        values = _measureService.GetMeasuresInRoom(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Device) {
                        values = _measureService.GetMeasuresInDevice(id, starttime, endtime);
                    }
                }
            }

            var stations = _workContext.Stations.AsEnumerable();
            if(statypes != null && statypes.Length > 0)
                stations = stations.Where(d => statypes.Contains(d.Current.Type.Id));

            var rooms = stations.SelectMany(s=>s.Rooms);
            if(roomtypes != null && roomtypes.Length > 0)
                rooms = rooms.Where(d => roomtypes.Contains(d.Current.Type.Id));

            var devices = rooms.SelectMany(r=>r.Devices);
            if(devtypes != null && devtypes.Length > 0)
                devices = devices.Where(d => devtypes.Contains(d.Current.SubType.Id));

            var points = _workContext.Points;
            if(devtypes != null && devtypes.Length > 0)
                points = points.FindAll(p => devtypes.Contains(p.DeviceType.Id));

            if(!string.IsNullOrWhiteSpace(point)) {
                var names = Common.SplitCondition(point);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Name, names));
            }

            var stores = (from val in values
                          join pt in points on val.PointId equals pt.Id
                          join device in devices on val.DeviceId equals device.Current.Id
                          join room in rooms on val.RoomId equals room.Current.Id
                          join station in stations on val.StationId equals station.Current.Id
                          join area in _workContext.Areas on val.AreaId equals area.Current.Id
                          select new ValStore<V_HMeasure> {
                              Current = val,
                              Point = pt,
                              Device = device.Current,
                              Room = room.Current,
                              Station = station.Current,
                              Area = area.Current,
                              AreaFullName = area.ToString()
                          }).ToList();

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project) {
            endDate = endDate.AddSeconds(86399);

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if(roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if(devTypes != null && devTypes.Length > 0)
                stores = stores.FindAll(d => devTypes.Contains(d.Device.Type.Id));

            if(logicTypes != null && logicTypes.Length > 0)
                stores = stores.FindAll(p => logicTypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(point)) {
                var names = Common.SplitCondition(point);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if(project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetHistory400203(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, starttime, endtime);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(statypes != null && statypes.Length > 0)
                stores = stores.FindAll(d => statypes.Contains(d.Station.Type.Id));

            if(roomtypes != null && roomtypes.Length > 0)
                stores = stores.FindAll(d => roomtypes.Contains(d.Room.Type.Id));

            if(devtypes != null && devtypes.Length > 0)
                stores = stores.FindAll(d => devtypes.Contains(d.Device.Type.Id));

            if(logictypes != null && logictypes.Length > 0)
                stores = stores.FindAll(p => logictypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(almlevels != null && almlevels.Length > 0)
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlarmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if(project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            return stores.OrderBy(s => s.Current.AlarmLevel).ToList();
        }

        private List<AlmStore<A_HAlarm>> GetHistory400204(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, starttime, endtime);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, starttime, endtime);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(statypes != null && statypes.Length > 0)
                stores = stores.FindAll(d => statypes.Contains(d.Station.Type.Id));

            if(roomtypes != null && roomtypes.Length > 0)
                stores = stores.FindAll(d => roomtypes.Contains(d.Room.Type.Id));

            if(devtypes != null && devtypes.Length > 0)
                stores = stores.FindAll(d => devtypes.Contains(d.Device.Type.Id));

            if(logictypes != null && logictypes.Length > 0)
                stores = stores.FindAll(p => logictypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(almlevels != null && almlevels.Length > 0)
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlarmLevel));

            if (confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if (confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if (project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if (project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            return stores.OrderBy(s => s.Device.Type.Id).ToList();
        }

        private List<Model400205> GetHistory400205(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400205>();
            if(!string.IsNullOrWhiteSpace(parent)) {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current  != null) {
                            var projects = _projectService.GetPagedProjectsInSpan(starttime, endtime);
                            var appSets = this.GetReservationsInDevices(projects);
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.Devices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<M_Reservation>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var appGroups = from app in appointments
                                                    group app by app.ProjectId into g
                                                    select new {
                                                        Key = g.Key,
                                                        Appointments = g.AsEnumerable()
                                                    };

                                    var proDetail = from pro in projects
                                                    join ags in appGroups on pro.Id equals ags.Key
                                                    select new {
                                                        Project = pro,
                                                        Appointments = ags.Appointments,
                                                        ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalSeconds,
                                                        AppointMaxTime = ags.Appointments.Max(a => a.EndTime),
                                                        AppointMinTime = ags.Appointments.Min(a => a.StartTime)
                                                    };

                                    var total = proDetail.Count();
                                    var timeout = proDetail.Count(p => p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime);
                                    models.Add(new Model400205 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        count = total,
                                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(proDetail.Any() ? proDetail.Average(p => p.ProjectInterval) : 0)),
                                        timeout = timeout,
                                        rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                        projects = proDetail.Select(p => new ProjectModel {
                                            index = 0,
                                            id = p.Project.Id.ToString(),
                                            name = p.Project.Name,
                                            start = CommonHelper.DateConverter(p.Project.StartTime),
                                            end = CommonHelper.DateConverter(p.Project.EndTime),
                                            responsible = p.Project.Responsible,
                                            contact = p.Project.ContactPhone,
                                            company = p.Project.Company,
                                            creator = p.Project.Creator,
                                            createdtime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                            comment = p.Project.Comment,
                                            enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
                                        }).ToList()
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.Devices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<M_Reservation>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var appGroups = from app in appointments
                                                    group app by app.ProjectId into g
                                                    select new {
                                                        Key = g.Key,
                                                        Appointments = g.AsEnumerable()
                                                    };

                                    var proDetail = from pro in projects
                                                    join ags in appGroups on pro.Id equals ags.Key
                                                    select new {
                                                        Project = pro,
                                                        Appointments = ags.Appointments,
                                                        ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalSeconds,
                                                        AppointMaxTime = ags.Appointments.Max(a => a.EndTime),
                                                        AppointMinTime = ags.Appointments.Min(a => a.StartTime)
                                                    };

                                    var total = proDetail.Count();
                                    var timeout = proDetail.Count(p => p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime);
                                    models.Add(new Model400205 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        count = total,
                                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(proDetail.Any() ? proDetail.Average(p => p.ProjectInterval) : 0)),
                                        timeout = timeout,
                                        rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                        projects = proDetail.Select(p => new ProjectModel {
                                            index = 0,
                                            id = p.Project.Id.ToString(),
                                            name = p.Project.Name,
                                            start = CommonHelper.DateConverter(p.Project.StartTime),
                                            end = CommonHelper.DateConverter(p.Project.EndTime),
                                            responsible = p.Project.Responsible,
                                            contact = p.Project.ContactPhone,
                                            company = p.Project.Company,
                                            creator = p.Project.Creator,
                                            createdtime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                            comment = p.Project.Comment,
                                            enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
                                        }).ToList()
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmSSH.Station) {
                        #region room children
                        var current = _workContext.Stations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var projects = _projectService.GetPagedProjectsInSpan(starttime, endtime);
                            var appSets = this.GetReservationsInDevices(projects);
                            var area = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var devices = _workContext.Devices.FindAll(d => d.Current.RoomId == room.Current.Id);
                                var devSet = new HashSet<string>();
                                foreach(var device in devices) {
                                    devSet.Add(device.Current.Id);
                                }

                                var appointments = new List<M_Reservation>();
                                foreach(var appSet in appSets) {
                                    if(devSet.Overlaps(appSet.Value))
                                        appointments.Add(appSet.Id);
                                }

                                var appGroups = from app in appointments
                                                group app by app.ProjectId into g
                                                select new {
                                                    Key = g.Key,
                                                    Appointments = g.AsEnumerable()
                                                };

                                var proDetail = from pro in projects
                                                join ags in appGroups on pro.Id equals ags.Key
                                                select new {
                                                    Project = pro,
                                                    Appointments = ags.Appointments,
                                                    ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalSeconds,
                                                    AppointMaxTime = ags.Appointments.Max(a => a.EndTime),
                                                    AppointMinTime = ags.Appointments.Min(a => a.StartTime)
                                                };

                                var total = proDetail.Count();
                                var timeout = proDetail.Count(p => p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime);
                                models.Add(new Model400205 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", current.Current.Name, room.Current.Name),
                                    count = total,
                                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(proDetail.Any() ? proDetail.Average(p => p.ProjectInterval) : 0)),
                                    timeout = timeout,
                                    rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                    projects = proDetail.Select(p => new ProjectModel {
                                        index = 0,
                                        id = p.Project.Id.ToString(),
                                        name = p.Project.Name,
                                        start = CommonHelper.DateConverter(p.Project.StartTime),
                                        end = CommonHelper.DateConverter(p.Project.EndTime),
                                        responsible = p.Project.Responsible,
                                        contact = p.Project.ContactPhone,
                                        company = p.Project.Company,
                                        creator = p.Project.Creator,
                                        createdtime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                        comment = p.Project.Comment,
                                        enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
                                    }).ToList()
                                });
                            }
                        }
                        #endregion
                    }
                }
            }

            return models;
        }

        private List<Model400206> GetHistory400206(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400206>();
            if(!string.IsNullOrWhiteSpace(parent)) {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            var appSets = this.GetReservationsInDevices(starttime, endtime);
                            var projects = _projectService.GetPagedProjects();
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.Devices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<M_Reservation>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var details = (from app in appointments
                                                   join pro in projects on app.ProjectId equals pro.Id
                                                   select new ReservationModel {
                                                       index = 0,
                                                       id = app.Id.ToString(),
                                                       startDate = CommonHelper.DateTimeConverter(app.StartTime),
                                                       endDate = CommonHelper.DateTimeConverter(app.EndTime),
                                                       projectName = pro.Name,
                                                       creator = app.Creator,
                                                       createdTime = CommonHelper.DateTimeConverter(app.CreatedTime),
                                                       comment = app.Comment,
                                                       enabled = app.Enabled,
                                                   }).ToList();

                                    models.Add(new Model400206 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        count = details.Count,
                                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds))),
                                        appointments = details
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.Devices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<M_Reservation>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var details = (from app in appointments
                                                   join pro in projects on app.ProjectId equals pro.Id
                                                   select new ReservationModel {
                                                       index = 0,
                                                       id = app.Id.ToString(),
                                                       startDate = CommonHelper.DateTimeConverter(app.StartTime),
                                                       endDate = CommonHelper.DateTimeConverter(app.EndTime),
                                                       projectName = pro.Name,
                                                       creator = app.Creator,
                                                       createdTime = CommonHelper.DateTimeConverter(app.CreatedTime),
                                                       comment = app.Comment,
                                                       enabled = app.Enabled,
                                                   }).ToList();

                                    models.Add(new Model400206 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds))),
                                        count = details.Count,
                                        appointments = details
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmSSH.Station) {
                        #region room children
                        var current = _workContext.Stations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var appSets = this.GetReservationsInDevices(starttime, endtime);
                            var projects = _projectService.GetPagedProjects();
                            var area = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var devices = _workContext.Devices.FindAll(d => d.Current.RoomId == room.Current.Id);
                                var devSet = new HashSet<string>();
                                foreach(var device in devices)
                                    devSet.Add(device.Current.Id);

                                var appointments = new List<M_Reservation>();
                                foreach(var appSet in appSets) {
                                    if(devSet.Overlaps(appSet.Value))
                                        appointments.Add(appSet.Id);
                                }

                                var details = (from app in appointments
                                               join pro in projects on app.ProjectId equals pro.Id
                                               select new ReservationModel {
                                                   index = 0,
                                                   id = app.Id.ToString(),
                                                   startDate = CommonHelper.DateTimeConverter(app.StartTime),
                                                   endDate = CommonHelper.DateTimeConverter(app.EndTime),
                                                   projectName = pro.Name,
                                                   creator = app.Creator,
                                                   createdTime = CommonHelper.DateTimeConverter(app.CreatedTime),
                                                   comment = app.Comment,
                                                   enabled = app.Enabled,
                                               }).ToList();

                                models.Add(new Model400206 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", current.Current.Name, room.Current.Name),
                                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds))),
                                    count = details.Count,
                                    appointments = details
                                });
                            }
                        }
                        #endregion
                    }
                }
            }

            return models;
        }

        private List<Model400207> GetHistory400207(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400207>();
            if(!string.IsNullOrWhiteSpace(parent)) {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    var rtValues = _workContext.RtValues;
                    if(rtValues != null && !string.IsNullOrWhiteSpace(rtValues.tingDianXinHao)) {
                        var index = 0;
                        var alarms = _hisAlarmService.GetAlarmsInPoint(rtValues.tingDianXinHao, starttime, endtime);
                        var stations = _workContext.Stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var station in stations) {
                            var details = alarms.FindAll(a => a.StationId == station.Current.Id);
                            var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);

                            models.Add(new Model400207 {
                                index = ++index,
                                area = area != null ? area.ToString() : "",
                                name = station.Current.Name,
                                type = station.Current.Type.Name,
                                count = details.Count,
                                interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                                details = details.Select(d => new ShiDianModel {
                                    start = CommonHelper.DateTimeConverter(d.StartTime),
                                    end = CommonHelper.DateTimeConverter(d.EndTime),
                                    timespan = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                                }).ToList()
                            });
                        }
                    }
                }
            }

            return models;
        }

        private List<Model400208> GetHistory400208(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400208>();
            if(!string.IsNullOrWhiteSpace(parent)) {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if(current != null) {
                    var rtValues = _workContext.RtValues;
                    if(rtValues != null && !string.IsNullOrWhiteSpace(rtValues.faDianXinHao)) {
                        var index = 0;
                        var alarms = _hisAlarmService.GetAlarmsInPoint(rtValues.faDianXinHao, starttime, endtime);
                        var stations = _workContext.Stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var station in stations) {
                            var details = alarms.FindAll(a => a.StationId == station.Current.Id);
                            var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);

                            models.Add(new Model400208 {
                                index = ++index,
                                area = area != null ? area.ToString() : "",
                                name = station.Current.Name,
                                type = station.Current.Type.Name,
                                count = details.Count,
                                interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                                details = details.Select(d => new ShiDianModel {
                                    start = CommonHelper.DateTimeConverter(d.StartTime),
                                    end = CommonHelper.DateTimeConverter(d.EndTime),
                                    timespan = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                                }).ToList()
                            });
                        }
                    }
                }
            }

            return models;
        }

        private List<IdValuePair<M_Reservation, HashSet<string>>> GetReservationsInDevices(DateTime start, DateTime end) {
            var entities = _reservationService.GetPagedReservationsInSpan(start, end);
            return this.GetReservationsInDevices(entities);
        }

        private List<IdValuePair<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Project> projects) {
            var matchs = projects.Select(p => p.Id);
            var reservations = _reservationService.GetPagedReservations().Where(a => matchs.Contains(a.ProjectId));
            return this.GetReservationsInDevices(reservations);
        }

        private List<IdValuePair<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Reservation> entities) {
            var appSets = new List<IdValuePair<M_Reservation, HashSet<string>>>();
            foreach(var entity in entities) {
                var appSet = new IdValuePair<M_Reservation, HashSet<string>>() { Id = entity, Value = new HashSet<string>() };
                var nodes = _nodesInReservationService.GetNodesInReservationsInReservation(entity.Id);
                foreach(var node in nodes) {
                    if(node.NodeType == EnmSSH.Device) {
                        appSet.Value.Add(node.NodeId);
                    }

                    if(node.NodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms.Find(r => r.Current.Id == node.NodeId);
                        if(current != null) {
                            foreach(var device in current.Devices) {
                                appSet.Value.Add(device.Current.Id);
                            }
                        }
                    }

                    if(node.NodeType == EnmSSH.Station) {
                        var devices = _workContext.Devices.FindAll(d => d.Current.StationId == node.NodeId);
                        foreach(var device in devices) {
                            appSet.Value.Add(device.Current.Id);
                        }
                    }

                    if(node.NodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == node.NodeId);
                        if(current != null) {
                            var devices = _workContext.Devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                            foreach(var device in devices) {
                                appSet.Value.Add(device.Current.Id);
                            }
                        }
                    }
                }

                appSets.Add(appSet);
            }

            return appSets;
        }

        private List<AlmStore<A_HAlarm>> GetCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project, List<ChartsModel> charts) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return new List<AlmStore<A_HAlarm>>();

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if(roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if(devTypes != null && devTypes.Length > 0)
                stores = stores.FindAll(d => devTypes.Contains(d.Device.Type.Id));

            if(logicTypes != null && logicTypes.Length > 0)
                stores = stores.FindAll(p => logicTypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(point)) {
                var names = Common.SplitCondition(point);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if(project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));


            var total1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var total2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var total3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var total4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            stores = (from store in stores
                      group store by new { store.Current.DeviceId, store.Current.PointId } into g
                      where g.Count() >= rtValues.chaoPin
                      select g).SelectMany(a => a).ToList();

            var abnormal1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var abnormal2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var abnormal3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var abnormal4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            if(charts != null) {
                var models1 = new List<ChartModel>();
                models1.Add(new ChartModel { index = 0, value = abnormal1 });
                models1.Add(new ChartModel { index = 1, value = total1 - abnormal1 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level1, models = models1 });

                var models2 = new List<ChartModel>();
                models2.Add(new ChartModel { index = 0, value = abnormal2 });
                models2.Add(new ChartModel { index = 1, value = total2 - abnormal2 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level2, models = models2 });

                var models3 = new List<ChartModel>();
                models3.Add(new ChartModel { index = 0, value = abnormal3 });
                models3.Add(new ChartModel { index = 1, value = total3 - abnormal3 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level3, models = models3 });

                var models4 = new List<ChartModel>();
                models4.Add(new ChartModel { index = 0, value = abnormal4 });
                models4.Add(new ChartModel { index = 1, value = total4 - abnormal4 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level4, models = models4 });
            }

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project, List<ChartsModel> charts) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return new List<AlmStore<A_HAlarm>>();

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if(roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if(devTypes != null && devTypes.Length > 0)
                stores = stores.FindAll(d => devTypes.Contains(d.Device.Type.Id));

            if(logicTypes != null && logicTypes.Length > 0)
                stores = stores.FindAll(p => logicTypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(point)) {
                var names = Common.SplitCondition(point);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if(project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));


            var total1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var total2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var total3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var total4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            var seconds = rtValues.chaoDuan * 60;
            stores = stores.FindAll(a => Math.Abs(a.Current.EndTime.Subtract(a.Current.StartTime).TotalSeconds) <= seconds);

            var abnormal1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var abnormal2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var abnormal3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var abnormal4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            if(charts != null) {
                var models1 = new List<ChartModel>();
                models1.Add(new ChartModel { index = 0, value = abnormal1 });
                models1.Add(new ChartModel { index = 1, value = total1 - abnormal1 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level1, models = models1 });

                var models2 = new List<ChartModel>();
                models2.Add(new ChartModel { index = 0, value = abnormal2 });
                models2.Add(new ChartModel { index = 1, value = total2 - abnormal2 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level2, models = models2 });

                var models3 = new List<ChartModel>();
                models3.Add(new ChartModel { index = 0, value = abnormal3 });
                models3.Add(new ChartModel { index = 1, value = total3 - abnormal3 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level3, models = models3 });

                var models4 = new List<ChartModel>();
                models4.Add(new ChartModel { index = 0, value = abnormal4 });
                models4.Add(new ChartModel { index = 1, value = total4 - abnormal4 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level4, models = models4 });
            }

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, int[] levels, string[] logicTypes, string point, string confirm, string project, List<ChartsModel> charts) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return new List<AlmStore<A_HAlarm>>();

            var alarms = new List<A_HAlarm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);

            if(staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if(roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if(devTypes != null && devTypes.Length > 0)
                stores = stores.FindAll(d => devTypes.Contains(d.Device.Type.Id));

            if(logicTypes != null && logicTypes.Length > 0)
                stores = stores.FindAll(p => logicTypes.Contains(p.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(point)) {
                var names = Common.SplitCondition(point);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));

            if(project == "unproject")
                stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));


            var total1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var total2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var total3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var total4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            var seconds = rtValues.chaoChang * 60;
            stores = stores.FindAll(a => Math.Abs(a.Current.EndTime.Subtract(a.Current.StartTime).TotalSeconds) >= seconds);

            var abnormal1 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
            var abnormal2 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
            var abnormal3 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
            var abnormal4 = stores.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);

            if(charts != null) {
                var models1 = new List<ChartModel>();
                models1.Add(new ChartModel { index = 0, value = abnormal1 });
                models1.Add(new ChartModel { index = 1, value = total1 - abnormal1 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level1, models = models1 });

                var models2 = new List<ChartModel>();
                models2.Add(new ChartModel { index = 0, value = abnormal2 });
                models2.Add(new ChartModel { index = 1, value = total2 - abnormal2 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level2, models = models2 });

                var models3 = new List<ChartModel>();
                models3.Add(new ChartModel { index = 0, value = abnormal3 });
                models3.Add(new ChartModel { index = 1, value = total3 - abnormal3 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level3, models = models3 });

                var models4 = new List<ChartModel>();
                models4.Add(new ChartModel { index = 0, value = abnormal4 });
                models4.Add(new ChartModel { index = 1, value = total4 - abnormal4 });
                charts.Add(new ChartsModel { index = (int)EnmAlarm.Level4, models = models4 });
            }

            return stores;
        }

        private List<ChartModel> GetHisAlmChart1(List<AlmStore<A_HAlarm>> stores) {
            var level1 = new ChartModel { index = (int)EnmAlarm.Level1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1) };
            var level2 = new ChartModel { index = (int)EnmAlarm.Level2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2) };
            var level3 = new ChartModel { index = (int)EnmAlarm.Level3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3) };
            var level4 = new ChartModel { index = (int)EnmAlarm.Level4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4) };
            return new List<ChartModel>() { level1, level2, level3, level4 };
        }

        private List<ChartModel> GetHisAlmChart2(string parent, List<AlmStore<A_HAlarm>> stores) {
            var models = new List<ChartModel>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                foreach(var root in roots) {
                    var curstores = stores.FindAll(s => root.Keys.Contains(s.Current.AreaId));
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                }
                #endregion
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        #region area
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) {
                            if(current.HasChildren) {
                                foreach(var child in current.ChildRoot) {
                                    var curstores = stores.FindAll(s => child.Keys.Contains(s.Current.AreaId));
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            } else if(current.Stations.Count > 0) {
                                foreach(var station in current.Stations) {
                                    var curstores = stores.FindAll(s => s.Current.StationId == station.Current.Id);
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = station.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = station.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = station.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = station.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Station) {
                        #region station
                        var current = _workContext.Stations.Find(s => s.Current.Id == id);
                        if(current != null && current.Rooms.Count > 0) {
                            foreach(var room in current.Rooms) {
                                var curstores = stores.FindAll(m => m.Current.RoomId == room.Current.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = room.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = room.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = room.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = room.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Room) {
                        #region room
                        var current = _workContext.Rooms.Find(r => r.Current.Id == id);
                        if(current != null && current.Devices.Count > 0) {
                            foreach(var device in current.Devices) {
                                var curstores = stores.FindAll(s => s.Current.DeviceId == device.Current.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = device.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = device.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = device.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = device.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Device) {
                        #region device
                        var current = _workContext.Devices.Find(d => d.Current.Id == id);
                        if(current != null) {
                            var curstores = stores.FindAll(s => s.Current.DeviceId == current.Current.Id);
                            models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = current.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                            models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = current.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                            models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = current.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                            models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = current.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                        }
                        #endregion
                    }
                }
            }

            return models;
        }

        #endregion

    }
}