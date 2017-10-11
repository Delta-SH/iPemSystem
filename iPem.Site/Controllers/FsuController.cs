using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.BInterface;
using iPem.Site.Models.SSH;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IGroupService _groupService;
        private readonly IParamDiffService _diffService;
        private readonly INoteService _noteService;
        private readonly IPackMgr _packMgr;

        #endregion

        #region Ctor

        public FsuController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IFsuService fsuService,
            IFsuEventService ftpService,
            IDictionaryService dictionaryService,
            IGroupService groupService,
            IParamDiffService diffService,
            INoteService noteService,
            IPackMgr packMgr) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._fsuService = fsuService;
            this._ftpService = ftpService;
            this._dictionaryService = dictionaryService;
            this._groupService = groupService;
            this._diffService = diffService;
            this._noteService = noteService;
            this._packMgr = packMgr;
        }

        #endregion

        #region Actions

        public ActionResult Index(int? id) {
            if (!_workContext.Authorizations().Menus.Contains(2001))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Configuration(int? id) {
            if (!_workContext.Authorizations().Menus.Contains(2002))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Event(int? id) {
            if (!_workContext.Authorizations().Menus.Contains(2003))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult ParamDiff(int? id) {
            if (!_workContext.Authorizations().Menus.Contains(2004))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        [AjaxAuthorize]
        public JsonResult RequestFsu(int start, int limit, string parent, int[] status, string[] vendors, int filter, string keywords) {
            var data = new AjaxDataModel<List<FsuModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FsuModel>()
            };

            try {
                var models = this.GetFsus(parent, status, vendors, filter, keywords);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadFsu(string parent, int[] status, string[] vendors, int filter, string keywords) {
            try {
                var models = this.GetFsus(parent, status, vendors, filter, keywords);
                using(var ms = _excelManager.Export<FsuModel>(models, "FSU信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestFtpFiles(int start, int limit, string id) {
            var data = new AjaxDataModel<List<FtpFileModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FtpFileModel>()
            };

            try {
                var models = this.GetFtpFiles(id);
                if (models != null) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadFtpFile(string fsu, string path) {
            try {
                if (string.IsNullOrWhiteSpace(fsu)) throw new ArgumentNullException("fsu");
                if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

                var current = _workContext.Fsus().Find(f => f.Current.Id == fsu);
                if (current == null) throw new iPemException("未找到FSU");
                var ext = _fsuService.GetExtFsu(current.Current.Id);
                if (ext == null) throw new iPemException("未找到FSU");
                if (!ext.Status) throw new iPemException("FSU通信中断");

                var helper = new FtpHelper(ext.IP, string.IsNullOrWhiteSpace(current.Current.FtpFilePath) ? "logs" : string.Format("{0}/logs", current.Current.FtpFilePath), current.Current.FtpUid ?? "", current.Current.FtpPwd ?? "");
                using (var ms = helper.Download(path)) {
                    return File(ms.ToArray(), "application/octet-stream", path);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestFsuEvents(int start, int limit, string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            var data = new AjaxDataModel<List<FsuEventModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FsuEventModel>()
            };

            try {
                var models = this.GetFsuEvents(parent, types, startDate, endDate, filter, keywords);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadFsuEvents(string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            try {
                var models = this.GetFsuEvents(parent, types, startDate, endDate, filter, keywords);
                using(var ms = _excelManager.Export<FsuEventModel>(models, "FSU日志信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Reboot(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");

                var curFsu = _workContext.Fsus().Find(f => f.Current.Id == id);
                if (curFsu == null) throw new iPemException("未找到Fsu");

                var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                if (curExtFsu == null) throw new iPemException("未找到Fsu");
                if (!curExtFsu.Status) throw new iPemException("Fsu通信中断");

                var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                if (curGroup == null) throw new iPemException("未找到SC采集组");
                if (!curGroup.Status) throw new iPemException("SC通信中断");

                var package = new SetFsuRebootPackage() { FsuId = curFsu.Current.Code };
                var result = _packMgr.SetFsuReboot(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                if (result == null) throw new iPemException("响应超时");
                if (result.Result == EnmBIResult.FAILURE) throw new iPemException(result.FailureCause ?? "重启失败");

                _webLogger.Information(EnmEventType.Operating, string.Format("FSU重启成功[{0}]", curFsu.Current.Code), null, _workContext.User().Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "重启成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult UpdateRelation() {
            try {
                _fsuService.UpdateFsus();
                _cacheManager.Remove(GlobalCacheKeys.Rs_FsusRepository);
                _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "D_FSU", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步FSU数据" });
                _webLogger.Information(EnmEventType.Operating, "更新FSU关联信息", null, _workContext.User().Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "更新关联成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestPoints(string parent, int[] types, string[] points, string[] vendors, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<FsuPointModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FsuPointModel>()
            };

            try {
                var models = this.GetFsuPoints(parent, types, points, vendors, cache);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult DownloadPoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                var models = this.GetFsuPoints(parent, types, points, vendors, cache);
                using (var ms = _excelManager.Export<FsuPointModel>(models, "存储规则信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestAlarmPoints(string parent, int[] types, string[] points, string[] vendors, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<FsuAlmPointModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FsuAlmPointModel>()
            };

            try {
                var models = this.GetFsuAlmPoints(parent, types, points, vendors, cache);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadAlarmPoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                var models = this.GetFsuAlmPoints(parent, types, points, vendors, cache);
                using (var ms = _excelManager.Export<FsuAlmPointModel>(models, "告警门限信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestRemotePoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                var models = this.GetFsuPoints(parent, types, points, vendors, cache);
                if (models == null || models.Count == 0) throw new iPemException("未找到信号列表");

                #region 组包
                var packages = new List<GetStorageRulePackage>();
                var groups = models.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new GetStorageRulePackage {
                        FsuId = group.Key,
                        DeviceList = new List<GetStorageRuleDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new GetStorageRuleDevice {
                            Id = devGroup.Key,
                            Signals = devGroup.Select(s => new TSignalMeasurementId { Id = s.pointcode, SignalNumber = s.pointnumber }).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 获取存储规则

                #region 请求数据
                var values = new List<FsuPointModel>();
                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,读取存储规则配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,读取存储规则配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,读取存储规则配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,读取存储规则配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,读取存储规则配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.GetStorageRule(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.Rules != null && device.Rules.Count > 0) {
                                    foreach (var rule in device.Rules) {
                                        values.Add(new FsuPointModel {
                                            fsucode = package.FsuId,
                                            devicecode = device.Id,
                                            pointcode = rule.Id,
                                            pointnumber = rule.SignalNumber,
                                            absolute = rule.AbsoluteVal,
                                            relative = rule.RelativeVal,
                                            interval = rule.StorageInterval,
                                            reftime = rule.StorageRefTime,
                                            remote = true
                                        });
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }
                #endregion

                #region 关联数据
                models = (from model in models
                          join value in values on new { model.fsucode, model.devicecode, model.pointcode, model.pointnumber } equals new { value.fsucode, value.devicecode, value.pointcode, value.pointnumber } into lt
                          from def in lt.DefaultIfEmpty()
                          select new FsuPointModel {
                              index = model.index,
                              fsuid = model.fsuid,
                              fsucode = model.fsucode,
                              fsu = model.fsu,
                              vendor = model.vendor,
                              deviceid = model.deviceid,
                              devicecode = model.devicecode,
                              device = model.device,
                              pointid = model.pointid,
                              pointcode = model.pointcode,
                              pointnumber = model.pointnumber,
                              point = model.point,
                              typeid = model.typeid,
                              type = model.type,
                              absolute = def != null ? def.absolute : "",
                              relative = def != null ? def.relative : "",
                              interval = def != null ? def.interval : "",
                              reftime = def != null ? def.reftime : "",
                              remote = def != null ? def.remote : false
                          }).ToList();
                #endregion

                #endregion

                var key = string.Format(GlobalCacheKeys.Fsu_Points, _workContext.Identifier());
                if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);
                _cacheManager.Set(key, models, GlobalCacheInterval.SiteResult_Interval);
                return Json(new AjaxResultModel { success = true, code = 200, message = "配置读取完成" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult SetRemotePoints(List<FsuPointModel> settings) {
            try {
                if (settings == null || settings.Count == 0) throw new iPemException("未找到配置信息");

                #region 组包
                var packages = new List<SetStorageRulePackage>();
                var groups = settings.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new SetStorageRulePackage {
                        FsuId = group.Key,
                        DeviceList = new List<SetStorageRuleDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new SetStorageRuleDevice {
                            Id = devGroup.Key,
                            Rules = devGroup.Select(s => new TStorageRule {
                                Id = s.pointcode,
                                SignalNumber = s.pointnumber,
                                Type = Enum.IsDefined(typeof(EnmBIPoint), s.typeid) ? (EnmBIPoint)s.typeid : EnmBIPoint.AI,
                                AbsoluteVal = s.absolute ?? "",
                                RelativeVal = s.relative ?? "",
                                StorageInterval = s.interval ?? "",
                                StorageRefTime = s.reftime ?? ""
                            }).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 下发存储规则

                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,下发存储规则配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,下发存储规则配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,下发存储规则配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,下发存储规则配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,下发存储规则配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.SetStorageRule(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.SuccessList != null && device.SuccessList.Count > 0) {
                                    foreach (var s in device.SuccessList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发存储规则配置成功[{0},{1},{2}{3}]", package.FsuId, device.Id, s.Id, s.SignalNumber), null, _workContext.User().Id);
                                    }
                                }

                                if (device.FailList != null && device.FailList.Count > 0) {
                                    foreach (var f in device.FailList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发存储规则配置失败[{0},{1},{2}{3}]", package.FsuId, device.Id, f.Id, f.SignalNumber), null, _workContext.User().Id);
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }

                #endregion

                return Json(new AjaxResultModel { success = true, code = 200, message = "配置下发完成，请重新读取配置信息。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult SetRemoteAllPoints(TStorageRule rule, string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                if (rule == null) throw new iPemException("未获得存储规则配置参数");
                var settings = this.GetFsuPoints(parent, types, points, vendors, cache);
                if (settings == null || settings.Count == 0) throw new iPemException("未找到信号列表");

                #region 组包
                var packages = new List<SetStorageRulePackage>();
                var groups = settings.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new SetStorageRulePackage {
                        FsuId = group.Key,
                        DeviceList = new List<SetStorageRuleDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new SetStorageRuleDevice {
                            Id = devGroup.Key,
                            Rules = devGroup.Select(s => new TStorageRule {
                                Id = s.pointcode,
                                SignalNumber = s.pointnumber,
                                Type = Enum.IsDefined(typeof(EnmBIPoint), s.typeid) ? (EnmBIPoint)s.typeid : EnmBIPoint.AI,
                                AbsoluteVal = rule.AbsoluteVal ?? "",
                                RelativeVal = rule.RelativeVal ?? "",
                                StorageInterval = rule.StorageInterval ?? "",
                                StorageRefTime = rule.StorageRefTime ?? ""
                            }).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 下发存储规则

                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,下发存储规则配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,下发存储规则配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,下发存储规则配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,下发存储规则配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,下发存储规则配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.SetStorageRule(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.SuccessList != null && device.SuccessList.Count > 0) {
                                    foreach (var s in device.SuccessList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发存储规则配置成功[{0},{1},{2}{3}]", package.FsuId, device.Id, s.Id, s.SignalNumber), null, _workContext.User().Id);
                                    }
                                }

                                if (device.FailList != null && device.FailList.Count > 0) {
                                    foreach (var f in device.FailList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发存储规则配置失败[{0},{1},{2}{3}]", package.FsuId, device.Id, f.Id, f.SignalNumber), null, _workContext.User().Id);
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }

                #endregion

                return Json(new AjaxResultModel { success = true, code = 200, message = "配置下发完成，请重新读取配置信息。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestRemoteAlmPoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                var models = this.GetFsuAlmPoints(parent, types, points, vendors, cache);
                if (models == null || models.Count == 0) throw new iPemException("未找到信号列表");

                #region 组包
                var packages = new List<GetThresholdPackage>();
                var groups = models.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new GetThresholdPackage {
                        FsuId = group.Key,
                        DeviceList = new List<GetThresholdDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new GetThresholdDevice {
                            Id = devGroup.Key,
                            Ids = devGroup.Select(s => s.pointcode).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 获取告警门限

                #region 请求数据
                var values = new List<FsuAlmPointModel>();
                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,读取告警门限配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,读取告警门限配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,读取告警门限配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,读取告警门限配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,读取告警门限配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.GetThreshold(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.Values != null && device.Values.Count > 0) {
                                    foreach (var val in device.Values) {
                                        values.Add(new FsuAlmPointModel {
                                            fsucode = package.FsuId,
                                            devicecode = device.Id,
                                            pointcode = val.Id,
                                            pointnumber = val.SignalNumber,
                                            threshold = val.Threshold,
                                            level = ((int)val.AlarmLevel).ToString(),
                                            nmid = val.NMAlarmID,
                                            remote = true
                                        });
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }
                #endregion

                #region 关联数据
                models = (from model in models
                          join value in values on new { model.fsucode, model.devicecode, model.pointcode, model.pointnumber } equals new { value.fsucode, value.devicecode, value.pointcode, value.pointnumber } into lt
                          from def in lt.DefaultIfEmpty()
                          select new FsuAlmPointModel {
                              index = model.index,
                              fsuid = model.fsuid,
                              fsucode = model.fsucode,
                              fsu = model.fsu,
                              vendor = model.vendor,
                              deviceid = model.deviceid,
                              devicecode = model.devicecode,
                              device = model.device,
                              pointid = model.pointid,
                              pointcode = model.pointcode,
                              pointnumber = model.pointnumber,
                              point = model.point,
                              typeid = model.typeid,
                              type = model.type,
                              threshold = def != null ? def.threshold : "",
                              level = def != null ? def.level : "",
                              nmid = def != null ? def.nmid : "",
                              remote = def != null ? def.remote : false
                          }).ToList();
                #endregion

                #endregion

                var key = string.Format(GlobalCacheKeys.Fsu_Alarm_Points, _workContext.Identifier());
                if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);
                _cacheManager.Set(key, models, GlobalCacheInterval.SiteResult_Interval);
                return Json(new AjaxResultModel { success = true, code = 200, message = "配置读取完成" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult SetRemoteAlmPoints(List<FsuAlmPointModel> settings) {
            try {
                if (settings == null || settings.Count == 0) throw new iPemException("未找到告警门限配置信息");

                #region 组包
                var packages = new List<SetThresholdPackage>();
                var groups = settings.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new SetThresholdPackage {
                        FsuId = group.Key,
                        DeviceList = new List<SetThresholdDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new SetThresholdDevice {
                            Id = devGroup.Key,
                            Values = devGroup.Select(s => new TThreshold {
                                Id = s.pointcode,
                                SignalNumber = s.pointnumber,
                                Type = EnmBIPoint.AL,
                                Threshold = s.threshold ?? "",
                                AlarmLevel = this.ParseBILevel(s.level),
                                NMAlarmID = s.nmid ?? ""
                            }).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 下发告警门限

                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,下发告警门限配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,下发告警门限配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,下发告警门限配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,下发告警门限配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,下发告警门限配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.SetThreshold(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.SuccessList != null && device.SuccessList.Count > 0) {
                                    foreach (var s in device.SuccessList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发告警门限配置成功[{0},{1},{2}{3}]", package.FsuId, device.Id, s.Id, s.SignalNumber), null, _workContext.User().Id);
                                    }
                                }

                                if (device.FailList != null && device.FailList.Count > 0) {
                                    foreach (var f in device.FailList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发告警门限配置失败[{0},{1},{2}{3}]", package.FsuId, device.Id, f.Id, f.SignalNumber), null, _workContext.User().Id);
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }

                #endregion

                return Json(new AjaxResultModel { success = true, code = 200, message = "配置下发完成，请重新读取配置信息。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult SetRemoteAllAlmPoints(TThreshold tthreshold, string parent, int[] types, string[] points, string[] vendors, bool cache) {
            try {
                if (tthreshold == null) throw new iPemException("未获得告警门限配置参数");
                var settings = this.GetFsuAlmPoints(parent, types, points, vendors, cache);
                if (settings == null || settings.Count == 0) throw new iPemException("未找到信号列表");

                #region 组包
                var packages = new List<SetThresholdPackage>();
                var groups = settings.GroupBy(m => m.fsucode);
                foreach (var group in groups) {
                    var package = new SetThresholdPackage {
                        FsuId = group.Key,
                        DeviceList = new List<SetThresholdDevice>()
                    };

                    var devGroups = group.GroupBy(d => d.devicecode);
                    foreach (var devGroup in devGroups) {
                        package.DeviceList.Add(new SetThresholdDevice {
                            Id = devGroup.Key,
                            Values = devGroup.Select(s => new TThreshold {
                                Id = s.pointcode,
                                SignalNumber = s.pointnumber,
                                Type = EnmBIPoint.AL,
                                Threshold = tthreshold.Threshold ?? "",
                                AlarmLevel = tthreshold.AlarmLevel,
                                NMAlarmID = tthreshold.NMAlarmID ?? ""
                            }).ToList()
                        });
                    }

                    packages.Add(package);
                }
                #endregion

                #region 下发告警门限

                foreach (var package in packages) {
                    try {
                        var curFsu = _workContext.Fsus().Find(f => f.Current.Code == package.FsuId);
                        if (curFsu == null) throw new iPemException(string.Format("未找到Fsu,下发告警门限配置失败({0})", package.FsuId));
                        var curExtFsu = _fsuService.GetExtFsu(curFsu.Current.Id);
                        if (curExtFsu == null) throw new iPemException(string.Format("未找到Fsu,下发告警门限配置失败({0})", package.FsuId));
                        if (!curExtFsu.Status) throw new iPemException(string.Format("Fsu通信中断,下发告警门限配置失败({0})", package.FsuId));

                        var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                        if (curGroup == null) throw new iPemException(string.Format("未找到SC采集组,下发告警门限配置失败({0})", curExtFsu.GroupId));
                        if (!curGroup.Status) throw new iPemException(string.Format("SC通信中断,下发告警门限配置失败({0})", curExtFsu.GroupId));

                        var result = _packMgr.SetThreshold(new UriBuilder("http", curGroup.IP, curGroup.Port, (_workContext.WsValues() != null && _workContext.WsValues().fsuPath != null) ? _workContext.WsValues().fsuPath : "").ToString(), package);
                        if (result != null && result.Result == EnmBIResult.SUCCESS && result.DeviceList != null) {
                            foreach (var device in result.DeviceList) {
                                if (device.SuccessList != null && device.SuccessList.Count > 0) {
                                    foreach (var s in device.SuccessList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发告警门限配置成功[{0},{1},{2}{3}]", package.FsuId, device.Id, s.Id, s.SignalNumber), null, _workContext.User().Id);
                                    }
                                }

                                if (device.FailList != null && device.FailList.Count > 0) {
                                    foreach (var f in device.FailList) {
                                        _webLogger.Information(EnmEventType.Operating, string.Format("下发告警门限配置失败[{0},{1},{2}{3}]", package.FsuId, device.Id, f.Id, f.SignalNumber), null, _workContext.User().Id);
                                    }
                                }
                            }
                        }
                    } catch (Exception exc) {
                        _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                    }
                }

                #endregion

                return Json(new AjaxResultModel { success = true, code = 200, message = "配置下发完成，请重新读取配置信息。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestParamDiff(string parent, string[] points, string date, string keywords, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<DiffModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<DiffModel>()
            };

            try {
                var models = this.GetParamDiff(parent, points, date, keywords, cache);
                if (models != null && models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.Count;

                    var end = start + limit;
                    if (end > models.Count)
                        end = models.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(models[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadParamDiff(string parent, string[] points, string date, string keywords, bool cache) {
            try {
                var models = this.GetParamDiff(parent, points, date, keywords, cache);
                using (var ms = _excelManager.Export<DiffModel>(models, string.Format("{0}参数巡检信息", date ?? DateTime.Today.ToString("yyyyMM")), string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User().Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<FsuModel> GetFsus(string parent, int[] status, string[] vendors, int filter, string keywords) {
            var result = new List<FsuModel>();
            var fsus = new List<D_Fsu>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus.AddRange(_workContext.Fsus().Select(f=>f.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) fsus.AddRange(_workContext.Fsus().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(f => f.Current));
                    } else if (nodeType == EnmSSH.Station) {
                        fsus.AddRange(_workContext.Fsus().FindAll(f => f.Current.StationId == id).Select(f => f.Current));
                    } else if (nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms().Find(a => a.Current.Id == id);
                        if (current != null) fsus = current.Fsus;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(keywords)) {
                var conditions = Common.SplitCondition(keywords);
                switch (filter) {
                    case 1:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Code, conditions));
                        break;
                    case 2:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Name, conditions));
                        break;
                    default:
                        break;
                }
            }

            if (vendors != null && vendors.Length > 0)
                fsus = fsus.FindAll(f => vendors.Contains(f.VendorId));

            var extFsus = _fsuService.GetExtFsus();
            if (status != null && status.Length > 0)
                extFsus = extFsus.FindAll(e => (status.Contains(1) && e.Status) || (status.Contains(0) && !e.Status));

            var stores = from fsu in fsus
                         join ext in extFsus on fsu.Id equals ext.Id
                         join area in _workContext.Areas() on fsu.AreaId equals area.Current.Id
                         select new FsuModel {
                             id = fsu.Id,
                             code = fsu.Code,
                             name = fsu.Name,
                             area = area.ToString(),
                             station = fsu.StationName,
                             room = fsu.RoomName,
                             vendor = fsu.VendorName,
                             ip = ext.IP ?? string.Empty,
                             port = ext.Port,
                             last = CommonHelper.DateTimeConverter(ext.LastTime),
                             change = CommonHelper.DateTimeConverter(ext.ChangeTime),
                             status = ext.Status ? "在线" : "离线",
                             comment = ext.Comment ?? string.Empty
                         };

            var index = 0;
            foreach (var store in stores.OrderBy(s => s.code)) {
                result.Add(new FsuModel {
                    index = ++index,
                    id = store.id,
                    code = store.code,
                    name = store.name,
                    area = store.area,
                    station = store.station,
                    room = store.room,
                    vendor = store.vendor,
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

        private List<FtpFileModel> GetFtpFiles(string id) {
            var result = new List<FtpFileModel>();
            var current = _workContext.Fsus().Find(f=>f.Current.Id == id);
            if (current == null) return result;
            var ext = _fsuService.GetExtFsu(id);
            if (ext == null) return result;
            if (!ext.Status) return result;

            var key = string.Format(GlobalCacheKeys.Fsu_Ftp_Files, current.Current.Id);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<FtpFileModel>>(key);

            var helper = new FtpHelper(ext.IP, string.IsNullOrWhiteSpace(current.Current.FtpFilePath) ? "logs" : string.Format("{0}/logs", current.Current.FtpFilePath), current.Current.FtpUid ?? "", current.Current.FtpPwd ?? "");
            var files = helper.GetFtpFiles().OrderByDescending(f => f.Name);
            var index = 0;
            foreach (var file in files) {
                result.Add(new FtpFileModel {
                    index = ++index,
                    name = file.Name,
                    size = file.Size,
                    path = file.Path,
                    time = CommonHelper.DateTimeConverter(file.CreatedTime)
                });
            }

            if(result.Count > 0) _cacheManager.Set(key, result, GlobalCacheInterval.SiteResult_Interval);
            return result;
        }

        private List<FsuEventModel> GetFsuEvents(string parent, int[] types, DateTime startDate, DateTime endDate, int filter, string keywords) {
            endDate = endDate.AddDays(1).AddMilliseconds(-1);

            var result = new List<FsuEventModel>();
            var fsus = new List<D_Fsu>();
            if (string.IsNullOrWhiteSpace(parent) || parent == "root") {
                fsus.AddRange(_workContext.Fsus().Select(f => f.Current));
            } else {
                var keys = Common.SplitKeys(parent);
                if (keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                    if (nodeType == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id == id);
                        if (current != null) fsus.AddRange(_workContext.Fsus().FindAll(s => current.Keys.Contains(s.Current.AreaId)).Select(f => f.Current));
                    } else if (nodeType == EnmSSH.Station) {
                        fsus.AddRange(_workContext.Fsus().FindAll(f => f.Current.StationId == id).Select(f => f.Current));
                    } else if (nodeType == EnmSSH.Room) {
                        var current = _workContext.Rooms().Find(a => a.Current.Id == id);
                        if (current != null) fsus = current.Fsus;
                    }
                }
            }

            if(!string.IsNullOrWhiteSpace(keywords)) {
                var conditions = Common.SplitCondition(keywords);
                switch(filter) {
                    case 1:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Code, conditions));
                        break;
                    case 2:
                        fsus = fsus.FindAll(f => CommonHelper.ConditionContain(f.Name, conditions));
                        break;
                    default:
                        break;
                }
            }

            var events = _ftpService.GetEvents(startDate, endDate);
            if (types != null && types.Length > 0) events = events.FindAll(e => types.Contains((int)e.EventType));

            var stores = from evt in events
                         join fsu in fsus on evt.FsuId equals fsu.Id
                         join room in _workContext.Rooms() on fsu.RoomId equals room.Current.Id
                         join station in _workContext.Stations() on fsu.StationId equals station.Current.Id
                         join area in _workContext.Areas() on fsu.AreaId equals area.Current.Id
                         select new FsuEventModel {
                             id = fsu.Id,
                             code = fsu.Code,
                             name = fsu.Name,
                             vendor = fsu.VendorName,
                             area = area.ToString(),
                             station = station.Current.Name,
                             room = room.Current.Name,
                             type = Common.GetFsuEventDisplay(evt.EventType),
                             message = evt.Message ?? string.Empty,
                             time = CommonHelper.DateTimeConverter(evt.EventTime)
                         };

            var index = 0;
            foreach (var store in stores.OrderByDescending(s => s.time)) {
                result.Add(new FsuEventModel {
                    index = ++index,
                    id = store.id,
                    code = store.code,
                    name = store.name,
                    vendor = store.vendor,
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

        private List<FsuPointModel> GetFsuPoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            var key = string.Format(GlobalCacheKeys.Fsu_Points, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<FsuPointModel>>(key);

            var result = new List<FsuPointModel>();
            if((string.IsNullOrWhiteSpace(parent) || parent == "root") && ((points == null || points.Length == 0)))
                return result;

            var devices = _workContext.Devices();
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if (keys.Length != 2) return result;
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                if ((points == null || points.Length == 0) && nodeType != EnmSSH.Device) return result;

                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id == id);
                    if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                } else if (nodeType == EnmSSH.Station) {
                    devices = devices.FindAll(d => d.Current.StationId == id);
                } else if (nodeType == EnmSSH.Room) {
                    devices = devices.FindAll(d => d.Current.RoomId == id);
                } else if (nodeType == EnmSSH.Device) {
                    devices = devices.FindAll(d => d.Current.Id == id);
                }
            }

            var fsus = _workContext.Fsus();
            if (vendors != null && vendors.Length > 0) fsus = fsus.FindAll(f => vendors.Contains(f.Current.VendorId));

            var deviceWfsu = from device in devices
                             join fsu in fsus on device.Current.FsuId equals fsu.Current.Id
                             select new { Fsu = fsu.Current, Device = device };

            var index = 0;
            foreach (var dwf in deviceWfsu) {
                foreach (var point in dwf.Device.Points) {
                    if (points != null && points.Length > 0 && !points.Contains(point.Id)) continue;
                    if (point.Type != EnmPoint.AI && point.Type != EnmPoint.DI) continue;

                    var type = _workContext.GetPointType(point);
                    if (types != null && types.Length > 0 && !types.Contains((int)type)) continue;

                    result.Add(new FsuPointModel {
                        index = ++index,
                        fsuid = dwf.Fsu.Id,
                        fsucode = dwf.Fsu.Code,
                        fsu = dwf.Fsu.Name,
                        vendor = dwf.Fsu.VendorName,
                        deviceid = dwf.Device.Current.Id,
                        devicecode = dwf.Device.Current.Code,
                        device = dwf.Device.Current.Name,
                        pointid = point.Id,
                        pointcode = point.Code,
                        pointnumber = point.Number,
                        point = point.Name,
                        typeid = (int)type,
                        type = Common.GetPointTypeDisplay(type),
                        remote = false
                    });
                }
            }

            _cacheManager.Set(key, result, GlobalCacheInterval.SiteResult_Interval);
            return result;
        }

        private List<FsuAlmPointModel> GetFsuAlmPoints(string parent, int[] types, string[] points, string[] vendors, bool cache) {
            var key = string.Format(GlobalCacheKeys.Fsu_Alarm_Points, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<FsuAlmPointModel>>(key);

            var result = new List<FsuAlmPointModel>();
            if ((string.IsNullOrWhiteSpace(parent) || parent == "root") && ((points == null || points.Length == 0)))
                return result;

            var devices = _workContext.Devices();
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if (keys.Length != 2) return result;
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                if ((points == null || points.Length == 0) && nodeType != EnmSSH.Device) return result;

                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id == id);
                    if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                } else if (nodeType == EnmSSH.Station) {
                    devices = devices.FindAll(d => d.Current.StationId == id);
                } else if (nodeType == EnmSSH.Room) {
                    devices = devices.FindAll(d => d.Current.RoomId == id);
                } else if (nodeType == EnmSSH.Device) {
                    devices = devices.FindAll(d => d.Current.Id == id);
                }
            }

            var fsus = _workContext.Fsus();
            if (vendors != null && vendors.Length > 0) fsus = fsus.FindAll(f => vendors.Contains(f.Current.VendorId));

            var deviceWfsu = from device in devices
                             join fsu in fsus on device.Current.FsuId equals fsu.Current.Id
                             select new { Fsu = fsu.Current, Device = device };

            var index = 0;
            foreach (var dwf in deviceWfsu) {
                foreach (var point in dwf.Device.Points) {
                    if (points != null && points.Length > 0 && !points.Contains(point.Id)) continue;
                    var type = _workContext.GetPointType(point);
                    if (type != EnmPoint.AL) continue;

                    result.Add(new FsuAlmPointModel {
                        index = ++index,
                        fsuid = dwf.Fsu.Id,
                        fsucode = dwf.Fsu.Code,
                        fsu = dwf.Fsu.Name,
                        vendor = dwf.Fsu.VendorName,
                        deviceid = dwf.Device.Current.Id,
                        devicecode = dwf.Device.Current.Code,
                        device = dwf.Device.Current.Name,
                        pointid = point.Id,
                        pointcode = point.Code,
                        pointnumber = point.Number,
                        point = point.Name,
                        typeid = (int)EnmPoint.AL,
                        type = Common.GetPointTypeDisplay(EnmPoint.AL),
                        remote = false
                    });
                }
            }

            _cacheManager.Set(key, result, GlobalCacheInterval.SiteResult_Interval);
            return result;
        }

        private List<DiffModel> GetParamDiff(string parent, string[] points, string date, string keywords, bool cache) {
            var key = string.Format(GlobalCacheKeys.Fsu_Param_Diff, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<DiffModel>>(key);

            if (string.IsNullOrWhiteSpace(date)) date = DateTime.Today.ToString("yyyyMM");
            var curDate = DateTime.ParseExact(date, "yyyyMM", CultureInfo.CurrentCulture);

            var result = new List<DiffModel>();
            var devices = _workContext.Devices();
            if (!string.IsNullOrWhiteSpace(parent) && parent != "root") {
                var keys = Common.SplitKeys(parent);
                if (keys.Length != 2) return result;
                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                if ((points == null || points.Length == 0) && nodeType != EnmSSH.Device) return result;

                if (nodeType == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id == id);
                    if (current != null) devices = devices.FindAll(d => current.Keys.Contains(d.Current.AreaId));
                } else if (nodeType == EnmSSH.Station) {
                    devices = devices.FindAll(d => d.Current.StationId == id);
                } else if (nodeType == EnmSSH.Room) {
                    devices = devices.FindAll(d => d.Current.RoomId == id);
                } else if (nodeType == EnmSSH.Device) {
                    devices = devices.FindAll(d => d.Current.Id == id);
                }
            }

            var diffs = _diffService.GetDiffs(curDate);
            if (points != null && points.Length > 0)
                diffs = diffs.FindAll(d => points.Contains(d.PointId));

            var allpoints = _workContext.Points();
            if (!string.IsNullOrWhiteSpace(keywords)) {
                var conditions = Common.SplitCondition(keywords);
                allpoints = allpoints.FindAll(d => CommonHelper.ConditionContain(d.Name, conditions));
            }

            var full = from diff in diffs
                       join point in allpoints on diff.PointId equals point.Id
                       join device in devices on diff.DeviceId equals device.Current.Id
                       join area in _workContext.Areas() on device.Current.AreaId equals area.Current.Id
                       select new DiffModel {
                           area = area.ToString(),
                           station = device.Current.StationName,
                           room = device.Current.RoomName,
                           device = device.Current.Name,
                           point = point.Name,
                           threshold = diff.Threshold,
                           level = diff.AlarmLevel,
                           nmid = diff.NMAlarmID,
                           absolute = diff.AbsoluteVal,
                           relative = diff.RelativeVal,
                           interval = diff.StorageInterval,
                           reftime = diff.StorageRefTime,
                           masked = diff.Masked ? "是" : "否"
                       };

            var index =0;
            foreach (var f in full) {
                f.index = ++index;
                result.Add(f);
            }

            _cacheManager.Set(key, result, GlobalCacheInterval.SiteResult_Interval);
            return result;
        }

        private EnmBILevel ParseBILevel(string level) {
            if (string.IsNullOrWhiteSpace(level)) return EnmBILevel.HINT;
            var val = int.Parse(level);
            return Enum.IsDefined(typeof(EnmBILevel), val) ? (EnmBILevel)val : EnmBILevel.HINT;
        }

        #endregion

    }
}