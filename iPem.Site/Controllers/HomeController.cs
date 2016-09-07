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
        private readonly IWebLogger _webLogger;
        private readonly IAppointmentService _appointmentService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IExtendAlmService _extendAlmService;
        private readonly INoticeService _noticeService;
        private readonly INoticeInUserService _noticeInUserService;
        private readonly IProfileService _profileService;
        private readonly IProjectService _projectsService;
        private readonly IActAlmService _actAlmService;
        private readonly IPointService _pointService;
        private readonly IFsuKeyService _fsuKeyService;

        #endregion

        #region Ctor

        public HomeController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IAppointmentService appointmentService,
            IDictionaryService dictionaryService,
            IExtendAlmService extendAlmService,
            INoticeService noticeService,
            INoticeInUserService noticeInUserService,
            IProfileService profileService,
            IProjectService projectsService,
            IActAlmService actAlmService,
            IPointService pointService,
            IFsuKeyService fsuKeyService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._appointmentService = appointmentService;
            this._dictionaryService = dictionaryService;
            this._extendAlmService = extendAlmService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._profileService = profileService;
            this._projectsService = projectsService;
            this._actAlmService = actAlmService;
            this._pointService = pointService;
            this._fsuKeyService = fsuKeyService;
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
            ViewBag.Control = _workContext.Operations.Contains(EnmOperation.Control);
            ViewBag.Adjust = _workContext.Operations.Contains(EnmOperation.Adjust);
            return View();
        }

        public ActionResult ActiveAlarm() {
            ViewBag.BarIndex = 2;
            ViewBag.MenuVisible = false;
            ViewBag.Confirm = _workContext.Operations.Contains(EnmOperation.Confirm);
            Session["ActAlmNoticeTime"] = DateTime.Now.Ticks;
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

                var start = DateTime.Now.AddSeconds(-30); 
                var end = DateTime.Now;
                if(Session["SpeechScanTime"] == null)
                    Session["SpeechScanTime"] = start.Ticks;
                else
                    start = new DateTime(Convert.ToInt64(Session["SpeechScanTime"]));

                var alarms = _actAlmService.GetAlmsAsList(start, end).FindAll(a => config.levels.Contains((int)a.AlmLevel));
                var stores = _workContext.GetActAlmStore(alarms);
                if(config.basic.Contains(3))
                    stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

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
                    if(names.Length > 0)
                        stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.Name, names));
                }

                if(!string.IsNullOrWhiteSpace(config.pointExtset)) {
                    var extsets = Common.SplitCondition(config.pointExtset);
                    if(extsets.Length > 0)
                        stores = stores.FindAll(a => CommonHelper.ConditionContain(a.Point.ExtSet1, extsets));
                }

                foreach(var store in stores) {
                    var contents = new List<string>();
                    if(config.contents.Contains(1))
                        contents.Add(store.AreaFullName);

                    if(config.contents.Contains(2))
                        contents.Add(store.Station.Name);

                    if(config.contents.Contains(3))
                        contents.Add(store.Room.Name);

                    if(config.contents.Contains(4))
                        contents.Add(store.Device.Name);

                    if(config.contents.Contains(5))
                        contents.Add(store.Point.Name);

                    if(config.contents.Contains(6))
                        contents.Add(CommonHelper.DateTimeConverter(store.Current.StartTime));

                    if(config.contents.Contains(7))
                        contents.Add(string.Format("发生{0}", Common.GetAlarmLevelDisplay(store.Current.AlmLevel)));

                    if(config.contents.Contains(8) && !string.IsNullOrWhiteSpace(store.Current.AlmDesc))
                        contents.Add(store.Current.AlmDesc);

                    data.data.Add(string.Join("，", contents));
                }

                if(!config.basic.Contains(2))
                    Session["SpeechScanTime"] = end.Ticks;

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
                    throw new iPemException("参数无效 text");

                var bytes = CommonHelper.ConvertTextToSpeech(text);
                if(bytes == null)
                    throw new iPemException("语音转换失败");

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
                var menus = _workContext.Menus;
                if(menus != null && menus.Count > 0) {
                    var roots = new List<Menu>();
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

        private void MenusRecursion(List<Menu> menus, int pid, TreeModel node) {
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
                    throw new ArgumentException("参数无效 listModel");

                var user = _workContext.User;
                var notices = _noticeService.GetNotices(user.Id);
                var noticesInUser = _noticeInUserService.GetNoticesAsList(user.Id);

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
                            data.data.AddRange(models);
                        }
                    }
                } else if("readed".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var readed = models.Where(m => m.readed);
                    if(readed.Any()) {
                        var result = new PagedList<NoticeModel>(readed, start / limit, limit, readed.Count());
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(readed);
                        }
                    }
                } else if("unread".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var unread = models.Where(m => !m.readed);
                    if(unread.Any()) {
                        var result = new PagedList<NoticeModel>(unread, start / limit, limit, unread.Count());
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(unread);
                        }
                    }
                } else {
                    throw new ArgumentException("参数无效 listModel");
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetNoticesCount() {
            var data = new AjaxDataModel<int> {
                success = true,
                message = "无数据",
                total = 0,
                data = 0
            };

            try {
                var unreadsCount = _noticeService.GetUnreadCount(_workContext.User.Id);
                if(unreadsCount > 0) {
                    data.message = "200 Ok";
                    data.total = unreadsCount;
                    data.data = unreadsCount;
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
                if(notices == null || notices.Length == 0)
                    throw new ArgumentException("参数无效 notices");

                if(string.IsNullOrWhiteSpace(status))
                    throw new ArgumentException("参数无效 status");

                if(!"readed".Equals(status, StringComparison.CurrentCultureIgnoreCase) 
                    && !"unread".Equals(status, StringComparison.CurrentCultureIgnoreCase))
                    throw new ArgumentException("参数无效 status");

                var result = new List<NoticeInUser>();
                var noticesInUser = _noticeInUserService.GetNoticesAsList(_workContext.User.Id);
                foreach(var notice in notices) {
                    var target = noticesInUser.Find(n => n.NoticeId == new Guid(notice));
                    if(target == null) continue;

                    target.Readed = "readed".Equals(status,StringComparison.CurrentCultureIgnoreCase);
                    target.ReadTime = target.Readed ? DateTime.Now : default(DateTime);
                    result.Add(target);
                }

                if(result.Count > 0)
                    _noticeInUserService.UpdateRange(result);

                return Json(new AjaxResultModel { success= true, code =200, message ="Ok" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetActAlmCount() {
            var data = new AjaxDataModel<int> {
                success = true,
                message = "无数据",
                total = 0,
                data = 0
            };

            try {
                var start = DateTime.Now; var end = DateTime.Now;
                if(Session["ActAlmNoticeTime"] != null)
                    start = new DateTime(Convert.ToInt64(Session["ActAlmNoticeTime"]));
                else
                    Session["ActAlmNoticeTime"] = start.Ticks;

                var alarms = _actAlmService.GetAlmsAsList(start, end);
                if(alarms.Count > 0) {
                    var matchs = new HashSet<string>();
                    foreach(var area in _workContext.RoleAreas) {
                        matchs.Add(area.Current.Id);
                    }

                    var count = alarms.Count(m => matchs.Contains(m.AreaId));
                    if(count > 0) {
                        data.message = "200 Ok";
                        data.total = count;
                        data.data = count;
                    }
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
                if(stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if(end > stores.Count)
                        end = stores.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            index = i + 1,
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            levelid = (int)stores[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            comment = stores[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            frequency = stores[i].Current.Frequency,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmed = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirm.Unconfirmed),
                            confirmedtime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty
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
                            fsuid = stores[i].Current.FsuId,
                            id = stores[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(stores[i].Current.AlmLevel),
                            levelid = (int)stores[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(stores[i].Current.StartTime),
                            area = stores[i].AreaFullName,
                            station = stores[i].Station.Name,
                            room = stores[i].Room.Name,
                            device = stores[i].Device.Name,
                            point = stores[i].Point.Name,
                            comment = stores[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", stores[i].Current.StartValue, stores[i].Current.ValueUnit),
                            frequency = stores[i].Current.Frequency,
                            interval = CommonHelper.IntervalConverter(stores[i].Current.StartTime),
                            project = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.ProjectId) ? stores[i].ExtSet1.ProjectId : string.Empty,
                            confirmed = Common.GetConfirmStatusDisplay(stores[i].ExtSet1 != null ? stores[i].ExtSet1.Confirmed : EnmConfirm.Unconfirmed),
                            confirmedtime = stores[i].ExtSet1 != null && stores[i].ExtSet1.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(stores[i].ExtSet1.ConfirmedTime.Value) : string.Empty,
                            confirmer = stores[i].ExtSet1 != null && !string.IsNullOrWhiteSpace(stores[i].ExtSet1.Confirmer) ? stores[i].ExtSet1.Confirmer : string.Empty,
                            background = Common.GetAlarmLevelColor(stores[i].Current.AlmLevel)
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

                        var points = new List<PointStore<Point>>();
                        for(var i = start; i < end; i++)
                            points.Add(stores[i]);

                        foreach(var point in points) {
                            var tick = DateTime.Now.Ticks;
                            var value = (float)Math.Round(CommonHelper.GenerateRandomInteger(-500, 500) * 0.782, 2);
                            var status = EnmPointStatus.Invalid;
                            var changed = DateTime.Now;

                            data.data.Add(new ActPointModel {
                                index = ++start,
                                area = point.AreaFullName,
                                station = point.Device.StationName,
                                room = point.Device.RoomName,
                                device = point.Device.Name,
                                point = point.Current.Name,
                                type = Common.GetPointTypeDisplay(point.Current.Type),
                                value = value,
                                unit = Common.GetUnitDisplay(point.Current.Type, value, point.Current.UnitState),
                                status = Common.GetPointStatusDisplay(status),
                                time = CommonHelper.DateTimeConverter(changed),
                                devid = point.Device.Id,
                                pointid = point.Current.Id,
                                typeid = (int)point.Current.Type,
                                statusid = (int)status,
                                rsspoint = point.RssPoint,
                                rssfrom = point.RssFrom,
                                timestamp = CommonHelper.ShortTimeConverter(changed)
                            });
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
        [AjaxAuthorize]
        public JsonResult ConfirmAlarms(string[] keys) {
            try {
                if(keys == null || keys.Length == 0)
                    throw new ArgumentException("参数无效 keys");

                var entities = new List<ExtAlm>();
                foreach(var key in keys) {
                    var ids = Common.SplitKeys(key);
                    if(ids.Length != 2) continue;

                    var current = _workContext.ActAlmStore.Find(a => a.Current.FsuId == ids[0] && a.Current.Id == ids[1]);
                    if(current != null) {
                        entities.Add(new ExtAlm {
                            FsuId = current.Current.FsuId,
                            Id = current.Current.Id,
                            Start = current.Current.StartTime,
                            Confirmed = EnmConfirm.Confirmed,
                            ConfirmedTime = DateTime.Now,
                            Confirmer = _workContext.Employee.Name
                        });
                    }
                }

                _extendAlmService.Update(entities);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警确认成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult GetAppointmentDetail(string id) {
            var data = new AjaxDataModel<AppointmentModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new AppointmentModel()
            };

            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var appointment = _appointmentService.GetAppointment(new Guid(id));
                if(appointment == null)
                    throw new iPemException("未找到数据对象");

                var project = _projectsService.GetProject(appointment.ProjectId);

                data.data.id = appointment.Id.ToString();
                data.data.startDate = CommonHelper.DateTimeConverter(appointment.StartTime);
                data.data.endDate = CommonHelper.DateTimeConverter(appointment.EndTime);
                data.data.projectId = appointment.ProjectId.ToString();
                data.data.projectName = project != null ? project.Name : data.data.projectId;
                data.data.creator = appointment.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(appointment.CreatedTime);
                data.data.comment = appointment.Comment;
                data.data.enabled = appointment.Enabled;
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
                if(!_workContext.Operations.Contains(EnmOperation.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device))
                    throw new ArgumentException("参数无效 device");

                if(string.IsNullOrWhiteSpace(point))
                    throw new ArgumentException("参数无效 point");

                var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == device);
                if(curDevice == null) throw new iPemException("未找到设备");

                var curFsu = _workContext.RoleFsus.Find(f => f.Current.Id == curDevice.Current.FsuId);
                if(curFsu == null) throw new iPemException("未找到Fsu");

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                return Json(new AjaxResultModel { success = true, code = 200, message = "参数设置成功" });
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult AdjustPoint(string device, string point, float adjust) {
            try {
                if(!_workContext.Operations.Contains(EnmOperation.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device))
                    throw new ArgumentException("参数无效 device");

                if(string.IsNullOrWhiteSpace(point))
                    throw new ArgumentException("参数无效 point");

                var curDevice = _workContext.RoleDevices.Find(d => d.Current.Id == device);
                if(curDevice == null) throw new iPemException("未找到设备");

                var curFsu = _workContext.RoleFsus.Find(f => f.Current.Id == curDevice.Current.FsuId);
                if(curFsu == null) throw new iPemException("未找到Fsu");

                var curPoint = curDevice.Protocol.Points.Find(p => p.Id == point);
                if(curPoint == null) throw new iPemException("未找到信号");

                return Json(new AjaxResultModel { success = true, code = 200, message = "参数设置成功" });
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult AddRssPoint(string device, string point) {
            try {
                var profile = _workContext.Profile ?? new ProfileValues() { RssPoints = new List<RssPoint>() };
                if(!profile.RssPoints.Any(p => p.device == device && p.point == point)) {
                    profile.RssPoints.Add(new RssPoint { device = device, point = point });
                    _profileService.Save(new UserProfile {
                        UserId = _workContext.User.Id,
                        ValuesJson = JsonConvert.SerializeObject(profile),
                        ValuesBinary = null,
                        LastUpdatedDate = DateTime.Now
                    });
                    _workContext.Store.Profile  = null;
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "关注成功" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult RemoveRssPoint(string device, string point) {
            try {
                var profile = _workContext.Profile ?? new ProfileValues() { RssPoints = new List<RssPoint>() };
                var current = profile.RssPoints.Find(p => p.device == device && p.point == point);
                if(current != null) {
                    profile.RssPoints.Remove(current);
                    _profileService.Save(new UserProfile {
                        UserId = _workContext.User.Id,
                        ValuesJson = JsonConvert.SerializeObject(profile),
                        ValuesBinary = null,
                        LastUpdatedDate = DateTime.Now
                    });
                    _workContext.Store.Profile = null;
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
                model.total1 = _workContext.ActAlmStore.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level1);
                model.total2 = _workContext.ActAlmStore.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level2);
                model.total3 = _workContext.ActAlmStore.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level3);
                model.total4 = _workContext.ActAlmStore.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level4);
                model.total = model.total1 + model.total2 + model.total3 + model.total4;
                model.alarms = new List<HomeAreaAlmModel>();

                var roots = _workContext.RoleAreas.FindAll(a => !a.HasParents);
                foreach(var root in roots) {
                    var alarmsInRoot = _workContext.ActAlmStore.FindAll(s => root.Keys.Contains(s.Current.AreaId));

                    var alarmsInArea = new HomeAreaAlmModel();
                    alarmsInArea.name = root.Current.Name;
                    alarmsInArea.level1 = alarmsInRoot.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level1);
                    alarmsInArea.level2 = alarmsInRoot.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level2);
                    alarmsInArea.level3 = alarmsInRoot.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level3);
                    alarmsInArea.level4 = alarmsInRoot.Count(a => a.Current.AlmLevel == EnmAlarmLevel.Level4);
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
        public JsonResult RequestHomeOff(int start, int limit) {
            var data = new AjaxChartModel<List<HomeOffModel>, ChartModel[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<HomeOffModel>(),
                chart = new ChartModel[2]
            };

            try {
                var offKeys = _fsuKeyService.GetAllKeysAsList();
                var allFsus = from fsu in _workContext.RoleFsus
                              join ok in offKeys on fsu.Current.Id equals ok.Id
                              join area in _workContext.RoleAreas on fsu.Current.AreaId equals area.Current.Id
                              select new {
                                  Area = area,
                                  Fsu = fsu,
                                  ChangedTime = ok.ChangeTime,
                                  Status = ok.Status
                              };

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = allFsus.Count(f => f.Status) };
                data.chart[1] = new ChartModel { index = 2, name = "离线", value = allFsus.Count(f => !f.Status) };

                var offFsus = allFsus.Where(f => !f.Status).OrderByDescending(f => f.ChangedTime).ToList();
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
                            name = offFsus[i].Fsu.Current.Name,
                            time = CommonHelper.DateTimeConverter(offFsus[i].ChangedTime),
                            interval = CommonHelper.IntervalConverter(offFsus[i].ChangedTime)
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
        public ActionResult DownloadHomeOff() {
            try {
                var offKeys = _fsuKeyService.GetAllKeysAsList().FindAll(k => !k.Status);
                var allFsus = from fsu in _workContext.RoleFsus
                              join area in _workContext.RoleAreas on fsu.Current.AreaId equals area.Current.Id
                              join ok in offKeys on fsu.Current.Id equals ok.Id into lt
                              from def in lt.DefaultIfEmpty()
                              select new {
                                  Area = area,
                                  Fsu = fsu,
                                  ChangedTime = def != null ? def.ChangeTime : DateTime.Now,
                                  Status = def != null ? def.Status : true
                              };

                var models = new List<HomeOffModel>();
                var offFsus = allFsus.Where(f => !f.Status).OrderByDescending(f => f.ChangedTime).ToList();
                for(int i = 0; i < offFsus.Count; i++) {
                    models.Add(new HomeOffModel {
                        index = i + 1,
                        area = offFsus[i].Area.ToString(),
                        station = offFsus[i].Fsu.Current.StationName,
                        room = offFsus[i].Fsu.Current.RoomName,
                        name = offFsus[i].Fsu.Current.Name,
                        time = CommonHelper.DateTimeConverter(offFsus[i].ChangedTime),
                        interval = CommonHelper.IntervalConverter(offFsus[i].ChangedTime)
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
                var offKeys = _fsuKeyService.GetAllKeysAsList();
                var allFsus = from fsu in _workContext.RoleFsus
                              join ok in offKeys on fsu.Current.Id equals ok.Id
                              select new { Fsu = fsu, Ok = ok };

                var unStations = new List<IdValuePair<Station, DateTime>>();
                foreach(var station in _workContext.RoleStations) {
                    if(!allFsus.Any(f => f.Fsu.Current.StationId == station.Current.Id && f.Ok.Status)) {
                        var staFsus = allFsus.Where(f => f.Fsu.Current.StationId == station.Current.Id);
                        if(staFsus.Any()) {
                            unStations.Add(new IdValuePair<Station, DateTime> {
                                Id = station.Current,
                                Value = staFsus.Max(f => f.Ok.ChangeTime)
                            });
                        }
                    }
                }

                data.chart[0] = new ChartModel { index = 1, name = "正常", value = _workContext.RoleStations.Count - unStations.Count };
                data.chart[1] = new ChartModel { index = 2, name = "断站", value = unStations.Count };

                var stations = (from station in unStations
                                join area in _workContext.RoleAreas on station.Id.AreaId equals area.Current.Id
                                select new {
                                    Station = station,
                                    Area = area
                                }).ToList();

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
        [Authorize]
        public ActionResult DownloadHomeUnconnected() {
            try {
                var offKeys = _fsuKeyService.GetAllKeysAsList();
                var allFsus = from fsu in _workContext.RoleFsus
                              join ok in offKeys on fsu.Current.Id equals ok.Id
                              select new { Fsu = fsu, Ok = ok };

                var unStations = new List<IdValuePair<Station, DateTime>>();
                foreach(var station in _workContext.RoleStations) {
                    if(!allFsus.Any(f => f.Fsu.Current.StationId == station.Current.Id && f.Ok.Status)) {
                        var staFsus = allFsus.Where(f => f.Fsu.Current.StationId == station.Current.Id);
                        if(staFsus.Any()) {
                            unStations.Add(new IdValuePair<Station, DateTime> {
                                Id = station.Current,
                                Value = staFsus.Max(f => f.Ok.ChangeTime)
                            });
                        }
                    }
                }

                var stations = (from station in unStations
                                join area in _workContext.RoleAreas on station.Id.AreaId equals area.Current.Id
                                select new {
                                    Station = station,
                                    Area = area
                                }).ToList();

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

        private List<AlmStore<ActAlm>> GetActAlmStore(string node, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project) {
            var stores = new List<AlmStore<ActAlm>>();
            if(node == "root") {
                stores = _workContext.ActAlmStore;
            } else {
                var keys = Common.SplitKeys(node);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            stores = _workContext.ActAlmStore.FindAll(a => current.Keys.Contains(a.Area.Id));
                    } else if(nodeType == EnmOrganization.Station) {
                        var alarms = _actAlmService.GetAlmsInStationAsList(id);
                        stores = _workContext.GetActAlmStore(alarms);
                    } else if(nodeType == EnmOrganization.Room) {
                        var alarms = _actAlmService.GetAlmsInRoomAsList(id);
                        stores = _workContext.GetActAlmStore(alarms);
                    } else if(nodeType == EnmOrganization.Device) {
                        var alarms = _actAlmService.GetAlmsInDeviceAsList(id);
                        stores = _workContext.GetActAlmStore(alarms);
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
                stores = stores.FindAll(s => almlevel.Contains((int)s.Current.AlmLevel));

            if(confirm == "confirm")
                stores = stores.FindAll(a => a.ExtSet1 != null && a.ExtSet1.Confirmed == EnmConfirm.Confirmed);

            if(confirm == "unconfirm")
                stores = stores.FindAll(a => a.ExtSet1 == null || a.ExtSet1.Confirmed == EnmConfirm.Unconfirmed);

            if(project == "project")
                stores = stores.FindAll(a => a.ExtSet1 != null && !string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            if(project == "unproject")
                stores = stores.FindAll(a => a.ExtSet1 == null || string.IsNullOrWhiteSpace(a.ExtSet1.ProjectId));

            return stores;
        }

        private List<ChartModel> GetActAlmChart1(List<AlmStore<ActAlm>> stores) {
            var models = new List<ChartModel>();
            if(stores != null && stores.Count > 0) {
                var groups = stores.GroupBy(s => s.Current.AlmLevel).OrderBy(g => g.Key);
                foreach(var group in groups) {
                    models.Add(new ChartModel {
                        index = (int)group.Key,
                        name = Common.GetAlarmLevelDisplay(group.Key),
                        value = group.Count(),
                        comment = ""
                    });
                }
            }

            return models;
        }

        private List<ChartModel> GetActAlmChart2(string node, List<AlmStore<ActAlm>> stores) {
            var models = new List<ChartModel>();
            if(stores != null && stores.Count > 0) {
                if(node == "root") {
                    #region root
                    var roots = _workContext.RoleAreas.FindAll(a => !a.HasParents);
                    foreach(var root in roots) {
                        var curstores = stores.FindAll(s => root.Keys.Contains(s.Current.AreaId));
                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = root.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = root.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = root.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = root.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                    }
                    #endregion
                } else {
                    var keys = Common.SplitKeys(node);
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
                                        var curstores = stores.FindAll(s => child.Keys.Contains(s.Current.AreaId));
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = child.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = child.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = child.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = child.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                                    }
                                } else if(current.Stations.Count > 0) {
                                    foreach(var station in current.Stations) {
                                        var curstores = stores.FindAll(s => s.Current.StationId == station.Current.Id);
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = station.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = station.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = station.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                                        models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = station.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                                    }
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station
                            var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                            if(current != null && current.Rooms.Count > 0) {
                                foreach(var room in current.Rooms) {
                                    var curstores = stores.FindAll(m => m.Current.RoomId == room.Current.Id);
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = room.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = room.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = room.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = room.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Room) {
                            #region room
                            var current = _workContext.RoleRooms.Find(r => r.Current.Id == id);
                            if(current != null && current.Devices.Count > 0) {
                                foreach(var device in current.Devices) {
                                    var curstores = stores.FindAll(s => s.Current.DeviceId == device.Current.Id);
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = device.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = device.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = device.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                                    models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = device.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Device) {
                            #region device
                            var current = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                            if(current != null) {
                                var curstores = stores.FindAll(s => s.Current.DeviceId == current.Current.Id);
                                models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level1, name = current.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level1), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level1) });
                                models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level2, name = current.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level2), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level2) });
                                models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level3, name = current.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level3), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level3) });
                                models.Add(new ChartModel { index = (int)EnmAlarmLevel.Level4, name = current.Current.Name, value = curstores.Count(s => s.Current.AlmLevel == EnmAlarmLevel.Level4), comment = Common.GetAlarmLevelDisplay(EnmAlarmLevel.Level4) });
                            }
                            #endregion
                        }
                    }
                }
            }

            return models;
        }

        private List<PointStore<Point>> GetActPoints(string node, int[] types) {
            var stores = new List<PointStore<Point>>();

            if(node == "root") {
                stores = this.GetRssPoints(node, EnmOrganization.Area);
            } else {
                var keys = Common.SplitKeys(node);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Device) {
                        var current = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                        if(current != null && current.Protocol != null) {
                            var area = _workContext.RoleAreas.Find(a => a.Current.Id == current.Current.AreaId);
                            if(area != null) {
                                var profile = _workContext.Profile ?? new ProfileValues() { RssPoints = new List<RssPoint>() };
                                var matchs = profile.ToRssHashSet();
                                foreach(var point in current.Protocol.Points) {
                                    var key = string.Format("{0}-{1}", current.Current.Id, point.Id);
                                    stores.Add(new PointStore<Point>() {
                                        Current = point,
                                        RssPoint = matchs.Contains(key),
                                        RssFrom = false,
                                        Device = current.Current,
                                        Area = area.Current,
                                        AreaFullName = area.ToString()
                                    });
                                }
                            }
                        }
                    } else {
                        stores = this.GetRssPoints(id, nodeType);
                    }
                }
            }

            stores = stores.FindAll(p => types.Contains((int)p.Current.Type)).OrderBy(p => p.Current.Type).ToList();
            return stores;
        }

        private List<PointStore<Point>> GetRssPoints(string node, EnmOrganization type) {
            var stores = new List<PointStore<Point>>();
            if(_workContext.Profile == null) return stores;
            if(_workContext.Profile.RssPoints.Count == 0) return stores;

            stores = (from rss in _workContext.Profile.RssPoints
                      join point in _workContext.Points on rss.point equals point.Id
                      join device in _workContext.RoleDevices on rss.device equals device.Current.Id
                      join area in _workContext.RoleAreas on device.Current.AreaId equals area.Current.Id
                      select new PointStore<Point> {
                          Current = point,
                          Device = device.Current,
                          Area = area.Current,
                          RssPoint = true,
                          RssFrom = true,
                          AreaFullName = area.ToString()
                      }).ToList();

            if(node == "root") return stores;

            if(type == EnmOrganization.Area) {
                var current = _workContext.RoleAreas.Find(a => a.Current.Id == node);
                if(current != null) stores = stores.FindAll(p => current.Keys.Contains(p.Area.Id));
            } else if(type == EnmOrganization.Station) {
                stores = stores.FindAll(p => p.Device.StationId == node);
            } else if(type == EnmOrganization.Room) {
                stores = stores.FindAll(p => p.Device.RoomId == node);
            }

            return stores;
        }

        #endregion

    }
}