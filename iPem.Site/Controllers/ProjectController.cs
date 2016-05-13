using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.NPOI;
using iPem.Core.Enum;
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
using MsDomain = iPem.Core.Domain.Master;
using MsSrv = iPem.Services.Master;
using RsDomain = iPem.Core.Domain.Resource;
using RsSrv = iPem.Services.Resource;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ProjectController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IAppointmentService _msAppointmentService;
        private readonly MsSrv.INodesInAppointmentService _msNodesInAppointmentService;
        private readonly MsSrv.IProjectService _msProjectsService;

        #endregion

        #region Ctor

        public ProjectController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IAppointmentService msAppointmentService,
            MsSrv.INodesInAppointmentService msNodesInAppointmentService,
            MsSrv.IProjectService msProjectsService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._msAppointmentService = msAppointmentService;
            this._msNodesInAppointmentService = msNodesInAppointmentService;
            this._msProjectsService = msProjectsService;
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
        public JsonResult GetProjects(int start, int limit, string name, DateTime? startTime, DateTime? endTime) {
            var data = new AjaxDataModel<List<ProjectModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ProjectModel>()
            };

            try {
                var bom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var eom = bom.AddMonths(1).AddSeconds(-1);

                if(!startTime.HasValue)
                    startTime = bom;
                if(!endTime.HasValue)
                    endTime = eom;
                else
                    endTime.Value.AddSeconds(86399);
                
                var names = Common.SplitCondition(name);
                var projects = names.Length > 0 ? _msProjectsService.GetProjectsByName(names, startTime.Value, endTime.Value, start / limit, limit) : _msProjectsService.GetProjects(startTime.Value, endTime.Value, start / limit, limit);
                if(projects.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = projects.TotalCount;
                    for(var i = 0; i < projects.Count; i++) {
                        data.data.Add(new ProjectModel {
                            Index = start + i + 1,
                            Id = projects[i].Id.ToString(),
                            Name = projects[i].Name,
                            StartTime = CommonHelper.DateConverter(projects[i].StartTime),
                            EndTime = CommonHelper.DateConverter(projects[i].EndTime),
                            Responsible = projects[i].Responsible,
                            ContactPhone = projects[i].ContactPhone,
                            Company = projects[i].Company,
                            Creator = projects[i].Creator,
                            CreatedTime = CommonHelper.DateTimeConverter(projects[i].CreatedTime),
                            Comment = projects[i].Comment,
                            Enabled = projects[i].Enabled
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                data = new ProjectModel { Index = 1, Id = Guid.NewGuid().ToString(), Name = "", StartTime = CommonHelper.DateConverter(DateTime.Today), EndTime = CommonHelper.DateConverter(DateTime.Today.AddDays(7)), Responsible = "", ContactPhone = "", Company = "", Creator = "", CreatedTime = CommonHelper.DateConverter(DateTime.Now), Comment = "", Enabled = true }
            };
            
            try {
                if(action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var project = _msProjectsService.GetProject(new Guid(id));
                if(project == null)
                    throw new iPemException("未找到数据对象");

                data.data.Id = project.Id.ToString();
                data.data.Name = project.Name;
                data.data.StartTime = CommonHelper.DateConverter(project.StartTime);
                data.data.EndTime = CommonHelper.DateConverter(project.EndTime);
                data.data.Responsible = project.Responsible;
                data.data.ContactPhone = project.ContactPhone;
                data.data.Company = project.Company;
                data.data.Comment = project.Comment;
                data.data.Enabled = project.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                    var newProject = new MsDomain.Project {
                        Id = new Guid(project.Id),
                        Name = project.Name,
                        StartTime = Convert.ToDateTime(project.StartTime),
                        EndTime = Convert.ToDateTime(project.EndTime).AddDays(1).AddSeconds(-1),
                        Responsible = project.Responsible,
                        ContactPhone = project.ContactPhone,
                        Company = project.Company,
                        Creator = _workContext.AssociatedEmployee.Name,
                        CreatedTime = DateTime.Now,
                        Comment = project.Comment,
                        Enabled = project.Enabled
                    };

                    _msProjectsService.Insert(newProject);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增工程[{0}]", project.Name), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var projectId = new Guid(project.Id);
                    var existedProject = _msProjectsService.GetProject(projectId);
                    if(existedProject == null)
                        throw new iPemException("工程不存在，保存失败。");

                    //existedProject.Id = projectId;
                    existedProject.Name = project.Name;
                    existedProject.StartTime = Convert.ToDateTime(project.StartTime);
                    existedProject.EndTime = Convert.ToDateTime(project.EndTime).AddDays(1).AddSeconds(-1);
                    existedProject.Responsible = project.Responsible;
                    existedProject.ContactPhone = project.ContactPhone;
                    existedProject.Company = project.Company;
                    existedProject.Comment = project.Comment;
                    existedProject.Enabled = project.Enabled;

                    _msProjectsService.Update(existedProject);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新工程[{0}]", existedProject.Name), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }
                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadProjects(string name, DateTime? startTime, DateTime? endTime) {
            try {
                var bom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var eom = bom.AddMonths(1).AddSeconds(-1);

                if(!startTime.HasValue)
                    startTime = bom;
                if(!endTime.HasValue)
                    endTime = eom;
                else
                    endTime.Value.AddSeconds(86399);

                var names = Common.SplitCondition(name);
                var projects = names.Length > 0 ? _msProjectsService.GetProjectsByName(names, startTime.Value, endTime.Value) : _msProjectsService.GetProjects(startTime.Value, endTime.Value);

                var models = new List<ProjectModel>();
                for(var i = 0; i < projects.Count; i++) {
                    models.Add(new ProjectModel {
                        Index = i + 1,
                        Id = projects[i].Id.ToString(),
                        Name = projects[i].Name,
                        StartTime = CommonHelper.DateConverter(projects[i].StartTime),
                        EndTime = CommonHelper.DateConverter(projects[i].EndTime),
                        Responsible = projects[i].Responsible,
                        ContactPhone = projects[i].ContactPhone,
                        Company = projects[i].Company,
                        Creator = projects[i].Creator,
                        CreatedTime = CommonHelper.DateTimeConverter(projects[i].CreatedTime),
                        Comment = projects[i].Comment,
                        Enabled = projects[i].Enabled
                    });
                }

                using(var ms = _excelManager.Export<ProjectModel>(models, "工程信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetAppointments(int start, int limit, DateTime? startTime, DateTime? endTime, int? type, string keyWord) {
            var data = new AjaxDataModel<List<AppointmentModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<AppointmentModel>()
            };

            try {
                if(!startTime.HasValue)
                    startTime = DateTime.Today.AddDays(-7);
                if(!endTime.HasValue)
                    endTime = DateTime.Today.AddDays(7);

                endTime = endTime.Value.AddSeconds(86399);

                var appointments = _msAppointmentService.GetAppointmentsByDate(startTime.Value, endTime.Value);
                var projects = _msProjectsService.GetAllProjects();
                var result = (from app in appointments
                              join pro in projects on app.ProjectId equals pro.Id
                              select new {
                                  Appointment = app,
                                  Project = pro
                              }).ToList();

                if(!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());
                    if(type == (int)EnmOrganization.Area) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Area);
                        var areas = _workContext.AssociatedAreas.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.AreaId
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Device) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Device);
                        var devices = _workContext.AssociatedDevices.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Room) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Room);
                        var rooms = _workContext.AssociatedRooms.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Station) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Station);
                        var stations = _workContext.AssociatedStations.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                if(result != null && result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.Count;

                    var end = start + limit;
                    if(end > result.Count)
                        end = result.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new AppointmentModel {
                            index = start + i + 1,
                            id = result[i].Appointment.Id.ToString(),
                            startTime = CommonHelper.DateTimeConverter(result[i].Appointment.StartTime),
                            endTime = CommonHelper.DateTimeConverter(result[i].Appointment.EndTime),
                            projectName = result[i].Project.Name,
                            creator = result[i].Appointment.Creator,
                            createdTime = CommonHelper.DateTimeConverter(result[i].Appointment.CreatedTime),
                            comment = result[i].Appointment.Comment,
                            enabled = result[i].Appointment.Enabled,
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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
                var models = _msProjectsService.GetAllProjects().AsQueryable().Select(p => new ComboItem<string, string> { id = p.Id.ToString(), text = p.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit);
                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for(var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
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
        public JsonResult GetAppointment(string id, int action) {
            var data = new AjaxDataModel<AppointmentModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new AppointmentModel {
                    index = 1,
                    id = Guid.NewGuid().ToString(),
                    startTime = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(2100)),
                    endTime = CommonHelper.DateTimeConverter(DateTime.Now.AddSeconds(88500)),
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

                var appointment = _msAppointmentService.GetAppointment(new Guid(id));
                if(appointment == null)
                    throw new iPemException("未找到数据对象");

                var nodes = _msNodesInAppointmentService.GetNodesInAppointment(appointment.Id);

                data.data.id = appointment.Id.ToString();
                data.data.startTime = CommonHelper.DateTimeConverter(appointment.StartTime);
                data.data.endTime = CommonHelper.DateTimeConverter(appointment.EndTime);
                data.data.projectId = appointment.ProjectId.ToString();
                data.data.creator = appointment.Creator;
                data.data.createdTime = CommonHelper.DateTimeConverter(appointment.CreatedTime);
                data.data.comment = appointment.Comment;
                data.data.enabled = appointment.Enabled;
                data.data.nodes = nodes.Select(m => Common.JoinKeys((int)m.NodeType, m.NodeId)).ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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

                var nodes = _msNodesInAppointmentService.GetNodesInAppointment(new Guid(id)).ToList();

                //预约区域
                var areaNodes = nodes.FindAll(a => a.NodeType == EnmOrganization.Area);
                var areaMatchs = from node in areaNodes
                                 join area in _workContext.AssociatedAreas on node.NodeId equals area.AreaId
                                 select area;

                data.data.areas = areaMatchs.Any() ? string.Join(",", areaMatchs.Select(a => a.Name)) : "无区域";


                //预约站点
                var stationNodes = nodes.FindAll(a => a.NodeType == EnmOrganization.Station);
                var stationMatchs = from node in stationNodes
                                     join station in _workContext.AssociatedStations on node.NodeId equals station.Id
                                     select station;

                data.data.stations = stationMatchs.Any() ? string.Join(",", stationMatchs.Select(a => a.Name)) : "无站点";

                //预约机房
                var roomNodes = nodes.FindAll(a => a.NodeType == EnmOrganization.Room);
                var roomMatchs = from node in roomNodes
                                  join room in _workContext.AssociatedRooms on node.NodeId equals room.Id
                                  select room;

                data.data.rooms = roomMatchs.Any() ? string.Join(",", roomMatchs.Select(a => a.Name)) : "无机房";

                //预约设备
                var deviceNodes = nodes.FindAll(a => a.NodeType == EnmOrganization.Device);
                var deviceMatchs = from node in deviceNodes
                                    join device in _workContext.AssociatedDevices on node.NodeId equals device.Id
                                    select device;

                data.data.devices = deviceMatchs.Any() ? string.Join(",", deviceMatchs.Select(a => a.Name)) : "无设备";
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
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

                var startTime = DateTime.Parse(appointment.startTime);
                var endTime = DateTime.Parse(appointment.endTime);
                var start_end = endTime.Subtract(startTime);
                if(start_end.TotalSeconds < 0)
                    throw new ArgumentException("预约结束时间不能早于预约开始时间！");

                if(start_end.TotalSeconds > 86400)
                    throw new ArgumentException("预约总时长不能超过24个小时！");

                if(appointment.nodes == null || appointment.nodes.Length == 0)
                    throw new ArgumentException("未勾选需要预约的监控点！");

                if(action == (int)EnmAction.Add) {
                    var newAppointment = new MsDomain.Appointment {
                        Id = Guid.NewGuid(),
                        StartTime = startTime,
                        EndTime = endTime,
                        ProjectId = new Guid(appointment.projectId),
                        Creator = _workContext.AssociatedEmployee.Name,
                        CreatedTime = DateTime.Now,
                        Comment = appointment.comment,
                        Enabled = appointment.enabled
                    };

                    var nodes = new List<MsDomain.NodesInAppointment>();
                    foreach(var node in appointment.nodes) {
                         var keys = Common.SplitKeys(node);
                         if(keys.Length == 2) {
                             var type = int.Parse(keys[0]);
                             nodes.Add(new MsDomain.NodesInAppointment {
                                  AppointmentId = newAppointment.Id,
                                  NodeId = keys[1],
                                  NodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area
                             });
                         }
                    }

                    _msAppointmentService.AddAppointment(newAppointment);
                    _msNodesInAppointmentService.AddNodesInAppointment(nodes);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增预约[{0}]", newAppointment.Id), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功！" });
                } else if(action == (int)EnmAction.Edit) {
                    var existed = _msAppointmentService.GetAppointment(new Guid(appointment.id));
                    if(existed.Creator != _workContext.AssociatedEmployee.Name)
                        throw new iPemException("您没有操作权限！");

                    existed.StartTime = startTime;
                    existed.EndTime = endTime;
                    existed.ProjectId = new Guid(appointment.projectId);
                    existed.Comment = appointment.comment;
                    existed.Enabled = appointment.enabled;

                    var nodes = new List<MsDomain.NodesInAppointment>();
                    foreach(var node in appointment.nodes) {
                        var keys = Common.SplitKeys(node);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            nodes.Add(new MsDomain.NodesInAppointment {
                                AppointmentId = existed.Id,
                                NodeId = keys[1],
                                NodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area
                            });
                        }
                    }

                    _msAppointmentService.UpdateAppointment(existed);
                    _msNodesInAppointmentService.DeleteNodesInAppointment(existed.Id);
                    _msNodesInAppointmentService.AddNodesInAppointment(nodes);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新预约[{0}]", appointment.id), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "更新成功！" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteAppointment(string id) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var appointment = _msAppointmentService.GetAppointment(new Guid(id));

                if(appointment.Creator != _workContext.AssociatedEmployee.Name)
                    throw new ArgumentException("您没有操作权限！");

                if(appointment == null)
                    throw new iPemException("预约不存在，删除失败！");

                _msAppointmentService.DeleteAppointment(appointment);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除预约[{0}]", appointment.Id), null, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功！" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadAppointments(DateTime? startTime, DateTime? endTime, int? type, string keyWord) {
            try {
                if(!startTime.HasValue)
                    startTime = DateTime.Today.AddDays(-7);
                if(!endTime.HasValue)
                    endTime = DateTime.Today.AddDays(7);

                endTime = endTime.Value.AddSeconds(86399);

                var appointments = _msAppointmentService.GetAppointmentsByDate(startTime.Value, endTime.Value);
                var projects = _msProjectsService.GetAllProjects();
                var result = (from app in appointments
                              join pro in projects on app.ProjectId equals pro.Id
                              select new {
                                  Appointment = app,
                                  Project = pro
                              }).ToList();

                if(!string.IsNullOrWhiteSpace(keyWord)) {
                    var keyWords = Common.SplitCondition(keyWord.Trim());

                    if(type == (int)EnmOrganization.Area) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Area);
                        var areas = _workContext.AssociatedAreas.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join area in areas on node.NodeId equals area.AreaId
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Device) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Device);
                        var devices = _workContext.AssociatedDevices.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join device in devices on node.NodeId equals device.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Room) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Room);
                        var rooms = _workContext.AssociatedRooms.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join room in rooms on node.NodeId equals room.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else if(type == (int)EnmOrganization.Station) {
                        var nodes = _msNodesInAppointmentService.GetNodesByType(EnmOrganization.Station);
                        var stations = _workContext.AssociatedStations.FindAll(a => CommonHelper.ConditionContain(a.Name, keyWords));
                        var matchs = from node in nodes
                                     join station in stations on node.NodeId equals station.Id
                                     select node;

                        var ids = (from node in matchs
                                   group node by node.AppointmentId into g
                                   select g.Key).ToArray();

                        result = result.FindAll(a => ids.Contains(a.Appointment.Id));
                    } else {
                        result = result.FindAll(a => CommonHelper.ConditionContain(a.Project.Name, keyWords));
                    }
                }

                var models = new List<AppointmentModel>();

                if(result != null && result.Count > 0) {
                    for(int i = 0; i < result.Count; i++) {
                        models.Add(new AppointmentModel {
                            index = i + 1,
                            id = result[i].Appointment.Id.ToString(),
                            startTime = CommonHelper.DateTimeConverter(result[i].Appointment.StartTime),
                            endTime = CommonHelper.DateTimeConverter(result[i].Appointment.EndTime),
                            projectName = result[i].Project.Name,
                            creator = result[i].Appointment.Creator,
                            createdTime = CommonHelper.DateTimeConverter(result[i].Appointment.CreatedTime),
                            comment = result[i].Appointment.Comment,
                            enabled = result[i].Appointment.Enabled,
                        });
                    }
                }
                using(var ms = _excelManager.Export<AppointmentModel>(models, "工程预约信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {

                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetOrganization(string node) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
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
                                selected = false,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

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
                                            selected = false,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
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
                                                id = Common.JoinKeys((int)EnmOrganization.Station, roots[i].Id),
                                                text = roots[i].Name,
                                                selected = false,
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = false
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
                                            id = Common.JoinKeys((int)EnmOrganization.Station, current.FirstChildren[i].Id),
                                            text = current.FirstChildren[i].Name,
                                            selected = false,
                                            icon = Icons.Juzhan,
                                            expanded = false,
                                            leaf = false
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
                                                id = Common.JoinKeys((int)EnmOrganization.Room, rooms[i].Id),
                                                text = rooms[i].Name,
                                                selected = false,
                                                icon = Icons.Room,
                                                expanded = false,
                                                leaf = false
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
                                        id = Common.JoinKeys((int)EnmOrganization.Device, devices[i].Id),
                                        text = devices[i].Name,
                                        selected = false,
                                        icon = Icons.Device,
                                        expanded = false,
                                        leaf = true
                                    };

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
        public JsonResult GetOrganizationPath(string[] nodes) {
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
                            if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                                var match = _workContext.AssociatedAreaAttributes[id];
                                var paths = new List<string>();

                                if(match.HasParents) {
                                    foreach(var parent in match.Parents)
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.Current.AreaId));
                                data.data.Add(paths.ToArray());
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station organization
                            if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                                var match = _workContext.AssociatedStationAttributes[id];
                                var paths = new List<string>();

                                if(_workContext.AssociatedAreaAttributes.ContainsKey(match.Current.AreaId)) {
                                    var current = _workContext.AssociatedAreaAttributes[match.Current.AreaId];
                                    if(current.HasParents) {
                                        foreach(var parent in current.Parents)
                                            paths.Add(Common.JoinKeys((int)EnmOrganization.Area, parent.AreaId));
                                    }
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Area, match.Current.AreaId));

                                if(match.HasParents) {
                                    foreach(var parent in match.Parents)
                                        paths.Add(Common.JoinKeys((int)EnmOrganization.Station, parent.Id));
                                }
                                paths.Add(Common.JoinKeys((int)EnmOrganization.Station, match.Current.Id));
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

        #endregion

    }
}