using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class KPIController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IDictionaryService _dictionaryService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IStationService _stationService;
        private readonly IStationTypeService _stationTypeService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IDeviceService _deviceService;
        private readonly IHAlarmService _hisAlarmService;
        private readonly IBatService _batService;
        private readonly IBatTimeService _batTimeService;
        private readonly IBatCurveService _batCurveService;
        private readonly IElecService _elecService;
        private readonly ILoadService _loadService;
        private readonly IOfflineService _offlineService;

        #endregion

        #region Ctor

        public KPIController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IDictionaryService dictionaryService,
            IEnumMethodService enumMethodService,
            IStationService stationService,
            IStationTypeService stationTypeService,
            IDeviceTypeService deviceTypeService,
            IDeviceService deviceService,
            IHAlarmService hisAlarmService,
            IBatService batService,
            IBatTimeService batTimeService,
            IBatCurveService batCurveService,
            IElecService elecService,
            ILoadService loadService,
            IOfflineService offlineService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._dictionaryService = dictionaryService;
            this._enumMethodService = enumMethodService;
            this._stationTypeService = stationTypeService;
            this._stationService = stationService;
            this._deviceTypeService = deviceTypeService;
            this._deviceService = deviceService;
            this._hisAlarmService = hisAlarmService;
            this._batService = batService;
            this._batTimeService = batTimeService;
            this._batCurveService = batCurveService;
            this._loadService = loadService;
            this._elecService = elecService;
            this._offlineService = offlineService;
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
        public ActionResult Performance(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
                return View(string.Format("performance{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if (id.HasValue && _workContext.Authorizations().Menus.Contains(id.Value))
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
        public ActionResult Download500101(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500101(parent, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500101>(models, "直流系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
        public ActionResult Download500102(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500102(parent, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500102>(models, "交流不间断系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500103(int start, int limit, string parent, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500103>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500103>()
            };

            try {
                var models = this.Get500103(parent, types, startDate, endDate);
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
        public ActionResult Download500103(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500103(parent, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500103>(models, "温控系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500104(int start, int limit, string parent, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500104>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500104>()
            };

            try {
                var models = this.Get500104(parent, types, startDate, endDate);
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
        public ActionResult Download500104(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500104(parent, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500104>(models, "监控可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500105(int start, int limit, string parent, string[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500105>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500105>()
            };

            try {
                var models = this.Get500105(parent, types, startDate, endDate);
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
        public ActionResult Download500105(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500105(parent, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500105>(models, "市电可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500201(int start, int limit, string parent, string[] types, int size) {
            var data = new AjaxDataModel<List<Model500201>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500201>()
            };

            try {
                var models = this.Get500201(parent, types, size);
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
        public ActionResult Download500201(string parent, string[] types, int size) {
            try {
                var models = this.Get500201(parent, types, size);
                using (var ms = _excelManager.Export<Model500201>(models, "监控覆盖率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500202(int start, int limit, string parent, string[] types, int size) {
            var data = new AjaxDataModel<List<Model500202>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500202>()
            };

            try {
                var models = this.Get500202(parent, types, size);
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
        public ActionResult Download500202(string parent, string[] types, int size) {
            try {
                var models = this.Get500202(parent, types, size);
                using (var ms = _excelManager.Export<Model500202>(models, "关键监控测点接入率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500203(int start, int limit, string parent, string[] types, int size) {
            var data = new AjaxDataModel<List<Model500203>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500203>()
            };

            try {
                var models = this.Get500203(parent, types, size);
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
        public ActionResult Download500203(string parent, string[] types, int size) {
            try {
                var models = this.Get500203(parent, types, size);
                using (var ms = _excelManager.Export<Model500203>(models, "站点标识率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500204(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500204>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500204>()
            };

            try {
                var models = this.Get500204(parent, types, size, startDate, endDate);
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
        public ActionResult Download500204(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500204(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500204>(models, "开关电源带载合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500205(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500205>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500205>()
            };

            try {
                var models = this.Get500205(parent, types, size, startDate, endDate);
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
        public ActionResult Download500205(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500205(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500205>(models, "蓄电池后备时长合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500206(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500206>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500206>()
            };

            try {
                var models = this.Get500206(parent, types, size, startDate, endDate);
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
        public ActionResult Download500206(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500206(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500206>(models, "温控容量合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500207(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500207>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500207>()
            };

            try {
                var models = this.Get500207(parent, types, size, startDate, endDate);
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
        public ActionResult Download500207(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500207(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500207>(models, "直流系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500208(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500208>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500208>()
            };

            try {
                var models = this.Get500208(parent, types, size, startDate, endDate);
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
        public ActionResult Download500208(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500208(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500208>(models, "监控故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500209(int start, int limit, string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<Model500209>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500209>()
            };

            try {
                var models = this.Get500209(parent, types, size, startDate, endDate);
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
        public ActionResult Download500209(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500209(parent, types, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500209>(models, "蓄电池核对性放电及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    foreach (var model in models) {
                        var names = model.name.Split(new char[] { ',' });
                        data.chart.Add(new Chart500301 {
                            index = model.index,
                            name = names.Length > 0 ? names[names.Length - 1] : "",
                            kt = model.kt,
                            zm = model.zm,
                            bg = model.bg,
                            it = model.it,
                            dy = model.dy,
                            ups = model.ups,
                            qt = model.qt
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
        public ActionResult Download500301(string parent, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500301(parent, size, startDate, endDate);
                using (var ms = _excelManager.Export<Model500301>(models, string.Format("能耗分类信息({0})", CommonHelper.PeriodConverter(startDate, endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500302(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "name", type = "string", column = "站点名称", width = 200 },
                    new GridColumn { name = "type", type = "string", column = "能耗分类", width = 100 }
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

        [AjaxAuthorize]
        public JsonResult Request500302(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<JObject>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>(),
                chart = new List<ChartsModel>()
            };

            try {
                var models = this.Get500302(parent, types, energies, period, startDate, endDate, cache, data.chart);
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

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500302(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500302(parent, types, energies, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("站点能耗统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500303(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "name", type = "string", column = "机房名称", width = 200 },
                    new GridColumn { name = "type", type = "string", column = "能耗分类", width = 100 }
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

        [AjaxAuthorize]
        public JsonResult Request500303(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxChartModel<List<JObject>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>(),
                chart = new List<ChartsModel>()
            };

            try {
                var models = this.Get500303(parent, types, energies, period, startDate, endDate, cache, data.chart);
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
        public ActionResult Download500303(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500303(parent, types, energies, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("机房能耗统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500304(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "name", type = "string", column = "站点名称", width = 200 }
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

        [AjaxAuthorize]
        public JsonResult Request500304(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.Get500304(parent, types, period, startDate, endDate, cache);
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
        public ActionResult Download500304(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500304(parent, types, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("站点PUE统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500305(EnmPDH period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<GridColumn>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<GridColumn> {
                    new GridColumn { name = "index", type = "int", column = "序号", width = 60 },
                    new GridColumn { name = "name", type = "string", column = "机房名称", width = 200 }
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

        [AjaxAuthorize]
        public JsonResult Request500305(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.Get500305(parent, types, period, startDate, endDate, cache);
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
        public ActionResult Download500305(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500305(parent, types, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("机房PUE统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500306(EnmPDH period, DateTime startDate, DateTime endDate) {
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

        [AjaxAuthorize]
        public JsonResult Request500306(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.Get500306(parent, period, startDate, endDate, cache);
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
        public ActionResult Download500306(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500306(parent, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("变压器能耗统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500307(EnmPDH period, DateTime startDate, DateTime endDate) {
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

        [AjaxAuthorize]
        public JsonResult Request500307(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<JObject>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<JObject>()
            };

            try {
                var models = this.Get500307(parent, period, startDate, endDate, cache);
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
        public ActionResult Download500307(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.Get500307(parent, period, startDate, endDate, cache);
                using (var ms = _excelManager.Export(models, string.Format("变压器损耗统计({0})",CommonHelper.PeriodConverter(startDate,endDate)), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
        public ActionResult Download500401(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500401(parent, size, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500401>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
        public ActionResult Download500402(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500402(parent, size, types, startDate, endDate);
                using (var ms = _excelManager.Export<Model500402>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
        public ActionResult Download500403(string parent, int size, int[] levels, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500403(parent, size, levels, startDate, endDate);
                using (var ms = _excelManager.Export<Model500403>(models, "告警确认及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model500101> Get500101(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500101>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.hxzlxtkydXinHao == null
                || rtValues.hxzlxtkydXinHao.Length == 0
                || rtValues.hxzlxtkydLeiXing == null
                || rtValues.hxzlxtkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.hxzlxtkydXinHao;
            var devTypes = rtValues.hxzlxtkydLeiXing;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if (parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach (var station in stations) {
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                var alarms = _hisAlarmService.GetAlarmsInStation(station.Current.Id, startDate, endDate).FindAll(a => points.Contains(a.PointId));

                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500101 {
                    index = ++index,
                    area = station.Current.AreaName,
                    station = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = devices.Count,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500102> Get500102(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500102>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.hxjlxtkydXinHao == null
                || rtValues.hxjlxtkydXinHao.Length == 0
                || rtValues.hxjlxtkydPangLuXinHao == null
                || rtValues.hxjlxtkydPangLuXinHao.Length == 0
                || rtValues.hxjlxtkydLeiXing == null
                || rtValues.hxjlxtkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var almPoints = rtValues.hxjlxtkydXinHao;
            var runPoints = rtValues.hxjlxtkydPangLuXinHao;
            var devTypes = rtValues.hxjlxtkydLeiXing;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if (parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate);
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach (var station in stations) {
                var alarms = allAlarms.FindAll(a => a.StationId == station.Current.Id);
                var almAlarms = alarms.FindAll(a => almPoints.Contains(a.PointId));
                var runAlarms = alarms.FindAll(a => runPoints.Contains(a.PointId));
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);

                var almTime = almAlarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var runTime = runAlarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500102 {
                    index = ++index,
                    area = station.Current.AreaName,
                    station = station.Current.Name,
                    type = station.Current.Type.Name,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    runTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(runTime)),
                    count = devices.Count,
                    time = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - ((almTime + runTime) / (devices.Count * cntTime)) : 1)
                });
            }

            return result;
        }

        private List<Model500103> Get500103(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500103>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.hxwkxtkydGaoWenXinHao == null
                || rtValues.hxwkxtkydGaoWenXinHao.Length == 0
                || rtValues.hxwkxtkydWenDuXinHao == null
                || rtValues.hxwkxtkydWenDuXinHao.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var gwPoints = rtValues.hxwkxtkydGaoWenXinHao;
            var wdPoints = rtValues.hxwkxtkydWenDuXinHao;

            var stations = _workContext.GetStationsWithPoints(wdPoints);
            if (types != null && types.Length > 0) stations = stations.FindAll(d => types.Contains(d.Type.Id));

            if (!"root".Equals(parent)) {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.AreaId));
            }

            var index = 0;
            var gwAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => gwPoints.Contains(a.PointId));
            foreach (var station in stations) {
                var alarms = gwAlarms.FindAll(a => a.StationId == station.Id);
                var total = station.CityElectNumber; //在GetStationsWithPoints方法中，使用CityElectNumber字段存储指定信号的数量

                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500103 {
                    index = ++index,
                    area = station.AreaName,
                    station = station.Name,
                    type = station.Type.Name,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    total = total,
                    time = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", total > 0 && cntTime > 0 ? 1 - almTime / (total * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500104> Get500104(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500104>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.hxjkkydXinHao == null
                || rtValues.hxjkkydXinHao.Length == 0
                || rtValues.hxjkkydLeiXing == null
                || rtValues.hxjkkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.hxjkkydXinHao;
            var devTypes = rtValues.hxjkkydLeiXing;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if (parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => points.Contains(a.PointId));
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach (var station in stations) {
                var alarms = allAlarms.FindAll(a => a.StationId == station.Current.Id);
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);

                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500104 {
                    index = ++index,
                    area = station.Current.AreaName,
                    station = station.Current.Name,
                    type = station.Current.Type.Name,
                    devCount = devices.Count,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500105> Get500105(string parent, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500105>();
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if (parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var offlines = _offlineService.GetHistory(EnmFormula.TD, startDate, endDate);
            foreach (var station in stations) {
                var offs = offlines.FindAll(a => a.Id == station.Current.Id);
                var offTime = offs.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;

                result.Add(new Model500105 {
                    index = ++index,
                    area = station.Current.AreaName,
                    station = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = station.Current.CityElectNumber,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(offTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", cntTime > 0 ? 1 - offTime / cntTime : 1)
                });
            }

            return result;
        }

        private List<Model500201> Get500201(string parent, string[] types, int size) {
            var result = new List<Model500201>();
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var iStations = _workContext.iStations(DateTime.Now);
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
                iStations = iStations.FindAll(s => staTypeNames.Contains(s.Current.TypeName));
            }

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach (var child in area.Children) {
                        keys.Add(child.Current.Name);
                    }

                    var curStations = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var lastStations = iStations.FindAll(s => keys.Contains(s.iArea.Name));
                    result.Add(new Model500201 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        current = curStations.Count,
                        last = lastStations.Count,
                        rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach (var child in area.Children) {
                                keys.Add(child.Current.Name);
                            }

                            var curStations = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var lastStations = iStations.FindAll(s => keys.Contains(s.iArea.Name));
                            result.Add(new Model500201 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                current = curStations.Count,
                                last = lastStations.Count,
                                rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var curStations = stations.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var lastStations = iStations.FindAll(s => s.iArea.Name == current.Current.Name);
                        result.Add(new Model500201 {
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            current = curStations.Count,
                            last = lastStations.Count,
                            rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500202> Get500202(string parent, string[] types, int size) {
            var result = new List<Model500202>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.qtgjjkcdjrlXinHao == null
                || rtValues.qtgjjkcdjrlXinHao.Length == 0
                || rtValues.qtgjjkcdjrlLeiXing == null
                || rtValues.qtgjjkcdjrlLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent)) return result;

            var points = rtValues.qtgjjkcdjrlXinHao;
            var devTypeIds = rtValues.qtgjjkcdjrlLeiXing;
            var devTypeNames = _deviceTypeService.GetSubDeviceTypes().FindAll(t => devTypeIds.Contains(t.Id)).Select(t => t.Name).ToList();

            var deviceKeys = _deviceService.GetDeviceKeysWithPoints(points);
            var devices = _workContext.Devices().FindAll(d => devTypeIds.Contains(d.Current.SubType.Id) && deviceKeys.Contains(d.Current.Id));
            var iDevices = _workContext.iDevices(DateTime.Now).FindAll(i => devTypeNames.Contains(i.Current.TypeName));
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                devices = devices.FindAll(s => types.Contains(s.Current.StaTypeId));
                iDevices = iDevices.FindAll(i => staTypeNames.Contains(i.iStation.TypeName));
            }

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach (var child in area.Children) {
                        keys.Add(child.Current.Name);
                    }

                    var curDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                    var lastDevices = iDevices.FindAll(d => keys.Contains(d.iArea.Name)).ToList();

                    result.Add(new Model500202 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        current = curDevices.Count,
                        last = lastDevices.Count,
                        rate = string.Format("{0:P2}", lastDevices.Count > 0 ? (double)curDevices.Count / (double)lastDevices.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach (var child in area.Children) {
                                keys.Add(child.Current.Name);
                            }

                            var curDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                            var lastDevices = iDevices.FindAll(d => keys.Contains(d.iArea.Name)).ToList();

                            result.Add(new Model500202 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                current = curDevices.Count,
                                last = lastDevices.Count,
                                rate = string.Format("{0:P2}", lastDevices.Count > 0 ? (double)curDevices.Count / (double)lastDevices.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var curDevices = devices.FindAll(d => d.Current.AreaId == current.Current.Id);
                        var lastDevices = iDevices.FindAll(d => d.iArea.Name == current.Current.Name);

                        result.Add(new Model500202 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            current = curDevices.Count,
                            last = lastDevices.Count,
                            rate = string.Format("{0:P2}", lastDevices.Count > 0 ? (double)curDevices.Count / (double)lastDevices.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500203> Get500203(string parent, string[] types, int size) {
            var result = new List<Model500203>();
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var iStations = _workContext.iStations(DateTime.Now);
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
                iStations = iStations.FindAll(s => staTypeNames.Contains(s.Current.TypeName));
            }

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach (var child in area.Children) {
                        keys.Add(child.Current.Name);
                    }

                    var curStations = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var lastStations = iStations.FindAll(s => keys.Contains(s.iArea.Name));

                    result.Add(new Model500203 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        current = curStations.Count,
                        last = lastStations.Count,
                        rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach (var child in area.Children) {
                                keys.Add(child.Current.Name);
                            }

                            var curStations = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var lastStations = iStations.FindAll(s => keys.Contains(s.iArea.Name));

                            result.Add(new Model500203 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                current = curStations.Count,
                                last = lastStations.Count,
                                rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var curStations = stations.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var lastStations = iStations.FindAll(s => s.iArea.Name == current.Current.Name);

                        result.Add(new Model500203 {
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            current = curStations.Count,
                            last = lastStations.Count,
                            rate = string.Format("{0:P2}", lastStations.Count > 0 ? (double)curStations.Count / (double)lastStations.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500204> Get500204(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500204>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.qtkgdydzhglLeiXing == null
                || rtValues.qtkgdydzhglLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var staKeys = new HashSet<string>(stations.Select(s => s.Current.Id));
            var devTypes = rtValues.qtkgdydzhglLeiXing;
            var devices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id) && staKeys.Contains(d.Current.StationId));
            var values = _loadService.GetLoads(startDate, endDate).FindAll(l => l.Value < 0.65);
            var devKeys = values.Select(v => v.DeviceId);

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var children1 = devices.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var children2 = children1.FindAll(c => devKeys.Contains(c.Current.Id));

                    result.Add(new Model500204 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        count = children2.Count,
                        total = children1.Count,
                        rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var children1 = devices.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var children2 = children1.FindAll(c => devKeys.Contains(c.Current.Id));

                            result.Add(new Model500204 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                count = children2.Count,
                                total = children1.Count,
                                rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var children1 = devices.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var children2 = children1.FindAll(c => devKeys.Contains(c.Current.Id));

                        result.Add(new Model500204 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            count = children2.Count,
                            total = children1.Count,
                            rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500205> Get500205(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500205>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null || string.IsNullOrWhiteSpace(parent))
                return result;

            var discharges = _batTimeService.GetValues(startDate, endDate, EnmBatStatus.Discharge).FindAll(d => d.EndTime.Subtract(d.StartTime).TotalMinutes >= rtValues.qtxdchbschglShiJian);
            var minvalues = _batCurveService.GetMinValues(EnmBatStatus.Discharge, EnmBatPoint.DCZDY, startDate, endDate).FindAll(v => v.Value >= rtValues.qtxdchbschglDianYa);
            var matchs1 = discharges.GroupBy(d => d.StationId).Select(g => g.Key);
            var matchs2 = minvalues.GroupBy(m => m.StationId).Select(g => g.Key);

            var stations = new List<S_Station>();
            if (matchs1.Any()) {
                var _stations = _workContext.Stations();
                if (types != null && types.Length > 0)
                    _stations = _stations.FindAll(s => types.Contains(s.Current.Type.Id));

                stations.AddRange(_stations.FindAll(s => matchs1.Contains(s.Current.Id)).Select(s => s.Current));
            }

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var children1 = stations.FindAll(s => area.Keys.Contains(s.AreaId));
                    var children2 = children1.FindAll(c => matchs2.Contains(c.Id));

                    result.Add(new Model500205 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        count = children2.Count,
                        total = children1.Count,
                        rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var children1 = stations.FindAll(s => area.Keys.Contains(s.AreaId));
                            var children2 = children1.FindAll(c => matchs2.Contains(c.Id));

                            result.Add(new Model500205 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                count = children2.Count,
                                total = children1.Count,
                                rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var children1 = stations.FindAll(s => s.AreaId == current.Current.Id);
                        var children2 = children1.FindAll(c => matchs2.Contains(c.Id));

                        result.Add(new Model500205 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            count = children2.Count,
                            total = children1.Count,
                            rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500206> Get500206(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500206>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.qtwkrlhglWenDuXinHao == null
                || rtValues.qtwkrlhglWenDuXinHao.Length == 0
                || rtValues.qtwkrlhglGaoWenXinHao == null
                || rtValues.qtwkrlhglGaoWenXinHao.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var wdPoints = rtValues.qtwkrlhglWenDuXinHao;
            var gwPoints = rtValues.qtwkrlhglGaoWenXinHao;
            var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => gwPoints.Contains(a.PointId));
            var staKeys = new HashSet<string>(alarms.Select(a => a.StationId));

            var wdStations = _workContext.GetStationsWithPoints(wdPoints);
            if (types != null && types.Length > 0) wdStations = wdStations.FindAll(s => types.Contains(s.Type.Id));
            var gwStations = wdStations.FindAll(s => staKeys.Contains(s.Id));

            var index = 0;
            if (parent == "root") {
                #region root
                var leaies = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var leaf in leaies) {
                    var wd = wdStations.FindAll(s => leaf.Keys.Contains(s.AreaId));
                    var gw = gwStations.FindAll(s => leaf.Keys.Contains(s.AreaId));

                    result.Add(new Model500206 {
                        index = ++index,
                        name = leaf.ToString(),
                        type = leaf.Current.Type.Value,
                        current = gw.Count,
                        last = wd.Count,
                        rate = string.Format("{0:P2}", wd.Count > 0 ? 1 - (double)gw.Count / (double)wd.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var leaies = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var leaf in leaies) {
                            var wd = wdStations.FindAll(s => leaf.Keys.Contains(s.AreaId));
                            var gw = gwStations.FindAll(s => leaf.Keys.Contains(s.AreaId));

                            result.Add(new Model500206 {
                                index = ++index,
                                name = leaf.ToString(),
                                type = leaf.Current.Type.Value,
                                current = gw.Count,
                                last = wd.Count,
                                rate = string.Format("{0:P2}", wd.Count > 0 ? 1 - (double)gw.Count / (double)wd.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var wd = wdStations.FindAll(s => s.AreaId == current.Current.Id);
                        var gw = gwStations.FindAll(s => s.AreaId == current.Current.Id);

                        result.Add(new Model500206 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            current = gw.Count,
                            last = wd.Count,
                            rate = string.Format("{0:P2}", wd.Count > 0 ? 1 - (double)gw.Count / (double)wd.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500207> Get500207(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500207>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.qtzlxtkydXinHao == null
                || rtValues.qtzlxtkydXinHao.Length == 0
                || rtValues.qtzlxtkydLeiXing == null
                || rtValues.qtzlxtkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.qtzlxtkydXinHao;
            var devTypes = rtValues.qtzlxtkydLeiXing;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var staKeys = new HashSet<string>(stations.Select(s => s.Current.Id));
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => points.Contains(a.PointId));
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id) && staKeys.Contains(d.Current.StationId));

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var devices = allDevices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                    var devKeys = new HashSet<string>(devices.Select(d => d.Current.Id));
                    var alarms = allAlarms.FindAll(a => devKeys.Contains(a.DeviceId));

                    var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                    var cntTime = endDate.Subtract(startDate).TotalSeconds;
                    result.Add(new Model500207 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                        count = devices.Count,
                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                        rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var devices = allDevices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                            var devKeys = new HashSet<string>(devices.Select(d => d.Current.Id));
                            var alarms = allAlarms.FindAll(a => devKeys.Contains(a.DeviceId));

                            var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                            var cntTime = endDate.Subtract(startDate).TotalSeconds;
                            result.Add(new Model500207 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                count = devices.Count,
                                cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                                rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var devices = allDevices.FindAll(d => d.Current.AreaId == current.Current.Id);
                        var devKeys = new HashSet<string>(devices.Select(d => d.Current.Id));
                        var alarms = allAlarms.FindAll(a => devKeys.Contains(a.DeviceId));

                        var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                        var cntTime = endDate.Subtract(startDate).TotalSeconds;
                        result.Add(new Model500207 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                            count = devices.Count,
                            cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                            rate = string.Format("{0:P2}", devices.Count > 0 && cntTime > 0 ? 1 - almTime / (devices.Count * cntTime) : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500208> Get500208(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500208>();
            var rtValues = _workContext.RtValues();
            if (rtValues == null
                || rtValues.qtjkgzcljslXinHao == null
                || rtValues.qtjkgzcljslXinHao.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.qtjkgzcljslXinHao;
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var index = 0;
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => points.Contains(a.PointId));
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var children = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var matchs = new HashSet<string>(children.Select(c => c.Current.Id));
                    var alarms = allAlarms.FindAll(a => matchs.Contains(a.StationId));

                    var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                    var cntTime = endDate.Subtract(startDate).TotalSeconds;
                    result.Add(new Model500208 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                        count = children.Count,
                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                        rate = string.Format("{0:P2}", children.Count > 0 && cntTime > 0 ? 1 - almTime / (children.Count * cntTime) : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var children = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var matchs = new HashSet<string>(children.Select(c => c.Current.Id));
                            var alarms = allAlarms.FindAll(a => matchs.Contains(a.StationId));

                            var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                            var cntTime = endDate.Subtract(startDate).TotalSeconds;
                            result.Add(new Model500208 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                count = children.Count,
                                cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                                rate = string.Format("{0:P2}", children.Count > 0 && cntTime > 0 ? 1 - almTime / (children.Count * cntTime) : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var children = stations.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var matchs = new HashSet<string>(children.Select(c => c.Current.Id));
                        var alarms = allAlarms.FindAll(a => matchs.Contains(a.StationId));

                        var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                        var cntTime = endDate.Subtract(startDate).TotalSeconds;
                        result.Add(new Model500208 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                            count = children.Count,
                            cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                            rate = string.Format("{0:P2}", children.Count > 0 && cntTime > 0 ? 1 - almTime / (children.Count * cntTime) : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500209> Get500209(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500209>();
            if (string.IsNullOrWhiteSpace(parent))
                return result;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var index = 0;
            var times = _batTimeService.GetValues(startDate, endDate, EnmBatStatus.Discharge).FindAll(b => b.EndTime.Subtract(b.StartTime).TotalHours >= 1);
            var staids = times.GroupBy(t => t.StationId).Select(g => g.Key);
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var children2 = children1.FindAll(c => staids.Contains(c.Current.Id));

                    result.Add(new Model500209 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        current = children2.Count,
                        last = children1.Count,
                        rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var children2 = children1.FindAll(c => staids.Contains(c.Current.Id));

                            result.Add(new Model500209 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                current = children2.Count,
                                last = children1.Count,
                                rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var children1 = stations.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var children2 = children1.FindAll(c => staids.Contains(c.Current.Id));

                        result.Add(new Model500209 {
                            index = ++index,
                            name = current.ToString(),
                            type = current.Current.Type.Value,
                            current = children2.Count,
                            last = children1.Count,
                            rate = string.Format("{0:P2}", children1.Count > 0 ? (double)children2.Count / (double)children1.Count : 1)
                        });
                        #endregion
                    }
                }
            }

            return result;
        }

        private List<Model500301> Get500301(string parent, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500301>();
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if (parent == "root") {
                #region root
                if (size == (int)EnmSSH.Station) {
                    var energies = _elecService.GetHistory(EnmSSH.Station, startDate, endDate);
                    foreach (var child in _workContext.Stations()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", child.Current.AreaName, child.Current.Name)));
                    }
                } else if (size == (int)EnmSSH.Room) {
                    var energies = _elecService.GetHistory(EnmSSH.Room, startDate, endDate);
                    foreach (var child in _workContext.Rooms()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", child.Current.AreaName, child.Current.StationName, child.Current.Name)));
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (size == (int)EnmSSH.Station) {
                        var energies = _elecService.GetHistory(EnmSSH.Station, startDate, endDate);
                        var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach (var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", child.Current.AreaName, child.Current.Name)));
                        }
                    } else if (size == (int)EnmSSH.Room) {
                        var energies = _elecService.GetHistory(EnmSSH.Room, startDate, endDate);
                        var children = _workContext.Rooms().FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach (var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", child.Current.AreaName, child.Current.StationName, child.Current.Name)));
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private Model500301 Calculate500301(List<V_Elec> categories, int index, string name) {
            var current = new Model500301 {
                index = index,
                name = name,
                kt = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.KT).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                zm = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.ZM).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                bg = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.BG).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                it = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.IT).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                dy = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.DY).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                ups = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.UPS).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                qt = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.QT).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero),
                tt = Math.Round(categories.FindAll(c => c.FormulaType == EnmFormula.TT).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero)
            };

            current.ktrate = string.Format("{0:P2}", current.tt > 0 ? current.kt / current.tt : 0);
            current.zmrate = string.Format("{0:P2}", current.tt > 0 ? current.zm / current.tt : 0);
            current.bgrate = string.Format("{0:P2}", current.tt > 0 ? current.bg / current.tt : 0);
            current.itrate = string.Format("{0:P2}", current.tt > 0 ? current.it / current.tt : 0);
            current.dyrate = string.Format("{0:P2}", current.tt > 0 ? current.dy / current.tt : 0);
            current.upsrate = string.Format("{0:P2}", current.tt > 0 ? current.ups / current.tt : 0);
            current.qtrate = string.Format("{0:P2}", current.tt > 0 ? current.qt / current.tt : 0);
            return current;
        }

        private DataTable Get500302(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, List<ChartsModel> charts = null) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500302, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500302(period, startDate, endDate);
            if (string.IsNullOrWhiteSpace(parent)) return model;

            if (energies == null || energies.Length == 0) {
                energies = new int[] {
                    (int)EnmFormula.KT,
                    (int)EnmFormula.ZM,
                    (int)EnmFormula.BG,
                    (int)EnmFormula.DY,
                    (int)EnmFormula.UPS,
                    (int)EnmFormula.IT,
                    (int)EnmFormula.QT
                };
            }

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
            }

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                stations = stations.FindAll(s => s.Current.Id.Equals(nodeKey.Value));
            }

            var index = 0;
            var values = _elecService.GetHistory(EnmSSH.Station, startDate, endDate);
            foreach (var station in stations) {
                var _values = values.FindAll(v => v.Id.Equals(station.Current.Id));
                var _models = new List<ChartModel>();
                foreach (var energy in energies) {
                    var __enm = (EnmFormula)energy;
                    var __values = _values.FindAll(v => v.FormulaType == __enm);

                    var row = model.NewRow();
                    row["name"] = string.Format("{0},{1}", station.Current.AreaName, station.Current.Name);
                    row["type"] = Common.GetEnergyDisplay(__enm);
                    for (var k = 3; k < model.Columns.Count; k++) {
                        var column = model.Columns[k];
                        var start = (DateTime)column.ExtendedProperties["Start"];
                        var end = (DateTime)column.ExtendedProperties["End"];
                        row[k] = Math.Round(__values.FindAll(v => v.StartTime >= start && v.StartTime <= end).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero);
                    }
                    model.Rows.Add(row);

                    if (charts != null && !cache) {
                        _models.Add(new ChartModel { index = energy, name = Common.GetEnergyDisplay(__enm), value = Math.Round(__values.Sum(c => c.Value), 3, MidpointRounding.AwayFromZero) });
                    }
                }

                if (charts != null && !cache) {
                    charts.Add(new ChartsModel { index = ++index, name = station.Current.Name, models = _models });
                }
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private DataTable GetDataTable500302(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500302");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("name", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "站点名称");
            model.Columns.Add(column1);

            var column2 = new DataColumn("type", typeof(string));
            column2.ExtendedProperties.Add("ExcelDisplayName", "能耗分类");
            model.Columns.Add(column2);

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

        private DataTable Get500303(string parent, string[] types, int[] energies, EnmPDH period, DateTime startDate, DateTime endDate, bool cache, List<ChartsModel> charts = null) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500303, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500303(period, startDate, endDate);
            if (string.IsNullOrWhiteSpace(parent)) return model;

            if (energies == null || energies.Length == 0) {
                energies = new int[] {
                    (int)EnmFormula.KT,
                    (int)EnmFormula.ZM,
                    (int)EnmFormula.BG,
                    (int)EnmFormula.DY,
                    (int)EnmFormula.UPS,
                    (int)EnmFormula.IT,
                    (int)EnmFormula.QT
                };
            }

            var rooms = _workContext.Rooms();
            if (types != null && types.Length > 0) {
                rooms = rooms.FindAll(s => types.Contains(s.Current.Type.Id));
            }

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) rooms = rooms.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                rooms = rooms.FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
            } else if (nodeKey.Key == EnmSSH.Room) {
                rooms = rooms.FindAll(r => r.Current.Id.Equals(nodeKey.Value));
            }

            var index = 0;
            var values = _elecService.GetHistory(EnmSSH.Room, startDate, endDate);
            foreach (var room in rooms) {
                var _values = values.FindAll(v => v.Id.Equals(room.Current.Id));
                var _models = new List<ChartModel>();
                foreach (var energy in energies) {
                    var __enm = (EnmFormula)energy;
                    var __values = _values.FindAll(v => v.FormulaType == __enm);

                    var row = model.NewRow();
                    row["name"] = string.Format("{0},{1},{2}", room.Current.AreaName, room.Current.StationName, room.Current.Name);
                    row["type"] = Common.GetEnergyDisplay(__enm);
                    for (var k = 3; k < model.Columns.Count; k++) {
                        var column = model.Columns[k];
                        var start = (DateTime)column.ExtendedProperties["Start"];
                        var end = (DateTime)column.ExtendedProperties["End"];
                        row[k] = Math.Round(__values.FindAll(v => v.StartTime >= start && v.StartTime <= end).Sum(c => c.Value), 3, MidpointRounding.AwayFromZero);
                    }
                    model.Rows.Add(row);

                    if (charts != null && !cache) {
                        _models.Add(new ChartModel { index = energy, name = Common.GetEnergyDisplay(__enm), value = Math.Round(__values.Sum(c => c.Value), 3, MidpointRounding.AwayFromZero) });
                    }
                }

                if (charts != null && !cache) {
                    charts.Add(new ChartsModel { index = ++index, name = room.Current.Name, models = _models });
                }
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private DataTable GetDataTable500303(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500303");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("name", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "机房名称");
            model.Columns.Add(column1);

            var column2 = new DataColumn("type", typeof(string));
            column2.ExtendedProperties.Add("ExcelDisplayName", "能耗分类");
            model.Columns.Add(column2);

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

        private DataTable Get500304(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500304, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500304(period, startDate, endDate);
            if (string.IsNullOrWhiteSpace(parent)) return model;

            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
            }

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                stations = stations.FindAll(s => s.Current.Id.Equals(nodeKey.Value));
            }

            var values = _elecService.GetHistory(EnmSSH.Station, EnmFormula.PUE, startDate, endDate);
            foreach (var station in stations) {
                var _values = values.FindAll(v => v.Id.Equals(station.Current.Id));

                var row = model.NewRow();
                row["name"] = string.Format("{0},{1}", station.Current.AreaName, station.Current.Name);
                for (var k = 2; k < model.Columns.Count; k++) {
                    var column = model.Columns[k];
                    var start = (DateTime)column.ExtendedProperties["Start"];
                    var end = (DateTime)column.ExtendedProperties["End"];
                    var __values = _values.FindAll(v => v.StartTime >= start && v.StartTime <= end);
                    var pue = Math.Round(__values.Count > 0 ? __values.Average(c => c.Value) : 0, 3, MidpointRounding.AwayFromZero);
                    row[k] = pue;
                }
                model.Rows.Add(row);
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private DataTable GetDataTable500304(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500304");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("name", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "站点名称");
            model.Columns.Add(column1);

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

        private DataTable Get500305(string parent, string[] types, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500305, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500305(period, startDate, endDate);
            if (string.IsNullOrWhiteSpace(parent)) return model;

            var rooms = _workContext.Rooms();
            if (types != null && types.Length > 0) {
                rooms = rooms.FindAll(s => types.Contains(s.Current.Type.Id));
            }

            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) rooms = rooms.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                rooms = rooms.FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
            } else if (nodeKey.Key == EnmSSH.Room) {
                rooms = rooms.FindAll(s => s.Current.Id.Equals(nodeKey.Value));
            }

            var values = _elecService.GetHistory(EnmSSH.Room, EnmFormula.PUE, startDate, endDate);
            foreach (var room in rooms) {
                var _values = values.FindAll(v => v.Id.Equals(room.Current.Id));

                var row = model.NewRow();
                row["name"] = string.Format("{0},{1},{2}", room.Current.AreaName, room.Current.StationName, room.Current.Name);
                for (var k = 2; k < model.Columns.Count; k++) {
                    var column = model.Columns[k];
                    var start = (DateTime)column.ExtendedProperties["Start"];
                    var end = (DateTime)column.ExtendedProperties["End"];
                    var __values = _values.FindAll(v => v.StartTime >= start && v.StartTime <= end);
                    var pue = Math.Round(__values.Count > 0 ? __values.Average(c => c.Value) : 0, 3, MidpointRounding.AwayFromZero);
                    row[k] = pue;
                }
                model.Rows.Add(row);
            }

            if (model.Rows.Count <= GlobalCacheLimit.Default_Limit) {
                var bytes = CommonHelper.DtToXml(model);
                _cacheManager.Set(key, bytes, GlobalCacheInterval.Site_Interval);
            }

            return model;
        }

        private DataTable GetDataTable500305(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500305");
            var column0 = new DataColumn("index", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            column0.ExtendedProperties.Add("ExcelDisplayName", "序号");
            model.Columns.Add(column0);

            var column1 = new DataColumn("name", typeof(string));
            column1.ExtendedProperties.Add("ExcelDisplayName", "机房名称");
            model.Columns.Add(column1);

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

        private DataTable Get500306(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500306, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500306(period, startDate, endDate);
            var rtValues = _workContext.RtValues();
            if (rtValues == null || rtValues.byqnhLeiXing == null || rtValues.byqnhLeiXing.Length == 0)
                return model;

            if(string.IsNullOrWhiteSpace(parent))
                return model;

            var devices = _workContext.Devices().FindAll(d => rtValues.byqnhLeiXing.Contains(d.Current.SubType.Id));
            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
            } else if (nodeKey.Key == EnmSSH.Room) {
                devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
            }

            var values = _elecService.GetHistory(EnmSSH.Device, EnmFormula.BY, startDate, endDate);
            foreach (var device in devices) {
                var details = values.FindAll(o => o.Id.Equals(device.Current.Id));

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

        private DataTable GetDataTable500306(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500306");
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

        private DataTable Get500307(string parent, EnmPDH period, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);

            var key = string.Format(GlobalCacheKeys.Report_500307, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) {
                var bytes = _cacheManager.Get<byte[]>(key);
                return CommonHelper.XmlToDt(bytes);
            }

            var model = this.GetDataTable500307(period, startDate, endDate);
            var rtValues = _workContext.RtValues();
            if (rtValues == null || rtValues.byqnhLeiXing == null || rtValues.byqnhLeiXing.Length == 0)
                return model;

            if (string.IsNullOrWhiteSpace(parent))
                return model;

            var devices = _workContext.Devices().FindAll(d => rtValues.byqnhLeiXing.Contains(d.Current.SubType.Id));
            var nodeKey = Common.ParseNode(parent);
            if (nodeKey.Key == EnmSSH.Area) {
                var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                if (current != null) devices = devices.FindAll(r => current.Keys.Contains(r.Current.AreaId));
            } else if (nodeKey.Key == EnmSSH.Station) {
                devices = devices.FindAll(r => r.Current.StationId.Equals(nodeKey.Value));
            } else if (nodeKey.Key == EnmSSH.Room) {
                devices = devices.FindAll(r => r.Current.RoomId.Equals(nodeKey.Value));
            }

            var values = _elecService.GetHistory(EnmSSH.Device, EnmFormula.XS, startDate, endDate);
            foreach (var device in devices) {
                var details = values.FindAll(o => o.Id.Equals(device.Current.Id));

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

        private DataTable GetDataTable500307(EnmPDH period, DateTime start, DateTime end) {
            var model = new DataTable("500307");
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

        private List<Model500401> Get500401(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500401>();
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return result;

            var devices = _workContext.Devices();
            if (types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                foreach (var area in areas) {
                    var childDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                    var childDevIds = new HashSet<string>(childDevices.Select(d => d.Current.Id));
                    var childAlarms = alarms.FindAll(a => childDevIds.Contains(a.DeviceId));

                    var devCount = childDevices.Count;
                    var almTime = childAlarms.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                    var cntTime = endDate.Subtract(startDate).TotalSeconds;
                    result.Add(new Model500401 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        devCount = devCount,
                        almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                        cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                        rate = string.Format("{0:P2}", (devCount > 0 && cntTime > 0) ? 1 - almTime / (devCount * cntTime) : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                        foreach (var area in areas) {
                            var childDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                            var childDevIds = new HashSet<string>(childDevices.Select(d => d.Current.Id));
                            var childAlarms = alarms.FindAll(a => childDevIds.Contains(a.DeviceId));

                            var devCount = childDevices.Count;
                            var almTime = childAlarms.Sum(d => d.EndTime.Subtract(d.StartTime).TotalSeconds);
                            var cntTime = endDate.Subtract(startDate).TotalSeconds;
                            result.Add(new Model500401 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                devCount = devCount,
                                almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                                cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                                rate = string.Format("{0:P2}", (devCount > 0 && cntTime > 0) ? 1 - almTime / (devCount * cntTime) : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var alarms = _hisAlarmService.GetAlarmsInArea(parent, startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                        var childDevices = devices.FindAll(d => d.Current.AreaId == parent);
                        var childDevIds = new HashSet<string>(childDevices.Select(d => d.Current.Id));
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
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return result;

            var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.jslHuLue);
            if (types != null && types.Length > 0) {
                var devMatchs = _workContext.Devices().FindAll(d => types.Contains(d.Current.Type.Id)).Select(d => d.Current.Id);
                alarms = alarms.FindAll(a => devMatchs.Contains(a.DeviceId));
            }

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var childAlarms = alarms.FindAll(a => area.Keys.Contains(a.AreaId));
                    var count = childAlarms.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                    var total = childAlarms.Count;
                    result.Add(new Model500402 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        count = count,
                        total = total,
                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var childAlarms = alarms.FindAll(a => area.Keys.Contains(a.AreaId));
                            var count = childAlarms.Count(a => a.EndTime.Subtract(a.StartTime).TotalMinutes >= rtValues.jslGuiDing);
                            var total = childAlarms.Count;
                            result.Add(new Model500402 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
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
            if (string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues();
            if (rtValues == null) return result;

            var alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            var stores = _workContext.AlarmsToStore(alarms);

            var index = 0;
            if (parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach (var area in areas) {
                    var childStores = stores.FindAll(a => area.Keys.Contains(a.Current.AreaId));
                    var count = childStores.Count(a => (a.Current.ConfirmedTime.HasValue ? a.Current.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
                    var total = childStores.Count;
                    result.Add(new Model500403 {
                        index = ++index,
                        name = area.ToString(),
                        type = area.Current.Type.Value,
                        count = count,
                        total = total,
                        rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                    });
                }
                #endregion
            } else {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if (current != null) {
                    if (current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach (var area in areas) {
                            var childStores = stores.FindAll(a => area.Keys.Contains(a.Current.AreaId));
                            var count = childStores.Count(a => (a.Current.ConfirmedTime.HasValue ? a.Current.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
                            var total = childStores.Count;
                            result.Add(new Model500403 {
                                index = ++index,
                                name = area.ToString(),
                                type = area.Current.Type.Value,
                                count = count,
                                total = total,
                                rate = string.Format("{0:P2}", total > 0 ? 1 - (double)count / (double)total : 1)
                            });
                        }
                        #endregion
                    } else {
                        #region self
                        var childStores = stores.FindAll(a => a.Current.AreaId == current.Current.Id);
                        var count = childStores.Count(a => (a.Current.ConfirmedTime.HasValue ? a.Current.ConfirmedTime.Value : a.Current.EndTime).Subtract(a.Current.StartTime).TotalMinutes >= rtValues.jslQueRen);
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