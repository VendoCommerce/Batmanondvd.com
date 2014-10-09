using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using CSBusiness;
using CSBusiness.CustomerManagement;
using CSCore.Utils;
using CSCore.DataHelper;
using System.Web;
using CSBusiness.Resolver;
using CSBusiness.CreditCard;
using System.Web.UI.WebControls;
using CSBusiness.Payment;
using CSBusiness.Preference;
using System.Linq;
using CSBusiness.ShoppingManagement;
using CSBusiness.OrderManagement;
using CSWebBase;

namespace CSWeb.Root.UserControls
{

    public partial class BillingShippingCreditForm : System.Web.UI.UserControl
    {
        #region Variable and Events Declaration
        bool _bError = false;
        public int rId = 1;
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
                rfvAddress1.ErrorMessage = ResourceHelper.GetResoureValue("ShippingAddress1ErrorMsg");
                rfvCity.ErrorMessage = ResourceHelper.GetResoureValue("ShippingCityErrorMsg");
                rfvZipCode.ErrorMessage = ResourceHelper.GetResoureValue("ShippingZipCodeErrorMsg");
                rfvEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailErrorMsg");
                revEmail.ErrorMessage = ResourceHelper.GetResoureValue("EmailValidationErrorMsg");
                rfvPhoneNumber.ErrorMessage = ResourceHelper.GetResoureValue("PhoneNumberErrorMsg");

                rfvBillingFirstName.ErrorMessage = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                rfvBillingLastName.ErrorMessage = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                rfvBillingAddress1.ErrorMessage = ResourceHelper.GetResoureValue("BillingAddress1ErrorMsg");
                rfvBillingCity.ErrorMessage = ResourceHelper.GetResoureValue("BillingCityErrorMsg");
                rfvBillingZipCode.ErrorMessage = ResourceHelper.GetResoureValue("BillingZipCodeErrorMsg");


                //rfvCreditCard.ErrorMessage = ResourceHelper.GetResoureValue("CCErrorMsg");
                rfvExpMonth.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateMonthErrorMsg") + "<br/>";
                rfvExpYear.ErrorMessage = ResourceHelper.GetResoureValue("ExpDateYearErrorMsg");
                rfvCVV.ErrorMessage = ResourceHelper.GetResoureValue("CVVErrorMsg");
                rfvCCType.ErrorMessage = ResourceHelper.GetResoureValue("CCTypeErrorMsg");

                txtCCNumber1.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber2.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber3.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");
                txtCCNumber4.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");

                txtPhoneNumber1.Attributes.Add("onkeyup", "return autoTab(this, 3, event);");
                txtPhoneNumber2.Attributes.Add("onkeyup", "return autoTab(this, 3, event);");
                txtPhoneNumber3.Attributes.Add("onkeyup", "return autoTab(this, 4, event);");

                ckbxSpecial.Checked = true;
            }

            if (!IsPostBack)
            {
                BindCountries(true);
                BindShippingCountries(true);
                BindRegions();
                BindBillingRegions();
                BindCreditCard();
                BindControls();
                PopulateExpiryYear();
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery", Page.ResolveUrl("~/Scripts/jquery-1.6.4.min.js"));
            //ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "jquery.autotab", Page.ResolveUrl("~/Scripts/jquery.autotab-1.1b.js"));

            ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab" + this.ClientID,
            String.Format(@"$(function() {{$('#{0}, #{1}, #{2}').autotab_magic().autotab_filter('numeric')}});",
                    txtPhoneNumber1.ClientID, txtPhoneNumber2.ClientID, txtPhoneNumber3.ClientID), true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "autotab1" + this.ClientID,
          String.Format(@"$(function() {{$('#{0}, #{1}, #{2},#{3}').autotab_magic().autotab_filter('numeric')}});",
                  txtCCNumber1.ClientID, txtCCNumber2.ClientID, txtCCNumber3.ClientID, txtCCNumber4.ClientID), true);

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
            ddlBillingCountry.DataSource = CountryManager.GetActiveCountry();
            ddlBillingCountry.DataBind();
            if (setValue)
                ddlBillingCountry.Items.FindByValue(ConfigHelper.DefaultCountry).Selected = true;
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
        private void BindBillingRegions()
        {

            ddlBillingState.Items.Clear();
            int countryId = Convert.ToInt32(ddlBillingCountry.SelectedItem.Value);
            List<StateProvince> list = StateManager.GetCacheStates(countryId);
            ddlBillingState.DataSource = list;
            ddlBillingState.DataValueField = "StateProvinceId";
            ddlBillingState.DataBind();
        }


