using iPem.Core;
using iPem.Core.Enum;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Extensions;
using HsDomain = iPem.Core.Domain.History;
using HsSrv = iPem.Services.History;
using MsDomain = iPem.Core.Domain.Master;
using MsSrv = iPem.Services.Master;
using Newtonsoft.Json;
using RsDomain = iPem.Core.Domain.Resource;
using RsSrv = iPem.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using iPem.Services.Common;
using iPem.Core.NPOI;
using iPem.Core.Caching;

namespace iPem.Site.Controllers {
    [Authorize]
    public class HomeController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IMenuService _menuService;
        private readonly MsSrv.INoticeService _noticeService;
        private readonly MsSrv.INoticeInUserService _noticeInUserService;
        private readonly MsSrv.IPointService _msPointService;
        private readonly HsSrv.IActAlmService _hsActAlmService;
        private readonly RsSrv.IDeviceTypeService _rsDeviceTypeService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;

        #endregion

        #region Ctor

        public HomeController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IMenuService menuService,
            MsSrv.INoticeService noticeService,
            MsSrv.INoticeInUserService noticeInUserService,
            MsSrv.IPointService msPointService,
            HsSrv.IActAlmService hsActAlmService,
            RsSrv.IDeviceTypeService rsDeviceTypeService,
            RsSrv.ILogicTypeService rsLogicTypeService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._menuService = menuService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._msPointService = msPointService;
            this._hsActAlmService = hsActAlmService;
            this._rsDeviceTypeService = rsDeviceTypeService;
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
            return View();
        }

        public ActionResult ActiveAlarm() {
            ViewBag.BarIndex = 2;
            ViewBag.MenuVisible = false;
            return View();
        }

        public ActionResult Notice() {
            ViewBag.BarIndex = 3;
            ViewBag.MenuVisible = false;
            return View();
        }

        public ActionResult UCenter() {
            ViewBag.BarIndex = 4;
            ViewBag.MenuVisible = false;
            ViewBag.Current = _workContext.CurrentUser;
            return View();
        }

        public ContentResult GetNavMenus() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var menus = _menuService.GetMenus(_workContext.CurrentRole.Id).ToList();
                if(menus.Count > 0) {
                    var _menus = menus.FindAll(m => m.LastId == 0).OrderBy(m => m.Index).ToList();
                    if(_menus.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = menus.Count;
                        for(var i = 0; i < _menus.Count; i++) {
                            var root = new TreeModel {
                                id = _menus[i].Id.ToString(),
                                text = _menus[i].Name,
                                href = string.IsNullOrWhiteSpace(_menus[i].Url) ? "javascript:void(0);" : string.Format("{0}?mid={1}", _menus[i].Url, _menus[i].Id),
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
                        href = string.IsNullOrWhiteSpace(_menus[i].Url) ? "javascript:void(0);" : string.Format("{0}?mid={1}", _menus[i].Url, _menus[i].Id),
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

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActAlarms(int start, int limit, string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname) {
            var data = new AjaxDataModel<List<ActAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>()
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var result = models.Skip(start).Take(limit);
                    foreach(var model in result) {
                        data.data.Add(new ActAlmModel {
                            id = model.alarm.Id,
                            level = Common.GetAlarmLevelDisplayName(model.alarm.AlmLevel),
                            start = CommonHelper.DateTimeConverter(model.alarm.StartTime),
                            area = string.Join(",", this.GetParentsInArea(model.area).Select(a => a.Name)),
                            station = string.Join(",", this.GetParentsInStation(model.station).Select(a => a.Name)),
                            room = model.room.Name,
                            devType = model.devicetype.Name,
                            device = model.device.Name,
                            logic = model.logic.Name,
                            point = model.point.Name,
                            comment = model.alarm.AlmDesc,
                            value = string.Format("{0:F2} {1}", model.alarm.StartValue, model.alarm.ValueUnit),
                            frequency = model.alarm.Frequency
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActChart1(int start, int limit, string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname) {
            return null;
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActChart2(int start, int limit, string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname) {
            return null;
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult RequestActChart3(int start, int limit, string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname) {
            return null;
        }

        private List<ActAlmStore> GetActAlmStore(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname) {
            var key = Common.GetCachedKey(UserCacheKeys.U_ActAlmResultPattern, _workContext);

            if(_cacheManager.IsSet(key))
                return _cacheManager.Get<List<ActAlmStore>>(key);

            if(string.IsNullOrWhiteSpace(nodeid)) 
                throw new ArgumentException("参数无效 id");

            var type = Enum.IsDefined(typeof(EnmOrganization), nodetype) ? (EnmOrganization)nodetype : EnmOrganization.Area;

            var currentAreas = new List<RsDomain.Area>();
            if(type == EnmOrganization.Area && nodeid != "root") {
                var current = _workContext.AssociatedAreas.Find(a => a.AreaId == nodeid);
                if(current != null) {
                    currentAreas.Add(current);
                    if(_workContext.AssociatedAreaAttributes.ContainsKey(current.AreaId)) {
                        var attribute = _workContext.AssociatedAreaAttributes[current.AreaId];
                        if(attribute.HasChildren) currentAreas.AddRange(attribute.Children);
                    }
                }
            } else {
                currentAreas = _workContext.AssociatedAreas;
            }

            if(currentAreas.Count == 0)
                return null;

            var currentStations = new List<RsDomain.Station>();
            if(type == EnmOrganization.Station) {
                var current = _workContext.AssociatedStations.Find(s => s.Id == nodeid);
                if(current != null) {
                    currentStations.Add(current);
                    if(_workContext.AssociatedStationAttributes.ContainsKey(current.Id)) {
                        var attribute = _workContext.AssociatedStationAttributes[current.Id];
                        if(attribute.HasChildren) currentStations.AddRange(attribute.Children);
                    }
                }
            } else {
                currentStations = _workContext.AssociatedStations;
            }

            if(statype != null && statype.Length > 0)
                currentStations = currentStations.FindAll(s => statype.Contains(s.StaTypeId));

            if(currentStations.Count == 0)
                return null;

            var currentRooms = new List<RsDomain.Room>();
            if(type == EnmOrganization.Room) {
                var current = _workContext.AssociatedRooms.Find(r => r.Id == nodeid);
                if(current != null) currentRooms.Add(current);
            } else {
                currentRooms = _workContext.AssociatedRooms;
            }

            if(roomtype != null && roomtype.Length > 0)
                currentRooms = currentRooms.FindAll(r => roomtype.Contains(r.RoomTypeId));

            if(currentRooms.Count == 0)
                return null;

            var currentDevices = new List<RsDomain.Device>();
            if(type == EnmOrganization.Device) {
                var current = _workContext.AssociatedDevices.Find(d => d.Id == nodeid);
                if(current != null) currentDevices.Add(current);
            } else {
                currentDevices = _workContext.AssociatedDevices;
            }

            if(devtype != null && devtype.Length > 0)
                currentDevices = currentDevices.FindAll(d => devtype.Contains(d.DeviceTypeId));

            if(currentDevices.Count == 0)
                return null;

            var currentPoints = _msPointService.GetPointsByType(new int[] { (int)EnmNode.AI, (int)EnmNode.DI }).ToList();
            if(logictype != null && logictype.Length > 0)
                currentPoints = currentPoints.FindAll(p => logictype.Contains(p.LogicTypeId));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = CommonHelper.ConditionSplit(pointname);
                if(names.Length > 0)
                    currentPoints = currentPoints.FindAll(p => CommonHelper.ConditionContain(p.Name, names));
            }

            var currentAlarms = (almlevel != null && almlevel.Length > 0) ? _hsActAlmService.GetActAlmsByLevels(almlevel) : _hsActAlmService.GetAllActAlms();

            var currentDevTypes = _rsDeviceTypeService.GetAllDeviceTypes();

            var currentLogicTypes = _rsLogicTypeService.GetAllLogicTypes();

            var models = (from alarm in currentAlarms
                          join point in currentPoints on alarm.PointId equals point.Id
                          join logic in currentLogicTypes on point.LogicTypeId equals logic.Id
                          join device in currentDevices on alarm.DeviceId equals device.Id
                          join detype in currentDevTypes on device.DeviceTypeId equals detype.Id
                          join room in currentRooms on device.RoomId equals room.Id
                          join station in currentStations on room.StationId equals station.Id
                          join area in currentAreas on station.AreaId equals area.AreaId
                          select new ActAlmStore {
                              alarm = alarm,
                              point = point,
                              logic = logic,
                              device = device,
                              devicetype = detype,
                              room = room,
                              station = station,
                              area = area
                          }).ToList();

            _cacheManager.Set<List<ActAlmStore>>(key, models, CachedIntervals.ActAlmIntervals);
            return models;
        }

        private List<RsDomain.Area> GetParentsInArea(RsDomain.Area current, bool include = true) {
            var result = new List<RsDomain.Area>();
            if(_workContext.AssociatedAreaAttributes.ContainsKey(current.AreaId))
                result.AddRange(_workContext.AssociatedAreaAttributes[current.AreaId].Parents);

            if(include)
                result.Add(current);

            return result;
        }

        private List<RsDomain.Station> GetParentsInStation(RsDomain.Station current, bool include = true) {
            var result = new List<RsDomain.Station>();
            if(_workContext.AssociatedStationAttributes.ContainsKey(current.Id))
                result.AddRange(_workContext.AssociatedStationAttributes[current.Id].Parents);

            if(include)
                result.Add(current);

            return result;
        }

        #endregion

    }
}