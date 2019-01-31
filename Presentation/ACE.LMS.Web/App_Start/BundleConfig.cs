using System.Web;
using System.Web.Optimization;

namespace ACE.LMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqx").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/jsUtilities.js",
                "~/Scripts/jqBlockUI.js",
                "~/Scripts/jqwidgets/jqxcore.js",
                "~/Scripts/jqwidgets/jqxdata.js",
                "~/Scripts/jqwidgets/jqx-all.js",
                "~/Scripts/jqwidgets/jqxcore.js",
                "~/Scripts/jqwidgets/jqxangular.js"
                //"~/Scripts/jqwidgets/jqxvalidator.js"
               ));

            bundles.Add(new StyleBundle("~/Content/jqwidgets/css").Include("~/Content/jqwidgets/jqx.base.css", "~/Content/jqwidgets/jqx.web.css"
                , "~/Content/jqwidgets/jqx.android.css"
                , "~/Content/jqwidgets/jqx.arctic.css"
                , "~/Content/jqwidgets/jqx.black.css"
                , "~/Content/jqwidgets/jqx.blackberry.css"
                , "~/Content/jqwidgets/jqx.bootstrap.css"
                , "~/Content/jqwidgets/jqx.classic.css"
                , "~/Content/jqwidgets/jqx.darkblue.css"
                , "~/Content/jqwidgets/jqx.energyblue.css"
                , "~/Content/jqwidgets/jqx.fresh.css"
                , "~/Content/jqwidgets/jqx.highcontrast.css"
                , "~/Content/jqwidgets/jqx.metro.css"
                , "~/Content/jqwidgets/jqx.mobile.css"
                , "~/Content/jqwidgets/jqx.office.css"
                , "~/Content/jqwidgets/jqx.orange.css"
                , "~/Content/jqwidgets/jqx.shinyblack.css"
                , "~/Content/jqwidgets/jqx.summer.css"
                , "~/Content/jqwidgets/jqx.ui-sunny.css"));
        }
    }
}
