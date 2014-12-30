using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CSWeb.App_Code
{
    public class NavigationControl
    {
        /// <summary>
        /// Will check cart status and redirects to proper page if necessary
        /// </summary>
        /// <param name="cartContext">The current cart context</param>
        /// <param name="redirectPage">The page that user will be redirected to</param>
        /// <returns>If redirect is necessary or not</returns>
        public static bool CheckOrderFlow(object orderStatus, string url, out string redirectPage)// CSBusiness.ClientCartContext cartContext)
        {
            redirectPage = string.Empty;

            url = url.ToLower();
            if (orderStatus == null)
            {
                if (!url.Contains("/cart"))
                    redirectPage = "CheckoutSessionExpired.aspx";
                else
                {
                    redirectPage = "choose";
                }
            }


            string orderStatusStr = (string)orderStatus;
            if (orderStatusStr == "PostSale")
                redirectPage = "postsale";

            if (orderStatusStr == "Receipt")
                redirectPage = "receipt";

            if (orderStatusStr == "Cart")
                redirectPage = "cart";

            if (redirectPage.Length > 0 && !url.Contains(redirectPage))
                return true;
            return false;
        }

        public static void DisableClientPageCache()
        {
            ////HttpContext.Current.Response.Cache.SetNoStore();
            ////HttpContext.Current.Response.Cache.AppendCacheExtension("no-cache");
            ////HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            ////HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            ////HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            ////HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static void RouteTo(string version)
        {
                string fullPath = HttpContext.Current.Request.Url.AbsolutePath;
            string pageName = Path.GetFileName(fullPath);
            HttpContext.Current.Response.Redirect(string.Format("/{0}/{1}?{2}", version, pageName, HttpContext.Current.Request.QueryString.ToString()));
        }

    }
}