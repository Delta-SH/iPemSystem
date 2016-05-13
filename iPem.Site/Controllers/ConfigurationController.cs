using iPem.Core.Caching;
using iPem.Services.Master;
using iPem.Site.Infrastructure;
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

        private readonly IWebLogger _webLogger;
        private readonly IMenuService _menuService;

        #endregion

        #region Ctor

        public ConfigurationController(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IMenuService menuService) {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._menuService = menuService;
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

        #endregion

    }
}