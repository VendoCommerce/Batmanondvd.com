using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Attributes;
using CSBusiness.CreditCard;
using CSBusiness.CustomerManagement;
using CSBusiness.OrderManagement;
using CSBusiness.Payment;
using CSBusiness.Preference;
using CSBusiness.Resolver;
using CSCore.Utils;
using CSCore.DataHelper;
using CSBusiness.ShoppingManagement;
using System.Web;
using CSWebBase;


namespace CSWeb.Mobile.UserControls
{
    public partial class BillingCreditForm : System.Web.UI.UserControl
    {

        #region Variable and Events Declaration
        // public event EventHandler UpdateShipping;
        bool _bError = false;
        public int rId = 1;
        private ClientCartContext CartContext
        {
            get
            {
                return Session["ClientOrderData"] as ClientCartContext;
            }
        }
        #endregion Variable and Events Declaration

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (Request.Params["rId"] != null)
                rId = Convert.ToInt32(Request.Params["rId"]);

            if (!IsPostBack)
            {
                txtFirstName.Focus();
                rfvFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvAddress1.ErrorMessage = ResourceHelper.GetResoureValue("BillingAddress1ErrorMsg");
                rfvCity.ErrorMessage = ResourceHelper.GetResoureValue("BillingCityErrorMsg");
                rfvZipCode.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");
                rfvState.ErrorMessage = ResourceHelper.GetResoureValue("BillingStateErrorMsg");

                //rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                RfvCCNumberError.ErrorMessage = ResourceHelper.GetResoureValue("CCErrorMsg");
                txtCCNumber1.Attributes.Add("onkeyup", "return autoTab(this, 16, event);");
                
                PopulateExpiryYear();

                if (rId == 0)
                {
                    //ReloadCartData();
                }
                else
                {
                    BindCreditCard();
                    BindCountries(true);
                    BindRegions();
                    
                }

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));
            



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



