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
using iPem.Site.Models.SSH;
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
        private readonly IWebEventService _webLogger;
        private readonly IDictionaryService _dictionaryService;
        private readonly IFsuService _fsuService;
        private readonly IFsuEventService _ftpService;

        #endregion

        #region Ctor

        public FsuController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IFsuService fsuService,
            IFsuEventService ftpService,
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
            if (!_workContext.Authorizations.Menus.Any(m => m.Id == 2001))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Configuration(int? id) {
            if (!_workContext.Authorizations.Menus.Any(m => m.Id == 2002))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Ftp(int? id) {
            if (!_workContext.Authorizations.Menus.Any(m => m.Id == 2003))
                throw new HttpException(404, "Page not found.");

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
            var fsus = new List<SSHFsu>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus = _workContext.Fsus;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null) fsus = _workContext.Fsus.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        fsus = _workContext.Fsus.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms.Find(a => a.Current.Id == id);
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

            var extFsus = _fsuService.GetExtFsus();
            if(status != null && status.Length > 0)
                extFsus = extFsus.FindAll(e => (status.Contains(1) && e.Status) || (status.Contains(0) && !e.Status));

            var stores = from fsu in fsus
                         join ext in extFsus on fsu.Current.Id equals ext.Id
                         join area in _workContext.Areas on fsu.Current.AreaId equals area.Current.Id
                         select new FsuModel {
                             id = fsu.Current.Id,
                             code = fsu.Current.Code,
                             name = fsu.Current.Name,
                             area = area.ToString(),
                             station = fsu.Current.StationName,
                             room = fsu.Current.RoomName,
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
            endDate = endDate.AddDays(1).AddMilliseconds(-1);

            var result = new List<FtpModel>();
            var fsus = new List<SSHFsu>();
            if(string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus = _workContext.Fsus;
            } else {
                var keys = Common.SplitKeys(parent);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if(nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas.Find(a => a.Current.Id == id);
                        if(current != null)
                            fsus = _workContext.Fsus.FindAll(s => current.Keys.Contains(s.Current.AreaId));
                    } else if(nodeType == EnmSSH.Station) {
                        fsus = _workContext.Fsus.FindAll(d => d.Current.StationId == id);
                    } else if(nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms.Find(a => a.Current.Id == id);
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

            var events = _ftpService.GetEventsInType(startDate, endDate, EnmFsuEvent.FTP);
            if (types != null && types.Length > 0)
                events = events.FindAll(e => types.Contains((int)e.EventType));

            var stores = from evt in events
                         join fsu in fsus on evt.FsuId equals fsu.Current.Id
                         join room in _workContext.Rooms on fsu.Current.RoomId equals room.Current.Id
                         join station in _workContext.Stations on fsu.Current.StationId equals station.Current.Id
                         join area in _workContext.Areas on fsu.Current.AreaId equals area.Current.Id
                         select new FtpModel {
                             id = fsu.Current.Id,
                             code = fsu.Current.Code,
                             name = fsu.Current.Name,
                             area = area.ToString(),
                             station = station.Current.Name,
                             room = room.Current.Name,
                             type = Common.GetFsuEventDisplay(evt.EventType),
                             message = evt.Message ?? string.Empty,
                             time = CommonHelper.DateTimeConverter(evt.EventTime)
                         };

            var index = 0;
            foreach (var store in stores.OrderByDescending(s => s.time)) {
                result.Add(new FtpModel {
                    index = ++index,
                    id = store.id,
                    code = store.code,
                    name = store.name,
                    area = store.area,
                    station = store.station,
                    room = store.room,
                    type = store.type,
                    message = store.message,
                    time = store.time
                });
            }

            return result;
        }

        #endregion

    }
}