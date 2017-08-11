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
using iPem.Site.Models.BInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class HomeController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IReservationService _reservationService;
        private readonly IDictionaryService _dictionaryService;
        private readonly INoticeService _noticeService;
        private readonly INoticeInUserService _noticeInUserService;
        private readonly IProfileService _profileService;
        private readonly IFollowPointService _followPointService;
        private readonly IProjectService _projectService;
        private readonly IAAlarmService _actAlarmService;
        private readonly IHAlarmService _hisAlarmService;
        private readonly IPointService _pointService;
        private readonly IFsuService _fsuService;
        private readonly IElecService _elecService;
        private readonly IAMeasureService _aMeasureService;
        private readonly IGroupService _groupService;
        private readonly ICutService _cutService;
        private readonly IPackMgr _packMgr;

        #endregion

        #region Ctor

        public HomeController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IReservationService reservationService,
            IDictionaryService dictionaryService,
            INoticeService noticeService,
            INoticeInUserService noticeInUserService,
            IProfileService profileService,
            IFollowPointService followPointService,
            IProjectService projectService,
            IAAlarmService actAlarmService,
            IHAlarmService hisAlarmService,
            IPointService pointService,
            IFsuService fsuService,
            IElecService elecService,
            IAMeasureService aMeasureService,
            IGroupService groupService,
            ICutService cutService,
            IPackMgr packMgr) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._reservationService = reservationService;
            this._dictionaryService = dictionaryService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._profileService = profileService;
            this._followPointService = followPointService;
            this._projectService = projectService;
            this._actAlarmService = actAlarmService;
            this._hisAlarmService = hisAlarmService;
            this._pointService = pointService;
            this._fsuService = fsuService;
            this._elecService = elecService;
            this._aMeasureService = aMeasureService;
            this._groupService = groupService;
            this._cutService = cutService;
            this._packMgr = packMgr;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            ViewBag.BarIndex = 0;
            return View();
        }

        public ActionResult ActiveData() {
            ViewBag.BarIndex = 1;
            ViewBag.MenuVisible = false;
            ViewBag.Control = _workContext.Authorizations.Permissions.Contains(EnmPermission.Control);
            ViewBag.Adjust = _workContext.Authorizations.Permissions.Contains(EnmPermission.Adjust);
            ViewBag.Threshold = _workContext.Authorizations.Permissions.Contains(EnmPermission.Threshold);
            return View();
        }

        public ActionResult ActiveAlarm() {
            ViewBag.BarIndex = 2;
            ViewBag.MenuVisible = false;
            ViewBag.Confirm = _workContext.Authorizations.Permissions.Contains(EnmPermission.Confirm);
            _workContext.LastNoticeTime = DateTime.Now;
            return View();
        }

        public ActionResult Notice() {
            ViewBag.BarIndex = 3;
            ViewBag.MenuVisible = true;
            return View();
        }

        public ActionResult UCenter() {
            ViewBag.BarIndex = 4;
            ViewBag.MenuVisible = true;
            ViewBag.Current = _workContext.User;
            return View();
        }

        public ActionResult Speech() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetBadges() {
            var data = new AjaxDataModel<BadgeModel> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new BadgeModel() { notices = 0, alarms = 0 }
            };

            try {
                data.data.notices = _noticeService.GetUnreadNotices(_workContext.User.Id).Count;
                data.data.alarms = _workContext.ActAlarms.FindAll(a => a.Current.CreatedTime >= _workContext.LastNoticeTime).Count;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetSpeech() {
            var data = new AjaxDataModel<List<string>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<string>()
            };

            try {
                var config = _workContext.TsValues;
                if(config == null) return Json(data, JsonRequestBehavior.AllowGet);

                if(config.bases == null || !config.bases.Contains(1))
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.levels == null || config.levels.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.contents == null || config.contents.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                var start = _workContext.LastSpeechTime; var end = DateTime.Now;
                var stores = _workContext.ActAlarms.FindAll(a => a.Current.CreatedTime >= start && a.Current.CreatedTime < end && config.levels.Contains((int)a.Current.AlarmLevel));
                if(config.bases.Contains(3))
                    stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));
                if(config.bases.Contains(4))
                    stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

                if(config.stationTypes != null && config.stationTypes.Length > 0)
                    stores = stores.FindAll(a => config.stationTypes.Contains(a.Station.Type.Id));

                if(config.roomTypes != null && config.roomTypes.Length > 0)
                    stores = stores.FindAll(a => config.roomTypes.Contains(a.Room.Type.Id));

                if(config.subDeviceTypes != null && config.subDeviceTypes.Length > 0)
                    stores = stores.FindAll(a => config.subDeviceTypes.Contains(a.Device.SubType.Id));

                if(config.subLogicTypes != null && config.subLogicTypes.Length > 0)
                    stores = stores.FindAll(a => config.subLogicTypes.Contains(a.Device.SubLogicType.Id));

                if (config.points != null && config.points.Length > 0)
                    stores = stores.FindAll(a => config.points.Contains(a.Point.Id));

                if(!string.IsNullOrWhiteSpace(config.keywords)) {
                    var keywords = Common.SplitCondition(config.keywords);
                    if(keywords.Length > 0) stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.Name, keywords));
                }

                foreach(var store in stores) {
                    var contents = new List<string>();
                    if(config.contents.Contains(1))
                        contents.Add(store.Area.Name);

                    if(config.contents.Contains(2))
                        contents.Add(store.Station.Name);

                    if(config.contents.Contains(3))
                        contents.Add(store.Room.Name);

                    if(config.contents.Contains(4))
                        contents.Add(store.Device.Name);

                    if(config.contents.Contains(5))
                        contents.Add(store.Point.Name);

                    if(config.contents.Contains(6))
                        contents.Add(CommonHelper.DateTimeConverter(store.Current.AlarmTime));

                    if(config.contents.Contains(7))
                        contents.Add(string.Format("发生{0}", Common.GetAlarmDisplay(store.Current.AlarmLevel)));

                    if(config.contents.Contains(8) && !string.IsNullOrWhiteSpace(store.Current.AlarmDesc))
                        contents.Add(store.Current.AlarmDesc);

                    data.data.Add(string.Join("，", contents));
                }

                if(!config.bases.Contains(2)) _workContext.LastSpeechTime = end;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult Speaker(string text) {
            try {
                if(string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("text");

                var bytes = CommonHelper.ConvertTextToSpeech(text);
                if(bytes == null) throw new iPemException("语音转换失败");
                return File(bytes, "audio/wav");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return File(new byte[] { }, "audio/wav");
            }
        }

        public ContentResult GetNavMenus() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var menus = _workContext.Authorizations.Menus;
                if(menus != null && menus.Count > 0) {
                    var roots = new List<U_Menu>();
                    foreach(var menu in menus) {
                        if(!menus.Any(m => m.Id == menu.LastId))
                            roots.Add(menu);
                    }

                    var _menus = roots.OrderBy(m => m.Index).ToList();
                    if(_menus.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = menus.Count;
                        for(var i = 0; i < _menus.Count; i++) {
                            var root = new TreeModel {
                                id = _menus[i].Id.ToString(),
                                text = _menus[i].Name,
                                href = string.IsNullOrWhiteSpace(_menus[i].Url) ? "javascript:void(0);" : string.Format("{0}/{1}", _menus[i].Url, _menus[i].Id),
                                icon = _menus[i].Icon,
                                expanded = false,
                                leaf = false
                            };

                            MenusRecursion(menus, _menus[i].Id, root);
                            data.data.Add(root);
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Content(JsonConvert.SerializeObject(data, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }), "application/json");
        }

        private void MenusRecursion(List<U_Menu> menus, int pid, TreeModel node) {
            var _menus = menus.FindAll(m => m.LastId == pid).OrderBy(m => m.Index).ToList();
            if(_menus.Count > 0) {
                node.data = new List<TreeModel>();
                for(var i = 0; i < _menus.Count; i++) {
                    var children = new TreeModel {
                        id = _menus[i].Id.ToString(),
                        text = _menus[i].Name,
                        href = string.IsNullOrWhiteSpace(_menus[i].Url) ? "javascript:void(0);" : string.Format("{0}/{1}", _menus[i].Url, _menus[i].Id),
                        icon = _menus[i].Icon,
                        expanded = false,
                        leaf = false
                    };

                    MenusRecursion(menus, _menus[i].Id, children);
                    node.data.Add(children);
                }
            } else {
                node.leaf = true;
            }
        }

        [AjaxAuthorize]
        public JsonResult GetNotices(int start, int limit, string listModel) {
            var data = new AjaxDataModel<List<NoticeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<NoticeModel>()
            };

            try {
                if(string.IsNullOrWhiteSpace(listModel))
                    throw new ArgumentException("listModel");

                var notices = _noticeService.GetNoticesInUser(_workContext.User.Id);
                var noticesInUser = _noticeInUserService.GetNoticesInUser(_workContext.User.Id);

                var models = from notice in notices
                             join niu in noticesInUser on notice.Id equals niu.NoticeId
                             where notice.Enabled
                             select new NoticeModel {
                                 id = notice.Id.ToString(),
                                 title = notice.Title,
                                 content = notice.Content,
                                 created = CommonHelper.DateTimeConverter(notice.CreatedTime),
                                 uid = niu.NoticeId.ToString(),
                                 readed = niu.Readed,
                                 readtime = CommonHelper.DateTimeConverter(niu.ReadTime)
                             };

                if("all".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    if(models.Any()) {
                        var result = new PagedList<NoticeModel>(models, start / limit, limit, models.Count());
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(result);
                        }
                    }
                } else if("readed".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var readed = models.Where(m => m.readed);
                    if(readed.Any()) {
                        var result = new PagedList<NoticeModel>(readed, start / limit, limit, readed.Count());
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(result);
                        }
                    }
                } else if("unread".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var unread = models.Where(m => !m.readed);
                    if(unread.Any()) {
                        var result = new PagedList<NoticeModel>(unread, start / limit, limit, unread.Count());
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(result);
                        }
                    }
                } else {
                    throw new ArgumentException("listModel");
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SetNotices(string[] notices, string status) {
            try {
                if (notices == null || notices.Length == 0)
                    throw new ArgumentException("notices");

                if (string.IsNullOrWhiteSpace(status))
                    throw new ArgumentException("status");

                if (!"readed".Equals(status, StringComparison.CurrentCultureIgnoreCase)
                    && !"unread".Equals(status, StringComparison.CurrentCultureIgnoreCase))
                    throw new ArgumentException("status");

                var result = new List<H_NoticeInUser>();
                var noticesInUser = _noticeInUserService.GetNoticesInUser(_workContext.User.Id);
                foreach (var notice in notices) {
                    var target = noticesInUser.Find(n => n.NoticeId == new Guid(notice));
                    if (target == null) continue;

                    target.Readed = "readed".Equals(status, StringComparison.CurrentCultureIgnoreCase);
                    target.ReadTime = target.Readed ? DateTime.Now : default(DateTime);
                    result.Add(target);
                }

                if (result.Count > 0) _noticeInUserService.Update(result.ToArray());
                return Json(new AjaxResultModel { success = true, code = 200, message = "Ok" }, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActAlarms(ActAlmCondition condition, bool onlyConfirms, bool onlyReservations,bool onlySystem , int start, int limit) {
            var data = new AjaxDataModel<List<ActAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>()
            };

            try {
                var stores = this.GetActAlmStore(condition, onlyConfirms, onlyReservations, onlySystem);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.AlarmTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.AlarmTime),
                            comment = stores[i].Current.AlarmDesc,
                            value = stores[i].Current.AlarmValue.ToString(),
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

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadActAlms(ActAlmCondition condition, bool onlyConfirms, bool onlyReservations, bool onlySystem) {
            try {
                var models = new List<ActAlmModel>();
                var stores = this.GetActAlmStore(condition, onlyConfirms, onlyReservations, onlySystem);
                if (stores != null) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new ActAlmModel {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.AlarmTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.AlarmTime),
                            comment = stores[i].Current.AlarmDesc,
                            value = stores[i].Current.AlarmValue.ToString(),
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
                

                using(var ms = _excelManager.Export<ActAlmModel>(models, "实时告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestRecoveries(ActAlmCondition condition, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                var stores = this.GetRecoveryStore(condition);
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
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRecoveries(ActAlmCondition condition) {
            try {
                var models = new List<HisAlmModel>();
                var stores = this.GetRecoveryStore(condition);
                if (stores != null && stores.Count > 0) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new HisAlmModel {
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

                using (var ms = _excelManager.Export<HisAlmModel>(models, "恢复告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActAlmDetail(string id, string title, bool primary, bool related, bool filter, int start, int limit) {
            var data = new AjaxDataModel<List<ActAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>()
            };

            try {
                if (string.IsNullOrWhiteSpace(id))  
                    throw new ArgumentException("id");
                
                List<AlmStore<A_AAlarm>> stores = null;
                if (primary)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetPrimaryAlarms(id));
                else if(related)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetRelatedAlarms(id));
                else if(filter)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetFilterAlarms(id));

                if (stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.AlarmTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.AlarmTime),
                            comment = stores[i].Current.AlarmDesc,
                            value = stores[i].Current.AlarmValue.ToString(),
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
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadActAlmDetail(string id, string title, bool primary, bool related, bool filter) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                List<AlmStore<A_AAlarm>> stores = null;
                if (primary)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetPrimaryAlarms(id));
                else if (related)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetRelatedAlarms(id));
                else if (filter)
                    stores = _workContext.AlarmsToStore(_actAlarmService.GetFilterAlarms(id));

                var models = new List<ActAlmModel>();
                if (stores != null) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new ActAlmModel {
                            index = i + 1,
                            nmalarmid = stores[i].Current.NMAlarmId,
                            level = Common.GetAlarmDisplay(stores[i].Current.AlarmLevel),
                            time = CommonHelper.DateTimeConverter(stores[i].Current.AlarmTime),
                            interval = CommonHelper.IntervalConverter(stores[i].Current.AlarmTime),
                            comment = stores[i].Current.AlarmDesc,
                            value = stores[i].Current.AlarmValue.ToString(),
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

                using (var ms = _excelManager.Export<ActAlmModel>(models, title ?? "告警详单", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestHisAlmDetail(string id, string title, bool reversal, int start, int limit) {
            var data = new AjaxDataModel<List<HisAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HisAlmModel>()
            };

            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                List<AlmStore<A_HAlarm>> stores = null;
                if (reversal) stores = _workContext.AlarmsToStore(_hisAlarmService.GetReversalAlarms(id, DateTime.Now.AddDays(-1), DateTime.Now));

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
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHisAlmDetail(string id, string title, bool reversal) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                List<AlmStore<A_HAlarm>> stores = null;
                if (reversal) stores = _workContext.AlarmsToStore(_hisAlarmService.GetReversalAlarms(id, DateTime.Now.AddDays(-1), DateTime.Now));

                var models = new List<HisAlmModel>();
                if (stores != null && stores.Count > 0) {
                    for (int i = 0; i < stores.Count; i++) {
                        models.Add(new HisAlmModel {
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

                using (var ms = _excelManager.Export<HisAlmModel>(models, title ?? "告警详单", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestActPoints(string node, bool normal, int[] types, int start, int limit) {
            var data = new AjaxDataModel<List<ActPointModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActPointModel>()
            };

            try {
                if(types != null && types.Length > 0) {
                    var stores = this.GetActPoints(node, types);
                    if(stores.Count > 0) {
                        var nodeKey = Common.ParseNode(node);
                        var models = new List<ActPointModel>();

                        #region 标准信号

                        var ptPoints = stores.FindAll(p => p.Type != EnmPoint.AL);
                        if (ptPoints.Count > 0) {
                            List<V_AMeasure> values;
                            if (nodeKey.Id == EnmSSH.Device) {
                                #region 通知获取数据
                                var pointsInFsus = ptPoints.GroupBy(p => p.Device.FsuId);
                                foreach (var pointsInFsu in pointsInFsus) {
                                    try {
                                        var curFsu = _workContext.Fsus.Find(f => f.Current.Id == pointsInFsu.Key);
                                        if (curFsu == null) continue;

                                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                                        if (curExtFsu == null) continue;
                                        if (!curExtFsu.Status) continue;

                                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                                        if (curGroup == null) continue;
                                        if (!curGroup.Status) continue;

                                        var devices = pointsInFsu.GroupBy(p => p.Device.Code);
                                        var package = new GetDataPackage {
                                            FsuId = curFsu.Current.Code,
                                            DeviceList = devices.Select(d => new GetDataDevice { Id = d.Key }).ToList()
                                        };

                                        _packMgr.GetData(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues != null && _workContext.WsValues.fsuPath != null) ? _workContext.WsValues.fsuPath : "").ToString(), package);
                                    } catch { }
                                }
                                #endregion
                                values = _aMeasureService.GetMeasuresInDevice(nodeKey.Value);
                            } else {
                                values = _aMeasureService.GetMeasures(ptPoints.Select(p => new IdValuePair<string, string>(p.Device.Id, p.Current.Id)).ToList());
                            }

                            var pValues = from point in ptPoints
                                          join val in values on new { DeviceId = point.Device.Id, PointId = point.Current.Id } equals new { val.DeviceId, val.PointId } into lt
                                          from def in lt.DefaultIfEmpty()
                                          select new { Point = point, Value = def };

                            foreach (var pv in pValues) {
                                var value = pv.Value != null && pv.Value.Value != double.MinValue ? pv.Value.Value.ToString() : "NULL";
                                var status = pv.Value != null ? pv.Value.Status : EnmState.Invalid;
                                var time = pv.Value != null ? pv.Value.UpdateTime : DateTime.Now;

                                models.Add(new ActPointModel {
                                    area = pv.Point.Area.Name,
                                    station = pv.Point.Device.StationName,
                                    room = pv.Point.Device.RoomName,
                                    device = pv.Point.Device.Name,
                                    point = pv.Point.Current.Name,
                                    type = Common.GetPointTypeDisplay(pv.Point.Type),
                                    value = value,
                                    unit = Common.GetUnitDisplay(pv.Point.Current.Type, value, pv.Point.Current.UnitState),
                                    status = Common.GetPointStatusDisplay(status),
                                    time = CommonHelper.DateTimeConverter(time),
                                    deviceid = pv.Point.Device.Id,
                                    pointid = pv.Point.Current.Id,
                                    typeid = (int)pv.Point.Type,
                                    statusid = (int)status,
                                    followed = pv.Point.Followed,
                                    followedOnly = pv.Point.FollowedOnly,
                                    timestamp = CommonHelper.ShortTimeConverter(time)
                                });
                            }
                        }

                        #endregion

                        #region 告警信号

                        var alPoints = stores.FindAll(p => p.Type == EnmPoint.AL);
                        if (alPoints.Count > 0) {
                            var almKeys = _workContext.AlarmsToDictionary(_workContext.ActAlarms, false);
                            foreach (var point in alPoints) {
                                var model = new ActPointModel {
                                    area = point.Area.Name,
                                    station = point.Device.StationName,
                                    room = point.Device.RoomName,
                                    device = point.Device.Name,
                                    point = point.Current.Name,
                                    type = Common.GetPointTypeDisplay(point.Type),
                                    value = "0",
                                    unit = "正常",
                                    status = Common.GetPointStatusDisplay(EnmState.Normal),
                                    time = CommonHelper.DateTimeConverter(DateTime.Now),
                                    deviceid = point.Device.Id,
                                    pointid = point.Current.Id,
                                    typeid = (int)point.Type,
                                    statusid = (int)EnmState.Normal,
                                    followed = point.Followed,
                                    followedOnly = point.FollowedOnly,
                                    timestamp = CommonHelper.ShortTimeConverter(DateTime.Now)
                                };

                                var key = Common.JoinKeys(point.Device.Id, point.Current.Id);
                                if (almKeys.ContainsKey(key)) {
                                    var alarm = almKeys[key];
                                    var status = Common.LevelToState(alarm.Current.AlarmLevel);
                                    var time = alarm.Current.AlarmTime;

                                    model.value = "1";
                                    model.unit = "告警";
                                    model.status = Common.GetPointStatusDisplay(status);
                                    model.statusid = (int)status;
                                    model.time = CommonHelper.DateTimeConverter(time);
                                    model.timestamp = CommonHelper.ShortTimeConverter(time);
                                }

                                models.Add(model);
                            }
                        }

                        #endregion

                        if (normal) models = models.FindAll(m => m.statusid != (int)EnmState.Invalid);
                        var end = start + limit;
                        if (end > models.Count)
                            end = models.Count;

                        data.message = "200 Ok";
                        data.total = models.Count;

                        for (var i = start; i < end; i++) {
                            var model = models[i];
                            model.index = i + 1;
                            data.data.Add(model);
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
        public JsonResult GetSeniorCondition(string id, int action) {
            var data = new AjaxDataModel<SeniorCondition> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new SeniorCondition {
                    id = Guid.NewGuid().ToString(),
                    name = string.Empty,
                    stationTypes = null,
                    roomTypes = null,
                    subDeviceTypes = null,
                    subLogicTypes = null,
                    points = null,
                    levels = null,
                    confirms = null,
                    reservations = null,
                    keywords = string.Empty
                }
            };

            try {
                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                if (id == "root")
                    throw new iPemException("无法编辑根节点");

                var profile = _profileService.GetProfile(_workContext.User.Id);
                if (profile == null || string.IsNullOrWhiteSpace(profile.ValuesJson))
                    throw new iPemException("未找到数据对象");

                var settings = JsonConvert.DeserializeObject<Setting>(profile.ValuesJson);
                if (settings.SeniorConditions == null || settings.SeniorConditions.Count == 0)
                    throw new iPemException("未找到数据对象");

                var current = settings.SeniorConditions.Find(c => c.id == id);
                if (current == null) throw new iPemException("未找到数据对象");

                data.data.id = current.id;
                data.data.name = current.name;
                data.data.stationTypes = current.stationTypes;
                data.data.roomTypes = current.roomTypes;
                data.data.subDeviceTypes = current.subDeviceTypes;
                data.data.subLogicTypes = current.subLogicTypes;
                data.data.points = current.points;
                data.data.levels = current.levels;
                data.data.confirms = current.confirms;
                data.data.reservations = current.reservations;
                data.data.keywords = current.keywords;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveSeniorCondition(SeniorCondition condition, int action) {

            try {
                if (condition == null) throw new ArgumentException("condition");
                if (condition.id == "root") throw new iPemException("无法保存根节点");
                if (action == (int)EnmAction.Add) {
                    var profile = _profileService.GetProfile(_workContext.User.Id);
                    if (profile == null || string.IsNullOrWhiteSpace(profile.ValuesJson)) {
                        profile = new U_Profile {
                            UserId = _workContext.User.Id,
                            ValuesJson = JsonConvert.SerializeObject(new Setting { SeniorConditions = new List<SeniorCondition> { condition } }),
                            LastUpdatedDate = DateTime.Now
                        };
                    } else {
                        var settings = JsonConvert.DeserializeObject<Setting>(profile.ValuesJson);
                        if (settings.SeniorConditions == null) settings.SeniorConditions = new List<SeniorCondition>();
                        if (settings.SeniorConditions.Any(c => c.name == condition.name))
                            throw new iPemException("条件名称已存在");

                        settings.SeniorConditions.Add(condition);
                        profile.ValuesJson = JsonConvert.SerializeObject(settings);
                        profile.LastUpdatedDate = DateTime.Now;
                    }

                    _profileService.Save(profile);
                    _workContext.ResetProfile();
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增告警条件[{0}]", condition.name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var profile = _profileService.GetProfile(_workContext.User.Id);
                    if (profile == null || string.IsNullOrWhiteSpace(profile.ValuesJson))
                        throw new iPemException("未找到数据对象");

                    var settings = JsonConvert.DeserializeObject<Setting>(profile.ValuesJson);
                    if (settings.SeniorConditions == null || settings.SeniorConditions.Count == 0)
                        throw new iPemException("未找到数据对象");

                    var count = settings.SeniorConditions.RemoveAll(c => c.id == condition.id);
                    if (count == 0) throw new iPemException("未找到数据对象");

                    settings.SeniorConditions.Add(condition);
                    profile.ValuesJson = JsonConvert.SerializeObject(settings);
                    profile.LastUpdatedDate = DateTime.Now;

                    _profileService.Save(profile);
                    _workContext.ResetProfile();
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新告警条件[{0}]", condition.name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteSeniorCondition(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                if (id == "root") throw new iPemException("无法删除根节点");

                var profile = _profileService.GetProfile(_workContext.User.Id);
                if (profile == null || string.IsNullOrWhiteSpace(profile.ValuesJson))
                    throw new iPemException("未找到数据对象");

                var settings = JsonConvert.DeserializeObject<Setting>(profile.ValuesJson);
                if (settings.SeniorConditions == null || settings.SeniorConditions.Count == 0)
                    throw new iPemException("未找到数据对象");

                var current = settings.SeniorConditions.Find(c => c.id == id);
                if (current == null) throw new iPemException("未找到数据对象");

                settings.SeniorConditions.Remove(current);
                profile.ValuesJson = JsonConvert.SerializeObject(settings);
                profile.LastUpdatedDate = DateTime.Now;

                _profileService.Save(profile);
                _workContext.ResetProfile();
                _webLogger.Information(EnmEventType.Operating, string.Format("删除告警条件[{0}]", current.name), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ConfirmAlarms(string[] keys) {
            try {
                if (!_workContext.Authorizations.Permissions.Contains(EnmPermission.Confirm))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(keys == null || keys.Length == 0)
                    throw new ArgumentException("keys");

                var entities = new List<A_AAlarm>();
                foreach(var key in keys) {
                    entities.Add(new A_AAlarm {
                        Id = key,
                        Confirmed = EnmConfirm.Confirmed,
                        ConfirmedTime = DateTime.Now,
                        Confirmer = _workContext.Employee != null ? _workContext.Employee.Name : _workContext.User.Uid
                    });
                }

                _actAlarmService.Confirm(entities.ToArray());
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警确认成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ConfirmAllAlarms(bool onlyReservation, bool onlySystem) {
            try {
                if (!_workContext.Authorizations.Permissions.Contains(EnmPermission.Confirm))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                var entities = new List<A_AAlarm>();
                foreach (var alarm in _workContext.ActAlarms) {
                    if (onlyReservation && string.IsNullOrWhiteSpace(alarm.Current.ReservationId)) continue;
                    if (onlySystem && alarm.Room != null) continue;

                    entities.Add(new A_AAlarm {
                        Id = alarm.Current.Id,
                        Confirmed = EnmConfirm.Confirmed,
                        ConfirmedTime = DateTime.Now,
                        Confirmer = _workContext.Employee != null ? _workContext.Employee.Name : _workContext.User.Uid
                    });
                }

                _actAlarmService.Confirm(entities.ToArray());
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警确认成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult GetReservation(string id) {
            var data = new AjaxDataModel<ReservationModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new ReservationModel()
            };

            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                var current = _reservationService.GetReservation(id);
                if(current == null) throw new iPemException("未找到数据对象");
                var project = _projectService.GetProject(current.ProjectId);

                data.data.id = current.Id;
                data.data.name = current.Name;
                data.data.startDate = CommonHelper.DateTimeConverter(current.StartTime);
                data.data.endDate = CommonHelper.DateTimeConverter(current.EndTime);
                data.data.projectId = current.ProjectId;
                data.data.projectName = project != null ? project.Name : "";
                data.data.creator = current.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(current.CreatedTime);
                data.data.comment = current.Comment;
                data.data.enabled = current.Enabled;
                return Json(data);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ControlPoint(string device, string point, int ctrl) {
            try {
                if(!_workContext.Authorizations.Permissions.Contains(EnmPermission.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device)) throw new ArgumentException("device");
                if(string.IsNullOrWhiteSpace(point)) throw new ArgumentException("point");

                var curDevice = _workContext.Devices.Find(d => d.Current.Id == device);
                if(curDevice == null) throw new iPemException("未找到设备");

                var curFsu = _workContext.Fsus.Find(f => f.Current.Id == curDevice.Current.FsuId);
                if(curFsu == null) throw new iPemException("未找到Fsu");

                var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                if(curExtFsu == null) throw new iPemException("未找到Fsu");
                if (!curExtFsu.Status) throw new iPemException("Fsu通信中断");

                var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                if (curGroup == null) throw new iPemException("未找到SC采集组");
                if (!curGroup.Status) throw new iPemException("SC通信中断");

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                var package = new SetPointPackage() {
                    FsuId = curFsu.Current.Code,
                    DeviceList = new List<SetPointDevice>() {
                        new SetPointDevice() {
                            Id = curDevice.Current.Code,
                            Values = new List<TSemaphore>() {
                                new TSemaphore() {
                                    Id = curPoint.Code,
                                    SignalNumber = curPoint.Number,
                                    Type = EnmBIPoint.DO,
                                    MeasuredVal = "NULL",
                                    SetupVal = ctrl.ToString(),
                                    Status = EnmBIState.NOALARM,
                                    Time = DateTime.Now
                                }
                            }
                        }
                    }
                };


                var result = _packMgr.SetPoint(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues != null && _workContext.WsValues.fsuPath != null) ? _workContext.WsValues.fsuPath : "").ToString(), package);
                if(result != null) {
                    if (result.Result == EnmBIResult.FAILURE) throw new iPemException(result.FailureCause ?? "参数设置失败");
                    if(result.DeviceList != null) {
                        var devResult = result.DeviceList.Find(d => d.Id == curDevice.Current.Code);
                        if(devResult != null && devResult.SuccessList.Any(s => s.Id == curPoint.Code && s.SignalNumber == curPoint.Number))
                            return Json(new AjaxResultModel { success = true, code = 200, message = "参数设置成功" });
                    }
                }

                throw new iPemException("参数设置失败");
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult AdjustPoint(string device, string point, float adjust) {
            try {
                if(!_workContext.Authorizations.Permissions.Contains(EnmPermission.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device)) throw new ArgumentException("device");
                if(string.IsNullOrWhiteSpace(point)) throw new ArgumentException("point");

                var curDevice = _workContext.Devices.Find(d => d.Current.Id == device);
                if(curDevice == null) throw new iPemException("未找到设备");

                var curFsu = _workContext.Fsus.Find(f => f.Current.Id == curDevice.Current.FsuId);
                if(curFsu == null) throw new iPemException("未找到Fsu");

                var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                if(curExtFsu == null) throw new iPemException("未找到Fsu");
                if(!curExtFsu.Status) throw new iPemException("Fsu通信中断");

                var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                if (curGroup == null) throw new iPemException("未找到SC采集组");
                if (!curGroup.Status) throw new iPemException("SC通信中断");

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                var package = new SetPointPackage() {
                    FsuId = curFsu.Current.Code,
                    DeviceList = new List<SetPointDevice>() {
                        new SetPointDevice() {
                            Id = curDevice.Current.Code,
                            Values = new List<TSemaphore>() {
                                new TSemaphore() {
                                    Id = curPoint.Code,
                                    SignalNumber = curPoint.Number,
                                    Type = EnmBIPoint.AO,
                                    MeasuredVal = "NULL",
                                    SetupVal = adjust.ToString(),
                                    Status = EnmBIState.NOALARM,
                                    Time = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = _packMgr.SetPoint(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues != null && _workContext.WsValues.fsuPath != null) ? _workContext.WsValues.fsuPath : "").ToString(), package);
                if(result != null) {
                    if (result.Result == EnmBIResult.FAILURE) throw new iPemException(result.FailureCause ?? "参数设置失败");
                    if(result.DeviceList != null) {
                        var devResult = result.DeviceList.Find(d => d.Id == curDevice.Current.Code);
                        if(devResult != null && devResult.SuccessList.Any(s => s.Id == curPoint.Code && s.SignalNumber == curPoint.Number))
                            return Json(new AjaxResultModel { success = true, code = 200, message = "参数设置成功" });
                    }
                }

                throw new iPemException("参数设置失败");
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult AddFollowPoint(string device, string point) {
            try {
                if (!_workContext.Profile.FollowPoints.Any(p => p.DeviceId == device && p.PointId == point)) {
                    var follow = new U_FollowPoint { DeviceId = device, PointId = point, UserId = _workContext.User.Id };
                    _workContext.Profile.FollowPoints.Add(follow);
                    _followPointService.Add(follow);

                    var key = string.Format(GlobalCacheKeys.User_Cache_FollowPoints, _workContext.User.Id);
                    if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "关注成功" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult RemoveFollowPoint(string device, string point) {
            try {
                var current = _workContext.Profile.FollowPoints.Find(p => p.DeviceId == device && p.PointId == point);
                if(current != null) {
                    _workContext.Profile.FollowPoints.Remove(current);
                    _followPointService.Remove(current);

                    var key = string.Format(GlobalCacheKeys.User_Cache_FollowPoints, _workContext.User.Id);
                    if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "已取消关注" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeAlm() {
            var data = new AjaxDataModel<HomeAlmModel> {
                success = true,
                message = "无数据",
                total = 0,
                data = null
            };

            try {
                var model = new HomeAlmModel();
                model.total1 = _workContext.ActAlarms.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
                model.total2 = _workContext.ActAlarms.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
                model.total3 = _workContext.ActAlarms.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
                model.total4 = _workContext.ActAlarms.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);
                model.total = model.total1 + model.total2 + model.total3 + model.total4;
                model.alarms = new List<HomeAreaAlmModel>();

                var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                foreach(var root in roots) {
                    var alarmsInRoot = _workContext.ActAlarms.FindAll(alarm => root.Keys.Contains(alarm.Area.Id));

                    var alarmsInArea = new HomeAreaAlmModel();
                    alarmsInArea.name = root.Current.Name;
                    alarmsInArea.level1 = alarmsInRoot.Count(a => a.Current.AlarmLevel == EnmAlarm.Level1);
                    alarmsInArea.level2 = alarmsInRoot.Count(a => a.Current.AlarmLevel == EnmAlarm.Level2);
                    alarmsInArea.level3 = alarmsInRoot.Count(a => a.Current.AlarmLevel == EnmAlarm.Level3);
                    alarmsInArea.level4 = alarmsInRoot.Count(a => a.Current.AlarmLevel == EnmAlarm.Level4);
                    alarmsInArea.total = alarmsInArea.level1 + alarmsInArea.level2 + alarmsInArea.level3 + alarmsInArea.level4;
                    model.alarms.Add(alarmsInArea);
                }

                data.data = model;
                data.total = 1;
                data.message = "200 Ok";
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeSvr() {
            var data = new AjaxDataModel<HomeServerModel> {
                success = true,
                message = "无数据",
                total = 0,
                data = null
            };

            try {
                var model = new HomeServerModel();
                model.cpu = CommonHelper.GetCpuUsage();
                model.memory = CommonHelper.GetMemoryUsage();
                model.time = CommonHelper.ShortTimeConverter(DateTime.Now);
                data.data = model;
                data.total = 1;
                data.message = "200 Ok";
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeEnergies() {
            var data = new AjaxDataModel<List<HomeEnergyModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomeEnergyModel>()
            };

            try {
                var energies = _elecService.GetEnergies(EnmSSH.Station, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Now);
                var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                foreach(var root in roots) {
                    var children = _workContext.Stations.FindAll(s => root.Keys.Contains(s.Current.AreaId)).Select(s => s.Current.Id);
                    var categories = energies.FindAll(e => children.Contains(e.Id));

                    data.data.Add(new HomeEnergyModel {
                        name = root.ToString(),
                        kt = categories.FindAll(c => c.FormulaType == EnmFormula.KT).Sum(c => c.Value),
                        zm = categories.FindAll(c => c.FormulaType == EnmFormula.ZM).Sum(c => c.Value),
                        bg = categories.FindAll(c => c.FormulaType == EnmFormula.BG).Sum(c => c.Value),
                        sb = categories.FindAll(c => c.FormulaType == EnmFormula.SB).Sum(c => c.Value),
                        kgdy = categories.FindAll(c => c.FormulaType == EnmFormula.KGDY).Sum(c => c.Value),
                        ups = categories.FindAll(c => c.FormulaType == EnmFormula.UPS).Sum(c => c.Value),
                        qt = categories.FindAll(c => c.FormulaType == EnmFormula.QT).Sum(c => c.Value)
                    });
                }

                data.total = data.data.Count;
                data.message = "200 Ok";
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeOff(int start, int limit) {
            var data = new AjaxChartModel<List<HomeOffModel>, ChartModel[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomeOffModel>(),
                chart = new ChartModel[2]
            };

            try {
                var extFsus = _fsuService.GetExtFsus();
                var allFsus = from fsu in _workContext.Fsus
                              join ext in extFsus on fsu.Current.Id equals ext.Id
                              join area in _workContext.Areas on fsu.Current.AreaId equals area.Current.Id
                              select new { Area = area, Fsu = fsu, LastTime = ext.LastTime, Status = ext.Status };

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = allFsus.Count(f => f.Status) };
                data.chart[1] = new ChartModel { index = 2, name = "离线", value = allFsus.Count(f => !f.Status) };

                var offFsus = allFsus.Where(f => !f.Status).OrderByDescending(f => f.LastTime).ToList();
                if(offFsus.Count > 0) {
                    data.message = "200 Ok";
                    data.total = offFsus.Count;

                    var end = start + limit;
                    if(end > offFsus.Count)
                        end = offFsus.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new HomeOffModel {
                            index = i + 1,
                            area = offFsus[i].Area.ToString(),
                            station = offFsus[i].Fsu.Current.StationName,
                            room = offFsus[i].Fsu.Current.RoomName,
                            vendor = offFsus[i].Fsu.Current.VendorName,
                            name = offFsus[i].Fsu.Current.Name,
                            time = CommonHelper.DateTimeConverter(offFsus[i].LastTime),
                            interval = CommonHelper.IntervalConverter(offFsus[i].LastTime)
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
        public ActionResult DownloadHomeOff() {
            try {
                var extFsus = _fsuService.GetExtFsus();
                var allFsus = from fsu in _workContext.Fsus
                              join ext in extFsus on fsu.Current.Id equals ext.Id
                              join area in _workContext.Areas on fsu.Current.AreaId equals area.Current.Id
                              select new { Area = area, Fsu = fsu, LastTime = ext.LastTime, Status = ext.Status };

                var offFsus = allFsus.Where(f => !f.Status).OrderByDescending(f => f.LastTime).ToList();
                var models = new List<HomeOffModel>();
                for(int i = 0; i < offFsus.Count; i++) {
                    models.Add(new HomeOffModel {
                        index = i + 1,
                        area = offFsus[i].Area.ToString(),
                        station = offFsus[i].Fsu.Current.StationName,
                        room = offFsus[i].Fsu.Current.RoomName,
                        vendor = offFsus[i].Fsu.Current.VendorName,
                        name = offFsus[i].Fsu.Current.Name,
                        time = CommonHelper.DateTimeConverter(offFsus[i].LastTime),
                        interval = CommonHelper.IntervalConverter(offFsus[i].LastTime)
                    });
                }

                using(var ms = _excelManager.Export<HomeOffModel>(models, "Fsu离线列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeUnconnected(int start, int limit) {
            var data = new AjaxChartModel<List<HomeUnconnectedModel>, ChartModel[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomeUnconnectedModel>(),
                chart = new ChartModel[2]
            };

            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Off).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomeUnconnectedModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomeUnconnectedModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = _workContext.Stations.Count - unStations.Count };
                data.chart[1] = new ChartModel { index = 2, name = "断站", value = unStations.Count };

                if (unStations.Count > 0) {
                    data.message = "200 Ok";
                    data.total = unStations.Count;

                    var end = start + limit;
                    if (end > unStations.Count)
                        end = unStations.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new HomeUnconnectedModel {
                            index = i + 1,
                            area = unStations[i].area,
                            station = unStations[i].station,
                            time = unStations[i].time,
                            interval = unStations[i].interval
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
        public ActionResult DownloadHomeUnconnected() {
            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Off).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomeUnconnectedModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomeUnconnectedModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                using (var ms = _excelManager.Export<HomeUnconnectedModel>(unStations, "站点断站列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHomeCutting(int start, int limit) {
            var data = new AjaxChartModel<List<HomeCuttingModel>, ChartModel[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomeCuttingModel>(),
                chart = new ChartModel[2]
            };

            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Cut).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomeCuttingModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomeCuttingModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = _workContext.Stations.Count - unStations.Count };
                data.chart[1] = new ChartModel { index = 2, name = "停电", value = unStations.Count };

                if (unStations.Count > 0) {
                    data.message = "200 Ok";
                    data.total = unStations.Count;

                    var end = start + limit;
                    if (end > unStations.Count)
                        end = unStations.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new HomeCuttingModel {
                            index = i + 1,
                            area = unStations[i].area,
                            station = unStations[i].station,
                            time = unStations[i].time,
                            interval = unStations[i].interval
                        });
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
        public ActionResult DownloadHomeCutting() {
            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Cut).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomeCuttingModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomeCuttingModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                using (var ms = _excelManager.Export<HomeCuttingModel>(unStations, "站点停电列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHomePower(int start, int limit) {
            var data = new AjaxChartModel<List<HomePowerModel>, ChartModel[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomePowerModel>(),
                chart = new ChartModel[2]
            };

            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Power).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomePowerModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomePowerModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = _workContext.Stations.Count - unStations.Count };
                data.chart[1] = new ChartModel { index = 2, name = "发电", value = unStations.Count };

                if (unStations.Count > 0) {
                    data.message = "200 Ok";
                    data.total = unStations.Count;

                    var end = start + limit;
                    if (end > unStations.Count)
                        end = unStations.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new HomePowerModel {
                            index = i + 1,
                            area = unStations[i].area,
                            station = unStations[i].station,
                            time = unStations[i].time,
                            interval = unStations[i].interval
                        });
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
        public ActionResult DownloadHomePower() {
            try {
                var cuttings = _cutService.GetCuttings(EnmCutType.Power).GroupBy(c => new { c.AreaId, c.StationId });
                var unStations = new List<HomePowerModel>();
                if (cuttings.Any()) {
                    unStations = (from cut in cuttings
                                  join sta in _workContext.Stations on cut.Key.StationId equals sta.Current.Id
                                  join area in _workContext.Areas on cut.Key.AreaId equals area.Current.Id
                                  select new HomePowerModel {
                                      area = area.ToString(),
                                      station = sta.Current.Name,
                                      time = CommonHelper.DateTimeConverter(cut.Min(c => c.StartTime)),
                                      interval = CommonHelper.IntervalConverter(cut.Min(c => c.StartTime))
                                  }).ToList();
                }

                using (var ms = _excelManager.Export<HomePowerModel>(unStations, "站点发电列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonNetResult CreateMatrixTemplate(int index) {
            var defaultDevType = _workContext.DeviceTypes.FirstOrDefault();
            var template = new MatrixTemplate {
                id = CommonHelper.GetIdAsString(),
                name = string.Format("新建模版({0})", index),
                type = defaultDevType != null ? defaultDevType.Id : "0",
                points = new string[0]
            };

            return new JsonNetResult {
                Data = new AjaxDataModel<TreeCustomModel<MatrixTemplate>> {
                    success = true,
                    message = "200 Ok",
                    total = 1,
                    data = new TreeCustomModel<MatrixTemplate> {
                        id = template.id,
                        text = template.name,
                        icon = Icons.All,
                        leaf = true,
                        custom = template
                    }
                },
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveMatrixTemplates(MatrixTemplate[] templates) {
            try {
                if (templates == null) throw new ArgumentException("templates");
                if (templates.Length == 0) throw new ArgumentException("未配置模版，无需保存。");

                var profile = _profileService.GetProfile(_workContext.User.Id);
                if (profile == null || string.IsNullOrWhiteSpace(profile.ValuesJson)) {
                    profile = new U_Profile {
                        UserId = _workContext.User.Id,
                        ValuesJson = JsonConvert.SerializeObject(new Setting { MatrixTemplates = new List<MatrixTemplate>(templates) }),
                        LastUpdatedDate = DateTime.Now
                    };
                } else {
                    var settings = JsonConvert.DeserializeObject<Setting>(profile.ValuesJson);
                    settings.MatrixTemplates = new List<MatrixTemplate>(templates);
                    profile.ValuesJson = JsonConvert.SerializeObject(settings);
                    profile.LastUpdatedDate = DateTime.Now;
                }

                _profileService.Save(profile);
                _workContext.ResetProfile();
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<AlmStore<A_AAlarm>> GetActAlmStore(ActAlmCondition condition, bool onlyConfirms = false, bool onlyReservations = false, bool onlySystem = false) {
            if (condition.stationTypes == null) condition.stationTypes = new string[0];
            if (condition.roomTypes == null) condition.roomTypes = new string[0];
            if (condition.subDeviceTypes == null) condition.subDeviceTypes = new string[0];
            if (condition.subLogicTypes == null) condition.subLogicTypes = new string[0];
            if (condition.points == null) condition.points = new string[0];
            if (condition.levels == null) condition.levels = new int[0];
            if (condition.confirms == null) condition.confirms = new int[0];
            if (condition.reservations == null) condition.reservations = new int[0];
            if (condition.keywords == null) condition.keywords = string.Empty;

            if (!string.IsNullOrWhiteSpace(condition.seniorNode) && condition.seniorNode != "root") {
                var seniorCondition = _workContext.Profile.Settings.SeniorConditions.Find(c => c.id == condition.seniorNode);
                if (seniorCondition != null) {
                    if (seniorCondition.stationTypes != null && seniorCondition.stationTypes.Length > 0) 
                        condition.stationTypes = condition.stationTypes.Union(seniorCondition.stationTypes).ToArray();

                    if (seniorCondition.roomTypes != null && seniorCondition.roomTypes.Length > 0)
                        condition.roomTypes = condition.roomTypes.Union(seniorCondition.roomTypes).ToArray();

                    if (seniorCondition.subDeviceTypes != null && seniorCondition.subDeviceTypes.Length > 0)
                        condition.subDeviceTypes = condition.subDeviceTypes.Union(seniorCondition.subDeviceTypes).ToArray();

                    if (seniorCondition.subLogicTypes != null && seniorCondition.subLogicTypes.Length > 0)
                        condition.subLogicTypes = condition.subLogicTypes.Union(seniorCondition.subLogicTypes).ToArray();

                    if (seniorCondition.points != null && seniorCondition.points.Length > 0)
                        condition.points = condition.points.Union(seniorCondition.points).ToArray();

                    if (seniorCondition.levels != null && seniorCondition.levels.Length > 0)
                        condition.levels = condition.levels.Union(seniorCondition.levels).ToArray();

                    if (seniorCondition.confirms != null && seniorCondition.confirms.Length > 0)
                        condition.confirms = condition.confirms.Union(seniorCondition.confirms).ToArray();

                    if (seniorCondition.reservations != null && seniorCondition.reservations.Length > 0)
                        condition.reservations = condition.reservations.Union(seniorCondition.reservations).ToArray();

                    if (!string.IsNullOrWhiteSpace(seniorCondition.keywords))
                        condition.keywords = Common.JoinCondition(condition.keywords, seniorCondition.keywords);
                }
            }

            if (onlySystem) return _workContext.ActAlarms.FindAll(a => a.Room.Id == "-1");

            var stores = _workContext.ActAlarms;
            if (condition.stationTypes.Length > 0)
                stores = stores.FindAll(a => condition.stationTypes.Contains(a.Station.Type.Id));

            if (condition.roomTypes.Length > 0)
                stores = stores.FindAll(a => condition.roomTypes.Contains(a.Room.Type.Id));

            if (condition.subDeviceTypes.Length > 0)
                stores = stores.FindAll(a => condition.subDeviceTypes.Contains(a.Device.SubType.Id));

            if (condition.subLogicTypes.Length > 0)
                stores = stores.FindAll(a => condition.subLogicTypes.Contains(a.Device.SubLogicType.Id));

            if (condition.points.Length > 0)
                stores = stores.FindAll(a => condition.points.Contains(a.Point.Id));

            if (condition.levels.Length > 0)
                stores = stores.FindAll(a => condition.levels.Contains((int)a.Current.AlarmLevel));

            if (onlyConfirms)
                stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Confirmed);
            else if (condition.confirms.Length > 0)
                stores = stores.FindAll(a => condition.confirms.Contains((int)a.Current.Confirmed));

            if (onlyReservations)
                stores = stores.FindAll(a => !string.IsNullOrWhiteSpace(a.Current.ReservationId));
            else if (condition.reservations.Length > 0)
                stores = stores.FindAll(a => (condition.reservations.Contains((int)EnmReservation.UnReservation) && string.IsNullOrWhiteSpace(a.Current.ReservationId)) || (condition.reservations.Contains((int)EnmReservation.Reservation) && !string.IsNullOrWhiteSpace(a.Current.ReservationId)));

            if (!string.IsNullOrWhiteSpace(condition.keywords)) {
                var matchs = Common.SplitCondition(condition.keywords);
                if (matchs.Length > 0) stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.Name, matchs));
            }

            if (condition.baseNode == "root")
                return stores;

            var keys = Common.SplitKeys(condition.baseNode);
            if (keys.Length == 2) {
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas.Find(a => a.Current.Id == id);
                    if (current != null) stores = stores.FindAll(a => current.Keys.Contains(a.Area.Id));
                } else if (nodeType == EnmSSH.Station) {
                    stores = stores.FindAll(a => a.Station.Id == id);
                } else if (nodeType == EnmSSH.Room) {
                    stores = stores.FindAll(a => a.Room.Id == id);
                } else if (nodeType == EnmSSH.Device) {
                    stores = stores.FindAll(a => a.Device.Id == id);
                }
            }

            return stores;
        }

        private List<AlmStore<A_HAlarm>> GetRecoveryStore(ActAlmCondition condition) {
            if (condition.stationTypes == null) condition.stationTypes = new string[0];
            if (condition.roomTypes == null) condition.roomTypes = new string[0];
            if (condition.subDeviceTypes == null) condition.subDeviceTypes = new string[0];
            if (condition.subLogicTypes == null) condition.subLogicTypes = new string[0];
            if (condition.points == null) condition.points = new string[0];
            if (condition.levels == null) condition.levels = new int[0];
            if (condition.confirms == null) condition.confirms = new int[0];
            if (condition.reservations == null) condition.reservations = new int[0];
            if (condition.keywords == null) condition.keywords = string.Empty;

            if (!string.IsNullOrWhiteSpace(condition.seniorNode) && condition.seniorNode != "root") {
                var seniorCondition = _workContext.Profile.Settings.SeniorConditions.Find(c => c.id == condition.seniorNode);
                if (seniorCondition != null) {
                    if (seniorCondition.stationTypes != null && seniorCondition.stationTypes.Length > 0)
                        condition.stationTypes = condition.stationTypes.Union(seniorCondition.stationTypes).ToArray();

                    if (seniorCondition.roomTypes != null && seniorCondition.roomTypes.Length > 0)
                        condition.roomTypes = condition.roomTypes.Union(seniorCondition.roomTypes).ToArray();

                    if (seniorCondition.subDeviceTypes != null && seniorCondition.subDeviceTypes.Length > 0)
                        condition.subDeviceTypes = condition.subDeviceTypes.Union(seniorCondition.subDeviceTypes).ToArray();

                    if (seniorCondition.subLogicTypes != null && seniorCondition.subLogicTypes.Length > 0)
                        condition.subLogicTypes = condition.subLogicTypes.Union(seniorCondition.subLogicTypes).ToArray();

                    if (seniorCondition.points != null && seniorCondition.points.Length > 0)
                        condition.points = condition.points.Union(seniorCondition.points).ToArray();

                    if (seniorCondition.levels != null && seniorCondition.levels.Length > 0)
                        condition.levels = condition.levels.Union(seniorCondition.levels).ToArray();

                    if (seniorCondition.confirms != null && seniorCondition.confirms.Length > 0)
                        condition.confirms = condition.confirms.Union(seniorCondition.confirms).ToArray();

                    if (seniorCondition.reservations != null && seniorCondition.reservations.Length > 0)
                        condition.reservations = condition.reservations.Union(seniorCondition.reservations).ToArray();

                    if (!string.IsNullOrWhiteSpace(seniorCondition.keywords))
                        condition.keywords = Common.JoinCondition(condition.keywords, seniorCondition.keywords);
                }
            }

            var start = DateTime.Now.Subtract(_workContext.LastLoginTime).TotalHours > 2 ? DateTime.Now.AddHours(-2) : _workContext.LastLoginTime;
            var end = DateTime.Now;

            var stores = _workContext.AlarmsToStore(_hisAlarmService.GetAlarms(start, end));
            if (condition.stationTypes.Length > 0)
                stores = stores.FindAll(a => condition.stationTypes.Contains(a.Station.Type.Id));

            if (condition.roomTypes.Length > 0)
                stores = stores.FindAll(a => condition.roomTypes.Contains(a.Room.Type.Id));

            if (condition.subDeviceTypes.Length > 0)
                stores = stores.FindAll(a => condition.subDeviceTypes.Contains(a.Device.SubType.Id));

            if (condition.subLogicTypes.Length > 0)
                stores = stores.FindAll(a => condition.subLogicTypes.Contains(a.Device.SubLogicType.Id));

            if (condition.points.Length > 0)
                stores = stores.FindAll(a => condition.points.Contains(a.Point.Id));

            if (condition.levels.Length > 0)
                stores = stores.FindAll(a => condition.levels.Contains((int)a.Current.AlarmLevel));

            if (condition.confirms.Length > 0)
                stores = stores.FindAll(a => condition.confirms.Contains((int)a.Current.Confirmed));

            if (condition.reservations.Length > 0)
                stores = stores.FindAll(a => (condition.reservations.Contains((int)EnmReservation.UnReservation) && string.IsNullOrWhiteSpace(a.Current.ReservationId)) || (condition.reservations.Contains((int)EnmReservation.Reservation) && !string.IsNullOrWhiteSpace(a.Current.ReservationId)));

            if (!string.IsNullOrWhiteSpace(condition.keywords)) {
                var matchs = Common.SplitCondition(condition.keywords);
                if (matchs.Length > 0) stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.Name, matchs));
            }

            if (condition.baseNode == "root")
                return stores;

            var keys = Common.SplitKeys(condition.baseNode);
            if (keys.Length == 2) {
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas.Find(a => a.Current.Id == id);
                    if (current != null) stores = stores.FindAll(a => current.Keys.Contains(a.Area.Id));
                } else if (nodeType == EnmSSH.Station) {
                    stores = stores.FindAll(a => a.Station.Id == id);
                } else if (nodeType == EnmSSH.Room) {
                    stores = stores.FindAll(a => a.Room.Id == id);
                } else if (nodeType == EnmSSH.Device) {
                    stores = stores.FindAll(a => a.Device.Id == id);
                }
            }

            return stores;
        }

        private List<PointStore<P_Point>> GetActPoints(string node, int[] types) {
            var stores = new List<PointStore<P_Point>>();
            var nodeKey = Common.ParseNode(node);
            if (nodeKey.Id == EnmSSH.Root) {
                stores = this.GetFollowPoints(node, EnmSSH.Area);
            } else if (nodeKey.Id == EnmSSH.Device) {
                var current = _workContext.Devices.Find(d => d.Current.Id == nodeKey.Value);
                if (current != null && current.Protocol != null) {
                    var area = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                    if (area != null) {
                        var followKeys = new HashSet<string>(_workContext.Profile.FollowPoints.Select(p => string.Format("{0}-{1}", p.DeviceId, p.PointId)));
                        foreach (var point in current.Protocol.Points) {
                            stores.Add(new PointStore<P_Point>() {
                                Current = point,
                                Type = (point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId)) ? EnmPoint.AL : point.Type,
                                Device = current.Current,
                                Area = new A_Area {
                                    Id = area.Current.Id,
                                    Code = area.Current.Code,
                                    Name = area.ToString(),
                                    Type = area.Current.Type,
                                    ParentId = area.Current.ParentId,
                                    Comment = area.Current.Comment,
                                    Enabled = area.Current.Enabled
                                },
                                Followed = followKeys.Contains(string.Format("{0}-{1}", current.Current.Id, point.Id)),
                                FollowedOnly = false
                            });
                        }
                    }
                }
            } else {
                stores = this.GetFollowPoints(nodeKey.Value, nodeKey.Id);
            }

            stores = stores.FindAll(p => types.Contains((int)p.Type)).OrderByDescending(p => (int)p.Type).ToList();
            return stores;
        }

        private List<PointStore<P_Point>> GetFollowPoints(string node, EnmSSH type) {
            if (_workContext.Profile == null) return new List<PointStore<P_Point>>();
            if (_workContext.Profile.FollowPoints.Count == 0) return new List<PointStore<P_Point>>();

            List<PointStore<P_Point>> stores;
            var key = string.Format(GlobalCacheKeys.User_Cache_FollowPoints, _workContext.User.Id);
            if (_cacheManager.IsSet(key)) {
                stores = _cacheManager.Get<List<PointStore<P_Point>>>(key);
            } else {
                stores = (from follow in _workContext.Profile.FollowPoints
                          join point in _workContext.Points on follow.PointId equals point.Id
                          join device in _workContext.Devices on follow.DeviceId equals device.Current.Id
                          join area in _workContext.Areas on device.Current.AreaId equals area.Current.Id
                          select new PointStore<P_Point> {
                              Current = point,
                              Type = (point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId)) ? EnmPoint.AL : point.Type,
                              Device = device.Current,
                              Area = new A_Area {
                                  Id = area.Current.Id,
                                  Code = area.Current.Code,
                                  Name = area.ToString(),
                                  Type = area.Current.Type,
                                  ParentId = area.Current.ParentId,
                                  Comment = area.Current.Comment,
                                  Enabled = area.Current.Enabled
                              },
                              Followed = true,
                              FollowedOnly = true
                          }).ToList();

                _cacheManager.Set<List<PointStore<P_Point>>>(key, stores, CachedIntervals.Global_Default_Intervals);
            }

            if(node == "root") return stores;
            if(type == EnmSSH.Area) {
                var current = _workContext.Areas.Find(a => a.Current.Id == node);
                if(current != null) stores = stores.FindAll(p => current.Keys.Contains(p.Area.Id));
            } else if(type == EnmSSH.Station) {
                stores = stores.FindAll(p => p.Device.StationId == node);
            } else if(type == EnmSSH.Room) {
                stores = stores.FindAll(p => p.Device.RoomId == node);
            }

            return stores;
        }

        #endregion

    }
}