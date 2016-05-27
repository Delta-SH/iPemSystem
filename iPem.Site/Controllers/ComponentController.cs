using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using RsSrv = iPem.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPem.Site.Extensions;
using iPem.Site.Models;
using iPem.Core.Enum;
using iPem.Core;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ComponentController : Controller {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;

        private readonly RsSrv.IEnumMethodsService _rsEnumMethodsService;
        private readonly RsSrv.IStationTypeService _rsStationTypeService;
        private readonly RsSrv.IRoomTypeService _rsRoomTypeService;
        private readonly RsSrv.IDeviceTypeService _rsDeviceTypeService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;
        private readonly RsSrv.IEmployeeService _rsEmployeeService;
        private readonly RsSrv.IDepartmentService _rsDepartmentService;

        #endregion

        #region Ctor

        public ComponentController(
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            RsSrv.IEnumMethodsService rsEnumMethodsService,
            RsSrv.IStationTypeService rsStationTypeService,
            RsSrv.IRoomTypeService rsRoomTypeService,
            RsSrv.IDeviceTypeService rsDeviceTypeService,
            RsSrv.ILogicTypeService rsLogicTypeService,
            RsSrv.IEmployeeService rsEmployeeService,
            RsSrv.IDepartmentService rsDepartmentService) {
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._rsEnumMethodsService = rsEnumMethodsService;
            this._rsStationTypeService = rsStationTypeService;
            this._rsRoomTypeService = rsRoomTypeService;
            this._rsDeviceTypeService = rsDeviceTypeService;
            this._rsLogicTypeService = rsLogicTypeService;
            this._rsEmployeeService = rsEmployeeService;
            this._rsDepartmentService = rsDepartmentService;
        }

        #endregion

        #region Action

        [AjaxAuthorize]
        public JsonResult GetAreaTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Area, "类型", start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id.ToString(), text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetStationTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _rsStationTypeService.GetAllStationTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetRoomTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _rsRoomTypeService.GetAllRoomTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetDeviceTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _rsDeviceTypeService.GetAllDeviceTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetAlarmLevels(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmAlarmLevel level in Enum.GetValues(typeof(EnmAlarmLevel))) {
                    if(level == EnmAlarmLevel.NoAlarm) continue;
                    data.data.Add(new ComboItem<int, string>() { id = (int)level, text = Common.GetAlarmLevelDisplay(level) });
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetLogicTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _rsLogicTypeService.GetAllLogicTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetPointTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmPoint type in Enum.GetValues(typeof(EnmPoint))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)type, text = Common.GetPointTypeDisplay(type) });
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetAreas(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
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
                                id = roots[i].AreaId,
                                text = roots[i].Name,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            if(multiselect.HasValue && multiselect.Value)
                                root.selected = false;

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(node)) {
                    #region area organization
                    if(_workContext.AssociatedAreaAttributes.ContainsKey(node)) {
                        var current = _workContext.AssociatedAreaAttributes[node];
                        if(current.HasChildren) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = current.FirstChildren.Count;
                            foreach(var child in current.FirstChildren) {
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(child.AreaId)) {
                                    var associatedChild = _workContext.AssociatedAreaAttributes[child.AreaId];
                                    var root = new TreeModel {
                                        id = associatedChild.Current.AreaId,
                                        text = associatedChild.Current.Name,
                                        icon = associatedChild.HasChildren ? Icons.Diqiu : Icons.Dingwei,
                                        expanded = false,
                                        leaf = !associatedChild.HasChildren
                                    };

                                    if(multiselect.HasValue && multiselect.Value)
                                        root.selected = false;

                                    data.data.Add(root);
                                }
                            }
                        }
                    }
                    #endregion
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetAreaPath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var match = _workContext.AssociatedAreas.Find(a => a.AreaId == node);
                    if(match != null) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(parent.AreaId);
                            }
                        }

                        paths.Add(match.AreaId);
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
        public JsonResult FilterAreaPath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var matchs = _workContext.AssociatedAreas.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(parent.AreaId);
                            }
                        }

                        paths.Add(match.AreaId);
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
        public JsonNetResult GetStations(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
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
                                id = Common.JoinKeys((int)EnmOrganization.Area, roots[i].AreaId),
                                text = roots[i].Name,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            if(multiselect.HasValue && multiselect.Value) {
                                if(!leafselect.HasValue || !leafselect.Value)
                                    root.selected = false;
                            }

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
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
                                            id = Common.JoinKeys((int)EnmOrganization.Area, current.FirstChildren[i].AreaId),
                                            text = current.FirstChildren[i].Name,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value) {
                                            if(!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

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
                                            if(_workContext.AssociatedStationAttributes.ContainsKey(roots[i].Id)) {
                                                var station = _workContext.AssociatedStationAttributes[roots[i].Id];
                                                var root = new TreeModel {
                                                    id = Common.JoinKeys((int)EnmOrganization.Station, station.Current.Id),
                                                    text = station.Current.Name,
                                                    icon = Icons.Juzhan,
                                                    expanded = false,
                                                    leaf = !station.HasChildren
                                                };

                                                if(multiselect.HasValue && multiselect.Value)
                                                    root.selected = false;

                                                data.data.Add(root);
                                            }
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
                                        if(_workContext.AssociatedStationAttributes.ContainsKey(current.FirstChildren[i].Id)) {
                                            var station = _workContext.AssociatedStationAttributes[current.FirstChildren[i].Id];
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmOrganization.Station, station.Current.Id),
                                                text = station.Current.Name,
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = !station.HasChildren
                                            };

                                            if(multiselect.HasValue && multiselect.Value)
                                                root.selected = false;

                                            data.data.Add(root);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetStationPath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Area) {
                            #region area organization
                            var match = _workContext.AssociatedAreas.Find(a => a.AreaId == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station organization
                            var match = _workContext.AssociatedStations.Find(s => s.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));

                                if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                                    var current = _workContext.AssociatedStationAttributes[match.Id];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterStationPath(string text) {
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
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));
                        data.data.Add(paths.ToArray());
                    }

                    var staMatchs = _workContext.AssociatedStations.FindAll(s => s.Name.ToLower().Contains(text));
                    foreach(var match in staMatchs) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                            }
                        }
                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));

                        if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                            var current = _workContext.AssociatedStationAttributes[match.Id];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Id));
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
        public JsonNetResult GetRooms(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
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
                                id = Common.JoinKeys((int)EnmOrganization.Area, roots[i].AreaId),
                                text = roots[i].Name,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            if(multiselect.HasValue && multiselect.Value) {
                                if(!leafselect.HasValue || !leafselect.Value)
                                    root.selected = false;
                            }

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
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
                                            id = Common.JoinKeys((int)EnmOrganization.Area, current.FirstChildren[i].AreaId),
                                            text = current.FirstChildren[i].Name,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value) {
                                            if(!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

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
                                                id = Common.JoinKeys((int)EnmOrganization.Station, roots[i].Id),
                                                text = roots[i].Name,
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = false
                                            };

                                            if(multiselect.HasValue && multiselect.Value) {
                                                if(!leafselect.HasValue || !leafselect.Value)
                                                    root.selected = false;
                                            }

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
                                            id = Common.JoinKeys((int)EnmOrganization.Station, current.FirstChildren[i].Id),
                                            text = current.FirstChildren[i].Name,
                                            icon = Icons.Juzhan,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value) {
                                            if(!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

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
                                                id = Common.JoinKeys((int)EnmOrganization.Room, rooms[i].Id),
                                                text = rooms[i].Name,
                                                icon = Icons.Room,
                                                expanded = false,
                                                leaf = true
                                            };

                                            if(multiselect.HasValue && multiselect.Value)
                                                root.selected = false;

                                            data.data.Add(root);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetRoomPath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Area) {
                            #region area organization
                            var match = _workContext.AssociatedAreas.Find(a => a.AreaId == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station organization
                            var match = _workContext.AssociatedStations.Find(s => s.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));

                                if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                                    var current = _workContext.AssociatedStationAttributes[match.Id];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Room) {
                            #region room organization
                            var match = _workContext.AssociatedRooms.Find(r => r.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedStationAttributes.ContainsKey(match.StationId)) {
                                    var station = _workContext.AssociatedStationAttributes[match.StationId];
                                    if(_workContext.AssociatedAreaAttributes.ContainsKey(station.Current.AreaId)) {
                                        var current = _workContext.AssociatedAreaAttributes[station.Current.AreaId];
                                        if(current.HasParents) {
                                            foreach(var parent in current.Parents)
                                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                        }
                                    }
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, station.Current.AreaId));

                                    if(station.HasParents) {
                                        foreach(var parent in station.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                    }
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Station, station.Current.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Room, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterRoomPath(string text) {
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
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));
                        data.data.Add(paths.ToArray());
                    }

                    var staMatchs = _workContext.AssociatedStations.FindAll(s => s.Name.ToLower().Contains(text));
                    foreach(var match in staMatchs) {
                        var paths = new List<string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                            var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                            }
                        }
                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));

                        if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                            var current = _workContext.AssociatedStationAttributes[match.Id];
                            if(current.HasParents) {
                                foreach(var parent in current.Parents)
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Id));
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
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                }
                            }
                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, root.AreaId));

                            if(_workContext.AssociatedStationAttributes.ContainsKey(root.Id)) {
                                var current = _workContext.AssociatedStationAttributes[root.Id];
                                if(current.HasParents) {
                                    foreach(var parent in current.Parents)
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                }
                            }
                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, root.Id));
                        }

                        paths.Add(Common.JoinKeys((int)EnmOrganization.Room, match.Id));
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
        public JsonNetResult GetDevices(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
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
                                id = Common.JoinKeys((int)EnmOrganization.Area, roots[i].AreaId),
                                text = roots[i].Name,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            if(multiselect.HasValue && multiselect.Value) {
                                if(!leafselect.HasValue || !leafselect.Value)
                                    root.selected = false;
                            }

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
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
                                            id = Common.JoinKeys((int)EnmOrganization.Area, current.FirstChildren[i].AreaId),
                                            text = current.FirstChildren[i].Name,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value) {
                                            if(!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

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
                                                id = Common.JoinKeys((int)EnmOrganization.Station, roots[i].Id),
                                                text = roots[i].Name,
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = false
                                            };

                                            if(multiselect.HasValue && multiselect.Value) {
                                                if(!leafselect.HasValue || !leafselect.Value)
                                                    root.selected = false;
                                            }

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
                                            id = Common.JoinKeys((int)EnmOrganization.Station, current.FirstChildren[i].Id),
                                            text = current.FirstChildren[i].Name,
                                            icon = Icons.Juzhan,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value) {
                                            if(!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

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
                                                id = Common.JoinKeys((int)EnmOrganization.Room, rooms[i].Id),
                                                text = rooms[i].Name,
                                                icon = Icons.Room,
                                                expanded = false,
                                                leaf = false
                                            };

                                            if(multiselect.HasValue && multiselect.Value) {
                                                if(!leafselect.HasValue || !leafselect.Value)
                                                    root.selected = false;
                                            }

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
                                        id = Common.JoinKeys((int)EnmOrganization.Device, devices[i].Id),
                                        text = devices[i].Name,
                                        icon = Icons.Device,
                                        expanded = false,
                                        leaf = true
                                    };

                                    if(multiselect.HasValue && multiselect.Value)
                                        root.selected = false;

                                    data.data.Add(root);
                                }
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetDevicePath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Area) {
                            #region area organization
                            var match = _workContext.AssociatedAreas.Find(a => a.AreaId == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station organization
                            var match = _workContext.AssociatedStations.Find(s => s.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.AreaId));

                                if(_workContext.AssociatedStationAttributes.ContainsKey(match.Id)) {
                                    var current = _workContext.AssociatedStationAttributes[match.Id];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Room) {
                            #region room organization
                            var match = _workContext.AssociatedRooms.Find(r => r.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                if(_workContext.AssociatedStationAttributes.ContainsKey(match.StationId)) {
                                    var station = _workContext.AssociatedStationAttributes[match.StationId];
                                    if(_workContext.AssociatedAreaAttributes.ContainsKey(station.Current.AreaId)) {
                                        var current = _workContext.AssociatedAreaAttributes[station.Current.AreaId];
                                        if(current.HasParents) {
                                            foreach(var parent in current.Parents)
                                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                        }
                                    }
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, station.Current.AreaId));

                                    if(station.HasParents) {
                                        foreach(var parent in station.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                    }
                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Station, station.Current.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Room, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Device) {
                            #region device organization
                            var match = _workContext.AssociatedDevices.Find(d => d.Id == id);
                            if(match != null) {
                                var paths = new List<string>();
                                var room = _workContext.AssociatedRooms.Find(r => r.Id == match.RoomId);
                                if(room != null) {
                                    if(_workContext.AssociatedStationAttributes.ContainsKey(room.StationId)) {
                                        var station = _workContext.AssociatedStationAttributes[room.StationId];
                                        if(_workContext.AssociatedAreaAttributes.ContainsKey(station.Current.AreaId)) {
                                            var current = _workContext.AssociatedAreaAttributes[station.Current.AreaId];
                                            if(current.HasParents) {
                                                foreach(var parent in current.Parents)
                                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                            }
                                        }
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, station.Current.AreaId));

                                        if(station.HasParents) {
                                            foreach(var parent in station.Parents)
                                                paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                        }
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Station, station.Current.Id));
                                    }

                                    paths.Add(Common.JoinKeys((int)EnmOrganization.Room, room.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmOrganization.Device, match.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetEmployees(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _rsDepartmentService.GetAllDepartments();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmHR.Department, roots[i].Id),
                                    text = roots[i].Name,
                                    icon = Icons.Department,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                    } else {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2
                            && ((int)EnmHR.Department).ToString().Equals(keys[0])
                            && !string.IsNullOrWhiteSpace(keys[1])) {
                            var children = _rsEmployeeService.GetEmployeesInDepartment(keys[1]);
                            if(children.Count > 0) {
                                data.success = true;
                                data.message = "200 Ok";
                                data.total = children.Count;
                                for(var i = 0; i < children.Count; i++) {
                                    var child = new TreeModel {
                                        id = children[i].Id,
                                        text = children[i].Name,
                                        icon = Icons.Employee,
                                        leaf = true
                                    };

                                    if(multiselect.HasValue && multiselect.Value)
                                        child.selected = false;

                                    data.data.Add(child);
                                }
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetEmployeePath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var current = _rsEmployeeService.GetEmpolyee(node);
                    if(current != null) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmHR.Department, current.DeptId), current.Id });
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterEmployeePath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var employees = _rsEmployeeService.GetAllEmployees().ToList();
                    var matchs = employees.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmHR.Department, match.DeptId), match.Id });
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveCharts(string[] svgs) {
            try {
                var images = Common.MergeSvgXml(svgs);
                if(images != null)
                    return File(images, "image/png", "chart.png");

                throw new iPemException("生成图片失败");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        #endregion

    }
}