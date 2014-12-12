using CSBusiness;
using CSBusiness.Preference;
using CSBusiness.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Tablet_BIG3
{
    public partial class choose : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.Page_Load(sender, e);
                SitePreference sitePrefCache = CSFactory.GetCacheSitePref();

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

            }

        }

        protected void lbSimple_Click(object sender, EventArgs e)
        {
            AddSku(ddlSimple.SelectedValue);
        }

        protected void lbComplete_Click(object sender, EventArgs e)
        {
            AddSku(ddlComplete.SelectedValue);
        }

        private void AddSku(string skuId)
        {
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
            if (clientData.CartInfo != null)
                clientData.CartInfo.CartItems.Clear();
            Session["PId"] = skuId;
            Session["OrderStatus"] = "Cart";

            Response.Redirect("AddProduct.aspx?PId=" + skuId + "&CId=3");

        }

    }
}