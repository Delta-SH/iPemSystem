using iPem.Core.Caching;
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
using iPem.Core.Enum;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ConfigurationController : Controller {

        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IMenuService _menuService;
        private readonly RsSrv.IStationTypeService _stationTypeService;
        private readonly HsSrv.IActAlmService _actAlmService;

        #endregion

        #region Ctor

        public ConfigurationController(
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IMenuService menuService,
            RsSrv.IStationTypeService stationTypeService,
            HsSrv.IActAlmService actAlmService) {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._menuService = menuService;
            this._stationTypeService = stationTypeService;
            this._actAlmService = actAlmService;
        }

        #endregion

        #region Actions

        public ActionResult Index() {
            return View();
        }

        public ActionResult CfgIFrame() {
            return View();
        }

        public ActionResult Map() {
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
                var stations = _workContext.AssociatedStations.FindAll(s => {
                    if(string.IsNullOrWhiteSpace(s.Longitude))
                        return false;

                    if(string.IsNullOrWhiteSpace(s.Latitude))
                        return false;

                    double lng, lat;
                    if(!double.TryParse(s.Longitude, out lng))
                        return false;

                    if(!double.TryParse(s.Latitude, out lat))
                        return false;

                    return lng >= minlng && lng <= maxlng && lat >= minlat && lat <= maxlat;
                });

                if(stations.Count > 0) {
                    if(stations.Count > 100)
                        stations = stations.Take(100).ToList();

                    data.message = "200 Ok";
                    data.total = stations.Count;

                    var types = _stationTypeService.GetAllStationTypes();
                    var alarms = _actAlmService.GetAllActAlms();
                    var almsInDev = from alarm in alarms
                                    group alarm by alarm.DeviceId into g
                                    select new {
                                        Id = g.Key,
                                        Alarms = g
                                    };

                    var almsInDev2 = from alarm in almsInDev
                                     join attribute in _workContext.AssociatedDeviceAttributes.Values on alarm.Id equals attribute.Current.Id
                                     select new {
                                         Id = attribute.Station.Id,
                                         Alarms = alarm.Alarms
                                     };

                    var almsInSta = from alarm in almsInDev2
                                    group alarm by alarm.Id into g
                                    select new {
                                        Id = g.Key,
                                        Alarms = g.SelectMany(a => a.Alarms)
                                    };

                    foreach(var station in stations) {
                        var type = types.FirstOrDefault(t=>t.Id == station.StaTypeId);
                        var model = new MarkerModel() {
                            id = station.Id,
                            name = station.Name,
                            type = type != null ? type.Name : "",
                            lng = double.Parse(station.Longitude),
                            lat = double.Parse(station.Latitude)
                        };

                        var eachSta = almsInSta.FirstOrDefault(s => s.Id == station.Id);
                        if(eachSta != null) {
                            model.alm1 = eachSta.Alarms.Count(a=>a.AlmLevel == EnmAlarmLevel.Level1);
                            model.alm2 = eachSta.Alarms.Count(a => a.AlmLevel == EnmAlarmLevel.Level2);
                            model.alm3 = eachSta.Alarms.Count(a => a.AlmLevel == EnmAlarmLevel.Level3);
                            model.alm4 = eachSta.Alarms.Count(a => a.AlmLevel == EnmAlarmLevel.Level4);

                            if(model.alm1 > 0)
                                model.level = (int)EnmAlarmLevel.Level1;
                            else if(model.alm2 > 0)
                                model.level = (int)EnmAlarmLevel.Level2;
                            else if(model.alm3 > 0)
                                model.level = (int)EnmAlarmLevel.Level3;
                            else if(model.alm4 > 0)
                                model.level = (int)EnmAlarmLevel.Level4;
                            else
                                model.level = (int)EnmAlarmLevel.NoAlarm;
                        } else {
                            model.level = (int)EnmAlarmLevel.NoAlarm;
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

        #endregion

    }
}