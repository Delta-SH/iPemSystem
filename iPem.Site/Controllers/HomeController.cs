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
        private readonly MsSrv.IProfileService _msProfileService;
        private readonly HsSrv.IActAlmService _hsActAlmService;
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
            MsSrv.IProfileService msProfileService,
            HsSrv.IActAlmService hsActAlmService,
            RsSrv.ILogicTypeService rsLogicTypeService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._menuService = menuService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._msPointService = msPointService;
            this._msProfileService = msProfileService;
            this._hsActAlmService = hsActAlmService;
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
            _workContext.Store.ActAlmNoticeTime = DateTime.Now;

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

        [AjaxAuthorize]
        public JsonResult GetActAlmCount() {
            var data = new AjaxDataModel<int> {
                success = true,
                message = "无数据",
                total = 0,
                data = 0
            };

            try {
                var actAlms = _hsActAlmService.GetActAlmsByTime(_workContext.Store.ActAlmNoticeTime, DateTime.Now);
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
        public JsonResult RequestActAlarms(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project, int start, int limit) {
            var data = new AjaxDataModel<List<ActAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ActAlmModel>()
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ActAlmModel {
                            id = models[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            levelValue = (int)models[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            area = string.Join(",", this.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", this.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            comment = models[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            frequency = models[i].Current.Frequency
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
        public JsonResult RequestActChart1(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project) {
            var data = new AjaxDataModel<List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartModel>()
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    var groups = models.GroupBy(m => m.Current.AlmLevel).OrderBy(g=>g.Key);
                    foreach(var group in groups) {
                        data.data.Add(new ChartModel {
                            name = Common.GetAlarmLevelDisplay(group.Key),
                            value = group.Count(),
                            comment = ""
                        });
                    }

                    data.message = "200 Ok";
                    data.total = data.data.Count;
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
        public JsonResult RequestActChart2(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project) {
            var data = new AjaxDataModel<List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartModel>()
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    var groups = models.GroupBy(m => new { m.Device.Type.Id, m.Device.Type.Name }).OrderBy(g => g.Key.Id);
                    foreach(var group in groups) {
                        data.data.Add(new ChartModel {
                            name = group.Key.Name,
                            value = group.Count(),
                            comment = ""
                        });
                    }

                    data.message = "200 Ok";
                    data.total = data.data.Count;
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
        public JsonResult RequestActChart3(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project) {
            var data = new AjaxDataModel<List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ChartModel>()
            };

            try {
                var models = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
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
                                data.data.Add(new ChartModel { name = root.Name, value = count, comment = "" });
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
                                        data.data.Add(new ChartModel { name = rc.Name, value = count, comment = "" });
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
                                        data.data.Add(new ChartModel { name = root.Name, value = count, comment = "" });
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
                                        data.data.Add(new ChartModel { name = rc.Name, value = count, comment = "" });
                                }
                            } else {
                                var rooms = _workContext.AssociatedRooms.FindAll(s => s.StationId == currentRoot.Current.Id);
                                foreach(var room in rooms) {
                                    var count = models.Count(m => m.Device.Room.Id == room.Id);
                                    if(count > 0)
                                        data.data.Add(new ChartModel { name = room.Name, value = count, comment = "" });
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
                                data.data.Add(new ChartModel { name = device.Name, value = count, comment = "" });
                        }
                        #endregion
                    } else if(type == EnmOrganization.Device) {
                        #region device
                        var current = _workContext.AssociatedDevices.Find(d => d.Id == nodeid);
                        if(current != null) {
                            var count = models.Count(m => m.Device.Current.Id == current.Id);
                            if(count > 0)
                                data.data.Add(new ChartModel { name = current.Name, value = count, comment = "" });
                        }
                        #endregion
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
        [Authorize]
        public ActionResult DownloadActAlms(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project) {
            try {
                var models = new List<ActAlmModel>();
                var cached = this.GetActAlmStore(nodeid, nodetype, statype, roomtype, devtype, almlevel, logictype, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new ActAlmModel {
                            id = cached[i].Current.Id,
                            level = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            levelValue = (int)cached[i].Current.AlmLevel,
                            start = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            area = string.Join(",", this.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", this.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            comment = cached[i].Current.AlmDesc,
                            value = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            frequency = cached[i].Current.Frequency,
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
        public JsonResult RequestRemoveActAlmCache() {
            try {
                var key = Common.GetCachedKey(SiteCacheKeys.Site_ActAlmResultPattern, _workContext);
                if(_cacheManager.IsSet(key))
                    _cacheManager.Remove(key);

                return Json(new AjaxResultModel { success = true, code = 200, message = "Ok" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SavePointRss(int[] stationtypes, int[] roomtypes, int[] devicetypes, int[] logictypes, int[] pointtypes, string pointnames) {
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

        /**
         *TODO:请求Point实时值
         *需要实现与FSU通信的WebService后才可以继续
         *只请求当前页的20条信号点的实时数据
         *通过阻塞的方式与FSU的WebService通信获取实时值
         */
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
                        if(_workContext.AssociatedDeviceAttributes.ContainsKey(nodeid)) {
                            var current = _workContext.AssociatedDeviceAttributes[nodeid];
                            var points = _msPointService.GetPoints(current.Current.Id, types);
                            var logics = _rsLogicTypeService.GetAllLogicTypes().ToDictionary(k => k.Id, v => v.Name);

                            if(points.TotalCount > 0) {
                                data.message = "200 Ok";
                                data.total = points.TotalCount;

                                var end = start + limit;
                                if(end > points.TotalCount)
                                    end = points.TotalCount;

                                for(int i = start; i < end; i++) {
                                    data.data.Add(new ActPointModel {
                                        key = string.Format("{0}${1}", current.Current.Id, points[i].Id),
                                        area = current.Area.Name,
                                        station = current.Station.Name,
                                        room = current.Room.Name,
                                        devType = current.Type.Name,
                                        devId = current.Current.Id,
                                        devName = current.Current.Name,
                                        logic = logics.ContainsKey(points[i].LogicTypeId) ? logics[points[i].LogicTypeId] : string.Empty,
                                        id = points[i].Id,
                                        name = points[i].Name,
                                        type = (int)points[i].Type,
                                        typeDisplay = Common.GetPointTypeDisplay(points[i].Type),
                                        value = (new Random().Next(-50, 50)),
                                        valueDisplay = string.Format("{0}{1}", (new Random().Next(-50, 50)), points[i].Unit ?? string.Empty),
                                        status = (int)EnmPointStatus.Normal,
                                        statusDisplay = Common.GetPointStatusDisplay(EnmPointStatus.Normal),
                                        timestamp = CommonHelper.TimeConverter(DateTime.Now)
                                    });
                                }
                            }
                        }
                    } else {
                        var points = this.GetRssPoints(nodeid, nodetype).FindAll(p => types.Contains((int)p.Value.Current.Type));
                        if(points.Count > 0) {
                            data.message = "200 Ok";
                            data.total = points.Count;

                            var end = start + limit;
                            if(end > points.Count)
                                end = points.Count;

                            for(int i = start; i < end; i++) {
                                data.data.Add(new ActPointModel {
                                    key = string.Format("{0}${1}", points[i].Id.Current.Id, points[i].Value.Current.Id),
                                    area = points[i].Id.Area.Name,
                                    station = points[i].Id.Station.Name,
                                    room = points[i].Id.Room.Name,
                                    devType = points[i].Id.Type.Name,
                                    devId = points[i].Id.Current.Id,
                                    devName = points[i].Id.Current.Name,
                                    logic = points[i].Value.LogicType.Name,
                                    id = points[i].Value.Current.Id,
                                    name = points[i].Value.Current.Name,
                                    type = (int)points[i].Value.Current.Type,
                                    typeDisplay = Common.GetPointTypeDisplay(points[i].Value.Current.Type),
                                    value = (new Random().Next(-50, 50)),
                                    valueDisplay = string.Format("{0}{1}", (new Random().Next(-50, 50)), points[i].Value.Current.Unit ?? string.Empty),
                                    status = (int)EnmPointStatus.Normal,
                                    statusDisplay = Common.GetPointStatusDisplay(EnmPointStatus.Normal),
                                    timestamp = CommonHelper.TimeConverter(DateTime.Now)
                                });
                            }
                        }
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

        /**
         *TODO:遥控参数下发
         *需要实现与FSU通信的WebService后才可以继续
         */
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ControlPoint(string device, string point, int ctrl) {
            if(!_workContext.AssociatedOperations.ContainsKey(EnmOperation.Control))
                return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

            return Json(new AjaxResultModel { success = true, code = 200, message = "参数已下发" });
        }

        /**
         *TODO:遥调参数下发
         *需要实现与FSU通信的WebService后才可以继续
         */
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult AdjustPoint(string device, string point, double adjust) {
            if(!_workContext.AssociatedOperations.ContainsKey(EnmOperation.Adjust))
                return Json(new AjaxResultModel { success = false, code = 500, message = "您没有操作权限" });

            return Json(new AjaxResultModel { success = true, code = 200, message = "参数已下发" });
        }

        /**
         *TODO:告警确认、工程告警等筛选
         *需要实现告警上传入库后才可以继续
         *需要明确告警确认字段和工程告警字段是扩展新表还是在原表加字段
         */
        private List<ActAlmStore> GetActAlmStore(string nodeid, int nodetype, int[] statype, int[] roomtype, int[] devtype, int[] almlevel, int[] logictype, string pointname, string confirm, string project) {
            var key = Common.GetCachedKey(SiteCacheKeys.Site_ActAlmResultPattern, _workContext);

            if(_cacheManager.IsSet(key))
                return _cacheManager.Get<List<ActAlmStore>>(key);

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

            var points = _workContext.GetAssociatedPointAttributes(new int[] { (int)EnmPoint.AI, (int)EnmPoint.DI });
            if(logictype != null && logictype.Length > 0)
                points = points.FindAll(p => logictype.Contains(p.Current.LogicTypeId));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = CommonHelper.ConditionSplit(pointname);
                if(names.Length > 0)
                    points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = ((almlevel != null && almlevel.Length > 0) ? _hsActAlmService.GetActAlmsByLevels(almlevel) : _hsActAlmService.GetAllActAlms()).ToList();
            var models = (from alarm in currentAlarms
                          join point in points on alarm.PointId equals point.Current.Id
                          join device in devices on alarm.DeviceId equals device.Current.Id
                          orderby alarm.StartTime descending
                          select new ActAlmStore {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            _cacheManager.Set<List<ActAlmStore>>(key, models, CachedIntervals.Site_ActiveIntervals);
            return models;
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

            _cacheManager.Set<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key, points, CachedIntervals.Site_ResultIntervals);
            return points;
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