using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSBusiness.Preference;
using CSCore.Utils;
using CSCore.DataHelper;
using CSWeb.Mobile.Store;
using System.Web;
using CSBusiness.Resolver;
using CSBusiness.CreditCard;
using System.Web.UI.WebControls;
using CSBusiness.Payment;
using CSBusiness.OrderManagement;

namespace CSWeb.Mobile.UserControls
{

    public partial class BillingShippingCreditForm : System.Web.UI.UserControl
    {
        #region Variable and Events Declaration
        bool _bError = false;
        public string RedirectUrl
        {
            get
            {
                return (string)(ViewState["RedirectUrl"] ?? String.Empty);
            }
            set
            {
                ViewState["RedirectUrl"] = value;
            }
        }

        public ClientCartContext ClientOrderData
        {
            get
            {
                if (Session["ClientOrderData"] == null)
                    return new ClientCartContext();
                return (ClientCartContext)Session["ClientOrderData"];
            }
            set
            {
                Session["ClientOrderData"] = value;
            }
        }
        #endregion Variable and Events Declaration

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(upBillingForm, typeof(string), "ShowPopup", "MM_showHideLayers('mask','','hide');", true); 
            if (!IsPostBack)
            {
                txtFirstName.Focus();
                rfvFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvAddress1.ErrorMessage = ResourceHelper.GetResoureValue("BillingAddress1ErrorMsg");
                rfvCity.ErrorMessage = ResourceHelper.GetResoureValue("BillingCityErrorMsg");
                rfvZipCode.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");
                rfvEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailErrorMsg");
                revEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailValidationErrorMsg");
                rfvPhoneNumber.ErrorMessage = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");

                rfvShippingFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvShippingLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvShippingAddress1.ErrorMessage = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                rfvShippingCity.ErrorMessage = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                rfvShippingZipCode.ErrorMessage = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");


                //rfvCreditCard.ErrorMessage = ResourceHelper.GetResoureValue("CCErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                //rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
            }

            if (!IsPostBack)
            {
                ////BindCountries(true);
                ////BindShippingCountries(true);
                BindRegions();
                BindShippingRegions();
                BindCreditCard();
                BindControls();
                PopulateExpiryYear();
                LoadOfferTerms(ClientOrderData.CartInfo.CartItems[0]);
                
            }
            

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            holderTaxAndShipping.Visible = TotalMode == ShoppingCartDisplayTotalMode.Full;
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            //String.Format(@"$(function() {{$('#{0}, #{1}, #{2}').autotab_magic().autotab_filter('numeric')}});",
            //        txtPhoneNumber1.ClientID), true);

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            //String.Format(@"$(function() {{$('#{0}, #{1}, #{2},#{3}').autotab_magic().autotab_filter('numeric')}});",
            //        txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab1" + this.ClientID,
            //    String.Format(@"$(function() {{$('#{0}, #{1}, #{2}, #{3}').autotab_magic().autotab_filter('numeric')}});",
            //        txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);

        }

        #endregion Page Events

        #region General Methods
        public int rId = 1;

        public void PopulateExpiryYear()
        {
            //Populate the credit card expiration month drop down 
            ddlExpMonth.Items.Add(new ListItem("Exp. Month", ""));
            ddlExpYear.Items.Add(new ListItem("Exp. Year", ""));

            for (int i = 1; i <= 12; i++)
            {
                DateTime month = new DateTime(2000, i, 1);
                ListItem li = new ListItem(month.ToString("MM"), month.ToString("MM"));
                ddlExpMonth.Items.Add(li);
            }
            //DropDownListExpMonth.SelectedValue = DateTime.Now.ToString("MM");
           
            ddlExpMonth.Items[0].Selected = true;



            //Populate the credit card expiration year drop down (go out 12 years)  
            for (int i = 0; i <= 11; i++)
            {
                String year = (DateTime.Today.Year + i).ToString();
                ListItem li = new ListItem(year, year);
                ddlExpYear.Items.Add(li);
            }
            ddlExpYear.Items[0].Selected = true;
        }

