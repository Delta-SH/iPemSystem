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
        private readonly IPointService _pointService;
        private readonly IFsuService _fsuService;
        private readonly IElecService _elecService;
        private readonly IAMeasureService _aMeasureService;

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
            IPointService pointService,
            IFsuService fsuService,
            IElecService elecService,
            IAMeasureService aMeasureService) {
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
            this._pointService = pointService;
            this._fsuService = fsuService;
            this._elecService = elecService;
            this._aMeasureService = aMeasureService;
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
                if(config == null)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.basic == null || !config.basic.Contains(1))
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.levels == null || config.levels.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.contents == null || config.contents.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                var start = _workContext.LastSpeechTime; var end = DateTime.Now;
                var stores = _workContext.ActAlarms.FindAll(a => a.Current.CreatedTime >= start && a.Current.CreatedTime < end && config.levels.Contains((int)a.Current.AlarmLevel));
                if(config.basic.Contains(3))
                    stores = stores.FindAll(a => string.IsNullOrWhiteSpace(a.Current.ReservationId));
                if(config.basic.Contains(4))
                    stores = stores.FindAll(a => a.Current.Confirmed == EnmConfirm.Unconfirmed);

                if(config.stationTypes != null && config.stationTypes.Length > 0)
                    stores = stores.FindAll(a => config.stationTypes.Contains(a.Station.Type.Id));

                if(config.roomTypes != null && config.roomTypes.Length > 0)
                    stores = stores.FindAll(a => config.roomTypes.Contains(a.Room.Type.Id));

                if(config.deviceTypes != null && config.deviceTypes.Length > 0)
                    stores = stores.FindAll(a => config.deviceTypes.Contains(a.Device.Type.Id));

                if(config.logicTypes != null && config.logicTypes.Length > 0)
                    stores = stores.FindAll(a => config.logicTypes.Contains(a.Point.LogicType.Id));

                if(!string.IsNullOrWhiteSpace(config.pointNames)) {
                    var names = Common.SplitCondition(config.pointNames);
                    if(names.Length > 0) stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.Name, names));
                }

                if(!string.IsNullOrWhiteSpace(config.pointExtset)) {
                    var extsets = Common.SplitCondition(config.pointExtset);
                    if(extsets.Length > 0) stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.ExtSet1, extsets) || CommonHelper.ConditionContain(a.Point.ExtSet2, extsets));
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

                if(!config.basic.Contains(2)) _workContext.LastSpeechTime = end;
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
        public JsonResult RequestActAlarms(string node, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<ActAlmModel>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>(),
                chart = new List<ChartModel>[2]
            };

            try {
                var stores = this.GetActAlmStore(node, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(stores != null) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            index = i + 1
                        });
                    }

                    data.chart[0] = this.GetActAlmChart1(stores);
                    data.chart[1] = this.GetActAlmChart2(node, stores);
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
        public ActionResult DownloadActAlms(string node, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project) {
            try {
                var models = new List<ActAlmModel>();
                var stores = this.GetActAlmStore(node, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(stores != null && stores.Count > 0) {
                    for(int i = 0; i < stores.Count; i++) {
                        models.Add(new ActAlmModel {
                            index = i + 1,
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

        [AjaxAuthorize]
        public JsonResult RequestActPoints(string node, int[] types, int start, int limit) {
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
                        data.message = "200 Ok";
                        data.total = stores.Count;

                        var end = start + limit;
                        if(end > stores.Count)
                            end = stores.Count;

                        var points = new List<PointStore<P_Point>>();
                        for(var i = start; i < end; i++)
                            points.Add(stores[i]);

                        #region 标准信号

                        var ptPoints = points.FindAll(p => p.Type != EnmPoint.AL);
                        if (ptPoints.Count > 0) {
                            var values = new List<V_AMeasure>();
                            var pointsInDevices = ptPoints.GroupBy(g => g.Device.Current.Id);
                            if (pointsInDevices.Count() > 5) {
                                foreach (var point in ptPoints) {
                                    values.Add(_aMeasureService.GetMeasure(point.Device.Current.Id, point.Current.Code, point.Current.Number));
                                }
                            } else {
                                foreach (var pointsInDevice in pointsInDevices) {
                                    values.AddRange(_aMeasureService.GetMeasuresInDevice(pointsInDevice.Key));
                                }
                            }

                            var pValues = from point in ptPoints
                                          join val in values on new { DeviceId = point.Device.Current.Id, SignalId = point.Current.Code, SignalNumber = point.Current.Number } equals new { val.DeviceId, val.SignalId, val.SignalNumber } into lt
                                          from def in lt.DefaultIfEmpty()
                                          select new {
                                              Point = point,
                                              Value = def
                                          };

                            foreach (var pv in pValues) {
                                var value = pv.Value != null && pv.Value.Value != double.MinValue ? pv.Value.Value.ToString() : "NULL";
                                var status = pv.Value != null ? pv.Value.Status : EnmState.Invalid;
                                var time = pv.Value != null ? pv.Value.UpdateTime : DateTime.Now;

                                data.data.Add(new ActPointModel {
                                    index = ++start,
                                    area = pv.Point.Area.ToString(),
                                    station = pv.Point.Device.Current.StationName,
                                    room = pv.Point.Device.Current.RoomName,
                                    device = pv.Point.Device.Current.Name,
                                    point = pv.Point.Current.Name,
                                    type = Common.GetPointTypeDisplay(pv.Point.Type),
                                    value = value,
                                    unit = Common.GetUnitDisplay(pv.Point.Current.Type, value, pv.Point.Current.UnitState),
                                    status = Common.GetPointStatusDisplay(status),
                                    time = CommonHelper.DateTimeConverter(time),
                                    deviceid = pv.Point.Device.Current.Id,
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

                        var alPoints = points.FindAll(p => p.Type == EnmPoint.AL);
                        if (alPoints.Count > 0) {
                            var almKeys = _workContext.AlarmsToDictionary(_workContext.ActAlarms, false);
                            foreach (var point in alPoints) {
                                var model = new ActPointModel {
                                    index = ++start,
                                    area = point.Area.ToString(),
                                    station = point.Device.Current.StationName,
                                    room = point.Device.Current.RoomName,
                                    device = point.Device.Current.Name,
                                    point = point.Current.Name,
                                    type = Common.GetPointTypeDisplay(point.Type),
                                    value = "0",
                                    unit = "正常",
                                    status = Common.GetPointStatusDisplay(EnmState.Normal),
                                    time = CommonHelper.DateTimeConverter(DateTime.Now),
                                    deviceid = point.Device.Current.Id,
                                    pointid = point.Current.Id,
                                    typeid = (int)point.Type,
                                    statusid = (int)EnmState.Normal,
                                    followed = point.Followed,
                                    followedOnly = point.FollowedOnly,
                                    timestamp = CommonHelper.ShortTimeConverter(DateTime.Now)
                                };

                                var key = Common.JoinKeys(point.Device.Current.Id, point.Current.Id);
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

                                data.data.Add(model);
                            }
                        }

                        #endregion
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
                if(keys == null || keys.Length == 0)
                    throw new ArgumentException("keys");

                var entities = new List<A_AAlarm>();
                foreach(var key in keys) {
                    entities.Add(new A_AAlarm {
                        Id = key,
                        Confirmed = EnmConfirm.Confirmed,
                        ConfirmedTime = DateTime.Now,
                        Confirmer = _workContext.Employee.Name
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
        public JsonResult GetReservationDetail(string id) {
            var data = new AjaxDataModel<ReservationModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new ReservationModel()
            };

            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                var reservation = _reservationService.GetReservation(id);
                if(reservation == null) throw new iPemException("未找到数据对象");

                var project = _projectService.GetProject(reservation.ProjectId);

                data.data.id = reservation.Id.ToString();
                data.data.startDate = CommonHelper.DateTimeConverter(reservation.StartTime);
                data.data.endDate = CommonHelper.DateTimeConverter(reservation.EndTime);
                data.data.projectId = reservation.ProjectId.ToString();
                data.data.projectName = project != null ? project.Name : "";
                data.data.creator = reservation.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(reservation.CreatedTime);
                data.data.comment = reservation.Comment;
                data.data.enabled = reservation.Enabled;
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
                if(!curExtFsu.Status) throw new iPemException("Fsu通信中断");

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
                                    Status = EnmState.Normal,
                                    Time = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = BIPackMgr.SetPoint(curExtFsu, _workContext.WsValues, package);
                if(result != null) {
                    if(result.Result == EnmResult.Failure) throw new iPemException(result.FailureCause ?? "参数设置失败");
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
                                    Status = EnmState.Normal,
                                    Time = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = BIPackMgr.SetPoint(curExtFsu, _workContext.WsValues, package);
                if(result != null) {
                    if(result.Result == EnmResult.Failure) throw new iPemException(result.FailureCause ?? "参数设置失败");
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
        public JsonResult GetThreshold(string device, string point) {
            var data = new AjaxDataModel<ThresholdModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = null
            };

            try {
                if (!_workContext.Authorizations.Permissions.Contains(EnmPermission.Threshold))
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

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                var package = new GetThresholdPackage() {
                    FsuId = curFsu.Current.Code,
                    DeviceList = new List<GetThresholdDevice>() {
                        new GetThresholdDevice() {
                            Id = curDevice.Current.Code,
                            Ids = new List<string>(){ curPoint.Code }
                        }
                    }
                };

                var result = BIPackMgr.GetThreshold(curExtFsu, _workContext.WsValues, package);
                if(result != null ) {
                    if(result.Result == EnmResult.Failure) throw new iPemException(result.FailureCause ?? "参数设置失败");
                    if(result.DeviceList != null) {
                        var devResult = result.DeviceList.Find(d => d.Id == curDevice.Current.Code);
                        if(devResult != null) {
                            var dResult = devResult.Values.Find(d => d.Id == curPoint.Code && d.SignalNumber == curPoint.Number);
                            if(dResult != null) {
                                data.data = new ThresholdModel {
                                    id = dResult.Id,
                                    number = dResult.SignalNumber,
                                    type = (int)dResult.Type,
                                    threshold = dResult.Threshold,
                                    level = ((int)dResult.AlarmLevel).ToString(),
                                    nmid = dResult.NMAlarmID
                                };
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                throw new iPemException("获取数据失败");
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SetThreshold(string device, string point, double threshold, int alarmLevel, string nmalarmID) {
            try {
                if (!_workContext.Authorizations.Permissions.Contains(EnmPermission.Threshold))
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

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                var package = new SetThresholdPackage() {
                    FsuId = curFsu.Current.Code,
                    DeviceList = new List<SetThresholdDevice>() {
                        new SetThresholdDevice() {
                            Id = curDevice.Current.Code,
                            Values = new List<TThreshold>() {
                                new TThreshold() {
                                    Id = curPoint.Code,
                                    SignalNumber = curPoint.Number,
                                    Type = EnmBIPoint.AL,
                                    Threshold = threshold.ToString(),
                                    AlarmLevel = Enum.IsDefined(typeof(EnmAlarm), alarmLevel) ? (EnmAlarm)alarmLevel : EnmAlarm.Level0,
                                    NMAlarmID = nmalarmID
                                }
                            }
                        }
                    }
                };

                var result = BIPackMgr.SetThreshold(curExtFsu, _workContext.WsValues, package);
                if(result != null) {
                    if(result.Result == EnmResult.Failure) throw new iPemException(result.FailureCause ?? "参数设置失败");
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
                    var alarmsInRoot = _workContext.ActAlarms.FindAll(alarm => root.Keys.Contains(alarm.Current.AreaId));

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
                var extFsus = _fsuService.GetExtFsus();
                var allFsus = from fsu in _workContext.Fsus
                              join ext in extFsus on fsu.Current.Id equals ext.Id
                              select new { Fsu = fsu, Ext = ext };

                var unStations = new List<IdValuePair<S_Station, DateTime>>();
                foreach(var station in _workContext.Stations) {
                    if(!allFsus.Any(f => f.Fsu.Current.StationId == station.Current.Id && f.Ext.Status)) {
                        var staFsus = allFsus.Where(f => f.Fsu.Current.StationId == station.Current.Id);
                        if(staFsus.Any()) {
                            unStations.Add(new IdValuePair<S_Station, DateTime> {
                                Id = station.Current,
                                Value = staFsus.Max(f => f.Ext.LastTime)
                            });
                        }
                    }
                }

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = _workContext.Stations.Count - unStations.Count };
                data.chart[1] = new ChartModel { index = 2, name = "断站", value = unStations.Count };

                var stations = (from station in unStations
                                join area in _workContext.Areas on station.Id.AreaId equals area.Current.Id
                                select new { Station = station, Area = area }).ToList();

                if(stations.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stations.Count;

                    var end = start + limit;
                    if(end > stations.Count)
                        end = stations.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new HomeUnconnectedModel {
                            index = i + 1,
                            area = stations[i].Area.ToString(),
                            station = stations[i].Station.Id.Name,
                            time = CommonHelper.DateTimeConverter(stations[i].Station.Value),
                            interval = CommonHelper.IntervalConverter(stations[i].Station.Value)
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
                var extFsus = _fsuService.GetExtFsus();
                var allFsus = from fsu in _workContext.Fsus
                              join ext in extFsus on fsu.Current.Id equals ext.Id
                              select new { Fsu = fsu, Ext = ext };

                var unStations = new List<IdValuePair<S_Station, DateTime>>();
                foreach (var station in _workContext.Stations) {
                    if (!allFsus.Any(f => f.Fsu.Current.StationId == station.Current.Id && f.Ext.Status)) {
                        var staFsus = allFsus.Where(f => f.Fsu.Current.StationId == station.Current.Id);
                        if (staFsus.Any()) {
                            unStations.Add(new IdValuePair<S_Station, DateTime> {
                                Id = station.Current,
                                Value = staFsus.Max(f => f.Ext.LastTime)
                            });
                        }
                    }
                }

                var stations = (from station in unStations
                                join area in _workContext.Areas on station.Id.AreaId equals area.Current.Id
                                select new { Station = station, Area = area }).ToList();

                var models = new List<HomeUnconnectedModel>();
                for(int i = 0; i < stations.Count; i++) {
                    models.Add(new HomeUnconnectedModel {
                        index = i + 1,
                        area = stations[i].Area.ToString(),
                        station = stations[i].Station.Id.Name,
                        time = CommonHelper.DateTimeConverter(stations[i].Station.Value),
                        interval = CommonHelper.IntervalConverter(stations[i].Station.Value)
                    });
                }

                using(var ms = _excelManager.Export<HomeUnconnectedModel>(models, "站点断站列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<AlmStore<A_AAlarm>> GetActAlmStore(string node, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project) {
            var stores = new List<AlmStore<A_AAlarm>>();
            if(node == "root") {
                stores = _workContext.ActAlarms;
            } else {
                var keys = Common.SplitKeys(node);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) stores = _workContext.ActAlarms.FindAll(a => current.Keys.Contains(a.Area.Id));
                    } else if(nodeType == EnmSSH.Station) {
                        stores = _workContext.ActAlarms.FindAll(a => a.Station.Id == id);
                    } else if(nodeType == EnmSSH.Room) {
                        stores = _workContext.ActAlarms.FindAll(a => a.Room.Id == id);
                    } else if(nodeType == EnmSSH.Device) {
                        stores = _workContext.ActAlarms.FindAll(a => a.Device.Id == id);
                    }
                }
            }

            if(statype != null && statype.Length > 0)
                stores = stores.FindAll(s => statype.Contains(s.Station.Type.Id));

            if(roomtype != null && roomtype.Length > 0)
                stores = stores.FindAll(s => roomtype.Contains(s.Room.Type.Id));

            if(devtype != null && devtype.Length > 0)
                stores = stores.FindAll(s => devtype.Contains(s.Device.Type.Id));

            if(logictype != null && logictype.Length > 0)
                stores = stores.FindAll(s => logictype.Contains(s.Point.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0)
                    stores = stores.FindAll(p => CommonHelper.ConditionContain(p.Point.Name, names));
            }

            if(almlevel != null && almlevel.Length > 0)
                stores = stores.FindAll(s => almlevel.Contains((int)s.Current.AlarmLevel));

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

        private List<ChartModel> GetActAlmChart1(List<AlmStore<A_AAlarm>> stores) {
            var level1 = new ChartModel { index = (int)EnmAlarm.Level1, name = Common.GetAlarmDisplay(EnmAlarm.Level1), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level1) };
            var level2 = new ChartModel { index = (int)EnmAlarm.Level2, name = Common.GetAlarmDisplay(EnmAlarm.Level2), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level2) };
            var level3 = new ChartModel { index = (int)EnmAlarm.Level3, name = Common.GetAlarmDisplay(EnmAlarm.Level3), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level3) };
            var level4 = new ChartModel { index = (int)EnmAlarm.Level4, name = Common.GetAlarmDisplay(EnmAlarm.Level4), value = stores.Count(s => s.Current.AlarmLevel == EnmAlarm.Level4) };
            return new List<ChartModel>() { level1, level2, level3, level4 };
        }

        private List<ChartModel> GetActAlmChart2(string node, List<AlmStore<A_AAlarm>> stores) {
            var models = new List<ChartModel>();
            return models;
        }

        private List<PointStore<P_Point>> GetActPoints(string node, int[] types) {
            var stores = new List<PointStore<P_Point>>();

            if(node == "root") {
                stores = this.GetFollowPoints(node, EnmSSH.Area);
            } else {
                var keys = Common.SplitKeys(node);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Device) {
                        var current = _workContext.Devices.Find(d => d.Current.Id == id);
                        if(current != null && current.Protocol != null) {
                            var area = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                            if(area != null) {
                                var followKeys = new HashSet<string>(_workContext.Profile.FollowPoints.Select(p => string.Format("{0}-{1}", p.DeviceId, p.PointId)));
                                foreach(var point in current.Protocol.Points) {
                                    stores.Add(new PointStore<P_Point>() {
                                        Current = point,
                                        Type = (point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId)) ? EnmPoint.AL : point.Type,
                                        Device = current,
                                        Area = area,
                                        Followed = followKeys.Contains(string.Format("{0}-{1}", current.Current.Id, point.Id)),
                                        FollowedOnly = false
                                    });
                                }
                            }
                        }
                    } else {
                        stores = this.GetFollowPoints(id, nodeType);
                    }
                }
            }

            stores = stores.FindAll(p => types.Contains((int)p.Type)).OrderByDescending(p => (int)p.Type).ToList();
            return stores;
        }

        private List<PointStore<P_Point>> GetFollowPoints(string node, EnmSSH type) {
            var stores = new List<PointStore<P_Point>>();
            if(_workContext.Profile == null) return stores;
            if(_workContext.Profile.FollowPoints.Count == 0) return stores;

            stores = (from follow in _workContext.Profile.FollowPoints
                      join point in _workContext.Points on follow.PointId equals point.Id
                      join device in _workContext.Devices on follow.DeviceId equals device.Current.Id
                      join area in _workContext.Areas on device.Current.AreaId equals area.Current.Id
                      select new PointStore<P_Point> {
                          Current = point,
                          Type = (point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId)) ? EnmPoint.AL : point.Type,
                          Device = device,
                          Area = area,
                          Followed = true,
                          FollowedOnly = true
                      }).ToList();

            if(node == "root") return stores;
            if(type == EnmSSH.Area) {
                var current = _workContext.Areas.Find(a => a.Current.Id == node);
                if(current != null) stores = stores.FindAll(p => current.Keys.Contains(p.Area.Current.Id));
            } else if(type == EnmSSH.Station) {
                stores = stores.FindAll(p => p.Device.Current.StationId == node);
            } else if(type == EnmSSH.Room) {
                stores = stores.FindAll(p => p.Device.Current.RoomId == node);
            }

            return stores;
        }

        #endregion

    }
}