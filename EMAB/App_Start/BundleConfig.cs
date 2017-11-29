using System.Web;
using System.Web.Optimization;

namespace EMAB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js")                
                .Include("~/bower_components/jqueri-ui/jquery-ui.min.js")
                .Include("~/bower_components/bootstrap/dist/js/bootstrap.min.js")
                .Include("~/bower_components/moment/min/moment.min.js")
                .Include("~/bower_components/moment/locale/ru.js")
                .Include("~/bower_components/moment/locale/uk.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/bower_components/jquery-validation/dist/jquery.validate.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/bower_components/angular/angular.min.js",
                      "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                      "~/bower_components/angular-bootstrap-datetimepicker-directive/angular-bootstrap-datetimepicker-directive.min.js",
                      "~/bower_components/angular-http/angular-http.js",
                      "~/bower_components/angular-messages/angular-messages.min.js",
                      "~/bower_components/angular-moment/angular-moment.min.js",
                      "~/bower_components/angular-resource/angular-resource.min.js",
                      "~/bower_components/angular-sanitize/angular-sanitize.min.js",
                      "~/bower_components/angular-toastr/dist/angular-toastr.tpls.min.js",
                      "~/bower_components/angular-translate/angular-translate.min.js",
                      "~/bower_components/angular-ui-router/release/angular-ui-router.min.js",
                      "~/bower_components/angular-animate/angular-animate.min.js"
                      ));

        }
    }
}
