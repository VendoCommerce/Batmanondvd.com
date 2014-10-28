using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSWeb.App_Code;


namespace CSWeb.Mobile.Store
{
    public partial class receipt : ShoppingCartBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
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
