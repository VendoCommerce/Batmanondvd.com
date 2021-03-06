﻿using CSBusiness;
using CSBusiness.OrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Mobile
{
    public partial class choose : CSWebBase.SiteBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
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
            if (!IsPostBack)
            rbClassic_CheckedChanged(null,null);
        }

        protected void imgContinue_Click(object sender, EventArgs e)
        {
            string skuId="";
            if (rbClassic.Checked)
                skuId = "110"; 
            if (rbComplete.Checked)
                skuId = "111";
            if (skuId.Length > 0)
            {
                if (Session["ClientOrderData"] != null)
                {
                    ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
                    if (clientData.CartInfo != null)
                        clientData.CartInfo.CartItems.Clear();
                }

                Session["PId"] = skuId;
                Session["OrderStatus"] = "Cart";
                Response.Redirect("AddProduct.aspx?PId=" + skuId + "&CId=3");
            }
            else
                lblPrompt.Text = ResourceHelper.GetResoureValue("NoSkuSelectedError");
        }

        private void LoadOfferTerms(Sku sku)
        {
            sku.LoadAttributeValues();
            if (sku.ContainsAttribute("offerterms")
                && sku.AttributeValues["offerterms"] != null)
                ltOfferTerms.Text = sku.AttributeValues["offerterms"].Value;
        }

        protected void rbClassic_CheckedChanged(object sender, EventArgs e)
        {
            int skuId = 0;
            if (rbClassic.Checked)
                skuId = 110;
            if (rbComplete.Checked)
                skuId = 111;

            SkuManager skuManager = new SkuManager();
            LoadOfferTerms(skuManager.GetSkuByID(skuId));

        }


    }
}