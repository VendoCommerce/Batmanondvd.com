using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSBusiness.Preference;
using CSCore.Utils;
using CSCore.DataHelper;
using CSWeb.Root.Store;
using System.Web;
using CSBusiness.Resolver;
using CSWebBase;

namespace CSWeb.Root.UserControls
{

    public partial class ShippingForm : System.Web.UI.UserControl
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
                rfvShippingState.ErrorMessage = ResourceHelper.GetResoureValue("ShippingStateErrorMsg");
            }

            if (!IsPostBack)
            {
                BindShippingCountries(true);
                BindShippingRegions();

            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));


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
            ddlShippingState.Items.Insert(0, new ListItem("- Select -", string.Empty));
        }


        protected void ShippingCountry_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindShippingRegions();
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


            if (ddlShippingState.SelectedValue.Equals(""))
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

            return _bError;

        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
                Session["PId"] = 30;
                Response.Redirect(RedirectUrl + "?CId=" + (int)CSBusiness.ShoppingManagement.ShoppingCartType.ShippingCreditCheckout);
            }
        }
        public void SaveData()
        {
            if (Page.IsValid)
            {
                //Set Customer Information
                Address shippingAddress = new Address();
                shippingAddress.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                shippingAddress.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                shippingAddress.Address1 = CommonHelper.fixquotesAccents(txtShippingAddress1.Text);
                shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtShippingAddress2.Text);
                shippingAddress.City = CommonHelper.fixquotesAccents(txtShippingCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlShippingState.SelectedValue);
                shippingAddress.CountryId = Convert.ToInt32(ddlShippingCountry.SelectedValue);
                shippingAddress.ZipPostalCode = txtShippingZipCode.Text;

                Customer CustData = new Customer();
                CustData.FirstName = CommonHelper.fixquotesAccents(txtShippingFirstName.Text);
                CustData.LastName = CommonHelper.fixquotesAccents(txtShippingLastName.Text);
                CustData.PhoneNumber = txtPhoneNumber.Text;
                CustData.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.Username = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.BillingAddress = shippingAddress;
                CustData.ShippingAddress = shippingAddress;

                //Set the Client Order objects
                ClientCartContext contextData = (ClientCartContext)Session["ClientOrderData"];
                contextData.CustomerInfo = CustData;
                contextData.CartAbandonmentId = CSResolve.Resolve<ICustomerService>().InsertCartAbandonment(CustData,contextData);
                Session["ClientOrderData"] = contextData;
            }
        }

        #endregion General Methods

    }
}