using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
        public JsonResult RequestHistory400201(string parent, DateTime startDate, DateTime endDate, string[] statypes, string[] roomtypes, string[] devtypes, string[] points, string keywords, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400201>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400201>()
            };

            try {
                var stores = this.GetHistory400201(parent, startDate, endDate, statypes, roomtypes, devtypes, points, keywords, cache);
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400201 {
                            index = i + 1,
                            area = stores[i].Area.Name,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            type = Common.GetPointTypeDisplay(stores[i].Point.Type),
                            value = stores[i].Current.Value,
                            unit = Common.GetUnitDisplay(stores[i].Point.Type, stores[i].Current.Value.ToString(), stores[i].Point.UnitState),
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
        public ActionResult DownloadHistory400201(string parent, DateTime startDate, DateTime endDate, string[] statypes, string[] roomtypes, string[] devtypes, string[] points, string keywords, bool cache) {
            try {
                var models = new List<Model400201>();
                var stores = this.GetHistory400201(parent, startDate, endDate, statypes, roomtypes, devtypes, points, keywords, cache);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400201 {
                            index = i + 1,
                            area = stores[i].Area.Name,
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
        public JsonResult RequestHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, string confirmers, string keywords, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[2]
            };

            try {
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirmers, keywords, confirm, project, cache);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
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
        public ActionResult DownloadHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, string confirmers, string keywords, int confirm, int project, bool cache) {
            try {
                var models = new List<Model400202>();
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirmers, keywords, confirm, project, cache);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400202 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
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
        public JsonResult RequestHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400203>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400203>()
            };

            try {
                var stores = this.GetHistory400203(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(stores[i]);
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
        public ActionResult DownloadHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var models = this.GetHistory400203(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                using(var ms = _excelManager.Export<Model400203>(models, "历史告警分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400203(string station, int level, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400203, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.Get<List<Model400203>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if(current != null) {
                        var alarms = new List<AlmStore<A_HAlarm>>();
                        if (level == (int)EnmAlarm.Level0) {
                            alarms = current.alarms;
                        } else if (level == (int)EnmAlarm.Level1) {
                            alarms = current.alarms1;
                        } else if (level == (int)EnmAlarm.Level2) {
                            alarms = current.alarms2;
                        } else if (level == (int)EnmAlarm.Level3) {
                            alarms = current.alarms3;
                        } else if (level == (int)EnmAlarm.Level4) {
                            alarms = current.alarms4;
                        }

                        data.message = "200 Ok";
                        data.total = alarms.Count;

                        var end = start + limit;
                        if (end > alarms.Count)
                            end = alarms.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new HisAlmModel {
                                index = i + 1,
                                nmalarmid = alarms[i].Current.NMAlarmId,
                                level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                comment = alarms[i].Current.AlarmDesc,
                                startvalue = alarms[i].Current.StartValue.ToString(),
                                endvalue = alarms[i].Current.EndValue.ToString(),
                                point = alarms[i].Point.Name,
                                device = alarms[i].Device.Name,
                                room = alarms[i].Room.Name,
                                station = alarms[i].Station.Name,
                                area = alarms[i].Area.Name,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                id = alarms[i].Current.Id,
                                areaid = alarms[i].Area.Id,
                                stationid = alarms[i].Station.Id,
                                roomid = alarms[i].Room.Id,
                                fsuid = alarms[i].Current.FsuId,
                                deviceid = alarms[i].Device.Id,
                                pointid = alarms[i].Point.Id,
                                levelid = (int)alarms[i].Current.AlarmLevel,
                                reversalid = alarms[i].Current.ReversalId
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400203(string station, int level) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400203, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<HisAlmModel>();
                var stores = _cacheManager.Get<List<Model400203>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null) {
                        var alarms = new List<AlmStore<A_HAlarm>>();
                        if (level == (int)EnmAlarm.Level0) {
                            alarms = current.alarms;
                        } else if (level == (int)EnmAlarm.Level1) {
                            alarms = current.alarms1;
                        } else if (level == (int)EnmAlarm.Level2) {
                            alarms = current.alarms2;
                        } else if (level == (int)EnmAlarm.Level3) {
                            alarms = current.alarms3;
                        } else if (level == (int)EnmAlarm.Level4) {
                            alarms = current.alarms4;
                        }

                        for (int i = 0; i < alarms.Count; i++) {
                            result.Add(new HisAlmModel {
                                index = i + 1,
                                nmalarmid = alarms[i].Current.NMAlarmId,
                                level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                comment = alarms[i].Current.AlarmDesc,
                                startvalue = alarms[i].Current.StartValue.ToString(),
                                endvalue = alarms[i].Current.EndValue.ToString(),
                                point = alarms[i].Point.Name,
                                device = alarms[i].Device.Name,
                                room = alarms[i].Room.Name,
                                station = alarms[i].Station.Name,
                                area = alarms[i].Area.Name,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                id = alarms[i].Current.Id,
                                areaid = alarms[i].Area.Id,
                                stationid = alarms[i].Station.Id,
                                roomid = alarms[i].Room.Id,
                                fsuid = alarms[i].Current.FsuId,
                                deviceid = alarms[i].Device.Id,
                                pointid = alarms[i].Point.Id,
                                levelid = (int)alarms[i].Current.AlarmLevel,
                                reversalid = alarms[i].Current.ReversalId,
                                background = Common.GetAlarmColor(alarms[i].Current.AlarmLevel)
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<HisAlmModel>(result, "告警分类统计-告警详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields400204(string[] types) {
            var data = new AjaxDataModel<List<IdValuePair<string,string>>> {
                success = true,
                message = "200 Ok",
                total = 4,
                data = new List<IdValuePair<string, string>> {
                    new IdValuePair<string,string>("index","序号"),
                    new IdValuePair<string,string>("area","所属区域"),
                    new IdValuePair<string,string>("stationid",""),
                    new IdValuePair<string,string>("station","所属站点")
                }
            };

            try {
                var columns = _workContext.DeviceTypes;
                if (types != null && types.Length > 0)
                    columns = columns.FindAll(c => types.Contains(c.Id));

                if (columns != null && columns.Count > 0) {
                    for (int i = 0; i < columns.Count; i++) {
                        data.data.Add(new IdValuePair<string, string>(columns[i].Id, columns[i].Name));
                    }
                }

                data.data.Add(new IdValuePair<string, string>("total", "总计"));
                data.total = data.data.Count;
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult RequestHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<JObject>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>(),
                chart = new List<ChartsModel>()
            };

            try {
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, subLogicTypes, points, levels, confirm, project, cache);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Rows.Count;

                    var end = start + limit;
                    if (end > stores.Rows.Count)
                        end = stores.Rows.Count;

                    var columns = _workContext.DeviceTypes;
                    if (devTypes != null && devTypes.Length > 0)
                        columns = columns.FindAll(d => devTypes.Contains(d.Id));

                    for(int i = start; i < end; i++) {
                        var jObject = new JObject();
                        for (int j = 0; j < stores.Columns.Count; j++) {
                            var column = stores.Columns[j];
                            if (column.ExtendedProperties.ContainsKey("JsonIgnore")) continue;
                            jObject.Add(stores.Columns[j].ColumnName, stores.Rows[i][j].ToString());
                        }
                        data.data.Add(jObject);

                        var station = stores.Rows[i]["station"].ToString();
                        var charts = new ChartsModel { index = i, name = station, models = new List<ChartModel>() };
                        var index = 0;
                        foreach (var column in columns) {
                            charts.models.Add(new ChartModel {
                                index = ++index,
                                name = column.Name,
                                value = (int)stores.Rows[i][column.Id]
                            });
                        }
                        data.chart.Add(charts);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, subLogicTypes, points, levels, confirm, project, cache);
                using (var ms = _excelManager.Export(stores, "设备告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400204(int index, string station, string type, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400204, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.Get<DataTable>(key);
                if (stores != null && index >= 1 && stores.Rows.Count >= index) {
                    var curRow = stores.Rows[index - 1];
                    var stationid = curRow["stationid"].ToString();
                    if (stationid == station) {
                        var curCell = curRow[string.Format("alarms-{0}", type)];
                        if (curCell != null) {
                            var alarms = curCell as List<AlmStore<A_HAlarm>>;
                            
                            data.message = "200 Ok";
                            data.total = alarms.Count;

                            var end = start + limit;
                            if (end > alarms.Count)
                                end = alarms.Count;

                            for (int i = start; i < end; i++) {
                                data.data.Add(new HisAlmModel {
                                    index = i + 1,
                                    nmalarmid = alarms[i].Current.NMAlarmId,
                                    level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                    interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                    comment = alarms[i].Current.AlarmDesc,
                                    startvalue = alarms[i].Current.StartValue.ToString(),
                                    endvalue = alarms[i].Current.EndValue.ToString(),
                                    point = alarms[i].Point.Name,
                                    device = alarms[i].Device.Name,
                                    room = alarms[i].Room.Name,
                                    station = alarms[i].Station.Name,
                                    area = alarms[i].Area.Name,
                                    confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                    confirmer = alarms[i].Current.Confirmer,
                                    confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                    reservation = alarms[i].Current.ReservationId,
                                    reversalcount = alarms[i].Current.ReversalCount,
                                    id = alarms[i].Current.Id,
                                    areaid = alarms[i].Area.Id,
                                    stationid = alarms[i].Station.Id,
                                    roomid = alarms[i].Room.Id,
                                    fsuid = alarms[i].Current.FsuId,
                                    deviceid = alarms[i].Device.Id,
                                    pointid = alarms[i].Point.Id,
                                    levelid = (int)alarms[i].Current.AlarmLevel,
                                    reversalid = alarms[i].Current.ReversalId
                                });
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400204(int index, string station, string type) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400204, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<HisAlmModel>();
                var stores = _cacheManager.Get<DataTable>(key);
                if (stores != null && index >= 1 && stores.Rows.Count >= index) {
                    var curRow = stores.Rows[index - 1];
                    var stationid = curRow["stationid"].ToString();
                    if (stationid == station) {
                        var curCell = curRow[string.Format("alarms-{0}", type)];
                        if (curCell != null) {
                            var alarms = curCell as List<AlmStore<A_HAlarm>>;
                            for (int i = 0; i < alarms.Count; i++) {
                                result.Add(new HisAlmModel {
                                    index = i + 1,
                                    nmalarmid = alarms[i].Current.NMAlarmId,
                                    level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                    interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                    comment = alarms[i].Current.AlarmDesc,
                                    startvalue = alarms[i].Current.StartValue.ToString(),
                                    endvalue = alarms[i].Current.EndValue.ToString(),
                                    point = alarms[i].Point.Name,
                                    device = alarms[i].Device.Name,
                                    room = alarms[i].Room.Name,
                                    station = alarms[i].Station.Name,
                                    area = alarms[i].Area.Name,
                                    confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                    confirmer = alarms[i].Current.Confirmer,
                                    confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                    reservation = alarms[i].Current.ReservationId,
                                    reversalcount = alarms[i].Current.ReversalCount,
                                    id = alarms[i].Current.Id,
                                    areaid = alarms[i].Area.Id,
                                    stationid = alarms[i].Station.Id,
                                    roomid = alarms[i].Room.Id,
                                    fsuid = alarms[i].Current.FsuId,
                                    deviceid = alarms[i].Device.Id,
                                    pointid = alarms[i].Point.Id,
                                    levelid = (int)alarms[i].Current.AlarmLevel,
                                    reversalid = alarms[i].Current.ReversalId,
                                    background = Common.GetAlarmColor(alarms[i].Current.AlarmLevel)
                                });
                            }
                        }
                    }
                }

                using (var ms = _excelManager.Export<HisAlmModel>(result, "设备告警统计-告警详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400205(string parent, DateTime startDate, DateTime endDate, int start, int limit) {
            var data = new AjaxDataModel<List<Model400205>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400205>()
            };

            try {
                var models = this.GetHistory400205(parent, startDate, endDate);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400205(string parent, DateTime startDate, DateTime endDate) {
            try {
                var models = this.GetHistory400205(parent, startDate, endDate);
                using(var ms = _excelManager.Export<Model400205>(models, "工程项目信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400206(string parent, DateTime startDate, DateTime endDate, int start, int limit) {
            var data = new AjaxDataModel<List<Model400206>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400206>()
            };

            try {
                var models = this.GetHistory400206(parent, startDate, endDate);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400206(string parent, DateTime startDate, DateTime endDate) {
            try {
                var models = this.GetHistory400206(parent, startDate, endDate);
                using(var ms = _excelManager.Export<Model400206>(models, "工程预约信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400207(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400207>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400207>()
            };

            try {
                var models = this.GetHistory400207(parent, types, startDate, endDate, cache);
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
        public ActionResult DownloadHistory400207(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400207(parent, types, startDate, endDate, cache);
                using(var ms = _excelManager.Export<Model400207>(models, "市电停电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400207(string station, int start, int limit) {
            var data = new AjaxDataModel<List<ShiDianModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ShiDianModel>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400207, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.Get<List<Model400207>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new ShiDianModel {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                timespan = current.details[i].timespan
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400207(string station) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400207, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<ShiDianModel>();
                var stores = _cacheManager.Get<List<Model400207>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new ShiDianModel {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                timespan = current.details[i].timespan
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<ShiDianModel>(result, "市电停电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400208>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400208>()
            };

            try {
                var models = this.GetHistory400208(parent, types, startDate, endDate, cache);
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
        public ActionResult DownloadHistory400208(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400208(parent, types, startDate, endDate, cache);
                using(var ms = _excelManager.Export<Model400208>(models, "油机发电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400208(string station, int start, int limit) {
            var data = new AjaxDataModel<List<ShiDianModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ShiDianModel>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400208, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.Get<List<Model400208>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new ShiDianModel {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                timespan = current.details[i].timespan
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400208(string station) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_Cache_400208, _workContext.Identifier);
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<ShiDianModel>();
                var stores = _cacheManager.Get<List<Model400208>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new ShiDianModel {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                timespan = current.details[i].timespan
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<ShiDianModel>(result, "油机发电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400301(string device, string[] points, DateTime startDate, DateTime endDate) {
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
                            var curValues = _measureService.GetMeasuresInDevice(curDevice.Current.Id, startDate, endDate);
                            for (var i = 0; i < curPoints.Count; i++) {
                                var values = curValues.FindAll(v => v.SignalId == curPoints[i].Code && v.SignalNumber == curPoints[i].Number);
                                var models = new List<ChartModel>();
                                for (var k = 0; k < values.Count; k++) {
                                    models.Add(new ChartModel {
                                        index = k + 1,
                                        name = CommonHelper.DateTimeConverter(values[k].UpdateTime),
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
                            var curValues = _batService.GetValuesInDevice(curDevice.Current.Id, starttime, endtime);
                            for(var i = 0; i < curPoints.Count; i++) {
                                var values = curValues.FindAll(v => v.PointId == curPoints[i].Id);
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
        public JsonResult RequestCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<Model400401>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400401>(),
                chart = new List<ChartsModel>()
            };

            try {
                var model = this.GetCustom400401(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null && model.Value != null) {
                    var stores = model.Id;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400401 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
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
        public ActionResult DownloadCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var models = new List<Model400401>();
                var model = this.GetCustom400401(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null) {
                    var stores = model.Id;
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400401 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
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
        public JsonResult RequestCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<Model400402>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400402>(),
                chart = new List<ChartsModel>()
            };

            try {
                var model = this.GetCustom400402(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null && model.Value != null) {
                    var stores = model.Id;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400402 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
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
        public ActionResult DownloadCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var models = new List<Model400402>();
                var model = this.GetCustom400402(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null) {
                    var stores = model.Id;
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400402 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
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
        public JsonResult RequestCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<Model400403>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400403>(),
                chart = new List<ChartsModel>()
            };

            try {
                var model = this.GetCustom400403(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null && model.Value != null) {
                    var stores = model.Id;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400403 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
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
        public ActionResult DownloadCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var models = new List<Model400403>();
                var model = this.GetCustom400403(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (model != null && model.Id != null) {
                    var stores = model.Id;
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400403 {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].Point.Name,
                            device = stores[i].Device.Name,
                            room = stores[i].Room.Name,
                            station = stores[i].Station.Name,
                            area = stores[i].Area.Name,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
                            areaid = stores[i].Area.Id,
                            stationid = stores[i].Station.Id,
                            roomid = stores[i].Room.Id,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Device.Id,
                            pointid = stores[i].Point.Id,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
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
                if(current != null) stations = _workContext.Stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
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

        private List<ValStore<V_HMeasure>> GetHistory400201(string parent, DateTime startDate, DateTime endDate, string[] statypes, string[] roomtypes, string[] devtypes, string[] points, string keywords, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_Cache_400201, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<ValStore<V_HMeasure>>>(key);

            var values = new List<V_HMeasure>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                values = _measureService.GetMeasures(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) values = _measureService.GetMeasures(startDate, endDate).FindAll(s => current.Keys.Contains(s.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        values = _measureService.GetMeasuresInStation(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Room) {
                        values = _measureService.GetMeasuresInRoom(id, startDate, endDate);
                    } else if(nodeType == EnmSSH.Device) {
                        values = _measureService.GetMeasuresInDevice(id, startDate, endDate);
                    }
                }
            }

            var stations = _workContext.Stations;
            if(statypes != null && statypes.Length > 0)
                stations = stations.FindAll(d => statypes.Contains(d.Current.Type.Id));

            var rooms = stations.SelectMany(s=>s.Rooms);
            if(roomtypes != null && roomtypes.Length > 0)
                rooms = rooms.Where(d => roomtypes.Contains(d.Current.Type.Id));

            var devices = rooms.SelectMany(r=>r.Devices);
            if(devtypes != null && devtypes.Length > 0)
                devices = devices.Where(d => devtypes.Contains(d.Current.SubType.Id));

            var _points = _workContext.Points;
            if (points != null && points.Length > 0)
                _points = _points.FindAll(p => points.Contains(p.Id));

            if(!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) _points = _points.FindAll(p => CommonHelper.ConditionContain(p.Name, names));
            }

            var stores = (from val in values
                          join pt in _points on new { val.SignalId, val.SignalNumber } equals new { SignalId = pt.Code, SignalNumber = pt.Number }
                          join device in devices on val.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on room.Current.StationId equals station.Current.Id
                          join area in _workContext.Areas on station.Current.AreaId equals area.Current.Id
                          select new ValStore<V_HMeasure> {
                              Current = val,
                              Point = pt,
                              Device = device.Current,
                              Room = room.Current,
                              Station = station.Current,
                              Area = new A_Area {
                                  Id = area.Current.Id,
                                  Code = area.Current.Code,
                                  Name = area.ToString(),
                                  Type = area.Current.Type,
                                  ParentId = area.Current.ParentId,
                                  Comment = area.Current.Comment,
                                  Enabled = area.Current.Enabled
                              }
                          }).ToList();

            _cacheManager.Set<List<ValStore<V_HMeasure>>>(key, stores, CachedIntervals.Global_SiteResult_Intervals);
            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, string confirmers, string keywords, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_Cache_400202, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<AlmStore<A_HAlarm>>>(key);

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

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.Device.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.Device.SubLogicType.Id));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Point.Id));

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (!string.IsNullOrWhiteSpace(confirmers)) {
                var names = Common.SplitCondition(confirmers);
                if (names.Length > 0) stores = stores.FindAll(p => !string.IsNullOrWhiteSpace(p.Current.Confirmer) && CommonHelper.ConditionContain(p.Current.Confirmer, names));
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if(confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if(project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if(project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            _cacheManager.Set<List<AlmStore<A_HAlarm>>>(key, stores, CachedIntervals.Global_SiteResult_Intervals);
            return stores;
        }

        private List<Model400203> GetHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_Cache_400203, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key))  return _cacheManager.Get<List<Model400203>>(key);

            var alarms = new List<A_HAlarm>();
            var stations = _workContext.Stations;
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if (current != null) {
                    alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0)
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));

            var rooms = stations.SelectMany(s => s.Rooms);
            if (roomTypes != null && roomTypes.Length > 0)
                rooms = rooms.Where(r => roomTypes.Contains(r.Current.Type.Id));

            var devices = rooms.SelectMany(r => r.Devices);
            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                devices = devices.Where(d => subDeviceTypes.Contains(d.Current.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                devices = devices.Where(d => subLogicTypes.Contains(d.Current.SubLogicType.Id));

            if (points != null && points.Length > 0)
                alarms = alarms.FindAll(a => points.Contains(a.PointId));

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            if (confirm == 1) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) alarms = alarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ReservationId));
            if (project == 0) alarms = alarms.FindAll(a => string.IsNullOrWhiteSpace(a.ReservationId));

            var stores = (from alarm in alarms
                          join point in _workContext.AL on alarm.PointId equals point.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on room.Current.StationId equals station.Current.Id
                          join area in _workContext.Areas on station.Current.AreaId equals area.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<A_HAlarm> {
                              Current = alarm,
                              Point = point,
                              Device = device.Current,
                              Room = room.Current,
                              Station = station.Current,
                              Area = new A_Area {
                                  Id = area.Current.Id,
                                  Code = area.Current.Code,
                                  Name = area.ToString(),
                                  Type = area.Current.Type,
                                  ParentId = area.Current.ParentId,
                                  Comment = area.Current.Comment,
                                  Enabled = area.Current.Enabled
                              }
                          }).ToList();

            var result = new List<Model400203>();
            var index = 0;
            foreach (var station in stations) {
                var area = _workContext.Areas.Find(a=>a.Current.Id == station.Current.AreaId);
                var _alarms = stores.FindAll(s => s.Station.Id == station.Current.Id);
                var _alarms1 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level1);
                var _alarms2 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level2);
                var _alarms3 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level3);
                var _alarms4 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level4);

                result.Add(new Model400203 {
                     index = ++index,
                     area = area != null ? area.ToString(): "",
                     stationid = station.Current.Id,
                     station = station.Current.Name,
                     level1 = _alarms1.Count,
                     alarms1 = _alarms1,
                     level2 = _alarms2.Count,
                     alarms2 = _alarms2,
                     level3 = _alarms3.Count,
                     alarms3 = _alarms3,
                     level4 = _alarms4.Count,
                     alarms4 = _alarms4,
                     total = _alarms.Count,
                     alarms = _alarms
                });
            }

            _cacheManager.Set<List<Model400203>>(key, result, CachedIntervals.Global_SiteResult_Intervals);
            return result;
        }

        private DataTable GetHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_Cache_400204, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<DataTable>(key);

            var alarms = new List<A_HAlarm>();
            var stations = _workContext.Stations;
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if (current != null) {
                    alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0)
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));

            var rooms = stations.SelectMany(s => s.Rooms);
            if (roomTypes != null && roomTypes.Length > 0)
                rooms = rooms.Where(r => roomTypes.Contains(r.Current.Type.Id));

            var devices = rooms.SelectMany(r => r.Devices);
            if (devTypes != null && devTypes.Length > 0)
                devices = devices.Where(d => devTypes.Contains(d.Current.Type.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                devices = devices.Where(d => subLogicTypes.Contains(d.Current.SubLogicType.Id));

            if (points != null && points.Length > 0)
                alarms = alarms.FindAll(a => points.Contains(a.PointId));

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            if (confirm == 1) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) alarms = alarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ReservationId));
            if (project == 0) alarms = alarms.FindAll(a => string.IsNullOrWhiteSpace(a.ReservationId));

            var stores = (from alarm in alarms
                          join point in _workContext.AL on alarm.PointId equals point.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on room.Current.StationId equals station.Current.Id
                          join area in _workContext.Areas on station.Current.AreaId equals area.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<A_HAlarm> {
                              Current = alarm,
                              Point = point,
                              Device = device.Current,
                              Room = room.Current,
                              Station = station.Current,
                              Area = new A_Area {
                                  Id = area.Current.Id,
                                  Code = area.Current.Code,
                                  Name = area.ToString(),
                                  Type = area.Current.Type,
                                  ParentId = area.Current.ParentId,
                                  Comment = area.Current.Comment,
                                  Enabled = area.Current.Enabled
                              }
                          }).ToList();

            var columns = _workContext.DeviceTypes;
            if (devTypes != null && devTypes.Length > 0)
                columns = columns.FindAll(d => devTypes.Contains(d.Id));

            var result = this.GetModel400204(columns);
            foreach (var station in stations) {
                var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);
                var _alarms = stores.FindAll(s => s.Station.Id == station.Current.Id);

                var row = result.NewRow();
                row["area"] = area != null ? area.ToString() : "";
                row["stationid"] = station.Current.Id;
                row["station"] = station.Current.Name;
                row["total"] = _alarms.Count;
                row["alarms-total"] = _alarms;

                foreach (var column in columns) {
                    var _dalarms = _alarms.FindAll(a => a.Device.Type.Id == column.Id);
                    row[column.Id] = _dalarms.Count;
                    row[string.Format("alarms-{0}", column.Id)] = _dalarms;
                }

                result.Rows.Add(row);
            }

            _cacheManager.Set<DataTable>(key, result, CachedIntervals.Global_SiteResult_Intervals);
            return result;
        }

        private DataTable GetModel400204(List<C_DeviceType> deviceTypes) {
            var model = new DataTable("Model400204");

            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("area", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "所属区域");
            model.Columns.Add(column1);

            var column2 = new DataColumn("stationid", typeof(string));
            column2.ExtendedProperties.Add("ExcelIgnore", null);
            model.Columns.Add(column2);

            var column3 = new DataColumn("station", typeof(string));
            column3.ExtendedProperties.Add("ExcelDisplayName", "所属站点");
            model.Columns.Add(column3);

            foreach (var type in deviceTypes) {
                var _column0 = new DataColumn(type.Id, typeof(int));
                _column0.ExtendedProperties.Add("ExcelDisplayName", type.Name);
                model.Columns.Add(_column0);

                var _column1 = new DataColumn(string.Format("alarms-{0}", type.Id), typeof(List<AlmStore<A_HAlarm>>));
                _column1.ExtendedProperties.Add("ExcelIgnore", null);
                _column1.ExtendedProperties.Add("JsonIgnore", null);
                model.Columns.Add(_column1);
            }

            var column4 = new DataColumn("total", typeof(int));
            column4.ExtendedProperties.Add("ExcelDisplayName", "总计");
            model.Columns.Add(column4);

            var column5 = new DataColumn("alarms-total", typeof(List<AlmStore<A_HAlarm>>));
            column5.ExtendedProperties.Add("ExcelIgnore", null);
            column5.ExtendedProperties.Add("JsonIgnore", null);
            model.Columns.Add(column5);

            return model;
        }

        private List<Model400205> GetHistory400205(string parent, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var models = new List<Model400205>();
            var stations = _workContext.Stations;
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var projects = _projectService.GetPagedProjectsInSpan(startDate, endDate);
            var appSets = this.GetReservationsInDevices(projects);
            foreach (var station in stations) {
                var devices = _workContext.Devices.FindAll(d => d.Current.StationId == station.Current.Id);
                var devSet = new HashSet<string>();
                foreach (var device in devices) {
                    devSet.Add(device.Current.Id);
                }

                var appointments = new List<M_Reservation>();
                foreach (var appSet in appSets) {
                    if (devSet.Overlaps(appSet.Value))
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

                var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);
                var total = proDetail.Count();
                var timeout = proDetail.Count(p => p.AppointMaxTime > p.Project.EndTime || p.AppointMinTime < p.Project.StartTime);
                models.Add(new Model400205 {
                    index = ++index,
                    area = area != null ? area.ToString() : "",
                    stationid = station.Current.Id,
                    station = station.Current.Name,
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

            return models;
        }

        private List<Model400206> GetHistory400206(string parent, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var stations = _workContext.Stations;
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var models = new List<Model400206>();
            var appSets = this.GetReservationsInDevices(startDate, endDate);
            var projects = _projectService.GetPagedProjects();
            foreach (var station in stations) {
                var devices = _workContext.Devices.FindAll(d => d.Current.StationId == station.Current.Id);
                var devSet = new HashSet<string>();
                foreach (var device in devices) {
                    devSet.Add(device.Current.Id);
                }

                var appointments = new List<M_Reservation>();
                foreach (var appSet in appSets) {
                    if (devSet.Overlaps(appSet.Value))
                        appointments.Add(appSet.Id);
                }

                var details = (from app in appointments
                               join pro in projects on app.ProjectId equals pro.Id
                               select new ReservationModel {
                                   index = 0,
                                   id = app.Id,
                                   name = app.Name,
                                   startDate = CommonHelper.DateTimeConverter(app.StartTime),
                                   endDate = CommonHelper.DateTimeConverter(app.EndTime),
                                   projectId = app.ProjectId,
                                   projectName = pro.Name,
                                   creator = app.Creator,
                                   createdTime = CommonHelper.DateTimeConverter(app.CreatedTime),
                                   comment = app.Comment,
                                   enabled = app.Enabled,
                               }).ToList();

                var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);
                models.Add(new Model400206 {
                    index = ++index,
                    area = area != null ? area.ToString() : "",
                    stationid = station.Current.Id,
                    station = station.Current.Name,
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(appointments.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds))),
                    count = details.Count,
                    reservations = details
                });
            }

            return models;
        }

        private List<Model400207> GetHistory400207(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_Cache_400207, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400207>>(key);

            var models = new List<Model400207>();
            var stations = _workContext.Stations;
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && parent == "root") {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var rtValues = _workContext.RtValues;
            if (rtValues != null && rtValues.tingDianXinHao != null && rtValues.tingDianXinHao.Length > 0) {
                var index = 0;
                var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => rtValues.tingDianXinHao.Contains(a.PointId));
                foreach (var station in stations) {
                    var details = alarms.FindAll(a => a.StationId == station.Current.Id);
                    var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);

                    models.Add(new Model400207 {
                        index = ++index,
                        area = area != null ? area.ToString() : "",
                        stationid = station.Current.Id,
                        station = station.Current.Name,
                        type = station.Current.Type.Name,
                        count = details.Count,
                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                        details = details.Select(d => new ShiDianModel {
                            area = area != null ? area.ToString() : "",
                            station = station.Current.Name,
                            start = CommonHelper.DateTimeConverter(d.StartTime),
                            end = CommonHelper.DateTimeConverter(d.EndTime),
                            timespan = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                        }).ToList()
                    });
                }
            }

            _cacheManager.Set<List<Model400207>>(key, models, CachedIntervals.Global_SiteResult_Intervals);
            return models;
        }

        private List<Model400208> GetHistory400208(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_Cache_400208, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400208>>(key);

            var models = new List<Model400208>();
            var stations = _workContext.Stations;
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && parent == "root") {
                var current = _workContext.Areas.Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var rtValues = _workContext.RtValues;
            if (rtValues != null && !string.IsNullOrWhiteSpace(rtValues.faDianXinHao)) {
                var index = 0;
                var alarms = _hisAlarmService.GetAlarmsInPoint(rtValues.faDianXinHao, startDate, endDate);
                foreach (var station in stations) {
                    var details = alarms.FindAll(a => a.StationId == station.Current.Id);
                    var area = _workContext.Areas.Find(a => a.Current.Id == station.Current.AreaId);

                    models.Add(new Model400208 {
                        index = ++index,
                        area = area != null ? area.ToString() : "",
                        stationid = station.Current.Id,
                        station = station.Current.Name,
                        type = station.Current.Type.Name,
                        count = details.Count,
                        interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                        details = details.Select(d => new ShiDianModel {
                            area = area != null ? area.ToString() : "",
                            station = station.Current.Name,
                            start = CommonHelper.DateTimeConverter(d.StartTime),
                            end = CommonHelper.DateTimeConverter(d.EndTime),
                            timespan = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                        }).ToList()
                    });
                }
            }

            _cacheManager.Set<List<Model400208>>(key, models, CachedIntervals.Global_SiteResult_Intervals);
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

        private IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if(rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_Cache_400401, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

            var alarms = new List<A_HAlarm>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if (current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.Device.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.Device.SubLogicType.Id));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Point.Id));

            if (levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

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

            var charts = new List<ChartsModel>();
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

            var result = new IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            _cacheManager.Set<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key, result, CachedIntervals.Global_SiteResult_Intervals);
            return result;
        }

        private IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if (rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_Cache_400402, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

            var alarms = new List<A_HAlarm>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if (current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.Device.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.Device.SubLogicType.Id));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Point.Id));

            if (levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

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

            var charts = new List<ChartsModel>();
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

            var result = new IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            _cacheManager.Set<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key, result, CachedIntervals.Global_SiteResult_Intervals);
            return result;
        }

        private IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues;
            if (rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_Cache_400402, _workContext.Identifier);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

            var alarms = new List<A_HAlarm>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if (current != null) alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        alarms = _hisAlarmService.GetAlarmsInStation(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Room) {
                        alarms = _hisAlarmService.GetAlarmsInRoom(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Device) {
                        alarms = _hisAlarmService.GetAlarmsInDevice(id, startDate, endDate);
                    }
                }
            }

            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.Station.Type.Id));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.Room.Type.Id));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.Device.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.Device.SubLogicType.Id));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Point.Id));

            if (levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

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

            var charts = new List<ChartsModel>();
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

            var result = new IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            _cacheManager.Set<IdValuePair<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key, result, CachedIntervals.Global_SiteResult_Intervals);
            return result;
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
                                    var curstores = stores.FindAll(s => child.Keys.Contains(s.Area.Id));
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            } else if(current.Stations.Count > 0) {
                                foreach(var station in current.Stations) {
                                    var curstores = stores.FindAll(s => s.Station.Id == station.Current.Id);
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
                                var curstores = stores.FindAll(m => m.Room.Id == room.Current.Id);
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
                                var curstores = stores.FindAll(s => s.Device.Id == device.Current.Id);
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
                            var curstores = stores.FindAll(s => s.Device.Id == current.Current.Id);
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