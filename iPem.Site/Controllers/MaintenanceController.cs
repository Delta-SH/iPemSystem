using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class MaintenanceController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IAAlarmService _aalarmService;
        private readonly ITAlarmService _talarmService;
        private readonly INoteService _noteService;
        private readonly ISignalService _signalService;
        private readonly IFormulaService _formulaService;

        #endregion

        #region Ctor

        public MaintenanceController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IAAlarmService aalarmService,
            ITAlarmService talarmService,
            INoteService noteService,
            ISignalService signalService,
            IFormulaService formulaService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._aalarmService = aalarmService;
            this._talarmService = talarmService;
            this._noteService = noteService;
            this._signalService = signalService;
            this._formulaService = formulaService;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            if (!_workContext.Authorizations().Menus.Contains(2005))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult Masking() {
            if (!_workContext.Authorizations().Menus.Contains(2008))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult PointParam() {
            if (!_workContext.Authorizations().Menus.Contains(2009))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult EneFormula() {
            if (!_workContext.Authorizations().Menus.Contains(2010))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult VirtualPoint() {
            if (!_workContext.Authorizations().Menus.Contains(2011))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        [AjaxAuthorize]
        public JsonResult RequestRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<RedundantAlmModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<RedundantAlmModel>()
            };

            try {
                var stores = this.GetRedundantAlarms(parent, startDate, endDate, levels, type, keyWord, cache);
                if (stores != null && stores.Count > 0) {
                    data.message = "200 Ok";
                    data.total = stores.Count;

                    var end = start + limit;
                    if (end > stores.Count)
                        end = stores.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(stores[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache) {
            try {
                var models = this.GetRedundantAlarms(parent, startDate, endDate, levels, type, keyWord, cache);
                using (var ms = _excelManager.Export<RedundantAlmModel>(models, "告警维护管理", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult FinishRedundantAlarms(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _aalarmService.GetAlarm(id);
                if (existed == null) throw new iPemException("告警不存在");

                var fsu = _workContext.Fsus().Find(f => f.Current.Id == existed.FsuId);
                if (fsu == null) throw new iPemException("未找到FSU");

                var device = _workContext.Devices().Find(d => d.Current.Id == existed.DeviceId);
                if (device == null) throw new iPemException("未找到设备");

                var point = _workContext.AL().Find(p => p.Id == existed.PointId);
                if (point == null) throw new iPemException("未找到信号");

                _talarmService.Add(new A_TAlarm {
                    FsuId = fsu.Current.Code,
                    DeviceId = device.Current.Code,
                    PointId = point.Id,
                    SignalId = point.Code,
                    SignalNumber = point.Number,
                    SerialNo = existed.SerialNo,
                    NMAlarmId = existed.NMAlarmId,
                    AlarmTime = DateTime.Now,
                    AlarmLevel = existed.AlarmLevel,
                    AlarmValue = 0,
                    AlarmFlag = EnmFlag.End,
                    AlarmDesc = existed.AlarmDesc,
                    AlarmRemark = "人工结束告警"
                });

                _webLogger.Information(EnmEventType.Other, string.Format("下发结束告警命令[{0}]", JsonConvert.SerializeObject(existed)), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警结束命令已下发,请稍后查询。" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteRedundantAlarms(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _aalarmService.GetAlarm(id);
                if (existed == null) throw new iPemException("告警不存在");

                _aalarmService.RemoveAlarms(existed);

                var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
                if (_cacheManager.IsSet(key)) _cacheManager.Remove(key);

                _webLogger.Information(EnmEventType.Other, string.Format("人工删除告警[{0}]", JsonConvert.SerializeObject(existed)), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "告警删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<MaskingModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<MaskingModel>()
            };

            try {
                var models = this.GetMaskings(parent, types, startDate, endDate, cache);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache) {
            try {
                var models = this.GetMaskings(parent, types, startDate, endDate, cache);
                using (var ms = _excelManager.Export<MaskingModel>(models, "告警屏蔽信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult RequestPointParams(string parent, int pmtype, string[] points, int[] types, bool cache, int start, int limit) {
            var data = new AjaxDataModel<List<PointParam>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<PointParam>()
            };

            try {
                var models = this.GetPointParams(parent, pmtype, points, types, cache);
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
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownloadPointParams(string parent, int pmtype, string[] points, int[] types, bool cache) {
            try {
                var models = this.GetPointParams(parent, pmtype, points, types, cache);
                using (var ms = _excelManager.Export<PointParam>(models, "信号参数信息", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFormula(string node) {
            var data = new AjaxDataModel<FormulaModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new FormulaModel()
            };

            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");

                var nodeKey = Common.ParseNode(node);
                if (nodeKey.Key == EnmSSH.Station || nodeKey.Key == EnmSSH.Room || nodeKey.Key == EnmSSH.Device) {
                    var formulas = _formulaService.GetFormulas(nodeKey.Value, nodeKey.Key);
                    foreach (var formula in formulas) {
                        switch (formula.FormulaType) {
                            case EnmFormula.KT:
                                data.data.ktFormulas = formula.FormulaText;
                                data.data.ktCompute = (int)formula.ComputeType;
                                data.data.ktRemarks = formula.Comment;
                                break;
                            case EnmFormula.ZM:
                                data.data.zmFormulas = formula.FormulaText;
                                data.data.zmCompute = (int)formula.ComputeType;
                                data.data.zmRemarks = formula.Comment;
                                break;
                            case EnmFormula.BG:
                                data.data.bgFormulas = formula.FormulaText;
                                data.data.bgCompute = (int)formula.ComputeType;
                                data.data.bgRemarks = formula.Comment;
                                break;
                            case EnmFormula.DY:
                                data.data.dyFormulas = formula.FormulaText;
                                data.data.dyCompute = (int)formula.ComputeType;
                                data.data.dyRemarks = formula.Comment;
                                break;
                            case EnmFormula.UPS:
                                data.data.upsFormulas = formula.FormulaText;
                                data.data.upsCompute = (int)formula.ComputeType;
                                data.data.upsRemarks = formula.Comment;
                                break;
                            case EnmFormula.IT:
                                data.data.itFormulas = formula.FormulaText;
                                data.data.itCompute = (int)formula.ComputeType;
                                data.data.itRemarks = formula.Comment;
                                break;
                            case EnmFormula.QT:
                                data.data.qtFormulas = formula.FormulaText;
                                data.data.qtCompute = (int)formula.ComputeType;
                                data.data.qtRemarks = formula.Comment;
                                break;
                            case EnmFormula.TT:
                                data.data.ttFormulas = formula.FormulaText;
                                data.data.ttCompute = (int)formula.ComputeType;
                                data.data.ttRemarks = formula.Comment;
                                break;
                            case EnmFormula.TD:
                                data.data.tdFormulas = formula.FormulaText;
                                data.data.tdRemarks = formula.Comment;
                                break;
                            case EnmFormula.WD:
                                data.data.wdFormulas = formula.FormulaText;
                                data.data.wdRemarks = formula.Comment;
                                break;
                            case EnmFormula.SD:
                                data.data.sdFormulas = formula.FormulaText;
                                break;
                            case EnmFormula.FD:
                                data.data.fdFormulas = formula.FormulaText;
                                break;
                            case EnmFormula.FDL:
                                data.data.yjFormulas = formula.FormulaText;
                                data.data.yjCompute = (int)formula.ComputeType;
                                data.data.yjRemarks = formula.Comment;
                                break;
                            case EnmFormula.BY:
                                data.data.byFormulas = formula.FormulaText;
                                data.data.byCompute = (int)formula.ComputeType;
                                data.data.byRemarks = formula.Comment;
                                break;
                            case EnmFormula.XS:
                                data.data.xsFormulas = formula.FormulaText;
                                data.data.xsCompute = (int)formula.ComputeType;
                                break;
                            default:
                                break;
                        }
                    }
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveFormula(string node, FormulaModel model) {
            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");
                if (model == null)
                    throw new ArgumentNullException("model");

                var nodeKey = Common.ParseNode(node);
                var target = nodeKey.Value;
                if (nodeKey.Key == EnmSSH.Station) {
                    var station = _workContext.Stations().Find(r => r.Current.Id.Equals(nodeKey.Value));
                    if (station == null) throw new iPemException("站点节点未找到");
                    target = station.Current.Id;
                } else if (nodeKey.Key == EnmSSH.Room) {
                    var room = _workContext.Rooms().Find(r => r.Current.Id.Equals(nodeKey.Value));
                    if (room == null) throw new iPemException("机房节点未找到");
                    target = room.Current.StationId;
                } else if (nodeKey.Key == EnmSSH.Device) {
                    var device = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                    if (device == null) throw new iPemException("设备节点未找到");
                    target = device.Current.StationId;
                } else {
                    throw new iPemException("能耗仅支持站点、机房、设备节点。");
                }

                var formulas = new List<M_Formula>();
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.KT, ComputeType = (EnmCompute)model.ktCompute, FormulaText = model.ktFormulas, Comment = model.ktRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.ZM, ComputeType = (EnmCompute)model.zmCompute, FormulaText = model.zmFormulas, Comment = model.zmRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.BG, ComputeType = (EnmCompute)model.bgCompute, FormulaText = model.bgFormulas, Comment = model.bgRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.DY, ComputeType = (EnmCompute)model.dyCompute, FormulaText = model.dyFormulas, Comment = model.dyRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.UPS, ComputeType = (EnmCompute)model.upsCompute, FormulaText = model.upsFormulas, Comment = model.upsRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.IT, ComputeType = (EnmCompute)model.itCompute, FormulaText = model.itFormulas, Comment = model.itRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.QT, ComputeType = (EnmCompute)model.qtCompute, FormulaText = model.qtFormulas, Comment = model.qtRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.TT, ComputeType = (EnmCompute)model.ttCompute, FormulaText = model.ttFormulas, Comment = model.ttRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.TD, ComputeType = EnmCompute.Kwh, FormulaText = model.tdFormulas, Comment = model.tdRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.WD, ComputeType = EnmCompute.Kwh, FormulaText = model.wdFormulas, Comment = model.wdRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.SD, ComputeType = EnmCompute.Kwh, FormulaText = model.sdFormulas, Comment = null });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.FD, ComputeType = EnmCompute.Kwh, FormulaText = model.fdFormulas, Comment = null });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.FDL, ComputeType = (EnmCompute)model.yjCompute, FormulaText = model.yjFormulas, Comment = model.yjRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.BY, ComputeType = (EnmCompute)model.byCompute, FormulaText = model.byFormulas, Comment = model.byRemarks });
                formulas.Add(new M_Formula { Id = nodeKey.Value, Type = nodeKey.Key, FormulaType = EnmFormula.XS, ComputeType = (EnmCompute)model.xsCompute, FormulaText = model.xsFormulas, Comment = null });

                //根据名称公式计算编号公式
                List<SSHDevice> devices = null;
                foreach (var formula in formulas) {
                    var text = formula.FormulaText;
                    if (string.IsNullOrWhiteSpace(text)) continue;
                    if (!Common.ValidateFormula(text))
                        throw new iPemException(string.Format("无效的格式({0})。", text));

                    var variables = Common.GetFormulaVariables(text);
                    foreach (var variable in variables) {
                        var factors = variable.Split(new string[] { ">>" }, StringSplitOptions.None);
                        if (factors.Length != 3) throw new iPemException(string.Format("无效的格式({0})。", variable));

                        var room = factors[0].Substring(1);
                        var dev = factors[1];
                        var point = factors[2];

                        if (devices == null) devices = _workContext.Devices().FindAll(d => d.Current.StationId.Equals(target));
                        var device = devices.Find(d => d.Current.RoomName.Equals(room) && d.Current.Name.Equals(dev));
                        if (device == null) throw new iPemException(string.Format("未找到设备信息({0})。", variable));

                        var signal = device.Signals.Find(s => s.PointName.Equals(point));
                        if (signal == null) throw new iPemException(string.Format("未找到信号信息({0})。", variable));
                        text = text.Replace(variable, string.Format("@{0}>>{1}", device.Current.Id, signal.PointId));
                    }

                    formula.FormulaValue = text;
                }

                _formulaService.Save(formulas.ToArray());
                _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "M_Formulas", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步能耗公式" });
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFormulaDevices(string node, string target, string[] devTypes) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(node)) {
                    var nodeKey = Common.ParseNode(node);
                    if (nodeKey.Key == EnmSSH.Root) {
                        var tarKey = Common.ParseNode(target);
                        if (tarKey.Key == EnmSSH.Station) {
                            var station = _workContext.Stations().Find(r => r.Current.Id.Equals(tarKey.Value));
                            if (station == null) throw new iPemException("站点节点未找到");
                            target = station.Current.Id;
                        } else if (tarKey.Key == EnmSSH.Room) {
                            var room = _workContext.Rooms().Find(r => r.Current.Id.Equals(tarKey.Value));
                            if (room == null) throw new iPemException("机房节点未找到");
                            target = room.Current.StationId;
                        } else if (tarKey.Key == EnmSSH.Device) {
                            var device = _workContext.Devices().Find(d => d.Current.Id.Equals(tarKey.Value));
                            if (device == null) throw new iPemException("设备节点未找到");
                            target = device.Current.StationId;
                        } else {
                            throw new iPemException("能耗仅支持站点、机房、设备节点。");
                        }

                        #region station
                        var rooms = _workContext.Rooms().FindAll(r => r.Current.StationId.Equals(target));
                        if (rooms.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = rooms.Count;
                            for (var i = 0; i < rooms.Count; i++) {
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmSSH.Room, rooms[i].Current.Id),
                                    text = rooms[i].Current.Name,
                                    icon = Icons.Room,
                                    expanded = false,
                                    leaf = false
                                };

                                data.data.Add(root);
                            }
                        }
                        #endregion
                    } else if (nodeKey.Key == EnmSSH.Room) {
                        #region room
                        var devices = _workContext.Devices().FindAll(d => d.Current.RoomId.Equals(nodeKey.Value));
                        if (devices.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = devices.Count;
                            for (var i = 0; i < devices.Count; i++) {
                                if (devTypes != null && devTypes.Length > 0 && !devTypes.Contains(devices[i].Current.Type.Id))
                                    continue;

                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmSSH.Device, devices[i].Current.Id),
                                    text = devices[i].Current.Name,
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
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetFormulaPoints(int start, int limit, string parent, bool ai = true, bool di = false, bool al = false) {
            var data = new AjaxDataModel<List<Kv<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<Kv<int, string>>()
            };

            try {
                if (!string.IsNullOrWhiteSpace(parent)) {
                    var nodeKey = Common.ParseNode(parent);
                    if (nodeKey.Key == EnmSSH.Device) {
                        var current = _workContext.Devices().Find(d => d.Current.Id.Equals(nodeKey.Value));
                        if (current != null) {
                            var signals = _signalService.GetAllSignals(current.Current.Id, ai, false, di, false, al);
                            for (var i = 0; i < signals.Count; i++) {
                                data.data.Add(new Kv<int, string> {
                                    Key = i + 1,
                                    Value = string.Format("@{0}>>{1}>>{2}", current.Current.RoomName, current.Current.Name, signals[i].PointName)
                                });
                            }
                        }
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
        public JsonResult GetVSignal(string device, string point, int action) {
            var data = new AjaxDataModel<VSignalModel> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new VSignalModel { dev = device, typevalue = (int)EnmPoint.AI, saved = 30, stats = 0, category = (int)EnmVSignalCategory.Category01, categoryName = Common.GetVSignalCategoryDisplay(EnmVSignalCategory.Category01) }
            };

            try {
                if (string.IsNullOrWhiteSpace(device))
                    throw new ArgumentNullException("device");

                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(point))
                    throw new ArgumentNullException("point");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var signal = _signalService.GetVSignal(device, point);
                if (signal == null) throw new iPemException("未找到数据对象");

                data.data.dev = signal.DeviceId;
                data.data.id = signal.PointId;
                data.data.name = signal.Name;
                data.data.typevalue = (int)signal.Type;
                data.data.formula = signal.FormulaText;
                data.data.unit = signal.UnitState;
                data.data.saved = signal.SavedPeriod;
                data.data.stats = signal.StaticPeriod;
                data.data.categoryName = Common.GetVSignalCategoryDisplay(signal.Category);
                data.data.category = (int)signal.Category;
                data.data.remark = signal.Remark;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, _workContext.User().Id, exc.Message, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetVSignals(int start, int limit, string node, string name) {
            var data = new AjaxDataModel<List<VSignalModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<VSignalModel>()
            };

            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");

                var nodeKey = Common.ParseNode(node);
                if (nodeKey.Key == EnmSSH.Device) {
                    var names = Common.SplitCondition(name);
                    var signals = _signalService.GetVSignals(nodeKey.Value, names);
                    if (signals.Count > 0) {
                        data.message = "200 Ok";
                        data.total = signals.Count;
                        for (var i = 0; i < signals.Count; i++) {
                            data.data.Add(new VSignalModel {
                                index = start + i + 1,
                                dev = signals[i].DeviceId,
                                id = signals[i].PointId,
                                name = signals[i].Name,
                                type = Common.GetPointTypeDisplay(signals[i].Type),
                                typevalue = (int)signals[i].Type,
                                formula = signals[i].FormulaText,
                                unit = signals[i].UnitState,
                                saved = signals[i].SavedPeriod,
                                stats = signals[i].StaticPeriod,
                                categoryName = Common.GetVSignalCategoryDisplay(signals[i].Category),
                                category = (int)signals[i].Category,
                                remark = signals[i].Remark
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, _workContext.User().Id, exc.Message, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadVSignals(string node, string name) {
            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");

                var models = new List<VSignalModel>();
                var nodeKey = Common.ParseNode(node);
                if (nodeKey.Key == EnmSSH.Device) {
                    var names = Common.SplitCondition(name);
                    var signals = _signalService.GetVSignals(nodeKey.Value, names);
                    if (signals.Count > 0) {
                        for (var i = 0; i < signals.Count; i++) {
                            models.Add(new VSignalModel {
                                index = i + 1,
                                dev = signals[i].DeviceId,
                                id = signals[i].PointId,
                                name = signals[i].Name,
                                type = Common.GetPointTypeDisplay(signals[i].Type),
                                typevalue = (int)signals[i].Type,
                                formula = signals[i].FormulaText,
                                unit = signals[i].UnitState,
                                saved = signals[i].SavedPeriod,
                                stats = signals[i].StaticPeriod,
                                categoryName = Common.GetVSignalCategoryDisplay(signals[i].Category),
                                category = (int)signals[i].Category,
                                remark = signals[i].Remark
                            });
                        }
                    }
                }

                using (var ms = _excelManager.Export<VSignalModel>(models, "虚拟信号列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveVSignal(VSignalModel model, int action) {
            try {
                if (model == null)
                    throw new ArgumentNullException("model");

                if(string.IsNullOrWhiteSpace(model.dev))
                    throw new ArgumentNullException("model.dev");

                if (string.IsNullOrWhiteSpace(model.id))
                    throw new ArgumentNullException("model.id");

                if (string.IsNullOrWhiteSpace(model.name))
                    throw new ArgumentNullException("model.name");

                if (string.IsNullOrWhiteSpace(model.formula))
                    throw new ArgumentNullException("model.formula");

                //根据名称公式计算编号公式
                var text = model.formula;
                var current = _workContext.Devices().Find(d => d.Current.Id.Equals(model.dev));
                if (current == null) throw new iPemException("设备节点未找到");
                var devices = _workContext.Devices().FindAll(d => d.Current.StationId.Equals(current.Current.StationId));
                if (devices.Count == 0) throw new iPemException("设备节点未找到");
                if (!Common.ValidateFormula(text)) throw new iPemException(string.Format("无效的公式格式({0})。", model.formula));
                var variables = Common.GetFormulaVariables(text);
                foreach (var variable in variables) {
                    var factors = variable.Split(new string[] { ">>" }, StringSplitOptions.None);
                    if (factors.Length != 3) throw new iPemException(string.Format("无效的公式格式({0})。", variable));

                    var room = factors[0].Substring(1);
                    var dev = factors[1];
                    var point = factors[2];

                    var device = devices.Find(d => d.Current.RoomName.Equals(room) && d.Current.Name.Equals(dev));
                    if (device == null) throw new iPemException(string.Format("未找到设备信息({0})。", variable));

                    var signal = device.Signals.Find(s => s.PointName.Equals(point));
                    if (signal == null) throw new iPemException(string.Format("未找到信号信息({0})。", variable));
                    text = text.Replace(variable, string.Format("@{0}>>{1}", device.Current.Id, signal.PointId));
                }

                if (action == (int)EnmAction.Add) {
                    var signal = _signalService.GetVSignal(model.dev, model.id);
                    if (signal != null) throw new iPemException("信号编码已存在");

                    var simple = _signalService.GetSimpleSignal(model.dev, model.id);
                    if (simple != null) throw new iPemException("信号编码已被真实信号占用");

                    signal = new D_VSignal {
                        DeviceId = model.dev,
                        PointId = model.id,
                        Name = model.name,
                        Type = (EnmPoint)model.typevalue,
                        FormulaText = model.formula,
                        FormulaValue = text,
                        UnitState = model.unit,
                        SavedPeriod = model.saved,
                        StaticPeriod = model.stats,
                        Category = Enum.IsDefined(typeof(EnmVSignalCategory), model.category) ? (EnmVSignalCategory)model.category : EnmVSignalCategory.Category01,
                        Remark = model.remark
                    };

                    _signalService.AddVSignals(signal);
                    _webLogger.Information(EnmEventType.Other, string.Format("新增虚拟信号[{0},{1},{2}]", signal.DeviceId, signal.PointId, signal.Name), _workContext.User().Id, null);
                    _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "D_VSignal", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步虚拟信号" });
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var signal = _signalService.GetVSignal(model.dev, model.id);
                    if (signal == null) throw new iPemException("信号不存在");

                    //signal.DeviceId = model.dev;
                    //signal.PointId = model.id;
                    signal.Name = model.name;
                    signal.Type = (EnmPoint)model.typevalue;
                    signal.FormulaText = model.formula;
                    signal.FormulaValue = text;
                    signal.UnitState = model.unit;
                    signal.SavedPeriod = model.saved;
                    signal.StaticPeriod = model.stats;
                    signal.Category = Enum.IsDefined(typeof(EnmVSignalCategory), model.category) ? (EnmVSignalCategory)model.category : EnmVSignalCategory.Category01;
                    signal.Remark = model.remark;

                    _signalService.UpdateVSignals(signal);
                    _webLogger.Information(EnmEventType.Other, string.Format("更新虚拟信号[{0},{1},{2}]", signal.DeviceId, signal.PointId, signal.Name), _workContext.User().Id, null);
                    _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "D_VSignal", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步虚拟信号" });
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
        public JsonResult DeleteVSignal(string device, string point) {
            try {
                if (string.IsNullOrWhiteSpace(device))
                    throw new ArgumentNullException("device");

                if (string.IsNullOrWhiteSpace(point))
                    throw new ArgumentNullException("point");

                var signal = _signalService.GetVSignal(device, point);
                if (signal == null) throw new iPemException("信号不存在");

                _signalService.RemoveVSignals(signal);
                _webLogger.Information(EnmEventType.Other, string.Format("删除虚拟信号[{0},{1},{2}]", signal.DeviceId, signal.PointId, signal.Name), _workContext.User().Id, null);
                _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "D_VSignal", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步虚拟信号" });
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        private List<RedundantAlmModel> GetRedundantAlarms(string parent, DateTime startDate, DateTime endDate, int[] levels, int type, string keyWord, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.RedundantAlarmsPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<RedundantAlmModel>(key).ToList();

            var alarms = _aalarmService.GetAllAlarmsInSpan(startDate, endDate).FindAll(a => !Common.IsSystemAlarm(a.FsuId));
            if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) alarms = alarms.FindAll(v => current.Keys.Contains(v.AreaId));
                } else if (nodeKey.Key == EnmSSH.Station) {
                    alarms = alarms.FindAll(v => v.StationId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Room) {
                    alarms = alarms.FindAll(v => v.RoomId.Equals(nodeKey.Value));
                } else if (nodeKey.Key == EnmSSH.Device) {
                    alarms = alarms.FindAll(v => v.DeviceId.Equals(nodeKey.Value));
                }
            }

            if (levels != null && levels.Length > 0)
                alarms = alarms.FindAll(v => levels.Contains((int)v.AlarmLevel));

            var words = Common.SplitCondition(keyWord);
            if (words.Length > 0 && type == 1)
                alarms = alarms.FindAll(v => words.Contains(v.SerialNo));

            var devices = _workContext.Devices();
            var points = _workContext.AL();
            var signals = _signalService.GetSimpleSignals(alarms.Select(a => new Kv<string, string>(a.DeviceId, a.PointId)));
            var stores = from alarm in alarms
                         join signal in signals on new { alarm.DeviceId, alarm.PointId } equals new { signal.DeviceId, signal.PointId }
                         join point in points on alarm.PointId equals point.Id
                         join device in devices on alarm.DeviceId equals device.Current.Id
                         select new AlmStore<A_AAlarm> {
                             Current = alarm,
                             PointName = signal.PointName,
                             AlarmName = point.Name,
                             DeviceName = device.Current.Name,
                             DeviceTypeId = device.Current.Type.Id,
                             SubDeviceTypeId = device.Current.SubType.Id,
                             SubLogicTypeId = device.Current.SubLogicType.Id,
                             RoomName = device.Current.RoomName,
                             RoomTypeId = device.Current.RoomTypeId,
                             StationName = device.Current.StationName,
                             StationTypeId = device.Current.StaTypeId,
                             AreaName = device.Current.AreaName,
                             SubCompany = device.Current.SubCompany ?? "--",
                             SubManager = device.Current.SubManager ?? "--"
                         };

            var index = 0;
            var result = new List<RedundantAlmModel>();
            foreach (var store in stores) {
                if (words.Length > 0 && type == 2 && !CommonHelper.ConditionContain(store.AlarmName, words))
                    continue;

                result.Add(new RedundantAlmModel {
                    id = store.Current.Id,
                    index = ++index,
                    serialno = store.Current.SerialNo,
                    level = Common.GetAlarmDisplay(store.Current.AlarmLevel),
                    time = CommonHelper.DateTimeConverter(store.Current.AlarmTime),
                    name = store.AlarmName,
                    nmalarmid = store.Current.NMAlarmId,
                    interval = CommonHelper.IntervalConverter(store.Current.AlarmTime),
                    point = store.PointName,
                    device = store.DeviceName,
                    room = store.RoomName,
                    station = store.StationName,
                    area = store.AreaName,
                    supporter = store.SubCompany,
                    manager = store.SubManager,
                    confirmed = Common.GetConfirmDisplay(store.Current.Confirmed),
                    confirmer = store.Current.Confirmer,
                    confirmedtime = store.Current.ConfirmedTime.HasValue ? CommonHelper.DateTimeConverter(store.Current.ConfirmedTime.Value) : "",
                    reservation = string.IsNullOrWhiteSpace(store.Current.ReservationId) ? "否" : "是",
                    primary = string.IsNullOrWhiteSpace(store.Current.PrimaryId) ? "否" : "是",
                    related = string.IsNullOrWhiteSpace(store.Current.RelatedId) ? "否" : "是",
                    filter = string.IsNullOrWhiteSpace(store.Current.FilterId) ? "否" : "是",
                    masked = !store.Current.Masked ? "否" : "是",
                    reversal = string.IsNullOrWhiteSpace(store.Current.ReversalId) ? "否" : "是",
                    levelid = (int)store.Current.AlarmLevel,
                    createdtime = CommonHelper.DateTimeConverter(store.Current.CreatedTime),
                    background = Common.GetAlarmColor(store.Current.AlarmLevel)
                });
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<MaskingModel> GetMaskings(string parent, int[] types, DateTime startDate, DateTime endDate, bool cache) {
            endDate = endDate.AddSeconds(86399);
            var key = string.Format(GlobalCacheKeys.MaskingPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_MaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_MaskingRepository);
            if (_cacheManager.IsSet(GlobalCacheKeys.Rs_HashMaskingRepository) && !cache) _cacheManager.Remove(GlobalCacheKeys.Rs_HashMaskingRepository);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<MaskingModel>(key).ToList();

            var maskings = _workContext.Maskings().FindAll(m => m.Time >= startDate && m.Time <= endDate);
            if (types != null && types.Length > 0) {
                maskings = maskings.FindAll(m => types.Contains((int)m.Type));
            }

            var result = new List<MaskingModel>();
            var maskingsInStation = maskings.FindAll(m => m.Type == EnmMaskType.Station);
            var maskingsInRoom = maskings.FindAll(m => m.Type == EnmMaskType.Room);
            var maskingsInDevice = maskings.FindAll(m => m.Type == EnmMaskType.Device);
            var maskingsInPoint = maskings.FindAll(m => m.Type == EnmMaskType.Point);

            if (string.IsNullOrWhiteSpace(parent) || parent.Equals("root")) {
                #region Root
                if (maskingsInStation.Count > 0) {
                    result.AddRange(from masking in maskingsInStation
                                    join station in _workContext.Stations() on masking.Id equals station.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = station.Current.AreaName,
                                        name = station.Current.Name,
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInRoom.Count > 0) {
                    result.AddRange(from masking in maskingsInRoom
                                    join room in _workContext.Rooms() on masking.Id equals room.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = room.Current.AreaName,
                                        name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInDevice.Count > 0) {
                    result.AddRange(from masking in maskingsInDevice
                                    join device in _workContext.Devices() on masking.Id equals device.Current.Id
                                    select new MaskingModel {
                                        index = masking.Time.Ticks,
                                        area = device.Current.AreaName,
                                        name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                        type = Common.GetMaskTypeDisplay(masking.Type),
                                        time = CommonHelper.DateTimeConverter(masking.Time)
                                    });
                }

                if (maskingsInPoint.Count > 0) {
                    foreach (var masking in maskingsInPoint) {
                        var ids = CommonHelper.SplitCondition(masking.Id);
                        if (ids.Length != 2) continue;

                        var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                        if (point == null) continue;

                        var device = _workContext.Devices().Find(d => d.Current.Id.Equals(ids[0]));
                        if (device == null) continue;

                        result.Add(new MaskingModel {
                            index = masking.Time.Ticks,
                            area = device.Current.AreaName,
                            name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                            type = Common.GetMaskTypeDisplay(masking.Type),
                            time = CommonHelper.DateTimeConverter(masking.Time)
                        });
                    }
                }
                #endregion
            } else {
                var nodeKey = Common.ParseNode(parent);
                if (nodeKey.Key == EnmSSH.Area) {
                    #region Area
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        if (maskingsInStation.Count > 0) {
                            var stations = _workContext.Stations().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInStation
                                            join station in stations on masking.Id equals station.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = station.Current.AreaName,
                                                name = station.Current.Name,
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInRoom.Count > 0) {
                            var rooms = _workContext.Rooms().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInRoom
                                            join room in rooms on masking.Id equals room.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = room.Current.AreaName,
                                                name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInDevice.Count > 0) {
                            var devices = _workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            result.AddRange(from masking in maskingsInDevice
                                            join device in devices on masking.Id equals device.Current.Id
                                            select new MaskingModel {
                                                index = masking.Time.Ticks,
                                                area = device.Current.AreaName,
                                                name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                                type = Common.GetMaskTypeDisplay(masking.Type),
                                                time = CommonHelper.DateTimeConverter(masking.Time)
                                            });
                        }

                        if (maskingsInPoint.Count > 0) {
                            var devices = _workContext.Devices().FindAll(s => current.Keys.Contains(s.Current.AreaId));
                            foreach (var masking in maskingsInPoint) {
                                var ids = CommonHelper.SplitCondition(masking.Id);
                                if (ids.Length != 2) continue;

                                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                                if (point == null) continue;

                                var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                                if (device == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = device.Current.AreaName,
                                    name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                    type = Common.GetMaskTypeDisplay(masking.Type),
                                    time = CommonHelper.DateTimeConverter(masking.Time)
                                });
                            }
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Station) {
                    #region Station
                    var current = maskingsInStation.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var station = _workContext.Stations().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (station != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = station.Current.AreaName,
                                name = station.Current.Name,
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInRoom.Count > 0) {
                        var rooms = _workContext.Rooms().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInRoom
                                        join room in rooms on masking.Id equals room.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = room.Current.AreaName,
                                            name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = device.Current.AreaName,
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.StationId.Equals(nodeKey.Value));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Room) {
                    #region Room
                    var current = maskingsInRoom.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var room = _workContext.Rooms().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (room != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = room.Current.AreaName,
                                name = string.Format("{0},{1}", room.Current.StationName, room.Current.Name),
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInDevice.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(nodeKey.Value));
                        result.AddRange(from masking in maskingsInDevice
                                        join device in devices on masking.Id equals device.Current.Id
                                        select new MaskingModel {
                                            index = masking.Time.Ticks,
                                            area = device.Current.AreaName,
                                            name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                            type = Common.GetMaskTypeDisplay(masking.Type),
                                            time = CommonHelper.DateTimeConverter(masking.Time)
                                        });
                    }

                    if (maskingsInPoint.Count > 0) {
                        var devices = _workContext.Devices().FindAll(s => s.Current.RoomId.Equals(nodeKey.Value));
                        foreach (var masking in maskingsInPoint) {
                            var ids = CommonHelper.SplitCondition(masking.Id);
                            if (ids.Length != 2) continue;

                            var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                            if (point == null) continue;

                            var device = devices.Find(d => d.Current.Id.Equals(ids[0]));
                            if (device == null) continue;

                            result.Add(new MaskingModel {
                                index = masking.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                type = Common.GetMaskTypeDisplay(masking.Type),
                                time = CommonHelper.DateTimeConverter(masking.Time)
                            });
                        }
                    }
                    #endregion
                } else if (nodeKey.Key == EnmSSH.Device) {
                    #region Device
                    var current = maskingsInDevice.Find(m => m.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (device != null) {
                            result.Add(new MaskingModel {
                                index = current.Time.Ticks,
                                area = device.Current.AreaName,
                                name = string.Format("{0},{1},{2}", device.Current.StationName, device.Current.RoomName, device.Current.Name),
                                type = Common.GetMaskTypeDisplay(current.Type),
                                time = CommonHelper.DateTimeConverter(current.Time)
                            });
                        }
                    }

                    if (maskingsInPoint.Count > 0) {
                        var device = _workContext.Devices().Find(s => s.Current.Id.Equals(nodeKey.Value));
                        if (device != null) {
                            foreach (var masking in maskingsInPoint) {
                                var ids = CommonHelper.SplitCondition(masking.Id);
                                if (ids.Length != 2) continue;

                                if (!device.Current.Id.Equals(ids[0])) continue;

                                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                                if (point == null) continue;

                                result.Add(new MaskingModel {
                                    index = masking.Time.Ticks,
                                    area = device.Current.AreaName,
                                    name = string.Format("{0},{1},{2},{3}", device.Current.StationName, device.Current.RoomName, device.Current.Name, point.Name),
                                    type = Common.GetMaskTypeDisplay(masking.Type),
                                    time = CommonHelper.DateTimeConverter(masking.Time)
                                });
                            }
                        }

                    }
                    #endregion
                }
            }

            var index = 0;
            result = result.OrderBy(r => r.index).ToList();
            foreach (var masking in result) {
                masking.index = ++index;
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        private List<PointParam> GetPointParams(string parent, int pmtype, string[] points, int[] types, bool cache) {
            var key = string.Format(GlobalCacheKeys.PointParamPattern, _workContext.Identifier());
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<PointParam>(key).ToList();

            var result = new List<PointParam>();

            List<D_Signal> signals = null;
            var _pmtype = Enum.IsDefined(typeof(EnmPointParam), pmtype) ? (EnmPointParam)pmtype : EnmPointParam.AbsThreshold;
            if (_pmtype == EnmPointParam.AbsThreshold) {
                signals = _signalService.GetAbsThresholds();
            } else if (_pmtype == EnmPointParam.PerThreshold) {
                signals = _signalService.GetPerThresholds();
            } else if (_pmtype == EnmPointParam.SavedPeriod) {
                signals = _signalService.GetSavedPeriods();
            } else if (_pmtype == EnmPointParam.StorageRefTime) {
                signals = _signalService.GetStorageRefTimes();
            } else if (_pmtype == EnmPointParam.AlarmLimit) {
                signals = _signalService.GetAlarmLimits();
            } else if (_pmtype == EnmPointParam.AlarmLevel) {
                signals = _signalService.GetAlarmLevels();
            } else if (_pmtype == EnmPointParam.AlarmDelay) {
                signals = _signalService.GetAlarmDelays();
            } else if (_pmtype == EnmPointParam.AlarmRecoveryDelay) {
                signals = _signalService.GetAlarmRecoveryDelays();
            } else if (_pmtype == EnmPointParam.AlarmFiltering) {
                signals = _signalService.GetAlarmFilterings();
            } else if (_pmtype == EnmPointParam.AlarmInferior) {
                signals = _signalService.GetAlarmInferiors();
            } else if (_pmtype == EnmPointParam.AlarmConnection) {
                signals = _signalService.GetAlarmConnections();
            } else if (_pmtype == EnmPointParam.AlarmReversal) {
                signals = _signalService.GetAlarmReversals();
            }

            if (signals != null && signals.Count > 0) {
                if (points != null && points.Length > 0)
                    signals = signals.FindAll(s => points.Contains(s.PointId));

                if (types != null && types.Length > 0)
                    signals = signals.FindAll(s => types.Contains((int)s.PointType));

                if (!string.IsNullOrWhiteSpace(parent) && !parent.Equals("root")) {
                    var nodeKey = Common.ParseNode(parent);
                    if (nodeKey.Key == EnmSSH.Area) {
                        var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                        if (current != null) signals = signals.FindAll(s => current.Keys.Contains(s.AreaId));
                    } else if (nodeKey.Key == EnmSSH.Station) {
                        signals = signals.FindAll(s => s.StationId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Room) {
                        signals = signals.FindAll(s => s.RoomId.Equals(nodeKey.Value));
                    } else if (nodeKey.Key == EnmSSH.Device) {
                        signals = signals.FindAll(s => s.DeviceId.Equals(nodeKey.Value));
                    }
                }

                var index = 0;
                foreach (var signal in signals) {
                    var current = string.IsNullOrWhiteSpace(signal.Current) ? "--" : signal.Current;
                    var normal = string.IsNullOrWhiteSpace(signal.Normal) ? "--" : signal.Normal;

                    var target = new PointParam {
                        index = ++index,
                        area = signal.AreaName,
                        station = signal.StationName,
                        room = signal.RoomName,
                        device = signal.DeviceName,
                        point = signal.PointName,
                        type = Common.GetPointParamDisplay(_pmtype),
                        current = current,
                        normal = normal,
                        diff = !current.Equals(normal)
                    };

                    if (_pmtype == EnmPointParam.AlarmFiltering
                        || _pmtype == EnmPointParam.AlarmInferior
                        || _pmtype == EnmPointParam.AlarmConnection
                        || _pmtype == EnmPointParam.AlarmReversal) {
                        target.diff = false;
                    }

                    target.background = target.diff ? Color.Red : Color.Transparent;
                    result.Add(target);
                }
            }

            if (result.Count <= GlobalCacheLimit.Default_Limit) {
                _cacheManager.AddItemsToList(key, result, GlobalCacheInterval.Site_Interval);
            }

            return result;
        }

        #endregion

    }
}