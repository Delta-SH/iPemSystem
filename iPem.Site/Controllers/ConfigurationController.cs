using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class ConfigurationController : Controller {
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
    }
}