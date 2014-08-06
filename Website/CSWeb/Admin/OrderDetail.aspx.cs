using System;
using System.Web;
using System.Web.UI;
using CSBusiness.Resolver;
using CSBusiness.OrderManagement;
using CSBusiness.Cache;
using System.Collections.Generic;
using CSBusiness;
using System.Web.UI.WebControls;
using CSData;
using CSBusiness.CustomerManagement;
using CSCore.Utils;


namespace CSWeb.Admin
{
    public partial class OrderDetail : BasePage
    {
        public int orderId = 0, sourceId=0;
        
        #region Information Properties

        protected string AuthorizationCode { get { return Convert.ToString(ViewState["AuthorizationCode"] ?? string.Empty); } set { ViewState["AuthorizationCode"] = value; } }
        protected string TransactionCode { get { return Convert.ToString(ViewState["TransactionCode"] ?? string.Empty); } set { ViewState["TransactionCode"] = value; } }
        protected string Version { get { return Convert.ToString(ViewState["Version"] ?? string.Empty); } set { ViewState["Version"] = value; } }
        protected string CreditCardName { get { return Convert.ToString(ViewState["CreditCardName"] ?? string.Empty); } set { ViewState["CreditCardName"] = value; } }
        protected string CreditCardExpireDate { get { return Convert.ToString(ViewState["CreditCardExpireDate"] ?? string.Empty); } set { ViewState["CreditCardExpireDate"] = value; } }
        protected string CreditCardCSC { get { return Convert.ToString(ViewState["CreditCardCSC"] ?? string.Empty); } set { ViewState["CreditCardCSC"] = value; } }
        protected string CreditCardLast4 { get { return Convert.ToString(ViewState["CreditCardLast4"] ?? string.Empty); } set { ViewState["CreditCardLast4"] = value; } }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["oId"] != null)
                orderId = Convert.ToInt32(Request["oId"].ToString());

            if (Request["sId"] != null)
                sourceId = Convert.ToInt32(Request["sId"].ToString());

