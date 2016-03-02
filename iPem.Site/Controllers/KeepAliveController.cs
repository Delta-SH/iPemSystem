using iPem.Core;
using iPem.Site.Infrastructure;
using System;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class KeepAliveController : Controller {
        public ActionResult Index() {
            return Content("I am alive!");
        }
    }
}