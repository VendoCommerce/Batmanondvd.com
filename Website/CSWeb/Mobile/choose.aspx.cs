using CSBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Mobile
{
    public partial class choose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgContinue_Click(object sender, EventArgs e)
        {
            string skuId="";
            if (rbClassic.Checked)
                skuId = "112"; 
            if (rbComplete.Checked)
                skuId = "113";
            if (skuId.Length > 0)
            {
                ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
                if (clientData.CartInfo != null)
                    clientData.CartInfo.CartItems.Clear();

                Session["PId"] = skuId;
                Response.Redirect("AddProduct.aspx?PId=" + skuId + "&CId=3");
            }
            else
                lblPrompt.Text = ResourceHelper.GetResoureValue("NoSkuSelectedError");
        }
    }
}