        /// <summary>
        /// List of Country from Cache Data
        /// </summary>
        //////public void BindCountries(bool setValue)
        //////{

        //////    ddlCountry.DataSource = CountryManager.GetActiveCountry();
        //////    ddlCountry.DataBind();
        //////    if (setValue)
        //////        ddlCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;

        //////}

        //////public void BindShippingCountries(bool setValue)
        //////{

        //////    ddlShippingCountry.DataSource = CountryManager.GetActiveCountry();
        //////    ddlShippingCountry.DataBind();
        //////    if (setValue)
        //////        ddlShippingCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;
        //////}

        private void LoadOfferTerms(Sku sku)
        {
            sku.LoadAttributeValues();
            if (sku.ContainsAttribute("offerterms")
                && sku.AttributeValues["offerterms"] != null)
                ltOfferTerms.Text = sku.AttributeValues["offerterms"].Value;
        }


        /// <summary>
        /// List of States from Cache Data
        /// </summary>
        private void BindRegions()
        {

            ddlState.Items.Clear();
            int countryId = 231;//USA   Convert.ToInt32(ddlCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlState.DataSource = list;
            ddlState.DataValueField = "StateProvinceId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("- Select -", string.Empty));

        }

        private void BindCreditCard()
        {
            ddlCCType.Items.Clear();
            ddlCCType.DataSource = CommonHelper.BindToEnum(typeof(CreditCardTypeEnum));
            ddlCCType.DataTextField = "Key";
            ddlCCType.DataValueField = "Value";
            ddlCCType.DataBind();
            ddlCCType.Items.Insert(0, new ListItem("- Select -", string.Empty));

        }
        private void BindShippingRegions()
        {

            ddlShippingState.Items.Clear();
            int countryId = 231;//USA  Convert.ToInt32(ddlShippingCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlShippingState.DataSource = list;
            ddlShippingState.DataValueField = "StateProvinceId";
            ddlShippingState.DataBind();
        }


