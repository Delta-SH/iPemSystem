using iPem.Core;
using iPem.Core.Enum;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Extensions;
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

namespace iPem.Site.Controllers {
    [Authorize]
    public class HomeController : Controller {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IMenuService _menuService;
        private readonly MsSrv.INoticeService _noticeService;
        private readonly MsSrv.INoticeInUserService _noticeInUserService;
        private readonly MsSrv.IAreaService _msAreaService;
        private readonly MsSrv.IStationService _msStationService;
        private readonly MsSrv.IRoomService _msRoomService;
        private readonly MsSrv.IDeviceService _msDeviceService;

        private readonly RsSrv.IAreaService _rsAreaService;
        private readonly RsSrv.IStationService _rsStationService;
        private readonly RsSrv.IRoomService _rsRoomService;
        private readonly RsSrv.IDeviceService _rsDeviceService;

        #endregion

        #region Ctor

        public HomeController(
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IMenuService menuService,
            MsSrv.INoticeService noticeService,
            MsSrv.INoticeInUserService noticeInUserService,
            MsSrv.IAreaService msAreaService,
            MsSrv.IStationService msStationService,
            MsSrv.IRoomService msRoomService,
            MsSrv.IDeviceService msDeviceService,
            RsSrv.IAreaService rsAreaService,
            RsSrv.IStationService rsStationService,
            RsSrv.IRoomService rsRoomService,
            RsSrv.IDeviceService rsDeviceService) {
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._menuService = menuService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._msAreaService = msAreaService;
            this._msStationService = msStationService;
            this._msRoomService = msRoomService;
            this._msDeviceService = msDeviceService;
            this._rsAreaService = rsAreaService;
            this._rsStationService = rsStationService;
            this._rsRoomService = rsRoomService;
            this._rsDeviceService = rsDeviceService;
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
                var menus = _menuService.GetAllMenus(_workContext.CurrentRole.Id, 0, int.MaxValue).ToList();
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
                success = false,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(id == "root") {
                    #region root organization
                    var areas = this.GetAreasInRole(_workContext.CurrentRole.Id);
                    var dict = areas.ToDictionary(k => k.AreaId, v => v.Name);
                    var roots = new List<RsDomain.Area>();
                    foreach(var area in areas) {
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
                        var areas = this.GetAreasInRole(_workContext.CurrentRole.Id);
                        var chilren = areas.FindAll(a => a.ParentId == id);
                        if(chilren.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = chilren.Count;
                            for(var i = 0; i < chilren.Count; i++) {
                                var root = new TreeModel {
                                    id = string.Format("ext-area-{0}", chilren[i].AreaId),
                                    text = chilren[i].Name,
                                    selected = false,
                                    icon = Icons.Diqiu,
                                    expanded = false,
                                    leaf = false,
                                    attributes = new List<CustomAttribute>() { 
                                        new CustomAttribute { key = "id", value = chilren[i].AreaId },
                                        new CustomAttribute { key = "type", value = ((int)EnmOrganization.Area).ToString() } 
                                    }
                                };

                                data.data.Add(root);
                            }
                        } else {
                            var stations = this.GetStationsInArea(id);
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
                                            new CustomAttribute { key = "type", value = ((int)EnmOrganization.Sta).ToString() } 
                                        }
                                    };

                                    data.data.Add(root);
                                }
                            }
                        }
                        #endregion
                    } else if(nodeType == EnmOrganization.Sta) {
                        #region station organization
                        var chilren = this.GetStationsInParent(id);
                        if(chilren.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = chilren.Count;
                            for(var i = 0; i < chilren.Count; i++) {
                                var root = new TreeModel {
                                    id = string.Format("ext-station-{0}", chilren[i].Id),
                                    text = chilren[i].Name,
                                    selected = false,
                                    icon = Icons.Juzhan,
                                    expanded = false,
                                    leaf = false,
                                    attributes = new List<CustomAttribute>() { 
                                        new CustomAttribute { key = "id", value = chilren[i].Id },
                                        new CustomAttribute { key = "type", value = ((int)EnmOrganization.Sta).ToString() } 
                                    }
                                };

                                data.data.Add(root);
                            }
                        } else {
                            var rooms = this.GetRoomsInStation(id);
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
                        #endregion
                    } else if(nodeType == EnmOrganization.Room) {
                        #region room organization
                        var devices = this.GetDevicesInRoom(id);
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
                                        new CustomAttribute { key = "type", value = ((int)EnmOrganization.Dev).ToString() } 
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

                    var allAreas = this.GetAreasInRole(_workContext.CurrentRole.Id);
                    var matchAreas = allAreas.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchAreas) {
                        var matchStack = new Stack<string>();
                            matchStack.Push(string.Format("ext-area-{0}", match.AreaId));

                        SearchAreaParent(allAreas, match, matchStack);
                        data.data.Add(this.StackToStringArray(matchStack));
                    }

                    var allStations = this.GetAllStations();
                    var matchStations = allStations.FindAll(s => s.Name.ToLower().Contains(text));
                    foreach(var match in matchStations) {
                        var matchStack = new Stack<string>();
                            matchStack.Push(string.Format("ext-station-{0}", match.Id));

                        var staRoot = SearchStationParent(allStations, match, matchStack);
                        var areaChild = allAreas.Find(a => a.AreaId == staRoot.AreaId);
                        if(areaChild != null) {
                            matchStack.Push(string.Format("ext-area-{0}", areaChild.AreaId));
                            SearchAreaParent(allAreas, areaChild, matchStack);
                        }

                        data.data.Add(this.StackToStringArray(matchStack));
                    }

                    var allRooms = this.GetAllRooms();
                    var matchRooms = allRooms.FindAll(r => r.Name.ToLower().Contains(text));
                    foreach(var match in matchRooms) {
                        var matchStack = new Stack<string>();
                            matchStack.Push(string.Format("ext-room-{0}", match.Id));

                        var rmRoot = allStations.Find(s => s.Id == match.StationId);
                        if(rmRoot != null) {
                            matchStack.Push(string.Format("ext-station-{0}", rmRoot.Id));
                            var staRoot = SearchStationParent(allStations, rmRoot, matchStack);
                            var areaChild = allAreas.Find(a => a.AreaId == staRoot.AreaId);
                            if(areaChild != null) {
                                matchStack.Push(string.Format("ext-area-{0}", areaChild.AreaId));
                                SearchAreaParent(allAreas, areaChild, matchStack);
                            }
                        }

                        data.data.Add(this.StackToStringArray(matchStack));
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private List<RsDomain.Area> GetAreasInRole(Guid role) {
            var msAreas = _msAreaService.GetAreasInRole(role);
            var rsAreas = _rsAreaService.GetAreas();
            return (from rs in rsAreas 
                    join ms in msAreas on rs.AreaId equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Station> GetAllStations() {
            var msStations = _msStationService.GetAllStations();
            var rsStations = _rsStationService.GetAllStations();
            return (from rs in rsStations
                    join ms in msStations on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Station> GetStationsInArea(string area) {
            var msStations = _msStationService.GetAllStations();
            var rsStations = _rsStationService.GetStationsInArea(area);
            return (from rs in rsStations
                    join ms in msStations on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Station> GetStationsInParent(string parent) {
            var msStations = _msStationService.GetAllStations();
            var rsStations = _rsStationService.GetStationsInParent(parent);
            return (from rs in rsStations
                    join ms in msStations on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Room> GetAllRooms() {
            var msRooms = _msRoomService.GetAllRooms();
            var rsRooms = _rsRoomService.GetAllRooms();
            return (from rs in rsRooms
                    join ms in msRooms on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Room> GetRoomsInStation(string staId) {
            var msRooms = _msRoomService.GetAllRooms();
            var rsRooms = _rsRoomService.GetRoomsByParent(staId);
            return (from rs in rsRooms
                    join ms in msRooms on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Device> GetAllDevices() {
            var msDevices = _msDeviceService.GetAllDevices();
            var rsDevices = _rsDeviceService.GetAllDevices();
            return (from rs in rsDevices
                    join ms in msDevices on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private List<RsDomain.Device> GetDevicesInRoom(string roomId) {
            var msDevices = _msDeviceService.GetAllDevices();
            var rsDevices = _rsDeviceService.GetDevicesByParent(roomId);
            return (from rs in rsDevices
                    join ms in msDevices on rs.Id equals ms.Id
                    select rs).ToList();
        }

        private RsDomain.Area SearchAreaParent(List<RsDomain.Area> collection, RsDomain.Area child, Stack<string> matchResult) {
            var parent = collection.Find(a => a.AreaId == child.ParentId);
            if(parent != null) {
                matchResult.Push(string.Format("ext-area-{0}", parent.AreaId));
                return SearchAreaParent(collection, parent, matchResult);
            } else
                return child;
        }

        private RsDomain.Station SearchStationParent(List<RsDomain.Station> collection, RsDomain.Station child, Stack<string> matchResult) {
            var parent = collection.Find(s => s.Id == child.ParentId);
            if(parent != null) {
                matchResult.Push(string.Format("ext-station-{0}", parent.Id));
                return SearchStationParent(collection, parent, matchResult);
            } else
                return child;
        }

        private string[] StackToStringArray(Stack<string> stack) {
            var point = 0;
            var result = new string[stack.Count];
            while(stack.Count > 0) {
                result[point++] = stack.Pop();
            }

            return result;
        }

        #endregion

    }
}