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
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using RsSrv = iPem.Services.Resource;

namespace iPem.Site.Controllers {
    public class ReportController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;

        private readonly MsSrv.IWebLogger _webLogger;
        private readonly RsSrv.IEnumMethodsService _rsEnumMethodsService;

        #endregion

        #region Ctor

        public ReportController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            RsSrv.IEnumMethodsService rsEnumMethodsService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._rsEnumMethodsService = rsEnumMethodsService;
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

        #endregion

    }
}