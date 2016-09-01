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
using iPem.Site.Models.Organization;
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
        private readonly IWebLogger _webLogger;
        private readonly IAppointmentService _appointmentService;
        private readonly IDictionaryService _dictionaryService;
        private readonly INodesInAppointmentService _nodesInAppointmentService;
        private readonly IProjectService _projectService;
        private readonly IBrandService _brandService;
        private readonly IHisAlmService _hisAlmService;
        private readonly IHisBatService _hisBatService;
        private readonly IHisStaticService _hisStaticService;
        private readonly IHisValueService _hisValueService;
        private readonly IEnumMethodsService _enumMethodsService;
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
            IWebLogger webLogger,
            IAppointmentService appointmentService,
            IDictionaryService dictionaryService,
            INodesInAppointmentService nodesInAppointmentService,
            IProjectService projectService,
            IBrandService brandService,
            IHisAlmService hisAlmService,
            IHisBatService hisBatService,
            IHisStaticService hisStaticService,
            IHisValueService hisValueService,
            IEnumMethodsService enumMethodsService,
            IPointService pointService,
            IProductorService productorService,
            ISubCompanyService subCompanyService,
            ISupplierService supplierService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._appointmentService = appointmentService;
            this._dictionaryService = dictionaryService;
            this._nodesInAppointmentService = nodesInAppointmentService;
            this._projectService = projectService;
            this._brandService = brandService;
            this._hisAlmService = hisAlmService;
            this._hisBatService = hisBatService;
            this._hisStaticService = hisStaticService;
            this._hisValueService = hisValueService;
            this._enumMethodsService = enumMethodsService;
            this._pointService = pointService;
            this._productorService = productorService;
            this._subCompanyService = subCompanyService;
            this._supplierService = supplierService;
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
        public ActionResult History(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("history{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Chart(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("chart{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if(id.HasValue && _workContext.Menus.Any(m => m.Id == id.Value))
                return View(string.Format("custom{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400101(string parent, int[] types, int start, int limit) {
            var data = new AjaxChartModel<List<Model400101>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400101>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400101(parent, types);
                if(models != null && models.Count > 0) {
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
                using(var ms = _excelManager.Export<Model400101>(models, "区域统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                if(models != null && models.Count > 0) {
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
                using(var ms = _excelManager.Export<Model400102>(models, "站点统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                if(models != null && models.Count > 0) {
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
                using(var ms = _excelManager.Export<Model400103>(models, "机房统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                if(models != null && models.Count > 0) {
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
                using(var ms = _excelManager.Export<Model400104>(models, "设备统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname,int start, int limit) {
            var data = new AjaxDataModel<List<Model400201>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400201>()
            };

            try {
                var stores = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, logictypes, pointname);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400201 {
                            index = start + i + 1,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            devName = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            type = Common.GetPointTypeDisplay(stores[i].Point.Type),
                            value = Common.GetValueDisplay(stores[i].Point.Type, stores[i].Current.Value, stores[i].Point.Unit),
                            timestamp = CommonHelper.DateTimeConverter(stores[i].Current.Time),
                            status = (int)stores[i].Current.State,
                            statusDisplay = Common.GetPointStatusDisplay(stores[i].Current.State)
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
        public ActionResult DownloadHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname) {
            try {
                var models = new List<Model400201>();
                var stores = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, logictypes, pointname);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400201 {
                            index = i + 1,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            devName = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            type = Common.GetPointTypeDisplay(stores[i].Point.Type),
                            value = Common.GetValueDisplay(stores[i].Point.Type, stores[i].Current.Value, stores[i].Point.Unit),
                            timestamp = CommonHelper.DateTimeConverter(stores[i].Current.Time),
                            status = (int)stores[i].Current.State,
                            statusDisplay = Common.GetPointStatusDisplay(stores[i].Current.State),
                            background = Common.GetPointStatusColor(stores[i].Current.State)
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400201>(models, "历史测值列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var stores = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(stores);
                    data.chart[2] = this.GetHisAlmChart3(parent, stores);
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
        public ActionResult DownloadHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400202>();
                var stores = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400202 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400202>(models, "历史告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400203(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<Model400203>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400203>(),
                chart = new List<ChartModel>()
            };

            try {
                var stores = this.GetHistory400203(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400203 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    var groups = from store in stores
                                 group store by store.Current.AlmLevel into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = Common.GetAlarmLevelDisplay(group.Key),
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
        public ActionResult DownloadHistory400203(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400203>();
                var stores = this.GetHistory400203(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400203 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400203>(models, "历史告警分类列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400204(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<Model400204>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400204>(),
                chart = new List<ChartModel>()
            };

            try {
                var stores = this.GetHistory400204(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400204 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    var groups = from store in stores
                                 group store by store.Device.Type into g
                                 select new {
                                     Key = g.Key.Name,
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400204(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400204>();
                var stores = this.GetHistory400204(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400204 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400204>(models, "设备告警分类列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400205(string parent, DateTime starttime, DateTime endtime, int start, int limit) {
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
                using(var ms = _excelManager.Export<Model400205>(models, "工程项目统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400206(string parent, DateTime starttime, DateTime endtime, int start, int limit) {
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
                using(var ms = _excelManager.Export<Model400206>(models, "工程预约统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                using(var ms = _excelManager.Export<Model400207>(models, "市电停电统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                using(var ms = _excelManager.Export<Model400208>(models, "油机发电统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400301(string device, string point, DateTime starttime, DateTime endtime, int start, int limit) {
            var data = new AjaxChartModel<List<Model400301>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400301>(),
                chart = new List<ChartModel>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                var models = _hisValueService.GetValuesAsList(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(models.Count > 0) {
                                    data.message = "200 Ok";
                                    data.total = models.Count;

                                    var end = start + limit;
                                    if(end > models.Count)
                                        end = models.Count;

                                    for(int i = start; i < end; i++) {
                                        data.data.Add(new Model400301 {
                                            index = start + i + 1,
                                            value = Common.GetValueDisplay(curPoint.Type, models[i].Value, curPoint.Unit),
                                            time = CommonHelper.DateTimeConverter(models[i].Time),
                                            threshold = Common.GetValueDisplay(curPoint.Type, models[i].Threshold, curPoint.Unit),
                                            state = (int)models[i].State,
                                            stateDisplay = Common.GetPointStatusDisplay(models[i].State)
                                        });
                                    }

                                    for(var i = 0; i < models.Count; i++) {
                                        data.chart.Add(new ChartModel {
                                            index = i + 1,
                                            name = CommonHelper.DateTimeConverter(models[i].Time),
                                            value = models[i].Value,
                                            comment = Common.GetValueDisplay(curPoint.Type, models[i].Value, curPoint.Unit)
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

        [HttpPost]
        [Authorize]
        public ActionResult DownloadChart400301(string device, string point, DateTime starttime, DateTime endtime) {
            try {
                var title = "信号测值列表";
                var models = new List<Model400301>();

                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                title = string.Format("{0} - {1}", curPoint.Name, title);
                                var values = _hisValueService.GetValuesAsList(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(values.Count > 0) {
                                    for(int i = 0; i < values.Count; i++) {
                                        models.Add(new Model400301 {
                                            index = i + 1,
                                            value = Common.GetValueDisplay(curPoint.Type, values[i].Value, curPoint.Unit),
                                            time = CommonHelper.DateTimeConverter(values[i].Time),
                                            threshold = Common.GetValueDisplay(curPoint.Type, values[i].Threshold, curPoint.Unit),
                                            state = (int)values[i].State,
                                            stateDisplay = Common.GetPointStatusDisplay(values[i].State),
                                            background = Common.GetPointStatusColor(values[i].State)
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                using(var ms = _excelManager.Export<Model400301>(models, title, string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400302(string device, string point, DateTime starttime, DateTime endtime, int start, int limit) {
            var data = new AjaxChartModel<List<Model400302>, List<Model400302>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400302>(),
                chart = new List<Model400302>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                var models = _hisStaticService.GetValuesAsList(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(models.Count > 0) {
                                    data.message = "200 Ok";
                                    data.total = models.Count;

                                    var end = start + limit;
                                    if(end > models.Count)
                                        end = models.Count;

                                    for(int i = start; i < end; i++) {
                                        data.data.Add(new Model400302 {
                                            index = start + i + 1,
                                            start = CommonHelper.DateTimeConverter(models[i].BeginTime),
                                            end = CommonHelper.DateTimeConverter(models[i].EndTime),
                                            maxvalue = models[i].MaxValue,
                                            maxdisplay = Common.GetValueDisplay(curPoint.Type, models[i].MaxValue, curPoint.Unit),
                                            maxtime = CommonHelper.DateTimeConverter(models[i].MaxTime),
                                            minvalue = models[i].MinValue,
                                            mindisplay = Common.GetValueDisplay(curPoint.Type, models[i].MinValue, curPoint.Unit),
                                            mintime = CommonHelper.DateTimeConverter(models[i].MinTime),
                                            avgvalue = models[i].AvgValue,
                                            avgdisplay = Common.GetValueDisplay(curPoint.Type, models[i].AvgValue, curPoint.Unit),
                                            total = models[i].Total
                                        });
                                    }

                                    for(var i = 0; i < models.Count; i++) {
                                        data.chart.Add(new Model400302 {
                                            index = i + 1,
                                            start = CommonHelper.DateTimeConverter(models[i].BeginTime),
                                            end = CommonHelper.DateTimeConverter(models[i].EndTime),
                                            maxvalue = models[i].MaxValue,
                                            maxdisplay = Common.GetValueDisplay(curPoint.Type, models[i].MaxValue, curPoint.Unit),
                                            maxtime = CommonHelper.DateTimeConverter(models[i].MaxTime),
                                            minvalue = models[i].MinValue,
                                            mindisplay = Common.GetValueDisplay(curPoint.Type, models[i].MinValue, curPoint.Unit),
                                            mintime = CommonHelper.DateTimeConverter(models[i].MinTime),
                                            avgvalue = models[i].AvgValue,
                                            avgdisplay = Common.GetValueDisplay(curPoint.Type, models[i].AvgValue, curPoint.Unit),
                                            total = models[i].Total
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

        [HttpPost]
        [Authorize]
        public ActionResult DownloadChart400302(string device, string point, DateTime starttime, DateTime endtime) {
            try {
                var title = "信号测值统计列表";
                var models = new List<Model400302>();

                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                            if(curPoint != null) {
                                title = string.Format("{0} - {1}", curPoint.Name, title);
                                var values = _hisStaticService.GetValuesAsList(curDevice.Current.Id, curPoint.Id, starttime, endtime);
                                if(values != null && values.Count > 0) {
                                    for(int i = 0; i < values.Count; i++) {
                                        models.Add(new Model400302 {
                                            index = i + 1,
                                            start = CommonHelper.DateTimeConverter(values[i].BeginTime),
                                            end = CommonHelper.DateTimeConverter(values[i].EndTime),
                                            maxvalue = values[i].MaxValue,
                                            maxdisplay = Common.GetValueDisplay(curPoint.Type, values[i].MaxValue, curPoint.Unit),
                                            maxtime = CommonHelper.DateTimeConverter(values[i].MaxTime),
                                            minvalue = values[i].MinValue,
                                            mindisplay = Common.GetValueDisplay(curPoint.Type, values[i].MinValue, curPoint.Unit),
                                            mintime = CommonHelper.DateTimeConverter(values[i].MinTime),
                                            avgvalue = values[i].AvgValue,
                                            avgdisplay = Common.GetValueDisplay(curPoint.Type, values[i].AvgValue, curPoint.Unit),
                                            total = values[i].Total
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                using(var ms = _excelManager.Export<Model400302>(models, title, string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestData400303(string device, string[] points, DateTime starttime, DateTime endtime, int start, int limit) {
            var data = new AjaxDataModel<List<Model400303>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400303>()
            };

            try {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoints = curDevice.Protocol.Points.FindAll(p => points.Contains(p.Id));
                            if(curPoints.Count > 0) {
                                var models = new List<IdValuePair<Point, HisBat>>();
                                foreach(var current in curPoints) {
                                    var values = _hisBatService.GetHisBatsAsList(curDevice.Current.Id, current.Id, starttime, endtime);
                                    foreach(var val in values) {
                                        models.Add(new IdValuePair<Point, HisBat>(current, val));
                                    }
                                }

                                if(models.Count > 0) {
                                    data.message = "200 Ok";
                                    data.total = models.Count;

                                    var end = start + limit;
                                    if(end > models.Count)
                                        end = models.Count;

                                    for(int i = start; i < end; i++) {
                                        data.data.Add(new Model400303 {
                                            index = start + i + 1,
                                            point = models[i].Id.Name,
                                            start = CommonHelper.DateTimeConverter(models[i].Value.StartTime),
                                            value = Common.GetValueDisplay(models[i].Id.Type, models[i].Value.Value, models[i].Id.Unit),
                                            time = CommonHelper.DateTimeConverter(models[i].Value.ValueTime)
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
            var data = new AjaxDataModel<string> {
                success = true,
                message = "无数据",
                total = 0,
                data = string.Empty
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadData400303(string device, string[] points, DateTime starttime, DateTime endtime) {
            try {
                var models = new List<Model400303>();
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoints = curDevice.Protocol.Points.FindAll(p => points.Contains(p.Id));
                            if(curPoints.Count > 0) {
                                var values = new List<IdValuePair<Point, HisBat>>();
                                foreach(var current in curPoints) {
                                    var curValues = _hisBatService.GetHisBatsAsList(curDevice.Current.Id, current.Id, starttime, endtime);
                                    foreach(var val in curValues) {
                                        values.Add(new IdValuePair<Point, HisBat>(current, val));
                                    }
                                }

                                for(int i = 0; i < values.Count; i++) {
                                    models.Add(new Model400303 {
                                        index = i + 1,
                                        point = values[i].Id.Name,
                                        start = CommonHelper.DateTimeConverter(values[i].Value.StartTime),
                                        value = Common.GetValueDisplay(values[i].Id.Type, values[i].Value.Value, values[i].Id.Unit),
                                        time = CommonHelper.DateTimeConverter(values[i].Value.ValueTime)
                                    });
                                }
                            }
                        }
                    }
                }

                using(var ms = _excelManager.Export<Model400303>(models, "电池放电测值列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400401(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<Model400401>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400401>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var stores = this.GetCustom400401(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400401 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            key = string.Format("{0}.{1}.{2}", stores[i].Room.Name, stores[i].Device.Name, stores[i].Point.Name),
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime,stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(stores);
                    data.chart[2] = this.GetHisAlmChart3(parent, stores);
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
        public ActionResult DownloadCustom400401(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400401>();
                var stores = this.GetCustom400401(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400401 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            key = string.Format("{0}.{1}.{2}", stores[i].Room.Name, stores[i].Device.Name, stores[i].Point.Name),
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400401>(models, "超频告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400402(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<Model400402>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400402>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var stores = this.GetCustom400402(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400402 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(stores);
                    data.chart[2] = this.GetHisAlmChart3(parent, stores);
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
        public ActionResult DownloadCustom400402(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400402>();
                var stores = this.GetCustom400402(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400402 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400402>(models, "超短告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400403(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400403>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400403>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var stores = this.GetCustom400403(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400403 {
                            index = start + i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(stores);
                    data.chart[2] = this.GetHisAlmChart3(parent, stores);
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
        public ActionResult DownloadCustom400403(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400403>();
                var stores = this.GetCustom400403(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400403 {
                            index = i + 1,
                            id = stores[i].Current.Id,
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            devType = stores[i].Device.Type.Name,
                            device = stores[i].Device.Name,
                            logic = stores[i].Point.LogicType.Name,
                            point = stores[i].Point.Name,
                            levelValue = (int)stores[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", stores[i].Current.EndValue, stores[i].Current.ValueUnit),
                            almComment = stores[i].Current.AlmDesc,
                            normalComment = stores[i].Current.NormalDesc,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            frequency = stores[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(stores[i].Current.EndType),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmedStatus = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirmStatus.Unconfirmed),
                            confirmedTime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400403>(models, "超长告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
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
                var areas = _workContext.RoleAreas;
                if(types.Length > 0) areas = areas.FindAll(a => types.Contains(a.Current.Type.Id));
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
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null && current.HasChildren) {
                    var ordered = current.Children.OrderBy(a => a.Current.Type.Id);
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

            var stations = new List<OrgStation>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                stations = _workContext.RoleStations;
            } else {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == parent);
                if(current != null)
                    stations = _workContext.RoleStations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            if(types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var loadTypes = _enumMethodsService.GetValuesAsList(EnmMethodType.Station, "市电引入方式");
            var powerTypes = _enumMethodsService.GetValuesAsList(EnmMethodType.Station, "供电性质");
            var stores = from station in stations
                         join lot in loadTypes on station.Current.CityElecLoadTypeId equals lot.Id into lt1
                         from def1 in lt1.DefaultIfEmpty()
                         join pot in powerTypes on station.Current.SuppPowerTypeId equals pot.Id into lt2
                         from def2 in lt2.DefaultIfEmpty()
                         orderby station.Current.Type.Id
                         select new {
                             Station = station.Current,
                             LoadType = def1 ?? new EnumMethods { Name = "未定义", Index = 0 },
                             PowerType = def2 ?? new EnumMethods { Name = "未定义", Index = 0 }
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

            var rooms = new List<OrgRoom>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                rooms = _workContext.RoleRooms;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            rooms = _workContext.RoleRooms.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        var current = _workContext.RoleStations.Find(a => a.Current.Id == id);
                        if(current != null) rooms = current.Rooms;
                    }
                }
            }

            if(types != null && types.Length > 0)
                rooms = rooms.FindAll(s => types.Contains(s.Current.Type.Id));

            var parms = _enumMethodsService.GetValuesAsList(EnmMethodType.Room, "产权");
            var stores = from room in rooms
                         join parm in parms on room.Current.PropertyId equals parm.Id into lt
                         from def in lt.DefaultIfEmpty()
                         orderby room.Current.Type.Id, room.Current.Name
                         select new {
                             Room = room.Current,
                             Method = def ?? new EnumMethods { Name = "未定义", Index = 0 }
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

            var devices = new List<OrgDevice>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                devices = _workContext.RoleDevices;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            devices = _workContext.RoleDevices.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmOrganization.Room) {
                        var current = _workContext.RoleRooms.Find(a => a.Current.Id == id);
                        if(current != null) devices = current.Devices;
                    }
                }
            }

            if(types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

            var productors = _productorService.GetAllProductorsAsList();
            var brands = _brandService.GetAllBrandsAsList();
            var suppliers = _supplierService.GetAllSuppliersAsList();
            var subCompanys = _subCompanyService.GetAllSubCompaniesAsList();
            var status = _enumMethodsService.GetValuesAsList(EnmMethodType.Device, "使用状态");
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
                            Status = def5 ?? new EnumMethods { Name = "未定义", Index = 0 }
                        };

            foreach(var store in stores) {
                result.Add(new Model400104 {
                    index = ++index,
                    id = store.Device.Id,
                    code = store.Device.Code,
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

        private List<ValStore<HisValue>> GetHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname) {
            endtime = endtime.AddSeconds(86399);

            var stations = _workContext.RoleStations.AsEnumerable();
            if(statypes != null && statypes.Length > 0)
                stations = stations.Where(d => statypes.Contains(d.Current.Type.Id));

            var rooms = stations.SelectMany(s=>s.Rooms);
            if(roomtypes != null && roomtypes.Length > 0)
                rooms = rooms.Where(d => roomtypes.Contains(d.Current.Type.Id));

            var devices = rooms.SelectMany(r=>r.Devices);
            if(devtypes != null && devtypes.Length > 0)
                devices = devices.Where(d => devtypes.Contains(d.Current.Type.Id));

            var points = _workContext.Points;
            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Name, names));
            }

            var values = _hisValueService.GetValuesAsList(starttime, endtime);
            var stores = (from val in values
                          join point in points on val.PointId equals point.Id
                          join device in devices on val.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on device.Current.StationId equals station.Current.Id
                          join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                          select new ValStore<HisValue> {
                              Current = val,
                              Point = point,
                              Device = device.Current,
                              Room = room.Current,
                              Station = station.Current,
                              Area = area.Current,
                              AreaFullName = area.ToString()
                          }).ToList();

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            stores = stores.FindAll(s => current.Keys.Contains(s.Area.Id));
                    } else if(nodeType == EnmOrganization.Station) {
                        stores = stores.FindAll(d => d.Station.Id == id);
                    } else if(nodeType == EnmOrganization.Room) {
                        stores = stores.FindAll(d => d.Room.Id == id);
                    } else if(nodeType == EnmOrganization.Device) {
                        stores = stores.FindAll(d => d.Device.Id == id);
                    }
                }
            }

            return stores;
        }

        private List<AlmStore<HisAlm>> GetHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<AlmStore<HisAlm>> GetHistory400203(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<AlmStore<HisAlm>> GetHistory400204(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<Model400205> GetHistory400205(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400205>();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var index = 0;
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current  != null) {
                            var projects = _projectService.GetProjects(starttime, endtime);
                            var appSets = this.GetAppointmentsInDevices(projects);
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.RoleDevices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<Appointment>();
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
                                                        ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalMinutes,
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
                                        interval = proDetail.Any() ? Math.Round(proDetail.Average(p => p.ProjectInterval), 2) : 0,
                                        timeout = timeout,
                                        rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                        projects = proDetail.Select(p => new ProjectModel {
                                            Index = 0,
                                            Id = p.Project.Id.ToString(),
                                            Name = p.Project.Name,
                                            StartTime = CommonHelper.DateConverter(p.Project.StartTime),
                                            EndTime = CommonHelper.DateConverter(p.Project.EndTime),
                                            Responsible = p.Project.Responsible,
                                            ContactPhone = p.Project.ContactPhone,
                                            Company = p.Project.Company,
                                            Creator = p.Project.Creator,
                                            CreatedTime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                            Comment = p.Project.Comment,
                                            Enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
                                        }).ToList()
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<Appointment>();
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
                                                        ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalMinutes,
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
                                        interval = proDetail.Any() ? Math.Round(proDetail.Average(p => p.ProjectInterval), 2) : 0,
                                        timeout = timeout,
                                        rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                        projects = proDetail.Select(p => new ProjectModel {
                                            Index = 0,
                                            Id = p.Project.Id.ToString(),
                                            Name = p.Project.Name,
                                            StartTime = CommonHelper.DateConverter(p.Project.StartTime),
                                            EndTime = CommonHelper.DateConverter(p.Project.EndTime),
                                            Responsible = p.Project.Responsible,
                                            ContactPhone = p.Project.ContactPhone,
                                            Company = p.Project.Company,
                                            Creator = p.Project.Creator,
                                            CreatedTime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                            Comment = p.Project.Comment,
                                            Enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
                                        }).ToList()
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var projects = _projectService.GetProjects(starttime, endtime);
                            var appSets = this.GetAppointmentsInDevices(projects);
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var devices = _workContext.RoleDevices.FindAll(d => d.Current.RoomId == room.Current.Id);
                                var devSet = new HashSet<string>();
                                foreach(var device in devices) {
                                    devSet.Add(device.Current.Id);
                                }

                                var appointments = new List<Appointment>();
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
                                                    ProjectInterval = pro.EndTime.Subtract(pro.StartTime).TotalMinutes,
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
                                    interval = proDetail.Any() ? Math.Round(proDetail.Average(p => p.ProjectInterval), 2) : 0,
                                    timeout = timeout,
                                    rate = string.Format("{0:P2}", total > 0 ? (double)timeout / (double)total : 0),
                                    projects = proDetail.Select(p => new ProjectModel {
                                        Index = 0,
                                        Id = p.Project.Id.ToString(),
                                        Name = p.Project.Name,
                                        StartTime = CommonHelper.DateConverter(p.Project.StartTime),
                                        EndTime = CommonHelper.DateConverter(p.Project.EndTime),
                                        Responsible = p.Project.Responsible,
                                        ContactPhone = p.Project.ContactPhone,
                                        Company = p.Project.Company,
                                        Creator = p.Project.Creator,
                                        CreatedTime = CommonHelper.DateTimeConverter(p.Project.CreatedTime),
                                        Comment = p.Project.Comment,
                                        Enabled = (p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime)
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
                            var appSets = this.GetAppointmentsInDevices(starttime, endtime);
                            var projects = _projectService.GetAllProjects();
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.RoleDevices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<Appointment>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var details = (from app in appointments
                                                   join pro in projects on app.ProjectId equals pro.Id
                                                   select new AppointmentModel {
                                                       index = 0,
                                                       id = app.Id.ToString(),
                                                       startTime = CommonHelper.DateTimeConverter(app.StartTime),
                                                       endTime = CommonHelper.DateTimeConverter(app.EndTime),
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
                                        interval = Math.Round(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                        appointments = details
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var devSet = new HashSet<string>();
                                    foreach(var device in devices) {
                                        devSet.Add(device.Current.Id);
                                    }

                                    var appointments = new List<Appointment>();
                                    foreach(var appSet in appSets) {
                                        if(devSet.Overlaps(appSet.Value))
                                            appointments.Add(appSet.Id);
                                    }

                                    var details = (from app in appointments
                                                   join pro in projects on app.ProjectId equals pro.Id
                                                   select new AppointmentModel {
                                                       index = 0,
                                                       id = app.Id.ToString(),
                                                       startTime = CommonHelper.DateTimeConverter(app.StartTime),
                                                       endTime = CommonHelper.DateTimeConverter(app.EndTime),
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
                                        interval = Math.Round(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                        count = details.Count,
                                        appointments = details
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var appSets = this.GetAppointmentsInDevices(starttime, endtime);
                            var projects = _projectService.GetAllProjects();
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var devices = _workContext.RoleDevices.FindAll(d => d.Current.RoomId == room.Current.Id);
                                var devSet = new HashSet<string>();
                                foreach(var device in devices)
                                    devSet.Add(device.Current.Id);

                                var appointments = new List<Appointment>();
                                foreach(var appSet in appSets) {
                                    if(devSet.Overlaps(appSet.Value))
                                        appointments.Add(appSet.Id);
                                }

                                var details = (from app in appointments
                                               join pro in projects on app.ProjectId equals pro.Id
                                               select new AppointmentModel {
                                                   index = 0,
                                                   id = app.Id.ToString(),
                                                   startTime = CommonHelper.DateTimeConverter(app.StartTime),
                                                   endTime = CommonHelper.DateTimeConverter(app.EndTime),
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
                                    interval = Math.Round(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
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
                            var values = this.GetTingDianValues(starttime, endtime);
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.RoleDevices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var details = from value in values
                                                  join point in _workContext.Points on value.PointId equals point.Id
                                                  join device in devices on value.DeviceId equals device.Current.Id
                                                  join room in _workContext.RoleRooms on device.Current.RoomId equals room.Current.Id
                                                  join station in _workContext.RoleStations on device.Current.StationId equals station.Current.Id
                                                  join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                                  select new ShiDianDetailModel {
                                                      area = area.ToString(),
                                                      station = station.Current.Name,
                                                      room = room.Current.Name,
                                                      device = device.Current.Name,
                                                      point = point.Name,
                                                      start = CommonHelper.DateTimeConverter(value.StartTime),
                                                      end = CommonHelper.DateTimeConverter(value.EndTime),
                                                      interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                  };

                                    models.Add(new Model400207 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        count = details.Count(),
                                        interval = details.Sum(d => d.interval),
                                        details = details
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var details = from value in values
                                                  join point in _workContext.Points on value.PointId equals point.Id
                                                  join device in devices on value.DeviceId equals device.Current.Id
                                                  join room in _workContext.RoleRooms on device.Current.RoomId equals room.Current.Id
                                                  join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                                  select new ShiDianDetailModel {
                                                      area = area.ToString(),
                                                      station = station.Current.Name,
                                                      room = room.Current.Name,
                                                      device = device.Current.Name,
                                                      point = point.Name,
                                                      start = CommonHelper.DateTimeConverter(value.StartTime),
                                                      end = CommonHelper.DateTimeConverter(value.EndTime),
                                                      interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                  };

                                    models.Add(new Model400207 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        count = details.Count(),
                                        interval = details.Sum(d => d.interval),
                                        details = details
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var values = this.GetTingDianValues(starttime, endtime);
                            var parea = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var details = from value in values
                                              join point in _workContext.Points on value.PointId equals point.Id
                                              join device in room.Devices on value.DeviceId equals device.Current.Id
                                              join sta in _workContext.RoleStations on device.Current.StationId equals sta.Current.Id
                                              join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                              select new ShiDianDetailModel {
                                                  area = area.ToString(),
                                                  station = sta.Current.Name,
                                                  room = room.Current.Name,
                                                  device = device.Current.Name,
                                                  point = point.Name,
                                                  start = CommonHelper.DateTimeConverter(value.StartTime),
                                                  end = CommonHelper.DateTimeConverter(value.EndTime),
                                                  interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                              };

                                models.Add(new Model400207 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", parea != null ? parea.ToString() : "", current.Current.Name, room.Current.Name),
                                    count = details.Count(),
                                    interval = details.Sum(d => d.interval),
                                    details = details
                                });
                            }
                        }
                        #endregion
                    }
                }
            }

            return models;
        }

        private List<Model400208> GetHistory400208(string parent, DateTime starttime, DateTime endtime) {
            endtime = endtime.AddSeconds(86399);

            var models = new List<Model400208>();
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
                            var values = this.GetFaDianValues(starttime, endtime);
                            if(current.HasChildren) {
                                #region area children
                                foreach(var child in current.ChildRoot) {
                                    var devices = _workContext.RoleDevices.FindAll(d => child.Keys.Contains(d.Current.AreaId));
                                    var details = from value in values
                                                  join point in _workContext.Points on value.PointId equals point.Id
                                                  join device in devices on value.DeviceId equals device.Current.Id
                                                  join room in _workContext.RoleRooms on device.Current.RoomId equals room.Current.Id
                                                  join station in _workContext.RoleStations on device.Current.StationId equals station.Current.Id
                                                  join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                                  select new ShiDianDetailModel {
                                                      area = area.ToString(),
                                                      station = station.Current.Name,
                                                      room = room.Current.Name,
                                                      device = device.Current.Name,
                                                      point = point.Name,
                                                      start = CommonHelper.DateTimeConverter(value.StartTime),
                                                      end = CommonHelper.DateTimeConverter(value.EndTime),
                                                      interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                  };

                                    models.Add(new Model400208 {
                                        index = ++index,
                                        type = child.Current.Type.Value,
                                        name = child.ToString(),
                                        count = details.Count(),
                                        interval = details.Sum(d => d.interval),
                                        details = details
                                    });
                                }
                                #endregion
                            } else {
                                #region station children
                                foreach(var station in current.Stations) {
                                    var devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                                    var details = from value in values
                                                  join point in _workContext.Points on value.PointId equals point.Id
                                                  join device in devices on value.DeviceId equals device.Current.Id
                                                  join room in _workContext.RoleRooms on device.Current.RoomId equals room.Current.Id
                                                  join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                                  select new ShiDianDetailModel {
                                                      area = area.ToString(),
                                                      station = station.Current.Name,
                                                      room = room.Current.Name,
                                                      device = device.Current.Name,
                                                      point = point.Name,
                                                      start = CommonHelper.DateTimeConverter(value.StartTime),
                                                      end = CommonHelper.DateTimeConverter(value.EndTime),
                                                      interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                  };

                                    models.Add(new Model400208 {
                                        index = ++index,
                                        type = station.Current.Type.Name,
                                        name = string.Format("{0},{1}", current.ToString(), station.Current.Name),
                                        count = details.Count(),
                                        interval = details.Sum(d => d.interval),
                                        details = details
                                    });
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        #region room children
                        var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                        if(current != null) {
                            var values = this.GetFaDianValues(starttime, endtime);
                            var parea = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            foreach(var room in current.Rooms) {
                                var details = from value in values
                                              join point in _workContext.Points on value.PointId equals point.Id
                                              join device in room.Devices on value.DeviceId equals device.Current.Id
                                              join sta in _workContext.RoleStations on device.Current.StationId equals sta.Current.Id
                                              join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                                              select new ShiDianDetailModel {
                                                  area = area.ToString(),
                                                  station = sta.Current.Name,
                                                  room = room.Current.Name,
                                                  device = device.Current.Name,
                                                  point = point.Name,
                                                  start = CommonHelper.DateTimeConverter(value.StartTime),
                                                  end = CommonHelper.DateTimeConverter(value.EndTime),
                                                  interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                              };

                                models.Add(new Model400208 {
                                    index = ++index,
                                    type = room.Current.Type.Name,
                                    name = string.Format("{0},{1},{2}", parea != null ? parea.ToString() : "", current.Current.Name, room.Current.Name),
                                    count = details.Count(),
                                    interval = details.Sum(d => d.interval),
                                    details = details
                                });
                            }
                        }
                        #endregion
                    }
                }
            }

            return models;
        }

        private List<IdValuePair<Appointment, HashSet<string>>> GetAppointmentsInDevices(DateTime start, DateTime end) {
            var entities = _appointmentService.GetAppointments(start, end);
            return this.GetAppointmentsInDevices(entities);
        }

        private List<IdValuePair<Appointment, HashSet<string>>> GetAppointmentsInDevices(IEnumerable<Project> projects) {
            var matchs = projects.Select(p => p.Id);
            var appointments = _appointmentService.GetAllAppointments().Where(a => matchs.Contains(a.ProjectId));
            return this.GetAppointmentsInDevices(appointments);
        }

        private List<IdValuePair<Appointment, HashSet<string>>> GetAppointmentsInDevices(IEnumerable<Appointment> appointments) {
            var appSets = new List<IdValuePair<Appointment, HashSet<string>>>();
            foreach(var appointment in appointments) {
                var appSet = new IdValuePair<Appointment, HashSet<string>>() { Id = appointment, Value = new HashSet<string>() };
                var nodes = _nodesInAppointmentService.GetNodes(appointment.Id);
                foreach(var node in nodes) {
                    if(node.NodeType == EnmOrganization.Device) {
                        appSet.Value.Add(node.NodeId);
                    }

                    if(node.NodeType == EnmOrganization.Room) {
                        var current = _workContext.RoleRooms.Find(r => r.Current.Id == node.NodeId);
                        if(current != null) {
                            foreach(var device in current.Devices) {
                                appSet.Value.Add(device.Current.Id);
                            }
                        }
                    }

                    if(node.NodeType == EnmOrganization.Station) {
                        var devices = _workContext.RoleDevices.FindAll(d => d.Current.StationId == node.NodeId);
                        foreach(var device in devices) {
                            appSet.Value.Add(device.Current.Id);
                        }
                    }

                    if(node.NodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == node.NodeId);
                        if(current != null) {
                            var devices = _workContext.RoleDevices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
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

        private List<ShiDianModel> GetTingDianValues(DateTime start, DateTime end) {
            var models = new List<ShiDianModel>();

            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return models;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(limit.tingdianxinhao.Length == 0) return models;

            var values = _hisValueService.GetValuesAsList(limit.tingdianxinhao, start, end);
            var groups = from val in values
                         group val by new { val.DeviceId, val.PointId } into g
                         select new {
                             DeviceId = g.Key.DeviceId,
                             PointId = g.Key.PointId,
                             Values = g
                         };

            foreach(var group in groups) {
                DateTime? onetime = null;
                var pValues = group.Values.OrderBy(v => v.Time);

                foreach(var pv in pValues) {
                    if(pv.Value == limit.tingdian && !onetime.HasValue) {
                        onetime = pv.Time;
                    } else if(pv.Value == limit.weitingdian && onetime.HasValue) {
                        models.Add(new ShiDianModel {
                            DeviceId = group.DeviceId,
                            PointId = group.PointId,
                            StartTime = onetime.Value,
                            EndTime = pv.Time
                        });

                        onetime = null;
                    }
                }

                if(onetime.HasValue) {
                    models.Add(new ShiDianModel {
                        DeviceId = group.DeviceId,
                        PointId = group.PointId,
                        StartTime = onetime.Value,
                        EndTime = end
                    });

                    onetime = null;
                }
            }

            return models;
        }

        private List<ShiDianModel> GetFaDianValues(DateTime start, DateTime end) {
            var models = new List<ShiDianModel>();

            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return models;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(limit.fadianxinhao.Length == 0) return models;

            var values = _hisValueService.GetValuesAsList(limit.fadianxinhao, start, end);
            var groups = from val in values
                         group val by new { val.DeviceId, val.PointId } into g
                         select new {
                             DeviceId = g.Key.DeviceId,
                             PointId = g.Key.PointId,
                             Values = g
                         };

            foreach(var group in groups) {
                DateTime? onetime = null;
                var pValues = group.Values.OrderBy(v => v.Time);

                foreach(var pv in pValues) {
                    if(pv.Value == limit.fadian && !onetime.HasValue) {
                        onetime = pv.Time;
                    } else if(pv.Value == limit.weifadian && onetime.HasValue) {
                        models.Add(new ShiDianModel {
                            DeviceId = group.DeviceId,
                            PointId = group.PointId,
                            StartTime = onetime.Value,
                            EndTime = pv.Time
                        });

                        onetime = null;
                    }
                }

                if(onetime.HasValue) {
                    models.Add(new ShiDianModel {
                        DeviceId = group.DeviceId,
                        PointId = group.PointId,
                        StartTime = onetime.Value,
                        EndTime = end
                    });

                    onetime = null;
                }
            }

            return models;
        }

        private List<AlmStore<HisAlm>> GetCustom400401(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return new List<AlmStore<HisAlm>>();

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            alarms = (from alarm in alarms
                      group alarm by new { alarm.DeviceId, alarm.PointId } into g
                      where g.Count() >= limit.chaopin
                      select new { G = g }).SelectMany(a => a.G).ToList();

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<AlmStore<HisAlm>> GetCustom400402(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return new List<AlmStore<HisAlm>>();

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            var seconds = limit.chaoduan * 60;
            alarms = alarms.FindAll(a => Math.Abs(a.EndTime.Subtract(a.StartTime).TotalSeconds) <= seconds);

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<AlmStore<HisAlm>> GetCustom400403(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson)) return new List<AlmStore<HisAlm>>();

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);

            var alarms = new List<HisAlm>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null) alarms = _hisAlmService.GetAllAlmsAsList(starttime, endtime).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        alarms = _hisAlmService.GetAlmsInStationAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Room) {
                        alarms = _hisAlmService.GetAlmsInRoomAsList(id, starttime, endtime);
                    } else if(nodeType == EnmOrganization.Device) {
                        alarms = _hisAlmService.GetAlmsInDeviceAsList(id, starttime, endtime);
                    }
                }
            }

            var seconds = limit.chaochang * 60;
            alarms = alarms.FindAll(a => a.EndTime.Subtract(a.StartTime).TotalSeconds >= seconds);

            var stores = _workContext.GetHisAlmStore(alarms, starttime, endtime);

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
                stores = stores.FindAll(a => almlevels.Contains((int)a.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<ChartModel> GetHisAlmChart1(List<AlmStore<HisAlm>> stores) {
            var data = new List<ChartModel>();
            if(stores != null && stores.Count > 0) {
                var groups = stores.GroupBy(m => m.Current.AlmLevel).OrderBy(g => g.Key);
                foreach(var group in groups) {
                    data.Add(new ChartModel {
                        name = Common.GetAlarmLevelDisplay(group.Key),
                        value = group.Count(),
                        comment = ""
                    });
                }
            }

            return data;
        }

        private List<ChartModel> GetHisAlmChart2(List<AlmStore<HisAlm>> stores) {
            var data = new List<ChartModel>();
            if(stores != null && stores.Count > 0) {
                var groups = stores.GroupBy(m => m.Device.Type).OrderBy(g => g.Key.Id);
                foreach(var group in groups) {
                    data.Add(new ChartModel {
                        name = group.Key.Name,
                        value = group.Count(),
                        comment = ""
                    });
                }
            }

            return data;
        }

        private List<ChartModel> GetHisAlmChart3(string parent, List<AlmStore<HisAlm>> stores) {
            var models = new List<ChartModel>();

            if(stores != null && stores.Count > 0) {
                if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                    #region root
                    var roots = _workContext.RoleAreas.FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var count = stores.Count(s => root.Keys.Contains(s.Current.AreaId));
                        if(count > 0)
                            models.Add(new ChartModel { name = root.Current.Name, value = count, comment = "" });
                    }
                    #endregion
                } else {
                    var keys = Common.SplitKeys(parent);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Area) {
                            #region area
                            var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                if(current.HasChildren) {
                                    foreach(var child in current.ChildRoot) {
                                        var count = stores.Count(s => child.Keys.Contains(s.Current.AreaId));
                                        if(count > 0)
                                            models.Add(new ChartModel { name = child.Current.Name, value = count, comment = "" });
                                    }
                                } else if(current.Stations.Count > 0) {
                                    foreach(var station in current.Stations) {
                                        var count = stores.Count(s => s.Current.StationId == station.Current.Id);
                                        if(count > 0)
                                            models.Add(new ChartModel { name = station.Current.Name, value = count, comment = "" });
                                    }
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station
                            var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                            if(current != null && current.Rooms.Count > 0) {
                                foreach(var room in current.Rooms) {
                                    var count = stores.Count(m => m.Current.RoomId == room.Current.Id);
                                    if(count > 0)
                                        models.Add(new ChartModel { name = room.Current.Name, value = count, comment = "" });
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Room) {
                            #region room
                            var current = _workContext.RoleRooms.Find(r => r.Current.Id == id);
                            if(current != null && current.Devices.Count > 0) {
                                foreach(var device in current.Devices) {
                                    var count = stores.Count(s => s.Current.DeviceId == device.Current.Id);
                                    if(count > 0)
                                        models.Add(new ChartModel { name = device.Current.Name, value = count, comment = "" });
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Device) {
                            #region device
                            var current = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                            if(current != null) {
                                var count = stores.Count(s => s.Current.DeviceId == current.Current.Id);
                                if(count > 0)
                                    models.Add(new ChartModel { name = current.Current.Name, value = count, comment = "" });
                            }
                            #endregion
                        }
                    }
                }
            }

            return models;
        }

        #endregion

    }
}