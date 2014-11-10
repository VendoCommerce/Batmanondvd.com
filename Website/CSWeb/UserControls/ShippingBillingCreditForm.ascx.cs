using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSBusiness.Preference;
using CSCore.Utils;
using CSCore.DataHelper;
using CSWeb.Root.Store;
using System.Web;
using CSBusiness.Resolver;
using CSBusiness.CreditCard;
using System.Web.UI.WebControls;
using CSBusiness.Payment;
using CSBusiness.Shipping;
using CSWebBase;
using CSBusiness.OrderManagement;
using CSWeb.App_Code;
using CSBusiness.Attributes;

namespace CSWeb.Root.UserControls
{

    public partial class ShippingBillingCreditForm : System.Web.UI.UserControl
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
                return (ClientCartContext)Session["ClientOrderData"];
            }
            set
            {
                Session["ClientOrderData"] = value;
            }
        }
        #endregion Variable and Events Declaration

        #region Page Events

        protected void Page_Init(object sender, EventArgs e)
        {
            sm1.SupportsPartialRendering = true;
        }

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
                rfvState.ErrorMessage = ResourceHelper.GetResoureValue("BillingStateErrorMsg");
                rfvShippingFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvShippingLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvShippingAddress1.ErrorMessage = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                rfvShippingCity.ErrorMessage = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                rfvShippingZipCode.ErrorMessage = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
                rfvShippingState.ErrorMessage = ResourceHelper.GetResoureValue("ShippingStateErrorMsg");

                rfvCreditCard.ErrorMessage = ResourceHelper.GetResoureValue("CCErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                //rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");

                txtPhoneNumber1.Attributes.Add("onkeyup", "return autoTab(this, 3, event);");
                txtPhoneNumber2.Attributes.Add("onkeyup", "return autoTab(this, 3, event);");
                txtPhoneNumber3.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
            }

            if (!IsPostBack)
            {
                BindRegions();
                BindShippingRegions();
                BindShippingCharges();

                BindCreditCard();
                FillShippingInfo();
                PopulateExpiryYear();
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ////ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            ////ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            //String.Format(@"$(function() {{$('#{0}, #{1}, #{2}').autotab_magic().autotab_filter('numeric')}});",
            //        txtPhoneNumber1.ClientID, txtPhoneNumber2.ClientID, txtPhoneNumber3.ClientID), true);

            

        }

        #endregion Page Events

        #region General Methods

        public void PopulateExpiryYear()
        {
            //Populate the credit card expiration month drop down 
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
        /// Binds the CreditCards.
        /// </summary>
        private void BindCreditCard()
        {
            ddlCCType.Items.Clear();
            ddlCCType.DataSource = CommonHelper.BindToEnum(typeof(CreditCardTypeEnum));
            ddlCCType.DataTextField = "Key";
            ddlCCType.DataValueField = "Value";
            ddlCCType.DataBind();
            ddlCCType.Items.Insert(0, new ListItem("- Select -", string.Empty));

        }

        /// <summary>
        /// List of States from Cache Data
        /// </summary>
        private void BindRegions()
        {

            ddlState.Items.Clear();
            int countryId = CountryManager.CountryId("United States");
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlState.DataSource = list;
            ddlState.DataValueField = "StateProvinceId";
            ddlState.DataBind();

            ddlState.Items.Insert(0, new ListItem("- Select -", string.Empty));
        }


        private void BindShippingRegions()
        {
            ddlShippingState.Items.Clear();
            int countryId = CountryManager.CountryId("United States");//Default country
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlShippingState.DataSource = list;
            ddlShippingState.DataValueField = "StateProvinceId";
            ddlShippingState.DataBind();

            ddlShippingState.Items.Insert(0, new ListItem("- Select -", string.Empty));
        }

        private void BindShippingCharges()
        {
            ddlAdditionShippingCharge.Items.Clear();

            ddlAdditionShippingCharge.DataSource = ShippingManager.GetShippingChargesByPref(1);
            ddlAdditionShippingCharge.DataTextField = "FriendlyLabel";
            ddlAdditionShippingCharge.DataValueField = "Key";
            ddlAdditionShippingCharge.DataBind();

            ddlAdditionShippingCharge.Items.Insert(0, new ListItem("- Select -", string.Empty));
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
                lblShippingStateError.Text = ResourceHelper.GetResoureValue("ShippingStateErrorMsg");
                lblShippingStateError.Visible = true;
                _bError = true;
            }
            else
                lblShippingStateError.Visible = false;

            if (CommonHelper.EnsureNotNull(txtShippingZipCode.Text) == String.Empty)
            {
                lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
                lblShippingZiPError.Visible = true;
                _bError = true;
            }
            else
            {
                    if (!CommonHelper.IsValidZipCode(txtShippingZipCode.Text))
                    {
                        lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
                        lblShippingZiPError.Visible = true;
                        _bError = true;

                    }
                    else
                        lblShippingZiPError.Visible = false;


            }

            //if (CommonHelper.EnsureNotNull(txtShippingZipCode.Text) == String.Empty)
            //{
            //    lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
            //    lblShippingZiPError.Visible = true;
            //    _bError = true;
            //}
            //else
            //{
            //    if (!CommonHelper.IsValidZipCode(txtShippingZipCode.Text))
            //    {
            //        lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
            //        lblShippingZiPError.Visible = true;
            //        _bError = true;

            //    }
            //    else
            //        lblShippingZiPError.Visible = false;

            //}

            string strPhoneNum = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;

            if (!CommonHelper.IsValidPhone(strPhoneNum))
            {
                lblPhoneNumberError.Text = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");
                lblPhoneNumberError.Visible = true;
                _bError = true;
            }
            else
                lblPhoneNumberError.Visible = false;

            

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
            SitePreference sitePrefCache = CSFactory.GetCacheSitePref();

            if (!sitePrefCache.AttributeValuesLoaded)
                sitePrefCache.LoadAttributeValues();

            if (sitePrefCache.GetAttributeValue<bool>("DuplicateOrderCheck", true))
            {
                if (DuplicateOrderDAL.IsDuplicateOrder(txtEmail.Text))
                {
                    lblEmailError.Text = ResourceHelper.GetResoureValue("DuplicateEmailCheck") + "<br /><br />";
                    lblEmailError.Visible = true;
                    _bError = true;
                }
                else
                    lblEmailError.Visible = false;
            }
            if (pnlQuantity.Visible)
            {
                if (ddlQuantityList.SelectedValue.Equals("select"))
                {
                    lblQuantityList.Text = ResourceHelper.GetResoureValue("QuantityErrorMsg");
                    lblQuantityList.Visible = true;
                    _bError = true;
                }
                else
                    lblQuantityList.Visible = false;
            }

            #region Name & Address

            if (pnlShippingAddress.Visible)
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
                    lblStateError.Text = ResourceHelper.GetResoureValue("BillingStateErrorMsg");
                    lblStateError.Visible = true;
                    _bError = true;
                }
                else
                    lblStateError.Visible = false;


                if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
                {
                    lblZiPError.Text = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");
                    lblZiPError.Visible = true;
                    _bError = true;
                }
                else
                {
                    if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
                    {
                        lblZiPError.Text = ResourceHelper.GetResoureValue("BillingZipCodeValidationErrorMsg");
                        lblZiPError.Visible = true;
                        _bError = true;

                    }
                    else
                        lblZiPError.Visible = false;
                }
                //if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
                //{
                //    lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeErrorMsg");
                //    lblZiPError.Visible = true;
                //    _bError = true;
                //}
                //else
                //{
                //    if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
                //    {
                //        lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeValidationErrorMsg");
                //        lblZiPError.Visible = true;
                //        _bError = true;

                //    }
                //    else
                //        lblZiPError.Visible = false;

                //}
            
            }


            #endregion

            #region Credit Card

            string c = ucTokenex.ReceivedToken;

            if (c.Equals(""))
            {
                lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                lblCCNumberError.Visible = true;
                txtCCNumber1.Text = string.Empty;
                _bError = true;
                return _bError;
            }
            else
                lblCCNumberError.Visible = false;

            //if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
            //{
            //    ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.MasterCard).ToString();
            //}
            //else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
            //{

            //    ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.VISA).ToString();
            //}
            //else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
            //{

            //    ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.Discover).ToString();
            //}
            //else
            //{

            //}

            if (ddlCCType.SelectedIndex < 1)
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                lblCCType.Visible = true;
                _bError = true;
            }
            else
                lblCCType.Visible = false;

            DateTime expire = new DateTime();
            if (ddlExpYear.SelectedIndex > -1 && ddlExpMonth.SelectedIndex > -1)
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

            if (c.Equals(""))
            {
                lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                lblCCNumberError.Visible = true;
                _bError = true;
            }
            else
            {
                if (ucTokenex.EncryptedCcNum.Length == 0)
                {
                    //    if ((c.ToString() != "4444333322221111") && (txtCvv.Text.IndexOf("147114711471") == -1))
                    //    {
                    lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                    lblCCNumberError.Visible = true;
                    _bError = true;
                }
                else
                    lblCCNumberError.Visible = false;
            }

            ////////////////////////if (CommonHelper.EnsureNotNull(txtCvv.Text) == String.Empty)
            ////////////////////////{
            ////////////////////////    lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            ////////////////////////    lblCvvError.Visible = true;
            ////////////////////////    _bError = true;
            ////////////////////////}
            ////////////////////////else
            ////////////////////////{

            ////////////////////////    if (CommonHelper.onlynums(txtCvv.Text) == false)
            ////////////////////////    {
            ////////////////////////        lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            ////////////////////////        lblCvvError.Visible = true;
            ////////////////////////        _bError = true;
            ////////////////////////    }

            ////////////////////////    if ((CommonHelper.CountNums(txtCvv.Text) != 3) && (CommonHelper.CountNums(txtCvv.Text) != 4))
            ////////////////////////    {
            ////////////////////////        lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
            ////////////////////////        lblCvvError.Visible = true;
            ////////////////////////        _bError = true;
            ////////////////////////    }
            ////////////////////////    else
            ////////////////////////        lblCvvError.Visible = false;

            ////////////////////////    if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
            ////////////////////////    {
            ////////////////////////        lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
            ////////////////////////        lblCCType.Visible = true;
            ////////////////////////        _bError = true;
            ////////////////////////    }
            ////////////////////////    else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
            ////////////////////////    {
            ////////////////////////        lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
            ////////////////////////        lblCCType.Visible = true;
            ////////////////////////        _bError = true;

            ////////////////////////    }
            ////////////////////////    else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
            ////////////////////////    {
            ////////////////////////        lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
            ////////////////////////        lblCCType.Visible = true;
            ////////////////////////        _bError = true;

            ////////////////////////    }
            ////////////////////////    else if ((c[0].ToString() == "3") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.AmericanExpress.ToString()))
            ////////////////////////    {
            ////////////////////////        lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
            ////////////////////////        lblCCType.Visible = true;
            ////////////////////////        _bError = true;

            ////////////////////////    }
            ////////////////////////    else
            ////////////////////////    {
            ////////////////////////        lblCCType.Visible = false;
            ////////////////////////    }

            ////////////////////////}

            #endregion

            return _bError;

        }
        
        protected void cbShippingSame_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbShippingSame.Checked)
                pnlShippingAddress.Visible = true;
            else
                pnlShippingAddress.Visible = false;
        }

        protected void FillShippingInfo()
        {
            try
            {
                ClientCartContext contextData = ClientOrderData;
                if (contextData != null && contextData.CustomerInfo != null)
                {

                    if (contextData.CustomerInfo.ShippingAddress != null)
                    {
                        txtShippingFirstName.Text = contextData.CustomerInfo.ShippingAddress.FirstName;
                        txtShippingLastName.Text = contextData.CustomerInfo.ShippingAddress.LastName;
                        txtShippingAddress1.Text = contextData.CustomerInfo.ShippingAddress.Address1;
                        txtShippingCity.Text = contextData.CustomerInfo.ShippingAddress.City;
                        txtShippingZipCode.Text = contextData.CustomerInfo.ShippingAddress.ZipPostalCode;
                        BindShippingRegions();
                        ddlShippingState.SelectedValue = contextData.CustomerInfo.ShippingAddress.StateProvinceId.ToString();
                        txtEmail.Text = contextData.CustomerInfo.Email;
                        txtPhoneNumber1.Text = contextData.CustomerInfo.PhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim().Substring(0, 3);
                        txtPhoneNumber2.Text = contextData.CustomerInfo.PhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim().Substring(3, 3);
                        txtPhoneNumber3.Text = contextData.CustomerInfo.PhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim().Substring(6, 4);


                    }
                }
            }
            catch
            {
                
                
            }
            

        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
                //SaveAdditionaInfo();
                //int qId = 1;
                //Session["PId"] = 30;
                //Response.Redirect(string.Format("AddProduct.aspx?CId={1}",
                //    Convert.ToString((int)CSBusiness.ShoppingManagement.ShoppingCartType.SingleCheckout)));
                Session["OrderStatus"] = "PostSale";
                Response.Redirect(string.Format("Postsale.aspx"));
            }
            //else
            //    txtAddress1.Focus();
                //imgBtn.Focus();
               // ScriptManager.RegisterStartupScript(pnlShippingAddress, this.GetType(), "scrolltobottom", "window.scrollTo(0,document.body.scrollHeight);", true);


        }
        private void SaveAdditionaInfo()
        {
            //ClientCartContext contextData = ClientOrderData;
            //contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(contextData.CustomerInfo, contextData);
            //ClientOrderData = contextData;
        }

        public void SaveData()
        {
            ClientCartContext clientData = ClientOrderData;
            if (Page.IsValid)
            {
                Customer CustData = new Customer();

                //Set Customer Information
                Address shippingAddress = new Address();
                shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                shippingAddress.Address2 = string.Empty;
                shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                shippingAddress.CountryId = CountryManager.CountryId("United States");
                shippingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtShippingZipCode.Text);

                CustData.ShippingAddress = shippingAddress;





                CustData.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                CustData.LastName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                CustData.PhoneNumber = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
                CustData.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.Username = CommonHelper.fixquotesAccents(txtEmail.Text);
                
                //CustData.ShippingAddress = billingAddress;

                if (!pnlShippingAddress.Visible)
                {
                    CustData.BillingAddress = shippingAddress;
                }
                else
                {
                    Address billingAddress = new Address();
                    billingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                    billingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                    billingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                    billingAddress.Address2 = string.Empty;
                    billingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                    billingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                    billingAddress.CountryId = CountryManager.CountryId("United States");
                    billingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtZipCode.Text);
                    CustData.BillingAddress = billingAddress;
                }

                

                PaymentInformation paymentDataInfo = new PaymentInformation();
                string CardNumber = ucTokenex.ReceivedToken;
                paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
                paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
                paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
                paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
                paymentDataInfo.CreditCardCSC = CommonHelper.Encrypt( txtCvv.Text);

                clientData.PaymentInfo = paymentDataInfo;

                // add rush shipping level to cart object
                if (!string.IsNullOrEmpty(ddlAdditionShippingCharge.SelectedValue))
                {                    
                    clientData.CartInfo.ShippingChargeKey = ddlAdditionShippingCharge.SelectedValue;
                }

                //Save opt-in value in order
                clientData.OrderAttributeValues.AddOrUpdateAttributeValue("SpecialOffersOptIn", new CSBusiness.Attributes.AttributeValue(chkOptIn.Checked)); 

                ClientOrderData = clientData;

                //Set the Client Order objects
                ClientCartContext contextData = (ClientCartContext)Session["ClientOrderData"];
                contextData.CustomerInfo = CustData;
                ////////contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(CustData, contextData);
                Session["ClientOrderData"] = contextData;
                //Save Order information before upsale process
                int orderId = 0;

                //if (rId == 1)
                contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(CustData, contextData);
                orderId = CSResolve.Resolve<IOrderService>().SaveOrder(clientData);
                UserSessions.InsertSessionEntry(Context, true, clientData.CartInfo.Total, clientData.CustomerInfo.CustomerId, orderId);

                //else
                //{
                //    //update order with modified customer shipping and billing and credit card information
                //    orderId = clientData.OrderId;
                //    CSResolve.Resolve<IOrderService>().UpdateOrder(orderId, clientData);
                //}

                if (orderId > 1)
                {
                    clientData.OrderId = orderId;
                    Session["ClientOrderData"] = clientData;

                    //if (rId == 1)
                    //    Response.Redirect("PostSale.aspx");
                    //else
                        //Response.Redirect("Postsale.aspx");
                }

            }
        }
        
        #endregion General Methods

    }
}