using System;
using System.Text;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSWebBase;
using CSWeb.App_Code;

namespace CSWeb.BIG3.Store
{
    public partial class cart : SiteBasePage
    {
        protected global::CSWeb.BIG3.UserControls.ShippingBillingCreditForm bscfShippingBillingCreditForm;
        protected override void Page_Load(object sender, EventArgs e)
        {
                string redirectPage = string.Empty;
                if (NavigationControl.CheckOrderFlow(Session["OrderStatus"], Request.RawUrl, out redirectPage))
                    Response.Redirect(redirectPage);
            if (!IsPostBack)
            {
                NavigationControl.DisableClientPageCache();
            }
            base.Page_Load(sender, e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            string redirectPage = string.Empty;
            if (NavigationControl.CheckOrderFlow(Session["OrderStatus"], Request.RawUrl, out redirectPage))
                Response.Redirect(redirectPage);
        }
    }
}