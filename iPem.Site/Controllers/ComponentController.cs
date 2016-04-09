using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using RsSrv = iPem.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPem.Site.Extensions;
using iPem.Site.Models;
using iPem.Core.Enum;
using iPem.Core;
using iPem.Site.Infrastructure;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ComponentController : Controller {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly MsSrv.IWebLogger _webLogger;

        private readonly RsSrv.IStationTypeService _rsStationTypeService;
        private readonly RsSrv.IRoomTypeService _rsRoomTypeService;
        private readonly RsSrv.IDeviceTypeService _rsDeviceTypeService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;

        #endregion

        #region Ctor

        public ComponentController(
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            RsSrv.IStationTypeService rsStationTypeService,
            RsSrv.IRoomTypeService rsRoomTypeService,
            RsSrv.IDeviceTypeService rsDeviceTypeService,
            RsSrv.ILogicTypeService rsLogicTypeService) {
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._rsStationTypeService = rsStationTypeService;
            this._rsRoomTypeService = rsRoomTypeService;
            this._rsDeviceTypeService = rsDeviceTypeService;
            this._rsLogicTypeService = rsLogicTypeService;
        }

        #endregion

        #region Action

        [AjaxAuthorize]
        public JsonResult GetStationTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var models = _rsStationTypeService.GetAllStationTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<int, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false; 
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetRoomTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var models = _rsRoomTypeService.GetAllRoomTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<int, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetDeviceTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var models = _rsDeviceTypeService.GetAllDeviceTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<int, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetAlarmLevels(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmAlarmLevel level in Enum.GetValues(typeof(EnmAlarmLevel))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)level, text = Common.GetAlarmLevelDisplay(level) });
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetLogicTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var models = _rsLogicTypeService.GetAllLogicTypes(start / limit, limit);
                if(models.Count > 0) {
                    data.message = "200 Ok";
                    data.total = models.TotalCount;
                    data.data.AddRange(models.Select(d => new ComboItem<int, string> { id = d.Id, text = d.Name }));
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetPointTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                foreach(EnmPoint type in Enum.GetValues(typeof(EnmPoint))) {
                    data.data.Add(new ComboItem<int, string>() { id = (int)type, text = Common.GetPointTypeDisplay(type) });
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