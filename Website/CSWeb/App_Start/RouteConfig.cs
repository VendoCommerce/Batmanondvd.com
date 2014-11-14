using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace CSWeb
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            AddVirtualVersions(routes);
        }

        private static void AddVirtualVersions(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("dynamicDefaultRedirect", "{version}/", "~/Redirector.aspx", true);
            routes.MapPageRoute("dynamicRedirect", "{version}/{page}", "~/Redirector.aspx", false);
        }
    }
}
