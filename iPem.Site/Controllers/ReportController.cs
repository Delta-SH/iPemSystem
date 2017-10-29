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
        private readonly IBatCurveService _batCurveService;
        private readonly IStaticService _staticService;
        private readonly IHMeasureService _measureService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IEmployeeService _employeeService;
        private readonly IMAuthorizationService _mauthorizationService;
        private readonly IPointService _pointService;
        private readonly IProductorService _productorService;
        private readonly ISubCompanyService _subCompanyService;
        private readonly ISupplierService _supplierService;
        private readonly ICutService _cutService;
        private readonly ICardRecordService _cardRecordService;

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
            IBatCurveService batCurveService,
            IStaticService staticService,
            IHMeasureService measureService,
            IEnumMethodService enumMethodService,
            IEmployeeService employeeService,
            IMAuthorizationService mauthorizationService,
            IPointService pointService,
            IProductorService productorService,
            ISubCompanyService subCompanyService,
            ISupplierService supplierService,
            ICutService cutService,
            ICardRecordService cardRecordService) {
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
            this._batCurveService = batCurveService;
            this._staticService = staticService;
            this._measureService = measureService;
            this._enumMethodService = enumMethodService;
            this._employeeService = employeeService;
            this._mauthorizationService = mauthorizationService;
            this._pointService = pointService;
            this._productorService = productorService;
            this._subCompanyService = subCompanyService;
            this._supplierService = supplierService;
            this._cutService = cutService;
            this._cardRecordService = cardRecordService;
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400101(string parent, int[] types) {
            try {
                var models = this.GetBase400101(parent, types);
                using(var ms = _excelManager.Export<Model400101>(models, "区域统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400102(string parent, string[] types) {
            try {
                var models = this.GetBase400102(parent, types);
                using(var ms = _excelManager.Export<Model400102>(models, "站点统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400103(string parent, string[] types) {
            try {
                var models = this.GetBase400103(parent, types);
                using(var ms = _excelManager.Export<Model400103>(models, "机房统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400104(string parent, string[] types) {
            try {
                var models = this.GetBase400104(parent, types);
                using(var ms = _excelManager.Export<Model400104>(models, "设备统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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

                    for (int i = start; i < end; i++) {
                        var store = stores[i];
                        store.index = i + 1;
                        data.data.Add(store);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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

                using(var ms = _excelManager.Export<Model400202>(models, "历史告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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

                var models = new List<Model400202>();
                if (stores != null && stores.Count > 0) {
                    for (int i = 0; i < stores.Count; i++) {
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
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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

                using (var ms = _excelManager.Export<Model400202>(models, title ?? "告警详单", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model400203>(models, "历史告警分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                                point = alarms[i].PointName,
                                device = alarms[i].DeviceName,
                                room = alarms[i].RoomName,
                                station = alarms[i].StationName,
                                area = alarms[i].AreaName,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                id = alarms[i].Current.Id,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                                point = alarms[i].PointName,
                                device = alarms[i].DeviceName,
                                room = alarms[i].RoomName,
                                station = alarms[i].StationName,
                                area = alarms[i].AreaName,
                                confirmed = Common.GetConfirmDisplay(alarms[i].Current.Confirmed),
                                confirmer = alarms[i].Current.Confirmer,
                                confirmedtime = alarms[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(alarms[i].Current.ConfirmedTime.Value) : "",
                                reservation = alarms[i].Current.ReservationId,
                                reversalcount = alarms[i].Current.ReversalCount,
                                id = alarms[i].Current.Id,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                if(stores != null && stores.Count > 0) {
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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

                var stores = _cacheManager.Get<List<Model400204>>(key);
                if (stores != null && stores.Count > 0) {
                    var current = stores.Find(s => s.stationid == station);
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
                                    index = i + 1,
                                    nmalarmid = models[i].Current.NMAlarmId,
                                    level = Common.GetAlarmDisplay(models[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                                    interval = CommonHelper.IntervalConverter(models[i].Current.StartTime, models[i].Current.EndTime),
                                    comment = models[i].Current.AlarmDesc,
                                    startvalue = models[i].Current.StartValue.ToString(),
                                    endvalue = models[i].Current.EndValue.ToString(),
                                    point = models[i].PointName,
                                    device = models[i].DeviceName,
                                    room = models[i].RoomName,
                                    station = models[i].StationName,
                                    area = models[i].AreaName,
                                    confirmed = Common.GetConfirmDisplay(models[i].Current.Confirmed),
                                    confirmer = models[i].Current.Confirmer,
                                    confirmedtime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : "",
                                    reservation = models[i].Current.ReservationId,
                                    reversalcount = models[i].Current.ReversalCount,
                                    id = models[i].Current.Id,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                var stores = _cacheManager.Get<List<Model400204>>(key);
                if (stores != null && stores.Count > 0) {
                    var current = stores.Find(s => s.stationid == station);
                    if (current != null && current.alarms != null) {
                        var models = type.Equals("total") ? current.alarms : current.alarms.FindAll(a => a.DeviceTypeId.Equals(type));
                        if (models.Count > 0) {
                            for (int i = 0; i < models.Count; i++) {
                                result.Add(new HisAlmModel {
                                    index = i + 1,
                                    nmalarmid = models[i].Current.NMAlarmId,
                                    level = Common.GetAlarmDisplay(models[i].Current.AlarmLevel),
                                    starttime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                                    endtime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                                    interval = CommonHelper.IntervalConverter(models[i].Current.StartTime, models[i].Current.EndTime),
                                    comment = models[i].Current.AlarmDesc,
                                    startvalue = models[i].Current.StartValue.ToString(),
                                    endvalue = models[i].Current.EndValue.ToString(),
                                    point = models[i].PointName,
                                    device = models[i].DeviceName,
                                    room = models[i].RoomName,
                                    station = models[i].StationName,
                                    area = models[i].AreaName,
                                    confirmed = Common.GetConfirmDisplay(models[i].Current.Confirmed),
                                    confirmer = models[i].Current.Confirmer,
                                    confirmedtime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : "",
                                    reservation = models[i].Current.ReservationId,
                                    reversalcount = models[i].Current.ReversalCount,
                                    id = models[i].Current.Id,
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model400205>(models, "工程项目信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model400206>(models, "工程预约信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model400207>(models, "市电停电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                var key = string.Format(GlobalCacheKeys.Report_400207, _workContext.Identifier());
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400207(string station) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400207, _workContext.Identifier());
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

                using (var ms = _excelManager.Export<ShiDianModel>(result, "市电停电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model400208>(models, "油机发电统计信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                var key = string.Format(GlobalCacheKeys.Report_400208, _workContext.Identifier());
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistoryDetail400208(string station) {
            try {
                var key = string.Format(GlobalCacheKeys.Report_400208, _workContext.Identifier());
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

                using (var ms = _excelManager.Export<ShiDianModel>(result, "油机发电详情", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                        var curDevice = _workContext.Devices().Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoints = curDevice.Points.FindAll(p => points.Contains(p.Id));
                            var curValues = _measureService.GetMeasuresInDevice(curDevice.Current.Id, startDate, endDate);
                            for (var i = 0; i < curPoints.Count; i++) {
                                var values = curValues.FindAll(v => v.PointId == curPoints[i].Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                        var curDevice = _workContext.Devices().Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoint = curDevice.Points.Find(p => p.Id == point);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                        var curDevice = _workContext.Devices().Find(d => d.Current.Id == id);
                        if(curDevice != null) {
                            var curPoints = curDevice.Points.FindAll(p => points.Contains(p.Id));
                            var curValues = _batCurveService.GetValues(curDevice.Current.Id, starttime, endtime);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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

                using(var ms = _excelManager.Export<Model400401>(models, "超频告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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

                using(var ms = _excelManager.Export<Model400402>(models, "超短告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            starttime = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            endtime = CommonHelper.DateTimeConverter(stores[i].Current.EndTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime, stores[i].Current.EndTime),
                            comment = stores[i].Current.AlarmDesc,
                            startvalue = stores[i].Current.StartValue.ToString(),
                            endvalue = stores[i].Current.EndValue.ToString(),
                            point = stores[i].PointName,
                            device = stores[i].DeviceName,
                            room = stores[i].RoomName,
                            station = stores[i].StationName,
                            area = stores[i].AreaName,
                            confirmed = Common.GetConfirmDisplay(stores[i].Current.Confirmed),
                            confirmer = stores[i].Current.Confirmer,
                            confirmedtime = stores[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].Current.ConfirmedTime.Value) : "",
                            reservation = stores[i].Current.ReservationId,
                            reversalcount = stores[i].Current.ReversalCount,
                            id = stores[i].Current.Id,
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

                using(var ms = _excelManager.Export<Model400403>(models, "超长告警信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model400101> GetBase400101(string parent, int[] types) {
            var index = 0;
            var result = new List<Model400101>();

            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var areas = _workContext.Areas();
                if(types != null && types.Length > 0) 
                    areas = areas.FindAll(a => types.Contains(a.Current.Type.Key));

                var ordered = areas.OrderBy(a => a.Current.Type.Key);
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
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null && current.HasChildren) {
                    var children = current.Children;
                    if(types != null && types.Length > 0)
                        children = children.FindAll(a => types.Contains(a.Current.Type.Key));

                    var ordered = children.OrderBy(a => a.Current.Type.Key);
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
                stations = _workContext.Stations();
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
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

            var rooms = new List<S_Room>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                rooms.AddRange(_workContext.Rooms().Select(r => r.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if(current != null) rooms.AddRange(_workContext.Rooms().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(r => r.Current));
                    } else if(nodeType == EnmSSH.Station) {
                        var current = _workContext.Stations().Find(a => a.Current.Id == id);
                        if(current != null) rooms = current.Rooms;
                    }
                }
            }

            if(types != null && types.Length > 0)
                rooms = rooms.FindAll(s => types.Contains(s.Type.Id));

            var parms = _enumMethodService.GetEnumsByType(EnmMethodType.Room, "产权");
            var stores = from room in rooms
                         join parm in parms on room.PropertyId equals parm.Id into lt
                         from def in lt.DefaultIfEmpty()
                         orderby room.Type.Id, room.Name
                         select new {
                             Room = room,
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

            var devices = new List<D_Device>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                devices.AddRange(_workContext.Devices().Select(d=>d.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) devices.AddRange(_workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(d => d.Current));
                    } else if(nodeType == EnmSSH.Station) {
                        devices.AddRange(_workContext.Devices().FindAll(d => d.Current.StationId == id).Select(d => d.Current));
                    } else if(nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms().Find(a => a.Current.Id == id);
                        if(current != null) devices = current.Devices;
                    }
                }
            }

            if(types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Type.Id));

            var productors = _productorService.GetProductors();
            var brands = _brandService.GetBrands();
            var suppliers = _supplierService.GetSuppliers();
            var subCompanys = _subCompanyService.GetCompanies();
            var status = _enumMethodService.GetEnumsByType(EnmMethodType.Device, "使用状态");
            var stores = from device in devices
                        join productor in productors on device.ProdId equals productor.Id into lt1
                        from def1 in lt1.DefaultIfEmpty()
                        join brand in brands on device.BrandId equals brand.Id into lt2
                        from def2 in lt2.DefaultIfEmpty()
                        join supplier in suppliers on device.SuppId equals supplier.Id into lt3
                        from def3 in lt3.DefaultIfEmpty()
                        join company in subCompanys on device.SubCompId equals company.Id into lt4
                        from def4 in lt4.DefaultIfEmpty()
                        join sts in status on device.StatusId equals sts.Id into lt5
                        from def5 in lt5.DefaultIfEmpty()
                        orderby device.Type.Id
                        select new {
                            Device = device,
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
            if (_workContext.Role().Id != U_Role.SuperId) {
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
            if (_workContext.Role().Id != U_Role.SuperId) {
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
                            empName = string.IsNullOrWhiteSpace(emp.EmpName) ? "" : string.Format("{0}({1})",emp.EmpName,emp.EmpCode??""),
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
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400201>>(key);

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
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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

            var stations = _workContext.Stations();
            if(statypes != null && statypes.Length > 0)
                stations = stations.FindAll(d => statypes.Contains(d.Current.Type.Id));

            var rooms = _workContext.Rooms();
            if(roomtypes != null && roomtypes.Length > 0)
                rooms = rooms.FindAll(d => roomtypes.Contains(d.Current.Type.Id));

            var devices = _workContext.Devices();
            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.SubType.Id));

            var _points = _workContext.Points();
            if (points != null && points.Length > 0)
                _points = _points.FindAll(p => points.Contains(p.Id));

            if(!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) _points = _points.FindAll(p => CommonHelper.ConditionContain(p.Name, names));
            }

            var stores = (from val in values
                          join pt in _points on val.PointId equals pt.Id
                          join device in devices on val.DeviceId equals device.Current.Id
                          join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                          select new Model400201 {
                              area = area.ToString(),
                              station = device.Current.StationName,
                              room = device.Current.RoomName,
                              device = device.Current.Name,
                              point = pt.Name,
                              type = Common.GetPointTypeDisplay(pt.Type),
                              value = val.Value,
                              unit = Common.GetUnitDisplay(pt.Type, val.Value.ToString(), pt.UnitState),
                              status = Common.GetPointStatusDisplay(EnmState.Normal),
                              time = CommonHelper.DateTimeConverter(val.UpdateTime)
                          }).ToList();

            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, stores, GlobalCacheInterval.Site_Interval);
            }

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetHistory400202(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int[] types, string confirmers, string keywords, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400202, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<AlmStore<A_HAlarm>>>(key);

            var alarms = new List<A_HAlarm>();
            if (types != null && types.Length > 0) {
                alarms = _hisAlarmService.GetNonAlarms(startDate, endDate).FindAll(a => (types.Contains(1) && a.RoomId == "-1") || (types.Contains(2) && a.Masked));
                if (!string.IsNullOrWhiteSpace(parent) && parent == "root") {
                    var keys = Common.SplitKeys(parent);
                    if (keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if (nodeType == EnmSSH.Area) {
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
                            if (current != null) alarms = alarms.FindAll(a => current.Keys.Contains(a.AreaId));
                        } else if (nodeType == EnmSSH.Station) {
                            alarms = alarms.FindAll(a => a.StationId == id);
                        } else if (nodeType == EnmSSH.Room) {
                            alarms = alarms.FindAll(a => a.RoomId == id);
                        } else if (nodeType == EnmSSH.Device) {
                            alarms = alarms.FindAll(a => a.DeviceId == id);
                        }
                    }
                }
            } else {
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
            }

            var sysalarms = alarms.FindAll(a => a.RoomId == "-1");
            var stores = _workContext.AlarmsToStore(alarms);
            if (sysalarms.Count > 0) {
                stores.AddRange(
                    from alarm in sysalarms
                    join point in _workContext.AL() on alarm.PointId equals point.Id
                    join gp in _workContext.Groups() on alarm.DeviceId equals gp.Id
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        DeviceName = gp.Name,
                        DeviceTypeId = SSHSystem.SC.Type.Id,
                        SubDeviceTypeId = SSHSystem.SC.SubType.Id,
                        SubLogicTypeId = SSHSystem.SC.SubLogicType.Id,
                        RoomName = SSHSystem.Room.Name,
                        RoomTypeId = SSHSystem.Room.Type.Id,
                        StationName = SSHSystem.Station.Name,
                        StationTypeId = SSHSystem.Station.Type.Id,
                        AreaName = SSHSystem.Area.Name
                    });
            }

            if(staTypes != null && staTypes.Length > 0)
                stores = stores.FindAll(d => staTypes.Contains(d.StationTypeId));

            if(roomTypes != null && roomTypes.Length > 0)
                stores = stores.FindAll(d => roomTypes.Contains(d.RoomTypeId));

            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                stores = stores.FindAll(d => subDeviceTypes.Contains(d.SubDeviceTypeId));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                stores = stores.FindAll(d => subLogicTypes.Contains(d.SubLogicTypeId));

            if (points != null && points.Length > 0)
                stores = stores.FindAll(p => points.Contains(p.Current.PointId));

            if(levels != null && levels.Length > 0)
                stores = stores.FindAll(a => levels.Contains((int)a.Current.AlarmLevel));

            if (!string.IsNullOrWhiteSpace(confirmers)) {
                var names = Common.SplitCondition(confirmers);
                if (names.Length > 0) stores = stores.FindAll(p => !string.IsNullOrWhiteSpace(p.Current.Confirmer) && CommonHelper.ConditionContain(p.Current.Confirmer, names));
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var names = Common.SplitCondition(keywords);
                if (names.Length > 0) stores = stores.FindAll(p => CommonHelper.ConditionContain(p.PointName, names));
            }

            if(confirm == 1) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            if(confirm == 0) stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);
            if(project == 1) stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            if(project == 0) stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));

            stores = stores.OrderByDescending(s => s.Current.StartTime).ToList();
            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, stores, GlobalCacheInterval.Site_Interval);
            }

            return stores;
        }

        private List<Model400203> GetHistory400203(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400203, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key))  return _cacheManager.Get<List<Model400203>>(key);

            var alarms = new List<A_HAlarm>();
            var stations = _workContext.Stations();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0)
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));

            var rooms = _workContext.Rooms();
            if (roomTypes != null && roomTypes.Length > 0)
                rooms = rooms.FindAll(r => roomTypes.Contains(r.Current.Type.Id));

            var devices = _workContext.Devices();
            if (subDeviceTypes != null && subDeviceTypes.Length > 0)
                devices = devices.FindAll(d => subDeviceTypes.Contains(d.Current.SubType.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                devices = devices.FindAll(d => subLogicTypes.Contains(d.Current.SubLogicType.Id));

            if (points != null && points.Length > 0)
                alarms = alarms.FindAll(a => points.Contains(a.PointId));

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            if (confirm == 1) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) alarms = alarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ReservationId));
            if (project == 0) alarms = alarms.FindAll(a => string.IsNullOrWhiteSpace(a.ReservationId));

            var stores = (from alarm in alarms
                          join point in _workContext.AL() on alarm.PointId equals point.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on room.Current.StationId equals station.Current.Id
                          join area in _workContext.Areas() on station.Current.AreaId equals area.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<A_HAlarm> {
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
                              AreaName = area.ToString()
                          }).ToList();

            var result = new List<Model400203>();
            var index = 0;
            foreach (var station in stations) {
                var area = _workContext.Areas().Find(a=>a.Current.Id == station.Current.AreaId);
                var _alarms = stores.FindAll(s => s.Current.StationId == station.Current.Id);
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

            if (stores.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<Model400204> GetHistory400204(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] devTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.Report_400204, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400204>>(key);

            var alarms = new List<A_HAlarm>();
            var stations = _workContext.Stations();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => current.Keys.Contains(a.AreaId));
                    stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                }
            }

            if (staTypes != null && staTypes.Length > 0)
                stations = stations.FindAll(s => staTypes.Contains(s.Current.Type.Id));

            var rooms = _workContext.Rooms();
            if (roomTypes != null && roomTypes.Length > 0)
                rooms = rooms.FindAll(r => roomTypes.Contains(r.Current.Type.Id));

            var devices = _workContext.Devices();
            if (devTypes != null && devTypes.Length > 0)
                devices = devices.FindAll(d => devTypes.Contains(d.Current.Type.Id));

            if (subLogicTypes != null && subLogicTypes.Length > 0)
                devices = devices.FindAll(d => subLogicTypes.Contains(d.Current.SubLogicType.Id));

            if (points != null && points.Length > 0)
                alarms = alarms.FindAll(a => points.Contains(a.PointId));

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            if (confirm == 1) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Confirmed);
            if (confirm == 0) alarms = alarms.FindAll(a => a.Confirmed == EnmConfirm.Unconfirmed);
            if (project == 1) alarms = alarms.FindAll(a => !string.IsNullOrWhiteSpace(a.ReservationId));
            if (project == 0) alarms = alarms.FindAll(a => string.IsNullOrWhiteSpace(a.ReservationId));

            var stores = (from alarm in alarms
                          join point in _workContext.AL() on alarm.PointId equals point.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          join room in rooms on device.Current.RoomId equals room.Current.Id
                          join station in stations on room.Current.StationId equals station.Current.Id
                          join area in _workContext.Areas() on station.Current.AreaId equals area.Current.Id
                          orderby alarm.StartTime descending
                          select new AlmStore<A_HAlarm> {
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
                              AreaName = area.ToString()
                          }).ToList();

            var index = 0;
            var models = new List<Model400204>();
            foreach (var station in stations) {
                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
                var _alarms = stores.FindAll(s => s.Current.StationId == station.Current.Id);

                models.Add(new Model400204 {
                    index = ++index,
                    area = area != null ? area.ToString() : "",
                    stationid = station.Current.Id,
                    station = station.Current.Name,
                    alarms = _alarms
                });
            }

            if (stores.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.Set(key, models, GlobalCacheInterval.Site_Interval);
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
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
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

        private List<Model400207> GetHistory400207(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400207, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400207>>(key);

            var models = new List<Model400207>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && parent == "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var cutteds = _cutService.GetCuteds(startDate, endDate, EnmCutType.Cut);
            foreach (var station in stations) {
                var details = cutteds.FindAll(a => a.StationId == station.Current.Id);
                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);

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

            if (cutteds.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.Set(key, models, GlobalCacheInterval.Site_Interval);
            }

            return models;
        }

        private List<Model400208> GetHistory400208(string parent, string[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_400208, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<Model400208>>(key);

            var models = new List<Model400208>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            if (!string.IsNullOrWhiteSpace(parent) && parent == "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var cutteds = _cutService.GetCuteds(startDate, endDate, EnmCutType.Power);
            foreach (var station in stations) {
                var details = cutteds.FindAll(a => a.StationId == station.Current.Id);
                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);

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

            if (cutteds.Count <= GlobalCacheLimit.ReSet_Limit) {
                _cacheManager.Set(key, models, GlobalCacheInterval.Site_Interval);
            }

            return models;
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
                _cacheManager.Set(key, models, GlobalCacheInterval.Site_Interval);
            }

            return models;
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(DateTime start, DateTime end) {
            var entities = _reservationService.GetPagedReservationsInSpan(start, end);
            return this.GetReservationsInDevices(entities);
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Project> projects) {
            var matchs = projects.Select(p => p.Id);
            var reservations = _reservationService.GetPagedReservations().Where(a => matchs.Contains(a.ProjectId));
            return this.GetReservationsInDevices(reservations);
        }

        private List<Kv<M_Reservation, HashSet<string>>> GetReservationsInDevices(IEnumerable<M_Reservation> entities) {
            var appSets = new List<Kv<M_Reservation, HashSet<string>>>();
            foreach(var entity in entities) {
                var appSet = new Kv<M_Reservation, HashSet<string>>() { Key = entity, Value = new HashSet<string>() };
                var nodes = _nodesInReservationService.GetNodesInReservationsInReservation(entity.Id);
                foreach(var node in nodes) {
                    if(node.NodeType == EnmSSH.Device) {
                        appSet.Value.Add(node.NodeId);
                    }

                    if(node.NodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms().Find(r => r.Current.Id == node.NodeId);
                        if(current != null) {
                            foreach(var device in current.Devices) {
                                appSet.Value.Add(device.Id);
                            }
                        }
                    }

                    if(node.NodeType == EnmSSH.Station) {
                        var devices = _workContext.Devices().FindAll(d => d.Current.StationId == node.NodeId);
                        foreach(var device in devices) {
                            appSet.Value.Add(device.Current.Id);
                        }
                    }

                    if(node.NodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == node.NodeId);
                        if(current != null) {
                            var devices = _workContext.Devices().FindAll(d => current.Keys.Contains(d.Current.AreaId));
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

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400401(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if(rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_400401, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

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

            var result = new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400402(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_400402, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

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

            var result = new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>> GetCustom400403(string parent, DateTime startDate, DateTime endDate, string[] staTypes, string[] roomTypes, string[] subDeviceTypes, string[] subLogicTypes, string[] points, int[] levels, int confirm, int project, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return null;

            var key = string.Format(GlobalCacheKeys.Report_400402, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>>(key);

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

            var result = new Kv<List<AlmStore<A_HAlarm>>, List<ChartsModel>>(stores, charts);
            if (stores.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.Set(key, result, GlobalCacheInterval.Site_Interval);
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
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var roots = _workContext.Areas().FindAll(a => !a.HasParents);
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
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                                    var curstores = stores.FindAll(s => s.Current.StationId == station.Id);
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = station.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                                }
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Station) {
                        #region station
                        var current = _workContext.Stations().Find(s => s.Current.Id == id);
                        if(current != null && current.Rooms.Count > 0) {
                            foreach(var room in current.Rooms) {
                                var curstores = stores.FindAll(m => m.Current.RoomId == room.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = room.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Room) {
                        #region room
                        var current = _workContext.Rooms().Find(r => r.Current.Id == id);
                        if(current != null && current.Devices.Count > 0) {
                            foreach(var device in current.Devices) {
                                var curstores = stores.FindAll(s => s.Current.DeviceId == device.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level1, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1), comment = Common.GetAlarmDisplay(EnmAlarm.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level2, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2), comment = Common.GetAlarmDisplay(EnmAlarm.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level3, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3), comment = Common.GetAlarmDisplay(EnmAlarm.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarm.Level4, name = device.Name, value = curstores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4), comment = Common.GetAlarmDisplay(EnmAlarm.Level4) });
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmSSH.Device) {
                        #region device
                        var current = _workContext.Devices().Find(d => d.Current.Id == id);
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