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
using Newtonsoft.Json.Linq;

namespace iPem.Site.Controllers {
    public class ReportController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IDictionaryService _msDictionaryService;
        private readonly MsSrv.IProjectService _msProjectService;
        private readonly MsSrv.IAppointmentService _msAppointmentService;
        private readonly MsSrv.INodesInAppointmentService _msNodesInAppointmentService;
        private readonly RsSrv.IEnumMethodsService _rsEnumMethodsService;
        private readonly RsSrv.IStationTypeService _rsStationTypeService;
        private readonly RsSrv.IProductorService _rsProductorService;
        private readonly RsSrv.IBrandService _rsBrandService;
        private readonly RsSrv.ISupplierService _rsSupplierService;
        private readonly RsSrv.ISubCompanyService _rsSubCompanyService;
        private readonly HsSrv.IHisAlmService _hsHisAlmService;
        private readonly HsSrv.IHisValueService _hsHisValueService;
        private readonly HsSrv.IHisStaticService _hsHisStaticService;
        private readonly HsSrv.IHisBatService _hsHisBatService;

        #endregion

        #region Ctor

        public ReportController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IDictionaryService msDictionaryService,
            MsSrv.IProjectService msProjectService,
            MsSrv.IAppointmentService msAppointmentService,
            MsSrv.INodesInAppointmentService msNodesInAppointmentService,
            RsSrv.IEnumMethodsService rsEnumMethodsService,
            RsSrv.IStationTypeService rsStationTypeService,
            RsSrv.IProductorService rsProductorService,
            RsSrv.IBrandService rsBrandService,
            RsSrv.ISupplierService rsSupplierService,
            RsSrv.ISubCompanyService rsSubCompanyService,
            HsSrv.IHisAlmService hsHisAlmService,
            HsSrv.IHisValueService hsHisValueService,
            HsSrv.IHisStaticService hsHisStaticService,
            HsSrv.IHisBatService hsHisBatService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._msDictionaryService = msDictionaryService;
            this._msProjectService = msProjectService;
            this._msAppointmentService = msAppointmentService;
            this._msNodesInAppointmentService = msNodesInAppointmentService;
            this._rsEnumMethodsService = rsEnumMethodsService;
            this._rsStationTypeService = rsStationTypeService;
            this._rsProductorService = rsProductorService;
            this._rsBrandService = rsBrandService;
            this._rsSupplierService = rsSupplierService;
            this._rsSubCompanyService = rsSubCompanyService;
            this._hsHisAlmService = hsHisAlmService;
            this._hsHisValueService = hsHisValueService;
            this._hsHisStaticService = hsHisStaticService;
            this._hsHisBatService = hsHisBatService;
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
        public ActionResult History(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("history{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Chart(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("chart{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400101(string parent, int[] types) {
            try {
                var models = this.GetBase400101(parent, types);
                using(var ms = _excelManager.Export<Model400101>(models, "区域统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400102(string parent, string[] types) {
            try {
                var models = this.GetBase400102(parent, types);
                using(var ms = _excelManager.Export<Model400102>(models, "站点统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400103(string parent, string[] types) {
            try {
                var models = this.GetBase400103(parent, types);
                using(var ms = _excelManager.Export<Model400103>(models, "机房统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400104(string parent, string[] types) {
            try {
                var models = this.GetBase400104(parent, types);
                using(var ms = _excelManager.Export<Model400104>(models, "设备统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400201(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname) {
            var data = new AjaxDataModel<List<Model400201>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400201>()
            };

            try {
                var models = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, logictypes, pointname);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400201 {
                            index = start + i + 1,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            devName = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            type = Common.GetPointTypeDisplay(models[i].Point.Current.Type),
                            value = Common.GetValueDisplay(models[i].Point.Current.Type, models[i].Current.Value, models[i].Point.Current.Unit),
                            timestamp = CommonHelper.DateTimeConverter(models[i].Current.Time),
                            status = (int)models[i].Current.State,
                            statusDisplay = Common.GetPointStatusDisplay(models[i].Current.State)
                        });
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
        public ActionResult DownloadHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname) {
            try {
                var models = new List<Model400201>();
                var cached = this.GetHistory400201(parent, starttime, endtime, statypes, roomtypes, devtypes, logictypes, pointname);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400201 {
                            index = i + 1,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            devName = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            type = Common.GetPointTypeDisplay(cached[i].Point.Current.Type),
                            value = Common.GetValueDisplay(cached[i].Point.Current.Type, cached[i].Current.Value, cached[i].Point.Current.Unit),
                            timestamp = CommonHelper.DateTimeConverter(cached[i].Current.Time),
                            status = (int)cached[i].Current.State,
                            statusDisplay = Common.GetPointStatusDisplay(cached[i].Current.State),
                            background = Common.GetPointStatusColor(cached[i].Current.State)
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400201>(models, "历史测值列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400202(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var models = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(models);
                    data.chart[1] = this.GetHisAlmChart2(models);
                    data.chart[2] = this.GetHisAlmChart3(parent, models);
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
        public ActionResult DownloadHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400202>();
                var cached = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400202 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400202>(models, "历史告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400203(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400203>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400203>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetHistory400203(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400203 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    var groups = from model in models
                                 group model by model.Current.AlmLevel into g
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var cached = this.GetHistory400203(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400203 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400203>(models, "历史告警分类列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400204(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400204>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400204>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetHistory400204(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400204 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    var groups = from model in models
                                 group model by model.Device.Current.DeviceTypeId into g
                                 select new {
                                     Key = g.First().Device.Type.Name,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var cached = this.GetHistory400204(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400204 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400204>(models, "设备告警分类列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                using(var ms = _excelManager.Export<Model400205>(models, "工程项目统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                using(var ms = _excelManager.Export<Model400206>(models, "工程预约统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400207(int start, int limit, string parent, DateTime starttime, DateTime endtime) {
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                using(var ms = _excelManager.Export<Model400207>(models, "市电停电统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208(int start, int limit, string parent, DateTime starttime, DateTime endtime) {
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                using(var ms = _excelManager.Export<Model400208>(models, "油机发电统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400301(int start, int limit, string device, string point, DateTime starttime, DateTime endtime) {
            var data = new AjaxChartModel<List<Model400301>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400301>(),
                chart = new List<ChartModel>()
            };

            try {
                if(_workContext.AssociatedPointAttributes.ContainsKey(point)) {
                    var current = _workContext.AssociatedPointAttributes[point];
                    var models = this.GetChart400301(device, point, starttime, endtime);
                    if(models != null && models.Count > 0) {
                        data.message = "200 Ok";
                        data.total = models.Count;

                        var end = start + limit;
                        if(end > models.Count)
                            end = models.Count;

                        for(int i = start; i < end; i++) {
                            data.data.Add(new Model400301 {
                                index = start + i + 1,
                                value = Common.GetValueDisplay(current.Current.Type, models[i].Value, current.Current.Unit),
                                time = CommonHelper.DateTimeConverter(models[i].Time),
                                threshold = Common.GetValueDisplay(current.Current.Type, models[i].Threshold, current.Current.Unit),
                                state = (int)models[i].State,
                                stateDisplay = Common.GetPointStatusDisplay(models[i].State)
                            });
                        }

                        for(var i = 0; i < models.Count; i++) {
                            data.chart.Add(new ChartModel {
                                index = i + 1,
                                name = CommonHelper.DateTimeConverter(models[i].Time),
                                value = models[i].Value,
                                comment = Common.GetValueDisplay(current.Current.Type, models[i].Value, current.Current.Unit)
                            });
                        }
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
        public ActionResult DownloadChart400301(string device, string point, DateTime starttime, DateTime endtime) {
            try {
                var title = "信号测值列表";
                var models = new List<Model400301>();
                if(_workContext.AssociatedPointAttributes.ContainsKey(point)) {
                    var current = _workContext.AssociatedPointAttributes[point];
                    title = string.Format("{0} - {1}", current.Current.Name, title);
                    var cached = this.GetChart400301(device, point, starttime, endtime);
                    if(cached != null && cached.Count > 0) {
                        for(int i = 0; i < cached.Count; i++) {
                            models.Add(new Model400301 {
                                index = i + 1,
                                value = Common.GetValueDisplay(current.Current.Type, cached[i].Value, current.Current.Unit),
                                time = CommonHelper.DateTimeConverter(cached[i].Time),
                                threshold = Common.GetValueDisplay(current.Current.Type, cached[i].Threshold, current.Current.Unit),
                                state = (int)cached[i].State,
                                stateDisplay = Common.GetPointStatusDisplay(cached[i].State),
                                background = Common.GetPointStatusColor(cached[i].State)
                            });
                        }
                    }
                }

                using(var ms = _excelManager.Export<Model400301>(models, title, string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400302(int start, int limit, string device, string point, DateTime starttime, DateTime endtime) {
            var data = new AjaxChartModel<List<Model400302>, List<Model400302>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400302>(),
                chart = new List<Model400302>()
            };

            try {
                if(_workContext.AssociatedPointAttributes.ContainsKey(point)) {
                    var current = _workContext.AssociatedPointAttributes[point];
                    var models = this.GetChart400302(device, point, starttime, endtime);
                    if(models != null && models.Count > 0) {
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
                                maxdisplay = Common.GetValueDisplay(current.Current.Type, models[i].MaxValue, current.Current.Unit),
                                maxtime = CommonHelper.DateTimeConverter(models[i].MaxTime),
                                minvalue = models[i].MinValue,
                                mindisplay = Common.GetValueDisplay(current.Current.Type, models[i].MinValue, current.Current.Unit),
                                mintime = CommonHelper.DateTimeConverter(models[i].MinTime),
                                avgvalue = models[i].AvgValue,
                                avgdisplay = Common.GetValueDisplay(current.Current.Type, models[i].AvgValue, current.Current.Unit),
                                total = models[i].Total
                            });
                        }

                        for(var i = 0; i < models.Count; i++) {
                            data.chart.Add(new Model400302 {
                                index = i + 1,
                                start = CommonHelper.DateTimeConverter(models[i].BeginTime),
                                end = CommonHelper.DateTimeConverter(models[i].EndTime),
                                maxvalue = models[i].MaxValue,
                                maxdisplay = Common.GetValueDisplay(current.Current.Type, models[i].MaxValue, current.Current.Unit),
                                maxtime = CommonHelper.DateTimeConverter(models[i].MaxTime),
                                minvalue = models[i].MinValue,
                                mindisplay = Common.GetValueDisplay(current.Current.Type, models[i].MinValue, current.Current.Unit),
                                mintime = CommonHelper.DateTimeConverter(models[i].MinTime),
                                avgvalue = models[i].AvgValue,
                                avgdisplay = Common.GetValueDisplay(current.Current.Type, models[i].AvgValue, current.Current.Unit),
                                total = models[i].Total
                            });
                        }
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
        public ActionResult DownloadChart400302(string device, string point, DateTime starttime, DateTime endtime) {
            try {
                var title = "信号测值统计列表";
                var models = new List<Model400302>();
                if(_workContext.AssociatedPointAttributes.ContainsKey(point)) {
                    var current = _workContext.AssociatedPointAttributes[point];
                    title = string.Format("{0} - {1}", current.Current.Name, title);
                    var cached = this.GetChart400302(device, point, starttime, endtime);
                    if(cached != null && cached.Count > 0) {
                        for(int i = 0; i < cached.Count; i++) {
                            models.Add(new Model400302 {
                                index = i + 1,
                                start = CommonHelper.DateTimeConverter(cached[i].BeginTime),
                                end = CommonHelper.DateTimeConverter(cached[i].EndTime),
                                maxvalue = cached[i].MaxValue,
                                maxdisplay = Common.GetValueDisplay(current.Current.Type, cached[i].MaxValue, current.Current.Unit),
                                maxtime = CommonHelper.DateTimeConverter(cached[i].MaxTime),
                                minvalue = cached[i].MinValue,
                                mindisplay = Common.GetValueDisplay(current.Current.Type, cached[i].MinValue, current.Current.Unit),
                                mintime = CommonHelper.DateTimeConverter(cached[i].MinTime),
                                avgvalue = cached[i].AvgValue,
                                avgdisplay = Common.GetValueDisplay(current.Current.Type, cached[i].AvgValue, current.Current.Unit),
                                total = cached[i].Total
                            });
                        }
                    }
                }

                using(var ms = _excelManager.Export<Model400302>(models, title, string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestData400303(int start, int limit, string device, string[] points, DateTime starttime, DateTime endtime) {
            var data = new AjaxDataModel<List<Model400303>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400303>()
            };

            try {
                var models = this.GetChart400303(device, points, starttime, endtime);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        if(!_workContext.AssociatedPointAttributes.ContainsKey(models[i].PointId))
                            continue;

                        var current = _workContext.AssociatedPointAttributes[models[i].PointId];
                        data.data.Add(new Model400303 {
                            index = start + i + 1,
                            point = current.Current.Name,
                            start = CommonHelper.DateTimeConverter(models[i].StartTime),
                            value = Common.GetValueDisplay(current.Current.Type, models[i].Value, current.Current.Unit),
                            time = CommonHelper.DateTimeConverter(models[i].ValueTime)
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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

            try {
                var attributes = new List<PointAttributes>();
                foreach(var point in points) {
                    if(_workContext.AssociatedPointAttributes.ContainsKey(point))
                        attributes.Add(_workContext.AssociatedPointAttributes[point]);
                }

                var values = new JArray();
                for(var i = 0; i < attributes.Count; i++) {
                    var value = new JObject();
                    value.Add("title", attributes[i].Current.Name);
                    value.Add("data", string.Format("line{0}-data", i));
                    value.Add("display", string.Format("line{0}-display", i));
                    value.Add("time", string.Format("line{0}-time", i));
                    values.Add(value);
                }

                var fields = new JObject();
                fields.Add("key", "index");
                fields.Add("values", values);

                var datas = new JArray();
                var maxcount = 0;
                var dataset = new List<HsDomain.HisBat>[attributes.Count];
                for(var i = 0; i < attributes.Count; i++) {
                    dataset[i] = this.GetLine400303(device, points[i], starttime, endtime);
                    if(dataset[i].Count > maxcount) maxcount = dataset[i].Count;
                }

                for(var i = 0; i < maxcount; i++) {
                    var token = new JObject();
                    token.Add("index", i + 1);
                    for(var j = 0; j < attributes.Count; j++) {
                        if(dataset[j].Count <= i) continue;

                        var item = dataset[j][i];
                        token.Add(string.Format("line{0}-data", j), item.Value);
                        token.Add(string.Format("line{0}-display", j), Common.GetValueDisplay(attributes[j].Current.Type, item.Value, attributes[j].Current.Unit));
                        token.Add(string.Format("line{0}-time", j), CommonHelper.DateTimeConverter(item.ValueTime));
                    }

                    datas.Add(token);
                }

                var json = new JObject();
                json.Add("fields", fields);
                json.Add("data", datas);

                data.message = "200 Ok";
                data.total = maxcount;
                data.data = json.ToString(Formatting.None);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadData400303(string device, string[] points, DateTime starttime, DateTime endtime) {
            try {
                var title = "电池放电测值列表";
                var result = new List<Model400303>();
                var models = this.GetChart400303(device, points, starttime, endtime);
                for(int i = 0; i < models.Count; i++) {
                    if(!_workContext.AssociatedPointAttributes.ContainsKey(models[i].PointId))
                        continue;

                    var current = _workContext.AssociatedPointAttributes[models[i].PointId];
                    result.Add(new Model400303 {
                        index = i + 1,
                        point = current.Current.Name,
                        start = CommonHelper.DateTimeConverter(models[i].StartTime),
                        value = Common.GetValueDisplay(current.Current.Type, models[i].Value, current.Current.Unit),
                        time = CommonHelper.DateTimeConverter(models[i].ValueTime)
                    });
                }

                using(var ms = _excelManager.Export<Model400303>(result, title, string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400401(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400401>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400401>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var models = this.GetCustom400401(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400401 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            key = string.Format("{0}.{1}.{2}", models[i].Device.Room.Name, models[i].Device.Current.Name, models[i].Point.Current.Name),
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(models);
                    data.chart[1] = this.GetHisAlmChart2(models);
                    data.chart[2] = this.GetHisAlmChart3(parent, models);
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
        public ActionResult DownloadCustom400401(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400401>();
                var cached = this.GetCustom400401(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400401 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            key = string.Format("{0}.{1}.{2}", cached[i].Device.Room.Name, cached[i].Device.Current.Name, cached[i].Point.Current.Name),
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400401>(models, "超频告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400402(int start, int limit, string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400402>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400402>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var models = this.GetCustom400402(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400402 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            interval = models[i].Current.EndTime.Subtract(models[i].Current.StartTime).TotalMinutes,
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(models);
                    data.chart[1] = this.GetHisAlmChart2(models);
                    data.chart[2] = this.GetHisAlmChart3(parent, models);
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
        public ActionResult DownloadCustom400402(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400402>();
                var cached = this.GetCustom400402(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400402 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            interval = cached[i].Current.EndTime.Subtract(cached[i].Current.StartTime).TotalMinutes,
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400402>(models, "超短告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var models = this.GetCustom400403(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400403 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            interval = models[i].Current.EndTime.Subtract(models[i].Current.StartTime).TotalMinutes,
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(models);
                    data.chart[1] = this.GetHisAlmChart2(models);
                    data.chart[2] = this.GetHisAlmChart3(parent, models);
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
        public ActionResult DownloadCustom400403(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400403>();
                var cached = this.GetCustom400403(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400403 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            interval = cached[i].Current.EndTime.Subtract(cached[i].Current.StartTime).TotalMinutes,
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400403>(models, "超长告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model400101> GetBase400101(string parent, int[] types) {
            var index = 0;
            var result = new List<Model400101>();

            var methods = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Area, "类型");
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var data = from area in _workContext.AssociatedAreas
                           join method in methods on area.NodeLevel equals method.Id into temp
                           from dm in temp.DefaultIfEmpty()
                           where types == null || types.Length == 0 || types.Contains(area.NodeLevel)
                           select new {
                               Area = area,
                               Method = dm != null ? dm : new RsDomain.EnumMethods { Name = "未定义", Index = int.MaxValue }
                           };

                var ordered = data.OrderBy(d => d.Method.Index);
                foreach(var area in ordered) {
                    result.Add(new Model400101 {
                        index = ++index,
                        id = area.Area.AreaId,
                        name = area.Area.Name,
                        type = area.Method.Name,
                        comment = area.Area.Comment,
                        enabled = area.Area.Enabled
                    });
                }
                #endregion
            } else {
                #region children
                if(_workContext.AssociatedAreaAttributes.ContainsKey(parent)) {
                    var current = _workContext.AssociatedAreaAttributes[parent];
                    if(current.HasChildren) {
                        var data = from area in current.Children
                                   join method in methods on area.NodeLevel equals method.Id into temp
                                   from dm in temp.DefaultIfEmpty()
                                   where types == null || types.Length == 0 || types.Contains(area.NodeLevel)
                                   select new {
                                       Area = area,
                                       Method = dm != null ? dm : new RsDomain.EnumMethods { Name = "未定义", Index = int.MaxValue }
                                   };

                        var ordered = data.OrderBy(d => d.Method.Index);
                        foreach(var area in ordered) {
                            result.Add(new Model400101 {
                                index = ++index,
                                id = area.Area.AreaId,
                                name = area.Area.Name,
                                type = area.Method.Name,
                                comment = area.Area.Comment,
                                enabled = area.Area.Enabled
                            });
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model400102> GetBase400102(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400102>();

            var stations = _workContext.AssociatedStations;
            var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
            var loadTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Station, "市电引入方式");
            var powerTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Station, "供电性质");

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var matchs = new Dictionary<string, string>();
                if(_workContext.AssociatedAreaAttributes.ContainsKey(parent)) {
                    var current = _workContext.AssociatedAreaAttributes[parent];
                    matchs[current.Current.AreaId] = current.Current.Name;

                    if(current.HasChildren) {
                        foreach(var child in current.Children)
                            matchs[child.AreaId] = child.Name;
                    }
                }

                stations = stations.FindAll(s => matchs.ContainsKey(s.AreaId));
            }

            if(types != null && types.Length > 0)
                stationTypes = stationTypes.FindAll(st => types.Contains(st.Id));

            var query = from station in stations
                        join st in stationTypes on station.StaTypeId equals st.Id
                        join lt in loadTypes on station.CityElecLoadTypeId equals lt.Id into temp1
                        from defaultLt in temp1.DefaultIfEmpty()
                        join pt in powerTypes on station.SuppPowerTypeId equals pt.Id into temp2
                        from defaultPt in temp2.DefaultIfEmpty()
                        orderby station.StaTypeId
                        select new {
                            Station = station,
                            StaType = st,
                            LoadType = defaultLt ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 },
                            PowerType = defaultPt ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 }
                        };

            foreach(var q in query) {
                result.Add(new Model400102 {
                    index = ++index,
                    id = q.Station.Id,
                    name = q.Station.Name,
                    type = q.StaType.Name,
                    longitude = q.Station.Longitude,
                    latitude = q.Station.Latitude,
                    altitude = q.Station.Altitude,
                    cityelecloadtype = q.LoadType.Name,
                    cityeleccap = q.Station.CityElecCap,
                    cityelecload = q.Station.CityElecLoad,
                    contact = q.Station.Contact,
                    lineradiussize = q.Station.LineRadiusSize,
                    linelength = q.Station.LineLength,
                    supppowertype = q.PowerType.Name,
                    traninfo = q.Station.TranInfo,
                    trancontno = q.Station.TranContNo,
                    tranphone = q.Station.TranPhone,
                    comment = q.Station.Comment,
                    enabled = q.Station.Enabled
                });
            }

            return result;
        }

        private List<Model400103> GetBase400103(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400103>();

            var methods = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Room, "产权");
            var rooms = from room in _workContext.AssociatedRoomAttributes.Values
                        join method in methods on room.Current.PropertyId equals method.Id into temp
                        from defaultMethod in temp.DefaultIfEmpty()
                        where types == null || types.Length == 0 || types.Contains(room.Current.RoomTypeId)
                        orderby room.Current.RoomTypeId, room.Current.Name
                        select new {
                            Room = room,
                            Method = defaultMethod != null ? defaultMethod.Name : "未定义"
                        };

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        #region area
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedAreaAttributes[id];
                            matchs[current.Current.AreaId] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.AreaId] = child.Name;
                            }
                        }

                        rooms = rooms.Where(r => matchs.ContainsKey(r.Room.Area.AreaId));
                        #endregion
                    } else if(nodeType == EnmOrganization.Station) {
                        #region station
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedStationAttributes[id];
                            matchs[current.Current.Id] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.Id] = child.Name;
                            }
                        }

                        rooms = rooms.Where(r => matchs.ContainsKey(r.Room.Station.Id));
                        #endregion
                    }
                }
            }

            foreach(var room in rooms) {
                result.Add(new Model400103 {
                    index = ++index,
                    id = room.Room.Current.Id,
                    name = room.Room.Current.Name,
                    type = room.Room.Type.Name,
                    property = room.Method,
                    address = room.Room.Current.Address,
                    floor = room.Room.Current.Floor,
                    length = room.Room.Current.Length,
                    width = room.Room.Current.Width,
                    height = room.Room.Current.Heigth,
                    floorLoad = room.Room.Current.FloorLoad,
                    lineHeigth = room.Room.Current.LineHeigth,
                    square = room.Room.Current.Square,
                    effeSquare = room.Room.Current.EffeSquare,
                    fireFighEuip = room.Room.Current.FireFighEuip,
                    owner = room.Room.Current.Owner,
                    queryPhone = room.Room.Current.QueryPhone,
                    powerSubMain = room.Room.Current.PowerSubMain,
                    tranSubMain = room.Room.Current.TranSubMain,
                    enviSubMain = room.Room.Current.EnviSubMain,
                    fireSubMain = room.Room.Current.FireFighEuip,
                    airSubMain = room.Room.Current.AirSubMain,
                    contact = room.Room.Current.Contact,
                    comment = room.Room.Current.Comment,
                    enabled = room.Room.Current.Enabled
                });
            }

            return result;
        }

        private List<Model400104> GetBase400104(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400104>();

            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Current.DeviceTypeId));

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        #region area
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedAreaAttributes[id];
                            matchs[current.Current.AreaId] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.AreaId] = child.Name;
                            }
                        }

                        devices = devices.FindAll(d => matchs.ContainsKey(d.Area.AreaId));
                        #endregion
                    } else if(nodeType == EnmOrganization.Station) {
                        #region station
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedStationAttributes[id];
                            matchs[current.Current.Id] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.Id] = child.Name;
                            }
                        }

                        devices = devices.FindAll(d => matchs.ContainsKey(d.Station.Id));
                        #endregion
                    } else if(nodeType == EnmOrganization.Room) {
                        #region room
                        devices = devices.FindAll(d => d.Room.Id == id);
                        #endregion
                    }
                }
            }

            var productors = _rsProductorService.GetAllProductors();
            var brands = _rsBrandService.GetAllBrands();
            var suppliers = _rsSupplierService.GetAllSuppliers();
            var subCompanys = _rsSubCompanyService.GetAllSubCompanies();
            var status = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Device, "使用状态");

            var query = from device in devices
                        join pdr in productors on device.Current.ProdId equals pdr.Id into temp1
                        from defaulPdr in temp1.DefaultIfEmpty()
                        join brd in brands on device.Current.BrandId equals brd.Id into temp2
                        from defaulBrd in temp2.DefaultIfEmpty()
                        join spr in suppliers on device.Current.SuppId equals spr.Id into temp3
                        from defaultSpr in temp3.DefaultIfEmpty()
                        join scy in subCompanys on device.Current.SubCompId equals scy.Id into temp4
                        from defaultScy in temp4.DefaultIfEmpty()
                        join sus in status on device.Current.StatusId equals sus.Id into temp5
                        from defaultSus in temp5.DefaultIfEmpty()
                        orderby device.Current.DeviceTypeId
                        select new {
                            Device = device.Current,
                            Type = device.Type,
                            SubDeviceType = device.SubType.Name,
                            Productor = defaulPdr != null ? defaulPdr.Name : null,
                            Brand = defaulBrd != null ? defaulBrd.Name : null,
                            Supplier = defaultSpr != null ? defaultSpr.Name : null,
                            SubCompany = defaultScy != null ? defaultScy.Name : null,
                            Status = defaultSus ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 }
                        };

            foreach(var q in query) {
                result.Add(new Model400104 {
                    index = ++index,
                    id = q.Device.Id,
                    code = q.Device.Code,
                    name = q.Device.Name,
                    type = q.Type.Name,
                    subType = q.SubDeviceType,
                    sysName = q.Device.SysName,
                    sysCode = q.Device.SysCode,
                    model = q.Device.Model,
                    productor = q.Productor,
                    brand = q.Brand,
                    supplier = q.Supplier,
                    subCompany = q.SubCompany,
                    startTime = CommonHelper.DateTimeConverter(q.Device.StartTime),
                    scrapTime = CommonHelper.DateTimeConverter(q.Device.ScrapTime),
                    status = q.Status.Name,
                    contact = q.Device.Contact,
                    comment = q.Device.Comment,
                    enabled = q.Device.Enabled
                });
            }

            return result;
        }

        private List<ValStore<HsDomain.HisValue>> GetHistory400201(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, string[] logictypes, string pointname) {
            endtime = endtime.AddSeconds(86399);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            if(devices.Count == 0) return null;

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentValues = (devices.Count == 1 ? _hsHisValueService.GetHisValues(devices[0].Current.Id, starttime, endtime) : _hsHisValueService.GetHisValues(starttime, endtime));
            var models = (from value in currentValues
                          join point in points on value.PointId equals point.Current.Id
                          join device in devices on value.DeviceId equals device.Current.Id
                          select new ValStore<HsDomain.HisValue> {
                              Current = value,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetHistory400203(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby alarm.AlmLevel
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetHistory400204(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            endtime = endtime.AddSeconds(86399);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby device.Current.DeviceTypeId
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<Model400205> GetHistory400205(string parent, DateTime starttime, DateTime endtime) {
            var result = new List<Model400205>();
            endtime = endtime.AddSeconds(86399);
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
                            var projects = _msProjectService.GetProjects(starttime, endtime);
                            var appSets = this.GetAppointmentsInDevices(projects);
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Area.AreaId));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var appGroups = from app in appentities
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
                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        result.Add(new Model400205 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var appGroups = from app in appentities
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
                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        result.Add(new Model400205 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
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
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var projects = _msProjectService.GetProjects(starttime, endtime);
                            var appSets = this.GetAppointmentsInDevices(projects);
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var appGroups = from app in appentities
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
                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        result.Add(new Model400205 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
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
                            } else {
                                #region room children
                                var rooms = _workContext.AssociatedRooms.FindAll(r => r.StationId == nodeid);
                                foreach(var room in rooms) {
                                    if(_workContext.AssociatedRoomAttributes.ContainsKey(room.Id)) {
                                        var childCurrent = _workContext.AssociatedRoomAttributes[room.Id];

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == childCurrent.Current.Id);
                                        foreach(var device in devices)
                                            devSet.Add(device.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var appGroups = from app in appentities
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
                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        result.Add(new Model400205 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
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
                }
            }

            return result;
        }

        private List<Model400206> GetHistory400206(string parent, DateTime starttime, DateTime endtime) {
            var result = new List<Model400206>();
            endtime = endtime.AddSeconds(86399);
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
                            var appSets = this.GetAppointmentsInDevices(starttime, endtime);
                            var projects = _msProjectService.GetAllProjects();
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Area.AreaId));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var details = (from app in appentities
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

                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        result.Add(new Model400206 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
                                            count = details.Count,
                                            interval = Math.Round(appentities.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                            appointments = details
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var details = (from app in appentities
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

                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        result.Add(new Model400206 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
                                            interval = Math.Round(appentities.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                            count = details.Count,
                                            appointments = details
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var appSets = this.GetAppointmentsInDevices(starttime, endtime);
                            var projects = _msProjectService.GetAllProjects();
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        foreach(var device in devices)
                                            devSet.Add(device.Current.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var details = (from app in appentities
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

                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        result.Add(new Model400206 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
                                            interval = Math.Round(appentities.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                            count = details.Count,
                                            appointments = details
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

                                        var devSet = new HashSet<string>();
                                        var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == childCurrent.Current.Id);
                                        foreach(var device in devices)
                                            devSet.Add(device.Id);

                                        var appentities = new List<MsDomain.Appointment>();
                                        foreach(var appSet in appSets) {
                                            if(devSet.Overlaps(appSet.Value))
                                                appentities.Add(appSet.Id);
                                        }

                                        var details = (from app in appentities
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

                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        result.Add(new Model400206 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
                                            interval = Math.Round(appentities.Sum(a => a.EndTime.Subtract(a.StartTime).TotalMinutes), 2),
                                            count = details.Count,
                                            appointments = details
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

        private List<Model400207> GetHistory400207(string parent, DateTime starttime, DateTime endtime) {
            var result = new List<Model400207>();
            endtime = endtime.AddSeconds(86399);

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
                            var values = this.GetTingDianValues(starttime, endtime);
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Area.AreaId));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n=>n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n=>n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        result.Add(new Model400207 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        result.Add(new Model400207 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var values = this.GetTingDianValues(starttime, endtime);
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        result.Add(new Model400207 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => d.Current.RoomId == childCurrent.Current.Id);
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        result.Add(new Model400207 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
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
                }
            }

            return result;
        }

        private List<Model400208> GetHistory400208(string parent, DateTime starttime, DateTime endtime) {
            var result = new List<Model400208>();
            endtime = endtime.AddSeconds(86399);

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
                            var values = this.GetFaDianValues(starttime, endtime);
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Area.AreaId));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = areaTypes.Find(t => t.Id == child.NodeLevel);
                                        result.Add(new Model400208 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Join(",", _workContext.GetParentsInArea(child).Select(n => n.Name)),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == root.StaTypeId);
                                        result.Add(new Model400208 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(current.Current).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(root).Select(n => n.Name))),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
                                        });
                                    }
                                }
                                #endregion
                            }
                        }
                    } else if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedStationAttributes[nodeid];
                            var values = this.GetFaDianValues(starttime, endtime);
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => matchs.Contains(d.Station.Id));
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var childtype = stationTypes.Find(s => s.Id == child.StaTypeId);
                                        result.Add(new Model400208 {
                                            index = ++index,
                                            type = childtype != null ? childtype.Name : "",
                                            name = string.Format("{0},{1}", string.Join(",", _workContext.GetParentsInArea(child.AreaId).Select(n => n.Name)), string.Join(",", _workContext.GetParentsInStation(child).Select(n => n.Name))),
                                            count = details.Count(),
                                            interval = details.Sum(d => d.interval),
                                            details = details
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

                                        var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => d.Current.RoomId == childCurrent.Current.Id);
                                        var details = from device in devices
                                                      join value in values on device.Current.Id equals value.DeviceId
                                                      select new ShiDianDetailModel {
                                                          area = string.Join(",", _workContext.GetParentsInArea(device.Area).Select(n => n.Name)),
                                                          station = string.Join(",", _workContext.GetParentsInStation(device.Station).Select(n => n.Name)),
                                                          room = device.Room.Name,
                                                          device = device.Current.Name,
                                                          point = _workContext.AssociatedPointAttributes.ContainsKey(value.PointId) ? _workContext.AssociatedPointAttributes[value.PointId].Current.Name : "",
                                                          start = CommonHelper.DateTimeConverter(value.StartTime),
                                                          end = CommonHelper.DateTimeConverter(value.EndTime),
                                                          interval = Math.Round(value.EndTime.Subtract(value.StartTime).TotalMinutes, 2)
                                                      };

                                        var stations = _workContext.GetParentsInStation(room.StationId);
                                        var areas = stations.Count > 0 ? _workContext.GetParentsInArea(stations[0].AreaId) : new List<RsDomain.Area>();
                                        result.Add(new Model400208 {
                                            index = ++index,
                                            type = childCurrent.Type.Name,
                                            name = string.Format("{0},{1},{2}", string.Join(",", areas.Select(n => n.Name)), string.Join(",", stations.Select(n => n.Name)), room.Name),
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
                }
            }

            return result;
        }

        private List<IdValuePair<MsDomain.Appointment, HashSet<string>>> GetAppointmentsInDevices(DateTime start, DateTime end) {
            var entities = _msAppointmentService.GetAppointmentsByDate(start, end);
            return this.GetAppointmentsInDevices(entities);
        }

        private List<IdValuePair<MsDomain.Appointment, HashSet<string>>> GetAppointmentsInDevices(IEnumerable<MsDomain.Project> projects) {
            var matchs = projects.Select(p => p.Id);
            var entities = _msAppointmentService.GetAllAppointments().Where(a => matchs.Contains(a.ProjectId));
            return this.GetAppointmentsInDevices(entities);
        }

        private List<IdValuePair<MsDomain.Appointment, HashSet<string>>> GetAppointmentsInDevices(IEnumerable<MsDomain.Appointment> entities) {
            var appSets = new List<IdValuePair<MsDomain.Appointment, HashSet<string>>>();
            foreach(var entity in entities) {
                var appSet = new IdValuePair<MsDomain.Appointment, HashSet<string>>() { Id = entity, Value = new HashSet<string>() };
                var nodes = _msNodesInAppointmentService.GetNodesInAppointment(entity.Id);
                foreach(var node in nodes) {
                    if(node.NodeType == EnmOrganization.Device) {
                        appSet.Value.Add(node.NodeId);
                    }

                    if(node.NodeType == EnmOrganization.Room) {
                        var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == node.NodeId);
                        foreach(var device in devices) {
                            appSet.Value.Add(device.Id);
                        }
                    }

                    if(node.NodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(node.NodeId)) {
                            var mlgb = new HashSet<string>();
                            var current = _workContext.AssociatedStationAttributes[node.NodeId];
                            mlgb.Add(current.Current.Id);
                            foreach(var child in current.Children)
                                mlgb.Add(child.Id);

                            var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => mlgb.Contains(d.Station.Id));
                            foreach(var device in devices) {
                                appSet.Value.Add(device.Current.Id);
                            }
                        }
                    }

                    if(node.NodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(node.NodeId)) {
                            var mlgb = new HashSet<string>();
                            var current = _workContext.AssociatedAreaAttributes[node.NodeId];
                            mlgb.Add(current.Current.AreaId);
                            foreach(var child in current.Children)
                                mlgb.Add(child.AreaId);

                            var devices = _workContext.AssociatedDeviceAttributes.Values.Where(d => mlgb.Contains(d.Area.AreaId));
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

            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return models;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(limit.tingdianxinhao.Length == 0)
                return models;

            var values = _hsHisValueService.GetHisValues(limit.tingdianxinhao, start, end);
            var groups = from v in values
                         group v by new { v.DeviceId, v.PointId } into g
                         select new {
                             DeviceId = g.Key.DeviceId,
                             PointId = g.Key.PointId,
                             Values = g
                         };

            foreach(var group in groups) {
                DateTime? onetime = null;
                var pointValues = group.Values.OrderBy(v => v.Time);

                foreach(var pv in pointValues) {
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

            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return models;

            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            if(limit.fadianxinhao.Length == 0)
                return models;

            var values = _hsHisValueService.GetHisValues(limit.fadianxinhao, start, end);
            var groups = from v in values
                         group v by new { v.DeviceId, v.PointId } into g
                         select new {
                             DeviceId = g.Key.DeviceId,
                             PointId = g.Key.PointId,
                             Values = g
                         };

            foreach(var group in groups) {
                DateTime? onetime = null;
                var pointValues = group.Values.OrderBy(v => v.Time);

                foreach(var pv in pointValues) {
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

        private List<AlmStore<HsDomain.HisAlm>> GetCustom400401(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return null;

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            currentAlarms = (from alarm in currentAlarms
                             group alarm by new { alarm.DeviceId, alarm.PointId } into g
                             where g.Count() >= limit.chaopin
                             select new { G = g }).SelectMany(a => a.G).ToList();

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby device.Current.RoomId, device.Current.Id, point.Current.Id
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetCustom400402(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return null;

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            var seconds = limit.chaoduan * 60;
            currentAlarms = currentAlarms.FindAll(a => Math.Abs(a.EndTime.Subtract(a.StartTime).TotalSeconds) <= seconds);

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetCustom400403(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var parms = _msDictionaryService.GetDictionary((int)EnmDictionary.Report);
            if(parms == null || string.IsNullOrWhiteSpace(parms.ValuesJson))
                return null;

            endtime = endtime.AddSeconds(86399);
            var limit = JsonConvert.DeserializeObject<RtValues>(parms.ValuesJson);
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.AreaId] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Area.AreaId));
                        }
                    }

                    if(nodeType == EnmOrganization.Station) {
                        if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                            var attribute = _workContext.AssociatedStationAttributes[nodeid];
                            var matchers = new Dictionary<string, string>();
                            matchers.Add(attribute.Current.Id, attribute.Current.Name);

                            if(attribute.HasChildren) {
                                foreach(var child in attribute.Children) {
                                    matchers[child.Id] = child.Name;
                                }
                            }

                            devices = devices.FindAll(d => matchers.ContainsKey(d.Station.Id));
                        }
                    }

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
            points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

            if(confirm == "confirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Confirmed);

            if(confirm == "unconfirm")
                currentAlarms = currentAlarms.FindAll(a => a.ConfirmedStatus == EnmConfirmStatus.Unconfirmed);

            if(project == "project")
                currentAlarms = currentAlarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ProjectId));

            if(project == "unproject")
                currentAlarms = currentAlarms.FindAll(a => string.IsNullOrWhiteSpace(a.ProjectId));

            var seconds = limit.chaochang * 60;
            currentAlarms = currentAlarms.FindAll(a => a.EndTime.Subtract(a.StartTime).TotalSeconds >= seconds);

            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        /**
         *TODO:数据抽象，主键重复判断
         *判断是否有时间重复值，再判断当数量大于1000条要抽象数据
         */
        private List<HsDomain.HisValue> GetChart400301(string device, string point, DateTime starttime, DateTime endtime) {
            var models = new List<HsDomain.HisValue>();
            if(!string.IsNullOrWhiteSpace(device) && device != "root") {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        models = _hsHisValueService.GetHisValues(nodeid, point, starttime, endtime).ToList();
                    }
                }
            }

            return models;
        }

        /**
         *TODO:数据抽象，主键重复判断
         *判断是否有时间重复值，再判断当数量大于1000条要抽象数据
         */
        private List<HsDomain.HisStatic> GetChart400302(string device, string point, DateTime starttime, DateTime endtime) {
            var models = new List<HsDomain.HisStatic>();
            if(!string.IsNullOrWhiteSpace(device) && device != "root") {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        models = _hsHisStaticService.GetHisValues(nodeid, point, starttime, endtime).ToList();
                    }
                }
            }

            return models;
        }

        private List<HsDomain.HisBat> GetChart400303(string device, string[] points, DateTime starttime, DateTime endtime) {
            var models = new List<HsDomain.HisBat>();
            if(!string.IsNullOrWhiteSpace(device) && device != "root") {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        foreach(var point in points) {
                            models.AddRange(_hsHisBatService.GetHisBats(nodeid, point, starttime, endtime));
                        }
                    }
                }
            }

            return models;
        }

        private List<HsDomain.HisBat> GetLine400303(string device, string point, DateTime starttime, DateTime endtime) {
            var models = new List<HsDomain.HisBat>();
            if(!string.IsNullOrWhiteSpace(device) && device != "root") {
                var keys = Common.SplitKeys(device);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        models = _hsHisBatService.GetHisBats(nodeid, point, starttime, endtime).ToList();
                    }
                }
            }

            return models;
        }

        private List<ChartModel> GetHisAlmChart1(List<AlmStore<HsDomain.HisAlm>> models) {
            var data = new List<ChartModel>();
            try {
                if(models != null && models.Count > 0) {
                    var groups = models.GroupBy(m => m.Current.AlmLevel).OrderBy(g => g.Key);
                    foreach(var group in groups) {
                        data.Add(new ChartModel {
                            name = Common.GetAlarmLevelDisplay(group.Key),
                            value = group.Count(),
                            comment = ""
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
            }

            return data;
        }

        private List<ChartModel> GetHisAlmChart2(List<AlmStore<HsDomain.HisAlm>> models) {
            var data = new List<ChartModel>();

            try {
                if(models != null && models.Count > 0) {
                    var groups = models.GroupBy(m => new { m.Device.Type.Id, m.Device.Type.Name }).OrderBy(g => g.Key.Id);
                    foreach(var group in groups) {
                        data.Add(new ChartModel {
                            name = group.Key.Name,
                            value = group.Count(),
                            comment = ""
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
            }

            return data;
        }

        private List<ChartModel> GetHisAlmChart3(string parent, List<AlmStore<HsDomain.HisAlm>> models) {
            var data = new List<ChartModel>();

            try {
                if(models != null && models.Count > 0) {
                    if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                        #region root
                        var dict = _workContext.AssociatedAreas.ToDictionary(k => k.AreaId, v => v.Name);
                        var roots = new List<RsDomain.Area>();
                        foreach(var area in _workContext.AssociatedAreas) {
                            if(!dict.ContainsKey(area.ParentId))
                                roots.Add(area);
                        }

                        foreach(var root in roots) {
                            var matchs = new Dictionary<string, string>();
                            matchs.Add(root.AreaId, root.Name);

                            if(_workContext.AssociatedAreaAttributes.ContainsKey(root.AreaId)) {
                                var current = _workContext.AssociatedAreaAttributes[root.AreaId];
                                if(current.HasChildren) {
                                    foreach(var child in current.Children) {
                                        matchs[child.AreaId] = child.Name;
                                    }
                                }
                            }

                            var count = models.Count(m => matchs.ContainsKey(m.Device.Area.AreaId));
                            if(count > 0)
                                data.Add(new ChartModel { name = root.Name, value = count, comment = "" });
                        }
                        #endregion
                    } else {
                        var keys = Common.SplitKeys(parent);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var nodeid = keys[1];
                            var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                            if(nodeType == EnmOrganization.Area) {
                                #region area
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                                    var currentRoot = _workContext.AssociatedAreaAttributes[nodeid];
                                    if(currentRoot.HasChildren) {
                                        foreach(var rc in currentRoot.FirstChildren) {
                                            var matchs = new Dictionary<string, string>();
                                            matchs.Add(rc.AreaId, rc.Name);

                                            if(_workContext.AssociatedAreaAttributes.ContainsKey(rc.AreaId)) {
                                                var current = _workContext.AssociatedAreaAttributes[rc.AreaId];
                                                if(current.HasChildren) {
                                                    foreach(var child in current.Children) {
                                                        matchs[child.AreaId] = child.Name;
                                                    }
                                                }
                                            }

                                            var count = models.Count(m => matchs.ContainsKey(m.Device.Area.AreaId));
                                            if(count > 0)
                                                data.Add(new ChartModel { name = rc.Name, value = count, comment = "" });
                                        }
                                    } else {
                                        var stations = _workContext.AssociatedStations.FindAll(s => s.AreaId == nodeid);
                                        var dict = stations.ToDictionary(k => k.Id, v => v.Name);
                                        var roots = new List<RsDomain.Station>();
                                        foreach(var sta in stations) {
                                            if(!dict.ContainsKey(sta.ParentId))
                                                roots.Add(sta);
                                        }

                                        foreach(var root in roots) {
                                            var matchs = new Dictionary<string, string>();
                                            matchs.Add(root.Id, root.Name);

                                            if(_workContext.AssociatedStationAttributes.ContainsKey(root.Id)) {
                                                var current = _workContext.AssociatedStationAttributes[root.Id];
                                                if(current.HasChildren) {
                                                    foreach(var child in current.Children) {
                                                        matchs[child.Id] = child.Name;
                                                    }
                                                }
                                            }

                                            var count = models.Count(m => matchs.ContainsKey(m.Device.Station.Id));
                                            if(count > 0)
                                                data.Add(new ChartModel { name = root.Name, value = count, comment = "" });
                                        }
                                    }
                                }
                                #endregion
                            } else if(nodeType == EnmOrganization.Station) {
                                #region station
                                if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                                    var currentRoot = _workContext.AssociatedStationAttributes[nodeid];
                                    if(currentRoot.HasChildren) {
                                        foreach(var rc in currentRoot.FirstChildren) {
                                            var matchs = new Dictionary<string, string>();
                                            matchs.Add(rc.Id, rc.Name);

                                            if(_workContext.AssociatedStationAttributes.ContainsKey(rc.Id)) {
                                                var current = _workContext.AssociatedStationAttributes[rc.Id];
                                                if(current.HasChildren) {
                                                    foreach(var child in current.Children) {
                                                        matchs[child.Id] = child.Name;
                                                    }
                                                }
                                            }

                                            var count = models.Count(m => matchs.ContainsKey(m.Device.Station.Id));
                                            if(count > 0)
                                                data.Add(new ChartModel { name = rc.Name, value = count, comment = "" });
                                        }
                                    } else {
                                        var rooms = _workContext.AssociatedRooms.FindAll(s => s.StationId == currentRoot.Current.Id);
                                        foreach(var room in rooms) {
                                            var count = models.Count(m => m.Device.Room.Id == room.Id);
                                            if(count > 0)
                                                data.Add(new ChartModel { name = room.Name, value = count, comment = "" });
                                        }
                                    }
                                }
                                #endregion
                            } else if(nodeType == EnmOrganization.Room) {
                                #region room
                                var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == nodeid);
                                foreach(var device in devices) {
                                    var count = models.Count(m => m.Device.Current.Id == device.Id);
                                    if(count > 0)
                                        data.Add(new ChartModel { name = device.Name, value = count, comment = "" });
                                }
                                #endregion
                            } else if(nodeType == EnmOrganization.Device) {
                                #region device
                                var current = _workContext.AssociatedDevices.Find(d => d.Id == nodeid);
                                if(current != null) {
                                    var count = models.Count(m => m.Device.Current.Id == current.Id);
                                    if(count > 0)
                                        data.Add(new ChartModel { name = current.Name, value = count, comment = "" });
                                }
                                #endregion
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
            }

            return data;
        }

        #endregion

    }
}