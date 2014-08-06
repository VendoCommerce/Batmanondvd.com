using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness.ShoppingManagement;
using CSBusiness.CustomerManagement;
using CSBusiness.Web;
using System.Web.Script.Serialization;
using CSCore.Utils;

namespace CSWeb
{
    public partial class GetTotals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            
            string token = Request.QueryString["token"];

            AjaxTotalsToken totalsToken = AjaxTotalsHelper.DecryptToken(token);

            if (totalsToken != null && Request.UserHostAddress == totalsToken.userIP 
                && DateTime.Now.CompareTo(DateTime.Parse(totalsToken.expireDateTime)) <= 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string skuId = Request.QueryString["PId"];
                string address1 = Request.Form["a1"];
                string address2 = Request.Form["a2"];
                string city = Request.Form["city"];
                string state = Request.Form["state"];
                string zip = Request.Form["zip"];
                string quantity = Request.Form["quantity"];
                string promo = Request.Form["promo"];

                if (!string.IsNullOrEmpty(skuId))
                {
                    Cart cartObject = new Cart();
                    cartObject.AddItem(Convert.ToInt32(skuId), Convert.ToInt32(quantity), true, false);

                    Address shippingAddress = new Address();
                    shippingAddress.Address1 = address1;
                    shippingAddress.Address2 = address2;
                    shippingAddress.City = city;
                    shippingAddress.StateProvinceId = Convert.ToInt32(state);
                    shippingAddress.ZipPostalCode = zip;

                    cartObject.ShippingAddress = shippingAddress;

                    cartObject.DiscountCode = promo;

                    cartObject.Compute();

                    
                    AjaxTotalsResponse responseObj = new AjaxTotalsResponse()
                    {
                        total = cartObject.Total,
                        shippingHandling = cartObject.ShippingCost,
                        subTotal = cartObject.SubTotal,
                        tax = cartObject.TaxCost,
                        discount = cartObject.DiscountAmount                        
                    };

                    Response.Write(serializer.Serialize(responseObj));
                }
                else
                {
                    Response.Write(serializer.Serialize(new AjaxTotalsResponse() { subTotal = 0, shippingHandling = 0, total = 0, tax = 0 }));
                }
            }

            Response.End();
        }
    }
}