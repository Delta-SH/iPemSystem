using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using HsDomain = iPem.Core.Domain.History;
using HsSrv = iPem.Services.History;
using MsDomain = iPem.Core.Domain.Master;
using MsSrv = iPem.Services.Master;
using RsDomain = iPem.Core.Domain.Resource;
using RsSrv = iPem.Services.Resource;
using iPem.Site.Models.BI;

namespace iPem.Site.Controllers {
    [Authorize]
    public class HomeController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.INoticeService _noticeService;
        private readonly MsSrv.INoticeInUserService _noticeInUserService;
        private readonly MsSrv.IDictionaryService _dictionaryService;
        private readonly MsSrv.IPointService _msPointService;
        private readonly MsSrv.IProfileService _msProfileService;
        private readonly MsSrv.IAppointmentService _msAppointmentService;
        private readonly MsSrv.IProjectService _msProjectsService;
        private readonly HsSrv.IActAlmService _hsActAlmService;
        private readonly HsSrv.IAlmExtendService _hsAlmExtendService;
        private readonly HsSrv.IActValueService _hsActValueService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;

        #endregion

        #region Ctor

        public HomeController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.INoticeService noticeService,
            MsSrv.INoticeInUserService noticeInUserService,
            MsSrv.IDictionaryService dictionaryService,
            MsSrv.IPointService msPointService,
            MsSrv.IProfileService msProfileService,
            MsSrv.IAppointmentService msAppointmentService,
            MsSrv.IProjectService msProjectsService,
            HsSrv.IActAlmService hsActAlmService,
            HsSrv.IAlmExtendService hsAlmExtendService,
            HsSrv.IActValueService hsActValueService,
            RsSrv.ILogicTypeService rsLogicTypeService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._dictionaryService = dictionaryService;
            this._msPointService = msPointService;
            this._msProfileService = msProfileService;
            this._msAppointmentService = msAppointmentService;
            this._msProjectsService = msProjectsService;
            this._hsActAlmService = hsActAlmService;
            this._hsAlmExtendService = hsAlmExtendService;
            this._hsActValueService = hsActValueService;
            this._rsLogicTypeService = rsLogicTypeService;
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
            ViewBag.Control = _workContext.AssociatedOperations.ContainsKey(EnmOperation.Control);
            ViewBag.Adjust = _workContext.AssociatedOperations.ContainsKey(EnmOperation.Adjust);
            return View();
        }

        public ActionResult ActiveAlarm() {
            ViewBag.BarIndex = 2;
            ViewBag.MenuVisible = false;
            ViewBag.Confirm = _workContext.AssociatedOperations.ContainsKey(EnmOperation.Confirm);
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
            ViewBag.Current = _workContext.CurrentUser;
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
                var dic = _dictionaryService.GetDictionary((int)EnmDictionary.Ts);
                if(dic == null || string.IsNullOrWhiteSpace(dic.ValuesJson))
                    return Json(data, JsonRequestBehavior.AllowGet);

                var config = JsonConvert.DeserializeObject<TsValues>(dic.ValuesJson);
                if(config.basic == null || !config.basic.Contains(1))
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.level == null || config.level.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(config.content == null || config.content.Length == 0)
                    return Json(data, JsonRequestBehavior.AllowGet);

                var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
                if(config.stationtypes != null && config.stationtypes.Length > 0)
                    devices = devices.FindAll(d => config.stationtypes.Contains(d.Station.StaTypeId));

                if(config.roomtypes != null && config.roomtypes.Length > 0)
                    devices = devices.FindAll(d => config.roomtypes.Contains(d.Room.RoomTypeId));

                if(config.devicetypes != null && config.devicetypes.Length > 0)
                    devices = devices.FindAll(d => config.devicetypes.Contains(d.Current.DeviceTypeId));

                var points = _workContext.AssociatedPointAttributes.Values.ToList();
                if(config.pointtypes != null && config.pointtypes.Length > 0)
                    points = points.FindAll(p => config.pointtypes.Contains((int)p.Current.Type));
                else
                    points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

                if(config.logictypes != null && config.logictypes.Length > 0)
                    points = points.FindAll(p => config.logictypes.Contains(p.LogicType.Id));

                if(!string.IsNullOrWhiteSpace(config.pointnames)) {
                    var names = Common.SplitCondition(config.pointnames);
                    if(names.Length > 0)
                        points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
                }

                var start = DateTime.Now.AddSeconds(-30); 
                var end = DateTime.Now;
                if(Session["SpeechScanTime"] == null)
                    Session["SpeechScanTime"] = start.Ticks;
                else
                    start = new DateTime(Convert.ToInt64(Session["SpeechScanTime"]));
                
                var alarms = _hsActAlmService.GetActAlmsByTime(start, end).Where(a => config.level.Contains((int)a.AlmLevel));
                if(config.basic.Contains(3)) 
                    alarms = alarms.Where(a => string.IsNullOrWhiteSpace(a.ProjectId));

                var result = from alarm in alarms
                             join point in points on alarm.PointId equals point.Current.Id
                             join device in devices on alarm.DeviceId equals device.Current.Id
                             orderby alarm.StartTime descending
                             select new {
                                 Current = alarm,
                                 Point = point,
                                 Device = device,
                             };

                foreach(var model in result) {
                    var contents = new List<string>();
                    if(config.content.Contains(1))
                        contents.Add(string.Join("", _workContext.GetParentsInArea(model.Device.Area).Select(a => a.Name)));

                    if(config.content.Contains(2))
                        contents.Add(string.Join("", _workContext.GetParentsInStation(model.Device.Station).Select(a => a.Name)));

                    if(config.content.Contains(3))
                        contents.Add(model.Device.Room.Name);

                    if(config.content.Contains(4))
                        contents.Add(model.Device.Current.Name);

                    if(config.content.Contains(5))
                        contents.Add(model.Point.Current.Name);

                    if(config.content.Contains(6))
                        contents.Add(CommonHelper.DateTimeConverter(model.Current.StartTime));

                    if(config.content.Contains(7))
                        contents.Add(string.Format("发生{0}", Common.GetAlarmLevelDisplay(model.Current.AlmLevel)));

                    if(config.content.Contains(8) && !string.IsNullOrWhiteSpace(model.Current.AlmDesc))
                        contents.Add(model.Current.AlmDesc);

                    data.data.Add(string.Join("，", contents));
                }

                if(!config.basic.Contains(2))
                    Session["SpeechScanTime"] = end.Ticks;

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var menus = _workContext.AssociatedMenus;
                if(menus != null && menus.Count > 0) {
                    var roots = new List<MsDomain.Menu>();
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

        private void MenusRecursion(List<MsDomain.Menu> menus, int pid, TreeModel node) {
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

                var user = _workContext.CurrentUser;
                var notices = _noticeService.GetNotices(user.Id);
                var noticesInUser = _noticeInUserService.GetNoticesInUser(user.Id);

                var models = (from notice in notices
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
                             }).ToList();

                if("all".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    if(models.Any()) {
                        var result = new PagedList<NoticeModel>(models, start / limit, limit);
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(models);
                        }
                    }
                } else if("readed".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var readed = models.FindAll(m => m.readed);
                    if(readed.Count > 0) {
                        var result = new PagedList<NoticeModel>(readed, start / limit, limit);
                        if(result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            data.data.AddRange(readed);
                        }
                    }
                } else if("unread".Equals(listModel, StringComparison.CurrentCultureIgnoreCase)) {
                    var unread = models.FindAll(m => !m.readed);
                    if(unread.Count > 0) {
                        var result = new PagedList<NoticeModel>(unread, start / limit, limit);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var unreadsCount = _noticeService.GetUnreadCount(_workContext.CurrentUser.Id);
                if(unreadsCount > 0) {
                    data.message = "200 Ok";
                    data.total = unreadsCount;
                    data.data = unreadsCount;
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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

                var result = new List<MsDomain.NoticeInUser>();
                var noticesInUser = _noticeInUserService.GetNoticesInUser(_workContext.CurrentUser.Id).ToList();
                foreach(var notice in notices) {
                    var target = noticesInUser.Find(n => n.NoticeId == new Guid(notice));
                    if(target == null) continue;

                    target.Readed = "readed".Equals(status,StringComparison.CurrentCultureIgnoreCase);
                    target.ReadTime = target.Readed ? DateTime.Now : default(DateTime);
                    result.Add(target);
                }

                if(result.Count > 0)
                    _noticeInUserService.UpdateNoticesInUsers(result);

                return Json(new AjaxResultModel { success= true, code =200, message ="Ok" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetOrganization(string node, string id, int? type) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(id == "root") {
                    #region root organization
                    var dict = _workContext.AssociatedAreas.ToDictionary(k => k.AreaId, v => v.Name);
                    var roots = new List<RsDomain.Area>();
                    foreach(var area in _workContext.AssociatedAreas) {
                        if(!dict.ContainsKey(area.ParentId))
                            roots.Add(area);
                    }

                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = string.Format("ext-area-{0}", roots[i].AreaId),
                                text = roots[i].Name,
                                selected = false,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false,
                                attributes = new List<CustomAttribute>() { 
                                    new CustomAttribute { key = "id", value = roots[i].AreaId },
                                    new CustomAttribute { key = "type", value = ((int)EnmOrganization.Area).ToString() } 
                                }
                            };

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(id) && type.HasValue) {
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type.Value) ? (EnmOrganization)type.Value : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        #region area organization
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedAreaAttributes[id];
                            if(current.HasChildren) {
                                data.success = true;
                                data.message = "200 Ok";
                                data.total = current.FirstChildren.Count;
                                for(var i = 0; i < current.FirstChildren.Count; i++) {
                                    var root = new TreeModel {
                                        id = string.Format("ext-area-{0}", current.FirstChildren[i].AreaId),
                                        text = current.FirstChildren[i].Name,
                                        selected = false,
                                        icon = Icons.Diqiu,
                                        expanded = false,
                                        leaf = false,
                                        attributes = new List<CustomAttribute>() { 
                                            new CustomAttribute { key = "id", value = current.FirstChildren[i].AreaId },
                                            new CustomAttribute { key = "type", value = ((int)EnmOrganization.Area).ToString() } 
                                        }
                                    };

                                    data.data.Add(root);
                                }
                            } else {
                                var stations = _workContext.AssociatedStations.FindAll(s => s.AreaId == id);
                                var dict = stations.ToDictionary(k => k.Id, v => v.Name);
                                var roots = new List<RsDomain.Station>();
                                foreach(var sta in stations) {
                                    if(!dict.ContainsKey(sta.ParentId))
                                        roots.Add(sta);
                                }

                                if(roots.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = roots.Count;
                                    for(var i = 0; i < roots.Count; i++) {
                                        var root = new TreeModel {
                                            id = string.Format("ext-station-{0}", roots[i].Id),
                                            text = roots[i].Name,
                                            selected = false,
                                            icon = Icons.Juzhan,
                                            expanded = false,
                                            leaf = false,
                                            attributes = new List<CustomAttribute>() { 
                                                new CustomAttribute { key = "id", value = roots[i].Id },
                                                new CustomAttribute { key = "type", value = ((int)EnmOrganization.Station).ToString() } 
                                            }
                                        };

                                        data.data.Add(root);
                                    }
                                }
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmOrganization.Station) {
                        #region station organization
                        if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedStationAttributes[id];
                            if(current.HasChildren) {
                                data.success = true;
                                data.message = "200 Ok";
                                data.total = current.FirstChildren.Count;
                                for(var i = 0; i < current.FirstChildren.Count; i++) {
                                    var root = new TreeModel {
                                        id = string.Format("ext-station-{0}", current.FirstChildren[i].Id),
                                        text = current.FirstChildren[i].Name,
                                        selected = false,
                                        icon = Icons.Juzhan,
                                        expanded = false,
                                        leaf = false,
                                        attributes = new List<CustomAttribute>() { 
                                            new CustomAttribute { key = "id", value = current.FirstChildren[i].Id },
                                            new CustomAttribute { key = "type", value = ((int)EnmOrganization.Station).ToString() } 
                                        }
                                    };

                                    data.data.Add(root);
                                }
                            } else {
                                var rooms = _workContext.AssociatedRooms.FindAll(r => r.StationId == id);
                                if(rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = rooms.Count;
                                    for(var i = 0; i < rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = string.Format("ext-room-{0}", rooms[i].Id),
                                            text = rooms[i].Name,
                                            selected = false,
                                            icon = Icons.Room,
                                            expanded = false,
                                            leaf = false,
                                            attributes = new List<CustomAttribute>() { 
                                            new CustomAttribute { key = "id", value = rooms[i].Id },
                                            new CustomAttribute { key = "type", value = ((int)EnmOrganization.Room).ToString() } 
                                        }
                                        };

                                        data.data.Add(root);
                                    }
                                }
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmOrganization.Room) {
                        #region room organization
                        var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == id);
                        if(devices.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = devices.Count;
                            for(var i = 0; i < devices.Count; i++) {
                                var root = new TreeModel {
                                    id = string.Format("ext-device-{0}", devices[i].Id),
                                    text = devices[i].Name,
                                    selected = false,
                                    icon = Icons.Device,
                                    expanded = false,
                                    leaf = true,
                                    attributes = new List<CustomAttribute>() { 
                                        new CustomAttribute { key = "id", value = devices[i].Id },
                                        new CustomAttribute { key = "type", value = ((int)EnmOrganization.Device).ToString() } 
                                    }
                                };

                                data.data.Add(root);
                            }
                        }
                        #endregion
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult SearchOrganization(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var areaMatchs = _workContext.AssociatedAreas.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in areaMatchs) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(string.Format("ext-area-{0}", parent.AreaId));
                            }
                        }

                        paths.Add(string.Format("ext-area-{0}", match.AreaId));
                        data.data.Add(paths.ToArray());
                    }

                    var staMatchs = _workContext.AssociatedStations.FindAll(s => s.Name.ToLower().Contains(text));
                    foreach(var match in staMatchs) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(string.Format("ext-area-{0}", parent.AreaId));
                            }
                        }
                        paths.Add(string.Format("ext-area-{0}", match.AreaId));

                        if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                            var current = _workContext.AssociatedStationAttributes[match.Id];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(string.Format("ext-station-{0}", parent.Id));
                            }
                        }

                        paths.Add(string.Format("ext-station-{0}", match.Id));
                        data.data.Add(paths.ToArray());
                    }

                    var roomMatchs = _workContext.AssociatedRooms.FindAll(r => r.Name.ToLower().Contains(text));
                    foreach(var match in roomMatchs) {
                        var paths = new List<string>();
                        var root = _workContext.AssociatedStations.Find(s => s.Id == match.StationId);
                        if(root != null) {
                            if(_workContext.AssociatedAreaAttributes.ContainsKey(root.AreaId)) {
                                var current = _workContext.AssociatedAreaAttributes[root.AreaId];
                                if(current.HasParents) {
                                    foreach(var parent in current.Parents)
                                        paths.Add(string.Format("ext-area-{0}", parent.AreaId));
                                }
                            }
                            paths.Add(string.Format("ext-area-{0}", root.AreaId));

                            if(_workContext.AssociatedStationAttributes.ContainsKey(root.Id)) {
                                var current = _workContext.AssociatedStationAttributes[root.Id];
                                if(current.HasParents) {
                                    foreach(var parent in current.Parents)
                                        paths.Add(string.Format("ext-station-{0}", parent.Id));
                                }
                            }
                            paths.Add(string.Format("ext-station-{0}", root.Id));
                        }

                        paths.Add(string.Format("ext-room-{0}", match.Id));
                        data.data.Add(paths.ToArray());
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
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

                var actAlms = _hsActAlmService.GetActAlmsByTime(start, end);
                if(actAlms.TotalCount > 0) {
                    var matchs = new Dictionary<string, string>();
                    foreach(var dev in _workContext.AssociatedDevices) {
                        matchs[dev.Id] = dev.Name;
                    }

                    var count = actAlms.Count(m => matchs.ContainsKey(m.DeviceId));
                    if(count > 0) {
                        data.message = "200 Ok";
                        data.total = count;
                        data.data = count;
                    }
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActAlarms(string nodeid, int nodetype, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxChartModel<List<ActAlmModel>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            levelValue = (int)models[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            comment = models[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            frequency = models[i].Current.Frequency,
                            project = models[i].Current.ProjectId,
                            confirmedstatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedtime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetActAlmChart1(models);
                    data.chart[1] = this.GetActAlmChart2(models);
                    data.chart[2] = this.GetActAlmChart3(nodeid, nodetype, models);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadActAlms(string nodeid, int nodetype, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project) {
            try {
                var models = new List<ActAlmModel>();
                var cached = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new ActAlmModel {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            levelValue = (int)cached[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            comment = cached[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            frequency = cached[i].Current.Frequency,
                            project = cached[i].Current.ProjectId,
                            confirmedstatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedtime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer,
                            background = Common.GetAlarmLevelColor(cached[i].Current.AlmLevel)
                        });
                    }
                }

                using(var ms = _excelManager.Export<ActAlmModel>(models, "实时告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetPointRss() {
            var data = new AjaxDataModel<PointRss> {
                success = true,
                message = "无数据",
                total = 0,
                data = new PointRss()
            };

            try {
                var profile = _workContext.CurrentProfile;
                if(profile != null 
                    && profile.PointRss != null)
                    data.data = profile.PointRss;
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SavePointRss(string[] stationtypes, string[] roomtypes, string[] devicetypes, string[] logictypes, int[] pointtypes, string pointnames) {
            try {
                var profile = _workContext.CurrentProfile ?? new ProfileValues();
                profile.PointRss = new PointRss() {
                    stationtypes = stationtypes,
                    roomtypes = roomtypes,
                    devicetypes = devicetypes,
                    logictypes = logictypes,
                    pointtypes = pointtypes,
                    pointnames = pointnames
                };

                _msProfileService.SaveUserProfile(new MsDomain.UserProfile {
                    UserId = _workContext.CurrentUser.Id,
                    ValuesJson = JsonConvert.SerializeObject(profile),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });
                _workContext.CurrentProfile = profile;

                this.RequestRemoveRssPointsCache();
                return Json(new AjaxResultModel { success = true, code = 200, message = "订阅成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestActPoints(string nodeid, int nodetype, int[] types, int start, int limit) {
            var data = new AjaxDataModel<List<ActPointModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActPointModel>()
            };

            try {
                if(types != null && types.Length > 0) {
                    var type = Enum.IsDefined(typeof(EnmOrganization), nodetype) ? (EnmOrganization)nodetype : EnmOrganization.Device;
                    if(type == EnmOrganization.Device) {
                        #region single device
                        if(_workContext.AssociatedDeviceAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedDeviceAttributes[nodeid];
                            var points = _msPointService.GetPoints(current.Current.Id, types);
                            var logics = _rsLogicTypeService.GetAllLogicTypes();
                            var sublogics = _rsLogicTypeService.GetAllSubLogicTypes();

                            if(points.TotalCount > 0) {
                                data.message = "200 Ok";
                                data.total = points.TotalCount;

                                var end = start + limit;
                                if(end > points.TotalCount)
                                    end = points.TotalCount;

                                var records = new List<MsDomain.Point>();
                                var orders = points.OrderBy(p => p.Type).ToArray();
                                for(int i = start; i < end; i++)
                                    records.Add(orders[i]);

                                #region request active values
                                try {
                                    if(current.Fsu != null) {
                                        var package = new ActDataTemplate() {
                                                Id = current.Fsu.Id,
                                                Code = current.Fsu.Code,
                                                Values = new List<ActDataDeviceTemplate>() {
                                                    new ActDataDeviceTemplate() {
                                                        Id = current.Current.Id,
                                                        Code = current.Current.Code,
                                                        Values = records.Select(r => r.Id).ToList()
                                                    }
                                                }
                                            };

                                            var actData = BIPack.RequestActiveData(_workContext.CurrentWsValues, package);
                                            if(actData != null && actData.Result == EnmBIResult.SUCCESS) {
                                                var values = new List<HsDomain.ActValue>();
                                                foreach(var v in actData.Values) {
                                                    foreach(var a in v.Values) {
                                                        values.Add(new HsDomain.ActValue() {
                                                            DeviceId = v.Id,
                                                            DeviceCode = v.Code,
                                                            PointId = a.Id,
                                                            MeasuredVal = a.MeasuredVal,
                                                            SetupVal = a.SetupVal,
                                                            Status = Enum.IsDefined(typeof(EnmPointStatus), a.Status) ? (EnmPointStatus)a.Status : EnmPointStatus.Invalid,
                                                            RecordTime = a.RecordTime
                                                        });
                                                    }
                                                }

                                                if(values.Count > 0)
                                                    _hsActValueService.AddValues(values);
                                            }
                                    }
                                } catch(Exception exc) {
                                    _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                                }
                                #endregion

                                var actValues = _hsActValueService.GetActValues(current.Current.Id);
                                var result = from rcd in records
                                             join sublogic in sublogics on rcd.SubLogicTypeId equals sublogic.Id
                                             join logic in logics on sublogic.LogicTypeId equals logic.Id
                                             join av in actValues on rcd.Id equals av.PointId into temp
                                             from defaultAv in temp.DefaultIfEmpty()
                                             select new {
                                                 Point = rcd,
                                                 SubLogic = sublogic,
                                                 Logic = logic,
                                                 Value = defaultAv
                                             };

                                foreach(var point in result) {
                                    var value = point.Value != null ? point.Value.MeasuredVal : 0f;
                                    var status = point.Value != null ? point.Value.Status : EnmPointStatus.Invalid;
                                    var rectime = point.Value != null ? point.Value.RecordTime : DateTime.Now;

                                    data.data.Add(new ActPointModel {
                                        key = string.Format("{0}${1}", current.Current.Id, point.Point.Id),
                                        area = current.Area.Name,
                                        station = current.Station.Name,
                                        room = current.Room.Name,
                                        devType = current.Type.Name,
                                        devId = current.Current.Id,
                                        devName = current.Current.Name,
                                        logic = point.Logic.Name,
                                        id = point.Point.Id,
                                        name = point.Point.Name,
                                        type = (int)point.Point.Type,
                                        typeDisplay = Common.GetPointTypeDisplay(point.Point.Type),
                                        value = value,
                                        valueDisplay = Common.GetValueDisplay(point.Point.Type,value,point.Point.Unit),
                                        status = (int)status,
                                        statusDisplay = Common.GetPointStatusDisplay(status),
                                        timestamp = CommonHelper.TimeConverter(rectime)
                                    });
                                }
                            }
                        }
                        #endregion
                    } else {
                        #region multiple devices
                        var points = this.GetRssPoints(nodeid, nodetype).FindAll(p=>types.Contains((int)p.Value.Current.Type));
                        if(points.Count > 0) {
                            data.message = "200 Ok";
                            data.total = points.Count;

                            var end = start + limit;
                            if(end > points.Count)
                                end = points.Count;

                            var records = new List<IdValuePair<DeviceAttributes, PointAttributes>>();
                            for(int i = start; i < end; i++)
                                records.Add(points[i]);

                            #region request active values

                            var devKeys = new List<string>();
                            try {
                                var fsuGroup = records.FindAll(r => r.Id.Fsu != null).GroupBy(g => new { g.Id.Fsu.Id, g.Id.Fsu.Code });
                                foreach(var fsu in fsuGroup) {
                                    var package = new ActDataTemplate() { Id = fsu.Key.Id, Code = fsu.Key.Code, Values = new List<ActDataDeviceTemplate>() };
                                    var devGroup = fsu.GroupBy(d => new { d.Id.Current.Id, d.Id.Current.Code });
                                    foreach(var dev in devGroup) {
                                        devKeys.Add(dev.Key.Id); 
                                        package.Values.Add(new ActDataDeviceTemplate() {
                                            Id = dev.Key.Id,
                                            Code = dev.Key.Code,
                                            Values = dev.Select(d => d.Value.Current.Id).ToList()
                                        });
                                    }

                                    var actData = BIPack.RequestActiveData(_workContext.CurrentWsValues, package);
                                    if(actData != null && actData.Result == EnmBIResult.SUCCESS) {
                                        var values = new List<HsDomain.ActValue>();
                                        foreach(var v in actData.Values) {
                                            foreach(var a in v.Values) {
                                                values.Add(new HsDomain.ActValue() {
                                                    DeviceId = v.Id,
                                                    DeviceCode = v.Code,
                                                    PointId = a.Id,
                                                    MeasuredVal = a.MeasuredVal,
                                                    SetupVal = a.SetupVal,
                                                    Status = Enum.IsDefined(typeof(EnmPointStatus), a.Status) ? (EnmPointStatus)a.Status : EnmPointStatus.Invalid,
                                                    RecordTime = a.RecordTime
                                                });
                                            }
                                        }

                                        if(values.Count > 0)
                                            _hsActValueService.AddValues(values);
                                    }
                                }
                            } catch(Exception exc) {
                                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                            }

                            #endregion

                            var actValues = _hsActValueService.GetActValues(devKeys.ToArray());
                            var result = from rcd in records
                                         join av in actValues on new { Device = rcd.Id.Current.Id, Point = rcd.Value.Current.Id } equals new { Device = av.DeviceId, Point = av.PointId } into temp
                                         from defaultAv in temp.DefaultIfEmpty()
                                         select new {
                                             Point = rcd,
                                             Value = defaultAv
                                         };

                            foreach(var point in result) {
                                var value = point.Value != null ? point.Value.MeasuredVal : 0f;
                                var status = point.Value != null ? point.Value.Status : EnmPointStatus.Invalid;
                                var rectime = point.Value != null ? point.Value.RecordTime : DateTime.Now;

                                data.data.Add(new ActPointModel {
                                    key = string.Format("{0}${1}", point.Point.Id.Current.Id, point.Point.Value.Current.Id),
                                    area = point.Point.Id.Area.Name,
                                    station = point.Point.Id.Station.Name,
                                    room = point.Point.Id.Room.Name,
                                    devType = point.Point.Id.Type.Name,
                                    devId = point.Point.Id.Current.Id,
                                    devName = point.Point.Id.Current.Name,
                                    logic = point.Point.Value.LogicType.Name,
                                    id = point.Point.Value.Current.Id,
                                    name = point.Point.Value.Current.Name,
                                    type = (int)point.Point.Value.Current.Type,
                                    typeDisplay = Common.GetPointTypeDisplay(point.Point.Value.Current.Type),
                                    value = value,
                                    valueDisplay = Common.GetValueDisplay(point.Point.Value.Current.Type, value, point.Point.Value.Current.Unit),
                                    status = (int)status,
                                    statusDisplay = Common.GetPointStatusDisplay(status),
                                    timestamp = CommonHelper.TimeConverter(rectime)
                                });
                            }
                        }
                        #endregion
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
        public JsonResult RequestRemoveRssPointsCache() {
            try {
                var key = Common.GetCachedKey(SiteCacheKeys.Site_RssPointsResultPattern, _workContext);
                if(_cacheManager.IsSet(key))
                    _cacheManager.Remove(key);

                return Json(new AjaxResultModel { success = true, code = 200, message = "Ok" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ConfirmAlarms(string[] ids) {
            try {
                if(ids == null || ids.Length == 0)
                    throw new ArgumentException("参数无效 id");

                var entities = new List<HsDomain.AlmExtend>();
                foreach(var id in ids) {
                    entities.Add(new HsDomain.AlmExtend {
                        Id = id,
                        ConfirmedStatus = EnmConfirmStatus.Confirmed,
                        ConfirmedTime = DateTime.Now,
                        Confirmer = _workContext.AssociatedEmployee.Name
                    });
                }

                _hsAlmExtendService.UpdateConfirm(entities);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警确认成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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

                var appointment = _msAppointmentService.GetAppointment(new Guid(id));
                if(appointment == null)
                    throw new iPemException("未找到数据对象");

                var project = _msProjectsService.GetProject(appointment.ProjectId);

                data.data.id = appointment.Id.ToString();
                data.data.startTime = CommonHelper.DateTimeConverter(appointment.StartTime);
                data.data.endTime = CommonHelper.DateTimeConverter(appointment.EndTime);
                data.data.projectId = appointment.ProjectId.ToString();
                data.data.projectName = project != null ? project.Name : data.data.projectId;
                data.data.creator = appointment.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(appointment.CreatedTime);
                data.data.comment = appointment.Comment;
                data.data.enabled = appointment.Enabled;
                return Json(data);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
                return Json(data);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ControlPoint(string device, string point, int ctrl) {
            try {
                if(!_workContext.AssociatedOperations.ContainsKey(EnmOperation.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device))
                    throw new ArgumentException("参数无效 device");

                if(string.IsNullOrWhiteSpace(point))
                    throw new ArgumentException("参数无效 point");

                if(!_workContext.AssociatedDeviceAttributes.ContainsKey(device))
                    throw new iPemException("未找到设备");

                if(!_workContext.AssociatedPointAttributes.ContainsKey(point))
                    throw new iPemException("未找到信号");

                var curDevice = _workContext.AssociatedDeviceAttributes[device];
                if(curDevice.Fsu == null) throw new iPemException("未找到Fsu");

                var curPoint = _workContext.AssociatedPointAttributes[point].Current;

                var package = new SetDataTemplate() {
                    Id = curDevice.Fsu.Id,
                    Code = curDevice.Fsu.Code,
                    Values = new List<SetDataDeviceTemplate>() {
                        new SetDataDeviceTemplate() {
                            Id = curDevice.Current.Id,
                            Code = curDevice.Current.Code,
                            Values = new List<TSemaphore>() {
                                new TSemaphore() {
                                    Id = curPoint.Id,
                                    Type = (int)Common.ConvertEnmPoint(curPoint.Type),
                                    MeasuredVal = 0,
                                    SetupVal = ctrl,
                                    Status = (int)EnmPointStatus.Normal,
                                    RecordTime = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = BIPack.SetPointData(_workContext.CurrentWsValues, package);
                if(result != null && result.Result == EnmBIResult.SUCCESS) {
                    if(result.Values != null) {
                        var devResult = result.Values.Find(d => d.Id == curDevice.Current.Id);
                        if(devResult != null && devResult.Success.Contains(curPoint.Id))
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
        public JsonResult AdjustPoint(string device, string point, double adjust) {
            try {
                if(!_workContext.AssociatedOperations.ContainsKey(EnmOperation.Control))
                    return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

                if(string.IsNullOrWhiteSpace(device))
                    throw new ArgumentException("参数无效 device");

                if(string.IsNullOrWhiteSpace(point))
                    throw new ArgumentException("参数无效 point");

                if(!_workContext.AssociatedDeviceAttributes.ContainsKey(device))
                    throw new iPemException("未找到设备");

                if(!_workContext.AssociatedPointAttributes.ContainsKey(point))
                    throw new iPemException("未找到信号");

                var curDevice = _workContext.AssociatedDeviceAttributes[device];
                if(curDevice.Fsu == null) throw new iPemException("未找到Fsu");

                var curPoint = _workContext.AssociatedPointAttributes[point].Current;

                var package = new SetDataTemplate() {
                    Id = curDevice.Fsu.Id,
                    Code = curDevice.Fsu.Code,
                    Values = new List<SetDataDeviceTemplate>() {
                        new SetDataDeviceTemplate() {
                            Id = curDevice.Current.Id,
                            Code = curDevice.Current.Code,
                            Values = new List<TSemaphore>() {
                                new TSemaphore() {
                                    Id = curPoint.Id,
                                    Type = (int)Common.ConvertEnmPoint(curPoint.Type),
                                    MeasuredVal = 0,
                                    SetupVal = adjust,
                                    Status = (int)EnmPointStatus.Normal,
                                    RecordTime = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = BIPack.SetPointData(_workContext.CurrentWsValues, package);
                if(result != null && result.Result == EnmBIResult.SUCCESS) {
                    if(result.Values != null) {
                        var devResult = result.Values.Find(d => d.Id == curDevice.Current.Id);
                        if(devResult != null && devResult.Success.Contains(curPoint.Id))
                            return Json(new AjaxResultModel { success = true, code = 200, message = "参数设置成功" });
                    }
                }

                throw new iPemException("参数设置失败");
            } catch(Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<AlmStore<HsDomain.ActAlm>> GetActAlmStore(string nodeid, int nodetype, string[] statype, string[] roomtype, string[] devtype, int[] almlevel, string[] logictype, string pointname, string confirm, string project) {
            if(string.IsNullOrWhiteSpace(nodeid)) 
                throw new ArgumentException("参数无效 id");

            var type = Enum.IsDefined(typeof(EnmOrganization), nodetype) ? (EnmOrganization)nodetype : EnmOrganization.Area;

            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();

            if(type == EnmOrganization.Area && nodeid != "root") {
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

            if(type == EnmOrganization.Station) {
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

            if(statype != null && statype.Length > 0)
                devices = devices.FindAll(d => statype.Contains(d.Station.StaTypeId));

            if(type == EnmOrganization.Room)
                devices = devices.FindAll(d => d.Room.Id == nodeid);

            if(roomtype != null && roomtype.Length > 0)
                devices = devices.FindAll(d => roomtype.Contains(d.Room.RoomTypeId));

            if(type == EnmOrganization.Device)
                devices = devices.FindAll(d => d.Current.Id == nodeid);

            if(devtype != null && devtype.Length > 0)
                devices = devices.FindAll(d => devtype.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
                points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictype != null && logictype.Length > 0)
                points = points.FindAll(p => logictype.Contains(p.LogicType.Id));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = ((almlevel != null && almlevel.Length > 0) ? _hsActAlmService.GetActAlmsByLevels(almlevel) : _hsActAlmService.GetAllActAlms()).ToList();

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
                          select new AlmStore<HsDomain.ActAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<ChartModel> GetActAlmChart1(List<AlmStore<HsDomain.ActAlm>> models) {
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

        private List<ChartModel> GetActAlmChart2(List<AlmStore<HsDomain.ActAlm>> models) {
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

        private List<ChartModel> GetActAlmChart3(string nodeid, int nodetype, List<AlmStore<HsDomain.ActAlm>> models) {
            var data = new List<ChartModel>();

            try {
                if(models != null && models.Count > 0) {
                    var type = Enum.IsDefined(typeof(EnmOrganization), nodetype) ? (EnmOrganization)nodetype : EnmOrganization.Area;
                    if(type == EnmOrganization.Area && nodeid == "root") {
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
                    } else if(type == EnmOrganization.Area) {
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
                    } else if(type == EnmOrganization.Station) {
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
                    } else if(type == EnmOrganization.Room) {
                        #region room
                        var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == nodeid);
                        foreach(var device in devices) {
                            var count = models.Count(m => m.Device.Current.Id == device.Id);
                            if(count > 0)
                                data.Add(new ChartModel { name = device.Name, value = count, comment = "" });
                        }
                        #endregion
                    } else if(type == EnmOrganization.Device) {
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
            }

            return data;
        }

        private List<IdValuePair<DeviceAttributes, PointAttributes>> GetRssPoints(string nodeid, int nodetype) {
            var key = Common.GetCachedKey(SiteCacheKeys.Site_RssPointsResultPattern, _workContext);

            if(_cacheManager.IsSet(key))
                return _cacheManager.Get<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key);

            var type = Enum.IsDefined(typeof(EnmOrganization), nodetype) ? (EnmOrganization)nodetype : EnmOrganization.Device;
            var points = _workContext.AssociatedRssPoints;
            if(type == EnmOrganization.Area && nodeid != "root") {
                if(_workContext.AssociatedAreaAttributes.ContainsKey(nodeid)) {
                    var attribute = _workContext.AssociatedAreaAttributes[nodeid];
                    var matchers = new Dictionary<string, string>();
                    matchers.Add(attribute.Current.AreaId, attribute.Current.Name);

                    if(attribute.HasChildren) {
                        foreach(var child in attribute.Children) {
                            matchers[child.AreaId] = child.Name;
                        }
                    }

                    points = points.FindAll(p => matchers.ContainsKey(p.Id.Area.AreaId));
                }
            }

            if(type == EnmOrganization.Station) {
                if(_workContext.AssociatedStationAttributes.ContainsKey(nodeid)) {
                    var attribute = _workContext.AssociatedStationAttributes[nodeid];
                    var matchers = new Dictionary<string, string>();
                    matchers.Add(attribute.Current.Id, attribute.Current.Name);

                    if(attribute.HasChildren) {
                        foreach(var child in attribute.Children) {
                            matchers[child.Id] = child.Name;
                        }
                    }

                    points = points.FindAll(p => matchers.ContainsKey(p.Id.Station.Id));
                }
            }

            if(type == EnmOrganization.Room)
                points = points.FindAll(p => p.Id.Room.Id == nodeid);

            points = points.OrderBy(p => p.Value.Current.Type).ToList();
            _cacheManager.Set<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key, points, CachedIntervals.Site_ResultIntervals);
            return points;
        }

        #endregion

    }
}