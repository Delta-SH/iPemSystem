using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.API;
using iPem.Site.Models.BInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iPem.Site.Controllers {
    public class GViewController : ApiController {

        #region Fields

        private readonly IApiWorkContext _workContext;
        private readonly IStationService _stationService;
        private readonly IRoomService _roomService;
        private readonly IFsuService _fsuService;
        private readonly IDeviceService _deviceService;
        private readonly IPointService _pointService;
        private readonly ISignalService _signalService;
        private readonly IAMeasureService _ameasureService;
        private readonly IGImageService _gimageService;
        private readonly IGPageService _gpageService;
        private readonly IGTemplateService _gtemplateService;
        private readonly IGroupService _groupService;
        private readonly IPackMgr _packMgr;

        #endregion

        #region Ctor

        public GViewController(
            IApiWorkContext workContext,
            IStationService stationService,
            IRoomService roomService,
            IFsuService fsuService,
            IDeviceService deviceService,
            IPointService pointService,
            ISignalService signalService,
            IAMeasureService ameasureService,
            IGImageService gimageService,
            IGPageService gpageService,
            IGTemplateService gtemplateService,
            IGroupService groupService,
            IPackMgr packMgr) {
            this._workContext = workContext;
            this._stationService = stationService;
            this._roomService = roomService;
            this._fsuService = fsuService;
            this._deviceService = deviceService;
            this._pointService = pointService;
            this._signalService = signalService;
            this._ameasureService = ameasureService;
            this._gimageService = gimageService;
            this._gpageService = gpageService;
            this._gtemplateService = gtemplateService;
            this._groupService = groupService;
            this._packMgr = packMgr;
        }

        #endregion

        #region Actions

        [HttpGet]
        public List<String> GetGVPageNames(string role, string id, int type) {
            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                return _gpageService.GetNames((int)EnmAPISCObj.Device == type ? U_Role.SuperId : role, id, type);
            } catch {
            }

            return new List<String>();
        }

        [HttpGet]
        public List<API_GV_Page> GetGVPages(string role, string id, int type) {
            var data = new List<API_GV_Page>();

            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                foreach (var page in _gpageService.GetPages((int)EnmAPISCObj.Device == type ? U_Role.SuperId : role, id, type)) {
                    data.Add(new API_GV_Page {
                        Name = page.Name,
                        IsHome = page.IsHome,
                        Content = page.Content,
                        SCObjID = page.ObjId,
                        SCObjType = page.ObjType
                    });
                }
            } catch {
            }

            return data;
        }

        [HttpGet]
        public API_GV_Page GetGVPage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var page = _gpageService.GetPage(name);
                if (page != null) {
                    return new API_GV_Page {
                        Name = page.Name,
                        IsHome = page.IsHome,
                        Content = page.Content,
                        SCObjID = page.ObjId,
                        SCObjType = page.ObjType
                    };
                }
            } catch {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVPageName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gpageService.Exist(name);
            } catch {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVPage(string role, [FromBody]API_GV_Page page) {
            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (page == null)
                    throw new ArgumentNullException("value");

                var target = new G_Page {
                    RoleId = (int)EnmAPISCObj.Device == page.SCObjType ? U_Role.SuperId : role,
                    Name = page.Name,
                    IsHome = page.IsHome,
                    Content = page.Content,
                    ObjId = page.SCObjID,
                    ObjType = page.SCObjType
                };

                if (_gpageService.Exist(page.Name)) {
                    _gpageService.Update(target);
                } else {
                    _gpageService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String DeleteGVPage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                _gpageService.Remove(name);
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<String> GetGVTemplateNames() {
            try {
                return _gtemplateService.GetNames();
            } catch {
            }

            return new List<String>();
        }

        [HttpGet]
        public API_GV_Template GetGVTemplate(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var template = _gtemplateService.GetTemplate(name);
                if (template != null) {
                    return new API_GV_Template {
                        Name = template.Name,
                        Content = template.Content
                    };
                }
            } catch {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVTemplateName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gtemplateService.Exist(name);
            } catch {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVTemplate([FromBody]API_GV_Template template) {
            try {
                if (template == null)
                    throw new ArgumentNullException("template");

                var target = new G_Template {
                    Name = template.Name,
                    Content = template.Content
                };

                if (_gtemplateService.Exist(template.Name)) {
                    _gtemplateService.Update(target);
                } else {
                    _gtemplateService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String DeleteGVTemplate(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                _gtemplateService.Remove(name);
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String ClearGVTemplates() {
            try {
                _gtemplateService.Clear();
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<API_GV_ImageInfo> GetGVImageInfos() {
            var data = new List<API_GV_ImageInfo>();

            try {
                foreach (var image in _gimageService.GetNames()) {
                    data.Add(new API_GV_ImageInfo {
                        Name = image.Name,
                        Type = image.Type,
                        UpdateMark = image.UpdateMark
                    });
                }
            } catch {
            }

            return data;
        }

        [HttpGet]
        public List<API_GV_Image> GetMinGVImages() {
            var data = new List<API_GV_Image>();

            try {
                foreach (var image in _gimageService.GetThumbnails()) {
                    data.Add(new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        UpdateMark = image.UpdateMark,
                        Content = JsonConvert.SerializeObject(image.Thumbnail)
                    });
                }
            } catch {
            }

            return data;
        }

        [HttpPost]
        public List<API_GV_Image> GetGVImages([FromBody]List<string> names) {
            var data = new List<API_GV_Image>();

            try {
                if (names == null)
                    throw new ArgumentNullException("names");

                foreach (var image in _gimageService.GetContents(names)) {
                    data.Add(new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        UpdateMark = image.UpdateMark,
                        Content = JsonConvert.SerializeObject(image.Content)
                    });
                }
            } catch {
            }

            return data;
        }

        [HttpGet]
        public API_GV_Image GetGVImage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var image = _gimageService.GetImage(name);
                if (image != null) {
                    return new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        UpdateMark = image.UpdateMark,
                        Content = JsonConvert.SerializeObject(image.Content)
                    };
                }
            } catch {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVImageName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gimageService.Exist(name);
            } catch {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVImage([FromBody]API_GV_ImagePair image) {
            try {
                if (image == null)
                    throw new ArgumentNullException("image");

                var target = new G_Image {
                    Name = image.Name,
                    Type = image.Type,
                    Content = JsonConvert.DeserializeObject<byte[]>(image.Source),
                    Thumbnail = JsonConvert.DeserializeObject<byte[]>(image.Min),
                    UpdateMark = image.UpdateMark
                };

                if (_gimageService.Exist(image.Name)) {
                    _gimageService.Update(target);
                } else {
                    _gimageService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String DeleteGVImage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                _gimageService.Remove(name);
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String ClearGVImages() {
            try {
                _gimageService.Clear();
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<API_GV_SCObj> GetSubSCObjs(string role, string id, int type) {
            var data = new List<API_GV_SCObj>();

            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                if ((int)EnmAPISCObj.Area == type) {
                    if ("root".Equals(id)) {
                        var roots = _workContext.GetAreas(role).FindAll(a => !a.HasParents);
                        if (roots.Count > 0) {
                            foreach (var root in roots) {
                                data.Add(new API_GV_SCObj {
                                    ID = root.Current.Id,
                                    Name = root.Current.Name,
                                    Type = (int)EnmAPISCObj.Area
                                });
                            }
                        }
                    } else {
                        var current = _workContext.GetAreas(role).Find(a => a.Current.Id.Equals(id));
                        if (current != null) {
                            if (current.HasChildren) {
                                foreach (var child in current.ChildRoot) {
                                    data.Add(new API_GV_SCObj {
                                        ID = child.Current.Id,
                                        Name = child.Current.Name,
                                        Type = (int)EnmAPISCObj.Area
                                    });
                                }
                            } else {
                                var stations = _workContext.GetStations(role).FindAll(s => s.AreaId.Equals(id));
                                foreach (var child in stations) {
                                    data.Add(new API_GV_SCObj {
                                        ID = child.Id,
                                        Name = child.Name,
                                        Type = (int)EnmAPISCObj.Station
                                    });
                                }
                            }
                        }
                    }
                } else if ((int)EnmAPISCObj.Station == type) {
                    var rooms = _workContext.GetRooms(role).FindAll(s => s.StationId.Equals(id));
                    foreach (var child in rooms) {
                        data.Add(new API_GV_SCObj {
                            ID = child.Id,
                            Name = child.Name,
                            Type = (int)EnmAPISCObj.Room
                        });
                    }
                } else if ((int)EnmAPISCObj.Room == type) {
                    var devices = _workContext.GetDevices(role).FindAll(s => s.RoomId.Equals(id));
                    foreach (var child in devices) {
                        data.Add(new API_GV_SCObj {
                            ID = child.Id,
                            Name = child.Name,
                            Type = (int)EnmAPISCObj.Device
                        });
                    }
                } else if ((int)EnmAPISCObj.Device == type) {
                    var signals = _signalService.GetAllSignals(id);
                    foreach (var child in signals) {
                        data.Add(new API_GV_SCObj {
                            ID = Common.JoinKeys(id, child.PointId),
                            Name = child.PointName,
                            Type = (int)EnmAPISCObj.Signal
                        });
                    }
                }
            } catch { }

            return data;
        }

        [HttpPost]
        public List<API_GV_SCValue> GetSCValues(string role, [FromBody]List<API_GV_Key> keys) {
            var data = new List<API_GV_SCValue>();

            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (keys == null)
                    throw new ArgumentNullException("keys");

                List<AlmStore<A_AAlarm>> alarms = null;
                foreach (var key in keys.GroupBy(k => k.Type)) {
                    if ((int)EnmAPISCObj.Area == key.Key) {
                        #region 计算区域状态
                        var ids = key.Select(k => k.ID);
                        if (alarms == null) alarms = _workContext.ActAlarms(role);
                        if (alarms.Count > 0) {
                            var arealevels = alarms.GroupBy(a => a.Current.AreaId).Select(g => new { id = g.Key, level = g.Min(a => (int)a.Current.AlarmLevel) }).ToList();
                            foreach (var id in ids) {
                                var target = new API_GV_SCValue {
                                    ID = id,
                                    Type = (int)EnmAPISCObj.Area,
                                    Number = (int)EnmAlarm.Level0,
                                    Desc = Common.GetAlarmDisplay(EnmAlarm.Level0),
                                    State = (int)EnmAlarm.Level0
                                };

                                if ("root".Equals(id)) {
                                    target.Number = target.State = arealevels.Min(a => a.level);
                                    target.Desc = Common.GetAlarmDisplay((EnmAlarm)target.State);
                                } else {
                                    var current = _workContext.GetAreas(role).Find(a => a.Current.Id.Equals(id));
                                    if (current != null) {
                                        if (current.HasChildren) {
                                            var levels = arealevels.FindAll(i => current.Keys.Contains(i.id));
                                            if (levels.Count > 0) {
                                                target.Number = target.State = levels.Min(a => a.level);
                                                target.Desc = Common.GetAlarmDisplay((EnmAlarm)target.State);
                                            }
                                        } else {
                                            var level = arealevels.Find(i => current.Current.Id.Equals(i.id));
                                            if (level != null) {
                                                target.Number = target.State = level.level;
                                                target.Desc = Common.GetAlarmDisplay((EnmAlarm)target.State);
                                            }
                                        }
                                    }
                                }

                                data.Add(target);
                            }
                        } else {
                            foreach (var id in ids) {
                                data.Add(new API_GV_SCValue {
                                    ID = id,
                                    Type = (int)EnmAPISCObj.Area,
                                    Number = (int)EnmAlarm.Level0,
                                    Desc = Common.GetAlarmDisplay(EnmAlarm.Level0),
                                    State = (int)EnmAlarm.Level0
                                });
                            }
                        }

                        #endregion
                    } else if ((int)EnmAPISCObj.Station == key.Key) {
                        #region 计算站点状态
                        var ids = key.Select(k => k.ID);
                        if (alarms == null) alarms = _workContext.ActAlarms(role);
                        if (alarms.Count > 0) {
                            var levels = alarms.GroupBy(a => a.Current.StationId).Select(g => new { id = g.Key, level = g.Min(a => (int)a.Current.AlarmLevel) });
                            data.AddRange(from id in ids
                                          join lvl in levels on id equals lvl.id into lt
                                          from il in lt.DefaultIfEmpty()
                                          select new API_GV_SCValue {
                                              ID = id,
                                              Type = (int)EnmAPISCObj.Station,
                                              Number = il == null ? (int)EnmAlarm.Level0 : il.level,
                                              Desc = Common.GetAlarmDisplay(il == null ? EnmAlarm.Level0 : (EnmAlarm)il.level),
                                              State = il == null ? (int)EnmAlarm.Level0 : il.level
                                          });
                        } else {
                            foreach (var id in ids) {
                                data.Add(new API_GV_SCValue {
                                    ID = id,
                                    Type = (int)EnmAPISCObj.Station,
                                    Number = (int)EnmAlarm.Level0,
                                    Desc = Common.GetAlarmDisplay(EnmAlarm.Level0),
                                    State = (int)EnmAlarm.Level0
                                });
                            }
                        }
                        #endregion
                    } else if ((int)EnmAPISCObj.Room == key.Key) {
                        #region 计算机房状态
                        var ids = key.Select(k => k.ID);
                        if (alarms == null) alarms = _workContext.ActAlarms(role);
                        if (alarms.Count > 0) {
                            var levels = alarms.GroupBy(a => a.Current.RoomId).Select(g => new { id = g.Key, level = g.Min(a => (int)a.Current.AlarmLevel) });
                            data.AddRange(from id in ids
                                          join lvl in levels on id equals lvl.id into lt
                                          from il in lt.DefaultIfEmpty()
                                          select new API_GV_SCValue {
                                              ID = id,
                                              Type = (int)EnmAPISCObj.Room,
                                              Number = il == null ? (int)EnmAlarm.Level0 : il.level,
                                              Desc = Common.GetAlarmDisplay(il == null ? EnmAlarm.Level0 : (EnmAlarm)il.level),
                                              State = il == null ? (int)EnmAlarm.Level0 : il.level
                                          });
                        } else {
                            foreach (var id in ids) {
                                data.Add(new API_GV_SCValue {
                                    ID = id,
                                    Type = (int)EnmAPISCObj.Room,
                                    Number = (int)EnmAlarm.Level0,
                                    Desc = Common.GetAlarmDisplay(EnmAlarm.Level0),
                                    State = (int)EnmAlarm.Level0
                                });
                            }
                        }
                        #endregion
                    } else if ((int)EnmAPISCObj.Device == key.Key) {
                        #region 计算设备状态
                        var ids = key.Select(k => k.ID);
                        if (alarms == null) alarms = _workContext.ActAlarms(role);
                        if (alarms.Count > 0) {
                            var levels = alarms.GroupBy(a => a.Current.DeviceId).Select(g => new { id = g.Key, level = g.Min(a => (int)a.Current.AlarmLevel) });
                            data.AddRange(from id in ids
                                          join lvl in levels on id equals lvl.id into lt
                                          from il in lt.DefaultIfEmpty()
                                          select new API_GV_SCValue {
                                              ID = id,
                                              Type = (int)EnmAPISCObj.Device,
                                              Number = il == null ? (int)EnmAlarm.Level0 : il.level,
                                              Desc = Common.GetAlarmDisplay(il == null ? EnmAlarm.Level0 : (EnmAlarm)il.level),
                                              State = il == null ? (int)EnmAlarm.Level0 : il.level
                                          });
                        } else {
                            foreach (var id in ids) {
                                data.Add(new API_GV_SCValue {
                                    ID = id,
                                    Type = (int)EnmAPISCObj.Device,
                                    Number = (int)EnmAlarm.Level0,
                                    Desc = Common.GetAlarmDisplay(EnmAlarm.Level0),
                                    State = (int)EnmAlarm.Level0
                                });
                            }
                        }
                        #endregion
                    } else if ((int)EnmAPISCObj.Signal == key.Key) {
                        #region 计算信号状态
                        var nodes = new List<Kv<string, string>>();
                        var nkeys = new HashSet<string>();
                        foreach (var id in key.Select(k => k.ID)) {
                            var ids = Common.SplitKeys(id);
                            if (ids.Length != 2) continue;
                            nodes.Add(new Kv<string, string> { Key = ids[0], Value = ids[1] });
                            nkeys.Add(ids[1]);
                        }

                        var points = _workContext.GetPoints().FindAll(p => nkeys.Contains(p.Id));
                        var values = _ameasureService.GetMeasures(nodes);
                        var signals = from node in nodes
                                      join point in points on node.Value equals point.Id
                                      join value in values on new { DeviceId = node.Key, PointId = node.Value } equals new { value.DeviceId, value.PointId } into lt1
                                      from nv in lt1.DefaultIfEmpty()
                                      select new API_GV_Signal {
                                          DeviceId = node.Key,
                                          PointId = node.Value,
                                          Type = (int)_workContext.GetPointType(point),
                                          Number = nv != null ? nv.Value : 0,
                                          Desc = nv != null ? Common.GetUnitDisplay(point.Type, nv.Value.ToString(), point.UnitState) : "",
                                          State = nv != null && (nv.Status == EnmState.Invalid || nv.Status == EnmState.Opevent) ? (int)EnmAPIState.Invalid : (int)nv.Status
                                      };

                        var alsignals = signals.Where(t => t.Type == (int)EnmPoint.AL);
                        if (alsignals.Any()) {
                            var almKeys = new Dictionary<string, EnmAlarm>();
                            if (alarms == null) alarms = _workContext.ActAlarms(role);
                            foreach (var alarm in alarms) {
                                almKeys[Common.JoinKeys(alarm.Current.DeviceId, alarm.Current.PointId)] = alarm.Current.AlarmLevel;
                            }

                            foreach (var signal in alsignals) {
                                var _key = Common.JoinKeys(signal.DeviceId, signal.PointId);
                                if (almKeys.ContainsKey(_key)) {
                                    signal.State = (int)Common.LevelToState(almKeys[_key]);
                                }
                            }
                        }

                        data.AddRange(signals.Select(s => new API_GV_SCValue {
                            ID = Common.JoinKeys(s.DeviceId, s.PointId),
                            Type = (int)EnmAPISCObj.Signal,
                            Number = s.Number,
                            Desc = s.Type == (int)EnmPoint.AI ? string.Format("{0} {1}", s.Number, s.Desc) : s.Desc,
                            State = s.State
                        }));
                        #endregion
                    }
                }
            } catch {
            }

            return data;
        }

        [HttpPost]
        public String OpSignal([FromBody]API_GV_OpSignal signal) {
            try {
                if (signal == null) throw new ArgumentNullException("signal");
                var ids = Common.SplitKeys(signal.ID);
                if (ids.Length != 2) throw new iPemException("参数ID无效");
                var device = ids[0]; var point = ids[1];
                var value = signal.Value;

                if (string.IsNullOrWhiteSpace(device)) throw new ArgumentNullException("device");
                if (string.IsNullOrWhiteSpace(point)) throw new ArgumentNullException("point");

                var curDevice = _deviceService.GetDevice(device);
                if (curDevice == null) throw new iPemException("未找到设备");

                var curFsu = _fsuService.GetFsu(curDevice.FsuId);
                if (curFsu == null) throw new iPemException("未找到Fsu");

                var curExtFsu = _fsuService.GetExtFsu(curFsu.Id);
                if (curExtFsu == null) throw new iPemException("未找到Fsu");
                if (!curExtFsu.Status) throw new iPemException("Fsu通信中断");

                var curGroup = _groupService.GetGroup(curExtFsu.GroupId);
                if (curGroup == null) throw new iPemException("未找到SC采集组");
                if (!curGroup.Status) throw new iPemException("SC通信中断");

                var signals = _signalService.GetAllSignals(new Kv<string, string>[] { new Kv<string, string>(device, point) });
                if (signals.Count == 0) throw new iPemException("未找到信号");
                var curPoint = signals.First();

                var package = new SetPointPackage() {
                    FsuId = curFsu.Code,
                    DeviceList = new List<SetPointDevice>() {
                        new SetPointDevice() {
                            Id = curDevice.Code,
                            Values = new List<TSemaphore>() {
                                new TSemaphore() {
                                    Id = curPoint.Code,
                                    SignalNumber = curPoint.Number,
                                    Type = (EnmBIPoint)((int)curPoint.PointType),
                                    MeasuredVal = "NULL",
                                    SetupVal = value.ToString(),
                                    Status = EnmBIState.NOALARM,
                                    Time = DateTime.Now
                                }
                            }
                        }
                    }
                };

                var result = _packMgr.SetPoint(new UriBuilder("http", curGroup.IP, curGroup.Port, "/").ToString(), package);
                if (result != null) {
                    if (result.Result == EnmBIResult.FAILURE) throw new iPemException(result.FailureCause ?? "参数设置失败");
                    if (result.DeviceList != null) {
                        var devResult = result.DeviceList.Find(d => d.Id == curDevice.Code);
                        if (devResult != null && devResult.SuccessList.Any(s => s.Id == curPoint.Code && s.SignalNumber == curPoint.Number)) {
                            return null;
                        }
                    }
                }

                throw new iPemException("参数设置失败");
            } catch (Exception exc) {
                return exc.Message;
            }
        }

        #endregion

    }
}