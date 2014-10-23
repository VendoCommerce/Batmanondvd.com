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

namespace CSWeb.Mobile.Store
{
    public partial class cart : CSBasePage
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
            if (ClientOrderData2.OrderId > 0)
            {
                Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(ClientOrderData2.OrderId, true);
                if (orderData.OrderStatusId == 2)
                {
                    // this means that  customer clicked back, so should be directed to receipt page.
                    Response.Redirect("receipt.aspx");
                }

                if (orderData.OrderStatusId == 1)
                {
                    // this means that  customer clicked back, so should be directed to receipt page.
                    Response.Redirect("PostSale.aspx");
                }

                if (orderData.OrderStatusId == 4 || orderData.OrderStatusId == 5)
                {
                    // this means that  customer clicked back, so should be directed to receipt page.
                    Response.Redirect("AuthorizeOrder.aspx");
                }



            }
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {

                SitePreference sitePrefCache = CSFactory.GetCacheSitePref();
                if (!sitePrefCache.GeoLocationService)
                {
                    string GeoCoountry = "";
                    GeoCoountry = CommonHelper.GetGeoTargetLocation(CommonHelper.IpAddress(HttpContext.Current));                    
                }                

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