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
using CSBusiness.Payment;

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
                rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");
            }

            if (!IsPostBack)
            {
                BindCountries(true);
                BindShippingCountries(true);
                BindRegions();
                BindShippingRegions();
                BindCreditCard();

            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));

            ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            String.Format(@"$(function() {{$('#{0}, #{1}, #{2}').autotab_magic().autotab_filter('numeric')}});",
                    txtPhoneNumber1.ClientID, txtPhoneNumber2.ClientID, txtPhoneNumber3.ClientID), true);

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            //String.Format(@"$(function() {{$('#{0}, #{1}, #{2},#{3}').autotab_magic().autotab_filter('numeric')}});",
            //        txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab1" + this.ClientID,
            //    String.Format(@"$(function() {{$('#{0}, #{1}, #{2}, #{3}').autotab_magic().autotab_filter('numeric')}});",
            //        txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);

        }

        #endregion Page Events

        #region General Methods

        /// <summary>
        /// List of Country from Cache Data
        /// </summary>
        public void BindCountries(bool setValue)
        {

            ddlCountry.DataSource = CountryManager.GetActiveCountry();
            ddlCountry.DataBind();
            if (setValue)
                ddlCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;

        }

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
        private void BindRegions()
        {

            ddlState.Items.Clear();
            int countryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlState.DataSource = list;
            ddlState.DataValueField = "StateProvinceId";
            ddlState.DataBind();
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
            int countryId = Convert.ToInt32(ddlShippingCountry.SelectedItem.Value);
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


            if (ddlState.SelectedValue.Equals("select"))
            {
                lblStateError.Text = ResourceHelper.GetResoureValue("StateErrorMsg");
                lblStateError.Visible = true;
                _bError = true;
            }
            else
                lblStateError.Visible = false;

            string strPhoneNum = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;

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


                if (ddlShippingState.SelectedValue.Equals("select"))
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
                pnlShippingAddress.Visible = true;
            else
                pnlShippingAddress.Visible = false;
        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
                //SaveAdditionaInfo();
                //int qId = 1;
                Response.Redirect(string.Format("AddProduct.aspx?PId={0}&CId={1}",
    Convert.ToInt32(ddlSize.SelectedValue), Convert.ToString((int)CSBusiness.ShoppingManagement.ShoppingCartType.SingleCheckout)));
                //Response.Redirect("store/addproduct.aspx" + "?PId=30&CId=" + (int)CSBusiness.ShoppingManagement.ShoppingCartType.ShippingCreditCheckout);
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
                Address billingAddress = new Address();
                billingAddress.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                billingAddress.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                billingAddress.Address1 = CommonHelper.fixquotesAccents(txtAddress1.Text);
                billingAddress.Address2 = CommonHelper.fixquotesAccents(txtAddress2.Text);
                billingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                billingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                billingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                billingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtZipCode.Text);

                Customer CustData = new Customer();
                CustData.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                CustData.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                CustData.PhoneNumber = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
                CustData.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.Username = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.BillingAddress = billingAddress;
                //CustData.ShippingAddress = billingAddress;

                if (!pnlShippingAddress.Visible)
                {
                    CustData.ShippingAddress = billingAddress;
                }
                else
                {
                    Address shippingAddress = new Address();
                    shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                    shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                    shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                    shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                    shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                    shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                    shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
                    shippingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtShippingZipCode.Text);

                    CustData.ShippingAddress = shippingAddress;



                }


                PaymentInformation paymentDataInfo = new PaymentInformation();
                string CardNumber = txtCCNumber1.Text;
                paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
                paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
                paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
                paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
                paymentDataInfo.CreditCardCSC = txtCvv.Text;

                clientData.PaymentInfo = paymentDataInfo;

                ClientOrderData = clientData;


                //Set the Client Order objects
                ClientCartContext contextData = (ClientCartContext)Session["ClientOrderData"];
                contextData.CustomerInfo = CustData;
                contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(CustData, contextData);
                Session["ClientOrderData"] = contextData;
            }
        }

        #endregion General Methods

    }
}