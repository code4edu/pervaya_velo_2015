using System.Web;
using System.Web.Optimization;

namespace MyHackathon
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                        "~/Scripts/*.js",
						"~/Scripts/angular_libs/*.js",
						"~/Scripts/App/*.js"
						));
			
            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
        }
    }
}
