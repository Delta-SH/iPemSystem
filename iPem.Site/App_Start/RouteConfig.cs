using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace iPem.Site {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "Account/",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { "iPem.Site.Controllers" }
            );

            routes.MapRoute(
                "LogOut",
                "Account/",
                new { controller = "Account", action = "LogOut", id = UrlParameter.Optional },
                new[] { "iPem.Site.Controllers" }
            );

            routes.MapRoute(
                "HomePage",
                "Home/",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "iPem.Site.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "iPem.Site.Controllers" }
            );
        }
    }
}