        protected void Country_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindRegions();
        }

        protected void ShippingCountry_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindShippingRegions();
        }
        public bool validateInput()
        {


            if (CommonHelper.EnsureNotNull(txtFirstName.Text) == String.Empty)
            {
                lblFirstNameError.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                lblFirstNameError.Visible = true;
                _bError = true;
            }
            else
                lblFirstNameError.Visible = false;

            if (CommonHelper.EnsureNotNull(txtLastName.Text) == String.Empty)
            {
                lblLastNameError.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                lblLastNameError.Visible = true;
                _bError = true;

            }
            else
                lblLastNameError.Visible = false;

            if (CommonHelper.EnsureNotNull(txtAddress1.Text) == String.Empty)
            {
                lblAddress1Error.Text = ResourceHelper.GetResoureValue("Address1ErrorMsg");
                lblAddress1Error.Visible = true;
                _bError = true;
            }
            else
                lblAddress1Error.Visible = false;

            if (CommonHelper.EnsureNotNull(txtCity.Text) == String.Empty)
            {
                lblCityError.Text = ResourceHelper.GetResoureValue("CityErrorMsg");
                lblCityError.Visible = true;
                _bError = true;
            }
            else
                lblCityError.Visible = false;


            if (ddlState.SelectedValue.Equals(""))
            {
                lblStateError.Text = ResourceHelper.GetResoureValue("StateErrorMsg");
                lblStateError.Visible = true;
                _bError = true;
            }
            else
                lblStateError.Visible = false;

            //string strPhoneNum = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
            string strPhoneNum = txtPhoneNumber1.Text;

            if (!CommonHelper.IsValidPhone(strPhoneNum))
            {
                lblPhoneNumberError.Text = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");
                lblPhoneNumberError.Visible = true;
                _bError = true;
            }
            else
                lblPhoneNumberError.Visible = false;

            if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
            {
                lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeErrorMsg");
                lblZiPError.Visible = true;
                _bError = true;
            }
            else
            {
                if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
                {
                    lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeValidationErrorMsg");
                    lblZiPError.Visible = true;
                    _bError = true;

                }
                else
                    lblZiPError.Visible = false;

            }

            if (CommonHelper.EnsureNotNull(txtEmail.Text) == String.Empty)
            {
                lblEmailError.Text = ResourceHelper.GetResoureValue("EmailErrorMsg");
                lblEmailError.Visible = true;
                _bError = true;
            }
            else
            {
                if (!CommonHelper.IsValidEmail(txtEmail.Text))
                {
                    lblEmailError.Text = ResourceHelper.GetResoureValue("EmailValidationErrorMsg");
                    lblEmailError.Visible = true;
                    _bError = true;
                }
                else
                    lblEmailError.Visible = false;
            }

            #region Name & Address

            if (pnlShippingAddress.Visible)
            {
                if (CommonHelper.EnsureNotNull(txtShippingFirstName.Text) == String.Empty)
                {
                    lblShippingFirstName.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                    lblShippingFirstName.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingFirstName.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingLastName.Text) == String.Empty)
                {
                    lblShippingLastName.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                    lblShippingLastName.Visible = true;
                    _bError = true;

                }
                else
                    lblShippingLastName.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingAddress1.Text) == String.Empty)
                {
                    lblShippingAddress1Error.Text = ResourceHelper.GetResoureValue("Address1ErrorMsg");
                    lblShippingAddress1Error.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingAddress1Error.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingCity.Text) == String.Empty)
                {
                    lblShippingCityError.Text = ResourceHelper.GetResoureValue("CityErrorMsg");
                    lblShippingCityError.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingCityError.Visible = false;


                if (ddlShippingState.SelectedValue.Equals(""))
                {
                    lblShippingStateError.Text = ResourceHelper.GetResoureValue("StateErrorMsg");
                    lblShippingStateError.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingStateError.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingZipCode.Text) == String.Empty)
                {
                    lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeErrorMsg");
                    lblShippingZiPError.Visible = true;
                    _bError = true;
                }
                else
                {
                    if (!CommonHelper.IsValidZipCode(txtShippingZipCode.Text))
                    {
                        lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeValidationErrorMsg");
                        lblShippingZiPError.Visible = true;
                        _bError = true;

                    }
                    else
                        lblShippingZiPError.Visible = false;

                }
            }
            #endregion

            #region Credit Card

            if (ddlCCType.SelectedIndex < 0)
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                lblCCType.Visible = true;
                _bError = true;
            }
            else
                lblCCType.Visible = false;

            DateTime expire = new DateTime();
            if (ddlExpYear.SelectedIndex > 0 && ddlExpMonth.SelectedIndex > 0)
            {
                expire = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
            }
            DateTime today = DateTime.Today;
            if (expire.Year <= today.Year && expire.Month <= today.Month)
            {
                lblExpDate.Text = ResourceHelper.GetResoureValue("ExpDateErrorMsg");
                lblExpDate.Visible = true;
                _bError = true;
            }
            else
                lblExpDate.Visible = false;

            string c = ucTokenex.ReceivedToken; 
            if (c.Equals(""))
            {
                lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                lblCCNumberError.Visible = true;
                _bError = true;
            }
            else
                lblCCNumberError.Visible = false;
            //else
            //{
            //    if ((c.ToString() != "4444333322221111"))// && (txtCvv.Text.IndexOf("147114711471") == -1))
            //    {
            //        if (!CommonHelper.ValidateCardNumber(c))
            //        {
            //            lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
            //            lblCCNumberError.Visible = true;
            //            _bError = true;
            //        }
            //        else
            //            lblCCNumberError.Visible = false;
            //    }
            //}

            //if (CommonHelper.EnsureNotNull(txtCvv.Text) == String.Empty)
            //{
            //    lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            //    lblCvvError.Visible = true;
            //    _bError = true;
            //}
            //else
            //{

            //    if (CommonHelper.onlynums(txtCvv.Text) == false)
            //    {
            //        lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            //        lblCvvError.Visible = true;
            //        _bError = true;
            //    }

            //    if ((CommonHelper.CountNums(txtCvv.Text) != 3) && (CommonHelper.CountNums(txtCvv.Text) != 4))
            //    {
            //        lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            //        lblCvvError.Visible = true;
            //        _bError = true;
            //    }
            //    else
            //        lblCvvError.Visible = false;

                //if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
                //{
                //    lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                //    lblCCType.Visible = true;
                //    _bError = true;
                //}
                //else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
                //{
                //    lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                //    lblCCType.Visible = true;
                //    _bError = true;

                //}
                //else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
                //{
                //    lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                //    lblCCType.Visible = true;
                //    _bError = true;

                //}
                //else if ((c[0].ToString() == "3") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.AmericanExpress.ToString()))
                //{
                //    lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                //    lblCCType.Visible = true;
                //    _bError = true;

                //}
                //else
                //{
                //    lblCCType.Visible = false;
                //}

            //}

            #endregion

            return _bError;

        }

        //protected void rblShippingDifferent_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rblShippingDifferent.)
        //    {

        //    }
        //}

        protected void cbShippingSame_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbShippingSame.Checked)
                pnlShippingAddress.Visible = false;
            else
                pnlShippingAddress.Visible = true;
        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
                //Session["PId"] = Convert.ToInt32(ddlSize.SelectedValue);
                Session["OrderStatus"] = "PostSale";
                Response.Redirect(string.Format("PostSale.aspx?CId={0}",
                     Convert.ToString((int)CSBusiness.ShoppingManagement.ShoppingCartType.SingleCheckout)));
            }
        }
        private void SaveAdditionaInfo()
        {
            ClientCartContext contextData = ClientOrderData;

            //contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(contextData.CustomerInfo, contextData);

            ClientOrderData = contextData;
        }
        public void SaveData()
        {
            ClientCartContext clientData = ClientOrderData;
            if (Page.IsValid)
            {

                //Set Customer Information
                Address shippingAddress = new Address();
                shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                shippingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                shippingAddress.Address2 = string.Empty;// CommonHelper.fixquotesAccents(txtAddress2.Text);
                shippingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                shippingAddress.CountryId = 231;//USA   Convert.ToInt32(ddlCountry.SelectedValue);
                shippingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtZipCode.Text);

                Customer CustData = new Customer();
                CustData.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                CustData.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                //CustData.PhoneNumber = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
                CustData.PhoneNumber = txtPhoneNumber1.Text;
                CustData.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.Username = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.ShippingAddress = shippingAddress;
                //CustData.ShippingAddress = billingAddress;

                if (!pnlShippingAddress.Visible)
                {
                    CustData.BillingAddress = shippingAddress;
                }
                else
                {
                    Address billingAddress = new Address();
                    billingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                    billingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                    billingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                    billingAddress.Address2 = string.Empty;// CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                    billingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                    billingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                    billingAddress.CountryId = 231;//USA   Convert.ToInt32(ddlShippingCountry.SelectedValue);
                    billingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtShippingZipCode.Text);

                    CustData.BillingAddress = billingAddress;
                }


                PaymentInformation paymentDataInfo = new PaymentInformation();
                string CardNumber = ucTokenex.ReceivedToken;
                paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber); 
                paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
                paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
                paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
                paymentDataInfo.CreditCardCSC = "";//txtCvv.Text;

                clientData.CustomerInfo = CustData;
                clientData.PaymentInfo = paymentDataInfo;
                ClientOrderData = clientData;

                //Save Order information before upsale process

                int orderId = 0;
                    if (rId == 1)
                        orderId = CSResolve.Resolve<IOrderService>().SaveOrder(clientData);
                    else
                    {
                        //update order with modified customer shipping and billing and credit card information
                        orderId = clientData.OrderId;
                        CSResolve.Resolve<IOrderService>().UpdateOrder(orderId, clientData);
                    }

                    //if (orderId > 1)
                    //{
                    //    clientData.OrderId = orderId;
                    //    Session["ClientOrderData"] = clientData;

                    //    if (rId == 1)
                    //        Response.Redirect("PostSale.aspx");
                    //    else
                    //        Response.Redirect("CardDecline.aspx");
                    //}

                //Set the Client Order objects
                ClientCartContext contextData = (ClientCartContext)Session["ClientOrderData"];
                contextData.OrderId = orderId;
                contextData.CustomerInfo = CustData;
                contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(CustData, contextData);
                Session["ClientOrderData"] = contextData;
            }
        }

        #endregion General Methods

        #region ShoppingCart

        public void BindControls()
        {
            if (CartContext.CartInfo.CartItems.Count > 0)
            {
                rptShoppingCart.DataSource = CartContext.CartInfo.CartItems.FindAll(x => x.Visible == true);
                rptShoppingCart.DataBind();

                pnlTotal.Visible = true;

                lblSubtotal.Text = String.Format("${0:0.00}", CartContext.CartInfo.SubTotal);
                lblTax.Text = String.Format("${0:0.00}", CartContext.CartInfo.TaxCost);
                lblShipping.Text = String.Format("${0:0.00}", CartContext.CartInfo.ShippingCost);
                //lblRushShipping.Text = String.Format("${0:0.00}", CartContext.CartInfo.RushShippingCost);
                lblOrderTotal.Text = String.Format("${0:0.00}", CartContext.CartInfo.Total);
                LoadUpgradeSku();
                //Sri Comments on 11/15: Need to Plug-in to Custom Shipping option Model
                SitePreference shippingGetShippingPref = CSFactory.GetCacheSitePref();
                //holderRushShipping.Visible = shippingGetShippingPref.IncludeRushShipping ?? false;
                //holderRushShippingTotal.Visible = chkIncludeRushShipping.Checked = (CartContext.CartInfo.ShippingMethod == UserShippingMethodType.Rush);
            }
            else
            {
                pnlTotal.Visible = false;
                rptShoppingCart.Visible = false;
            }

        }

        private void LoadUpgradeSku()
        {
            int upgradeSkuId = 0;
            int skuId = 0;
            foreach (Sku sku in CartContext.CartInfo.CartItems)
            {
                skuId = sku.SkuId;
                //if (sku.SkuId == 112)
                //    upgradeSkuId = 114;
                if (sku.SkuId == 113)
                    upgradeSkuId = 115;
                break;
            }
            if (upgradeSkuId > 0)
            {
                SkuManager skuManager = new SkuManager();
                Sku sku = skuManager.GetSkuByID(skuId);
                sku.LoadAttributeValues();
                if (sku.ContainsAttribute("upgradetext") && sku.AttributeValues["upgradetext"] != null)
                    lblUpgrade.Text = sku.AttributeValues["upgradetext"].Value;

                imgUpgrade.CommandArgument = upgradeSkuId.ToString();
                pnlUpgrade.Visible = true;
            }
        }

        protected void rptShoppingCart_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSkuCode = e.Item.FindControl("lblSkuCode") as Label;
                Label lblSkuDescription = e.Item.FindControl("lblSkuDescription") as Label;
                TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                //Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;
                Label lblSkuInitialPrice = e.Item.FindControl("lblSkuInitialPrice") as Label;
                ImageButton btnRemoveItem = e.Item.FindControl("btnRemoveItem") as ImageButton;
                HtmlContainerControl holderQuantity = e.Item.FindControl("holderQuantity") as HtmlContainerControl;
                HtmlContainerControl holderRemove = e.Item.FindControl("holderRemove") as HtmlContainerControl;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;

                Sku cartItem = e.Item.DataItem as Sku;

                lblSkuDescription.Text = cartItem.ShortDescription;
                //lblQuantity.Text = txtQuantity.Text = cartItem.Quantity.ToString();
                lblSkuInitialPrice.Text = String.Format("${0:0.##}", cartItem.InitialPrice);
                if (cartItem.ImagePath != null && cartItem.ImagePath.Length > 0)
                {
                    imgProduct.ImageUrl = cartItem.ImagePath;
                    lblSkuCode.Visible = false;
                }
                else
                {
                    imgProduct.Visible = false;
                    lblSkuCode.Text = cartItem.SkuCode.ToString();
                }


                btnRemoveItem.CommandArgument = cartItem.SkuId.ToString();

                //txtQuantity.Attributes["onchange"] = Page.ClientScript.GetPostBackEventReference(refresh, "");

                switch (QuantityMode)
                {
                    case ShoppingCartQuanityMode.Hidden:
                        holderQuantity.Visible = false;
                        break;
                    //case ShoppingCartQuanityMode.Editable:
                    //    lblQuantity.Visible = false;
                    //    break;
                    //case ShoppingCartQuanityMode.Readonly:
                    //    txtQuantity.Visible = false;
                    //    break;
                    default:
                        break;
                }

                if (HideRemoveButton)
                {
                    holderRemove.Visible = false;
                }
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlContainerControl holderHeaderQuantity = e.Item.FindControl("holderHeaderQuantity") as HtmlContainerControl;
                HtmlContainerControl holderHeaderRemove = e.Item.FindControl("holderHeaderRemove") as HtmlContainerControl;
                if (QuantityMode == ShoppingCartQuanityMode.Hidden)
                {
                    holderHeaderQuantity.Visible = false;
                }

                if (HideRemoveButton)
                {
                    holderHeaderRemove.Visible = false;
                }
            }
        }

        protected void rptShoppingCart_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    {
                        int skuToRemove = Convert.ToInt32(e.CommandArgument);
                        CartContext.CartInfo.UpdateSku(skuToRemove);
                        BindControls();
                        if (UpdateCart != null)
                            UpdateCart(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void OnTextChanged_Changed(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            ImageButton btnRemoveItem = item.FindControl("btnRemoveItem") as ImageButton;

            int skuID = Convert.ToInt32(btnRemoveItem.CommandArgument);
            Sku cartItem = CartContext.CartInfo.CartItems.FirstOrDefault(c => c.SkuId == skuID);
            int newQuantity = 0;
            if (int.TryParse(txtQuantity.Text, out newQuantity))
                cartItem.Quantity = newQuantity;
            CartContext.CartInfo.Compute();
            BindControls();
            if (UpdateCart != null)
                UpdateCart(sender, e);
        }

        protected void imgUpgrade_Command(object sender, CommandEventArgs e)
        {
            int skuId = Convert.ToInt32(e.CommandArgument);
            CartContext.CartInfo.AddOrUpdate(skuId, 1, true, false, false);
            try
            {
                CartContext.CartInfo.RemoveSku(112);
                CartContext.CartInfo.RemoveSku(113);
            }
            catch (Exception)
            {
            }
            CartContext.CartInfo.Compute();
            pnlUpgrade.Visible = false;
            BindControls();
            LoadOfferTerms(CartContext.CartInfo.CartItems[0]);
        }

        public event EventHandler UpdateCart;

        private ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] as ClientCartContext;
            }
        }

        public ShoppingCartDisplayTotalMode TotalMode
        {
            get
            {
                return ViewState["TotalMode"] == null ? ShoppingCartDisplayTotalMode.Full : (ShoppingCartDisplayTotalMode)ViewState["TotalMode"];
            }
            set
            {
                ViewState["TotalMode"] = value;
            }
        }

        public ShoppingCartQuanityMode QuantityMode
        {
            get
            {
                return ViewState["QuantityMode"] == null ? ShoppingCartQuanityMode.Readonly : (ShoppingCartQuanityMode)ViewState["QuantityMode"];
            }
            set
            {
                ViewState["QuantityMode"] = value;
            }
        }

        public bool HideRemoveButton
        {
            get
            {
                return (bool)(ViewState["HideRemoveButton"] ?? false);
            }
            set
            {
                ViewState["HideRemoveButton"] = value;
            }
        }

        #endregion ShoppingCart

    }

    public enum ShoppingCartDisplayTotalMode
    {
        SubtotalOnly,
        Full
    }

    public enum ShoppingCartQuanityMode
    {
        Editable,
        Readonly,
        Hidden
    }
}