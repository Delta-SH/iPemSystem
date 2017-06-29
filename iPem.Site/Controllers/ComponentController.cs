using iPem.Core;
using iPem.Core.Enum;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ComponentController : Controller {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;

        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IPointService _pointService;

        #endregion

        #region Ctor

        public ComponentController(
            IWorkContext workContext,
            IWebEventService webLogger,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IPointService pointService) {
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._departmentService = departmentService;
            this._employeeService = employeeService;
            this._pointService = pointService;
        }

        #endregion

        #region Action

        [AjaxAuthorize]
        public JsonResult GetAreaTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.AreaTypes;
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id.ToString(),
                            text = models[i].Name
                        });
                    }
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
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.StationTypes;
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = models[i].Name
                        });
                    }
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
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.RoomTypes;
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = models[i].Name
                        });
                    }
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
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.DeviceTypes;
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = models[i].Name
                        });
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetVendors(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.Vendors;
                if (models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = models[i].Name
                        });
                    }
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetFsuEvents(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmFsuEvent evt in Enum.GetValues(typeof(EnmFsuEvent))) {
                    if (evt == EnmFsuEvent.Undefined) continue;
                    data.data.Add(new ComboItem<int, string>() { id = (int)evt, text = Common.GetFsuEventDisplay(evt) });
                }

                if (data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetAlarmLevels(int start, int limit, bool all = false) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmAlarm level in Enum.GetValues(typeof(EnmAlarm))) {
                    if(level == EnmAlarm.Level0 && !all) continue;
                    data.data.Add(new ComboItem<int, string>() { id = (int)level, text = Common.GetAlarmDisplay(level) });
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetConfirms(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmConfirm confirm in Enum.GetValues(typeof(EnmConfirm))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)confirm, text = Common.GetConfirmDisplay(confirm) });
                }

                if (data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetReservations(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmReservation reservation in Enum.GetValues(typeof(EnmReservation))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)reservation, text = Common.GetReservationDisplay(reservation) });
                }

                if (data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetLogicTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.LogicTypes;
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = models[i].Name
                        });
                    }
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
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmPoint type in Enum.GetValues(typeof(EnmPoint))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)type, text = Common.GetPointTypeDisplay(type) });
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = roots[i].Current.Id,
                                text = roots[i].Current.Name,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = !roots[i].HasChildren
                            };

                            if(multiselect.HasValue && multiselect.Value)
                                root.selected = false;

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if(!string.IsNullOrWhiteSpace(node)) {
                    #region area organization
                    var current = _workContext.Areas.Find(a => a.Current.Id == node);
                    if(current != null && current.HasChildren) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = current.ChildRoot.Count;
                        foreach(var child in current.ChildRoot) {
                            var root = new TreeModel {
                                id = child.Current.Id,
                                text = child.Current.Name,
                                icon = child.HasChildren ? Icons.Diqiu : Icons.Dingwei,
                                expanded = false,
                                leaf = !child.HasChildren
                            };

                            if(multiselect.HasValue && multiselect.Value)
                                root.selected = false;

                            data.data.Add(root);
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
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
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
                    var current = _workContext.Areas.Find(a => a.Current.Id == node);
                    if(current != null)
                        data.data.Add(current.ToPath());
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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

                    var matchs = _workContext.Areas.FindAll(a => a.Current.Name.ToLower().Contains(text));
                    foreach(var match in matchs)
                        data.data.Add(match.ToPath());
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = Common.JoinKeys((int)EnmSSH.Area, roots[i].Current.Id),
                                text = roots[i].Current.Name,
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                if(current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for(var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Area, current.ChildRoot[i].Current.Id),
                                            text = current.ChildRoot[i].Current.Name,
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
                                    if(current.Stations.Count > 0) {
                                        data.success = true;
                                        data.message = "200 Ok";
                                        data.total = current.Stations.Count;
                                        for(var i = 0; i < current.Stations.Count; i++) {
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Current.Id),
                                                text = current.Stations[i].Current.Name,
                                                icon = Icons.Juzhan,
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
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                var paths = current.ToPath();
                                for(var i = 0; i < paths.Length; i++) {
                                    paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                                }

                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                                if(parent != null) {
                                    var parentPaths = parent.ToPath();
                                    foreach(var pp in parentPaths) {
                                        paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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

                    var areaMatchs = _workContext.Areas.FindAll(a => a.Current.Name.ToLower().Contains(text));
                    foreach(var current in areaMatchs) {
                        var paths = current.ToPath();
                        for(var i = 0; i < paths.Length; i++) {
                            paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                        }

                        data.data.Add(paths);
                    }

                    var staMatchs = _workContext.Stations.FindAll(s => s.Current.Name.ToLower().Contains(text));
                    foreach(var current in staMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                        if(parent != null) {
                            var parentPaths = parent.ToPath();
                            foreach(var pp in parentPaths) {
                                paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                        data.data.Add(paths.ToArray());
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = Common.JoinKeys((int)EnmSSH.Area, roots[i].Current.Id),
                                text = roots[i].Current.Name,
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                if(current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for(var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Area, current.ChildRoot[i].Current.Id),
                                            text = current.ChildRoot[i].Current.Name,
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
                                    if(current.Stations.Count > 0) {
                                        data.success = true;
                                        data.message = "200 Ok";
                                        data.total = current.Stations.Count;
                                        for(var i = 0; i < current.Stations.Count; i++) {
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Current.Id),
                                                text = current.Stations[i].Current.Name,
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
                        } else if(nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                if(current.Rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Rooms.Count;
                                    for(var i = 0; i < current.Rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Room, current.Rooms[i].Current.Id),
                                            text = current.Rooms[i].Current.Name,
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
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                var paths = current.ToPath();
                                for(var i = 0; i < paths.Length; i++) {
                                    paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                                }

                                data.data.Add(paths);
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                                if(parent != null) {
                                    var parentPaths = parent.ToPath();
                                    foreach(var pp in parentPaths) {
                                        paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Room) {
                            #region room organization
                            var current = _workContext.Rooms.Find(r => r.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Stations.Find(s => s.Current.Id == current.Current.StationId);
                                if(parent != null) {
                                    var pparent = _workContext.Areas.Find(a => a.Current.Id == parent.Current.AreaId);
                                    if(pparent != null) {
                                        var pparentPaths = pparent.ToPath();
                                        foreach(var pp in pparentPaths) {
                                            paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                        }
                                    }

                                    paths.Add(Common.JoinKeys((int)EnmSSH.Station, parent.Current.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Room, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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

                    var areaMatchs = _workContext.Areas.FindAll(a => a.Current.Name.ToLower().Contains(text));
                    foreach(var current in areaMatchs) {
                        var paths = current.ToPath();
                        for(var i = 0; i < paths.Length; i++) {
                            paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                        }

                        data.data.Add(paths);
                    }

                    var staMatchs = _workContext.Stations.FindAll(s => s.Current.Name.ToLower().Contains(text));
                    foreach(var current in staMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                        if(parent != null) {
                            var parentPaths = parent.ToPath();
                            foreach(var pp in parentPaths) {
                                paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                        data.data.Add(paths.ToArray());
                    }

                    var roomMatchs = _workContext.Rooms.FindAll(r => r.Current.Name.ToLower().Contains(text));
                    foreach(var current in roomMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Stations.Find(s => s.Current.Id == current.Current.StationId);
                        if(parent != null) {
                            var pparent = _workContext.Areas.Find(a => a.Current.Id == parent.Current.AreaId);
                            if(pparent != null) {
                                var pparentPaths = pparent.ToPath();
                                foreach(var pp in pparentPaths) {
                                    paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                }
                            }

                            paths.Add(Common.JoinKeys((int)EnmSSH.Station, parent.Current.Id));
                        }

                        paths.Add(Common.JoinKeys((int)EnmSSH.Room, current.Current.Id));
                        data.data.Add(paths.ToArray());
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas.FindAll(a => !a.HasParents);
                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = Common.JoinKeys((int)EnmSSH.Area, roots[i].Current.Id),
                                text = roots[i].Current.Name,
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                if(current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for(var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Area, current.ChildRoot[i].Current.Id),
                                            text = current.ChildRoot[i].Current.Name,
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
                                    if(current.Stations.Count > 0) {
                                        data.success = true;
                                        data.message = "200 Ok";
                                        data.total = current.Stations.Count;
                                        for(var i = 0; i < current.Stations.Count; i++) {
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Current.Id),
                                                text = current.Stations[i].Current.Name,
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
                        } else if(nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                if(current.Rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Rooms.Count;
                                    for(var i = 0; i < current.Rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Room, current.Rooms[i].Current.Id),
                                            text = current.Rooms[i].Current.Name,
                                            icon = Icons.Room,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if(multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Room) {
                            #region room organization
                            var current = _workContext.Rooms.Find(d => d.Current.Id == id);
                            if(current != null) {
                                if(current.Devices.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Devices.Count;
                                    for(var i = 0; i < current.Devices.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Device, current.Devices[i].Current.Id),
                                            text = current.Devices[i].Current.Name,
                                            icon = Icons.Device,
                                            expanded = false,
                                            leaf = true
                                        };

                                        if(multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
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
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
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
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if(nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                var paths = current.ToPath();
                                for(var i = 0; i < paths.Length; i++) {
                                    paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                                }

                                data.data.Add(paths);
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas.Find(a => a.Current.Id == current.Current.AreaId);
                                if(parent != null) {
                                    var parentPaths = parent.ToPath();
                                    foreach(var pp in parentPaths) {
                                        paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                    }
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Room) {
                            #region room organization
                            var current = _workContext.Rooms.Find(r => r.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Stations.Find(s => s.Current.Id == current.Current.StationId);
                                if(parent != null) {
                                    var pparent = _workContext.Areas.Find(a => a.Current.Id == parent.Current.AreaId);
                                    if(pparent != null) {
                                        var pparentPaths = pparent.ToPath();
                                        foreach(var pp in pparentPaths) {
                                            paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                        }
                                    }

                                    paths.Add(Common.JoinKeys((int)EnmSSH.Station, parent.Current.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Room, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmSSH.Device) {
                            #region device organization
                            var current = _workContext.Devices.Find(d => d.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Rooms.Find(r => r.Current.Id == current.Current.RoomId);
                                if(parent != null) {
                                    var pparent = _workContext.Stations.Find(s => s.Current.Id == parent.Current.StationId);
                                    if(pparent != null) {
                                        var ppparent = _workContext.Areas.Find(a => a.Current.Id == pparent.Current.AreaId);
                                        if(ppparent != null) {
                                            var ppparentPaths = ppparent.ToPath();
                                            foreach(var pp in ppparentPaths) {
                                                paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                                            }
                                        }

                                        paths.Add(Common.JoinKeys((int)EnmSSH.Station, pparent.Current.Id));
                                    }

                                    paths.Add(Common.JoinKeys((int)EnmSSH.Room, parent.Current.Id));
                                }

                                paths.Add(Common.JoinKeys((int)EnmSSH.Device, current.Current.Id));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetPoints(string device, bool AI = true, bool AO = true, bool DI = true, bool DO = true) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(device)) {
                    var types = new List<EnmPoint>();
                    if(AI) types.Add(EnmPoint.AI);
                    if(AO) types.Add(EnmPoint.AO);
                    if(DI) types.Add(EnmPoint.DI);
                    if(DO) types.Add(EnmPoint.DO);

                    var models = _pointService.GetPointsInDevice(device).FindAll(p => types.Contains(p.Type));
                    if(models.Count > 0) {
                        data.message = "200 Ok";
                        data.total = models.Count;
                        data.data.AddRange(models.Select(d => new ComboItem<string, string> { id = d.Id, text = d.Name }));
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
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _departmentService.GetDepartments();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmHRH.Department, roots[i].Id),
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
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var key = keys[1];
                            if((int)EnmHRH.Department == type) {
                                var children = _employeeService.GetEmployeesByDept(key);
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
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
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
                    var current = _employeeService.GetEmployeeById(node);
                    if(current != null) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmHRH.Department, current.DeptId), current.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
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

                    var employees = _employeeService.GetEmployees();
                    var matchs = employees.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmHRH.Department, match.DeptId), match.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetLogicTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes;
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmLTH.DevType, roots[i].Id),
                                    text = roots[i].Name,
                                    icon = Icons.Device,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                    } else {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var key = keys[1];
                            if((int)EnmLTH.DevType == type) {
                                var children = _workContext.LogicTypes.FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = children[i].Name,
                                            icon = Icons.Category,
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
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetLogicTreePath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var current = _workContext.LogicTypes.Find(l => l.Id == node);
                    if(current != null) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmLTH.DevType, current.DeviceTypeId), current.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterLogicTreePath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var matchs = _workContext.LogicTypes.FindAll(l => l.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmLTH.DevType, match.DeviceTypeId), match.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetSubLogicTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes;
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmLTH.DevType, roots[i].Id),
                                    text = roots[i].Name,
                                    icon = Icons.Device,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                    } else {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var key = keys[1];
                            if((int)EnmLTH.DevType == type) {
                                var children = _workContext.LogicTypes.FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = Common.JoinKeys((int)EnmLTH.Logic, children[i].Id),
                                            text = children[i].Name,
                                            icon = Icons.Category,
                                            expanded = false,
                                            leaf = false
                                        };

                                        data.data.Add(child);
                                    }
                                }
                            } else if((int)EnmLTH.Logic == type) {
                                var children = _workContext.SubLogicTypes.FindAll(l => l.LogicTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = children[i].Name,
                                            icon = Icons.Category,
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
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetSubLogicTreePath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var current = _workContext.SubLogicTypes.Find(l => l.Id == node);
                    if(current != null) {
                        var parent = _workContext.LogicTypes.Find(l => l.Id == current.LogicTypeId);
                        if(parent != null) {
                            data.data.Add(new string[] { Common.JoinKeys((int)EnmLTH.DevType, parent.DeviceTypeId), Common.JoinKeys((int)EnmLTH.Logic, parent.Id), current.Id });
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterSubLogicTreePath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var matchs = _workContext.SubLogicTypes.FindAll(l => l.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        var parent = _workContext.LogicTypes.Find(l => l.Id == match.LogicTypeId);
                        if(parent != null) {
                            data.data.Add(new string[] { Common.JoinKeys((int)EnmLTH.DevType, parent.DeviceTypeId), Common.JoinKeys((int)EnmLTH.Logic, parent.Id), match.Id });
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetPointTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes;
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmPTH.DevType, roots[i].Id),
                                    text = roots[i].Name,
                                    icon = Icons.Category,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                    } else {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var key = keys[1];
                            if((int)EnmPTH.DevType == type) {
                                var children = _workContext.Points.FindAll(p => p.DeviceType.Id == key);
                                if (children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for (var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = children[i].Name,
                                            icon = Icons.Signal,
                                            leaf = true
                                        };

                                        if (multiselect.HasValue && multiselect.Value)
                                            child.selected = false;

                                        data.data.Add(child);
                                    }
                                }
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetPointTreePath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var current = _workContext.Points.Find(p => p.Id == node);
                    if(current != null) {
                        var parent = _workContext.DeviceTypes.Find(s => s.Id == current.DeviceType.Id);
                        if(parent != null) {
                            data.data.Add(new string[] { Common.JoinKeys((int)EnmPTH.DevType, parent.Id), current.Id });
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterPointTreePath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var matchs = _workContext.Points.FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        var parent = _workContext.DeviceTypes.Find(s => s.Id == match.DeviceType.Id);
                        if(parent != null) {
                            data.data.Add(new string[] { Common.JoinKeys((int)EnmPTH.DevType, parent.Id), match.Id });
                        }
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetSubDeviceTypes(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes;
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmDTH.DevType, roots[i].Id),
                                    text = roots[i].Name,
                                    icon = Icons.Category,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                    } else {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var key = keys[1];
                            if ((int)EnmDTH.DevType == type) {
                                var children = _workContext.SubDeviceTypes.FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = children[i].Name,
                                            icon = Icons.Category,
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
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetSubDeviceTypesPath(string[] nodes) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                foreach(var node in nodes) {
                    var current = _workContext.SubDeviceTypes.Find(s => s.Id == node);
                    if(current != null) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmDTH.DevType, current.DeviceTypeId), current.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult FilterSubDeviceTypesPath(string text) {
            var data = new AjaxDataModel<List<string[]>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<string[]>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(text)) {
                    text = text.Trim().ToLower();

                    var matchs = _workContext.SubDeviceTypes.FindAll(s => s.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        data.data.Add(new string[] { Common.JoinKeys((int)EnmDTH.DevType, match.DeviceTypeId), match.Id });
                    }
                }

                if(data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonNetResult GetSeniorConditions(string node) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(node)) {
                    if (node == "root") {
                        if (_workContext.Profile.Settings != null 
                            && _workContext.Profile.Settings.SeniorConditions != null
                            && _workContext.Profile.Settings.SeniorConditions.Count > 0) {
                            var conditions = _workContext.Profile.Settings.SeniorConditions.OrderBy(s=>s.name).ToArray();
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = conditions.Length;
                            for (var i = 0; i < conditions.Length; i++) {
                                var root = new TreeModel {
                                    id = conditions[i].id,
                                    text = conditions[i].name,
                                    icon = Icons.Query,
                                    leaf = true
                                };

                                data.data.Add(root);
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveCharts(string[] svgs) {
            try {
                var images = Common.MergeSvgXml(svgs);
                if(images != null)
                    return File(images, "image/png", "chart.png");

                throw new iPemException("An error occurred when trying to generate the image.");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        #endregion

    }
}