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

            //jquery steps
            bundles.Add(new ScriptBundle("~/bundles/steps").Include(
                "~/Scripts/jquery.steps-{version}.js"));
            bundles.Add(new StyleBundle("~/bundles/steps/css").Include(
                "~/Content/install/jquery.steps.css"));

            //jquery cookie
            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                "~/Scripts/jquery.cookie-{version}.js"));

            //moment
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment-{version}.js"));

            //icheck
            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                "~/Scripts/icheck-{version}.js"));
            bundles.Add(new StyleBundle("~/bundles/icheck/css").Include(
                "~/Content/install/icheck-1.0.2/skins/line/blue.css",
                "~/Content/install/icheck-1.0.2/skins/flat/blue.css"));

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

            //ux widgets
            bundles.Add(new ScriptBundle("~/bundles/ux").Include(
                "~/Scripts/My97DatePicker/WdatePicker.js",
                "~/Scripts/ux/Label.js",
                "~/Scripts/ux/MultiCombo.js",
                "~/Scripts/ux/TreePicker.js",
                "~/Scripts/ux/DateTimePicker.js",
                "~/Scripts/ux/IFrame.js"));
            bundles.Add(new StyleBundle("~/bundles/ux/css").Include(
                "~/Content/ux/global.css",
                "~/Content/ux/font.css",
                "~/Content/ux/icon.css",
                "~/Content/ux/label.css",
                "~/Content/ux/multicombo.css"));

            //install
            bundles.Add(new ScriptBundle("~/bundles/install").Include(
                "~/Scripts/install.js"));
            bundles.Add(new StyleBundle("~/bundles/install/css").Include(
                "~/Content/install/style.css"));

            //database configuration
            bundles.Add(new ScriptBundle("~/bundles/install/dbconfiguration").Include(
                "~/Scripts/dbconfiguration.js"));
            bundles.Add(new StyleBundle("~/bundles/install/dbconfiguration/css").Include(
                "~/Content/install/dbconfiguration.css"));

            //site
            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                "~/Scripts/lang.js",
                "~/Scripts/icons.js",
                "~/Scripts/global.js",
                "~/Scripts/template.js"));
            bundles.Add(new StyleBundle("~/bundles/global/css").Include(
                "~/Content/global.css"));

            //component
            bundles.Add(new ScriptBundle("~/bundles/components").Include(
                "~/Scripts/components/AreaTypeComponent.js",
                "~/Scripts/components/StationTypeComponent.js",
                "~/Scripts/components/RoomTypeComponent.js",
                "~/Scripts/components/DeviceTypeComponent.js",
                "~/Scripts/components/AlarmLevelComponent.js",
                "~/Scripts/components/LogicTypeComponent.js",
                "~/Scripts/components/AreaComponent.js",
                "~/Scripts/components/StationComponent.js",
                "~/Scripts/components/RoomComponent.js",
                "~/Scripts/components/DeviceComponent.js",
                "~/Scripts/components/PointComponent.js",
                "~/Scripts/components/EmployeeComponent.js",
                "~/Scripts/components/ImageExporterComponent.js"));

            //help
            bundles.Add(new ScriptBundle("~/bundles/help").Include(
                "~/Scripts/help.js"));
            bundles.Add(new StyleBundle("~/bundles/help/css").Include(
                "~/Content/themes/css/help.css"));

            //speech
            bundles.Add(new ScriptBundle("~/bundles/speech").Include(
                "~/Scripts/home.speech.js"));
            bundles.Add(new StyleBundle("~/bundles/speech/css").Include(
                "~/Content/themes/css/home.speech.css"));
        }
    }
}