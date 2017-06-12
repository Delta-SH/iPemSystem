using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ProjectController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebLogger _webLogger;
        private readonly IAppointmentService _appointmentService;
        private readonly INodesInAppointmentService _nodesInAppointmentService;
        private readonly IProjectService _projectsService;

        #endregion

        #region Ctor

        public ProjectController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IAppointmentService appointmentService,
            INodesInAppointmentService nodesInAppointmentService,
            IProjectService projectsService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._appointmentService = appointmentService;
            this._nodesInAppointmentService = nodesInAppointmentService;
            this._projectsService = projectsService;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            return View();
        }

        public ActionResult Appointment() {
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
                    _projectsService.GetProjects(names, startDate, endDate, start / limit, limit) : 
                    _projectsService.GetProjects(startDate, endDate, start / limit, limit);

                if(projects.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = projects.TotalCount;
                    for(var i = 0; i < projects.Count; i++) {
                        data.data.Add(new ProjectModel {
                            index = start + i + 1,
                            id = projects[i].Id.ToString(),
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
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                    creator = "", 
                    createdtime = CommonHelper.DateConverter(DateTime.Now), 
                    comment = "", 
                    enabled = true 
                }
            };
            
            try {
                if(action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var project = _projectsService.GetProject(new Guid(id));
                if(project == null)
                    throw new iPemException("未找到数据对象");

                data.data.id = project.Id.ToString();
                data.data.name = project.Name;
                data.data.start = CommonHelper.DateConverter(project.StartTime);
                data.data.end = CommonHelper.DateConverter(project.EndTime);
                data.data.responsible = project.Responsible;
                data.data.contact = project.ContactPhone;
                data.data.company = project.Company;
                data.data.comment = project.Comment;
                data.data.enabled = project.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveProject(ProjectModel project, int action) {
            try {
                if(project == null)
                    throw new ArgumentException("参数无效 project");

                if(action == (int)EnmAction.Add) {
                    var newOne = new M_Project {
                        Id = new Guid(project.id),
                        Name = project.name,
                        StartTime = Convert.ToDateTime(project.start),
                        EndTime = Convert.ToDateTime(project.end).AddDays(1).AddSeconds(-1),
                        Responsible = project.responsible,
                        ContactPhone = project.contact,
                        Company = project.company,
                        Creator = _workContext.Employee.Name,
                        CreatedTime = DateTime.Now,
                        Comment = project.comment,
                        Enabled = project.enabled
                    };

                    _projectsService.Add(newOne);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增工程[{0}]", project.name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existedOne = _projectsService.GetProject(new Guid(project.id));
                    if(existedOne == null)
                        throw new iPemException("工程不存在，保存失败。");

                    //existedOne.Id = projectId;
                    existedOne.Name = project.name;
                    existedOne.StartTime = Convert.ToDateTime(project.start);
                    existedOne.EndTime = Convert.ToDateTime(project.end).AddDays(1).AddSeconds(-1);
                    existedOne.Responsible = project.responsible;
                    existedOne.ContactPhone = project.contact;
                    existedOne.Company = project.company;
                    existedOne.Comment = project.comment;
                    existedOne.Enabled = project.enabled;

                    _projectsService.Update(existedOne);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新工程[{0}]", existedOne.Name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }
                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                    _projectsService.GetProjectsAsList(names, startDate, endDate) : 
                    _projectsService.GetProjectsAsList(startDate, endDate);

                var models = new List<ProjectModel>();
                for(var i = 0; i < projects.Count; i++) {
                    models.Add(new ProjectModel {
                        index = i + 1,
                        id = projects[i].Id.ToString(),
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

                using(var ms = _excelManager.Export<ProjectModel>(models, "工程信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetAppointments(int start, int limit, DateTime startDate, DateTime endDate, int type, string keyWord) {
            var data = new AjaxDataModel<List<AppointmentModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<AppointmentModel>()
            };

            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var appointments = _appointmentService.GetAppointmentsAsList(startDate, endDate);
                var projects = _projectsService.GetAllProjectsAsList();
                var result = (from app in appointments
                              join pro in projects on app.ProjectId equals pro.Id
                              select new {
                                  Appointment = app,
                                  Project = pro
                              }).ToList();

                if(!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());
                    if(type == (int)EnmSSH.Area) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Area);
                        var areas = _workContext.RoleAreas.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Device) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Device);
                        var devices = _workContext.RoleDevices.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Room) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Room);
                        var rooms = _workContext.RoleRooms.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Station) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Station);
                        var stations = _workContext.RoleStations.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.Count;

                    var end = start + limit;
                    if(end > result.Count)
                        end = result.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new AppointmentModel {
                            index = i + 1,
                            id = result[i].Appointment.Id.ToString(),
                            startDate = CommonHelper.DateTimeConverter(result[i].Appointment.StartTime),
                            endDate = CommonHelper.DateTimeConverter(result[i].Appointment.EndTime),
                            projectName = result[i].Project.Name,
                            creator = result[i].Appointment.Creator,
                            createdTime = CommonHelper.DateTimeConverter(result[i].Appointment.CreatedTime),
                            comment = result[i].Appointment.Comment,
                            enabled = result[i].Appointment.Enabled,
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                var models = _projectsService.GetAllProjectsAsList().Select(p => new ComboItem<string, string> { id = p.Id.ToString(), text = p.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit, models.Count());
                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for(var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
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
        public JsonResult GetAppointment(string id, int action) {
            var data = new AjaxDataModel<AppointmentModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new AppointmentModel {
                    index = 1,
                    id = Guid.NewGuid().ToString(),
                    startDate = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(2100)),
                    endDate = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(88500)),
                    comment = "",
                    enabled = true,
                    nodes = new string[] { }
                }
            };

            try {
                if(action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var appointment = _appointmentService.GetAppointment(new Guid(id));
                if(appointment == null)
                    throw new iPemException("未找到数据对象");

                var nodes = _nodesInAppointmentService.GetNodes(appointment.Id);
                data.data.id = appointment.Id.ToString();
                data.data.startDate = CommonHelper.DateTimeConverter(appointment.StartTime);
                data.data.endDate = CommonHelper.DateTimeConverter(appointment.EndTime);
                data.data.projectId = appointment.ProjectId.ToString();
                data.data.creator = appointment.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(appointment.CreatedTime);
                data.data.comment = appointment.Comment;
                data.data.enabled = appointment.Enabled;
                data.data.nodes = nodes.Select(m => Common.JoinKeys((int)m.NodeType, m.NodeId)).ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetAppointmentDetails(string id) {
            var data = new AjaxDataModel<AppointmentDetailModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new AppointmentDetailModel() {
                    areas = "",
                    stations = "",
                    rooms = "",
                    devices = ""
                }
            };

            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var nodes = _nodesInAppointmentService.GetNodesAsList(new Guid(id));

                //预约区域
                var areaNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Area);
                var areaMatchs = from node in areaNodes
                                 join area in _workContext.RoleAreas on node.NodeId equals area.Current.Id
                                 select area.Current;

                data.data.areas = areaMatchs.Any() ? string.Join(",", areaMatchs.Select(a => a.Name)) : "无区域";


                //预约站点
                var stationNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Station);
                var stationMatchs = from node in stationNodes
                                     join station in _workContext.RoleStations on node.NodeId equals station.Current.Id
                                     select station.Current;

                data.data.stations = stationMatchs.Any() ? string.Join(",", stationMatchs.Select(a => a.Name)) : "无站点";

                //预约机房
                var roomNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Room);
                var roomMatchs = from node in roomNodes
                                  join room in _workContext.RoleRooms on node.NodeId equals room.Current.Id
                                  select room.Current;

                data.data.rooms = roomMatchs.Any() ? string.Join(",", roomMatchs.Select(a => a.Name)) : "无机房";

                //预约设备
                var deviceNodes = nodes.FindAll(a => a.NodeType == EnmSSH.Device);
                var deviceMatchs = from node in deviceNodes
                                    join device in _workContext.RoleDevices on node.NodeId equals device.Current.Id
                                    select device.Current;

                data.data.devices = deviceMatchs.Any() ? string.Join(",", deviceMatchs.Select(a => a.Name)) : "无设备";
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveAppointment(AppointmentModel appointment, int action) {
            try {
                if(appointment == null)
                    throw new ArgumentException("参数无效 appointment");

                var startTime = DateTime.Parse(appointment.startDate);
                var endTime = DateTime.Parse(appointment.endDate);
                var interval = endTime.Subtract(startTime);
                if(interval.TotalSeconds < 0)
                    throw new ArgumentException("预约结束时间不能早于预约开始时间！");

                if(interval.TotalSeconds > 86400)
                    throw new ArgumentException("预约总时长不能超过24个小时！");

                if(appointment.nodes == null || appointment.nodes.Length == 0)
                    throw new ArgumentException("未勾选需要预约的监控点！");

                if(action == (int)EnmAction.Add) {
                    var newOne = new M_Reservation {
                        Id = Guid.NewGuid(),
                        StartTime = startTime,
                        EndTime = endTime,
                        ProjectId = new Guid(appointment.projectId),
                        Creator = _workContext.Employee.Name,
                        CreatedTime = DateTime.Now,
                        Comment = appointment.comment,
                        Enabled = appointment.enabled
                    };

                    var nodes = new List<M_NodeInReservation>();
                    foreach(var node in appointment.nodes) {
                         var keys = Common.SplitKeys(node);
                         if(keys.Length == 2) {
                             var type = int.Parse(keys[0]);
                             nodes.Add(new M_NodeInReservation {
                                  ReservationId = newOne.Id,
                                  NodeId = keys[1],
                                  NodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area
                             });
                         }
                    }

                    _appointmentService.Add(newOne);
                    _nodesInAppointmentService.Add(nodes);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增预约[{0}]", newOne.Id), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功！" });
                } else if(action == (int)EnmAction.Edit) {
                    var existed = _appointmentService.GetAppointment(new Guid(appointment.id));
                    if(existed.Creator != _workContext.Employee.Name)
                        throw new iPemException("您没有操作权限！");

                    existed.StartTime = startTime;
                    existed.EndTime = endTime;
                    existed.ProjectId = new Guid(appointment.projectId);
                    existed.Comment = appointment.comment;
                    existed.Enabled = appointment.enabled;

                    var nodes = new List<M_NodeInReservation>();
                    foreach(var node in appointment.nodes) {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            nodes.Add(new M_NodeInReservation {
                                ReservationId = existed.Id,
                                NodeId = keys[1],
                                NodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area
                            });
                        }
                    }

                    _appointmentService.Update(existed);
                    _nodesInAppointmentService.Remove(existed.Id);
                    _nodesInAppointmentService.Add(nodes);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新预约[{0}]", appointment.id), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "更新成功！" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteAppointment(string id) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var appointment = _appointmentService.GetAppointment(new Guid(id));

                if(appointment.Creator != _workContext.Employee.Name)
                    throw new ArgumentException("您没有操作权限！");

                if(appointment == null)
                    throw new iPemException("预约不存在，删除失败！");

                _appointmentService.Delete(appointment);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除预约[{0}]", appointment.Id), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功！" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadAppointments(DateTime startDate, DateTime endDate, int type, string keyWord) {
            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var appointments = _appointmentService.GetAppointmentsAsList(startDate, endDate);
                var projects = _projectsService.GetAllProjectsAsList();
                var result = (from app in appointments
                              join pro in projects on app.ProjectId equals pro.Id
                              select new {
                                  Appointment = app,
                                  Project = pro
                              }).ToList();

                if(!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());
                    if(type == (int)EnmSSH.Area) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Area);
                        var areas = _workContext.RoleAreas.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Device) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Device);
                        var devices = _workContext.RoleDevices.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Room) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Room);
                        var rooms = _workContext.RoleRooms.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmSSH.Station) {
                        var nodes = _nodesInAppointmentService.GetNodesAsList(EnmSSH.Station);
                        var stations = _workContext.RoleStations.FindAll(a => CommonHelper.ConditionContain(a.Current.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Current.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.ReservationId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                var models = new List<AppointmentModel>();
                if(result.Count > 0) {
                    for(int i = 0; i < result.Count; i++) {
                        models.Add(new AppointmentModel {
                            index = i + 1,
                            id = result[i].Appointment.Id.ToString(),
                            startDate = CommonHelper.DateTimeConverter(result[i].Appointment.StartTime),
                            endDate = CommonHelper.DateTimeConverter(result[i].Appointment.EndTime),
                            projectName = result[i].Project.Name,
                            creator = result[i].Appointment.Creator,
                            createdTime = CommonHelper.DateTimeConverter(result[i].Appointment.CreatedTime),
                            comment = result[i].Appointment.Comment,
                            enabled = result[i].Appointment.Enabled,
                        });
                    }
                }

                using(var ms = _excelManager.Export<AppointmentModel>(models, "工程预约信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        #endregion

    }
}