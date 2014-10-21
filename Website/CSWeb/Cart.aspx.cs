using System;
using System.Text;
using CSCore;
using CSCore.Utils;
using CSBusiness.Preference;
using CSBusiness;
using System.Web;
using CSWebBase;

namespace CSWeb.Root.Store
{
    public partial class cart : SiteBasePage
    {
        protected global::CSWeb.Root.UserControls.ShippingBillingCreditForm bscfShippingBillingCreditForm;
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }
    }
}