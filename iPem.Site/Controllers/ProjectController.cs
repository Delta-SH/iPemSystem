using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
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
    public class ProjectController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IReservationService _reservationService;
        private readonly INodeInReservationService _nodesInReservationService;
        private readonly IProjectService _projectService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEntitiesInRoleService _entitiesInRoleService;
        private readonly IAreaService _areaService;
        private readonly INoticeService _noticeService;
        private readonly INoticeInUserService _noticeInUserService;

        #endregion

        #region Ctor

        public ProjectController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IReservationService reservationService,
            INodeInReservationService nodesInReservationService,
            IProjectService projectService,
            INoteService noteService,
            IUserService userService,
            IRoleService roleService,
            IEntitiesInRoleService entitiesInRoleService,
            IAreaService areaService,
            INoticeService noticeService,
            INoticeInUserService noticeInUserService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._reservationService = reservationService;
            this._nodesInReservationService = nodesInReservationService;
            this._projectService = projectService;
            this._noteService = noteService;
            this._userService = userService;
            this._roleService = roleService;
            this._entitiesInRoleService = entitiesInRoleService;
            this._areaService = areaService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            if (!_workContext.Authorizations().Menus.Contains(2006))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Reservation() {
            if (!_workContext.Authorizations().Menus.Contains(2007))
                throw new HttpException(404, "Page not found.");

            ViewBag.Check = _workContext.Authorizations().Permissions.Contains(EnmPermission.Check);

            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetProjects(int start, int limit, string name, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<ProjectModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ProjectModel>()
            };

            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var names = Common.SplitCondition(name);
                var projects = names.Length > 0 ?
                    _projectService.GetPagedProjectsInNames(names, startDate, endDate, start / limit, limit) :
                    _projectService.GetPagedProjectsInSpan(startDate, endDate, start / limit, limit);

                if (projects.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = projects.TotalCount;
                    for (var i = 0; i < projects.Count; i++) {
                        data.data.Add(new ProjectModel {
                            index = start + i + 1,
                            id = projects[i].Id,
                            name = projects[i].Name,
                            start = CommonHelper.DateConverter(projects[i].StartTime),
                            end = CommonHelper.DateConverter(projects[i].EndTime),
                            responsible = projects[i].Responsible,
                            contact = projects[i].ContactPhone,
                            company = projects[i].Company,
                            creator = projects[i].Creator,
                            createdtime = CommonHelper.DateTimeConverter(projects[i].CreatedTime),
                            comment = projects[i].Comment,
                            enabled = projects[i].Enabled
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetProject(string id, int action) {
            var data = new AjaxDataModel<ProjectModel> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new ProjectModel {
                    index = 1,
                    id = Guid.NewGuid().ToString(),
                    name = "",
                    start = CommonHelper.DateConverter(DateTime.Today),
                    end = CommonHelper.DateConverter(DateTime.Today.AddDays(7)),
                    responsible = "",
                    contact = "",
                    company = "",
                    creator = _workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid,
                    createdtime = CommonHelper.DateConverter(DateTime.Now),
                    comment = "",
                    enabled = true
                }
            };

            try {
                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var project = _projectService.GetProject(id);
                if (project == null) throw new iPemException("未找到数据对象");

                data.data.id = project.Id;
                data.data.name = project.Name;
                data.data.start = CommonHelper.DateConverter(project.StartTime);
                data.data.end = CommonHelper.DateConverter(project.EndTime);
                data.data.responsible = project.Responsible;
                data.data.contact = project.ContactPhone;
                data.data.company = project.Company;
                data.data.comment = project.Comment;
                data.data.enabled = project.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveProject(ProjectModel project, int action) {
            try {
                if (project == null) throw new ArgumentException("project");
                if (action == (int)EnmAction.Add) {
                    var newOne = new M_Project {
                        Id = project.id,
                        Name = project.name,
                        StartTime = Convert.ToDateTime(project.start),
                        EndTime = Convert.ToDateTime(project.end).AddDays(1).AddSeconds(-1),
                        Responsible = project.responsible,
                        ContactPhone = project.contact,
                        Company = project.company,
                        Creator = _workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid,
                        CreatedTime = DateTime.Now,
                        Comment = project.comment,
                        Enabled = project.enabled
                    };

                    _projectService.Add(newOne);
                    _webLogger.Information(EnmEventType.Other, string.Format("新增工程[{0}]", project.name), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var existed = _projectService.GetProject(project.id);
                    if (existed == null) throw new iPemException("工程不存在，保存失败。");
                    if (existed.Creator != (_workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid)) throw new iPemException("您没有操作权限。");

                    //existed.Id = projectId;
                    existed.Name = project.name;
                    existed.StartTime = Convert.ToDateTime(project.start);
                    existed.EndTime = Convert.ToDateTime(project.end).AddDays(1).AddSeconds(-1);
                    existed.Responsible = project.responsible;
                    existed.ContactPhone = project.contact;
                    existed.Company = project.company;
                    existed.Comment = project.comment;
                    existed.Enabled = project.enabled;

                    _projectService.Update(existed);
                    _webLogger.Information(EnmEventType.Other, string.Format("更新工程[{0}]", existed.Name), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadProjects(string name, DateTime startDate, DateTime endDate) {
            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var names = Common.SplitCondition(name);
                var projects = names.Length > 0 ?
                    _projectService.GetProjectsInNames(names, startDate, endDate) :
                    _projectService.GetProjectsInSpan(startDate, endDate);

                var models = new List<ProjectModel>();
                for (var i = 0; i < projects.Count; i++) {
                    models.Add(new ProjectModel {
                        index = i + 1,
                        id = projects[i].Id,
                        name = projects[i].Name,
                        start = CommonHelper.DateConverter(projects[i].StartTime),
                        end = CommonHelper.DateConverter(projects[i].EndTime),
                        responsible = projects[i].Responsible,
                        contact = projects[i].ContactPhone,
                        company = projects[i].Company,
                        creator = projects[i].Creator,
                        createdtime = CommonHelper.DateTimeConverter(projects[i].CreatedTime),
                        comment = projects[i].Comment,
                        enabled = projects[i].Enabled
                    });
                }

                using (var ms = _excelManager.Export<ProjectModel>(models, "工程信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetReservations(int start, int limit, DateTime startDate, DateTime endDate, int type, string keyWord) {
            var data = new AjaxDataModel<List<ReservationModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ReservationModel>()
            };

            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);
                var reservations = _reservationService.GetReservationsInSpan(startDate, endDate);
                var projects = _projectService.GetProjects();
                var result = (from res in reservations
                              join pro in projects on res.ProjectId equals pro.Id
                              select new { Reservation = res, Project = pro }).ToList();

                if (!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());
                    if (type == (int)EnmSSH.Area) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Area);
                        var areas = _workContext.Areas().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Device) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Device);
                        var devices = _workContext.Devices().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Room) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Room);
                        var rooms = _workContext.Rooms().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Station) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Station);
                        var stations = _workContext.Stations().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                if (result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.Count;

                    var end = start + limit;
                    if (end > result.Count)
                        end = result.Count;

                    var users = _userService.GetPagedUsers().ToDictionary(k => k.Id, v => v.Uid);

                    for (int i = start; i < end; i++) {
                        data.data.Add(new ReservationModel {
                            index = i + 1,
                            id = result[i].Reservation.Id,
                            name = result[i].Reservation.Name,
                            expStartDate = CommonHelper.DateTimeConverter(result[i].Reservation.ExpStartTime),
                            startDate = CommonHelper.DateTimeConverter(result[i].Reservation.StartTime),
                            endDate = CommonHelper.DateTimeConverter(result[i].Reservation.EndTime),
                            projectId = result[i].Project.Id,
                            projectName = result[i].Project.Name,
                            creator = result[i].Reservation.Creator,
                            user = users[result[i].Reservation.UserId],
                            createdTime = CommonHelper.DateTimeConverter(result[i].Reservation.CreatedTime),
                            comment = result[i].Reservation.Comment,
                            enabled = result[i].Reservation.Enabled,
                            statusId = ((int)result[i].Reservation.Status),
                            status = Common.GetResStatusDisplay(result[i].Reservation.Status)
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetComboProjects(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _projectService.GetValidProjects().OrderByDescending(p => p.CreatedTime).Select(p => new ComboItem<string, string> { id = p.Id.ToString(), text = p.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit, models.Count());
                if (result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for (var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetReservation(string id, int action) {
            var data = new AjaxDataModel<ReservationModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new ReservationModel {
                    index = 1,
                    id = Guid.NewGuid().ToString(),
                    name = "",
                    expStartDate = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(2100)),
                    endDate = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(88500)),
                    projectId = "",
                    projectName = "",
                    creator = _workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid,
                    //user = _workContext.User().Uid,
                    createdTime = CommonHelper.DateTimeConverter(DateTime.Now),
                    comment = "",
                    enabled = true,
                    nodes = new string[0]
                }
            };

            try {
                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var reservation = _reservationService.GetReservation(id);
                if (reservation == null) throw new iPemException("未找到数据对象");

                var nodes = _nodesInReservationService.GetNodesInReservationsInReservation(reservation.Id);
                data.data.id = reservation.Id;
                data.data.name = reservation.Name;
                data.data.expStartDate = CommonHelper.DateTimeConverter(reservation.ExpStartTime);
                data.data.startDate = CommonHelper.DateTimeConverter(reservation.StartTime);
                data.data.endDate = CommonHelper.DateTimeConverter(reservation.EndTime);
                data.data.projectId = reservation.ProjectId;
                data.data.creator = reservation.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(reservation.CreatedTime);
                data.data.comment = reservation.Comment;
                data.data.enabled = reservation.Enabled;
                data.data.nodes = nodes.Select(m => Common.JoinKeys((int)m.NodeType, m.NodeId)).ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetReservationDetails(string id) {
            var data = new AjaxDataModel<ReservationDetailModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new ReservationDetailModel() {
                    id = "",
                    name = "",
                    areas = "",
                    stations = "",
                    rooms = "",
                    devices = ""
                }
            };

            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var current = _reservationService.GetReservation(id);
                if (current == null) throw new iPemException("未找到对象");

                data.data.id = current.Id;
                data.data.name = current.Name;
                var nodes = _nodesInReservationService.GetNodesInReservationsInReservation(current.Id);

                //预约区域
                var areaNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Area);
                var areaMatchs = from node in areaNodes
                                 join area in _workContext.Areas() on node.NodeId equals area.Current.Id
                                 select area.Current;

                data.data.areas = areaMatchs.Any() ? string.Join(",", areaMatchs.Select(a => a.Name)) : "无区域";

                //预约站点
                var stationNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Station);
                var stationMatchs = from node in stationNodes
                                    join station in _workContext.Stations() on node.NodeId equals station.Current.Id
                                    select station.Current;

                data.data.stations = stationMatchs.Any() ? string.Join(",", stationMatchs.Select(a => a.Name)) : "无站点";

                //预约机房
                var roomNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Room);
                var roomMatchs = from node in roomNodes
                                 join room in _workContext.Rooms() on node.NodeId equals room.Current.Id
                                 select room.Current;

                data.data.rooms = roomMatchs.Any() ? string.Join(",", roomMatchs.Select(a => a.Name)) : "无机房";

                //预约设备
                var deviceNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Device);
                var deviceMatchs = from node in deviceNodes
                                   join device in _workContext.Devices() on node.NodeId equals device.Current.Id
                                   select device.Current;

                data.data.devices = deviceMatchs.Any() ? string.Join(",", deviceMatchs.Select(a => a.Name)) : "无设备";
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveReservation(ReservationModel model, int action) {
            try {
                if (model == null) throw new ArgumentException("model");
                var startTime = DateTime.Parse(model.expStartDate);
                var endTime = DateTime.Parse(model.endDate);
                var interval = endTime.Subtract(startTime);
                if (interval.TotalSeconds < 0) throw new iPemException("预约结束时间不能早于预约开始时间");
                //if (interval.TotalSeconds > 86400) throw new iPemException("预约总时长不能超过24个小时");
                if (model.nodes == null || model.nodes.Length == 0) throw new iPemException("未选择需要预约的监控节点");

                var project = _projectService.GetProject(model.projectId);
                if (project == null) throw new iPemException("未找到所关联的工程信息");
                if (!(project.StartTime <= startTime && project.EndTime > endTime))
                    throw new iPemException("预约时间已超出关联的工程时间");

                if (action == (int)EnmAction.Add) {
                    var newOne = new M_Reservation {
                        Id = model.id,
                        Name = model.name,
                        ExpStartTime = startTime,
                        EndTime = endTime,
                        ProjectId = project.Id,
                        Creator = _workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid,
                        UserId = _workContext.User().Id,
                        CreatedTime = DateTime.Now,
                        Comment = model.comment,
                        Enabled = model.enabled,
                        Status = EnmResult.Undefine
                    };

                    var nodes = new List<M_NodeInReservation>();
                    foreach (var node in model.nodes) {
                        var keys = Common.SplitKeys(node);
                        if (keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            nodes.Add(new M_NodeInReservation {
                                ReservationId = newOne.Id,
                                NodeId = keys[1],
                                NodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area
                            });
                        }
                    }

                    _reservationService.Add(newOne);
                    _nodesInReservationService.Add(nodes.ToArray());
                    _webLogger.Information(EnmEventType.Other, string.Format("新增预约[{0}]", newOne.Id), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var existed = _reservationService.GetReservation(model.id);
                    if (existed == null) throw new iPemException("预约不存在，保存失败。");
                    if (existed.Creator != (_workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid)) throw new iPemException("您没有操作权限。");

                    existed.Name = model.name;
                    existed.ExpStartTime = startTime;
                    existed.EndTime = endTime;
                    existed.ProjectId = project.Id;
                    existed.Comment = model.comment;
                    existed.Enabled = model.enabled;
                    existed.Status = EnmResult.Undefine;

                    var nodes = new List<M_NodeInReservation>();
                    foreach (var node in model.nodes) {
                        var keys = Common.SplitKeys(node);
                        if (keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            nodes.Add(new M_NodeInReservation {
                                ReservationId = existed.Id,
                                NodeId = keys[1],
                                NodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area
                            });
                        }
                    }

                    _reservationService.Update(existed);
                    _nodesInReservationService.Remove(existed.Id);
                    _nodesInReservationService.Add(nodes.ToArray());
                    _webLogger.Information(EnmEventType.Other, string.Format("更新预约[{0}]", model.id), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteReservation(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var reservation = _reservationService.GetReservation(id);
                if (reservation == null) throw new iPemException("预约不存在，删除失败。");
                if (reservation.Creator != (_workContext.Employee() != null ? _workContext.Employee().Name : _workContext.User().Uid)) throw new ArgumentException("您没有操作权限。");
                _reservationService.Delete(reservation);
                _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "M_Reservations", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步工程预约" });
                _webLogger.Information(EnmEventType.Other, string.Format("删除预约[{0}]", reservation.Id), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult CheckReservation(string id, int status, string comment) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                var reservation = _reservationService.GetReservation(id);
                if (reservation == null) throw new iPemException("未找到数据对象");

                reservation.Status = Enum.IsDefined(typeof(EnmResult), status) ? (EnmResult)status : EnmResult.Failure;
                if (reservation.Status == EnmResult.Success) {
                    var interval = 0d;
                    var rtValues = _workContext.RtValues();
                    if (rtValues != null) interval = rtValues.gcyyshsxShiChang;

                    var now = DateTime.Now;
                    if (now >= reservation.ExpStartTime) {
                        reservation.StartTime = now;
                    } else {
                        reservation.StartTime = now.AddMinutes(interval);
                        if (reservation.StartTime >= reservation.ExpStartTime) {
                            reservation.StartTime = reservation.ExpStartTime;
                        }
                    }
                }

                _reservationService.Check(reservation.Id, reservation.StartTime, reservation.Status, comment);

                #region 通知服务重载配置
                if (reservation.Status == EnmResult.Success) {
                    _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "M_Reservations", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步工程预约" });
                }
                #endregion

                #region 通知用户审核结果
                var newNotice = new H_Notice {
                    Id = Guid.NewGuid(),
                    Title = "工程预约审核结果",
                    Content = reservation.Status == EnmResult.Failure ? string.Format("您于 [ {0} ] 申请的工程预约 [ {1} ] 由于 [ {2} ] 原因，审核未通过。", reservation.ExpStartTime, reservation.Name, comment) : string.Format("您于 [ {0} ] 申请的工程预约 [ {1} ] 已通过审核,该预约将于[ {2} ]生效。", reservation.ExpStartTime, reservation.Name, reservation.StartTime),
                    CreatedTime = DateTime.Now,
                    Enabled = true
                };

                var roleId = _roleService.GetRoleByUid(reservation.UserId).Id;
                var toUsers = _userService.GetUsersInRole(roleId, false);
                var noticesInUsers = toUsers.Select(u => new H_NoticeInUser {
                    NoticeId = newNotice.Id,
                    UserId = u.Id,
                    Readed = false,
                    ReadTime = DateTime.MinValue
                });

                _noticeService.Add(newNotice);
                _noticeInUserService.Add(noticesInUsers.ToArray());
                #endregion

                _webLogger.Information(EnmEventType.Other, string.Format("审核工程预约[ {0} ]，审核结果：[ {1} ]，预约编号：[ {2} ]，预期开始时间：[ {3} ]，实际开始时间：[ {4} ]，结束时间：[ {5} ]，创建人员：[ {6} ]，备注[ {7} ]", reservation.Name, Common.GetResStatusDisplay(reservation.Status), reservation.Id, CommonHelper.DateTimeConverter(reservation.ExpStartTime), CommonHelper.DateTimeConverter(reservation.StartTime), CommonHelper.DateTimeConverter(reservation.EndTime), reservation.Creator, reservation.Comment), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "审核完成" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadReservations(DateTime startDate, DateTime endDate, int type, string keyWord) {
            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var reservations = _reservationService.GetReservationsInSpan(startDate, endDate);
                var projects = _projectService.GetProjects();
                var result = (from res in reservations
                              join pro in projects on res.ProjectId equals pro.Id
                              select new { Reservation = res, Project = pro }).ToList();

                if (!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());
                    if (type == (int)EnmSSH.Area) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Area);
                        var areas = _workContext.Areas().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Device) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Device);
                        var devices = _workContext.Devices().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Room) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Room);
                        var rooms = _workContext.Rooms().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else if (type == (int)EnmSSH.Station) {
                        var nodes = _nodesInReservationService.GetNodesInReservationsInType(EnmSSH.Station);
                        var stations = _workContext.Stations().FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Reservation.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                var users = _userService.GetPagedUsers().ToDictionary(k => k.Id, v => v.Uid);

                var models = new List<ReservationModel>();
                if (result.Count > 0) {
                    for (int i = 0; i < result.Count; i++) {
                        models.Add(new ReservationModel {
                            index = i + 1,
                            id = result[i].Reservation.Id,
                            name = result[i].Reservation.Name,
                            expStartDate = CommonHelper.DateTimeConverter(result[i].Reservation.ExpStartTime),
                            startDate = CommonHelper.DateTimeConverter(result[i].Reservation.StartTime),
                            endDate = CommonHelper.DateTimeConverter(result[i].Reservation.EndTime),
                            projectId = result[i].Project.Id,
                            projectName = result[i].Project.Name,
                            creator = result[i].Reservation.Creator,
                            user = users[result[i].Reservation.UserId],
                            createdTime = CommonHelper.DateTimeConverter(result[i].Reservation.CreatedTime),
                            comment = result[i].Reservation.Comment,
                            enabled = result[i].Reservation.Enabled,
                            status = Common.GetResStatusDisplay(result[i].Reservation.Status)
                        });
                    }
                }

                using (var ms = _excelManager.Export<ReservationModel>(models, "工程预约信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        #endregion

    }
}