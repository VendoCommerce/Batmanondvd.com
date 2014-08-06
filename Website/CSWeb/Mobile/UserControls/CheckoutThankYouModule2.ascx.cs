using System;
using System.Web;
using System.Web.UI;
using CSBusiness.Preference;
using CSBusiness.Resolver;
using CSBusiness.OrderManagement;
using CSBusiness.Cache;
using System.Collections.Generic;
using CSBusiness;
using System.Web.UI.WebControls;
using CSWeb.Mobile.Store;
using System.Text;
using CSData;
using CSBusiness.Attributes;

namespace CSWeb.Mobile.UserControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckoutThankYouModule2 : System.Web.UI.UserControl
    {
        protected Literal LiteralSubTotal, LiteralShipping, LiteralTax, LiteralTotal,
            LiteralName, LiteralAddress, LiteralCity, LiteralEmail, LiteralZip, LiteralState, LiteralCountry,
            LiteralName_b, LiteralAddress_b, LiteralCity_b, LiteralZip_b, LiteralState_b, LiteralCountry_b, LiteralRushShipping, LiteralGoogleAnalytics, LiteralID, LiteralSid, LiteralOfferId,
            LiteralAddress2, LiteralAddress2_b, LiteralPhone;

        protected DataList dlordersList;
        protected Label lblPurchaseName, lblPromotionPrice;
        protected System.Web.UI.WebControls.Panel pnlRushLabel, pnlRush, pnlPromotionalAmount, pnlPromotionLabel, pnlBAddress2, pnlSAddress2;
        protected HyperLink hlPrintLink;
        public int orderId = 0;

        private ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] as ClientCartContext;
            }
        }

        public decimal CalculateSkuBaseShipping(int skuId)
        {
            //Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId);
            SitePreference shippingPreferences = CSFactory.GetCartPrefrence(CartContext.CartInfo);
            int prefid = shippingPreferences.ShippingPrefID;
            List<SkuShipping> skuShippingItems = ShippingDAL.GetSkuShipping();
            decimal shippingCost = 0;

            int totalShippingSettings = skuShippingItems.Count;
            for (int i = 0; i < totalShippingSettings; i++)
            {
                SkuShipping currentSetting = skuShippingItems[i];
                if (skuId == currentSetting.SkuId && prefid == currentSetting.PrefId)
                {
                    shippingCost += currentSetting.Cost;
                }
            }
            return shippingCost;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["oId"] != null)
                orderId = Convert.ToInt32(Request.Params["oId"]);
            else
                orderId = CartContext.OrderId;
            if (!this.IsPostBack)
            {
                if (orderId > 0)
                {
                    Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId);
                    if (!orderData.AttributeValuesLoaded)
                        orderData.LoadAttributeValues();

                    if (!orderData.AttributeValues.ContainsAttribute("OrderFlowCompleted"))
                    {
                        Dictionary<string, AttributeValue> orderAttributes = new Dictionary<string, AttributeValue>();
                        orderAttributes.Add("OrderFlowCompleted", new CSBusiness.Attributes.AttributeValue("1"));
                        CSResolve.Resolve<IOrderService>().UpdateOrderAttributes(orderData.OrderId, orderAttributes, orderData.OrderStatusId);
                    }
                }
                BindData();
                //Fire OrderConfirmation Test
                OrderHelper.SendOrderCompletedEmail(orderId);

                //reset entire Context object
                //this.CartContext.EmptyData();
            }
        }

        private void BindData()
        {
            if (orderId > 0)
            {
                Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId);

                dlordersList.DataSource = orderData.SkuItems;
                dlordersList.DataBind();
                LiteralSubTotal.Text = Math.Round(orderData.SubTotal, 2).ToString();
                LiteralShipping.Text = Math.Round(orderData.ShippingCost, 2).ToString();
                LiteralTax.Text = Math.Round(orderData.Tax, 2).ToString();
                LiteralTotal.Text = Math.Round(orderData.Total, 2).ToString();
                if (orderData.RushShippingCost > 0)
                {
                    pnlRushLabel.Visible = true;
                    pnlRush.Visible = true;
                    LiteralRushShipping.Text = Math.Round(orderData.RushShippingCost, 2).ToString();
                }


                if (orderData.DiscountCode.Length > 0)
                {
                    pnlPromotionLabel.Visible = true;
                    pnlPromotionalAmount.Visible = true;

                    lblPromotionPrice.Text = String.Format("(${0:0.00})", orderData.DiscountAmount);
                }



                LiteralName.Text = String.Format("{0} {1}", orderData.CustomerInfo.ShippingAddress.FirstName, orderData.CustomerInfo.ShippingAddress.LastName);
                LiteralEmail.Text = orderData.CustomerInfo.Email;
                LiteralAddress.Text = orderData.CustomerInfo.ShippingAddress.Address1;
                LiteralAddress2.Text = orderData.CustomerInfo.ShippingAddress.Address2;
                if (LiteralAddress2.Text.Equals(""))
                {
                    pnlSAddress2.Visible = false;
                }
                else
                {
                    pnlSAddress2.Visible = true;
                }
                LiteralCity.Text = orderData.CustomerInfo.ShippingAddress.City;
                LiteralZip.Text = orderData.CustomerInfo.ShippingAddress.ZipPostalCode;
                LiteralState.Text = StateManager.GetStateName(orderData.CustomerInfo.ShippingAddress.StateProvinceId);
                LiteralCountry.Text = CountryManager.CountryName(orderData.CustomerInfo.ShippingAddress.CountryId);

                LiteralName_b.Text = String.Format("{0} {1}", orderData.CustomerInfo.BillingAddress.FirstName, orderData.CustomerInfo.BillingAddress.LastName);
                LiteralAddress_b.Text = orderData.CustomerInfo.BillingAddress.Address1;
                LiteralAddress2_b.Text = orderData.CustomerInfo.BillingAddress.Address2;
                if (LiteralAddress2_b.Text.Equals(""))
                {
                    pnlBAddress2.Visible = false;
                }
                else
                {
                    pnlBAddress2.Visible = true;
                }
                LiteralCity_b.Text = orderData.CustomerInfo.BillingAddress.City;
                LiteralZip_b.Text = orderData.CustomerInfo.BillingAddress.ZipPostalCode;
                LiteralState_b.Text = StateManager.GetStateName(orderData.CustomerInfo.BillingAddress.StateProvinceId);
                LiteralCountry_b.Text = CountryManager.CountryName(orderData.CustomerInfo.BillingAddress.CountryId);
                LiteralPhone.Text = orderData.CustomerInfo.BillingAddress.PhoneNumber;
                //Google Analutics E-Commerce Pixel
                //LoadGoogleAnalytics(orderData);


            }

        }
        public void LoadGoogleAnalytics(Order Order)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<script>");
                sb.AppendLine("var pageTracker = _gat._getTracker('UA-10581943-59');");
                sb.AppendLine("pageTracker._trackPageview();");
                sb.AppendFormat("pageTracker._addTrans('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' );\n", Order.OrderId, "", Order.Total, Math.Round(Order.Tax, 2), Math.Round(Order.ShippingCost, 2), Order.CustomerInfo.BillingAddress.City, Order.CustomerInfo.BillingAddress.StateProvinceId, CountryManager.CountryName(Order.CustomerInfo.BillingAddress.CountryId));


                foreach (Sku sku in Order.SkuItems)
                {
                    sb.AppendFormat("pageTracker._addItem('{0}','{1}','{2}','{3}','{4}','{5}');\n", Order.OrderId, sku.SkuCode, sku.Title, "", Math.Round(Convert.ToDouble(sku.InitialPrice), 2), sku.Quantity);
                }
                sb.AppendLine("pageTracker._trackTrans();");
                sb.AppendLine("</script>");
                LiteralGoogleAnalytics.Text = sb.ToString();
            }
            catch (Exception) { }
        }
        public string GetOrderId()
        {
            if (CartContext != null)
            {
                return "VA" + CartContext.OrderId;
            }
            else
            {
                if (Request.Params["oId"] != null)
                    return "VA" + Request.Params["oId"];
            }
            return "";
        }


    }
}
