using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace CSBusiness.Web
{
    public class VersionManager
    {

        private static Uri _originalUrl;
        public static Uri OriginaUri
        {
            get
            {
                if (_originalUrl == null)
                    return HttpContext.Current.Request.Url;
                else
                    return _originalUrl;
            }
        }

        public static string GetRedirectPath(RouteData routeData)
        {
            string version = (routeData.Values["version"] != null) ? routeData.Values["version"].ToString() : string.Empty;
            string page = (routeData.Values["page"] != null) ? routeData.Values["page"].ToString() : "index";
            version = GetRedirectVersion(version);

            if (version.Length > 0) version += "/";

            string path = string.Format("~/{0}{1}", version, GetPageName(page));
            _originalUrl = HttpContext.Current.Request.Url;
            return path;

        }

        private static string GetPageName(string page)
        {
            page = page.ToLower();
            if (!page.Contains(".aspx"))
                return string.Format("{0}.aspx", page);
            return page;
        }

        //Get dynamic version if it exists
        private static string GetRedirectVersion(string version)
        {
            switch (version.ToLower())
            {
                case "display":
                    return "";
                case "ps_a1":
                    return "";
                case "mobile2":
                    return "mobile";
                default:
                    return version;
            }

        }
    }

}
