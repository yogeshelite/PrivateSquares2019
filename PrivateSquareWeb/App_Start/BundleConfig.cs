using System.Web;
using System.Web.Optimization;

namespace PrivateSquareWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/theme/lib/jquery/js/jquery.js",
                        "~/theme/lib/popper.js/js/popper.js",
                        "~/theme/lib/bootstrap/js/bootstrap.js",
                        "~/theme/lib/jquery.cookie/js/jquery.cookie.js",
                        "~/theme/lib/select2/js/select2.full.min.js",
                        "~/theme/lib/jquery-ui/js/jquery-ui.js",
                        "~/theme/js/slim.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/theme/lib/font-awesome/css/font-awesome.css",
                      "~/theme/lib/Ionicons/css/ionicons.css",
                      "~/theme/lib/select2/css/select2.min.css",
                      "~/theme/css/slim.css"));
                


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}

