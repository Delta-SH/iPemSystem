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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class ReportController : JsonNetController {

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
        private readonly IBatCurveService _batcurveService;
        private readonly IBatTimeService _batimeService;
        private readonly IStaticService _staticService;
        private readonly ISignalService _signalService;
        private readonly IHMeasureService _measureService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IEmployeeService _employeeService;
        private readonly IMAuthorizationService _mauthorizationService;
        private readonly IPointService _pointService;
        private readonly IProductorService _productorService;
        private readonly ISubCompanyService _subCompanyService;
        private readonly ISupplierService _supplierService;
        private readonly IOfflineService _offlineService;
        private readonly ICardRecordService _cardRecordService;
        private readonly IACabinetService _cabinetService;

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
            IBatCurveService batcurveService,
            IBatTimeService batimeService,
            IStaticService staticService,
            ISignalService signalService,
            IHMeasureService measureService,
            IEnumMethodService enumMethodService,
            IEmployeeService employeeService,
            IMAuthorizationService mauthorizationService,
            IPointService pointService,
            IProductorService productorService,
            ISubCompanyService subCompanyService,
            ISupplierService supplierService,
            IOfflineService offlineService,
            ICardRecordService cardRecordService,
            IACabinetService cabinetService) {
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
            this._batcurveService = batcurveService;
            this._batimeService = batimeService;
            this._staticService = staticService;
            this._signalService = signalService;
            this._measureService = measureService;
            this._enumMethodService = enumMethodService;
            this._employeeService = employeeService;
            this._mauthorizationService = mauthorizationService;
            this._pointService = pointService;
            this._productorService = productorService;
            this._subCompanyService = subCompanyService;
            this._supplierService = supplierService;
            this._offlineService = offlineService;
            this._cardRecordService = cardRecordService;
            this._cabinetService = cabinetService;
        }

        #endregion

        #region Actions

        [Authorize]
        public ActionResult Base(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
                return View(string.Format("base{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult History(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
                return View(string.Format("history{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Chart(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
                return View(string.Format("chart{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
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
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach (var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400101(string parent, int[] types) {
            try {
                var models = this.GetBase400101(parent, types);
                using (var ms = _excelManager.Export<Model400101>(models, "区域统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach (var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400102(string parent, string[] types) {
            try {
                var models = this.GetBase400102(parent, types);
                using (var ms = _excelManager.Export<Model400102>(models, "站点统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach (var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400103(string parent, string[] types) {
            try {
                var models = this.GetBase400103(parent, types);
                using (var ms = _excelManager.Export<Model400103>(models, "机房统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach (var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400104(string parent, string[] types) {
            try {
                var models = this.GetBase400104(parent, types);
                using (var ms = _excelManager.Export<Model400104>(models, "设备统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400105(int start, int limit, bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            var data = new AjaxDataModel<List<Model400105>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400105>()
            };

            try {
                var stores = this.GetBase400105(cache, departments, emptypes, keytype, keywords);
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400105(bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            try {
                var models = this.GetBase400105(cache, departments, emptypes, keytype, keywords);
                using (var ms = _excelManager.Export<Model400105>(models, "员工报表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestDetail400105(int start, int limit, string card) {
            var data = new AjaxDataModel<List<DetailModel400105>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400105>()
            };

            try {
                var stores = this.GetDetail400105(card);
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadDetail400105(string card) {
            try {
                var models = this.GetDetail400105(card);
                using (var ms = _excelManager.Export<DetailModel400105>(models, string.Format("卡片（{0}）授权设备详情", card), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400106(int start, int limit, bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            var data = new AjaxDataModel<List<Model400106>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400106>()
            };

            try {
                var stores = this.GetBase400106(cache, departments, emptypes, keytype, keywords);
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400106(bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            try {
                var models = this.GetBase400106(cache, departments, emptypes, keytype, keywords);
                using (var ms = _excelManager.Export<Model400106>(models, "外协报表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestDetail400106(int start, int limit, string card) {
            var data = new AjaxDataModel<List<DetailModel400106>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400106>()
            };

            try {
                var stores = this.GetDetail400106(card);
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadDetail400106(string card) {
            try {
                var models = this.GetDetail400106(card);
                using (var ms = _excelManager.Export<DetailModel400106>(models, string.Format("卡片（{0}）授权设备详情", card), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        var store = stores[i];
                        store.index = i + 1;
                        data.data.Add(store);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400201(string parent, DateTime startDate, DateTime endDate, string[] statypes, string[] roomtypes, string[] devtypes, string[] points, string keywords, bool cache) {
            try {
                var models = this.GetHistory400201(parent, startDate, endDate, statypes, roomtypes, devtypes, points, keywords, cache);
                for (int i = 0; i < models.Count; i++) {
                    models[i].index = i + 1;
                }

                using (var ms = _excelManager.Export<Model400201>(models, "历史测值信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int[] types, string confirmers, string keywords, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[2]
            };

            try {
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, types, confirmers, keywords, confirm, project, cache);
                if (stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
                        });
                    }

                    data.chart[0] = this.GetHisAlmChart1(stores);
                    data.chart[1] = this.GetHisAlmChart2(parent, stores);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int[] types, string confirmers, string keywords, int confirm, int project, bool cache) {
            try {
                var models = new List<Model400202>();
                var stores = this.GetHistory400202(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, types, confirmers, keywords, confirm, project, cache);
                if (stores != null && stores.Count > 0) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400202 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using (var ms = _excelManager.Export<Model400202>(models, "历史告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestDetail400202(string id, string title, DateTime date, bool primary, bool related, bool filter, bool reversal, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");

                List<AlmStore<A_HAlarm>> stores = null;
                if (primary)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetPrimaryAlarms(id, date, date.AddDays(2)));
                else if (related)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetRelatedAlarms(id, date, date.AddDays(2)));
                else if (filter)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetFilterAlarms(id, date, date.AddDays(2)));
                else if (reversal)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetReversalAlarms(id, date.AddDays(-2), date));

                if (stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new HisAlmModel {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
                        });
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
        public ActionResult DownloadDetail400202(string id, string title, DateTime date, bool primary, bool related, bool filter, bool reversal) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");

                List<AlmStore<A_HAlarm>> stores = null;
                if (primary)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetPrimaryAlarms(id, date, date.AddDays(2)));
                else if (related)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetRelatedAlarms(id, date, date.AddDays(2)));
                else if (filter)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetFilterAlarms(id, date, date.AddDays(2)));
                else if (reversal)
                    stores = _workContext.AlarmsToStore(_hisAlarmService.GetReversalAlarms(id, date.AddDays(-2), date));

                var models = new List<HisAlmModel>();
                if (stores != null && stores.Count > 0) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new HisAlmModel {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using (var ms = _excelManager.Export<HisAlmModel>(models, title ?? "告警详单", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (stores != null) {
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var models = this.GetHistory400203(parent, startDate, endDate, staTypes, roomTypes, subDeviceTypes, subLogicTypes, points, levels, confirm, project, cache);
                using (var ms = _excelManager.Export<Model400203>(models, "历史告警分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                var key = string.Format(GlobalCacheKeys.Report_400203, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400203>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.stationid == station);
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

                        data.message = "200 Ok";
                        data.total = alarms.Count;

                        var end = start + limit;
                        if (end > alarms.Count)
                            end = alarms.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new HisAlmModel {
                                id = alarms[i].Current.Id,
                                index = i + 1,
                                level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                name = alarms[i].AlarmName,
                                nmalarmid = alarms[i].Current.NMAlarmId,
                                interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                point = alarms[i].PointName,
                                device = alarms[i].DeviceName,
                                room = alarms[i].RoomName,
                                station = alarms[i].StationName,
                                area = alarms[i].AreaName,
                                supporter = alarms[i].SubCompany,
                                manager = alarms[i].SubManager,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                areaid = alarms[i].Current.AreaId,
                                stationid = alarms[i].Current.StationId,
                                roomid = alarms[i].Current.RoomId,
                                fsuid = alarms[i].Current.FsuId,
                                deviceid = alarms[i].Current.DeviceId,
                                pointid = alarms[i].Current.PointId,
                                levelid = (int)alarms[i].Current.AlarmLevel,
                                reversalid = alarms[i].Current.ReversalId
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400203(string station, int level) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400203, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<HisAlmModel>();
                var stores = _cacheManager.GetItemsFromList<Model400203>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.stationid == station);
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
                                id = alarms[i].Current.Id,
                                index = i + 1,
                                level = Common.GetAlarmDisplay(alarms[i].Current.AlarmLevel),
                                starttime = CommonHelper.DateTimeConverter(alarms[i].Current.StartTime),
                                endtime = CommonHelper.DateTimeConverter(alarms[i].Current.EndTime),
                                name = alarms[i].AlarmName,
                                nmalarmid = alarms[i].Current.NMAlarmId,
                                interval = CommonHelper.IntervalConverter(alarms[i].Current.StartTime, alarms[i].Current.EndTime),
                                point = alarms[i].PointName,
                                device = alarms[i].DeviceName,
                                room = alarms[i].RoomName,
                                station = alarms[i].StationName,
                                area = alarms[i].AreaName,
                                supporter = alarms[i].SubCompany,
                                manager = alarms[i].SubManager,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                areaid = alarms[i].Current.AreaId,
                                stationid = alarms[i].Current.StationId,
                                roomid = alarms[i].Current.RoomId,
                                fsuid = alarms[i].Current.FsuId,
                                deviceid = alarms[i].Current.DeviceId,
                                pointid = alarms[i].Current.PointId,
                                levelid = (int)alarms[i].Current.AlarmLevel,
                                reversalid = alarms[i].Current.ReversalId,
                                background = Common.GetAlarmColor(alarms[i].Current.AlarmLevel)
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<HisAlmModel>(result, "告警分类统计-告警详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields400204(string[] types) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "200 Ok",
                total = 4,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "area", type = "string", column = "所属区域", width = 150 },
                    new GridColumn { name = "stationid", type = "string" },
                    new GridColumn { name = "station", type = "string", column = "所属站点", width = 150 }
                }
            };

            try {
                var columns = _workContext.DeviceTypes();
                if (types != null && types.Length > 0)
                    columns = columns.FindAll(c => types.Contains(c.Id));

                if (columns != null && columns.Count > 0) {
                    for (int i = 0; i < columns.Count; i++) {
                        data.data.Add(new GridColumn { name = columns[i].Id, type = "string", column = columns[i].Name, align = "center", detail = true });
                    }
                }

                data.data.Add(new GridColumn { name = "total", type = "string", column = "总计", align = "center", detail = true });
                data.total = data.data.Count;
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<JObject>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>(),
                chart = new List<ChartsModel>()
            };

            try {
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    var columns = _workContext.DeviceTypes();
                    if (devTypes != null && devTypes.Length > 0) {
                        columns = columns.FindAll(d => devTypes.Contains(d.Id));
                    }

                    var models = this.GetDataTable400204(columns);
                    for (int i = start; i < end; i++) {
                        var row = models.NewRow();
                        row["index"] = stores[i].index;
                        row["area"] = stores[i].area;
                        row["stationid"] = stores[i].stationid;
                        row["station"] = stores[i].station;
                        row["total"] = stores[i].alarms.Count;
                        foreach (var column in columns) {
                            var _alarms = stores[i].alarms.FindAll(a => a.DeviceTypeId == column.Id);
                            row[column.Id] = _alarms.Count;
                        }

                        models.Rows.Add(row);
                    }

                    for (var i = 0; i < models.Rows.Count; i++) {
                        var row = models.Rows[i];

                        //数据
                        var jObject = new JObject();
                        for (int j = 0; j < models.Columns.Count; j++) {
                            var column = models.Columns[j];
                            if (column.ExtendedProperties.ContainsKey("JsonIgnore")) continue;
                            jObject.Add(column.ColumnName, row[j].ToString());
                        }
                        data.data.Add(jObject);

                        //图表
                        var charts = new ChartsModel { index = i, name = row["station"] as string, models = new List<ChartModel>() };
                        var index = 0;
                        foreach (var column in columns) {
                            charts.models.Add(new ChartModel {
                                index = ++index,
                                name = column.Name,
                                value = (int)row[column.Id]
                            });
                        }
                        data.chart.Add(charts);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            try {
                var columns = _workContext.DeviceTypes();
                if (devTypes != null && devTypes.Length > 0) {
                    columns = columns.FindAll(d => devTypes.Contains(d.Id));
                }

                var models = this.GetDataTable400204(columns);
                var stores = this.GetHistory400204(parent, startDate, endDate, staTypes, roomTypes, devTypes, subLogicTypes, points, levels, confirm, project, cache);
                if (stores != null && stores.Count > 0) {
                    foreach (var store in stores) {
                        var row = models.NewRow();
                        row["index"] = store.index;
                        row["area"] = store.area;
                        row["stationid"] = store.stationid;
                        row["station"] = store.station;
                        row["total"] = store.alarms.Count;
                        foreach (var column in columns) {
                            var _alarms = store.alarms.FindAll(a => a.DeviceTypeId == column.Id);
                            row[column.Id] = _alarms.Count;
                        }

                        models.Rows.Add(row);
                    }
                }

                using (var ms = _excelManager.Export(models, "设备告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400204(string station, string type, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400204, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400204>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.stationid == station);
                    if (current != null && current.alarms != null) {
                        var models = type.Equals("total") ? current.alarms : current.alarms.FindAll(a => a.DeviceTypeId.Equals(type));
                        if (models.Count > 0) {
                            data.message = "200 Ok";
                            data.total = models.Count;

                            var end = start + limit;
                            if (end > models.Count)
                                end = models.Count;

                            for (int i = start; i < end; i++) {
                                data.data.Add(new HisAlmModel {
                                    id = models[i].Current.Id,
                                    index = i + 1,
                                    level = Common.GetAlarmDisplay(models[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                                    name = models[i].AlarmName,
                                    nmalarmid = models[i].Current.NMAlarmId,
                                    interval = CommonHelper.IntervalConverter(models[i].Current.StartTime, models[i].Current.EndTime),
                                    point = models[i].PointName,
                                    device = models[i].DeviceName,
                                    room = models[i].RoomName,
                                    station = models[i].StationName,
                                    area = models[i].AreaName,
                                    supporter = models[i].SubCompany,
                                    manager = models[i].SubManager,
                                    confirmed = Common.GetConfirmDisplay(models[i].Current.Confirmed),
                                    confirmer = models[i].Current.Confirmer,
                                    confirmedtime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : "",
                                    reservation = models[i].Current.ReservationId,
                                    reversalcount = models[i].Current.ReversalCount,
                                    areaid = models[i].Current.AreaId,
                                    stationid = models[i].Current.StationId,
                                    roomid = models[i].Current.RoomId,
                                    fsuid = models[i].Current.FsuId,
                                    deviceid = models[i].Current.DeviceId,
                                    pointid = models[i].Current.PointId,
                                    levelid = (int)models[i].Current.AlarmLevel,
                                    reversalid = models[i].Current.ReversalId
                                });
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400204(string station, string type) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400204, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<HisAlmModel>();
                var stores = _cacheManager.GetItemsFromList<Model400204>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.stationid == station);
                    if (current != null && current.alarms != null) {
                        var models = type.Equals("total") ? current.alarms : current.alarms.FindAll(a => a.DeviceTypeId.Equals(type));
                        if (models.Count > 0) {
                            for (int i = 0; i < models.Count; i++) {
                                result.Add(new HisAlmModel {
                                    id = models[i].Current.Id,
                                    index = i + 1,
                                    level = Common.GetAlarmDisplay(models[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                                    name = models[i].AlarmName,
                                    nmalarmid = models[i].Current.NMAlarmId,
                                    interval = CommonHelper.IntervalConverter(models[i].Current.StartTime, models[i].Current.EndTime),
                                    point = models[i].PointName,
                                    device = models[i].DeviceName,
                                    room = models[i].RoomName,
                                    station = models[i].StationName,
                                    area = models[i].AreaName,
                                    supporter = models[i].SubCompany,
                                    manager = models[i].SubManager,
                                    confirmed = Common.GetConfirmDisplay(models[i].Current.Confirmed),
                                    confirmer = models[i].Current.Confirmer,
                                    confirmedtime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : "",
                                    reservation = models[i].Current.ReservationId,
                                    reversalcount = models[i].Current.ReversalCount,
                                    areaid = models[i].Current.AreaId,
                                    stationid = models[i].Current.StationId,
                                    roomid = models[i].Current.RoomId,
                                    fsuid = models[i].Current.FsuId,
                                    deviceid = models[i].Current.DeviceId,
                                    pointid = models[i].Current.PointId,
                                    levelid = (int)models[i].Current.AlarmLevel,
                                    reversalid = models[i].Current.ReversalId,
                                    background = Common.GetAlarmColor(models[i].Current.AlarmLevel)
                                });
                            }
                        }
                    }
                }

                using (var ms = _excelManager.Export<HisAlmModel>(result, "设备告警统计-告警详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                using (var ms = _excelManager.Export<Model400205>(models, "工程项目信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                using (var ms = _excelManager.Export<Model400206>(models, "工程预约信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400207_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400207_1>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400207_1>()
            };

            try {
                var models = this.GetHistory400207_1(parent, types, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400207_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400207_2>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400207_2>()
            };

            try {
                var models = this.GetHistory400207_2(parent, types, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400207_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400207_1(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400207_1>(models, "站点停电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400207_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400207_2(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400207_2>(models, "机房停电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400207_1(string id, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400207_1>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400207_1>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400207_1, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400207_1>(key).ToList();
                if (stores != null) {
                    var current = stores.Find(s => s.id.Equals(id));
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new DetailModel400207_1 {
                                index = i + 1,
                                area = current.details[i].area,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400207_2(string id, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400207_2>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400207_2>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400207_2, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400207_2>(key).ToList();
                if (stores != null) {
                    var current = stores.Find(s => s.id.Equals(id));
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new DetailModel400207_2 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400207_1(string id) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400207_1, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400207_1>();
                var stores = _cacheManager.GetItemsFromList<Model400207_1>(key).ToList();
                if (stores != null) {
                    var current = stores.Find(s => s.id.Equals(id));
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new DetailModel400207_1 {
                                index = i + 1,
                                area = current.details[i].area,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400207_1>(result, "站点停电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400207_2(string id) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400207_2, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400207_2>();
                var stores = _cacheManager.GetItemsFromList<Model400207_2>(key).ToList();
                if (stores != null) {
                    var current = stores.Find(s => s.id.Equals(id));
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new DetailModel400207_2 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400207_2>(result, "机房停电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400208_1>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400208_1>()
            };

            try {
                var models = this.GetHistory400208_1(parent, types, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400208_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400208_1(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400208_1>(models, "站点发电次数统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400208_2>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400208_2>()
            };

            try {
                var models = this.GetHistory400208_2(parent, types, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400208_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400208_2(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400208_2>(models, "油机发电次数统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400208_3(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.GetHistory400208_3(parent, period, startDate, endDate, cache);
                if (models != null && models.Rows.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Rows.Count;

                    var end = start + limit;
                    if (end > models.Rows.Count)
                        end = models.Rows.Count;

                    for (int i = start; i < end; i++) {
                        var row = models.Rows[i];
                        var jObject = new JObject();
                        for (int j = 0; j < models.Columns.Count; j++) {
                            var column = models.Columns[j];
                            if (column.ExtendedProperties.ContainsKey("JsonIgnore")) continue;
                            jObject.Add(column.ColumnName, row[j].ToString());
                        }
                        data.data.Add(jObject);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400208_3(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400208_3(parent, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("油机发电量统计({0})", CommonHelper.PeriodConverter(startDate, endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400208_1(string id, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400208>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400208>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400208_1, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400208_1>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new DetailModel400208 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                room = current.details[i].room,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval,
                                value = current.details[i].value
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400208_1(string id) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400208_1, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400208>();
                var stores = _cacheManager.GetItemsFromList<Model400208_1>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new DetailModel400208 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                room = current.details[i].room,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval,
                                value = current.details[i].value
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400208>(result, "油机发电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400208_2(string id, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400208>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400208>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400208_2, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400208_2>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(new DetailModel400208 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                room = current.details[i].room,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval,
                                value = current.details[i].value
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400208_2(string id) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400208_2, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400208>();
                var stores = _cacheManager.GetItemsFromList<Model400208_2>(key);
                if (stores != null) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(new DetailModel400208 {
                                index = i + 1,
                                area = current.details[i].area,
                                station = current.details[i].station,
                                room = current.details[i].room,
                                name = current.details[i].name,
                                start = current.details[i].start,
                                end = current.details[i].end,
                                interval = current.details[i].interval,
                                value = current.details[i].value
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400208>(result, "油机发电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields400208_3(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "area", type = "string", column = "所属区域", width = 150 },
                    new GridColumn { name = "station", type = "string", column = "所属站点", width = 150 },
                    new GridColumn { name = "room", type = "string", column = "所属机房", width = 150 },
                    new GridColumn { name = "name", type = "string", column = "设备名称", width = 150 }
                }
            };

            try {
                endDate = endDate.AddSeconds(86399);
                Common.CheckPeriods(startDate, endDate, period);

                var fields = CommonHelper.GetPeriods(startDate, endDate, period);
                if (fields.Count > 0) {
                    data.message = "200 Ok";
                    data.total = fields.Count;
                    foreach (var field in fields) {
                        data.data.Add(new GridColumn { name = field.Name, type = "string", column = field.Name, align = "left" });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private DataTable GetDataTable400208_3(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("DataTable400208_3");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("area", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "所属区域");
            model.Columns.Add(column1);

            var column2 = new DataColumn("station", typeof(string));
            column2.ExtendedProperties.Add("ExcelDisplayName", "所属站点");
            model.Columns.Add(column2);

            var column3 = new DataColumn("room", typeof(string));
            column3.ExtendedProperties.Add("ExcelDisplayName", "所属机房");
            model.Columns.Add(column3);

            var column4 = new DataColumn("name", typeof(string));
            column4.ExtendedProperties.Add("ExcelDisplayName", "设备名称");
            model.Columns.Add(column4);

            var dates = CommonHelper.GetPeriods(start, end, period);
            foreach (var date in dates) {
                var column = new DataColumn(date.Name, typeof(double));
                column.DefaultValue = 0;
                column.ExtendedProperties.Add("Start", date.Start);
                column.ExtendedProperties.Add("End", date.End);
                column.ExtendedProperties.Add("ExcelDisplayName", date.Name);
                model.Columns.Add(column);
            }

            return model;
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400209(bool cache, string parent, DateTime startDate, DateTime endDate, int[] recTypes, int keyType, string keyWords, int start, int limit) {
            var data = new AjaxDataModel<List<Model400209>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400209>()
            };

            try {
                var stores = this.GetHistory400209(cache, parent, startDate, endDate, recTypes, keyType, keyWords);
                if (stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        stores[i].index = start + i + 1;
                        data.data.Add(stores[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400209(bool cache, string parent, DateTime startDate, DateTime endDate, int[] recTypes, int keyType, string keyWords) {
            try {
                var models = this.GetHistory400209(cache, parent, startDate, endDate, recTypes, keyType, keyWords);
                for (int i = 0; i < models.Count; i++) {
                    models[i].index = i + 1;
                }

                using (var ms = _excelManager.Export<Model400209>(models, "历史刷卡记录", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400210(bool cache, string parent, int[] empTypes, int keyType, string keywords, DateTime startDate, DateTime endDate, int start, int limit) {
            var data = new AjaxDataModel<List<Model400210>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400210>()
            };

            try {
                var models = this.GetHistory400210(cache, parent, empTypes, keyType, keywords, startDate, endDate);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        models[i].index = start + i + 1;
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400210(bool cache, string parent, int[] empTypes, int keyType, string keywords, DateTime startDate, DateTime endDate) {
            try {
                var models = this.GetHistory400210(cache, parent, empTypes, keyType, keywords, startDate, endDate);
                for (int i = 0; i < models.Count; i++) {
                    models[i].index = i + 1;
                }

                using (var ms = _excelManager.Export<Model400210>(models, "刷卡次数统计", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400210(string card, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400210>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400210>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400210, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.Get<List<Model400210>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.cardId == card);
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            data.data.Add(current.details[i]);
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400210(string card) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400210, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400210>();
                var stores = _cacheManager.Get<List<Model400210>>(key);
                if (stores != null) {
                    var current = stores.Find(s => s.cardId == card);
                    if (current != null) {
                        for (int i = 0; i < current.details.Count; i++) {
                            result.Add(current.details[i]);
                        }
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400210>(result, "刷卡详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400211(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400211>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400211>()
            };

            try {
                var models = this.GetHistory400211(parent, types, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400211(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetHistory400211(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400211>(models, "放电次数统计", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistoryDetail400211(string id, int start, int limit) {
            var data = new AjaxDataModel<List<DetailModel400211>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DetailModel400211>()
            };

            try {
                var key = string.Format(GlobalCacheKeys.Report_400211, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var stores = _cacheManager.GetItemsFromList<Model400211>(key);
                if (stores != null && stores.Any()) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        data.message = "200 Ok";
                        data.total = current.details.Count;

                        var end = start + limit;
                        if (end > current.details.Count)
                            end = current.details.Count;

                        for (int i = start; i < end; i++) {
                            current.details[i].index = i + 1;
                            data.data.Add(current.details[i]);
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400211(string id) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400211, _workContext.Identifier());
                if (!_cacheManager.IsSet(key)) throw new iPemException("缓存已过期，请重新查询。");

                var result = new List<DetailModel400211>();
                var stores = _cacheManager.GetItemsFromList<Model400211>(key);
                if (stores != null && stores.Any()) {
                    var current = stores.FirstOrDefault(s => s.id.Equals(id));
                    if (current != null) {
                        result = current.details;
                    }
                }

                using (var ms = _excelManager.Export<DetailModel400211>(result, "电池放电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                var nodeKey = Common.ParseNode(device);
                if (nodeKey.Key == EnmSSH.Device) {
                    var curDevice = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                    if (curDevice != null) {
                        var curPoints = curDevice.Signals.FindAll(p => points.Contains(p.PointId));
                        var curValues = _measureService.GetMeasuresInPoints(curDevice.Current.Id, curPoints.Select(p => p.PointId).ToArray(), startDate, endDate);
                        for (var i = 0; i < curPoints.Count; i++) {
                            var values = curValues.FindAll(v => v.PointId == curPoints[i].PointId);
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
                                name = curPoints[i].PointName,
                                models = models
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                var nodeKey = Common.ParseNode(device);
                if (nodeKey.Key == EnmSSH.Device) {
                    var curDevice = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                    if (curDevice != null) {
                        var curPoint = curDevice.Signals.Find(p => p.PointId.Equals(point));
                        if (curPoint != null) {
                            var models = _staticService.GetValuesInPoint(curDevice.Current.Id, curPoint.PointId, starttime, endtime);
                            if (models.Count > 0) {
                                data.message = "200 Ok";
                                data.total = models.Count;

                                for (int i = 0; i < models.Count; i++) {
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
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400303(string parent, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400303>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400303>()
            };

            try {
                var models = this.GetChart400303(parent, startDate, endDate, cache);
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
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadChart400303(string parent, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetChart400303(parent, startDate, endDate, cache);
                using (var ms = _excelManager.Export<Model400303>(models, "电池放电曲线", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBatteryChart(string id, int pack, DateTime proc, string action) {
            var data = new AjaxDataModel<List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartsModel>()
            };

            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                var curves = new List<V_BatCurve>();
                if ("zdy".Equals(action)) {
                    curves = _batcurveService.GetProcedures(id, pack, EnmBatPoint.DCZDY, proc, proc.AddDays(3));
                } else if ("zdl".Equals(action)) {
                    curves = _batcurveService.GetProcedures(id, pack, EnmBatPoint.DCZDL, proc, proc.AddDays(3));
                } else if ("dy".Equals(action)) {
                    curves = _batcurveService.GetProcedures(id, pack, EnmBatPoint.DCDY, proc, proc.AddDays(3));
                } else if ("wd".Equals(action)) {
                    curves = _batcurveService.GetProcedures(id, pack, EnmBatPoint.DCWD, proc, proc.AddDays(3));
                }

                if (curves.Count > 0) {
                    var signals = _signalService.GetSimpleSignalsInDevice(id, true, false, false, false, false);
                    var groups = curves.GroupBy(c => c.PointId);
                    var values = from gop in groups
                                 join signal in signals on gop.Key equals signal.PointId
                                 select new { signal, curves = gop };

                    var index = 0;
                    foreach (var val in values) {
                        var model = new ChartsModel {
                            index = ++index,
                            name = val.signal.PointName,
                            models = new List<ChartModel>()
                        };

                        var _index = 0;
                        foreach (var curve in val.curves.OrderBy(c => c.ValueTime)) {
                            model.models.Add(new ChartModel {
                                index = ++_index,
                                name = Math.Round(curve.ValueTime.Subtract(curve.ProcTime).TotalMinutes, 2).ToString(),
                                value = curve.Value,
                                comment = val.signal.UnitState
                            });
                        }

                        data.data.Add(model);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400304_1(string parent, int[] levels, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<ChartsModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartsModel>[]{ 
                    new List<ChartsModel>(),
                    new List<ChartsModel>(),
                    new List<ChartsModel>(),
                    new List<ChartsModel>()
                }
            };

            try {
                var alarms = _hisAlarmService.GetAlarms(startDate, endDate);
                if (levels != null && levels.Length > 0) {
                    alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));
                }

                if (alarms.Count > 0) {
                    var devices = _workContext.Devices();
                    var nodeKey = Common.ParseNode(parent);
                    if (nodeKey.Key == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                        if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                    } else if (nodeKey.Key == EnmSSH.Station) {
                        devices = devices.FindAll(d => d.Current.StationId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Room) {
                        devices = devices.FindAll(d => d.Current.RoomId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Device) {
                        devices = devices.FindAll(d => d.Current.Id.Equals(nodeKey.Value));
                    }

                    var all = from alarm in alarms
                              join device in devices on alarm.DeviceId equals device.Current.Id
                              select new { Device = device.Current, Alarm = alarm };

                    if (all.Any()) {
                        data.message = "200 Ok";
                        data.total = all.Count();

                        //按设备类型分组
                        var charts1 = all.GroupBy(a => a.Device.Type.Name);
                        foreach (var gc in charts1) {
                            var values = new List<ChartModel>();
                            values.Add(new ChartModel { index = 1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level1) });
                            values.Add(new ChartModel { index = 2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level2) });
                            values.Add(new ChartModel { index = 3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level3) });
                            values.Add(new ChartModel { index = 4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level4) });

                            data.data[0].Add(new ChartsModel {
                                index = 0,
                                name = gc.Key,
                                models = values
                            });
                        }

                        //按告警类型分组
                        var charts2 = all.GroupBy(a => a.Device.LogicType.Name);
                        foreach (var gc in charts2) {
                            var values = new List<ChartModel>();
                            values.Add(new ChartModel { index = 1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level1) });
                            values.Add(new ChartModel { index = 2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level2) });
                            values.Add(new ChartModel { index = 3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level3) });
                            values.Add(new ChartModel { index = 4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level4) });

                            data.data[1].Add(new ChartsModel {
                                index = 1,
                                name = gc.Key,
                                models = values
                            });
                        }

                        //按告警级别分组
                        var charts3 = all.GroupBy(a => a.Alarm.AlarmLevel);
                        foreach (var gc in charts3) {
                            var values = new List<ChartModel>();
                            values.Add(new ChartModel { index = 1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level1) });
                            values.Add(new ChartModel { index = 2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level2) });
                            values.Add(new ChartModel { index = 3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level3) });
                            values.Add(new ChartModel { index = 4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level4) });

                            data.data[2].Add(new ChartsModel {
                                index = 2,
                                name = Common.GetAlarmDisplay(gc.Key),
                                models = values
                            });
                        }

                        //按确认类型分组
                        var charts4 = all.GroupBy(a => a.Alarm.Confirmed);
                        foreach (var gc in charts4) {
                            var values = new List<ChartModel>();
                            values.Add(new ChartModel { index = 1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level1) });
                            values.Add(new ChartModel { index = 2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level2) });
                            values.Add(new ChartModel { index = 3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level3) });
                            values.Add(new ChartModel { index = 4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = gc.Count(a => a.Alarm.AlarmLevel == EnmAlarm.Level4) });

                            data.data[3].Add(new ChartsModel {
                                index = 3,
                                name = Common.GetConfirmDisplay(gc.Key),
                                models = values
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestChart400304_2(string parent, int[] levels, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartModel>[]{ 
                    new List<ChartModel>(),
                    new List<ChartModel>(),
                    new List<ChartModel>(),
                    new List<ChartModel>()
                }
            };

            try {
                var alarms = _hisAlarmService.GetAlarms(startDate, endDate);
                if (levels != null && levels.Length > 0) {
                    alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));
                }

                if (alarms.Count > 0) {
                    var devices = _workContext.Devices();
                    var nodeKey = Common.ParseNode(parent);
                    if (nodeKey.Key == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                        if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                    } else if (nodeKey.Key == EnmSSH.Station) {
                        devices = devices.FindAll(d => d.Current.StationId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Room) {
                        devices = devices.FindAll(d => d.Current.RoomId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Device) {
                        devices = devices.FindAll(d => d.Current.Id.Equals(nodeKey.Value));
                    }

                    var all = from alarm in alarms
                              join device in devices on alarm.DeviceId equals device.Current.Id
                              select new { Device = device.Current, Alarm = alarm };

                    if (all.Any()) {
                        data.message = "200 Ok";
                        data.total = all.Count();

                        //按设备类型分组
                        var charts1 = all.GroupBy(a => a.Device.Type.Name);
                        foreach (var gc in charts1) {
                            data.data[0].Add(new ChartModel {
                                index = 0,
                                name = gc.Key,
                                value = gc.Count()
                            });
                        }

                        //按告警类型分组
                        var charts2 = all.GroupBy(a => a.Device.LogicType.Name);
                        foreach (var gc in charts2) {
                            data.data[1].Add(new ChartModel {
                                index = 1,
                                name = gc.Key,
                                value = gc.Count()
                            });
                        }

                        //按告警级别分组
                        var charts3 = all.GroupBy(a => a.Alarm.AlarmLevel);
                        foreach (var gc in charts3) {
                            data.data[2].Add(new ChartModel {
                                index = 2,
                                name = Common.GetAlarmDisplay(gc.Key),
                                value = gc.Count()
                            });
                        }

                        //按确认类型分组
                        var charts4 = all.GroupBy(a => a.Alarm.Confirmed);
                        foreach (var gc in charts4) {
                            data.data[3].Add(new ChartModel {
                                index = 3,
                                name = Common.GetConfirmDisplay(gc.Key),
                                value = gc.Count()
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null && model.Value != null) {
                    var stores = model.Key;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400401 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null) {
                    var stores = model.Key;
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400401 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using (var ms = _excelManager.Export<Model400401>(models, "超频告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null && model.Value != null) {
                    var stores = model.Key;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400402 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null) {
                    var stores = model.Key;
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400402 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using (var ms = _excelManager.Export<Model400402>(models, "超短告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null && model.Value != null) {
                    var stores = model.Key;
                    var charts = model.Value;

                    data.message = "200 Ok";
                    data.total = stores.Count;
                    data.chart = model.Value;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new Model400403 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (model != null && model.Key != null) {
                    var stores = model.Key;
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new Model400403 {
                            id = stores[i].Current.Id,
                            index = i + 1,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            name = stores[i].AlarmName,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            supporter = stores[i].SubCompany,
                            manager = stores[i].SubManager,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            areaid = stores[i].Current.AreaId,
                            stationid = stores[i].Current.StationId,
                            roomid = stores[i].Current.RoomId,
                            fsuid = stores[i].Current.FsuId,
                            deviceid = stores[i].Current.DeviceId,
                            pointid = stores[i].Current.PointId,
                            levelid = (int)stores[i].Current.AlarmLevel,
                            reversalid = stores[i].Current.ReversalId,
                            background = Common.GetAlarmColor(stores[i].Current.AlarmLevel)
                        });
                    }
                }

                using (var ms = _excelManager.Export<Model400403>(models, "超长告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields400404(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "area", type = "string", column = "所属区域", width = 150 },
                    new GridColumn { name = "station", type = "string", column = "所属站点", width = 150 },
                    new GridColumn { name = "room", type = "string", column = "所属机房", width = 150 },
                    new GridColumn { name = "name", type = "string", column = "分路编号", width = 150 }
                }
            };

            try {
                endDate = endDate.AddSeconds(86399);
                Common.CheckPeriods(startDate, endDate, period);

                var fields = CommonHelper.GetPeriods(startDate, endDate, period);
                if (fields.Count > 0) {
                    data.message = "200 Ok";
                    data.total = fields.Count;
                    foreach (var field in fields) {
                        data.data.Add(new GridColumn { name = field.Name, type = "float", column = field.Name, align = "left" });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400404(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.GetCustom400404(parent, period, startDate, endDate, cache);
                if (models != null && models.Rows.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Rows.Count;

                    var end = start + limit;
                    if (end > models.Rows.Count)
                        end = models.Rows.Count;

                    for (int i = start; i < end; i++) {
                        var row = models.Rows[i];
                        var jObject = new JObject();
                        for (int j = 0; j < models.Columns.Count; j++) {
                            var column = models.Columns[j];
                            if (column.ExtendedProperties.ContainsKey("JsonIgnore")) continue;
                            jObject.Add(column.ColumnName, row[j].ToString());
                        }
                        data.data.Add(jObject);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadCustom400404(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetCustom400404(parent, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("列头柜分路功率信息({0})", CommonHelper.PeriodConverter(startDate, endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestCustom400405(string parent, string date, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<Model400405>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400405>()
            };

            try {
                var models = this.GetCustom400405(parent, date, cache);
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
        [Authorize]
        public ActionResult DownloadCustom400405(string parent, string date, bool cache) {
            try {
                var models = this.GetCustom400405(parent, date, cache);
                using (var ms = _excelManager.Export<Model400405>(models, string.Format("列头柜分路电量({0})", date), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model400101> GetBase400101(string parent, int[] types) {
            var index = 0;
            var result = new List<Model400101>();

            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var areas = _workContext.Areas();
                if (types != null && types.Length > 0)
                    areas = areas.FindAll(a => types.Contains(a.Current.Type.Key));

                var ordered = areas.OrderBy(a => a.Current.Type.Key);
                foreach (var current in ordered) {
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
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null && current.HasChildren) {
                    var children = current.Children;
                    if (types != null && types.Length > 0)
                        children = children.FindAll(a => types.Contains(a.Current.Type.Key));

                    var ordered = children.OrderBy(a => a.Current.Type.Key);
                    foreach (var child in ordered) {
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
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                stations = _workContext.Stations();
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var loadTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Station, "市电引入方式");
            var powerTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Station, "供电性质");
            var stores = from station in stations
                         join area in _workContext.Areas() on station.Current.AreaId equals area.Current.Id
                         join lot in loadTypes on station.Current.CityElecLoadTypeId equals lot.Id into lt1
                         from def1 in lt1.DefaultIfEmpty()
                         join pot in powerTypes on station.Current.SuppPowerTypeId equals pot.Id into lt2
                         from def2 in lt2.DefaultIfEmpty()
                         orderby station.Current.Type.Id
                         select new {
                             Area = area.ToString(),
                             Station = station.Current,
                             LoadType = def1 ?? new C_EnumMethod { Name = "未定义", Index = 0 },
                             PowerType = def2 ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                         };

            foreach (var store in stores.OrderBy(s => s.Area).ThenBy(s => s.Station.Name)) {
                result.Add(new Model400102 {
                    index = ++index,
                    area = store.Area,
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

            var rooms = new List<S_Room>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                rooms.AddRange(_workContext.Rooms().Select(r => r.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) rooms.AddRange(_workContext.Rooms().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(r => r.Current));
                    } else if (nodeType == EnmSSH.Station) {
                        var current = _workContext.Stations().Find(a => a.Current.Id == id);
                        rooms.AddRange(_workContext.Rooms().FindAll(r => r.Current.StationId == id).Select(r => r.Current));
                    }
                }
            }

            if (types != null && types.Length > 0)
                rooms = rooms.FindAll(s => types.Contains(s.Type.Id));

            var parms = _enumMethodService.GetEnumsByType(EnmMethodType.Room, "产权");
            var stores = from room in rooms
                         join area in _workContext.Areas() on room.AreaId equals area.Current.Id
                         join parm in parms on room.PropertyId equals parm.Id into lt
                         from def in lt.DefaultIfEmpty()
                         orderby room.Type.Id, room.Name
                         select new {
                             Area = area.ToString(),
                             Room = room,
                             Method = def ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                         };

            foreach (var store in stores.OrderBy(s => s.Room.StationName).ThenBy(s => s.Room.Name)) {
                result.Add(new Model400103 {
                    index = ++index,
                    area = store.Area,
                    station = store.Room.StationName,
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

            var devices = new List<D_Device>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                devices.AddRange(_workContext.Devices().Select(d => d.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) devices.AddRange(_workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(d => d.Current));
                    } else if (nodeType == EnmSSH.Station) {
                        devices.AddRange(_workContext.Devices().FindAll(d => d.Current.StationId == id).Select(d => d.Current));
                    } else if (nodeType == EnmSSH.Room) {
                        devices.AddRange(_workContext.Devices().FindAll(d => d.Current.RoomId == id).Select(d => d.Current));
                    }
                }
            }

            if (types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Type.Id));

            var status = _enumMethodService.GetEnumsByType(EnmMethodType.Device, "使用状态");
            var stores = from device in devices
                         join area in _workContext.Areas() on device.AreaId equals area.Current.Id
                         join sts in status on device.StatusId equals sts.Id into lt
                         from def in lt.DefaultIfEmpty()
                         orderby device.Type.Id
                         select new {
                             Area = area.ToString(),
                             Device = device,
                             Productor = device.Productor ?? string.Empty,
                             Brand = device.Brand ?? string.Empty,
                             Supplier = device.Supplier ?? string.Empty,
                             SubCompany = device.SubCompany ?? string.Empty,
                             Status = def ?? new C_EnumMethod { Name = "未定义", Index = 0 }
                         };

            foreach (var store in stores.OrderBy(s => s.Device.StationName).ThenBy(s => s.Device.RoomName).ThenBy(s => s.Device.Name)) {
                result.Add(new Model400104 {
                    index = ++index,
                    area = store.Area,
                    station = store.Device.StationName,
                    room = store.Device.RoomName,
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

        private List<Model400105> GetBase400105(bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            var stores = new List<Model400105>();

            var empys = _employeeService.GetEmployees();
            if (departments != null && departments.Length > 0) {
                empys = empys.FindAll(e => departments.Contains(e.DeptId));
            }

            if (emptypes != null && emptypes.Length > 0) {
                empys = empys.FindAll(e => emptypes.Contains(string.IsNullOrWhiteSpace(e.CardId) ? 0 : 1));
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var keys = Common.SplitCondition(keywords);
                if (keys.Length > 0) {
                    switch (keytype) {
                        case 1:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.Code, keys));
                            break;
                        case 2:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.DecimalCard, keys));
                            break;
                        case 3:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.Name, keys));
                            break;
                    }
                }
            }

            if (empys.Count == 0) return stores;

            var auths = _mauthorizationService.GetAuthorizationsInType(EnmEmpType.Employee);
            if (!_workContext.Role().Id.Equals(U_Role.SuperId)) {
                auths = auths.FindAll(a => _workContext.DeviceKeys().Contains(a.DeviceId));
            }

            var authsInCard = from auth in auths
                              group auth by auth.CardId into g
                              select new { Key = g.Key, Count = g.Count() };

            var temps = from emp in empys
                        join aic in authsInCard on emp.CardId equals aic.Key into lt
                        from def in lt.DefaultIfEmpty()
                        select new Model400105 {
                            id = emp.Id,
                            empNo = emp.Code,
                            name = emp.Name,
                            engName = emp.EngName,
                            usedName = emp.UsedName,
                            sex = Common.GetSexDisplay(emp.Sex),
                            dept = emp.DeptName,
                            duty = emp.DutyName,
                            icard = emp.ICardId,
                            birthday = CommonHelper.DateConverter(emp.Birthday),
                            degree = Common.GetDegreeDisplay(emp.Degree),
                            marriage = Common.GetMarriageDisplay(emp.Marriage),
                            nation = emp.Nation,
                            provinces = emp.Provinces,
                            native = emp.Native,
                            address = emp.Address,
                            postalCode = emp.PostalCode,
                            addrPhone = emp.AddrPhone,
                            workPhone = emp.WorkPhone,
                            mobilePhone = emp.MobilePhone,
                            email = emp.Email,
                            leaving = emp.IsLeft,
                            entryTime = CommonHelper.DateConverter(emp.EntryTime),
                            retireTime = CommonHelper.DateConverter(emp.RetireTime),
                            isFormal = emp.IsFormal,
                            remarks = emp.Remarks,
                            enabled = emp.Enabled,
                            cardId = emp.CardId ?? "",
                            decimalCard = emp.DecimalCard ?? "",
                            devCount = def != null ? def.Count : 0
                        };

            var index = 0;
            foreach (var temp in temps) {
                temp.index = ++index;
                stores.Add(temp);
            }

            return stores;
        }

        private List<DetailModel400105> GetDetail400105(string card) {
            var stores = new List<DetailModel400105>();
            if (string.IsNullOrWhiteSpace(card)) return stores;
            var auths = _mauthorizationService.GetAuthorizationsInCard(card);
            if (auths.Count == 0) return stores;
            var temps = from auth in auths
                        join dev in _workContext.Devices() on auth.DeviceId equals dev.Current.Id
                        join area in _workContext.Areas() on dev.Current.AreaId equals area.Current.Id
                        select new DetailModel400105 {
                            area = area.ToString(),
                            station = dev.Current.StationName,
                            room = dev.Current.RoomName,
                            card = auth.CardId,
                            name = dev.Current.Name
                        };

            var index = 0;
            foreach (var temp in temps) {
                temp.index = ++index;
                stores.Add(temp);
            }

            return stores;
        }

        private List<Model400106> GetBase400106(bool cache, string[] departments, int[] emptypes, int keytype, string keywords) {
            var stores = new List<Model400106>();

            var empys = _employeeService.GetOutEmployees();
            if (departments != null && departments.Length > 0) {
                empys = empys.FindAll(e => departments.Contains(e.DeptId));
            }

            if (emptypes != null && emptypes.Length > 0) {
                empys = empys.FindAll(e => emptypes.Contains(string.IsNullOrWhiteSpace(e.CardId) ? 0 : 1));
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var keys = Common.SplitCondition(keywords);
                if (keys.Length > 0) {
                    switch (keytype) {
                        case 1:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.DecimalCard, keys));
                            break;
                        case 2:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.Name, keys));
                            break;
                        case 3:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.EmpName, keys));
                            break;
                        case 4:
                            empys = empys.FindAll(e => CommonHelper.ConditionContain(e.EmpCode, keys));
                            break;
                    }
                }
            }

            if (empys.Count == 0) return stores;

            var auths = _mauthorizationService.GetAuthorizationsInType(EnmEmpType.OutEmployee);
            if (!_workContext.Role().Id.Equals(U_Role.SuperId)) {
                auths = auths.FindAll(a => _workContext.DeviceKeys().Contains(a.DeviceId));
            }

            var authsInCard = from auth in auths
                              group auth by auth.CardId into g
                              select new { Key = g.Key, Count = g.Count() };

            var temps = from emp in empys
                        join aic in authsInCard on emp.CardId equals aic.Key into lt
                        from def in lt.DefaultIfEmpty()
                        select new Model400106 {
                            id = emp.Id,
                            name = emp.Name,
                            sex = Common.GetSexDisplay(emp.Sex),
                            dept = emp.DeptName,
                            icard = emp.ICardId,
                            icardAddress = emp.ICardAddress,
                            address = emp.Address,
                            company = emp.CompanyName,
                            project = emp.ProjectName,
                            workPhone = emp.WorkPhone,
                            mobilePhone = emp.MobilePhone,
                            email = emp.Email,
                            empName = string.IsNullOrWhiteSpace(emp.EmpName) ? "" : string.Format("{0}({1})", emp.EmpName, emp.EmpCode ?? ""),
                            remarks = emp.Remarks,
                            enabled = emp.Enabled,
                            cardId = emp.CardId ?? "",
                            decimalCard = emp.DecimalCard ?? "",
                            devCount = def != null ? def.Count : 0
                        };

            var index = 0;
            foreach (var temp in temps) {
                temp.index = ++index;
                stores.Add(temp);
            }

            return stores;
        }

        private List<DetailModel400106> GetDetail400106(string card) {
            var stores = new List<DetailModel400106>();
            if (string.IsNullOrWhiteSpace(card)) return stores;
            var auths = _mauthorizationService.GetAuthorizationsInCard(card);
            if (auths.Count == 0) return stores;
            var temps = from auth in auths
                        join dev in _workContext.Devices() on auth.DeviceId equals dev.Current.Id
                        join area in _workContext.Areas() on dev.Current.AreaId equals area.Current.Id
                        select new DetailModel400106 {
                            area = area.ToString(),
                            station = dev.Current.StationName,
                            room = dev.Current.RoomName,
                            card = auth.CardId,
                            name = dev.Current.Name
                        };

            var index = 0;
            foreach (var temp in temps) {
                temp.index = ++index;
                stores.Add(temp);
            }

            return stores;
        }

        private List<Model400201> GetHistory400201(string parent, DateTime startDate, DateTime endDate, string[] statypes, string[] roomtypes, string[] devtypes, string[] points, string keywords, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400201, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400201>(key).ToList();

            var values = new List<V_HMeasure>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                values = _measureService.GetMeasures(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) values = _measureService.GetMeasures(startDate, endDate).FindAll(s => current.Keys.Contains(s.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        values = _measureService.GetMeasuresInStation(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Room) {
                        values = _measureService.GetMeasuresInRoom(id, startDate, endDate);
                    } else if (nodeType == EnmSSH.Device) {
                        values = _measureService.GetMeasuresInDevice(id, startDate, endDate);
                    }
                }
            }

            var devices = _workContext.Devices();
            if (statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Current.StaTypeId));

            if (roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Current.RoomTypeId));

            if (devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.SubType.Id));

            if (points != null && points.Length > 0)
                values = values.FindAll(p => points.Contains(p.PointId));

            var signals = _signalService.GetAllSignals(values.Select(v => new Kv<string, string>(v.DeviceId, v.PointId)));
            if (!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) signals = signals.FindAll(p => CommonHelper.ConditionContain(p.PointName, names));
            }

            var stores = from val in values
                         join signal in signals on new { val.DeviceId, val.PointId } equals new { signal.DeviceId, signal.PointId }
                         join device in devices on val.DeviceId equals device.Current.Id
                         select new Model400201 {
                             area = device.Current.AreaName,
                             station = device.Current.StationName,
                             room = device.Current.RoomName,
                             device = device.Current.Name,
                             point = signal.PointName,
                             name = signal.OfficialName,
                             type = Common.GetPointTypeDisplay(signal.PointType),
                             value = val.Value,
                             unit = Common.GetUnitDisplay(signal.PointType, val.Value.ToString(), signal.UnitState),
                             status = Common.GetPointStatusDisplay(EnmState.Normal),
                             time = CommonHelper.DateTimeConverter(val.UpdateTime)
                         };

            if (stores.Count() <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, stores, GlobalCacheInterval.Site_Interval);
            }

            return stores.ToList();
        }

        private List<AlmStore<A_HAlarm>> GetHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int[] types, string confirmers, string keywords, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400202, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<AlmStore<A_HAlarm>>(key).ToList();

            List<A_HAlarm> alarms = null;
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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

            if (alarms == null) return null;

            var noralarms = new List<A_HAlarm>();
            var scalarms = new List<A_HAlarm>();
            var fsualarms = new List<A_HAlarm>();
            foreach (var alarm in alarms) {
                if (Common.IsSystemSCAlarm(alarm.FsuId))
                    scalarms.Add(alarm);
                else if (Common.IsSystemFSUAlarm(alarm.FsuId))
                    fsualarms.Add(alarm);
                else
                    noralarms.Add(alarm);
            }

            var stores = _workContext.AlarmsToStore(noralarms);
            if (types != null && types.Contains(1)) {
                if (scalarms.Count > 0) {
                    stores.AddRange(_workContext.AlarmsToSc(scalarms));
                }

                if (fsualarms.Count > 0) {
                    stores.AddRange(_workContext.AlarmsToFsu(fsualarms));
                }
            }

            if (types != null && types.Contains(2)) {
                var masks = _hisAlarmService.GetMaskedAlarms(startDate, endDate);
                if (masks.Count > 0 && !string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                    var keys = Common.SplitKeys(parent);
                    if (keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if (nodeType == EnmSSH.Area) {
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
                            if (current != null) masks = masks.FindAll(a => current.Keys.Contains(a.AreaId));
                        } else if (nodeType == EnmSSH.Station) {
                            masks = masks.FindAll(a => a.StationId.Equals(id));
                        } else if (nodeType == EnmSSH.Room) {
                            masks = masks.FindAll(a => a.RoomId.Equals(id));
                        } else if (nodeType == EnmSSH.Device) {
                            masks = masks.FindAll(a => a.DeviceId.Equals(id));
                        }
                    }
                }

                stores.AddRange(_workContext.AlarmsToStore(masks));
            }

            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.StationTypeId));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.RoomTypeId));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Current.PointId));

            if (levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (!string.IsNullOrWhiteSpace(confirmers)) {
                var names = Common.SplitCondition(confirmers);
                if (names.Length > 0) stores = stores.FindAll(p => !string.IsNullOrWhiteSpace(p.Current.Confirmer) && CommonHelper.ConditionContain(p.Current.Confirmer, names));
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) stores = stores.FindAll(p => CommonHelper.ConditionContain(p.PointName, names));
            }

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            stores = stores.OrderByDescending(s => s.Current.StartTime).ToList();
            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList<AlmStore<A_HAlarm>>(key, stores, GlobalCacheInterval.Site_Interval);
            }

            return stores;
        }

        private List<Model400203> GetHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400203, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400203>(key).ToList();

            var stations = _workContext.Stations();
            var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            var stores = _workContext.AlarmsToStore(alarms);
            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    stations = stations.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                    stores = stores.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0) {
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));
                stores = stores.FindAll(s => staTypes.Contains(s.StationTypeId));
            }

            if (roomTypes != null && roomTypes.Length > 0) {
                stores = stores.FindAll(r => roomTypes.Contains(r.RoomTypeId));
            }

            if (subDeviceTypes != null && subDeviceTypes.Length > 0) {
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));
            }

            if (subLogicTypes != null && subLogicTypes.Length > 0) {
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));
            }

            if (points != null && points.Length > 0) {
                stores = stores.FindAll(a => points.Contains(a.Current.PointId));
            }

            if (levels != null && levels.Length > 0) {
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));
            }

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            var result = new List<Model400203>();
            var index = 0;
            foreach (var station in stations) {
                var _alarms = stores.FindAll(s => s.Current.StationId == station.Current.Id);
                var _alarms1 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level1);
                var _alarms2 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level2);
                var _alarms3 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level3);
                var _alarms4 = _alarms.FindAll(a => a.Current.AlarmLevel == EnmAlarm.Level4);

                result.Add(new Model400203 {
                    index = ++index,
                    area = station.Current.AreaName,
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

            if (stores.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList<Model400203>(key, result, GlobalCacheInterval.ReSet_Interval);
            }

            return result;
        }

        private List<Model400204> GetHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400204, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400204>(key).ToList();

            var stations = _workContext.Stations();
            var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            var stores = _workContext.AlarmsToStore(alarms);
            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    stations = stations.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                    stores = stores.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0) {
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));
                stores = stores.FindAll(s => staTypes.Contains(s.StationTypeId));
            }

            if (roomTypes != null && roomTypes.Length > 0) {
                stores = stores.FindAll(r => roomTypes.Contains(r.RoomTypeId));
            }

            if (devTypes != null && devTypes.Length > 0) {
                stores = stores.FindAll(d => devTypes.Contains(d.DeviceTypeId));
            }

            if (subLogicTypes != null && subLogicTypes.Length > 0) {
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));
            }

            if (points != null && points.Length > 0) {
                stores = stores.FindAll(a => points.Contains(a.Current.PointId));
            }

            if (levels != null && levels.Length > 0) {
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));
            }

            if (confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if (project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            var index = 0;
            var models = new List<Model400204>();
            foreach (var station in stations) {
                var _alarms = stores.FindAll(s => s.Current.StationId == station.Current.Id);

                models.Add(new Model400204 {
                    index = ++index,
                    area = station.Current.AreaName,
                    stationid = station.Current.Id,
                    station = station.Current.Name,
                    alarms = _alarms
                });
            }

            if (stores.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList<Model400204>(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private DataTable GetDataTable400204(List<C_DeviceType> columns) {
            var model = new DataTable("DataTable400204");

            var column0 = new DataColumn("index", typeof(int));
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

            foreach (var column in columns) {
                var _column = new DataColumn(column.Id, typeof(int));
                _column.ExtendedProperties.Add("ExcelDisplayName", column.Name);
                model.Columns.Add(_column);
            }

            var column4 = new DataColumn("total", typeof(int));
            column4.ExtendedProperties.Add("ExcelDisplayName", "总计");
            model.Columns.Add(column4);

            return model;
        }

        private List<Model400205> GetHistory400205(string parent, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var models = new List<Model400205>();
            var stations = _workContext.Stations();
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var projects = _projectService.GetPagedProjectsInSpan(startDate, endDate);
            var appSets = this.GetReservationsInDevices(projects);
            foreach (var station in stations) {
                var devices = _workContext.Devices().FindAll(d => d.Current.StationId == station.Current.Id);
                var devSet = new HashSet<string>();
                foreach (var device in devices) {
                    devSet.Add(device.Current.Id);
                }

                var appointments = new List<M_Reservation>();
                foreach (var appSet in appSets) {
                    if (devSet.Overlaps(appSet.Value))
                        appointments.Add(appSet.Key);
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

                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
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

            var stations = _workContext.Stations();
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var models = new List<Model400206>();
            var appSets = this.GetReservationsInDevices(startDate, endDate);
            var projects = _projectService.GetPagedProjects();
            foreach (var station in stations) {
                var devices = _workContext.Devices().FindAll(d => d.Current.StationId == station.Current.Id);
                var devSet = new HashSet<string>();
                foreach (var device in devices) {
                    devSet.Add(device.Current.Id);
                }

                var appointments = new List<M_Reservation>();
                foreach (var appSet in appSets) {
                    if (devSet.Overlaps(appSet.Value))
                        appointments.Add(appSet.Key);
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

                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
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

        private List<Model400207_1> GetHistory400207_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400207_1, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400207_1>(key).ToList();

            var models = new List<Model400207_1>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var offs = _offlineService.GetHistory(EnmSSH.Station, EnmFormula.TD, startDate, endDate);
            foreach (var station in stations) {
                var details = offs.FindAll(a => a.Id.Equals(station.Current.Id));
                models.Add(new Model400207_1 {
                    index = ++index,
                    area = station.Current.AreaName,
                    id = station.Current.Id,
                    name = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = details.Count,
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                    details = details.Select(d => new DetailModel400207_1 {
                        area = station.Current.AreaName,
                        name = station.Current.Name,
                        start = CommonHelper.DateTimeConverter(d.StartTime),
                        end = CommonHelper.DateTimeConverter(d.EndTime),
                        interval = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                    }).ToList()
                });
            }

            if (offs.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private List<Model400207_2> GetHistory400207_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400207_2, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400207_2>(key).ToList();

            var models = new List<Model400207_2>();
            var rooms = _workContext.Rooms();
            if (types != null && types.Length > 0)
                rooms = rooms.FindAll(s => types.Contains(s.Current.Type.Id));


            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) rooms = rooms.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    rooms = rooms.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
                }
            }

            var index = 0;
            var offs = _offlineService.GetHistory(EnmSSH.Room, EnmFormula.TD, startDate, endDate);
            foreach (var room in rooms) {
                var details = offs.FindAll(a => a.Id.Equals(room.Current.Id));
                models.Add(new Model400207_2 {
                    index = ++index,
                    area = room.Current.AreaName,
                    station = room.Current.StationName,
                    id = room.Current.Id,
                    name = room.Current.Name,
                    type = room.Current.Type.Name,
                    count = details.Count,
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                    details = details.Select(d => new DetailModel400207_2 {
                        area = room.Current.AreaName,
                        station = room.Current.StationName,
                        name = room.Current.Name,
                        start = CommonHelper.DateTimeConverter(d.StartTime),
                        end = CommonHelper.DateTimeConverter(d.EndTime),
                        interval = CommonHelper.IntervalConverter(d.StartTime, d.EndTime)
                    }).ToList()
                });
            }

            if (offs.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private List<Model400208_1> GetHistory400208_1(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400208_1, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400208_1>(key).ToList();

            var models = new List<Model400208_1>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var offlines = _offlineService.GetHistory(EnmSSH.Device, EnmFormula.FD, startDate, endDate);
            var offdevs = from off in offlines
                          join dev in _workContext.Devices() on off.Id equals dev.Current.Id
                          select new { Device = dev.Current, Off = off };

            var index = 0;
            foreach (var station in stations) {
                var details = offdevs.Where(o => o.Device.StationId.Equals(station.Current.Id));
                models.Add(new Model400208_1 {
                    index = ++index,
                    area = station.Current.AreaName,
                    id = station.Current.Id,
                    name = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = details.Count(),
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Any() ? details.Sum(d => d.Off.EndTime.Subtract(d.Off.StartTime).TotalSeconds) : 0)),
                    value = details.Sum(d => d.Off.Value).ToString(),
                    details = details.Select(d => new DetailModel400208 {
                        area = d.Device.AreaName,
                        station = d.Device.StationName,
                        room = d.Device.RoomName,
                        name = d.Device.Name,
                        start = CommonHelper.DateTimeConverter(d.Off.StartTime),
                        end = CommonHelper.DateTimeConverter(d.Off.EndTime),
                        interval = CommonHelper.IntervalConverter(d.Off.StartTime, d.Off.EndTime),
                        value = d.Off.Value.ToString()
                    }).ToList()
                });
            }

            if (offlines.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private List<Model400208_2> GetHistory400208_2(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400208_2, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400208_2>(key).ToList();

            var models = new List<Model400208_2>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null || rtValues.fdjzLeiXing == null || rtValues.fdjzLeiXing.Length == 0)
                return models;

            var devices = _workContext.Devices().FindAll(d => rtValues.fdjzLeiXing.Contains(d.Current.SubType.Id));
            if (types != null && types.Length > 0) devices = devices.FindAll(d => types.Contains(d.Current.SubType.Id));

            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
                }
            }

            var index = 0;
            var offlines = _offlineService.GetHistory(EnmSSH.Device, EnmFormula.FD, startDate, endDate);
            foreach (var device in devices) {
                var details = offlines.FindAll(o => o.Id.Equals(device.Current.Id));
                models.Add(new Model400208_2 {
                    index = ++index,
                    area = device.Current.AreaName,
                    station = device.Current.StationName,
                    room = device.Current.RoomName,
                    id = device.Current.Id,
                    name = device.Current.Name,
                    type = device.Current.Type.Name,
                    count = details.Count,
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds))),
                    value = details.Sum(d => d.Value).ToString(),
                    details = details.Select(d => new DetailModel400208 {
                        area = device.Current.AreaName,
                        station = device.Current.StationName,
                        room = device.Current.RoomName,
                        name = device.Current.Name,
                        start = CommonHelper.DateTimeConverter(d.StartTime),
                        end = CommonHelper.DateTimeConverter(d.EndTime),
                        interval = CommonHelper.IntervalConverter(d.StartTime, d.EndTime),
                        value = d.Value.ToString()
                    }).ToList()
                });
            }

            if (offlines.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private DataTable GetHistory400208_3(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400208_3, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable400208_3(period, startDate, endDate);
            var rtValues = _workContext.RtValues();
            if (rtValues == null || rtValues.fdjzLeiXing == null || rtValues.fdjzLeiXing.Length == 0)
                return model;

            var devices = _workContext.Devices().FindAll(d => rtValues.fdjzLeiXing.Contains(d.Current.SubType.Id));
            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
                }
            }

            var offlines = _offlineService.GetHistory(EnmSSH.Device, EnmFormula.FD, startDate, endDate);
            foreach (var device in devices) {
                var details = offlines.FindAll(o => o.Id.Equals(device.Current.Id));

                var row = model.NewRow();
                row["area"] = device.Current.AreaName;
                row["station"] = device.Current.StationName;
                row["room"] = device.Current.RoomName;
                row["name"] = device.Current.Name;
                for (var k = 5; k < model.Columns.Count; k++) {
                    var column = model.Columns[k];
                    var start = (DateTime)column.ExtendedProperties["Start"];
                    var end = (DateTime)column.ExtendedProperties["End"];
                    row[k] = Math.Round(details.Count > 0 ? details.FindAll(c => c.StartTime >= start && c.StartTime <= end).Sum(c => c.Value) : 0, 3, MidpointRounding.AwayFromZero);
                }
                model.Rows.Add(row);
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private List<Model400209> GetHistory400209(bool cache, string parent, DateTime startDate, DateTime endDate, int[] recTypes, int keyType, string keywords) {
            endDate = endDate.AddSeconds(86399);
            var stores = new List<Model400209>();

            List<H_CardRecord> records = null;
            if (parent == "root") {
                records = _cardRecordService.GetRecords(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) records = _cardRecordService.GetRecords(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        records = _cardRecordService.GetRecordsInStation(startDate, endDate, id);
                    } else if (nodeType == EnmSSH.Room) {
                        records = _cardRecordService.GetRecordsInRoom(startDate, endDate, id);
                    } else if (nodeType == EnmSSH.Device) {
                        records = _cardRecordService.GetRecordsInDevice(startDate, endDate, id);
                    }
                }
            }

            if (records == null || records.Count == 0) return stores;

            if (recTypes != null && recTypes.Length > 0) {
                records = records.FindAll(r => recTypes.Contains((int)r.Type));
            }

            var tStores = from rec in records
                          join dev in _workContext.Devices() on rec.DeviceId equals dev.Current.Id
                          join area in _workContext.Areas() on rec.AreaId equals area.Current.Id
                          select new {
                              area = area.ToString(),
                              device = dev.Current,
                              record = rec
                          };

            if (!tStores.Any()) return stores;

            var index = 0;
            var remarks = new EnmRecRemark[] { EnmRecRemark.Remark0, EnmRecRemark.Remark8, EnmRecRemark.Remark9, EnmRecRemark.Remark10, EnmRecRemark.Remark11 };
            foreach (var store in tStores) {
                var normal = remarks.Contains(store.record.Remark);
                stores.Add(new Model400209 {
                    index = ++index,
                    area = store.area.ToString(),
                    station = store.device.StationName,
                    room = store.device.RoomName,
                    device = store.device.Name,
                    recType = Common.GetRecTypeDisplay(store.record.Type),
                    cardId = normal ? store.record.CardId : null,
                    decimalCard = normal ? store.record.DecimalCard : store.record.CardId,
                    time = CommonHelper.DateTimeConverter(store.record.PunchTime),
                    remark = Common.GetRecRemarkDisplay(store.record.Remark)
                });
            }

            var valids = stores.FindAll(s => !string.IsNullOrWhiteSpace(s.cardId));
            if (valids.Count > 0) {
                var employees = _employeeService.GetEmployees().FindAll(e => !string.IsNullOrWhiteSpace(e.CardId));
                if (employees.Count > 0) {
                    var dictionaries = employees.ToDictionary(k => k.CardId, v => v);
                    foreach (var val in valids) {
                        if (dictionaries.ContainsKey(val.cardId)) {
                            var current = dictionaries[val.cardId];
                            val.employeeCode = current.Code;
                            val.employeeName = current.Name;
                            val.employeeType = Common.GetEmployeeTypeDisplay(current.Type);
                            val.department = current.DeptName;
                        }
                    }
                }

                var outEmployees = _employeeService.GetOutEmployees().FindAll(e => !string.IsNullOrWhiteSpace(e.CardId));
                if (outEmployees.Count > 0) {
                    var dictionaries = outEmployees.ToDictionary(k => k.CardId, v => v);
                    foreach (var val in valids) {
                        if (dictionaries.ContainsKey(val.cardId)) {
                            var current = dictionaries[val.cardId];
                            val.employeeCode = null;
                            val.employeeName = current.Name;
                            val.employeeType = Common.GetEmployeeTypeDisplay(current.Type);
                            val.department = current.DeptName;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var keys = Common.SplitCondition(keywords);
                if (keys.Length > 0) {
                    switch (keyType) {
                        case 1:
                            stores = stores.FindAll(s => !string.IsNullOrWhiteSpace(s.cardId) && CommonHelper.ConditionContain(s.decimalCard, keys));
                            break;
                        case 2:
                            stores = stores.FindAll(e => CommonHelper.ConditionContain(e.employeeCode, keys));
                            break;
                        case 3:
                            stores = stores.FindAll(s => CommonHelper.ConditionContain(s.employeeName, keys));
                            break;
                    }
                }
            }

            return stores;
        }

        private List<Model400210> GetHistory400210(bool cache, string parent, int[] empTypes, int keyType, string keywords, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400210, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400210>>(key);

            var models = new List<Model400210>();

            List<H_CardRecord> records = null;
            if (parent == "root") {
                records = _cardRecordService.GetRecords(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) records = _cardRecordService.GetRecords(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    } else if (nodeType == EnmSSH.Station) {
                        records = _cardRecordService.GetRecordsInStation(startDate, endDate, id);
                    } else if (nodeType == EnmSSH.Room) {
                        records = _cardRecordService.GetRecordsInRoom(startDate, endDate, id);
                    } else if (nodeType == EnmSSH.Device) {
                        records = _cardRecordService.GetRecordsInDevice(startDate, endDate, id);
                    }
                }
            }

            if (records == null || records.Count == 0) return models;

            var remarks = new EnmRecRemark[] { EnmRecRemark.Remark0, EnmRecRemark.Remark8, EnmRecRemark.Remark9, EnmRecRemark.Remark10, EnmRecRemark.Remark11 };
            records = records.FindAll(r => remarks.Contains(r.Remark));
            if (records.Count == 0) return models;

            var tStores = from rec in records
                          join dev in _workContext.Devices() on rec.DeviceId equals dev.Current.Id
                          join area in _workContext.Areas() on rec.AreaId equals area.Current.Id
                          select new {
                              area = area.ToString(),
                              device = dev.Current,
                              record = rec
                          };

            if (!tStores.Any()) return models;

            var gStores = from store in tStores
                          group store by store.record.CardId into g
                          select new {
                              CardId = g.Key,
                              Stores = g.OrderBy(s => s.record.PunchTime)
                          };

            if (empTypes == null || empTypes.Length == 0 || empTypes.Contains((int)EnmEmpType.Employee)) {
                var employees = _employeeService.GetEmployees().FindAll(e => !string.IsNullOrWhiteSpace(e.CardId));
                if (employees.Count > 0) {
                    var temps = from store in gStores
                                join emp in employees on store.CardId equals emp.CardId
                                select new { Employee = emp, Stores = store.Stores };

                    foreach (var temp in temps) {
                        var model = new Model400210 {
                            employeeCode = temp.Employee.Code,
                            employeeName = temp.Employee.Name,
                            employeeType = Common.GetEmployeeTypeDisplay(temp.Employee.Type),
                            department = temp.Employee.DeptName,
                            cardId = temp.Employee.CardId,
                            decimalCard = temp.Employee.DecimalCard,
                            count = temp.Stores.Count(),
                            details = new List<DetailModel400210>()
                        };

                        var index = 0;
                        foreach (var detail in temp.Stores) {
                            model.details.Add(new DetailModel400210 {
                                index = ++index,
                                area = detail.area,
                                station = detail.device.StationName,
                                room = detail.device.RoomName,
                                device = detail.device.Name,
                                recType = Common.GetRecTypeDisplay(detail.record.Type),
                                cardId = detail.record.CardId,
                                decimalCard = detail.record.DecimalCard,
                                time = CommonHelper.DateTimeConverter(detail.record.PunchTime),
                                remark = Common.GetRecRemarkDisplay(detail.record.Remark)
                            });
                        }

                        models.Add(model);
                    }
                }
            }

            if (empTypes == null || empTypes.Length == 0 || empTypes.Contains((int)EnmEmpType.OutEmployee)) {
                var outEmployees = _employeeService.GetOutEmployees().FindAll(e => !string.IsNullOrWhiteSpace(e.CardId));
                if (outEmployees.Count > 0) {
                    var temps = from store in gStores
                                join emp in outEmployees on store.CardId equals emp.CardId
                                select new { Employee = emp, Stores = store.Stores };

                    foreach (var temp in temps) {
                        var model = new Model400210 {
                            employeeCode = null,
                            employeeName = temp.Employee.Name,
                            employeeType = Common.GetEmployeeTypeDisplay(temp.Employee.Type),
                            department = temp.Employee.DeptName,
                            cardId = temp.Employee.CardId,
                            decimalCard = temp.Employee.DecimalCard,
                            count = temp.Stores.Count(),
                            details = new List<DetailModel400210>()
                        };

                        var index = 0;
                        foreach (var detail in temp.Stores) {
                            model.details.Add(new DetailModel400210 {
                                index = ++index,
                                area = detail.area,
                                station = detail.device.StationName,
                                room = detail.device.RoomName,
                                device = detail.device.Name,
                                recType = Common.GetRecTypeDisplay(detail.record.Type),
                                cardId = detail.record.CardId,
                                decimalCard = detail.record.DecimalCard,
                                time = CommonHelper.DateTimeConverter(detail.record.PunchTime),
                                remark = Common.GetRecRemarkDisplay(detail.record.Remark)
                            });
                        }

                        models.Add(model);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var keys = Common.SplitCondition(keywords);
                if (keys.Length > 0) {
                    switch (keyType) {
                        case 1:
                            models = models.FindAll(s => CommonHelper.ConditionContain(s.decimalCard, keys));
                            break;
                        case 2:
                            models = models.FindAll(e => CommonHelper.ConditionContain(e.employeeCode, keys));
                            break;
                        case 3:
                            models = models.FindAll(s => CommonHelper.ConditionContain(s.employeeName, keys));
                            break;
                    }
                }
            }

            if (models.Sum(m => m.details.Count) <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.Set(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private List<Model400211> GetHistory400211(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400211, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400211>(key).ToList();

            var models = new List<Model400211>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
            }

            if (!string.IsNullOrWhiteSpace(parent) && !"root".Equals(parent)) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            if (stations.Count == 0) return models;

            var index = 0;
            var procedures = _batimeService.GetProcedures(startDate, endDate);
            var procdevs = from proc in procedures
                           join device in _workContext.Devices() on proc.DeviceId equals device.Current.Id
                           select new { Device = device.Current, Procedure = proc };

            foreach (var station in stations) {
                var details = procdevs.Where(a => a.Device.StationId == station.Current.Id);
                var model = new Model400211 {
                    index = ++index,
                    area = station.Current.AreaName,
                    id = station.Current.Id,
                    name = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = details.Count(),
                    interval = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(details.Any() ? details.Sum(d => d.Procedure.EndTime.Subtract(d.Procedure.StartTime).TotalSeconds) : 0)),
                    details = details.Select(d => new DetailModel400211 {
                        area = d.Device.AreaName,
                        station = d.Device.StationName,
                        room = d.Device.RoomName,
                        id = d.Device.Id,
                        name = d.Device.Name,
                        pack = d.Procedure.PackId,
                        start = CommonHelper.DateTimeConverter(d.Procedure.StartTime),
                        end = CommonHelper.DateTimeConverter(d.Procedure.EndTime),
                        interval = CommonHelper.IntervalConverter(d.Procedure.StartTime, d.Procedure.EndTime),
                        proctime = CommonHelper.DateTimeConverter(d.Procedure.ProcTime)
                    }).ToList()
                };

                models.Add(model);
            }

            if (procedures.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.ReSet_Interval);
            }

            return models;
        }

        private List<Model400303> GetChart400303(string parent, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400303, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400303>(key).ToList();

            var models = new List<Model400303>();
            var procedures = new List<V_BatTime>();
            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Device) {
                procedures.AddRange(_batimeService.GetProcedures(nodeKey.Value, startDate, endDate));
            } else {
                procedures = _batimeService.GetProcedures(startDate, endDate);
            }
            if (procedures.Count == 0) return models;

            var devices = _workContext.Devices();
            if (nodeKey.Key == EnmSSH.Area) {
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    devices = devices.FindAll(d => d.Current.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    devices = devices.FindAll(d => d.Current.RoomId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Device) {
                    devices = devices.FindAll(d => d.Current.Id.Equals(nodeKey.Value));
                }
            }
            if (devices.Count == 0) return models;

            var procdevs = from proc in procedures
                           join device in devices on proc.DeviceId equals device.Current.Id
                           select new { Device = device.Current, Procedure = proc };

            var index = 0;
            foreach (var proc in procdevs) {
                models.Add(new Model400303 {
                    index = ++index,
                    area = proc.Device.AreaName,
                    station = proc.Device.StationName,
                    room = proc.Device.RoomName,
                    id = proc.Device.Id,
                    name = proc.Device.Name,
                    pack = proc.Procedure.PackId,
                    start = CommonHelper.DateTimeConverter(proc.Procedure.StartTime),
                    end = CommonHelper.DateTimeConverter(proc.Procedure.EndTime),
                    interval = CommonHelper.IntervalConverter(proc.Procedure.StartTime, proc.Procedure.EndTime),
                    proctime = CommonHelper.DateTimeConverter(proc.Procedure.ProcTime)
                });
            }

            if (procedures.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, models, GlobalCacheInterval.Site_Interval);
            }

            return models;
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(DateTime start, DateTime end) {
            var entities = _reservationService.GetReservationsInSpan(start, end).FindAll(r => r.Status == EnmResult.Success);
            return this.GetReservationsInDevices(entities);
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Project> projects) {
            var matchs = projects.Select(p => p.Id);
            var reservations = _reservationService.GetPagedReservations().Where(a => matchs.Contains(a.ProjectId));
            return this.GetReservationsInDevices(reservations);
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Reservation> entities) {
            var appSets = new List<Kv<M_Reservation, HashSet<string>>>();
            foreach (var entity in entities) {
                var appSet = new Kv<M_Reservation, HashSet<string>>() { Key = entity, Value = new HashSet<string>() };
                var nodes = _nodesInReservationService.GetNodesInReservationsInReservation(entity.Id);
                foreach (var node in nodes) {
                    if (node.NodeType == EnmSSH.Device) {
                        appSet.Value.Add(node.NodeId);
                    }

                    if (node.NodeType == EnmSSH.Room) {
                        var devices = _workContext.Devices().FindAll(d => d.Current.RoomId == node.NodeId);
                        foreach (var device in devices) {
                            appSet.Value.Add(device.Current.Id);
                        }
                    }

                    if (node.NodeType == EnmSSH.Station) {
                        var devices = _workContext.Devices().FindAll(d => d.Current.StationId == node.NodeId);
                        foreach (var device in devices) {
                            appSet.Value.Add(device.Current.Id);
                        }
                    }

                    if (node.NodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == node.NodeId);
                        if (current != null) {
                            var devices = _workContext.Devices().FindAll(d => current.Keys.Contains(d.Current.AreaId));
                            foreach (var device in devices) {
                                appSet.Value.Add(device.Current.Id);
                            }
                        }
                    }
                }

                appSets.Add(appSet);
            }

            return appSets;
        }

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return null;

            var key1 = string.Format(GlobalCacheKeys.Report_400401, "alarm", _workContext.Identifier());
            var key2 = string.Format(GlobalCacheKeys.Report_400401, "chart", _workContext.Identifier());

            if (!cache) {
                if (_cacheManager.IsSet(key1))
                    _cacheManager.Remove(key1);

                if (_cacheManager.IsSet(key2))
                    _cacheManager.Remove(key2);
            }

            if (_cacheManager.IsSet(key1) && _cacheManager.IsSet(key2)) {
                var cache1 = _cacheManager.GetItemsFromList<AlmStore<A_HAlarm>>(key1).ToList();
                var cache2 = _cacheManager.GetItemsFromList<ChartsModel>(key2).ToList();
                return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(cache1, cache2);
            }

            List<A_HAlarm> alarms = null;
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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

            if (alarms == null) return null;

            alarms = alarms.FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.StationTypeId));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.RoomTypeId));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Current.PointId));

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

            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList<AlmStore<A_HAlarm>>(key1, stores, GlobalCacheInterval.Site_Interval);
                _cacheManager.AddItemsToList<ChartsModel>(key2, charts, GlobalCacheInterval.Site_Interval);
            }

            return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
        }

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return null;

            var key1 = string.Format(GlobalCacheKeys.Report_400402, "alarm", _workContext.Identifier());
            var key2 = string.Format(GlobalCacheKeys.Report_400402, "chart", _workContext.Identifier());

            if (!cache) {
                if (_cacheManager.IsSet(key1))
                    _cacheManager.Remove(key1);

                if (_cacheManager.IsSet(key2))
                    _cacheManager.Remove(key2);
            }

            if (_cacheManager.IsSet(key1) && _cacheManager.IsSet(key2)) {
                var cache1 = _cacheManager.GetItemsFromList<AlmStore<A_HAlarm>>(key1).ToList();
                var cache2 = _cacheManager.GetItemsFromList<ChartsModel>(key2).ToList();
                return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(cache1, cache2);
            }

            List<A_HAlarm> alarms = null;
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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

            if (alarms == null) return null;

            alarms = alarms.FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.StationTypeId));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.RoomTypeId));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Current.PointId));

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

            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList<AlmStore<A_HAlarm>>(key1, stores, GlobalCacheInterval.Site_Interval);
                _cacheManager.AddItemsToList<ChartsModel>(key2, charts, GlobalCacheInterval.Site_Interval);
            }

            return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
        }

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return null;

            var key1 = string.Format(GlobalCacheKeys.Report_400403, "alarm", _workContext.Identifier());
            var key2 = string.Format(GlobalCacheKeys.Report_400403, "chart", _workContext.Identifier());

            if (!cache) {
                if (_cacheManager.IsSet(key1))
                    _cacheManager.Remove(key1);

                if (_cacheManager.IsSet(key2))
                    _cacheManager.Remove(key2);
            }

            if (_cacheManager.IsSet(key1) && _cacheManager.IsSet(key2)) {
                var cache1 = _cacheManager.GetItemsFromList<AlmStore<A_HAlarm>>(key1).ToList();
                var cache2 = _cacheManager.GetItemsFromList<ChartsModel>(key2).ToList();
                return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(cache1, cache2);
            }

            List<A_HAlarm> alarms = null;
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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

            if (alarms == null) return null;

            alarms = alarms.FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            var stores = _workContext.AlarmsToStore(alarms);
            if (staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.StationTypeId));

            if (roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.RoomTypeId));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Current.PointId));

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

            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList<AlmStore<A_HAlarm>>(key1, stores, GlobalCacheInterval.Site_Interval);
                _cacheManager.AddItemsToList<ChartsModel>(key2, charts, GlobalCacheInterval.Site_Interval);
            }

            return new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
        }

        private DataTable GetCustom400404(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400404, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable400404(period, startDate, endDate);
            if (string.IsNullOrWhiteSpace(parent))
                return model;

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Device) {
                var current = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                if (current == null) return model;
                var signals = _signalService.GetVSignals(nodeKey.Value, EnmVSignalCategory.Category03);
                if (signals.Count == 0) return model;
                var values = _cabinetService.GetHistory(nodeKey.Value, EnmVSignalCategory.Category03, startDate, endDate);
                foreach (var signal in signals) {
                    var details = values.FindAll(v => v.DeviceId.Equals(signal.DeviceId) && v.PointId.Equals(signal.PointId));

                    var row = model.NewRow();
                    row["area"] = current.Current.AreaName;
                    row["station"] = current.Current.StationName;
                    row["room"] = current.Current.RoomName;
                    row["name"] = signal.Name;
                    for (var k = 5; k < model.Columns.Count; k++) {
                        var column = model.Columns[k];
                        var start = (DateTime)column.ExtendedProperties["Start"];
                        var end = (DateTime)column.ExtendedProperties["End"];

                        var _values = 0d;
                        var _details = details.FindAll(d => d.ValueTime >= start && d.ValueTime <= end);
                        if (_details.Count > 0) _values = Math.Round(_details.Average(d => d.Value), 2, MidpointRounding.AwayFromZero);
                        row[k] = _values;
                    }
                    model.Rows.Add(row);
                }
            } else {
                var signals = _signalService.GetVSignals(EnmVSignalCategory.Category03);
                if (signals.Count == 0) return model;
                var devkeys = new HashSet<string>(signals.Select(s => s.DeviceId));
                var devices = _workContext.Devices().FindAll(d => devkeys.Contains(d.Current.Id));
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
                }
                if (devices.Count == 0) return model;

                var _signals = from signal in signals
                               join dev in devices on signal.DeviceId equals dev.Current.Id
                               select new { Device = dev.Current, Signal = signal };

                var values = _cabinetService.GetHistory(EnmVSignalCategory.Category03, startDate, endDate);
                foreach (var _signal in _signals) {
                    var details = values.FindAll(v => v.DeviceId.Equals(_signal.Signal.DeviceId) && v.PointId.Equals(_signal.Signal.PointId));

                    var row = model.NewRow();
                    row["area"] = _signal.Device.AreaName;
                    row["station"] = _signal.Device.StationName;
                    row["room"] = _signal.Device.RoomName;
                    row["name"] = _signal.Signal.Name;
                    for (var k = 5; k < model.Columns.Count; k++) {
                        var column = model.Columns[k];
                        var start = (DateTime)column.ExtendedProperties["Start"];
                        var end = (DateTime)column.ExtendedProperties["End"];

                        var _values = 0d;
                        var _details = details.FindAll(d => d.ValueTime >= start && d.ValueTime <= end);
                        if (_details.Count > 0) _values = Math.Round(_details.Average(d => d.Value), 2, MidpointRounding.AwayFromZero);
                        row[k] = _values;
                    }
                    model.Rows.Add(row);
                }
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private DataTable GetDataTable400404(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("400404");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("area", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "所属区域");
            model.Columns.Add(column1);

            var column2 = new DataColumn("station", typeof(string));
            column2.ExtendedProperties.Add("ExcelDisplayName", "所属站点");
            model.Columns.Add(column2);

            var column3 = new DataColumn("room", typeof(string));
            column3.ExtendedProperties.Add("ExcelDisplayName", "所属机房");
            model.Columns.Add(column3);

            var column4 = new DataColumn("name", typeof(string));
            column4.ExtendedProperties.Add("ExcelDisplayName", "分路编号");
            model.Columns.Add(column4);

            var dates = CommonHelper.GetPeriods(start, end, period);
            foreach (var date in dates) {
                var column = new DataColumn(date.Name, typeof(double));
                column.DefaultValue = 0;
                column.ExtendedProperties.Add("Start", date.Start);
                column.ExtendedProperties.Add("End", date.End);
                column.ExtendedProperties.Add("ExcelDisplayName", date.Name);
                model.Columns.Add(column);
            }

            return model;
        }

        private List<Model400405> GetCustom400405(string parent, string date, bool cache) {
            var key = string.Format(GlobalCacheKeys.Report_400405, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<Model400405>(key).ToList();

            var result = new List<Model400405>();
            if (string.IsNullOrWhiteSpace(parent))
                return result;

            if (string.IsNullOrWhiteSpace(date))
                return result;

            var curstart = DateTime.ParseExact(date, "yyyyMM", CultureInfo.CurrentCulture);
            var curend = curstart.AddMonths(1).AddSeconds(-1);
            var laststart = curstart.AddMonths(-1);
            var lastend = curstart.AddSeconds(-1);

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Device) {
                var device = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                if (device == null) return result;
                var signals = _signalService.GetVSignals(nodeKey.Value, EnmVSignalCategory.Category05);
                if (signals.Count == 0) return result;

                var index = 0;
                var cvalues = _cabinetService.GetLast(nodeKey.Value, EnmVSignalCategory.Category05, curstart, curend);
                var lvalues = _cabinetService.GetLast(nodeKey.Value, EnmVSignalCategory.Category05, laststart, lastend);
                foreach (var signal in signals) {
                    var cdetail = cvalues.Find(v => v.DeviceId.Equals(signal.DeviceId) && v.PointId.Equals(signal.PointId));
                    var ldetail = lvalues.Find(v => v.DeviceId.Equals(signal.DeviceId) && v.PointId.Equals(signal.PointId));

                    var model = new Model400405 {
                        index = ++index,
                        area = device.Current.AreaName,
                        station = device.Current.StationName,
                        room = device.Current.RoomName,
                        name = signal.Name,
                        last = ldetail != null ? Math.Round(ldetail.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        current = cdetail != null ? Math.Round(cdetail.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        currentA = cdetail != null ? Math.Round(cdetail.AValue, 2, MidpointRounding.AwayFromZero) : 0,
                        currentB = cdetail != null ? Math.Round(cdetail.BValue, 2, MidpointRounding.AwayFromZero) : 0
                    };

                    model.total = model.current - model.last;
                    result.Add(model);
                }
            } else {
                var signals = _signalService.GetVSignals(EnmVSignalCategory.Category05);
                if (signals.Count == 0) return result;
                var devkeys = new HashSet<string>(signals.Select(s => s.DeviceId));
                var devices = _workContext.Devices().FindAll(d => devkeys.Contains(d.Current.Id));
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
                }
                if (devices.Count == 0) return result;

                var _signals = from signal in signals
                               join dev in devices on signal.DeviceId equals dev.Current.Id
                               select new { Device = dev.Current, Signal = signal };

                var index = 0;
                var cvalues = _cabinetService.GetLast(EnmVSignalCategory.Category05, curstart, curend);
                var lvalues = _cabinetService.GetLast(EnmVSignalCategory.Category05, laststart, lastend);
                foreach (var _signal in _signals) {
                    var cdetail = cvalues.Find(v => v.DeviceId.Equals(_signal.Signal.DeviceId) && v.PointId.Equals(_signal.Signal.PointId));
                    var ldetail = lvalues.Find(v => v.DeviceId.Equals(_signal.Signal.DeviceId) && v.PointId.Equals(_signal.Signal.PointId));

                    var model = new Model400405 {
                        index = ++index,
                        area = _signal.Device.AreaName,
                        station = _signal.Device.StationName,
                        room = _signal.Device.RoomName,
                        name = _signal.Signal.Name,
                        last = ldetail != null ? Math.Round(ldetail.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        current = cdetail != null ? Math.Round(cdetail.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        currentA = cdetail != null ? Math.Round(cdetail.AValue, 2, MidpointRounding.AwayFromZero) : 0,
                        currentB = cdetail != null ? Math.Round(cdetail.BValue, 2, MidpointRounding.AwayFromZero) : 0
                    };

                    model.total = Math.Round(model.current - model.last, 2, MidpointRounding.AwayFromZero);
                    result.Add(model);
                }
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

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
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                foreach (var root in roots) {
                    var curstores = stores.FindAll(s => root.Keys.Contains(s.Current.AreaId));
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = root.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                }
                #endregion
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        #region area
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) {
                            if (current.HasChildren) {
                                foreach (var child in current.ChildRoot) {
                                    var curstores = stores.FindAll(s => child.Keys.Contains(s.Current.AreaId));
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = child.Current.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            } else if (current.Stations.Count > 0) {
                                foreach (var station in current.Stations) {
                                    var curstores = stores.FindAll(s => s.Current.StationId == station.Id);
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            }
                        }
                        #endregion
                    } else if (nodeType == EnmSSH.Station) {
                        #region station
                        var current = _workContext.Stations().Find(s => s.Current.Id == id);
                        if (current != null && current.Rooms.Count > 0) {
                            foreach (var room in current.Rooms) {
                                var curstores = stores.FindAll(m => m.Current.RoomId == room.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if (nodeType == EnmSSH.Room) {
                        #region room
                        var current = _workContext.Rooms().Find(r => r.Current.Id == id);
                        if (current != null && current.Devices.Count > 0) {
                            foreach (var device in current.Devices) {
                                var curstores = stores.FindAll(s => s.Current.DeviceId == device.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if (nodeType == EnmSSH.Device) {
                        #region device
                        var current = _workContext.Devices().Find(d => d.Current.Id == id);
                        if (current != null) {
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