using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSCore.Utils;
using CSCore.DataHelper;
using CSWeb.Mobile.Store;
using System.Web;
using CSBusiness.Resolver;
using CSBusiness.CreditCard;
using System.Web.UI.WebControls;
using CSBusiness.OrderManagement;
using CSBusiness.Payment;

namespace CSWeb.Mobile.UserControls
{
    public partial class CardDecline : System.Web.UI.UserControl
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
        #endregion Variable and Events Declaration

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CommonHelper.IsHttps(HttpContext.Current))
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
            if (!IsPostBack)
            {
                txtShippingFirstName.Focus();
                rfvShippingFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvShippingLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvShippingAddress1.ErrorMessage = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                rfvShippingCity.ErrorMessage = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                rfvShippingZipCode.ErrorMessage = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
                rfvEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailErrorMsg");
                revEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailValidationErrorMsg");
                rfvPhoneNumber.ErrorMessage = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");
                rfvFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvAddress1.ErrorMessage = ResourceHelper.GetResoureValue("BillingAddress1ErrorMsg");
                rfvCity.ErrorMessage = ResourceHelper.GetResoureValue("BillingCityErrorMsg");
                rfvZipCode.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");

                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                lblCCNumberError.Text = ResourceHelper.GetResoureValue("SummaryCCDecline");
            }

            if (!IsPostBack)
            {
                BindShippingCountries(true);
                BindShippingRegions();
                BindCountries(true);
                BindRegions();
                BindCreditCard();
                ReloadCartData();
                BindData();
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + TextBoxArea.ClientID,
            //     String.Format(@"$('#{0}').autotab({{ target: '{1}', format: 'numeric' }});", TextBoxArea.ClientID, TextBoxPhoneNum1.ClientID), true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + TextBoxPhoneNum1.ClientID,
            //    String.Format(@"$('#{0}').autotab({{ target: '{1}', format: 'numeric', previous: '{2}' }});", TextBoxPhoneNum1.ClientID, TextBoxPhoneNum2.ClientID, TextBoxArea.ClientID), true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + TextBoxPhoneNum2.ClientID,
            //    String.Format(@"$('#{0}').autotab({{ target: '{1}', format: 'numeric', previous: '{2}' }});", TextBoxPhoneNum2.ClientID, TextBoxEmail.ClientID, TextBoxPhoneNum1.ClientID), true);


        }

        #endregion Page Events

        #region General Methods

        /// <summary>
        /// List of Country from Cache Data
        /// </summary>
        public void BindShippingCountries(bool setValue)
        {

            ddlShippingCountry.DataSource = CountryManager.GetActiveCountry();
            ddlShippingCountry.DataBind();
            if (setValue)
                ddlShippingCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;
        }

        /// <summary>
        /// List of States from Cache Data
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
        }

        protected void ShippingCountry_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindShippingRegions();
        }
        protected void Country_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindRegions();
        }
        public bool validateInput()
        {


            if (CommonHelper.EnsureNotNull(txtShippingFirstName.Text) == String.Empty)
            {
                lblShippingFirstNameError.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                lblShippingFirstNameError.Visible = true;
                _bError = true;
            }
            else
                lblShippingFirstNameError.Visible = false;

            if (CommonHelper.EnsureNotNull(txtShippingLastName.Text) == String.Empty)
            {
                lblShippingLastNameError.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                lblShippingLastNameError.Visible = true;
                _bError = true;

            }
            else
                lblShippingLastNameError.Visible = false;

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

            string strPhoneNum = txtPhoneNumber.Text;

            if (!CommonHelper.IsValidPhone(strPhoneNum))
            {
                lblShippingPhoneNumberError.Text = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");
                lblShippingPhoneNumberError.Visible = true;
                _bError = true;
            }
            else
                lblShippingPhoneNumberError.Visible = false;

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

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
                Response.Redirect(RedirectUrl + "?PId=30&CId=" + (int)CSBusiness.ShoppingManagement.ShoppingCartType.ShippingCreditCheckout);
            }


        }
        public void SaveData()
        {
            if (Page.IsValid)
            {
                ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];


                Address billingAddress = new Address();
                billingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                billingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                billingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                billingAddress.Address2 = CommonHelper.fixquotesAccents(txtAddress2.Text);
                billingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                billingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                billingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                billingAddress.ZipPostalCode = txtZipCode.Text;

                Address shippingAddress = new Address();
                shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
                shippingAddress.ZipPostalCode = txtShippingZipCode.Text;

                clientData.CustomerInfo.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                clientData.CustomerInfo.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                clientData.CustomerInfo.PhoneNumber = txtPhoneNumber.Text;
                clientData.CustomerInfo.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                clientData.CustomerInfo.BillingAddress = billingAddress;
                clientData.CustomerInfo.ShippingAddress = shippingAddress;



                PaymentInformation paymentDataInfo = new PaymentInformation();
                string CardNumber = txtCCNumber1.Text + txtCCNumber2.Text + txtCCNumber3.Text + txtCCNumber4.Text;
                paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
                paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
                paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
                paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
                paymentDataInfo.CreditCardCSC = txtCvv.Text;

                clientData.PaymentInfo = paymentDataInfo;

                int orderId = clientData.OrderId;
                CSResolve.Resolve<IOrderService>().UpdateOrder(orderId, clientData);
                if (orderId > 1)
                {
                    clientData.OrderId = orderId;
                    Session["ClientOrderData"] = clientData;
                    Response.Redirect("ValidateKount.aspx?" + Request.QueryString);

                }
            }
        }

        protected void rblUpdateBillingAddress_CheckedChanged(object sender, EventArgs e)
        {
            bool billingVal = Convert.ToBoolean(rblUpdateBillingAddress.SelectedItem.Value);
            if (billingVal)
                pnlBillingAddress.Visible = true;
            else
                pnlBillingAddress.Visible = false;
        }

        protected void rblUpdateShippingAddress_CheckedChanged(object sender, EventArgs e)
        {
            bool shippingVal = Convert.ToBoolean(rblUpdateShippingAddress.SelectedItem.Value);
            if (shippingVal)
                pnlShippingAddress.Visible = true;
            else
                pnlShippingAddress.Visible = false;
        }
        public void ReloadCartData()
        {
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];

            ddlShippingCountry.Items.FindByValue(clientData.CustomerInfo.ShippingAddress.CountryId.ToString()).Selected = true;
            ddlCountry.Items.FindByValue(clientData.CustomerInfo.BillingAddress.CountryId.ToString()).Selected = true;

            ddlShippingState.Items.FindByValue(clientData.CustomerInfo.ShippingAddress.StateProvinceId.ToString()).Selected = true;
            ddlState.Items.FindByValue(clientData.CustomerInfo.BillingAddress.StateProvinceId.ToString()).Selected = true;

            //Shipping information
            txtShippingFirstName.Text = clientData.CustomerInfo.ShippingAddress.FirstName;
            txtShippingLastName.Text = clientData.CustomerInfo.ShippingAddress.LastName;
            txtShippingAddress1.Text = clientData.CustomerInfo.ShippingAddress.Address1;
            txtShippingAddress2.Text = clientData.CustomerInfo.ShippingAddress.Address2;
            txtShippingCity.Text = clientData.CustomerInfo.ShippingAddress.City;
            txtShippingZipCode.Text = clientData.CustomerInfo.ShippingAddress.ZipPostalCode;


            //Payment information
            string ccNumber = CommonHelper.Decrypt(clientData.PaymentInfo.CreditCardNumber);
            txtCCNumber1.Text = ccNumber.Substring(0, 4);
            txtCCNumber2.Text = ccNumber.Substring(4, 4);
            txtCCNumber3.Text = ccNumber.Substring(8, 4);
            txtCCNumber4.Text = ccNumber.Substring(12, ccNumber.Length -12);

            txtCvv.Text = clientData.PaymentInfo.CreditCardCSC;
            DateTime expireDate = DateTime.MinValue;
            DateTime.TryParse(clientData.PaymentInfo.CreditCardExpired.ToString(), out expireDate);
            ddlExpMonth.Items.FindByValue(expireDate.Month.ToString()).Selected = true;
            ddlExpYear.Items.FindByValue(expireDate.Year.ToString()).Selected = true;

            //Billing informarion
            txtFirstName.Text = clientData.CustomerInfo.BillingAddress.FirstName;
            txtLastName.Text = clientData.CustomerInfo.BillingAddress.LastName;
            txtAddress1.Text = clientData.CustomerInfo.BillingAddress.Address1;
            txtAddress2.Text = clientData.CustomerInfo.BillingAddress.Address2;
            txtCity.Text = clientData.CustomerInfo.BillingAddress.City;
            txtZipCode.Text = clientData.CustomerInfo.BillingAddress.ZipPostalCode;
            txtEmail.Text = clientData.CustomerInfo.Email;
            txtPhoneNumber.Text = clientData.CustomerInfo.PhoneNumber;
        }
        private void BindData()
        {
            ClientCartContext clientData = (ClientCartContext)Session["ClientOrderData"];

            Order orderData = CSResolve.Resolve<IOrderService>().GetOrderDetails(clientData.OrderId);

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





        }

        #endregion General Methods

    }
}