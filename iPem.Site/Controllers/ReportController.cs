using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MsDomain = iPem.Core.Domain.Master;
using HsDomain = iPem.Core.Domain.History;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using HsSrv = iPem.Services.History;
using RsSrv = iPem.Services.Resource;

namespace iPem.Site.Controllers {
    public class ReportController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly RsSrv.IEnumMethodsService _rsEnumMethodsService;
        private readonly RsSrv.IStationTypeService _rsStationTypeService;
        private readonly RsSrv.ISubDeviceTypeService _rsSubDeviceTypeService;
        private readonly RsSrv.IProductorService _rsProductorService;
        private readonly RsSrv.IBrandService _rsBrandService;
        private readonly RsSrv.ISupplierService _rsSupplierService;
        private readonly RsSrv.ISubCompanyService _rsSubCompanyService;
        private readonly HsSrv.IHisAlmService _hsHisAlmService;

        #endregion

        #region Ctor

        public ReportController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            RsSrv.IEnumMethodsService rsEnumMethodsService,
            RsSrv.IStationTypeService rsStationTypeService,
            RsSrv.ISubDeviceTypeService rsSubDeviceTypeService,
            RsSrv.IProductorService rsProductorService,
            RsSrv.IBrandService rsBrandService,
            RsSrv.ISupplierService rsSupplierService,
            RsSrv.ISubCompanyService rsSubCompanyService,
            HsSrv.IHisAlmService hsHisAlmService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._rsEnumMethodsService = rsEnumMethodsService;
            this._rsStationTypeService = rsStationTypeService;
            this._rsSubDeviceTypeService = rsSubDeviceTypeService;
            this._rsProductorService = rsProductorService;
            this._rsBrandService = rsBrandService;
            this._rsSupplierService = rsSupplierService;
            this._rsSubCompanyService = rsSubCompanyService;
            this._hsHisAlmService = hsHisAlmService;
        }

        #endregion

        #region Actions

        [Authorize]
        public ActionResult Base(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("base{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult History(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("history{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Chart(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("chart{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [Authorize]
        public ActionResult Custom(int? id) {
            if(id.HasValue && _workContext.AssociatedMenus.Any(m => m.Id == id.Value))
                return View(string.Format("custom{0}", id.Value));

            throw new HttpException(404, "Page not found.");
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400101(int start, int limit, string parent, int[] types) {
            var data = new AjaxChartModel<List<Model400101>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400101>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400101(parent, types);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400101(string parent, int[] types) {
            try {
                var models = this.GetBase400101(parent, types);
                using(var ms = _excelManager.Export<Model400101>(models, "区域统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400102(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400102>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400102>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400102(parent, types);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400102(string parent, string[] types) {
            try {
                var models = this.GetBase400102(parent, types);
                using(var ms = _excelManager.Export<Model400102>(models, "站点统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400103(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400103>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400103>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400103(parent, types);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400103(string parent, string[] types) {
            try {
                var models = this.GetBase400103(parent, types);
                using(var ms = _excelManager.Export<Model400103>(models, "机房统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestBase400104(int start, int limit, string parent, string[] types) {
            var data = new AjaxChartModel<List<Model400104>, List<ChartModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400104>(),
                chart = new List<ChartModel>()
            };

            try {
                var models = this.GetBase400104(parent, types);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }

                    var groups = from model in models
                                 group model by model.type into g
                                 select new {
                                     Key = g.Key,
                                     Count = g.Count()
                                 };

                    foreach(var group in groups) {
                        data.chart.Add(new ChartModel {
                            name = group.Key,
                            value = group.Count
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadBase400104(string parent, string[] types) {
            try {
                var models = this.GetBase400104(parent, types);
                using(var ms = _excelManager.Export<Model400104>(models, "设备统计列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestHistory400202(int start, int limit, string parent, DateTime starttime, DateTime endtime,  string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var data = new AjaxChartModel<List<Model400202>, List<ChartModel>[]> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Model400202>(),
                chart = new List<ChartModel>[3]
            };

            try {
                var models = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(new Model400202 {
                            index = start + i + 1,
                            id = models[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(models[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(models[i].Device.Station).Select(a => a.Name)),
                            room = models[i].Device.Room.Name,
                            devType = models[i].Device.Type.Name,
                            device = models[i].Device.Current.Name,
                            logic = models[i].Point.LogicType.Name,
                            point = models[i].Point.Current.Name,
                            levelValue = (int)models[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(models[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(models[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(models[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", models[i].Current.StartValue, models[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", models[i].Current.EndValue, models[i].Current.ValueUnit),
                            almComment = models[i].Current.AlmDesc,
                            normalComment = models[i].Current.NormalDesc,
                            frequency = models[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(models[i].Current.EndType),
                            project = models[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(models[i].Current.ConfirmedStatus),
                            confirmedTime = models[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(models[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = models[i].Current.Confirmer
                        });
                    }

                    data.chart[0] = this.GetHistory400202Chart1(models);
                    data.chart[1] = this.GetHistory400202Chart2(models);
                    data.chart[2] = this.GetHistory400202Chart3(parent, models);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            try {
                var models = new List<Model400202>();
                var cached = this.GetHistory400202(parent, starttime, endtime, statypes, roomtypes, devtypes, almlevels, logictypes, pointname, confirm, project);
                if(cached != null && cached.Count > 0) {
                    for(int i = 0; i < cached.Count; i++) {
                        models.Add(new Model400202 {
                            index = i + 1,
                            id = cached[i].Current.Id,
                            area = string.Join(",", _workContext.GetParentsInArea(cached[i].Device.Area).Select(a => a.Name)),
                            station = string.Join(",", _workContext.GetParentsInStation(cached[i].Device.Station).Select(a => a.Name)),
                            room = cached[i].Device.Room.Name,
                            devType = cached[i].Device.Type.Name,
                            device = cached[i].Device.Current.Name,
                            logic = cached[i].Point.LogicType.Name,
                            point = cached[i].Point.Current.Name,
                            levelValue = (int)cached[i].Current.AlmLevel,
                            levelDisplay = Common.GetAlarmLevelDisplay(cached[i].Current.AlmLevel),
                            startTime = CommonHelper.DateTimeConverter(cached[i].Current.StartTime),
                            endTime = CommonHelper.DateTimeConverter(cached[i].Current.EndTime),
                            startValue = string.Format("{0:F2} {1}", cached[i].Current.StartValue, cached[i].Current.ValueUnit),
                            endValue = string.Format("{0:F2} {1}", cached[i].Current.EndValue, cached[i].Current.ValueUnit),
                            almComment = cached[i].Current.AlmDesc,
                            normalComment = cached[i].Current.NormalDesc,
                            frequency = cached[i].Current.Frequency,
                            endType = Common.GetEndTypeDisplay(cached[i].Current.EndType),
                            project = cached[i].Current.ProjectId,
                            confirmedStatus = Common.GetConfirmStatusDisplay(cached[i].Current.ConfirmedStatus),
                            confirmedTime = cached[i].Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(cached[i].Current.ConfirmedTime.Value) : string.Empty,
                            confirmer = cached[i].Current.Confirmer
                        });
                    }
                }

                using(var ms = _excelManager.Export<Model400202>(models, "历史告警列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<Model400101> GetBase400101(string parent, int[] types) {
            var index = 0;
            var result = new List<Model400101>();

            var methods = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Area, "类型");
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                #region root
                var data = from area in _workContext.AssociatedAreas
                           join method in methods on area.NodeLevel equals method.Id into temp
                           from dm in temp.DefaultIfEmpty()
                           where types == null || types.Length == 0 || types.Contains(area.NodeLevel)
                           select new {
                               Area = area,
                               Method = dm != null ? dm : new RsDomain.EnumMethods { Name = "未定义", Index = int.MaxValue }
                           };

                var ordered = data.OrderBy(d => d.Method.Index);
                foreach(var area in ordered) {
                    result.Add(new Model400101 {
                        index = ++index,
                        id = area.Area.AreaId,
                        name = area.Area.Name,
                        type = area.Method.Name,
                        comment = area.Area.Comment,
                        enabled = area.Area.Enabled
                    });
                }
                #endregion
            } else {
                #region children
                if(_workContext.AssociatedAreaAttributes.ContainsKey(parent)) {
                    var current = _workContext.AssociatedAreaAttributes[parent];
                    if(current.HasChildren) {
                        var data = from area in current.Children
                                   join method in methods on area.NodeLevel equals method.Id into temp
                                   from dm in temp.DefaultIfEmpty()
                                   where types == null || types.Length == 0 || types.Contains(area.NodeLevel)
                                   select new {
                                       Area = area,
                                       Method = dm != null ? dm : new RsDomain.EnumMethods { Name = "未定义", Index = int.MaxValue }
                                   };

                        var ordered = data.OrderBy(d => d.Method.Index);
                        foreach(var area in ordered) {
                            result.Add(new Model400101 {
                                index = ++index,
                                id = area.Area.AreaId,
                                name = area.Area.Name,
                                type = area.Method.Name,
                                comment = area.Area.Comment,
                                enabled = area.Area.Enabled
                            });
                        }
                    }
                }
                #endregion
            }

            return result;
        }

        private List<Model400102> GetBase400102(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400102>();

            var stations = _workContext.AssociatedStations;
            var stationTypes = _rsStationTypeService.GetAllStationTypes().ToList();
            var loadTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Station, "市电引入方式");
            var powerTypes = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Station, "供电性质");

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var matchs = new Dictionary<string, string>();
                if(_workContext.AssociatedAreaAttributes.ContainsKey(parent)) {
                    var current = _workContext.AssociatedAreaAttributes[parent];
                    matchs[current.Current.AreaId] = current.Current.Name;

                    if(current.HasChildren) {
                        foreach(var child in current.Children)
                            matchs[child.AreaId] = child.Name;
                    }
                }

                stations = stations.FindAll(s => matchs.ContainsKey(s.AreaId));
            }

            if(types != null && types.Length > 0)
                stationTypes = stationTypes.FindAll(st => types.Contains(st.Id));

            var query = from station in stations
                        join st in stationTypes on station.StaTypeId equals st.Id
                        join lt in loadTypes on station.CityElecLoadTypeId equals lt.Id into temp1
                        from defaultLt in temp1.DefaultIfEmpty()
                        join pt in powerTypes on station.SuppPowerTypeId equals pt.Id into temp2
                        from defaultPt in temp2.DefaultIfEmpty()
                        orderby station.StaTypeId
                        select new {
                            Station = station,
                            StaType = st,
                            LoadType = defaultLt ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 },
                            PowerType = defaultPt ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 }
                        };

            foreach(var q in query) {
                result.Add(new Model400102 {
                    index = ++index,
                    id = q.Station.Id,
                    name = q.Station.Name,
                    type = q.StaType.Name,
                    longitude = q.Station.Longitude,
                    latitude = q.Station.Latitude,
                    altitude = q.Station.Altitude,
                    cityelecloadtype = q.LoadType.Name,
                    cityeleccap = q.Station.CityElecCap,
                    cityelecload = q.Station.CityElecLoad,
                    contact = q.Station.Contact,
                    lineradiussize = q.Station.LineRadiusSize,
                    linelength = q.Station.LineLength,
                    supppowertype = q.PowerType.Name,
                    traninfo = q.Station.TranInfo,
                    trancontno = q.Station.TranContNo,
                    tranphone = q.Station.TranPhone,
                    comment = q.Station.Comment,
                    enabled = q.Station.Enabled
                });
            }

            return result;
        }

        private List<Model400103> GetBase400103(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400103>();

            var methods = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Room, "产权");
            var rooms = from room in _workContext.AssociatedRoomAttributes.Values
                        join method in methods on room.Current.PropertyId equals method.Id into temp
                        from defaultMethod in temp.DefaultIfEmpty()
                        where types == null || types.Length == 0 || types.Contains(room.Current.RoomTypeId)
                        orderby room.Current.RoomTypeId, room.Current.Name
                        select new {
                            Room = room,
                            Method = defaultMethod != null ? defaultMethod.Name : "未定义"
                        };

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        #region area
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedAreaAttributes[id];
                            matchs[current.Current.AreaId] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.AreaId] = child.Name;
                            }
                        }

                        rooms = rooms.Where(r => matchs.ContainsKey(r.Room.Area.AreaId));
                        #endregion
                    } else if(nodeType == EnmOrganization.Station) {
                        #region station
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedStationAttributes[id];
                            matchs[current.Current.Id] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.Id] = child.Name;
                            }
                        }

                        rooms = rooms.Where(r => matchs.ContainsKey(r.Room.Station.Id));
                        #endregion
                    }
                }
            }

            foreach(var room in rooms) {
                result.Add(new Model400103 {
                    index = ++index,
                    id = room.Room.Current.Id,
                    name = room.Room.Current.Name,
                    type = room.Room.Type.Name,
                    property = room.Method,
                    address = room.Room.Current.Address,
                    floor = room.Room.Current.Floor,
                    length = room.Room.Current.Length,
                    width = room.Room.Current.Width,
                    height = room.Room.Current.Heigth,
                    floorLoad = room.Room.Current.FloorLoad,
                    lineHeigth = room.Room.Current.LineHeigth,
                    square = room.Room.Current.Square,
                    effeSquare = room.Room.Current.EffeSquare,
                    fireFighEuip = room.Room.Current.FireFighEuip,
                    owner = room.Room.Current.Owner,
                    queryPhone = room.Room.Current.QueryPhone,
                    powerSubMain = room.Room.Current.PowerSubMain,
                    tranSubMain = room.Room.Current.TranSubMain,
                    enviSubMain = room.Room.Current.EnviSubMain,
                    fireSubMain = room.Room.Current.FireFighEuip,
                    airSubMain = room.Room.Current.AirSubMain,
                    contact = room.Room.Current.Contact,
                    comment = room.Room.Current.Comment,
                    enabled = room.Room.Current.Enabled
                });
            }

            return result;
        }

        private List<Model400104> GetBase400104(string parent, string[] types) {
            var index = 0;
            var result = new List<Model400104>();

            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(types != null && types.Length > 0)
                devices = devices.FindAll(d => types.Contains(d.Current.DeviceTypeId));

            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        #region area
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedAreaAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedAreaAttributes[id];
                            matchs[current.Current.AreaId] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.AreaId] = child.Name;
                            }
                        }

                        devices = devices.FindAll(d => matchs.ContainsKey(d.Area.AreaId));
                        #endregion
                    } else if(nodeType == EnmOrganization.Station) {
                        #region station
                        var matchs = new Dictionary<string, string>();
                        if(_workContext.AssociatedStationAttributes.ContainsKey(id)) {
                            var current = _workContext.AssociatedStationAttributes[id];
                            matchs[current.Current.Id] = current.Current.Name;

                            if(current.HasChildren) {
                                foreach(var child in current.Children)
                                    matchs[child.Id] = child.Name;
                            }
                        }

                        devices = devices.FindAll(d => matchs.ContainsKey(d.Station.Id));
                        #endregion
                    } else if(nodeType == EnmOrganization.Room) {
                        #region room
                        devices = devices.FindAll(d => d.Room.Id == id);
                        #endregion
                    }
                }
            }

            var subDeviceTypes = _rsSubDeviceTypeService.GetAllSubDeviceTypes();
            var productors = _rsProductorService.GetAllProductors();
            var brands = _rsBrandService.GetAllBrands();
            var suppliers = _rsSupplierService.GetAllSuppliers();
            var subCompanys = _rsSubCompanyService.GetAllSubCompanies();
            var status = _rsEnumMethodsService.GetEnumMethods(EnmMethodType.Device, "使用状态");

            var query = from device in devices
                        join sdt in subDeviceTypes on device.Current.SubDeviceTypeId equals sdt.Id into temp0
                        from defaulSdt in temp0.DefaultIfEmpty()
                        join pdr in productors on device.Current.ProdId equals pdr.Id into temp1
                        from defaulPdr in temp1.DefaultIfEmpty()
                        join brd in brands on device.Current.BrandId equals brd.Id into temp2
                        from defaulBrd in temp2.DefaultIfEmpty()
                        join spr in suppliers on device.Current.SuppId equals spr.Id into temp3
                        from defaultSpr in temp3.DefaultIfEmpty()
                        join scy in subCompanys on device.Current.SubCompId equals scy.Id into temp4
                        from defaultScy in temp4.DefaultIfEmpty()
                        join sus in status on device.Current.StatusId equals sus.Id into temp5
                        from defaultSus in temp5.DefaultIfEmpty()
                        orderby device.Current.DeviceTypeId
                        select new {
                            Device = device.Current,
                            Type  = device.Type,
                            SubDeviceType = defaulSdt != null ? defaulSdt.Name : null,
                            Productor = defaulPdr != null ? defaulPdr.Name : null,
                            Brand = defaulBrd != null ? defaulBrd.Name : null,
                            Supplier = defaultSpr != null ? defaultSpr.Name : null,
                            SubCompany = defaultScy != null ? defaultScy.Name : null,
                            Status = defaultSus ?? new RsDomain.EnumMethods { Name = "未定义", Index = 0 }
                        };

            foreach(var q in query) {
                result.Add(new Model400104 {
                    index = ++index,
                    id = q.Device.Id,
                    code = q.Device.Code,
                    name = q.Device.Name,
                    type = q.Type.Name,
                    subType = q.SubDeviceType,
                    sysName = q.Device.SysName,
                    sysCode = q.Device.SysCode,
                    model = q.Device.Model,
                    productor = q.Productor,
                    brand = q.Brand,
                    supplier = q.Supplier,
                    subCompany = q.SubCompany,
                    startTime = CommonHelper.DateTimeConverter(q.Device.StartTime),
                    scrapTime = CommonHelper.DateTimeConverter(q.Device.ScrapTime),
                    status = q.Status.Name,
                    contact = q.Device.Contact,
                    comment = q.Device.Comment,
                    enabled = q.Device.Enabled
                });
            }

            return result;
        }

        private List<AlmStore<HsDomain.HisAlm>> GetHistory400202(string parent, DateTime starttime, DateTime endtime, string[] statypes, string[] roomtypes, string[] devtypes, int[] almlevels, string[] logictypes, string pointname, string confirm, string project) {
            var devices = _workContext.AssociatedDeviceAttributes.Values.ToList();
            if(!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var nodeid = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
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

                    if(nodeType == EnmOrganization.Station) {
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

                    if(nodeType == EnmOrganization.Room)
                        devices = devices.FindAll(d => d.Room.Id == nodeid);

                    if(nodeType == EnmOrganization.Device)
                        devices = devices.FindAll(d => d.Current.Id == nodeid);
                }
            }

            if(statypes != null && statypes.Length > 0)
                devices = devices.FindAll(d => statypes.Contains(d.Station.StaTypeId));

            if(roomtypes != null && roomtypes.Length > 0)
                devices = devices.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

            if(devtypes != null && devtypes.Length > 0)
                devices = devices.FindAll(d => devtypes.Contains(d.Current.DeviceTypeId));

            var points = _workContext.AssociatedPointAttributes.Values.ToList();
                points = points.FindAll(p => p.Current.Type == EnmPoint.AI || p.Current.Type == EnmPoint.DI);

            if(logictypes != null && logictypes.Length > 0)
                points = points.FindAll(p => logictypes.Contains(p.Current.LogicTypeId));

            if(!string.IsNullOrWhiteSpace(pointname)) {
                var names = Common.SplitCondition(pointname);
                if(names.Length > 0) points = points.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, names));
            }

            var currentAlarms = _hsHisAlmService.GetHisAlms(starttime, endtime).ToList();
            if(almlevels != null && almlevels.Length > 0)
                currentAlarms = currentAlarms.FindAll(a => almlevels.Contains((int)a.AlmLevel));

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
                          select new AlmStore<HsDomain.HisAlm> {
                              Current = alarm,
                              Point = point,
                              Device = device,
                          }).ToList();

            return models;
        }

        private List<ChartModel> GetHistory400202Chart1(List<AlmStore<HsDomain.HisAlm>> models) {
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

        private List<ChartModel> GetHistory400202Chart2(List<AlmStore<HsDomain.HisAlm>> models) {
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

        private List<ChartModel> GetHistory400202Chart3(string parent, List<AlmStore<HsDomain.HisAlm>> models) {
            var data = new List<ChartModel>();

            try {
                if(models != null && models.Count > 0) {
                    if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
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
                    } else {
                        var keys = Common.SplitKeys(parent);
                        if(keys.Length == 2) {
                            var type = int.Parse(keys[0]);
                            var nodeid = keys[1];
                            var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                            if(nodeType == EnmOrganization.Area) {
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
                            } else if(nodeType == EnmOrganization.Station) {
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
                            } else if(nodeType == EnmOrganization.Room) {
                                #region room
                                var devices = _workContext.AssociatedDevices.FindAll(d => d.RoomId == nodeid);
                                foreach(var device in devices) {
                                    var count = models.Count(m => m.Device.Current.Id == device.Id);
                                    if(count > 0)
                                        data.Add(new ChartModel { name = device.Name, value = count, comment = "" });
                                }
                                #endregion
                            } else if(nodeType == EnmOrganization.Device) {
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
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
            }

            return data;
        }

        #endregion

    }
}