        protected void Country_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindRegions();
            BindCartSummary();

        }

        protected void State_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindCartSummary();
        }

        public void BindCartSummary()
        {
            if (CartContext.CartInfo.ItemCount > 0)
            {
                if (ddlState.SelectedValue != "")
                    CartContext.CustomerInfo.BillingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                else
                    CartContext.CustomerInfo.BillingAddress.StateProvinceId = 0;
                CartContext.CustomerInfo.BillingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                CartContext.CartInfo.Compute();

                //if (UpdateShipping != null)
                //{
                //    UpdateShipping(this, new EventArgs());
                //}
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
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
        /// Binds the regions.
        /// </summary>

        public void BindCountries(bool setValue)
        {
            ddlCountry.DataSource = CountryManager.GetActiveCountry();
            ddlCountry.DataBind();
            if (setValue)
                ddlCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;
        }

        /// <summary>
        /// Binds the regions.
        /// </summary>
        private void BindRegions()
        {
            ddlState.Items.Clear();
            int countryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlState.DataSource = list;
            ddlState.DataValueField = "StateProvinceId";
            ddlState.DataBind();

            ddlState.Items.Insert(0, new ListItem("- Select -", string.Empty));
        }



        //public void BindBillingCountries(bool setValue)
        //{
        //    ddlCountry.DataSource = CountryManager.GetActiveCountry();
        //    ddlCountry.DataTextField = "Name";
        //    ddlCountry.DataValueField = "CountryId";
        //    ddlCountry.DataBind();
        //    if (setValue)
        //        ddlCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;

        //}

        //private void BindBillingRegions()
        //{

        //    ddlState.Items.Clear();
        //    int billingCountryId = Convert.ToInt32(ddlCountry.SelectedValue);
        //    ddlState.DataSource = StateManager.GetCacheStates(billingCountryId);
        //    ddlState.DataValueField = "StateProvinceId";
        //    ddlState.DataTextField = "Name";
        //    ddlState.DataBind();

        //}

        protected void cbBillingDifferent_CheckedChanged(object sender, EventArgs e)
        {
            pnlBillingAddress.Visible = !cbBillingDifferent.Checked;
           
                SetBillingAddress();
                BindCartSummary();
           
           
            
        }

        //Sri Comments: User action may vary depends on the scenario
        public void SetBillingAddress()
        {
           
                if (!cbBillingDifferent.Checked)
                {
                    Address billingAddress = new Address();
                    billingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                    billingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                    billingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                    billingAddress.Address2 = CommonHelper.fixquotesAccents(txtAddress2.Text);
                    billingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                    if (ddlState.SelectedValue != "")
                        billingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                    
                    billingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                    billingAddress.ZipPostalCode = txtZipCode.Text;
                    CartContext.CustomerInfo.BillingAddress = billingAddress;
                }
                else
                {
                    CartContext.CartInfo.ShippingAddress = CartContext.CustomerInfo.ShippingAddress;
                    ddlCountry.SelectedValue = CartContext.CartInfo.ShippingAddress.CountryId.ToString();

                    if (CartContext.CartInfo.ShippingAddress.StateProvinceId != 0 &&
                        ddlState.Items.FindByValue(CartContext.CartInfo.ShippingAddress.StateProvinceId.ToString()) != null)
                    {
                        ddlState.SelectedValue = CartContext.CartInfo.ShippingAddress.StateProvinceId.ToString();
                    }
                }
           
            
           
            
        }



        //protected void rblBillingReview_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool billingVal = Convert.ToBoolean(rbBillingReview.SelectedItem.Value);
        //    if (billingVal)
        //        pnlBilling.Visible = true;
        //    else
        //        pnlBilling.Visible = false;
        //}


        //public void ReloadCartData()
        //{
        //    ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];

        //    LabelCreditCardNumberError.Visible=true;
        //    LabelCreditCardNumberError.Text = "Sorry, we were unable to process your Payment Information as entered. Please try placing your order again or call Customer Service at (800) 672-2259.";
        //    rfvFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
        //    rfvLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
        //    rfvAddress1.ErrorMessage = ResourceHelper.GetResoureValue("BillingAddress1ErrorMsg");
        //    rfvCity.ErrorMessage = ResourceHelper.GetResoureValue("BillingCityErrorMsg");
        //    rfvZipCode.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");
        //    rfvZipCodeReg.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeValidationErrorMsg");
        //    rfvPhone.ErrorMessage = ResourceHelper.GetResoureValue("PhoneNumErrorMsg");
        //    rfvEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailErrorMsg");
        //    rfvEmailReg.ErrorMessage = ResourceHelper.GetResoureValue("EmailValidationErrorMsg");

        //    pnlBillingReview.Visible = true;
        //    BindBillingCountries(false);
        //    BindShippingCountries(false);

        //    ddlShippingCountry.Items.FindByValue(clientData.CustomerInfo.ShippingAddress.CountryId.ToString()).Selected = true;
        //    ddlCountry.Items.FindByValue(clientData.CustomerInfo.BillingAddress.CountryId.ToString()).Selected = true;

        //    BindBillingRegions();
        //    BindShippingRegions();

        //    ddlShippingState.Items.FindByValue(clientData.CustomerInfo.ShippingAddress.StateProvinceId.ToString()).Selected = true;
        //    ddlState.Items.FindByValue(clientData.CustomerInfo.BillingAddress.StateProvinceId.ToString()).Selected = true;

        //    //Shipping information
        //    TextBoxShippingFirstName.Text = clientData.CustomerInfo.ShippingAddress.FirstName;
        //    TextBoxShippingLastName.Text = clientData.CustomerInfo.ShippingAddress.LastName;
        //    TextBoxShippingAddress1.Text = clientData.CustomerInfo.ShippingAddress.Address1;
        //    TextBoxShippingAddress2.Text = clientData.CustomerInfo.ShippingAddress.Address2;
        //    TextBoxShippingCity.Text = clientData.CustomerInfo.ShippingAddress.City;
        //    TextBoxShippingZip.Text = clientData.CustomerInfo.ShippingAddress.ZipPostalCode;         


        //    //Payment information
        //    TextBoxCCNum.Text = CommonHelper.Decrypt(clientData.PaymentInfo.CreditCardNumber);
        //    TextBoxCVV.Text = clientData.PaymentInfo.CreditCardCSC;
        //    DateTime expireDate = DateTime.MinValue;
        //    DateTime.TryParse(clientData.PaymentInfo.CreditCardExpired.ToString(), out expireDate);
        //    DropDownListExpMonth.Items.FindByValue(expireDate.Month.ToString()).Selected = true;
        //    DropDownListExpYear.Items.FindByValue(expireDate.Year.ToString()).Selected = true;

        //    //Billing informarion
        //    TextBoxFirstName.Text = clientData.CustomerInfo.BillingAddress.FirstName;
        //    TextBoxLastName.Text = clientData.CustomerInfo.BillingAddress.LastName;
        //    TextBoxAddress1.Text = clientData.CustomerInfo.BillingAddress.Address1;
        //    TextBoxAddress2.Text = clientData.CustomerInfo.BillingAddress.Address2;
        //    TextBoxCity.Text = clientData.CustomerInfo.BillingAddress.City;
        //    TextBoxZip.Text = clientData.CustomerInfo.BillingAddress.ZipPostalCode;
        //    TextBoxEmail.Text = clientData.CustomerInfo.Email;



        //    //quick fix
        //    TextBoxArea.Text = clientData.CustomerInfo.PhoneNumber.Substring(0, 3);
        //    TextBoxPhoneNum1.Text = clientData.CustomerInfo.PhoneNumber.Substring(3, 3);
        //    TextBoxPhoneNum2.Text = clientData.CustomerInfo.PhoneNumber.Substring(6, 4);
        //}

        public bool validateInput()
        {
            //_bError = false;
            if (!cbBillingDifferent.Checked)
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


                if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
                {
                    lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
                    lblZiPError.Visible = true;
                    _bError = true;
                }
                else
                {
                    if (ddlCountry.SelectedItem.Text.Contains("United States") || ddlCountry.SelectedValue.Contains("327") || ddlCountry.SelectedValue.Contains("397") || ddlCountry.SelectedValue.Contains("444"))
                    {
                        if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
                        {
                            lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
                            lblZiPError.Visible = true;
                            _bError = true;

                        }
                        else
                            lblZiPError.Visible = false;

                    }
                    else
                    {
                        if (!CommonHelper.IsValidZipCodeCanadian(txtZipCode.Text))
                        {
                            lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
                            lblZiPError.Visible = true;
                            _bError = true;

                        }
                        else
                            lblZiPError.Visible = false;
                    }

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
            //if (!CheckBoxAgree.Checked)
            //{
            //    lblAgreeError.Text = "Please Agree to the terms and conditions";
            //    lblAgreeError.Visible = true;
            //    _bError = true;

            //}
            //else
            //    lblAgreeError.Visible = false;

            if (ddlCCType.SelectedIndex < 0)
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

            string c = txtCCNumber1.Text;
            if (c.Equals(""))
            {
                lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                lblCCNumberError.Visible = true;
                _bError = true;
            }
            else
            {
                if ((c.ToString() != "4444333322221111") && (txtCvv.Text.IndexOf("147114711471") == -1))
                {
                    if (!CommonHelper.ValidateCardNumber(c))
                    {
                        lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
                        lblCCNumberError.Visible = true;
                        _bError = true;
                    }
                    else
                        lblCCNumberError.Visible = false;
                }
                else
                    lblCCNumberError.Visible = false;


            }

            if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
            {
                ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.MasterCard).ToString();
            }
            else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
            {

                ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.VISA).ToString();
            }
            else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
            {

                ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.Discover).ToString();
            }
            else if ((c[0].ToString() == "3") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.AmericanExpress.ToString()))
            {

                ddlCCType.SelectedValue = ((int)CreditCardTypeEnum.AmericanExpress).ToString();
            }
            else
            {

            }

            if (ddlCCType.SelectedIndex < 0)
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                lblCCType.Visible = true;
                _bError = true;
            }
            else
                lblCCType.Visible = false;


            if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                lblCCType.Visible = true;
                _bError = true;
            }
            else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                lblCCType.Visible = true;
                _bError = true;

            }
            else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                lblCCType.Visible = true;
                _bError = true;

            }
            else if ((c[0].ToString() == "3") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.AmericanExpress.ToString()))
            {
                lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
                lblCCType.Visible = true;
                _bError = true;

            }
            else
            {
                lblCCType.Visible = false;
            }

            SitePreference sitePrefCache = CSFactory.GetCacheSitePref();

            if (!sitePrefCache.AttributeValuesLoaded)
                sitePrefCache.LoadAttributeValues();

            if (sitePrefCache.GetAttributeValue<bool>("DuplicateOrderCheck", true))
            {
                ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
                if (DuplicateOrderDAL.IsDuplicateOrder(clientData.CustomerInfo.Email))
                {
                    lblEmailError.Text = ResourceHelper.GetResoureValue("DuplicateEmailCheck") + "<br /><br />";
                    lblEmailError.Visible = true;
                    _bError = true;
                }
                else
                    lblEmailError.Visible = false;
            }

            if (CommonHelper.EnsureNotNull(txtCvv.Text) == String.Empty)
            {
                lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
                lblCvvError.Visible = true;
                _bError = true;
            }
            else
            {

                if (CommonHelper.onlynums(txtCvv.Text) == false)
                {
                    lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
                    lblCvvError.Visible = true;
                    _bError = true;
                }
                if ((CommonHelper.CountNums(txtCvv.Text) != 3) && (CommonHelper.CountNums(txtCvv.Text) != 4))
                {
                    lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
                    lblCvvError.Visible = true;
                    _bError = true;
                }
                else
                    lblCvvError.Visible = false;

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

            }
            if (_bError)
            {
                lblValidation.Visible = true;
                lblValidation.Text = "Please fix above errors";
            }
            else
            {
                lblValidation.Visible = false;
            }
            return _bError;
        }

        //public bool validateInput()
        //{
        //    if (!cbBillingDifferent.Checked)
        //    {
        //        if (CommonHelper.EnsureNotNull(txtFirstName.Text) == String.Empty)
        //        {
        //            lblFirstNameError.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
        //            lblFirstNameError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblFirstNameError.Visible = false;

        //        if (CommonHelper.EnsureNotNull(txtLastName.Text) == String.Empty)
        //        {
        //            lblLastNameError.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
        //            lblLastNameError.Visible = true;
        //            _bError = true;

        //        }
        //        else
        //            lblLastNameError.Visible = false;

        //        if (CommonHelper.EnsureNotNull(txtAddress1.Text) == String.Empty)
        //        {
        //            lblAddress1Error.Text = ResourceHelper.GetResoureValue("Address1ErrorMsg");
        //            lblAddress1Error.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblAddress1Error.Visible = false;

        //        if (CommonHelper.EnsureNotNull(txtCity.Text) == String.Empty)
        //        {
        //            lblCityError.Text = ResourceHelper.GetResoureValue("CityErrorMsg");
        //            lblCityError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblCityError.Visible = false;


        //        if (ddlState.SelectedValue.Equals(""))
        //        {
        //            lblStateError.Text = ResourceHelper.GetResoureValue("StateErrorMsg");
        //            lblStateError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblStateError.Visible = false;


        //        if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
        //        {
        //            lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
        //            lblZiPError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //        {
        //            if (ddlCountry.SelectedItem.Text.Contains("United States"))
        //            {
        //                if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
        //                {
        //                    lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
        //                    lblZiPError.Visible = true;
        //                    _bError = true;

        //                }
        //                else
        //                    lblZiPError.Visible = false;

        //            }
        //            else
        //            {
        //                if (!CommonHelper.IsValidZipCodeCanadian(txtZipCode.Text))
        //                {
        //                    lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
        //                    lblZiPError.Visible = true;
        //                    _bError = true;

        //                }
        //                else
        //                    lblZiPError.Visible = false;
        //            }

        //        }


                
        //        //if (CommonHelper.EnsureNotNull(txtZipCode.Text) == String.Empty)
        //        //{
        //        //    lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeErrorMsg");
        //        //    lblZiPError.Visible = true;
        //        //    _bError = true;
        //        //}
        //        //else
        //        //{
        //        //    if (!CommonHelper.IsValidZipCode(txtZipCode.Text))
        //        //    {
        //        //        lblZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeValidationErrorMsg");
        //        //        lblZiPError.Visible = true;
        //        //        _bError = true;

        //        //    }
        //        //    else
        //        //        lblZiPError.Visible = false;

        //        //}
        //    }
        //    //if (!CheckBoxAgree.Checked)
        //    //{
        //    //    lblAgreeError.Text = "Please Agree to the terms and conditions";
        //    //    lblAgreeError.Visible = true;
        //    //    _bError = true;

        //    //}
        //    //else
        //    //    lblAgreeError.Visible = false;

        //    if (ddlCCType.SelectedIndex < 0)
        //    {
        //        lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
        //        lblCCType.Visible = true;
        //        _bError = true;
        //    }
        //    else
        //        lblCCType.Visible = false;


        //    DateTime expire = new DateTime();
        //    if (ddlExpYear.SelectedIndex > -1 && ddlExpMonth.SelectedIndex > -1)
        //    {
        //        expire = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
        //    }
        //    DateTime today = DateTime.Today;
        //    if (expire.Year <= today.Year && expire.Month <= today.Month)
        //    {
        //        lblExpDate.Text = ResourceHelper.GetResoureValue("ExpDateErrorMsg");
        //        lblExpDate.Visible = true;
        //        _bError = true;
        //    }
        //    else
        //        lblExpDate.Visible = false;

        //    string c = txtCCNumber1.Text;
        //    if (c.Equals(""))
        //    {
        //        lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
        //        lblCCNumberError.Visible = true;
        //        _bError = true;
        //    }
        //    else
        //    {
        //        if ((c.ToString() != "4444333322221111") && (txtCvv.Text.IndexOf("147114711471") == -1))
        //        {
        //            if (!CommonHelper.ValidateCardNumber(c))
        //            {
        //                lblCCNumberError.Text = ResourceHelper.GetResoureValue("CCErrorMsg");
        //                lblCCNumberError.Visible = true;
        //                _bError = true;
        //            }
        //            else
        //                lblCCNumberError.Visible = false;
        //        }
        //        else
        //            lblCCNumberError.Visible = false;


        //    }

        //    SitePreference sitePrefCache = CSFactory.GetCacheSitePref();

        //    if (!sitePrefCache.AttributeValuesLoaded)
        //        sitePrefCache.LoadAttributeValues();

        //    if (sitePrefCache.GetAttributeValue<bool>("DuplicateOrderCheck", true))
        //    {
        //        ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];
        //        if (DuplicateOrderDAL.IsDuplicateOrder(clientData.CustomerInfo.Email))
        //        {
        //            lblEmailError.Text = ResourceHelper.GetResoureValue("DuplicateEmailCheck") + "<br /><br />";
        //            lblEmailError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblEmailError.Visible = false;
        //    }

        //    if (CommonHelper.EnsureNotNull(txtCvv.Text) == String.Empty)
        //    {
        //        lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
        //        lblCvvError.Visible = true;
        //        _bError = true;
        //    }
        //    else
        //    {

        //        if (CommonHelper.onlynums(txtCvv.Text) == false)
        //        {
        //            lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
        //            lblCvvError.Visible = true;
        //            _bError = true;
        //        }
        //        if ((CommonHelper.CountNums(txtCvv.Text) != 3) && (CommonHelper.CountNums(txtCvv.Text) != 4))
        //        {
        //            lblCvvError.Text = ResourceHelper.GetResoureValue("CVVErrorMsg");
        //            lblCvvError.Visible = true;
        //            _bError = true;
        //        }
        //        else
        //            lblCvvError.Visible = false;

        //        if ((c[0].ToString() == "5") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.MasterCard.ToString()))
        //        {
        //            lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
        //            lblCCType.Visible = true;
        //            _bError = true;
        //        }
        //        else if ((c[0].ToString() == "4") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.VISA.ToString()))
        //        {
        //            lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
        //            lblCCType.Visible = true;
        //            _bError = true;

        //        }
        //        else if ((c[0].ToString() == "6") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.Discover.ToString()))
        //        {
        //            lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
        //            lblCCType.Visible = true;
        //            _bError = true;

        //        }
        //        else if ((c[0].ToString() == "3") && (ddlCCType.SelectedItem.Text.ToString() != CreditCardTypeEnum.AmericanExpress.ToString()))
        //        {
        //            lblCCType.Text = ResourceHelper.GetResoureValue("CCTypeValidationErrorMsg");
        //            lblCCType.Visible = true;
        //            _bError = true;

        //        }
        //        else
        //        {
        //            lblCCType.Visible = false;
        //        }

        //    }
        //    return _bError;
        //}



        public void SaveData()
        {
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];

            // attribute save example            
            //clientData.OrderAttributeValues = new Dictionary<string, CSBusiness.Attributes.AttributeValue>();
            //clientData.OrderAttributeValues.Add("ref_url", new CSBusiness.Attributes.AttributeValue("http://www.google.com"));

            //clientData.OrderAttributeValues = new Dictionary<string, CSBusiness.Attributes.AttributeValue>();
            clientData.OrderAttributeValues.AddOrUpdateAttributeValue("CustomOrderId", new CSBusiness.Attributes.AttributeValue("CS" + CommonHelper.GetRandonOrderNumber(DateTime.Now)));
            //Recapture billing information if the user modified the information
            if (rId == 0)
            {
                if (!cbBillingDifferent.Checked) //override if the user select yes button
                {
                    Address billingAddress = new Address();
                    billingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                    billingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                    billingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                    billingAddress.Address2 = CommonHelper.fixquotesAccents(txtAddress2.Text);
                    billingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                    billingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                    billingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                    billingAddress.ZipPostalCode = txtZipCode.Text;

                    clientData.CustomerInfo.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                    clientData.CustomerInfo.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                    clientData.CustomerInfo.BillingAddress = billingAddress;
                }
            }

            SetBillingAddress();


            PaymentInformation paymentDataInfo = new PaymentInformation();
            string CardNumber = txtCCNumber1.Text;
            paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
            paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
            paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
            paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
            paymentDataInfo.CreditCardCSC = txtCvv.Text;

            CartContext.PaymentInfo = paymentDataInfo;

            //Slight Version manipulation just for dynamic radio versions. - Start
            int tempVersionid = CartContext.VersionId;
            List<CSBusiness.Version> list = (CSFactory.GetCacheSitePref()).VersionItems;
            CSBusiness.Version item = list.Find(x => x.Title.ToLower() == OrderHelper.GetDynamicVersionName().ToLower());
            if (item != null)
                CartContext.VersionId = item.VersionId;
            //Slight Version manipulation just for dynamic radio versions. - End

            int orderId = 0;

            if (CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.InstantOrderProcess
                || CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.EnableReviewOrder
                || CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.EnableUpsellReviewOrder)
            {
                //Save Order information before upsale process
                
                if (rId == 1)
                    orderId = CSResolve.Resolve<IOrderService>().SaveOrder(clientData);
                else
                {
                    //update order with modified customer shipping and billing and credit card information
                    orderId = clientData.OrderId;
                    CSResolve.Resolve<IOrderService>().UpdateOrder(orderId, clientData);
                }

                if (orderId > 1)
                {
                    clientData.OrderId = orderId;
                    Session["ClientOrderData"] = clientData;

                    

                    if (rId == 1)
                        Response.Redirect("PostSale.aspx");
                    else
                        Response.Redirect("CardDecline.aspx");
                }
            }
        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
            }

        }

        #endregion General Methods

    }
}
