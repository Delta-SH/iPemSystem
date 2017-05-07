using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class FsuController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebLogger _webLogger;
        private readonly IDictionaryService _dictionaryService;
        private readonly IFsuService _fsuService;
        private readonly IHisFtpService _ftpService;

        #endregion

        #region Ctor

        public FsuController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IFsuService fsuService,
            IHisFtpService ftpService,
            IDictionaryService dictionaryService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._fsuService = fsuService;
            this._ftpService = ftpService;
            this._dictionaryService = dictionaryService;
        }

        #endregion

        #region Actions

        public ActionResult Index(int? id) {
            return View();
        }

        public ActionResult Configuration(int? id) {
            return View();
        }

        public ActionResult Ftp(int? id) {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult RequestFsu(int start, int limit, string parent, int[] status, int filter, string keywords) {
            var data = new AjaxDataModel<List<FsuModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FsuModel>()
            };

            try {
                var models = this.GetFsus(parent, status, filter, keywords);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadFsu(string parent, int[] status, int filter, string keywords) {
            try {
                var models = this.GetFsus(parent, status, filter, keywords);
                using(var ms = _excelManager.Export<FsuModel>(models, "FSU信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestFtp(int start, int limit, string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            var data = new AjaxDataModel<List<FtpModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FtpModel>()
            };

            try {
                var models = this.GetFtps(parent, types, startDate, endDate, filter, keywords);
                if(models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if(end > models.Count)
                        end = models.Count;

                    for(int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadFtp(string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            try {
                var models = this.GetFtps(parent, types, startDate, endDate, filter, keywords);
                using(var ms = _excelManager.Export<FtpModel>(models, "FTP日志信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<FsuModel> GetFsus(string parent, int[] status, int filter, string keywords) {
            var result = new List<FsuModel>();
            var fsus = new List<OrgFsu>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus = _workContext.RoleFsus;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            fsus = _workContext.RoleFsus.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        fsus = _workContext.RoleFsus.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmOrganization.Room) {
                        var current = _workContext.RoleRooms.Find(a => a.Current.Id == id);
                        if(current != null) fsus = current.Fsus;
                    }
                }
            }

            if(!string.IsNullOrWhiteSpace(keywords)) {
                var conditions = Common.SplitCondition(keywords);
                switch(filter) {
                    case 1:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Current.Code, conditions));
                        break;
                    case 2:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Current.Name, conditions));
                        break;
                    default:
                        break;
                }
            }

            var extends = _fsuService.GetAllExtendsAsList();
            if(status != null && status.Length > 0)
                extends = extends.FindAll(e => (status.Contains(1) && e.Status) || (status.Contains(0) && !e.Status));

            var stores = from fsu in fsus
                         join ext in extends on fsu.Current.Id equals ext.Id
                         join room in _workContext.RoleRooms on fsu.Current.RoomId equals room.Current.Id
                         join station in _workContext.RoleStations on fsu.Current.StationId equals station.Current.Id
                         join area in _workContext.RoleAreas on fsu.Current.AreaId equals area.Current.Id
                         select new FsuModel {
                             id = fsu.Current.Id,
                             code = fsu.Current.Code,
                             name = fsu.Current.Name,
                             area = area.ToString(),
                             station = station.Current.Name,
                             room = room.Current.Name,
                             ip = ext.IP ?? string.Empty,
                             port = ext.Port,
                             last = CommonHelper.DateTimeConverter(ext.LastTime),
                             change = CommonHelper.DateTimeConverter(ext.ChangeTime),
                             status = ext.Status ? "在线" : "离线",
                             comment = ext.Comment ?? string.Empty
                         };

            var index = 0;
            foreach(var store in stores.OrderBy(s=>s.code)) {
                result.Add(new FsuModel {
                    index = ++index,
                    id = store.id,
                    code = store.code,
                    name = store.name,
                    area = store.area,
                    station = store.station,
                    room = store.room,
                    ip = store.ip,
                    port = store.port,
                    last = store.last,
                    change = store.change,
                    status = store.status,
                    comment = store.comment
                });
            }

            return result;
        }

        private List<FtpModel> GetFtps(string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            var result = new List<FtpModel>();
            var fsus = new List<OrgFsu>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus = _workContext.RoleFsus;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if(nodeType == EnmOrganization.Area) {
                        var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                        if(current != null)
                            fsus = _workContext.RoleFsus.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmOrganization.Station) {
                        fsus = _workContext.RoleFsus.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmOrganization.Room) {
                        var current = _workContext.RoleRooms.Find(a => a.Current.Id == id);
                        if(current != null) fsus = current.Fsus;
                    }
                }
            }

            if(!string.IsNullOrWhiteSpace(keywords)) {
                var conditions = Common.SplitCondition(keywords);
                switch(filter) {
                    case 1:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Current.Code, conditions));
                        break;
                    case 2:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Current.Name, conditions));
                        break;
                    default:
                        break;
                }
            }

            var events = _ftpService.GetEventsAsList(startDate, endDate, EnmFtpEvent.FTP);
            if(events != null && events.Count > 0)
                extends = extends.FindAll(e => (status.Contains(1) && e.Status) || (status.Contains(0) && !e.Status));

            var stores = from fsu in fsus
                         join ext in extends on fsu.Current.Id equals ext.Id
                         join room in _workContext.RoleRooms on fsu.Current.RoomId equals room.Current.Id
                         join station in _workContext.RoleStations on fsu.Current.StationId equals station.Current.Id
                         join area in _workContext.RoleAreas on fsu.Current.AreaId equals area.Current.Id
                         select new FsuModel {
                             id = fsu.Current.Id,
                             code = fsu.Current.Code,
                             name = fsu.Current.Name,
                             area = area.ToString(),
                             station = station.Current.Name,
                             room = room.Current.Name,
                             ip = ext.IP ?? string.Empty,
                             port = ext.Port,
                             last = CommonHelper.DateTimeConverter(ext.LastTime),
                             change = CommonHelper.DateTimeConverter(ext.ChangeTime),
                             status = ext.Status ? "在线" : "离线",
                             comment = ext.Comment ?? string.Empty
                         };

            var index = 0;
            foreach(var store in stores.OrderBy(s => s.code)) {
                result.Add(new FsuModel {
                    index = ++index,
                    id = store.id,
                    code = store.code,
                    name = store.name,
                    area = store.area,
                    station = store.station,
                    room = store.room,
                    ip = store.ip,
                    port = store.port,
                    last = store.last,
                    change = store.change,
                    status = store.status,
                    comment = store.comment
                });
            }

            return result;
        }

        #endregion
    }
}