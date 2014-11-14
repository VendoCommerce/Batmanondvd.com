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
            //routes.MapPageRoute("home", "{version}/home", "~/{version}/index.aspx");
            routes.MapPageRoute("dynamicRedirect", "{version}/{page}", "~/Redirector.aspx");
            //routes.MapPageRoute("dynamicDefaultRedirect", "{version}", "~/Redirector.aspx");
        }
    }
}
