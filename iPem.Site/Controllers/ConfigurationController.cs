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
    public class ConfigurationController : Controller {

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
            if (!_workContext.Authorizations.Menus.Any(m => m.Id == 3001))
                throw new HttpException(404, "Page not found.");

            return View();
        }

        public ActionResult CfgIFrame() {
            return View();
        }

        public ActionResult Map() {
            if (!_workContext.Authorizations.Menus.Any(m => m.Id == 3002))
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
                var stations = _workContext.Stations.FindAll(s => {
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
                    if(stations.Count > 100)
                        stations = stations.Take(100).ToList();

                    data.message = "200 Ok";
                    data.total = stations.Count;

                    var alarms = _actAlmService.GetAlarms();
                    var almsInSta = from alarm in alarms
                                    group alarm by alarm.StationId into g
                                    select new {
                                        Id = g.Key,
                                        Alarms = g
                                    };

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

        #endregion

    }
}