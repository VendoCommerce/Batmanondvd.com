using System;
using System.Text;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSBusiness.Web;
using CSWeb.App_Code;

namespace CSWeb.Mobile.Store
{
    public partial class cart : CSWebBase.SiteBasePage
    {
        public ClientCartContext ClientOrderData2
        {
            get
            {
                return (ClientCartContext)Session["ClientOrderData"];
            }
            set
            {
                Session["ClientOrderData"] = value;
            }
        }
         
        protected override void Page_Load(object sender, EventArgs e)
        {
                string redirectPage = string.Empty;
                if (NavigationControl.CheckOrderFlow(Session["OrderStatus"], Request.RawUrl, out redirectPage))
                    Response.Redirect(redirectPage);

                if (!IsPostBack)
                {
                    NavigationControl.DisableClientPageCache();
                }

            if (!Page.IsPostBack)
            {

                SitePreference sitePrefCache = CSFactory.GetCacheSitePref();
                if (!sitePrefCache.GeoLocationService)
                {
                    string GeoCoountry = "";
                    GeoCoountry = CommonHelper.GetGeoTargetLocation(CommonHelper.IpAddress(HttpContext.Current));                    
                }                

            }

            base.Page_Load(sender, e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            string redirectPage = string.Empty;
            if (NavigationControl.CheckOrderFlow(Session["OrderStatus"], Request.RawUrl, out redirectPage))
                Response.Redirect(redirectPage);
        }

        public string GetCleanPhoneNumber(string data)
        {
            return OrderHelper.GetCleanPhoneNumber(data);
        }



        public string GetDynamicVersionData(string data)
        {
            return OrderHelper.GetDynamicVersionData(data);
        }
    }
}