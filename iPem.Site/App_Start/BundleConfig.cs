using System.Web;
using System.Web.Optimization;

namespace iPem.Site {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            //validation
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery.validation-1.14.0/jquery.validate.js",
                "~/Scripts/jquery.validation-1.14.0/additional-methods.js",
                "~/Scripts/jquery.validation-1.14.0/localization/messages_zh.js"));

            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap-{version}.js"));
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/bootstrap-3.3.5/css/bootstrap.css"));

            //extjs
            bundles.Add(new ScriptBundle("~/bundles/extjs").Include(
                "~/Scripts/ext-4.2.1.883/ext-all-dev.js",
                "~/Scripts/ext-4.2.1.883/locale/ext-lang-zh_CN.js"));
            bundles.Add(new StyleBundle("~/bundles/extjs/css").Include(
                "~/Scripts/ext-4.2.1.883/resources/css/ext-all-neptune.css"));
        }
    }
}