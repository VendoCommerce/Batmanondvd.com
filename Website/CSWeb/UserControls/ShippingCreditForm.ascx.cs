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


namespace CSWeb.Root.UserControls
{
    public partial class ShippingCreditForm : System.Web.UI.UserControl
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
            if (!CommonHelper.IsHttps(HttpContext.Current))
            {
                if (Request.Url.ToString().Contains("www"))
                {
                    Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("www", "secure")));
                }
                else
                {
                    Response.Redirect((Request.Url.ToString().Replace("http:/", "https:/").Replace("secure.", "").Replace("https://", "https://secure.")));
                }
            }
            if (Request.Params["rId"] != null)
                rId = Convert.ToInt32(Request.Params["rId"]);

            if (!IsPostBack)
            {
                txtShippingFirstName.Focus();
                rfvShippingFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvShippingLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvShippingAddress1.ErrorMessage = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                rfvShippingCity.ErrorMessage = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                rfvShippingZipCode.ErrorMessage = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");

                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");

                txtCCNumber1.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber2.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber3.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber4.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                PopulateExpiryYear();
                if (rId == 0)
                {
                    //ReloadCartData();
                }
                else
                {
                    BindCreditCard();
                    BindShippingCountries(true);
                    BindShippingRegions();
                }

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
               String.Format(@"$(function() {{$('#{0}, #{1}, #{2}, #{3}').autotab_magic().autotab_filter('numeric')}});",
                   txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);



        }
        #endregion Page Events

        #region General Methods
        protected void ShippingCountry_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindCartSummary();

        }

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

        protected void ShippingState_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Address shippingAddress = new Address();
            shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
            shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
            CartContext.CartInfo.ShippingAddress = CartContext.CustomerInfo.ShippingAddress = shippingAddress;

            BindCartSummary();
        }

        public void BindCartSummary()
        {
            if (CartContext.CartInfo.ItemCount > 0)
            {
                if (ddlShippingCountry.SelectedValue != "")
                    CartContext.CustomerInfo.ShippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                else
                    CartContext.CustomerInfo.ShippingAddress.StateProvinceId = 0;
                CartContext.CustomerInfo.ShippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);

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

        public void BindShippingCountries(bool setValue)
        {
            ddlShippingCountry.DataSource = CountryManager.GetActiveCountry();
            ddlShippingCountry.DataBind();
            if (setValue)
                ddlShippingCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;
        }

        /// <summary>
        /// Binds the regions.
        /// </summary>
        private void BindShippingRegions()
        {
            ddlShippingState.Items.Clear();
            int countryId = Convert.ToInt32(ddlShippingCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlShippingState.DataSource = list;
            ddlShippingState.DataValueField = "StateProvinceId";
            ddlShippingState.DataBind();
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

        protected void cbShippingDifferent_CheckedChanged(object sender, EventArgs e)
        {
            bool billingVal = !cbBillingDifferent.Checked;
            if (billingVal)
                pnlShippingAddress.Visible = true;
            else
                pnlShippingAddress.Visible = false;

            SetShippingAddress();
            BindCartSummary();
        }

        //Sri Comments: User action may vary depends on the scenario
        public void SetShippingAddress()
        {
            if (!cbBillingDifferent.Checked)
            {
                Address shippingAddress = new Address();
                shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
                shippingAddress.ZipPostalCode = txtShippingZipCode.Text;
                CartContext.CartInfo.ShippingAddress = CartContext.CustomerInfo.ShippingAddress = shippingAddress;
            }
            else
            {
                CartContext.CartInfo.ShippingAddress = CartContext.CustomerInfo.ShippingAddress = CartContext.CustomerInfo.BillingAddress;
                ddlShippingCountry.SelectedValue = CartContext.CartInfo.ShippingAddress.CountryId.ToString();

                if (CartContext.CartInfo.ShippingAddress.StateProvinceId != 0 &&
                    ddlShippingState.Items.FindByValue(CartContext.CartInfo.ShippingAddress.StateProvinceId.ToString()) != null)
                {
                    ddlShippingState.SelectedValue = CartContext.CartInfo.ShippingAddress.StateProvinceId.ToString();
                }
            }
        }

        public bool validateInput()
        {
            if (!cbBillingDifferent.Checked)
            {
                if (CommonHelper.EnsureNotNull(txtShippingFirstName.Text) == String.Empty)
                {
                    lblFirstNameError.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                    lblFirstNameError.Visible = true;
                    _bError = true;
                }
                else
                    lblFirstNameError.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingLastName.Text) == String.Empty)
                {
                    lblLastNameError.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                    lblLastNameError.Visible = true;
                    _bError = true;

                }
                else
                    lblLastNameError.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingAddress1.Text) == String.Empty)
                {
                    lblShippingAddress1Error.Text = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                    lblShippingAddress1Error.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingAddress1Error.Visible = false;

                if (CommonHelper.EnsureNotNull(txtShippingCity.Text) == String.Empty)
                {
                    lblShippingCityError.Text = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                    lblShippingCityError.Visible = true;
                    _bError = true;
                }
                else
                    lblShippingCityError.Visible = false;


                if (ddlShippingState.SelectedValue.Equals("select"))
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
                    if (ddlShippingCountry.SelectedValue.Contains("231"))
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
                    else
                    {
                        if (!CommonHelper.IsValidZipCodeCanadian(txtShippingZipCode.Text))
                        {
                            lblShippingZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
                            lblShippingZiPError.Visible = true;
                            _bError = true;

                        }
                        else
                            lblShippingZiPError.Visible = false;
                    }

                }

            }
            //if (!cbTeams.Checked)
            //{
            //    lblTerms.Text = ResourceHelper.GetResoureValue("TermsErrorMsg");
            //    lblTerms.Visible = true;
            //    _bError = true;

            //}
            //else
            //    lblTerms.Visible = false;
            
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

            string c = txtCCNumber1.Text + txtCCNumber2.Text + txtCCNumber3.Text + txtCCNumber4.Text;
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

            }
            return _bError;
        }



        public void SaveData()
        {
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];

            // attribute save example            
            //clientData.OrderAttributeValues = new Dictionary<string, CSBusiness.Attributes.AttributeValue>();
            //clientData.OrderAttributeValues.Add("ref_url", new CSBusiness.Attributes.AttributeValue("http://www.google.com"));
            if (clientData.OrderAttributeValues == null)
            {
                clientData.OrderAttributeValues = new Dictionary<string, CSBusiness.Attributes.AttributeValue>();
            }
            clientData.OrderAttributeValues.AddOrUpdateAttributeValue("CustomOrderId", new CSBusiness.Attributes.AttributeValue("CS" + CommonHelper.GetRandonOrderNumber(DateTime.Now)));
            clientData.OrderAttributeValues.AddOrUpdateAttributeValue("cartabandonmentid", new CSBusiness.Attributes.AttributeValue(clientData.CartAbandonmentId.ToString()));

            string DailyCookie = String.Empty;
            if (Request.Cookies["DailyCookie"] != null)
            {
                DailyCookie = Request.Cookies["DailyCookie"].Value;
            }
            clientData.OrderAttributeValues.Add("CustomerDailyCookieID", new CSBusiness.Attributes.AttributeValue(DailyCookie));


            //Recapture billing information if the user modified the information
            if (rId == 0)
            {
                if (Convert.ToBoolean(!cbBillingDifferent.Checked)) //override if the user select yes button
                {
                    Address shippingAddress = new Address();
                    shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                    shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                    shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                    shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                    shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                    shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                    shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
                    shippingAddress.ZipPostalCode = txtShippingZipCode.Text;

                    clientData.CustomerInfo.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                    clientData.CustomerInfo.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                    clientData.CustomerInfo.ShippingAddress = shippingAddress;
                }
            }

            SetShippingAddress();


            PaymentInformation paymentDataInfo = new PaymentInformation();
            string CardNumber = txtCCNumber1.Text + txtCCNumber2.Text + txtCCNumber3.Text + txtCCNumber4.Text;
            paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
            paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
            paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
            paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
            paymentDataInfo.CreditCardCSC = txtCvv.Text;

            CartContext.PaymentInfo = paymentDataInfo;

            int orderId = 0;

            if (CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.InstantOrderProcess
                || CSFactory.OrderProcessCheck() == (int)OrderProcessTypeEnum.EnableReviewOrder)
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
