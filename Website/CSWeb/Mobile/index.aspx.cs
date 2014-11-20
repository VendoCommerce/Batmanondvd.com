using System;
using System.Text;
using CSBusiness.Attributes;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSBusiness.Web;
using System.Web.UI;

namespace CSWeb.Mobile.Store
{
    public partial class index : CSWebBase.SiteBasePage
    {
        public string Version = "mobile";
        protected override bool IsLandingPage
        {
            get
            {
                return true;
            }
        }

        protected void imgPhone_Click(object sender, ImageClickEventArgs e)
        {
            UserSessions.InsertSessionEntry(Context, true, false);
            Response.Redirect("tel:18006732909");
        }

        protected void lb_Clicked(object sender, EventArgs e)
        {
            Session["PId"]=GetDynamicVersionData("mainkit");
            Response.Redirect("AddProduct.aspx?CId="+(int)CSBusiness.ShoppingManagement.ShoppingCartType.ShippingCreditCheckout);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (Request.Headers["X-HTTPS"] != null)
            {
                if (Request.Headers["X-HTTPS"].ToLower().Equals("no"))
                {
                    if (Request.Url.ToString().Contains("www"))
                    {
                        Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("index.aspx", "")));
                    }
                    else
                    {
                        Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("https://", "https://www.").Replace("index.aspx", "")));
                    }
                }
            }
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