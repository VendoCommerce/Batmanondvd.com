using System;
using System.Web;
using System.Globalization;
using System.Threading;
using CSCore.DataHelper;
using CSWebBase;

namespace CSWeb
{
	public class ShoppingCartBasePage : System.Web.UI.Page
	{
        protected override void OnLoad(EventArgs e)
        {
            if (Session["ClientOrderData"] == null)
            {
                if (Session["oid"] == null)
                {
                    Response.Redirect("CheckoutSessionExpired.aspx");
                }
            }

            base.OnLoad(e);
            AbTestingVersionUpdate updateVersionInfo = new AbTestingVersionUpdate();
            updateVersionInfo.LoadScripts(Page);            
        }
	}
}