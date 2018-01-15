using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Services.Cs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ConfigurationController : JsonNetController {

        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IAAlarmService _actAlmService;

        #endregion

        #region Ctor

        public ConfigurationController(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IAAlarmService actAlmService) {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._actAlmService = actAlmService;
        }

        #endregion

        #region Actions

        public ActionResult Index() {
            if (!_workContext.Authorizations().Menus.Contains(3001))
                throw new HttpException(404, "Page not found.");

            ViewBag.RoleId = _workContext.Role().Id.ToString();
            return View();
        }

        public ActionResult Map() {
            if (!_workContext.Authorizations().Menus.Contains(3002))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult MapIFrame() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetMarkers(double minlng, double minlat, double maxlng, double maxlat) {
            var data = new AjaxDataModel<List<MarkerModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<MarkerModel>()
            };

            try {
                var stations = _workContext.Stations().FindAll(s => {
                    if(string.IsNullOrWhiteSpace(s.Current.Longitude))
                        return false;

                    if(string.IsNullOrWhiteSpace(s.Current.Latitude))
                        return false;

                    double lng, lat;
                    if(!double.TryParse(s.Current.Longitude, out lng))
                        return false;

                    if(!double.TryParse(s.Current.Latitude, out lat))
                        return false;

                    return lng >= minlng && lng <= maxlng && lat >= minlat && lat <= maxlat;
                });

                if(stations.Count > 0) {
                    if(stations.Count > 100) stations = stations.Take(100).ToList();
                    data.message = "200 Ok";
                    data.total = stations.Count;

                    var almsInSta = from alarm in _workContext.ActAlarms()
                                    group alarm by alarm.Current.StationId into g
                                    select new { Id = g.Key, Alarms = g.Select(a => a.Current) };

                    foreach(var station in stations) {
                        var model = new MarkerModel() {
                            id = station.Current.Id,
                            name = station.Current.Name,
                            type = station.Current.Type.Name,
                            lng = double.Parse(station.Current.Longitude),
                            lat = double.Parse(station.Current.Latitude)
                        };

                        var eachSta = almsInSta.FirstOrDefault(s => s.Id == station.Current.Id);
                        if(eachSta != null) {
                            model.alm1 = eachSta.Alarms.Count(a=>a.AlarmLevel == EnmAlarm.Level1);
                            model.alm2 = eachSta.Alarms.Count(a => a.AlarmLevel == EnmAlarm.Level2);
                            model.alm3 = eachSta.Alarms.Count(a => a.AlarmLevel == EnmAlarm.Level3);
                            model.alm4 = eachSta.Alarms.Count(a => a.AlarmLevel == EnmAlarm.Level4);

                            if(model.alm1 > 0)
                                model.level = (int)EnmAlarm.Level1;
                            else if(model.alm2 > 0)
                                model.level = (int)EnmAlarm.Level2;
                            else if(model.alm3 > 0)
                                model.level = (int)EnmAlarm.Level3;
                            else if(model.alm4 > 0)
                                model.level = (int)EnmAlarm.Level4;
                            else
                                model.level = (int)EnmAlarm.Level0;
                        } else {
                            model.level = (int)EnmAlarm.Level0;
                            model.alm1 = 0;
                            model.alm2 = 0;
                            model.alm3 = 0;
                            model.alm4 = 0;
                        }

                        data.data.Add(model);
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetWpfObj(string node) {
            var data = new AjaxDataModel<GviewModel> {
                success = false,
                message = "No data",
                total = 0,
                data = null
            };

            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");

                var nodeKey = Common.ParseNode(node);
                if (nodeKey.Key == EnmSSH.Area) {
                    var current = _workContext.Areas().Find(a => a.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = 1;
                        data.data = new GviewModel();
                        data.data.currentId = current.Current.Id;
                        data.data.currentType = (int)EnmAPISCObj.Area;
                        data.data.currentName = current.Current.Name;
                        if (current.HasParents) {
                            var parent = current.Parents.Last();
                            data.data.parentId = parent.Id;
                            data.data.parentType = (int)EnmAPISCObj.Area;
                            data.data.parentName = parent.Name;
                        }
                    }
                } else if (nodeKey.Key == EnmSSH.Station) {
                    var current = _workContext.Stations().Find(s => s.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = 1;
                        data.data = new GviewModel();
                        data.data.currentId = current.Current.Id;
                        data.data.currentType = (int)EnmAPISCObj.Station;
                        data.data.currentName = current.Current.Name;
                        data.data.parentId = current.Current.AreaId;
                        data.data.parentType = (int)EnmAPISCObj.Area;

                        var parent = _workContext.Areas().Find(a => a.Current.Id.Equals(current.Current.AreaId));
                        if(parent != null) data.data.parentName = parent.Current.Name;
                    }
                } else if (nodeKey.Key == EnmSSH.Room) {
                    var current = _workContext.Rooms().Find(r => r.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = 1;
                        data.data = new GviewModel();
                        data.data.currentId = current.Current.Id;
                        data.data.currentType = (int)EnmAPISCObj.Room;
                        data.data.currentName = current.Current.Name;
                        data.data.parentId = current.Current.StationId;
                        data.data.parentType = (int)EnmAPISCObj.Station;
                        data.data.parentName = current.Current.StationName;
                    }
                } else if (nodeKey.Key == EnmSSH.Device) {
                    var current = _workContext.Devices().Find(r => r.Current.Id.Equals(nodeKey.Value));
                    if (current != null) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = 1;
                        data.data = new GviewModel();
                        data.data.currentId = current.Current.Id;
                        data.data.currentType = (int)EnmAPISCObj.Device;
                        data.data.currentName = current.Current.Name;
                        data.data.parentId = current.Current.RoomId;
                        data.data.parentType = (int)EnmAPISCObj.Room;
                        data.data.parentName = current.Current.RoomName;
                    }
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetWpfCtrlObj(string node) {
            var data = new AjaxDataModel<GviewCtrlModel> {
                success = false,
                message = "No data",
                total = 0,
                data = null
            };

            try {
                if (string.IsNullOrWhiteSpace(node))
                    throw new ArgumentNullException("node");

                var ids = Common.SplitKeys(node);
                if (ids.Length != 2) throw new ArgumentException("node");

                var device = _workContext.Devices().Find(r => r.Current.Id.Equals(ids[0]));
                if (device == null) throw new iPemException("未找到设备信息");

                var point = _workContext.Points().Find(p => p.Id.Equals(ids[1]));
                if (point == null) throw new iPemException("未找到信号信息");

                if (point.Type != EnmPoint.AO && point.Type != EnmPoint.DO) 
                    throw new iPemException("信号类型错误");

                data.success = true;
                data.message = "200 Ok";
                data.total = 1;
                data.data = new GviewCtrlModel();
                data.data.deviceId = device.Current.Id;
                data.data.deviceName = device.Current.Name;
                data.data.pointId = point.Id;
                data.data.pointName = point.Name;
                data.data.pointType = (int)point.Type;
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}