            if (!Page.IsPostBack)
            {
                PopulateDropDowns();

                if (orderId > 0)
                {
                    LoadOrderDetails();
                }

            }

        }

        private void PopulateDropDowns()
        {
            List<CSBusiness.Country> countries = CountryManager.GetAllCountry();

            ddlShippingCountry.DataSource = countries;
            ddlShippingCountry.DataBind();

            ddlBillingCountry.DataSource = countries;
            ddlBillingCountry.DataBind();
        }

        protected void ddlShippingCountry_SelectedIndexChanged(object sender, EventArgs e)
        {   
            SetCountryAndStateDropDown(ddlShippingCountry, ddlShippingState, Convert.ToInt32(ddlShippingCountry.SelectedItem.Value ?? "0"), null);
        }

        protected void ddlBillingCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCountryAndStateDropDown(ddlBillingCountry, ddlBillingState, Convert.ToInt32(ddlBillingCountry.SelectedItem.Value ?? "0"), null);
        }

        private void SetCountryAndStateDropDown(DropDownList ddlCountry, DropDownList ddlState, int countryId, string selectedStateProvinceId)
        {
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(Convert.ToString(countryId)));

            List<StateProvince> states = StateManager.GetAllStates(countryId);
            
            ddlState.DataSource = states;
            ddlState.DataBind();

            if (states != null && states.Count > 0 && !string.IsNullOrEmpty(selectedStateProvinceId))
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(selectedStateProvinceId));
        }


        protected void LoadOrderDetails()
        {
            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId, true);

            dlordersList.DataSource = orderData.SkuItems;
            dlordersList.DataBind();

            LiteralSubTotal.Text = Math.Round(orderData.SubTotal, 2).ToString();
            LiteralShipping.Text = Math.Round(orderData.ShippingCost, 2).ToString();
            LiteralTax.Text = Math.Round(orderData.Tax, 2).ToString();
            LiteralTotal.Text = Math.Round(orderData.Total, 2).ToString();

            // shipping literals
            LiteralName.Text = String.Format("{0} {1}", orderData.CustomerInfo.ShippingAddress.FirstName, orderData.CustomerInfo.ShippingAddress.LastName);
            LiteralEmail.Text = orderData.CustomerInfo.Email;
            LiteralAddress.Text = orderData.CustomerInfo.ShippingAddress.Address1;
            LiteralAddress2.Text = orderData.CustomerInfo.ShippingAddress.Address2;
            LiteralCity.Text = orderData.CustomerInfo.ShippingAddress.City;
            LiteralZip.Text = orderData.CustomerInfo.ShippingAddress.ZipPostalCode;
            LiteralState.Text = StateManager.GetStateName(orderData.CustomerInfo.ShippingAddress.StateProvinceId);
            LiteralCountry.Text = CountryManager.CountryName(orderData.CustomerInfo.ShippingAddress.CountryId);

            // shipping edit controls
            txtShippingFirstName.Text = orderData.CustomerInfo.ShippingAddress.FirstName;
            txtShippingLastName.Text = orderData.CustomerInfo.ShippingAddress.LastName;
            txtShippingAddress.Text = orderData.CustomerInfo.ShippingAddress.Address1;
            txtShippingAddress2.Text = orderData.CustomerInfo.ShippingAddress.Address2;
            txtShippingCity.Text = orderData.CustomerInfo.ShippingAddress.City;
            txtShippingZipCode.Text = orderData.CustomerInfo.ShippingAddress.ZipPostalCode;
            SetCountryAndStateDropDown(ddlShippingCountry, ddlShippingState, orderData.CustomerInfo.ShippingAddress.CountryId, Convert.ToString(orderData.CustomerInfo.ShippingAddress.StateProvinceId));
            
            // billing literals
            LiteralName_b.Text = String.Format("{0} {1}", orderData.CustomerInfo.BillingAddress.FirstName, orderData.CustomerInfo.BillingAddress.LastName);
            LiteralAddress_b.Text = orderData.CustomerInfo.BillingAddress.Address1;
            LiteralAddress2_b.Text = orderData.CustomerInfo.BillingAddress.Address2;
            LiteralCity_b.Text = orderData.CustomerInfo.BillingAddress.City;
            LiteralZip_b.Text = orderData.CustomerInfo.BillingAddress.ZipPostalCode;
            LiteralState_b.Text = StateManager.GetStateName(orderData.CustomerInfo.BillingAddress.StateProvinceId);
            LiteralCountry_b.Text = CountryManager.CountryName(orderData.CustomerInfo.BillingAddress.CountryId);

            // billing edit controls
            txtBillingFirstName.Text = orderData.CustomerInfo.BillingAddress.FirstName;
            txtBillingLastName.Text = orderData.CustomerInfo.BillingAddress.LastName;
            txtBillingAddress.Text = orderData.CustomerInfo.BillingAddress.Address1;
            txtBillingAddress2.Text = orderData.CustomerInfo.BillingAddress.Address2;
            txtBillingCity.Text = orderData.CustomerInfo.BillingAddress.City;
            txtBillingZipCode.Text = orderData.CustomerInfo.BillingAddress.ZipPostalCode;
            SetCountryAndStateDropDown(ddlBillingCountry, ddlBillingState, orderData.CustomerInfo.BillingAddress.CountryId, Convert.ToString(orderData.CustomerInfo.BillingAddress.StateProvinceId));

            txtEmail.Text = orderData.CustomerInfo.Email;

            // payment info 
            AuthorizationCode = orderData.CreditInfo.AuthorizationCode;
            TransactionCode = orderData.CreditInfo.TransactionCode;
            Version = orderData.VersionName;
            CreditCardName = orderData.CreditInfo.CreditCardName;
            CreditCardExpireDate = orderData.CreditInfo.CreditCardExpired.ToString("MM/yyyy");
            CreditCardCSC = orderData.CreditInfo.CreditCardCSC;
            CreditCardLast4 = orderData.CreditInfo.CreditCardNumber.Length > 4 ? orderData.CreditInfo.CreditCardNumber.Substring(orderData.CreditInfo.CreditCardNumber.Length - 4, 4)
                : orderData.CreditInfo.CreditCardNumber;

            ucAttributes.Populate(orderData);
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            //redirect 
            if(sourceId==0)
                Response.Redirect("CustomerOrderDetail.aspx");
            else
                Response.Redirect("OrderList.aspx");
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {                
                Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(orderId);

                orderData.CustomerInfo.ShippingAddress.FirstName = txtShippingFirstName.Text;
                orderData.CustomerInfo.ShippingAddress.LastName = txtShippingLastName.Text;
                orderData.CustomerInfo.ShippingAddress.Address1 = txtShippingAddress.Text;
                orderData.CustomerInfo.ShippingAddress.Address2 = txtShippingAddress2.Text;
                orderData.CustomerInfo.ShippingAddress.City = txtShippingCity.Text;
                orderData.CustomerInfo.ShippingAddress.ZipPostalCode = txtShippingZipCode.Text;
                orderData.CustomerInfo.ShippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedItem.Value);
                orderData.CustomerInfo.ShippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedItem.Value);

                orderData.CustomerInfo.BillingAddress.FirstName = txtBillingFirstName.Text;
                orderData.CustomerInfo.BillingAddress.LastName = txtBillingLastName.Text;
                orderData.CustomerInfo.BillingAddress.Address1 = txtBillingAddress.Text;
                orderData.CustomerInfo.BillingAddress.Address2 = txtBillingAddress2.Text;
                orderData.CustomerInfo.BillingAddress.City = txtBillingCity.Text;
                orderData.CustomerInfo.BillingAddress.ZipPostalCode = txtBillingZipCode.Text;
                orderData.CustomerInfo.BillingAddress.StateProvinceId = Convert.ToInt32(ddlBillingState.SelectedItem.Value);
                orderData.CustomerInfo.BillingAddress.CountryId = Convert.ToInt32(ddlBillingCountry.SelectedItem.Value);

                orderData.CustomerInfo.Email = txtEmail.Text;
                orderData.CustomerInfo.PhoneNumber = orderData.CustomerInfo.BillingAddress.PhoneNumber;
                
                CSResolve.Resolve<ICustomerService>().UpdateCustomer(orderId, orderData.CustomerInfo);

                ucAttributes.SaveAllEnteredAttributeValues(orderData);

                LoadOrderDetails();
            }
            else
                mpeThePopup.Show();
        }

        protected void txtEmail_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = !string.IsNullOrEmpty(e.Value) && CommonHelper.IsValidEmail(e.Value);
        }

        protected void ZipCode_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = !string.IsNullOrEmpty(e.Value) && CommonHelper.IsValidZipCode(e.Value);
        }
    }
}