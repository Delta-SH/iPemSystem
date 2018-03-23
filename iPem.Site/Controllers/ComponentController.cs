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
    public class ComponentController : JsonNetController {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;

        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IPointService _pointService;
        private readonly ISignalService _signalService;

        #endregion

        #region Ctor

        public ComponentController(
            IWorkContext workContext,
            IWebEventService webLogger,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IPointService pointService,
            ISignalService signalService) {
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._departmentService = departmentService;
            this._employeeService = employeeService;
            this._pointService = pointService;
            this._signalService = signalService;
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
                var models = _workContext.AreaTypes();
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id.ToString(),
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
                var models = _workContext.StationTypes();
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
                var models = _workContext.RoomTypes();
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
                var models = _workContext.DeviceTypes();
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
                var models = _workContext.Vendors();
                if (models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
        public JsonResult GetBIAlarmLevels(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmBILevel level in Enum.GetValues(typeof(EnmBILevel))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)level, text = Common.GetBIAlarmDisplay(level) });
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
                var models = _workContext.LogicTypes();
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
        public JsonResult GetPointParams(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmPointParam type in Enum.GetValues(typeof(EnmPointParam))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)type, text = Common.GetPointParamDisplay(type) });
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
        public JsonResult GetComputes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmCompute compute in Enum.GetValues(typeof(EnmCompute))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)compute, text = Common.GetComputeDisplay(compute) });
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
        public JsonResult GetDepartments(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _workContext.Departments();
                if (models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = models[i].Id,
                            text = string.Format("{0}-{1}", models[i].Id, models[i].Name)
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
        public JsonResult GetMaskingTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach (EnmMaskType type in Enum.GetValues(typeof(EnmMaskType))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)type, text = Common.GetMaskTypeDisplay(type) });
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
        public JsonResult GetAreas(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
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
                    var current = _workContext.Areas().Find(a => a.Current.Id == node);
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

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                    var current = _workContext.Areas().Find(a => a.Current.Id == node);
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

                    var matchs = _workContext.Areas().FindAll(a => a.Current.Name.ToLower().Contains(text));
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
        public JsonResult GetStations(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
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
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Id),
                                                text = current.Stations[i].Name,
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

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                            var current = _workContext.Stations().Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas().Find(a => a.Current.Id == current.Current.AreaId);
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

                    var areaMatchs = _workContext.Areas().FindAll(a => a.Current.Name.ToLower().Contains(text));
                    foreach(var current in areaMatchs) {
                        var paths = current.ToPath();
                        for(var i = 0; i < paths.Length; i++) {
                            paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                        }

                        data.data.Add(paths);
                    }

                    var staMatchs = _workContext.Stations().FindAll(s => s.Current.Name.ToLower().Contains(text));
                    foreach(var current in staMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Areas().Find(a => a.Current.Id == current.Current.AreaId);
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
        public JsonResult GetRooms(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(node == "root") {
                    #region root organization
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
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
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Id),
                                                text = current.Stations[i].Name,
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
                            var current = _workContext.Stations().Find(s => s.Current.Id == id);
                            if(current != null) {
                                if(current.Rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Rooms.Count;
                                    for(var i = 0; i < current.Rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Room, current.Rooms[i].Id),
                                            text = current.Rooms[i].Name,
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

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                            var current = _workContext.Stations().Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas().Find(a => a.Current.Id == current.Current.AreaId);
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
                            var current = _workContext.Rooms().Find(r => r.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Stations().Find(s => s.Current.Id == current.Current.StationId);
                                if(parent != null) {
                                    var pparent = _workContext.Areas().Find(a => a.Current.Id == parent.Current.AreaId);
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

                    var areaMatchs = _workContext.Areas().FindAll(a => a.Current.Name.ToLower().Contains(text));
                    foreach(var current in areaMatchs) {
                        var paths = current.ToPath();
                        for(var i = 0; i < paths.Length; i++) {
                            paths[i] = Common.JoinKeys((int)EnmSSH.Area, paths[i]);
                        }

                        data.data.Add(paths);
                    }

                    var staMatchs = _workContext.Stations().FindAll(s => s.Current.Name.ToLower().Contains(text));
                    foreach(var current in staMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Areas().Find(a => a.Current.Id == current.Current.AreaId);
                        if(parent != null) {
                            var parentPaths = parent.ToPath();
                            foreach(var pp in parentPaths) {
                                paths.Add(Common.JoinKeys((int)EnmSSH.Area, pp));
                            }
                        }

                        paths.Add(Common.JoinKeys((int)EnmSSH.Station, current.Current.Id));
                        data.data.Add(paths.ToArray());
                    }

                    var roomMatchs = _workContext.Rooms().FindAll(r => r.Current.Name.ToLower().Contains(text));
                    foreach(var current in roomMatchs) {
                        var paths = new List<string>();
                        var parent = _workContext.Stations().Find(s => s.Current.Id == current.Current.StationId);
                        if(parent != null) {
                            var pparent = _workContext.Areas().Find(a => a.Current.Id == parent.Current.AreaId);
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
        public JsonResult GetDevices(string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (node == "root") {
                    #region root organization
                    var roots = _workContext.Areas().FindAll(a => !a.HasParents);
                    if (roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = roots.Count;
                        for (var i = 0; i < roots.Count; i++) {
                            var root = new TreeModel {
                                id = Common.JoinKeys((int)EnmSSH.Area, roots[i].Current.Id),
                                text = Common.GetNodeName(roots[i].Current.Name, roots[i].Current.Vendor),
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            if (multiselect.HasValue && multiselect.Value) {
                                if (!leafselect.HasValue || !leafselect.Value)
                                    root.selected = false;
                            }

                            data.data.Add(root);
                        }
                    }
                    #endregion
                } else if (!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if (keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if (nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
                            if (current != null) {
                                if (current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for (var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Area, current.ChildRoot[i].Current.Id),
                                            text = Common.GetNodeName(current.ChildRoot[i].Current.Name, current.ChildRoot[i].Current.Vendor),
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if (multiselect.HasValue && multiselect.Value) {
                                            if (!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

                                        data.data.Add(root);
                                    }
                                } else {
                                    if (current.Stations.Count > 0) {
                                        data.success = true;
                                        data.message = "200 Ok";
                                        data.total = current.Stations.Count;
                                        for (var i = 0; i < current.Stations.Count; i++) {
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmSSH.Station, current.Stations[i].Id),
                                                text = Common.GetNodeName(current.Stations[i].Name, current.Stations[i].Vendor),
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = false
                                            };

                                            if (multiselect.HasValue && multiselect.Value) {
                                                if (!leafselect.HasValue || !leafselect.Value)
                                                    root.selected = false;
                                            }

                                            data.data.Add(root);
                                        }
                                    }
                                }
                            }
                            #endregion
                        } else if (nodeType == EnmSSH.Station) {
                            #region station organization
                            var current = _workContext.Stations().Find(s => s.Current.Id == id);
                            if (current != null) {
                                if (current.Rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Rooms.Count;
                                    for (var i = 0; i < current.Rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Room, current.Rooms[i].Id),
                                            text = Common.GetNodeName(current.Rooms[i].Name, current.Rooms[i].Vendor),
                                            icon = Icons.Room,
                                            expanded = false,
                                            leaf = false
                                        };

                                        if (multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        } else if (nodeType == EnmSSH.Room) {
                            #region room organization
                            var current = _workContext.Rooms().Find(d => d.Current.Id == id);
                            if (current != null) {
                                if (current.Devices.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Devices.Count;
                                    for (var i = 0; i < current.Devices.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Device, current.Devices[i].Id),
                                            text = Common.GetNodeName(current.Devices[i].Name, current.Devices[i].Vendor),
                                            icon = Icons.Device,
                                            expanded = false,
                                            leaf = true
                                        };

                                        if (multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                            var current = _workContext.Areas().Find(a => a.Current.Id == id);
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
                            var current = _workContext.Stations().Find(s => s.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Areas().Find(a => a.Current.Id == current.Current.AreaId);
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
                            var current = _workContext.Rooms().Find(r => r.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Stations().Find(s => s.Current.Id == current.Current.StationId);
                                if(parent != null) {
                                    var pparent = _workContext.Areas().Find(a => a.Current.Id == parent.Current.AreaId);
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
                            var current = _workContext.Devices().Find(d => d.Current.Id == id);
                            if(current != null) {
                                var paths = new List<string>();
                                var parent = _workContext.Rooms().Find(r => r.Current.Id == current.Current.RoomId);
                                if(parent != null) {
                                    var pparent = _workContext.Stations().Find(s => s.Current.Id == parent.Current.StationId);
                                    if(pparent != null) {
                                        var ppparent = _workContext.Areas().Find(a => a.Current.Id == pparent.Current.AreaId);
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
        public JsonResult GetPoints(string device, bool _ai = true, bool _ao = true, bool _di = true, bool _do = true, bool _al = true) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(device)) {
                    var signals = _signalService.GetSimpleSignalsInDevice(device, _ai, _ao, _di, _do, _al);
                    if(signals.Count > 0) {
                        data.message = "200 Ok";
                        data.total = signals.Count;
                        data.data.AddRange(signals.Select(s => new ComboItem<string, string> { id = s.PointId, text = s.PointName }));
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetControls(string point, int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(point)) {
                    var current = _workContext.Points().Find(p => p.Type == EnmPoint.DO && p.Id.Equals(point));
                    if (current != null && !string.IsNullOrWhiteSpace(current.UnitState)) {
                        var status = Common.GetDIStatus(current.UnitState);
                        data.message = "200 Ok";
                        data.total = status.Count;
                        data.data.AddRange(status.Select(d => new ComboItem<int, string> { id = d.Key, text = string.Format("{0}-{1}", d.Value, d.Key) }));
                    }
                }

                if (data.data.Count == 0) {
                    data.message = "200 Ok";
                    data.total = 3;
                    data.data.Add(new ComboItem<int, string> { id = 0, text = string.Format("{0}-{1}", "常开控制", 0) });
                    data.data.Add(new ComboItem<int, string> { id = 1, text = string.Format("{0}-{1}", "常闭控制", 1) });
                    data.data.Add(new ComboItem<int, string> { id = 4, text = string.Format("{0}-{1}", "脉冲控制", 4) });
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetEmployees(string node, bool? multiselect) {
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetLogicTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmLTH.DevType, roots[i].Id),
                                    text = string.Format("{0}-{1}", roots[i].Id, roots[i].Name),
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
                                var children = _workContext.LogicTypes().FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = string.Format("{0}-{1}", children[i].Id, children[i].Name),
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                    var current = _workContext.LogicTypes().Find(l => l.Id == node);
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

                    var matchs = _workContext.LogicTypes().FindAll(l => l.Name.ToLower().Contains(text));
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
        public JsonResult GetSubLogicTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmLTH.DevType, roots[i].Id),
                                    text = string.Format("{0}-{1}", roots[i].Id, roots[i].Name),
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
                                var children = _workContext.LogicTypes().FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = Common.JoinKeys((int)EnmLTH.Logic, children[i].Id),
                                            text = string.Format("{0}-{1}", children[i].Id, children[i].Name),
                                            icon = Icons.Category,
                                            expanded = false,
                                            leaf = false
                                        };

                                        data.data.Add(child);
                                    }
                                }
                            } else if((int)EnmLTH.Logic == type) {
                                var children = _workContext.SubLogicTypes().FindAll(l => l.LogicTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = string.Format("{0}-{1}", children[i].Id, children[i].Name),
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                    var current = _workContext.SubLogicTypes().Find(l => l.Id == node);
                    if(current != null) {
                        var parent = _workContext.LogicTypes().Find(l => l.Id == current.LogicTypeId);
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

                    var matchs = _workContext.SubLogicTypes().FindAll(l => l.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        var parent = _workContext.LogicTypes().Find(l => l.Id == match.LogicTypeId);
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
        public JsonResult GetPointTree(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmPTH.DevType, roots[i].Id),
                                    text = string.Format("{0}-{1}", roots[i].Id, roots[i].Name),
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
                                var children = _workContext.Points().FindAll(p => p.DeviceType.Id == key);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                    var current = _workContext.Points().Find(p => p.Id == node);
                    if(current != null) {
                        var parent = _workContext.DeviceTypes().Find(s => s.Id == current.DeviceType.Id);
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

                    var matchs = _workContext.Points().FindAll(a => a.Name.ToLower().Contains(text));
                    foreach(var match in matchs) {
                        var parent = _workContext.DeviceTypes().Find(s => s.Id == match.DeviceType.Id);
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
        public JsonResult GetSubDeviceTypes(string node, bool? multiselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    if(node == "root") {
                        var roots = _workContext.DeviceTypes();
                        if(roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = roots.Count;
                            for(var i = 0; i < roots.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmDTH.DevType, roots[i].Id),
                                    text = string.Format("{0}-{1}", roots[i].Id, roots[i].Name),
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
                                var children = _workContext.SubDeviceTypes().FindAll(l => l.DeviceTypeId == key);
                                if(children.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = children.Count;
                                    for(var i = 0; i < children.Count; i++) {
                                        var child = new TreeModel {
                                            id = children[i].Id,
                                            text = string.Format("{0}-{1}", children[i].Id, children[i].Name),
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                    var current = _workContext.SubDeviceTypes().Find(s => s.Id == node);
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

                    var matchs = _workContext.SubDeviceTypes().FindAll(s => s.Name.ToLower().Contains(text));
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
        public JsonResult GetSeniorConditions(string node) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(node) && node == "root") {
                    var conditions = _workContext.ProfileConditions();
                    if (conditions != null && conditions.Any()) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = 0;
                        foreach (var condition in conditions) {
                            data.total++;
                            data.data.Add(new TreeModel {
                                id = condition.id,
                                text = condition.name,
                                icon = Icons.Query,
                                leaf = true
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetSeniorConditionCombo(int start, int limit, bool all = false) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var result = new List<SeniorCondition>();
                if (all) result.Add(new SeniorCondition { id = "root", name = "--" });

                var conditions = _workContext.ProfileConditions();
                if (conditions != null && conditions.Any()) {
                    result.AddRange(conditions.OrderBy(s => s.name));
                }

                if (result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.Count;

                    var end = start + limit;
                    if (end > result.Count)
                        end = result.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ComboItem<string, string> {
                            id = result[i].id,
                            text = result[i].name
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
        public JsonResult GetMatrixTemplates() {
            var data = new AjaxDataModel<List<TreeCustomModel<MatrixTemplate>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeCustomModel<MatrixTemplate>>()
            };

            try {
                var templates = _workContext.ProfileMatrixs();
                if (templates != null && templates.Any()) {
                    data.success = true;
                    data.message = "200 Ok";
                    data.total = 0;
                    foreach (var template in templates) {
                        data.total++;
                        data.data.Add(new TreeCustomModel<MatrixTemplate> {
                            id = template.id,
                            text = template.name,
                            icon = Icons.All,
                            leaf = true,
                            custom = template
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetPointInDevType(string node) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(node)) {
                    var children = _workContext.GetPoints(true, false, true, false, false).FindAll(p => p.DeviceType.Id == node);
                    if (children.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = children.Count;
                        foreach (var child in children) {
                            data.data.Add(new TreeModel {
                                id = child.Id,
                                text = child.Name,
                                icon = Icons.Signal,
                                leaf = true
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        #endregion

    }
}