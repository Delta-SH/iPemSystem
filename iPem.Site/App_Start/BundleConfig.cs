using System.Web;
using System.Web.Optimization;

namespace iPem.Site {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/framework/jquery-{version}.js"));

            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/framework/bootstrap-{version}.js"));
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/framework/bootstrap-3.3.5/css/bootstrap.css"));
        }
    }
}