        protected void Country_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindRegions();
        }

        protected void BillingCountry_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindBillingRegions();
        }
        public bool validateInput()
        {
            if (ClientOrderData.CartInfo.CartItems.Count == 0)
            {
                lblBtnMessage.Text = "Your Shopping Cart is currently empty.";

                _bError = true;
            }

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
                if (!(CommonHelper.IsValidZipCodeUS(txtZipCode.Text) || CommonHelper.IsValidZipCodeCanadian(txtZipCode.Text)))
                {
                    lblZiPError.Text = ResourceHelper.GetResoureValue("ShippingZipCodeValidationErrorMsg");
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

            if (pnlBillingAddress.Visible)
            {
                if (CommonHelper.EnsureNotNull(txtBillingFirstName.Text) == String.Empty)
                {
                    lblBillingFirstName.Text = ResourceHelper.GetResoureValue("FirstNameErrorMsg");
                    lblBillingFirstName.Visible = true;
                    _bError = true;
                }
                else
                    lblBillingFirstName.Visible = false;

                if (CommonHelper.EnsureNotNull(txtBillingLastName.Text) == String.Empty)
                {
                    lblBillingLastName.Text = ResourceHelper.GetResoureValue("LastNameErrorMsg");
                    lblBillingLastName.Visible = true;
                    _bError = true;

                }
                else
                    lblBillingLastName.Visible = false;

                if (CommonHelper.EnsureNotNull(txtBillingAddress1.Text) == String.Empty)
                {
                    lblBillingAddress1Error.Text = ResourceHelper.GetResoureValue("Address1ErrorMsg");
                    lblBillingAddress1Error.Visible = true;
                    _bError = true;
                }
                else
                    lblBillingAddress1Error.Visible = false;

                if (CommonHelper.EnsureNotNull(txtBillingCity.Text) == String.Empty)
                {
                    lblBillingCityError.Text = ResourceHelper.GetResoureValue("CityErrorMsg");
                    lblBillingCityError.Visible = true;
                    _bError = true;
                }
                else
                    lblBillingCityError.Visible = false;


                if (ddlBillingState.SelectedValue.Equals("select"))
                {
                    lblBillingStateError.Text = ResourceHelper.GetResoureValue("StateErrorMsg");
                    lblBillingStateError.Visible = true;
                    _bError = true;
                }
                else
                    lblBillingStateError.Visible = false;

                if (CommonHelper.EnsureNotNull(txtBillingZipCode.Text) == String.Empty)
                {
                    lblBillingZiPError.Text = ResourceHelper.GetResoureValue("ZipCodeErrorMsg");
                    lblBillingZiPError.Visible = true;
                    _bError = true;
                }
                else
                {
                    if (!(CommonHelper.IsValidZipCodeUS(txtZipCode.Text) || CommonHelper.IsValidZipCodeCanadian(txtZipCode.Text)))
                    {
                        lblBillingZiPError.Text = ResourceHelper.GetResoureValue("BillingZipCodeValidationErrorMsg");
                        lblBillingZiPError.Visible = true;
                        _bError = true;

                    }
                    else
                        lblBillingZiPError.Visible = false;

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

        protected void cbBillingSame_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbBillingSame.Checked)
                pnlBillingAddress.Visible = true;
            else
                pnlBillingAddress.Visible = false;
        }

        protected void imgBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!validateInput())
            {
                SaveData();
        
                Response.Redirect(string.Format("AddProduct.aspx?CId={0}", Convert.ToString((int)CSBusiness.ShoppingManagement.ShoppingCartType.SingleCheckout)));
            }


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
                shippingAddress.Address2 = CommonHelper.fixquotesAccents(txtAddress2.Text);
                shippingAddress.City = CommonHelper.fixquotesAccents(txtCity.Text);
                shippingAddress.StateProvinceId = Convert.ToInt32(ddlState.SelectedValue);
                shippingAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                shippingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtZipCode.Text);

                Customer CustData = new Customer();
                CustData.FirstName = CommonHelper.fixquotesAccents(txtFirstName.Text);
                CustData.LastName = CommonHelper.fixquotesAccents(txtLastName.Text);
                CustData.PhoneNumber = txtPhoneNumber1.Text + txtPhoneNumber2.Text + txtPhoneNumber3.Text;
                CustData.Email = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.Username = CommonHelper.fixquotesAccents(txtEmail.Text);
                CustData.ShippingAddress = shippingAddress;
                //CustData.ShippingAddress = billingAddress;

                if (!pnlBillingAddress.Visible)
                {
                    CustData.BillingAddress = shippingAddress;
                }
                else
                {
                    Address billingAddress = new Address();
                    billingAddress.FirstName = CommonHelper.fixquotesAccents(txtBillingFirstName.Text);
                    billingAddress.LastName = CommonHelper.fixquotesAccents(txtBillingLastName.Text);
                    billingAddress.Address1 = CommonHelper.fixquotesAccents(txtBillingAddress1.Text);
                    billingAddress.Address2 = CommonHelper.fixquotesAccents(txtBillingAddress2.Text);
                    billingAddress.City = CommonHelper.fixquotesAccents(txtBillingCity.Text);
                    billingAddress.StateProvinceId = Convert.ToInt32(ddlBillingState.SelectedValue);
                    billingAddress.CountryId = Convert.ToInt32(ddlBillingCountry.SelectedValue);
                    billingAddress.ZipPostalCode = CommonHelper.fixquotesAccents(txtBillingZipCode.Text);

                    CustData.BillingAddress = billingAddress;
                }


                PaymentInformation paymentDataInfo = new PaymentInformation();
                string CardNumber = txtCCNumber1.Text + txtCCNumber2.Text + txtCCNumber3.Text + txtCCNumber4.Text;
                paymentDataInfo.CreditCardNumber = CommonHelper.Encrypt(CardNumber);
                paymentDataInfo.CreditCardType = Convert.ToInt32(ddlCCType.SelectedValue);
                paymentDataInfo.CreditCardName = ddlCCType.SelectedItem.Text;
                paymentDataInfo.CreditCardExpired = new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue), 1);
                paymentDataInfo.CreditCardCSC = txtCvv.Text;

                clientData.CustomerInfo = CustData;
                clientData.PaymentInfo = paymentDataInfo;
                ClientOrderData = clientData;

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

                        //if (OrderHelper.ValidationCheck(clientData.OrderId))
                        //{
                        //    if (rId == 1)
                        //        Response.Redirect("PostSale.aspx");
                        //    else
                        //        Response.Redirect("CardDecline.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("CardDecline.aspx");
                        //}

                       
                    }
                }
            }
        }

        protected void rptShoppingCart_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                HiddenField hidSkuId = e.Item.FindControl("hidSkuId") as HiddenField;
                Label lblSkuCode = e.Item.FindControl("lblSkuCode") as Label;
                Label lblSkuTitle = e.Item.FindControl("lblSkuTitle") as Label;
                Label lblSkuDescription = e.Item.FindControl("lblSkuDescription") as Label;
                Label lblSkuInitialPrice = e.Item.FindControl("lblSkuInitialPrice") as Label;
                ImageButton btnRemoveItem = e.Item.FindControl("btnRemoveItem") as ImageButton;
                TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;

                Sku cartItem = e.Item.DataItem as Sku;
                cartItem.LoadAttributeValues();
                imgProduct.ImageUrl = cartItem.ImagePath;
                hidSkuId.Value = CSCore.Utils.CommonHelper.Encrypt(Convert.ToString(cartItem.SkuId));
                lblSkuTitle.Text = cartItem.Title;
                lblSkuDescription.Text = cartItem.ShortDescription;
                lblSkuInitialPrice.Text = cartItem.InitialPrice.ToString("C");
                txtQuantity.Text = cartItem.Quantity.ToString();

                btnRemoveItem.CommandArgument = cartItem.SkuId.ToString();

                lblQuantity.Text = " (x " + cartItem.Quantity.ToString() + ")";

                if (cartItem.ImagePath.Length > 0)
                {
                    imgProduct.ImageUrl = cartItem.ImagePath;
                    lblSkuCode.Visible = false;
                }
                else
                {
                    imgProduct.Visible = false;
                    lblSkuCode.Text = cartItem.SkuCode.ToString();
                }

                if (cartItem.AttributeValues != null)
                {
                    if (cartItem.AttributeValues.ContainsKey("isrushsku"))
                    {
                        if (cartItem.AttributeValues["isrushsku"].Value == "1")
                        {
                            txtQuantity.Enabled = false;
                        }
                    }
                }
                //Hiding txtQuantity if RushSKU
            }
        }

        protected void rptShoppingCart_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int skuToRemove = -1;

            switch (e.CommandName)
            {
                case "delete":
                    ClientCartContext cartContext = ClientOrderData;

                    skuToRemove = Convert.ToInt32(e.CommandArgument);
                    cartContext.CartInfo.RemoveSku(skuToRemove);

                    if (cartContext.CustomerInfo != null && cartContext.CustomerInfo.ShippingAddress != null)
                        cartContext.CartInfo.ShippingAddress = cartContext.CustomerInfo.ShippingAddress;

                    cartContext.CartInfo.Compute();

                    ClientOrderData = cartContext;

                    BindControls();
                    break;
                case "quantity":
                    /*
                    ClientCartContext cartContext = ClientOrderData;

                    cartContext.CartInfo.AddItem(skuId, qId, true, false);

                    cartContext.CartInfo.Compute();
                    cartContext.CartInfo.ShowQuantity = false;
                    clientData.CartInfo = cartObject;
                    Session["ClientOrderData"] = clientData;
                      */
                    break;
                default:
                    break;
            }
        }

        protected void lbUpdateQuantity_Click(object sender, EventArgs e)
        {
            ClientCartContext cartContext = ClientOrderData;

            foreach (RepeaterItem item in rptShoppingCart.Items)
            {
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                HiddenField hidSkuId = item.FindControl("hidSkuId") as HiddenField;

                if (txtQuantity != null && hidSkuId != null)
                {
                    int skuId = -1;

                    try
                    {
                        skuId = int.Parse(CSCore.Utils.CommonHelper.Decrypt(hidSkuId.Value));
                    }
                    catch { }

                    if (skuId != -1)
                    {
                        int quantity;

                        if (int.TryParse(txtQuantity.Text, out quantity))
                        {
                            cartContext.CartInfo.AddOrUpdate(skuId, quantity, true, false, false);

                            if (cartContext.CustomerInfo != null && cartContext.CustomerInfo.ShippingAddress != null)
                                cartContext.CartInfo.ShippingAddress = cartContext.CustomerInfo.ShippingAddress;

                            cartContext.CartInfo.Compute();
                        }
                    }
                }
            }

            ClientOrderData = cartContext;

            BindControls();
        }

        public void BindControls()
        {
            // bad cc charge error msg (based off querystring)
            if (Request.QueryString["err"] == "1")
            {
                lblBtnMessage.Text = "We're unable to authorize the card provided for this transaction.  Please enter a new card.";
            }
            else
                lblBtnMessage.Text = string.Empty;

            if (ClientOrderData.CartInfo.CartItems.Count > 0)
            {
                rptShoppingCart.DataSource = ClientOrderData.CartInfo.CartItems.FindAll(x => x.Visible == true);
                rptShoppingCart.DataBind();
                lblSubtotal.Text = String.Format("${0:0.00}", ClientOrderData.CartInfo.SubTotal);
                lblTax.Text = String.Format("${0:0.00}", ClientOrderData.CartInfo.TaxCost);
                lblShipping.Text = String.Format("${0:0.00}", ClientOrderData.CartInfo.ShippingCost);
                lblOrderTotal.Text = String.Format("${0:0.00}", ClientOrderData.CartInfo.Total);
                lblCartMessage.Text = string.Empty;
            }
            else
            {
                lblCartMessage.Text = "Your Shopping Cart is currently empty.";

                lblSubtotal.Text = string.Empty;
                lblTax.Text = string.Empty;
                lblShipping.Text = string.Empty;
                lblOrderTotal.Text = string.Empty;

                rptShoppingCart.Visible = false;
            }
        }

        #endregion General Methods

    }
}
