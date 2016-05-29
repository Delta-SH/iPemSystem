using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class ErrorsController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult NoAccess() {
            return View();
        }

        public ActionResult FileNotFound() {
            return View();
        }

    }
}