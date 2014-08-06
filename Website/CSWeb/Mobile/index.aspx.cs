using System;
using System.Text;
using CSBusiness.Attributes;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSBusiness.Web;

namespace CSWeb.Mobile.Store
{
    public partial class index : CSBasePage
    {
        public string Version = "mobile";
        protected override bool IsLandingPage
        {
            get
            {
                return true;
            }
        }

        protected void lb_Clicked(object sender, EventArgs e)
        {
            Response.Redirect("AddProduct.aspx?PId="+GetDynamicVersionData("mainkit")+"&CId="+(int)CSBusiness.ShoppingManagement.ShoppingCartType.ShippingCreditCheckout);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
            if (!Page.IsPostBack)
            {

                SitePreference sitePrefCache = CSFactory.GetCacheSitePref();
                if (!sitePrefCache.GeoLocationService)
                {
                    string GeoCoountry = "";
                    GeoCoountry = CommonHelper.GetGeoTargetLocation(CommonHelper.IpAddress(HttpContext.Current));                    
                }

                if (Request["versionlp"] != null)
                {
                    Version = Request["versionlp"].ToString();
                    if (clientData.OrderAttributeValues != null)
                    {
                        if (clientData.OrderAttributeValues.ContainsKey("DynamicVerionName"))
                        {
                            Version = clientData.OrderAttributeValues["DynamicVerionName"].Value;
                        }
                        else
                        {
                            clientData.OrderAttributeValues.Add("DynamicVerionName", new AttributeValue(Version));
                        }
                    }
                }
                OrderHelper.SetDynamicLandingPageVersion(Version, clientData);

            }

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