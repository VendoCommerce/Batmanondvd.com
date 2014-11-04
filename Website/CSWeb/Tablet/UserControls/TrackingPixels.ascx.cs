using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Attributes;
using CSBusiness.OrderManagement;
using CSBusiness.Resolver;

namespace CSWeb.Tablet.UserControls
{

    
    public partial class TrackingPixels : System.Web.UI.UserControl
    {
        public Order CurrentOrder = null;
        public string versionName = "";
        public string versionNameReferrer = "";
        public string versionNameClientFunction = ""; 
        public decimal cartTotal = 0;
        public ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] != null ? Session["ClientOrderData"] as ClientCartContext : null;
            }
            set { Session["ClientOrderData"] = value; }
        }

        public static string GetRandomNumber()
        {
            Random rnd = new Random();
            return  rnd.Next(1, 999999999).ToString(); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            versionName = CSWeb.OrderHelper.GetVersionName();
            if (versionName == "")
            {
                versionName = "control";
                List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
                CSBusiness.Version item = list.Find(x => x.Title.ToLower() == versionName.ToLower());
                if (item != null)
                    versionName = item.Title.ToUpper();
            }
             string url = Request.Url.AbsolutePath.ToLower();

            try
            {
                if (url.EndsWith("/postsale.aspx"))
                {
                    versionNameClientFunction = "'" + Session["PostSaleLabelName"].ToString() + "'";
                }
            }
            catch 
            {
                
                
            }
             
            //versionNameReferrer = CSWeb.OrderHelper.GetVersionNameByReferrer(CartContext);
            SetHomePagePnl();
            SetHomeAndSubPagesPnl();
            SetCartPagePnl();
            SetAllPagesPnl();
            SetReceiptPagePnl();
        }
        private void WriteGAPixel()
        {
            string start = "ga('ecommerce:addTransaction', {";
            string end = "});\n";
            StringBuilder sbGAPixel = new StringBuilder();
            sbGAPixel.Append(start);
            sbGAPixel.AppendFormat("'id':'{0}','affiliation':'{1}','revenue':'{2}','shipping':'{3}','tax':'{4}'",
               CurrentOrder.OrderId.ToString(), "BatmanOnDvd.com", Math.Round(CurrentOrder.Total, 2).ToString(), Math.Round(CurrentOrder.ShippingCost, 2).ToString(), Math.Round(CurrentOrder.Tax, 2).ToString());
            sbGAPixel.Append(end);
            foreach (Sku sku in CurrentOrder.SkuItems)
            {
                start = "ga('ecommerce:addItem', {";
                end = "});\n";
                sbGAPixel.Append(start);
                sbGAPixel.AppendFormat("'id':'{0}','name':'{1}','sku':'{2}','category':'{3}','price':'{4}','quantity':'{5}'",
                    CurrentOrder.OrderId.ToString(), sku.Title, sku.SkuCode, "",
                    Math.Round(Convert.ToDouble(sku.InitialPrice), 2).ToString(), sku.Quantity.ToString());
                sbGAPixel.Append(end);
            }



            litGAReceiptPixel.Text = sbGAPixel.ToString();
        }



        private void MDGConfirmPixel()
        {
            StringBuilder MDGConfirm = new StringBuilder();

            MDGConfirm.AppendFormat("piwikTracker.setCustomVariable(1, \"OrderID\", \"'{0}'\", \"visit\");\n",
             CurrentOrder.OrderId.ToString());

            MDGConfirm.AppendFormat("piwikTracker.setCustomVariable(2, \"OrderPrice\", \"'{0}'\", \"visit\");\n",
               Math.Round(CurrentOrder.Total, 2));

            litMdgConfirm.Text = MDGConfirm.ToString();
        }
        private void SetHomePagePnl()
        {
            string url = Request.Url.AbsolutePath.ToLower();

            if (url.EndsWith("/index.aspx"))
            {
                pnlHomePage.Visible = true;
            }
            else
            {
                pnlHomePage.Visible = false;
            }
        }
        private void SetCartPagePnl()
        {
            string url = Request.Url.AbsolutePath.ToLower();

            if (url.EndsWith("/cart2.aspx"))
            {
                SetCartListrakPixel();
                pnlCartPages.Visible = true;

            }
            else
            {
                pnlCartPages.Visible = false;
            }
        }
        private void SetAllPagesPnl()
        {
            if (!(Request.RawUrl.ToLower().Contains("checkoutthankyou") || Request.RawUrl.ToLower().Contains("receipt")))
            {
                pnlAllPages.Visible = true;

            }
            else
            {
                pnlAllPages.Visible = false;
            }

        }
        private void SetHomeAndSubPagesPnl()
        {
            if (!(Request.RawUrl.ToLower().Contains("checkoutthankyou") || Request.RawUrl.ToLower().Contains("postsale") || Request.RawUrl.ToLower().Contains("receipt") || Request.RawUrl.ToLower().Contains("cart")))
            {
                pnlHomeAndSubPages.Visible = true;

            }
            else
            {
                pnlHomeAndSubPages.Visible = false;
            }

        }
        private void SetCurrentOrder()
        {
            int orderId = 0;
            if (Session["oid"] != null)
            {
                orderId = Convert.ToInt32(Session["oid"].ToString());
            }
            else if (CartContext != null)
            {
                orderId = CartContext.OrderId;
            }
            CurrentOrder = new OrderManager().GetBatchProcessOrders(orderId);
            CurrentOrder.LoadAttributeValues();
            SetTotalsForAdwardsAndBing(CurrentOrder);

        }

        private void SetReceiptPagePnl()
        {
            if (Request.RawUrl.ToLower().Contains("checkoutthankyou") || Request.RawUrl.ToLower().Contains("receipt"))
            {
                SetCurrentOrder();
                WriteGAPixel();
                //MDGConfirmPixel();
                string[] testCreditCards;

                testCreditCards = ResourceHelper.GetResoureValue("TestCreditCard").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); ;

                foreach (string word in testCreditCards)
                {
                    if (CurrentOrder.CreditInfo.CreditCardNumber.Equals(word)
                        || CurrentOrder.Email.ToLower().Contains("@conversionsystems.com")
                         || CurrentOrder.Email.ToLower().Contains("@test.com"))
                    {
                        pnlReceiptPage.Visible = false;
                    }
                    else
                    {
                        pnlReceiptPage.Visible = true;
                    }
                }
                //SetConversionListrakPixel();
                //SetTotalsForAdwardsAndBing();
                //reset entire Context object
                //this.CartContext.EmptyData();
                //CartContext = null;

            }

        }

        private void SetConversionListrakPixel()
        {
            StringBuilder sbListrakPixel = new StringBuilder();
            sbListrakPixel.AppendLine("<script type=\"text/javascript\">");
            sbListrakPixel.AppendLine("_ltk.Order.SetCustomer('" + CurrentOrder.Email + "', '" + CurrentOrder.CustomerInfo.BillingAddress.FirstName + "', '" + CurrentOrder.CustomerInfo.BillingAddress.LastName + "')");
            sbListrakPixel.AppendLine("_ltk.Order.OrderNumber = '" + CurrentOrder.OrderId + "'");
            sbListrakPixel.AppendLine("_ltk.Order.ItemTotal = '" + Math.Round(CurrentOrder.SubTotal, 2) + "'");
            sbListrakPixel.AppendLine("_ltk.Order.ShippingTotal = '" + Math.Round(CurrentOrder.ShippingCost, 2) + "'");
            sbListrakPixel.AppendLine("_ltk.Order.TaxTotal = '" + Math.Round(CurrentOrder.Tax, 2) + "'");
            sbListrakPixel.AppendLine("_ltk.Order.HandlingTotal = '0.00'");
            sbListrakPixel.AppendLine("_ltk.Order.OrderTotal = '" + Math.Round(CurrentOrder.SubTotal + CurrentOrder.Tax + CurrentOrder.ShippingCost, 2) + "'");

            foreach (Sku sku in CurrentOrder.SkuItems)
            {
                sbListrakPixel.AppendLine("_ltk.Order.AddItem('" + sku.SkuCode + "', " + sku.Quantity + ", '" + Math.Round(sku.InitialPrice, 2) + "');");
            }
            sbListrakPixel.AppendLine("_ltk.Order.Submit();");
            sbListrakPixel.AppendLine("</script>");


            sbListrakPixel.AppendLine("<script type=\"text/javascript\">");
            sbListrakPixel.AppendLine("_ltk.SCA.OrderNumber = '" + CurrentOrder.OrderId + "';"); // required
            sbListrakPixel.AppendLine("_ltk.SCA.SetCustomer('" + CurrentOrder.Email + "', '" + CurrentOrder.CustomerInfo.BillingAddress.FirstName + "', '" + CurrentOrder.CustomerInfo.BillingAddress.LastName + "');"); // required
            sbListrakPixel.AppendLine("_ltk.SCA.Submit();");
            sbListrakPixel.AppendLine("</script>");
            //ltlConversionListrakPixel.Text = sbListrakPixel.ToString();
        }

        private void SetCartListrakPixel()
        {
            StringBuilder sbListrakPixel = new StringBuilder();
            sbListrakPixel.AppendLine("<script type=\"text/javascript\">");
            sbListrakPixel.AppendLine("_ltk.SCA.SetCustomer('" + CartContext.CustomerInfo.Email + "', '" + CartContext.CustomerInfo.FirstName + "', '" + CartContext.CustomerInfo.LastName + "');");
            foreach (Sku s in CartContext.CartInfo.CartItems)
            {
                sbListrakPixel.AppendLine("_ltk.SCA.AddItemWithLinks('" + s.SkuCode + "', " + s.Quantity + ", '" + Math.Round(s.InitialPrice, 2) + "', '" + s.Title + "', '" + s.ImagePath + "', 'index.aspx');"); // one line per item
            }
            sbListrakPixel.AppendLine("_ltk.SCA.Submit();");
            sbListrakPixel.AppendLine("</script>");
            //ltlCartListTrakPixel.Text = sbListrakPixel.ToString();
        }
        private void SetTotalsForAdwardsAndBing(Order order)
        {
            try
            {
                cartTotal = order.SubTotal + order.Tax + order.ShippingCost;
            }
            catch { }
        }

    }
}
