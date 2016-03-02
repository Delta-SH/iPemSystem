using iPem.Core;
using iPem.Core.Data;
using iPem.Core.Enum;
using iPem.Core.Task;
using iPem.Services.Master;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace iPem.Site {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //initialize engine context
            EngineContext.Initialize();

            //start scheduled tasks
            var scheduleTasks = new List<ITask> {
                new KeepAliveTask {Order = 0, Enabled = true},
                new ClearCacheTask{Order = 1, Enabled = true}
            };
            TaskManager.Instance.Initialize(scheduleTasks);
            TaskManager.Instance.Start();
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if(webHelper.IsStaticResource(this.Request))
                return;

            //sets application store.
            var local = webHelper.GetAppLocation();
            var host = webHelper.GetAppHost();
            var port = webHelper.GetAppPort();
            if(!local.Equals(EngineContext.Current.AppStore.Location, StringComparison.InvariantCultureIgnoreCase))
                EngineContext.Current.AppStore.Location = local;
            if(!host.Equals(EngineContext.Current.AppStore.Host, StringComparison.InvariantCultureIgnoreCase))
                EngineContext.Current.AppStore.Host = host;
            if(port != EngineContext.Current.AppStore.Port)
                EngineContext.Current.AppStore.Port = port;

            //keep alive page requested (we ignore it to prevent creating a guest customer records)
            var currentPage = webHelper.GetThisPageUrl(false);
            var keepAliveUrl = string.Format("{0}keepalive", local);
            if(currentPage.StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;

            //ensure database is installed
            var dbManager = EngineContext.Current.Resolve<IDbManager>();
            if(!dbManager.DatabaseIsInstalled()) {
                var installUrl = string.Format("{0}installation", local);
                if(!currentPage.StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase)) {
                    this.Response.Redirect(installUrl);
                }
            }

            if(!dbManager.DatabaseIsInstalled())
                return;
        }

        protected void Application_EndRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            //we don't do it in Application_BeginRequest because a user is not authenticated yet
            SetWorkingCulture();
        }

        protected void Application_Error(Object sender, EventArgs e) {
            var exception = Server.GetLastError();
            if(exception == null)
                return;

            var dbManager = EngineContext.Current.Resolve<IDbManager>();
            if(!dbManager.DatabaseIsInstalled())
                return;

            try {
                var logger = EngineContext.Current.Resolve<IWebLogger>();
                logger.Error(EnmEventType.Exception, exception.Message, exception);
            } catch(Exception) {
                //don't throw new exception if occurs
            }
        }

        protected void SetWorkingCulture() {
            //ensure database is installed
            var dbManager = EngineContext.Current.Resolve<IDbManager>();
            if(!dbManager.DatabaseIsInstalled())
                return;

            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if(webHelper.IsStaticResource(this.Request))
                return;

            //keep alive page requested (we ignore it to prevent creation of guest customer records)
            var currentPage = webHelper.GetThisPageUrl(false);
            var keepAliveUrl = string.Format("{0}keepalive", webHelper.GetAppLocation());
            if(currentPage.StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;

            if(this.Request.Cookies["UICulture"] != null) {
                var languageCulture = this.Request.Cookies["UICulture"].Value;
                if(!String.IsNullOrWhiteSpace(languageCulture)) {
                    var culture = new CultureInfo(languageCulture);
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
        }
    }
}