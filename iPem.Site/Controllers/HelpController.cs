using iPem.Site.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class HelpController : JsonNetController {
        public ActionResult Index() {
            ViewBag.active = 0;
            return View();
        }

        public ActionResult FAQ() {
            ViewBag.active = 1;
            return View();
        }

        public ActionResult Contact() {
            ViewBag.active = 2;
            return View();
        }
    }
}