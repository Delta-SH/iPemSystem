using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
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
        private readonly IHAlarmService _hisAlarmService;
        private readonly IBatService _batService;
        private readonly IBatTimeService _batTimeService;
        private readonly IElecService _elecService;
        private readonly ILoadService _loadService;
        private readonly ICutService _cutService;

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
            IHAlarmService hisAlarmService,
            IBatService batService,
            IBatTimeService batTimeService,
            IElecService elecService,
            ILoadService loadService,
            ICutService cutService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._dictionaryService = dictionaryService;
            this._enumMethodService = enumMethodService;
            this._stationTypeService = stationTypeService;
            this._stationService = stationService;
            this._deviceTypeService = deviceTypeService;
            this._hisAlarmService = hisAlarmService;
            this._batService = batService;
            this._batTimeService = batTimeService;
            this._loadService = loadService;
            this._elecService = elecService;
            this._cutService = cutService;
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
        public ActionResult Download500101(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500101(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500101>(models, "直流系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model500102>(models, "交流不间断系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500103(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500103(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500103>(models, "温控系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500104(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500104(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500104>(models, "监控可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500105(string parent, string[] types, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500105(parent, types, startDate, endDate);
                using(var ms = _excelManager.Export<Model500105>(models, "市电可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500201(string parent, string[] types, int size) {
            try {
                var models = this.Get500201(parent, types, size);
                using(var ms = _excelManager.Export<Model500201>(models, "监控覆盖率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500202(string parent, string[] types, int size) {
            try {
                var models = this.Get500202(parent, types, size);
                using(var ms = _excelManager.Export<Model500202>(models, "关键监控测点接入率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500203(string parent, string[] types, int size) {
            try {
                var models = this.Get500203(parent, types, size);
                using(var ms = _excelManager.Export<Model500203>(models, "站点标识率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500204(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500204(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500204>(models, "开关电源带载合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500205(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500205(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500205>(models, "蓄电池后备时长合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500206(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500206(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500206>(models, "温控容量合格率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500207(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500207(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500207>(models, "直流系统可用度", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500208(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500208(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500208>(models, "监控故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
        public ActionResult Download500209(string parent, string[] types, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500209(parent, types, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500209>(models, "蓄电池核对性放电及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model500301>(models, "能耗分类信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFields500302(int period, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<string>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<string>()
            };

            try {
                var models = this.GetModel500302(period, startDate, endDate);
                if(models != null && models.Columns.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Columns.Count;

                    for(int i = 0; i < models.Columns.Count; i++) {
                        data.data.Add(models.Columns[i].ColumnName);
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
        public JsonResult Request500302(int start, int limit, string parent, int period, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<DataTable> {
                success = true,
                message = "无数据",
                total = 0,
                data = null
            };

            try {
                var models = this.Get500302(parent, period, size, startDate, endDate);
                if(models != null && models.Rows.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Rows.Count;

                    var end = start + limit;
                    if(end > models.Rows.Count)
                        end = models.Rows.Count;

                    data.data = models.Clone();
                    for(int i = start; i < end; i++) {
                        data.data.Rows.Add(models.Rows[i].ItemArray);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Download500302(string parent, int period, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500302(parent, period, size, startDate, endDate);
                using(var ms = _excelManager.Export(models, "能耗趋势分析", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500303(int start, int limit, string parent, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxChartModel<List<Model500303>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500303>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.Get500303(parent, size, startDate, endDate);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    foreach(var model in models) {
                        var names = model.name.Split(new char[] { ',' });
                        data.chart.Add(new ChartModel { 
                            index = model.index,
                            name = names.Length > 0 ? names[names.Length - 1] : "", 
                            value = model.current,
                            comment = model.last.ToString()
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
        public ActionResult Download500303(string parent, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500303(parent, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500303>(models, "能耗同址同比", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500304(int start, int limit, string parent, int size, DateTime startDate, DateTime endDate) {
            var data = new AjaxChartModel<List<Model500304>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500304>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.Get500304(parent, size, startDate, endDate);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    foreach(var model in models) {
                        var names = model.name.Split(new char[] { ',' });
                        data.chart.Add(new ChartModel {
                            index = model.index,
                            name = names.Length > 0 ? names[names.Length - 1] : "",
                            value = model.current,
                            comment = model.last.ToString()
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
        public ActionResult Download500304(string parent, int size, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500304(parent, size, startDate, endDate);
                using(var ms = _excelManager.Export<Model500304>(models, "能耗同址环比", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500305(int start, int limit, string[] parents, int period, DateTime startDate, DateTime endDate) {
            var data = new AjaxChartModel<List<Model500305>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500305>(),
                chart = new List<ChartsModel>()
            };

            try {
                if(parents == null || parents.Length != 2) throw new iPemException("仅支持两个对比站点");

                var akeys = Common.SplitKeys(parents[0]);
                if(akeys.Length != 2) throw new iPemException("无效的参数 parents");

                var bkeys = Common.SplitKeys(parents[1]);
                if(bkeys.Length != 2) throw new iPemException("无效的参数 parents");

                var models = this.Get500305(akeys[1], bkeys[1], period, startDate, endDate);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    foreach(var model in models) {
                        var chart = new ChartsModel {
                            index = model.index,
                            name = model.period,
                            models = new List<ChartModel>() { new ChartModel { index = 0, name = model.Aname, value = model.Avalue }, new ChartModel { index = 1, name = model.Bname, value = model.Bvalue } }
                        };
                        data.chart.Add(chart);
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
        public ActionResult Download500305(string[] parents, int period, DateTime startDate, DateTime endDate) {
            try {
                if(parents == null || parents.Length != 2) throw new iPemException("仅支持两个对比站点");

                var akeys = Common.SplitKeys(parents[0]);
                if(akeys.Length != 2) throw new iPemException("无效的参数 parents");

                var bkeys = Common.SplitKeys(parents[1]);
                if(bkeys.Length != 2) throw new iPemException("无效的参数 parents");

                var models = this.Get500305(akeys[1], bkeys[1], period, startDate, endDate);
                using(var ms = _excelManager.Export<Model500305>(models, "站点能耗对比", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Request500306(int start, int limit, string parent, DateTime startDate, DateTime endDate) {
            var data = new AjaxChartModel<List<Model500306>, List<ChartsModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model500306>(),
                chart = new List<ChartsModel>()
            };

            try {
                var models = this.Get500306(parent, startDate, endDate);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    foreach(var model in models) {
                        var names = model.name.Split(new char[] { ',' });
                        var chart = new ChartsModel {
                            index = model.index,
                            name = names.Length > 0 ? names[names.Length - 1] : "",
                            models = new List<ChartModel>()
                        };

                        chart.models.Add(new ChartModel {
                            index = 0,
                            name = "设备能耗",
                            value = model.device
                        });

                        chart.models.Add(new ChartModel {
                            index = 1,
                            name = "总能耗",
                            value = model.total
                        });

                        chart.models.Add(new ChartModel {
                            index = 2,
                            name = "PUE",
                            value = model.pue
                        });

                        chart.models.Add(new ChartModel {
                            index = 3,
                            name = "能效",
                            value = model.eer
                        });

                        data.chart.Add(chart);
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
        public ActionResult Download500306(string parent, DateTime startDate, DateTime endDate) {
            try {
                var models = this.Get500306(parent, startDate, endDate);
                using(var ms = _excelManager.Export<Model500306>(models, "站点PUE信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model500401>(models, "系统设备完好率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model500402>(models, "故障处理及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
                using(var ms = _excelManager.Export<Model500403>(models, "告警确认及时率", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
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
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach(var station in stations) {
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                var alarms = _hisAlarmService.GetAlarmsInStation(station.Current.Id, startDate, endDate).FindAll(a => points.Contains(a.PointId));

                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500101 {
                    index = ++index,
                    area = area == null ? "" : area.ToString(),
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
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate);
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach(var station in stations) {
                var alarms = allAlarms.FindAll(a => a.StationId == station.Current.Id);
                var almAlarms = alarms.FindAll(a => almPoints.Contains(a.PointId));
                var runAlarms = alarms.FindAll(a => runPoints.Contains(a.PointId));
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);
                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);

                var almTime = almAlarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var runTime = runAlarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500102 {
                    index = ++index,
                    area = area == null ? "" : area.ToString(),
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
            if(types != null && types.Length > 0) stations = stations.FindAll(d => types.Contains(d.Type.Id));

            if(parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.AreaId));
            }

            var index = 0;
            var gwAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => gwPoints.Contains(a.PointId));
            foreach(var station in stations) {
                var alarms = gwAlarms.FindAll(a => a.StationId == station.Id);
                var total = station.CityElectNumber; //在GetStationsWithPoints方法中，使用CityElectNumber字段存储指定信号的数量

                var area = _workContext.Areas().Find(a => a.Current.Id == station.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500103 {
                    index = ++index,
                    area = area == null ? "" : area.ToString(),
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
            if(rtValues == null
                || rtValues.hxjkkydXinHao == null
                || rtValues.hxjkkydXinHao.Length == 0
                || rtValues.hxjkkydLeiXing == null
                || rtValues.hxjkkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.hxjkkydXinHao;
            var devTypes = rtValues.hxjkkydLeiXing;

            var stations = _workContext.Stations();
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => points.Contains(a.PointId));
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id));
            foreach(var station in stations) {
                var alarms = allAlarms.FindAll(a => a.StationId == station.Current.Id);
                var devices = allDevices.FindAll(d => d.Current.StationId == station.Current.Id);

                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;
                result.Add(new Model500104 {
                    index = ++index,
                    area = area == null ? "" : area.ToString(),
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
            if(types != null && types.Length > 0)
                stations = stations.FindAll(d => types.Contains(d.Current.Type.Id));

            if(parent != "root") {
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) stations = stations.FindAll(s => current.Keys.Contains(s.Current.AreaId));
            }

            var index = 0;
            var cuteds = _cutService.GetCuteds(startDate, endDate, EnmCutType.Cut);
            foreach(var station in stations) {
                var alarms = cuteds.FindAll(a => a.StationId == station.Current.Id);
                var area = _workContext.Areas().Find(a => a.Current.Id == station.Current.AreaId);
                var almTime = alarms.Sum(a => a.EndTime.Subtract(a.StartTime).TotalSeconds);
                var cntTime = endDate.Subtract(startDate).TotalSeconds;

                result.Add(new Model500105 {
                    index = ++index,
                    area = area == null ? "" : area.ToString(),
                    station = station.Current.Name,
                    type = station.Current.Type.Name,
                    count = station.Current.CityElectNumber,
                    almTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(almTime)),
                    cntTime = CommonHelper.IntervalConverter(TimeSpan.FromSeconds(cntTime)),
                    rate = string.Format("{0:P2}", station.Current.CityElectNumber > 0 && cntTime > 0 ? 1 - almTime / (station.Current.CityElectNumber * cntTime) : 1)
                });
            }

            return result;
        }

        private List<Model500201> Get500201(string parent, string[] types, int size) {
            var result = new List<Model500201>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var iStations = _workContext.iStations(DateTime.Now);
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
                iStations = iStations.FindAll(s => staTypeNames.Contains(s.Current.TypeName));
            }

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach(var child in area.Children) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach(var child in area.Children) {
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
            if(rtValues == null 
                || rtValues.qtgjjkcdjrlLeiXing == null 
                || rtValues.qtgjjkcdjrlLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent)) return result;

            var devTypeIds = rtValues.qtgjjkcdjrlLeiXing;
            var devTypeNames = _deviceTypeService.GetSubDeviceTypes().FindAll(t => devTypeIds.Contains(t.Id)).Select(t => t.Name).ToList();

            var devices = _workContext.Devices().FindAll(d => devTypeIds.Contains(d.Current.SubType.Id));
            var iDevices = _workContext.iDevices(DateTime.Now).FindAll(i=>devTypeNames.Contains(i.Current.TypeName));
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                devices = devices.FindAll(s => types.Contains(s.Current.StaTypeId));
                iDevices = iDevices.FindAll(i => staTypeNames.Contains(i.iStation.TypeName));
            }
            
            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach(var child in area.Children) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach(var child in area.Children) {
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
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var iStations = _workContext.iStations(DateTime.Now);
            var stations = _workContext.Stations();
            if (types != null && types.Length > 0) {
                var staTypeNames = _workContext.StationTypes().FindAll(t => types.Contains(t.Id)).Select(t => t.Name);
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));
                iStations = iStations.FindAll(s => staTypeNames.Contains(s.Current.TypeName));
            }

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var keys = new List<string>();
                    keys.Add(area.Current.Name);
                    foreach(var child in area.Children) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var keys = new List<string>();
                            keys.Add(area.Current.Name);
                            foreach(var child in area.Children) {
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
            if(rtValues == null
                || rtValues.qtkgdydzhglLeiXing == null
                || rtValues.qtkgdydzhglLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var stations = _workContext.Stations();
            if(types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var staKeys = new HashSet<string>(stations.Select(s => s.Current.Id));
            var devTypes = rtValues.qtkgdydzhglLeiXing;
            var devices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id) && staKeys.Contains(d.Current.StationId));
            var values = _loadService.GetLoads(startDate, endDate).FindAll(l => l.Value < 0.65);
            var devKeys = values.Select(v => v.DeviceId);

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
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

            var values = _batTimeService.GetValues(startDate, endDate, EnmBatStatus.Discharge);
            var ovalues1 = values.FindAll(b => b.EndTime.Subtract(b.StartTime).TotalMinutes >= rtValues.qtxdchbschglShiJian);
            var ovalues2 = ovalues1.FindAll(v => v.EndValue >= rtValues.qtxdchbschglDianYa);
            var matchs1 = from o in ovalues1 group o by o.StationId into g select g.Key;
            var matchs2 = from o in ovalues2 group o by o.StationId into g select g.Key;

            var stations = _workContext.Stations().FindAll(s => matchs1.Contains(s.Current.Id));
            if(types != null && types.Length > 0) 
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var children2 = children1.FindAll(c => matchs2.Contains(c.Current.Id));

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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var children2 = children1.FindAll(c => matchs2.Contains(c.Current.Id));

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
                        var children1 = stations.FindAll(s => s.Current.AreaId == current.Current.Id);
                        var children2 = children1.FindAll(c => matchs2.Contains(c.Current.Id));

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
            if(rtValues == null
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
            if(parent == "root") {
                #region root
                var leaies = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var leaf in leaies) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var leaies = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var leaf in leaies) {
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
            if(rtValues == null
                || rtValues.qtzlxtkydXinHao == null
                || rtValues.qtzlxtkydXinHao.Length == 0
                || rtValues.qtzlxtkydLeiXing == null
                || rtValues.qtzlxtkydLeiXing.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.qtzlxtkydXinHao;
            var devTypes = rtValues.qtzlxtkydLeiXing;

            var stations = _workContext.Stations();
            if(types != null && types.Length > 0) stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var staKeys = new HashSet<string>(stations.Select(s => s.Current.Id));
            var allAlarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => points.Contains(a.PointId));
            var allDevices = _workContext.Devices().FindAll(d => devTypes.Contains(d.Current.SubType.Id) && staKeys.Contains(d.Current.StationId));

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
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
            if(rtValues == null
                || rtValues.qtjkgzcljslXinHao == null
                || rtValues.qtjkgzcljslXinHao.Length == 0
                || string.IsNullOrWhiteSpace(parent))
                return result;

            var points = rtValues.qtjkgzcljslXinHao;
            var stations = _workContext.Stations();
            if(types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var allAlms = _hisAlarmService.GetAlarms(startDate, endDate)
                          .FindAll(a => points.Contains(a.PointId));

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var children = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var matchs = children.Select(c => c.Current.Id);
                    var alarms = allAlms.FindAll(a => matchs.Contains(a.StationId));
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var children = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var matchs = children.Select(c => c.Current.Id);
                            var alarms = allAlms.FindAll(a => matchs.Contains(a.StationId));
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
                        var matchs = children.Select(c => c.Current.Id);
                        var alarms = allAlms.FindAll(a => matchs.Contains(a.StationId));
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
            if(string.IsNullOrWhiteSpace(parent))
                return result;

            var stations = _workContext.Stations();
            if(types != null && types.Length > 0)
                stations = stations.FindAll(s => types.Contains(s.Current.Type.Id));

            var values = _batTimeService.GetValues(startDate, endDate, EnmBatStatus.Discharge).FindAll(b => b.EndTime.Subtract(b.StartTime).TotalHours >= 1);
            var staKeys = from val in values
                          group val by val.StationId into g
                          select g.Key;

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
                    var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                    var children2 = children1.FindAll(c => staKeys.Contains(c.Current.Id));

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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
                            var children1 = stations.FindAll(s => area.Keys.Contains(s.Current.AreaId));
                            var children2 = children1.FindAll(c => staKeys.Contains(c.Current.Id));

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
                        var children2 = children1.FindAll(c => staKeys.Contains(c.Current.Id));

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
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if(parent == "root") {
                #region root
                if(size == (int)EnmSSH.Area) {
                    var energies = _elecService.GetEnergies(EnmSSH.Station, startDate, endDate);
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                        var categories = energies.FindAll(e => children.Contains(e.Id));
                        result.Add(this.Calculate500301(categories, ++index, root.ToString()));
                    }
                } else if(size == (int)EnmSSH.Station) {
                    var energies = _elecService.GetEnergies(EnmSSH.Station, startDate, endDate);
                    foreach(var child in _workContext.Stations()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name)));
                    }
                } else if(size == (int)EnmSSH.Room) {
                    var energies = _elecService.GetEnergies(EnmSSH.Room, startDate, endDate);
                    foreach(var child in _workContext.Rooms()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);
                        result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name)));
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(size == (int)EnmSSH.Area) {
                        var energies = _elecService.GetEnergies(EnmSSH.Station, startDate, endDate);
                        if(current.HasChildren) {
                            foreach(var root in current.ChildRoot) {
                                var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                                var categories = energies.FindAll(e => children.Contains(e.Id));
                                result.Add(this.Calculate500301(categories, ++index, root.ToString()));
                            }
                        } else {
                            var children = _workContext.Stations().FindAll(s => s.Current.AreaId == current.Current.Id).Select(s => s.Current.Id);
                            var categories = energies.FindAll(e => children.Contains(e.Id));
                            result.Add(this.Calculate500301(categories, ++index, current.ToString()));
                        }
                    } else if(size == (int)EnmSSH.Station) {
                        var energies = _elecService.GetEnergies(EnmSSH.Station, startDate, endDate);
                        var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1}", area != null ? area.ToString() : current.ToString(), child.Current.Name)));
                        }
                    } else if(size == (int)EnmSSH.Room) {
                        var energies = _elecService.GetEnergies(EnmSSH.Room, startDate, endDate);
                        var children = _workContext.Rooms().FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);
                            result.Add(this.Calculate500301(categories, ++index, string.Format("{0},{1},{2}", area != null ? area.ToString() : current.ToString(), child.Current.StationName, child.Current.Name)));
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

        private DataTable Get500302(string parent, int period, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = this.GetModel500302(period, startDate, endDate);
            if(string.IsNullOrWhiteSpace(parent)) return result;

            if(parent == "root") {
                #region root
                if(size == (int)EnmSSH.Area) {
                    var energies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                        var categories = energies.FindAll(e => children.Contains(e.Id));

                        this.Calculate500302(result, categories, root.ToString());
                    }
                } else if(size == (int)EnmSSH.Station) {
                    var energies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    foreach(var child in _workContext.Stations()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        this.Calculate500302(result, categories, string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name));
                    }
                } else if(size == (int)EnmSSH.Room) {
                    var energies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                    foreach(var child in _workContext.Rooms()) {
                        var categories = energies.FindAll(e => e.Id == child.Current.Id);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        this.Calculate500302(result, categories, string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name));
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(size == (int)EnmSSH.Area) {
                        var energies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        if(current.HasChildren) {
                            foreach(var root in current.ChildRoot) {
                                var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                                var categories = energies.FindAll(e => children.Contains(e.Id));

                                this.Calculate500302(result, categories, root.ToString());
                            }
                        } else {
                            var children = _workContext.Stations().FindAll(s => s.Current.AreaId == current.Current.Id).Select(s => s.Current.Id);
                            var categories = energies.FindAll(e => children.Contains(e.Id));

                            this.Calculate500302(result, categories, current.ToString());
                        }
                    } else if(size == (int)EnmSSH.Station) {
                        var energies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            this.Calculate500302(result, categories, string.Format("{0},{1}", area != null ? area.ToString() : current.ToString(), child.Current.Name));
                        }
                    } else if(size == (int)EnmSSH.Room) {
                        var energies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                        var children = _workContext.Rooms().FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach(var child in children) {
                            var categories = energies.FindAll(e => e.Id == child.Current.Id);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            this.Calculate500302(result, categories, string.Format("{0},{1},{2}", area != null ? area.ToString() : current.ToString(), child.Current.StationName, child.Current.Name));
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private DataTable GetModel500302(int period, DateTime startDate, DateTime endDate) {
            var model = new DataTable("Model500302");
            var column0 = new DataColumn("序号", typeof(int));
            column0.AutoIncrement = true;
            column0.AutoIncrementSeed = 1;
            model.Columns.Add(column0);

            var column1 = new DataColumn("名称", typeof(string));
            model.Columns.Add(column1);

            startDate = startDate.Date;endDate = endDate.Date;
            var dates = new List<DateTime>();
            while(startDate <= endDate) {
                dates.Add(startDate);
                startDate = startDate.AddDays(1);
            }

            if(period == (int)EnmPDH.Month) {
                dates = dates.GroupBy(d => new { d.Year, d.Month }).Select(g => new DateTime(g.Key.Year, g.Key.Month, 1)).ToList();
                foreach(var date in dates) {
                    var column = new DataColumn(CommonHelper.MonthConverter(date), typeof(double));
                    column.DefaultValue = 0;
                    column.ExtendedProperties.Add("Start", date);
                    column.ExtendedProperties.Add("End", date.AddMonths(1).AddSeconds(-1));
                    model.Columns.Add(column);
                }
            } else if(period == (int)EnmPDH.Week) {
                dates = dates.GroupBy(d => d.Date.AddDays(-1 * (((int)d.DayOfWeek + 6) % 7))).Select(g => g.Key).ToList();
                foreach(var date in dates) {
                    var column = new DataColumn(CommonHelper.WeekConverter(date), typeof(double));
                    column.DefaultValue = 0;
                    column.ExtendedProperties.Add("Start", date);
                    column.ExtendedProperties.Add("End", date.AddDays(6).AddSeconds(86399));
                    model.Columns.Add(column);
                }
            } else if(period == (int)EnmPDH.Day) {
                foreach(var date in dates) {
                    var column = new DataColumn(CommonHelper.DateConverter(date), typeof(double));
                    column.DefaultValue = 0;
                    column.ExtendedProperties.Add("Start", date);
                    column.ExtendedProperties.Add("End", date.AddSeconds(86399));
                    model.Columns.Add(column);
                }
            }

            return model;
        }

        private void Calculate500302(DataTable dt, List<V_Elec> categories, string name) {
            var row = dt.NewRow();
            row[1] = name;
            for(var k = 2; k < dt.Columns.Count; k++) {
                var column = dt.Columns[k];
                var start = (DateTime)column.ExtendedProperties["Start"];
                var end = (DateTime)column.ExtendedProperties["End"];

                row[k] = categories.FindAll(c => c.StartTime >= start && c.EndTime <= end).Sum(c => c.Value);
            }
            dt.Rows.Add(row);
        }

        private List<Model500303> Get500303(string parent, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500303>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if(parent == "root") {
                #region root
                if(size == (int)EnmSSH.Area) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                        var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                        var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);

                        result.Add(new Model500303 {
                            index = ++index,
                            name = root.ToString(),
                            period = string.Format("{0} ~ {1}",CommonHelper.DateConverter(startDate),CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                } else if(size == (int)EnmSSH.Station) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                    foreach(var child in _workContext.Stations()) {
                        var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                        var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        result.Add(new Model500303 {
                            index = ++index,
                            name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                } else if(size == (int)EnmSSH.Room) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                    foreach(var child in _workContext.Rooms()) {
                        var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                        var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        result.Add(new Model500303 {
                            index = ++index,
                            name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(size == (int)EnmSSH.Area) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                        if(current.HasChildren) {
                            foreach(var root in current.ChildRoot) {
                                var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                                var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                                var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                                var currentValue = currentCategories.Sum(c => c.Value);
                                var lastValue = lastCategories.Sum(c => c.Value);

                                result.Add(new Model500303 {
                                    index = ++index,
                                    name = root.ToString(),
                                    period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                    current = currentValue,
                                    last = lastValue,
                                    increase = currentValue - lastValue,
                                    rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                                });
                            }
                        } else {
                            var children = _workContext.Stations().FindAll(s => s.Current.AreaId == current.Current.Id).Select(s => s.Current.Id);
                            var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                            var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);

                            result.Add(new Model500303 {
                                index = ++index,
                                name = current.ToString(),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    } else if(size == (int)EnmSSH.Station) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                        var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var child in children) {
                            var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                            var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            result.Add(new Model500303 {
                                index = ++index,
                                name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    } else if(size == (int)EnmSSH.Room) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate.AddYears(-1), endDate.AddYears(-1));
                        var children = _workContext.Rooms().FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach(var child in children) {
                            var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                            var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            result.Add(new Model500303 {
                                index = ++index,
                                name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model500304> Get500304(string parent, int size, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500304>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if(parent == "root") {
                #region root
                if(size == (int)EnmSSH.Area) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                        var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                        var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);

                        result.Add(new Model500304 {
                            index = ++index,
                            name = root.ToString(),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                } else if(size == (int)EnmSSH.Station) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                    foreach(var child in _workContext.Stations()) {
                        var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                        var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        result.Add(new Model500304 {
                            index = ++index,
                            name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                } else if(size == (int)EnmSSH.Room) {
                    var currentEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                    var lastEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                    foreach(var child in _workContext.Rooms()) {
                        var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                        var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                        var currentValue = currentCategories.Sum(c => c.Value);
                        var lastValue = lastCategories.Sum(c => c.Value);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        result.Add(new Model500304 {
                            index = ++index,
                            name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            current = currentValue,
                            last = lastValue,
                            increase = currentValue - lastValue,
                            rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                        });
                    }
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) {
                    if(size == (int)EnmSSH.Area) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                        if(current.HasChildren) {
                            foreach(var root in current.ChildRoot) {
                                var children = _workContext.Stations().FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                                var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                                var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                                var currentValue = currentCategories.Sum(c => c.Value);
                                var lastValue = lastCategories.Sum(c => c.Value);

                                result.Add(new Model500304 {
                                    index = ++index,
                                    name = root.ToString(),
                                    period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                    current = currentValue,
                                    last = lastValue,
                                    increase = currentValue - lastValue,
                                    rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                                });
                            }
                        } else {
                            var children = _workContext.Stations().FindAll(s => s.Current.AreaId == current.Current.Id).Select(s => s.Current.Id);
                            var currentCategories = currentEnergies.FindAll(e => children.Contains(e.Id));
                            var lastCategories = lastEnergies.FindAll(e => children.Contains(e.Id));
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);

                            result.Add(new Model500304 {
                                index = ++index,
                                name = current.ToString(),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    } else if(size == (int)EnmSSH.Station) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                        var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                        foreach(var child in children) {
                            var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                            var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            result.Add(new Model500304 {
                                index = ++index,
                                name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    } else if(size == (int)EnmSSH.Room) {
                        var currentEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate, endDate);
                        var lastEnergies = _elecService.GetEnergies(EnmSSH.Room, EnmFormula.ZL, startDate.AddMonths(-1), endDate.AddMonths(-1));
                        var children = _workContext.Rooms().FindAll(r => current.Keys.Contains(r.Current.AreaId));
                        foreach(var child in children) {
                            var currentCategories = currentEnergies.FindAll(e => e.Id == child.Current.Id);
                            var lastCategories = lastEnergies.FindAll(e => e.Id == child.Current.Id);
                            var currentValue = currentCategories.Sum(c => c.Value);
                            var lastValue = lastCategories.Sum(c => c.Value);
                            var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                            result.Add(new Model500304 {
                                index = ++index,
                                name = string.Format("{0},{1},{2}", area != null ? area.ToString() : "", child.Current.StationName, child.Current.Name),
                                period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                                current = currentValue,
                                last = lastValue,
                                increase = currentValue - lastValue,
                                rate = string.Format("{0:P2}", lastValue > 0 ? (currentValue - lastValue) / lastValue : 1)
                            });
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model500305> Get500305(string aid, string bid, int period, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500305>();

            var aStation = _workContext.Stations().Find(s => s.Current.Id == aid);
            if(aStation == null) return result;

            var bStation = _workContext.Stations().Find(s => s.Current.Id == bid);
            if(bStation == null) return result;

            var aEnergies = _elecService.GetEnergies(aStation.Current.Id, EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
            var bEnergies = _elecService.GetEnergies(bStation.Current.Id, EnmSSH.Station, EnmFormula.ZL, startDate, endDate);

            startDate = startDate.Date; endDate = endDate.Date;
            var dates = new List<DateTime>();
            while(startDate <= endDate) {
                dates.Add(startDate);
                startDate = startDate.AddDays(1);
            }

            var index = 0;
            if(period == (int)EnmPDH.Month) {
                dates = dates.GroupBy(d => new { d.Year, d.Month }).Select(g => new DateTime(g.Key.Year, g.Key.Month, 1)).ToList();
                foreach(var date in dates) {
                    var end = date.AddMonths(1).AddSeconds(-1);

                    var avalue = aEnergies.FindAll(a => a.StartTime >= date && a.EndTime <= end).Sum(e => e.Value);
                    var bvalue = bEnergies.FindAll(b => b.StartTime >= date && b.EndTime <= end).Sum(e => e.Value);
                    result.Add(new Model500305 {
                        index = ++index,
                        period = CommonHelper.MonthConverter(date),
                        Aname = aStation.Current.Name,
                        Bname = bStation.Current.Name,
                        Avalue = avalue,
                        Bvalue = bvalue,
                        increase = avalue-bvalue,
                        rate = string.Format("{0:P2}", bvalue > 0 ? (avalue - bvalue) / bvalue : 1)
                    });
                }
            } else if(period == (int)EnmPDH.Week) {
                dates = dates.GroupBy(d => d.Date.AddDays(-1 * (((int)d.DayOfWeek + 6) % 7))).Select(g => g.Key).ToList();
                foreach(var date in dates) {
                    var end = date.AddDays(6).AddSeconds(86399);

                    var avalue = aEnergies.FindAll(a => a.StartTime >= date && a.EndTime <= end).Sum(e => e.Value);
                    var bvalue = bEnergies.FindAll(b => b.StartTime >= date && b.EndTime <= end).Sum(e => e.Value);
                    result.Add(new Model500305 {
                        index = ++index,
                        period = CommonHelper.WeekConverter(date),
                        Aname = aStation.Current.Name,
                        Bname = bStation.Current.Name,
                        Avalue = avalue,
                        Bvalue = bvalue,
                        increase = avalue - bvalue,
                        rate = string.Format("{0:P2}", bvalue > 0 ? (avalue - bvalue) / bvalue : 1)
                    });
                }
            } else if(period == (int)EnmPDH.Day) {
                foreach(var date in dates) {
                    var end = date.AddSeconds(86399);

                    var avalue = aEnergies.FindAll(a => a.StartTime >= date && a.EndTime <= end).Sum(e => e.Value);
                    var bvalue = bEnergies.FindAll(b => b.StartTime >= date && b.EndTime <= end).Sum(e => e.Value);
                    result.Add(new Model500305 {
                        index = ++index,
                        period = CommonHelper.DateConverter(date),
                        Aname = aStation.Current.Name,
                        Bname = bStation.Current.Name,
                        Avalue = avalue,
                        Bvalue = bvalue,
                        increase = avalue - bvalue,
                        rate = string.Format("{0:P2}", bvalue > 0 ? (avalue - bvalue) / bvalue : 1)
                    });
                }
            }

            return result;
        }

        private List<Model500306> Get500306(string parent, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);

            var result = new List<Model500306>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var index = 0;
            if(parent == "root") {
                #region root
                var deviceEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.SB, startDate, endDate);
                var totalEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                foreach(var child in _workContext.Stations()) {
                    var deviceCategories = deviceEnergies.FindAll(e => e.Id == child.Current.Id);
                    var totalCategories = totalEnergies.FindAll(e => e.Id == child.Current.Id);
                    var deviceValue = deviceCategories.Sum(c => c.Value);
                    var totalValue = totalCategories.Sum(c => c.Value);
                    var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                    result.Add(new Model500306 {
                        index = ++index,
                        name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                        period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                        device = deviceValue,
                        total = totalValue,
                        pue = deviceValue > 0 ? Math.Round(totalValue / deviceValue, 2) : 0d,
                        eer = totalValue > 0 ? Math.Round(deviceValue / totalValue, 2) : 0d
                    });
                }
                #endregion
            } else {
                #region children
                var current = _workContext.Areas().Find(a => a.Current.Id == parent);
                if(current != null) {
                    var deviceEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.SB, startDate, endDate);
                    var totalEnergies = _elecService.GetEnergies(EnmSSH.Station, EnmFormula.ZL, startDate, endDate);
                    var children = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    foreach(var child in children) {
                        var deviceCategories = deviceEnergies.FindAll(e => e.Id == child.Current.Id);
                        var totalCategories = totalEnergies.FindAll(e => e.Id == child.Current.Id);
                        var deviceValue = deviceCategories.Sum(c => c.Value);
                        var totalValue = totalCategories.Sum(c => c.Value);
                        var area = _workContext.Areas().Find(a => a.Current.Id == child.Current.AreaId);

                        result.Add(new Model500306 {
                            index = ++index,
                            name = string.Format("{0},{1}", area != null ? area.ToString() : "", child.Current.Name),
                            period = string.Format("{0} ~ {1}", CommonHelper.DateConverter(startDate), CommonHelper.DateConverter(endDate)),
                            device = deviceValue,
                            total = totalValue,
                            pue = deviceValue > 0 ? Math.Round(totalValue / deviceValue, 2) : 0d,
                            eer = totalValue > 0 ? Math.Round(deviceValue / totalValue, 2) : 0d
                        });
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model500401> Get500401(string parent, int size, string[] types, DateTime startDate, DateTime endDate) {
            endDate = endDate.AddSeconds(86399);
            
            var result = new List<Model500401>();
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues();
            if(rtValues == null) return result;

            var devices = _workContext.Devices();
            if(types != null && types.Length > 0) 
                devices = devices.FindAll(d => types.Contains(d.Current.Type.Id));

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                foreach(var area in areas) {
                    var childDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                    var childDevIds = childDevices.Select(d => d.Current.Id);
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.whlHuLue);
                        foreach(var area in areas) {
                            var childDevices = devices.FindAll(d => area.Keys.Contains(d.Current.AreaId));
                            var childDevIds = childDevices.Select(d => d.Current.Id);
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

            var rtValues = _workContext.RtValues();
            if(rtValues == null) return result;

            var alarms = _hisAlarmService.GetAlarms(startDate, endDate).FindAll(a => a.EndTime.Subtract(a.StartTime).TotalMinutes > rtValues.jslHuLue);
            if(types != null && types.Length > 0) {
                var devMatchs = _workContext.Devices().FindAll(d => types.Contains(d.Current.Type.Id)).Select(d => d.Current.Id);
                alarms = alarms.FindAll(a => devMatchs.Contains(a.DeviceId));
            }

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
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
            if(string.IsNullOrWhiteSpace(parent)) return result;

            var rtValues = _workContext.RtValues();
            if(rtValues == null) return result;

            var alarms = _hisAlarmService.GetAlarms(startDate, endDate);
            if(levels != null && levels.Length > 0)
                alarms = alarms.FindAll(a => levels.Contains((int)a.AlarmLevel));

            var stores = _workContext.AlarmsToStore(alarms);

            var index = 0;
            if(parent == "root") {
                #region root
                var areas = _workContext.Areas().FindAll(a => a.Current.Type.Key == size);
                foreach(var area in areas) {
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
                if(current != null) {
                    if(current.HasChildren) {
                        #region children
                        var areas = current.Children.FindAll(a => a.Current.Type.Key == size);
                        foreach(var area in areas) {
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