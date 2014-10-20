using System;
using System.Web;
using System.Globalization;
using System.Threading;
using CSCore.DataHelper;
using System.Web.UI.WebControls;

namespace CSWeb.Admin
{
    public class BasePage:System.Web.UI.Page
    {
        public int userTypeId = 0; 
        protected void BaseLoad()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            if (Request.Cookies["CSVal"] == null)
                Response.Redirect("/admin/login.aspx?targeturl=" + Request.RawUrl);
            else
            {

                HttpCookie cookie = Request.Cookies["CSVal"];
                userTypeId = Convert.ToInt32(cookie.Value);

            }
                    
        }

        
    }
}