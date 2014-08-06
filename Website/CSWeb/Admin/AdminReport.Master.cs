using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSData;

namespace CSWeb.Admin
{
    public partial class AdminReport : System.Web.UI.MasterPage
    {
        public static string siteName = string.Empty;
        public static string siteUrl = string.Empty;
        public static string siteTitle = string.Empty;
        protected string CartVersion
        {
            get
            {
                return "";

                // TODO
                /*
                string version = Session["AdminSite.CartVersion"] as string;

                if (!string.IsNullOrEmpty(version))
                {
                    
                }
                */
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                liHeader.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
                liSubHeader.Text = DateTime.Now.DayOfWeek + " " + DateTime.Now.AddHours(3).ToShortTimeString() + " (EST)";
                SitePref PrefObject = CSFactory.GetSitePreference();
                siteTitle = PrefObject.SiteHeader;
                siteName = PrefObject.SiteName;
                siteUrl = PrefObject.SiteUrl;
                imgLogo.ImageUrl = PrefObject.LogoPath;
                
            }
            if (Request.Cookies["CSVal"] != null)
            {
                HttpCookie cookie = Request.Cookies["CSVal"];
                int userTypeId = Convert.ToInt32(cookie.Value);
                if (userTypeId == 4)
                {
                    //plLeftNav.Visible = true;
                }

            }
        }
    }
}