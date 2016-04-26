using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region StyleBundle

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/site.css"
            ));

            bundles.Add(new StyleBundle("~/Content/jqueryui/css").Include(
                "~/Content/jqueryui/jquery-ui-1.10.4.custom.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common-material.min.css",
                "~/Content/kendo/kendo.material.min.css",
                "~/Content/kendo/kendo.dataviz.min.css",
                "~/Content/kendo/kendo.dataviz.material.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss/css").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap-tour/bootstrap-tour.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                "~/Content/sweet-alert/sweet-alert.css"));

            #endregion

            #region ScriptBundle

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.11.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-1.10.4.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/bootstrap/js/bootstrap.min.js",
                "~/Scripts/bootstrap-tour/bootstrap-tour.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/cultures/kendo.culture.id-ID.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/sweet-alert").Include(
                "~/Scripts/sweet-alert/sweet-alert.min.js"
            ));

            //ckeditor
            bundles.Add(new ScriptBundle("~/Scripts/wcw/ckeditor").Include(
                "~/Scripts/wcw/ckeditor.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/webapp").Include(
                "~/Scripts/webapp.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                 "~/Scripts/modernizr-*"));

            //angular js
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-route.js"));

            var scriptBundle = new ScriptBundle("~/bundles/angularApp")
                .IncludeDirectory("~/Scripts/app/", "*.js", searchSubdirectories: true);

            bundles.Add(scriptBundle);

            #endregion

            #region smart admin

            bundles.Add(new StyleBundle("~/Content/smart-admin/css/bundle").IncludeDirectory("~/Content/smart-admin/css", "*.min.css"));

            bundles.Add(new ScriptBundle("~/scripts/smartadmin").Include(
                "~/scripts/smart-admin/app.config.js",
                "~/scripts/smart-admin/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/Content/bootstrap/js/bootstrap.min.js",
                "~/scripts/smart-admin/notification/SmartNotification.min.js",
                "~/scripts/smart-admin/smartwidgets/jarvis.widget.min.js",
                "~/scripts/smart-admin/plugin/jquery-validate/jquery.validate.min.js",
                "~/scripts/smart-admin/plugin/masked-input/jquery.maskedinput.min.js",
                "~/scripts/smart-admin/plugin/select2/select2.min.js",
                "~/scripts/smart-admin/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "~/scripts/smart-admin/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/scripts/smart-admin/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/scripts/smart-admin/plugin/fastclick/fastclick.min.js",
                "~/scripts/smart-admin/app.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/full-calendar").Include(
                "~/scripts/smart-admin/plugin/moment/moment.min.js",
                "~/scripts/smart-admin/plugin/fullcalendar/jquery.fullcalendar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/charts").Include(
                "~/scripts/smart-admin/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                "~/scripts/smart-admin/plugin/sparkline/jquery.sparkline.min.js",
                "~/scripts/smart-admin/plugin/morris/morris.min.js",
                "~/scripts/smart-admin/plugin/morris/raphael.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.cust.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.resize.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.time.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.fillbetween.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.orderBar.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.pie.min.js",
                "~/scripts/smart-admin/plugin/flot/jquery.flot.tooltip.min.js",
                "~/scripts/smart-admin/plugin/dygraphs/dygraph-combined.min.js",
                "~/scripts/smart-admin/plugin/chartjs/chart.min.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/datatables").Include(
                "~/scripts/smart-admin/plugin/datatables/jquery.dataTables.min.js",
                "~/scripts/smart-admin/plugin/datatables/dataTables.colVis.min.js",
                "~/scripts/smart-admin/plugin/datatables/dataTables.tableTools.min.js",
                "~/scripts/smart-admin/plugin/datatables/dataTables.bootstrap.min.js",
                "~/scripts/smart-admin/plugin/datatable-responsive/datatables.responsive.min.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/jq-grid").Include(
                "~/scripts/smart-admin/plugin/jqgrid/jquery.jqGrid.min.js",
                "~/scripts/smart-admin/plugin/jqgrid/grid.locale-en.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/forms").Include(
                "~/scripts/smart-admin/plugin/jquery-form/jquery-form.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/smart-chat").Include(
                "~/scripts/smart-admin/smart-chat-ui/smart.chat.ui.min.js",
                "~/scripts/smart-admin/smart-chat-ui/smart.chat.manager.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/vector-map").Include(
                "~/scripts/smart-admin/plugin/vectormap/jquery-jvectormap-1.2.2.min.js",
                "~/scripts/smart-admin/plugin/vectormap/jquery-jvectormap-world-mill-en.js"
                ));

            #endregion

            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = true;
        }
    }
}