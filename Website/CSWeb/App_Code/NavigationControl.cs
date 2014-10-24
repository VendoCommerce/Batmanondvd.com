using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using System;
using System.Collections.Generic;
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
                redirectPage = "index.aspx";

            string orderStatusStr = (string)orderStatus;
            if (orderStatusStr == "PostSale")
                redirectPage = "postsale.aspx";

            if (orderStatusStr == "Receipt")
                redirectPage = "receipt.aspx";

            if (orderStatusStr == "Cart")
                redirectPage = "cart.aspx";

            if (redirectPage.Length > 0 && !url.Contains(redirectPage))
                return true;
            return false;
        }
    }
}