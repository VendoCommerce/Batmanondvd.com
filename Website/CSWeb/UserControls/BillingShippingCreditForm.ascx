<%@ Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Root.UserControls.BillingShippingCreditForm"
            CodeBehind="BillingShippingCreditForm.ascx.cs" %>
            <script type="text/javascript" src="/Scripts/autoTab.js"></script>
            <script type="text/javascript">

                var pointerToMicrosoftValidator = ValidatorUpdateIsValid;
                ValidatorUpdateIsValid = function () {
                    pointerToMicrosoftValidator();
                    if (Page_IsValid) {

                    } else {
                        MM_showHideLayers('mask', '', 'hide');
                    }
                    // do something after Microsoft finishes 
                }
</script>
<asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>
<asp:UpdatePanel ID="upBillingForm" runat="server">
    <ContentTemplate>
        <div id="Rootontent_C" class="clearfix">
            <asp:Label ID="lblCartMessage" runat="server" />
            <div class="shopping_top clearfix">
                <div class="shopping_header">
                    Shopping Cart</div>
                <asp:Repeater runat="server" ID="rptShoppingCart" OnItemDataBound="rptShoppingCart_OnItemDataBound"
                    OnItemCommand="rptShoppingCart_OnItemCommand">
                    <HeaderTemplate>
                        <div class="shopping_top_row" style="height: 25px">
                            <div class="shopping_item_header">
                                Item</div>
                            <div class="shopping_desc_header">
                                Description</div>
                            <div class="shopping_qty_header">
                                Quantity</div>
                            <div class="shopping_price_header">
                                Price</div>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="shopping_top_row clearfix">
                            <div class="shopping_item">
                                <asp:Image runat="server" ID="imgProduct" Width="130px" />
                                <asp:Label runat="server" ID="lblSkuCode" /></div>
                            <div class="shopping_desc">
                                <asp:HiddenField runat="server" ID="hidSkuId" />
                                <p class="basket_title">
                                    <asp:Label ID="lblSkuTitle" runat="server" />
                                    <asp:Label ID="lblQuantity" runat="server" Visible="false" />
                                </p>
                                <p class="basket_description">
                                    <asp:Label ID="lblSkuDescription" runat="server" /></p>
                            </div>
                            <div class="shopping_qty">
                                <asp:TextBox ID="txtQuantity" runat="server" MaxLength="1" /><asp:ImageButton ID="btnRemoveItem"
                                    runat="server" CommandName="delete" ImageUrl="/content/images/remove_btn.jpg"
                                    CausesValidation="false" /></div>
                            <div class="shopping_price">
                                <asp:Label runat="server" ID="lblSkuInitialPrice"></asp:Label></div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
                <div class="shopping_top_row_total clearfix">
                    <div class="shopping_top_extrainfo" style="visibility:hidden">
                        <label>
                            Promotional Code:</label><input type="text" class="promo_code" />
                    </div>
                    <!-- end shopping top area where promotional codes, continue shopping buttons, etc can appear, if applicable -->
                    <div class="shopping_top_totals clearfix">
                        <span class="total_description">Subtotal:<br />
                            S &amp; H:<br />
                            Total:</span> <span class="total_prices">
                                <asp:Literal runat="server" ID='lblSubtotal'></asp:Literal><br />
                                <asp:Literal runat="server" ID="lblShipping"></asp:Literal><br />
                                <asp:Literal runat="server" ID="lblTax" Visible="false"></asp:Literal>
                                <asp:Literal runat="server" ID="lblOrderTotal"></asp:Literal></span>
                    </div>
                    <!-- end shopping top totals area -->
                </div>
                <div class="update_btn_section" runat="server" visible="false">
                    <asp:Label ID="lblUpdateMessage" runat="server" CssClass="update_message" />
                    <asp:LinkButton ID="lbUpdateQuantity" OnClick="lbUpdateQuantity_Click" runat="server"
                        CausesValidation="false" CssClass="update_btn">Update</asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="address_section">
                <div class="billingsection_header">
                    Shipping Info</div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblFirstNameError" runat="server" Visible="false">
                        </asp:Label>
                    </span>
                    <label class="label-1">
                        First Name*</label>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="14" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Last Name*</label>
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="14" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblAddress1Error" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Shipping Address*</label>
                    <asp:TextBox ID="txtAddress1" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1"></span>
                    <label class="label-1">
                        Address 2</label>
                    <asp:TextBox ID="txtAddress2" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblCityError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        City*</label>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:Label ID="lblStateError" runat="server" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" Display="Dynamic" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                    </span>
                    <label class="label-1">
                        State*</label>
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="NAME" CssClass="input-1"
                        size="1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:Label ID="lblCountryError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Country*</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                        AutoPostBack="true" OnSelectedIndexChanged="Country_SelectedIndexChanged" CssClass="input-1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" Display="Dynamic" ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblZiPError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Postal Code*</label>
                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="7" CssClass="input-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                            ControlToValidate="txtPhoneNumber1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblPhoneNumberError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Phone*</label>
                    <asp:TextBox ID="txtPhoneNumber1" runat="server" MaxLength="3" CssClass="input-3"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNumber2" runat="server" MaxLength="3" CssClass="input-3"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNumber3" runat="server" MaxLength="4" CssClass="input-3"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <span class="error-1">
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
                        <asp:Label ID="lblEmailError" runat="server" Visible="false"></asp:Label>
                    </span>
                    <label class="label-1">
                        Email*</label>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="input-1"></asp:TextBox>
                </div>
                <asp:Panel ID="pnlQuantity" runat="server" Visible="false">
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:Label ID="lblQuantityList" runat="server" Visible="false"></asp:Label>
                        </span>
                        <label class="label-1">
                            Quantity*</label>
                        <asp:DropDownList ID="ddlQuantityList" runat="server" CssClass="text-1">
                            <asp:ListItem Value="select" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="form_line clearfix" style="margin-left: 6px; padding-top: 10px;">
                    <asp:CheckBox ID="cbBillingSame" runat="server" CssClass="input-4" OnCheckedChanged="cbBillingSame_CheckedChanged"
                        AutoPostBack="true" Checked="true" />
                    <label class="label-2">
                        My billing information is the same as the shipping information.
                    </label>
                </div>
                <asp:Panel ID="pnlBillingAddress" runat="server" Visible="false">
                    <div class="billingsection_header">
                        Billing Info</div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:RequiredFieldValidator ID="rfvBillingFirstName" runat="server" Display="Dynamic"
                                ControlToValidate="txtBillingFirstName"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBillingFirstName" runat="server" Visible="false">
                            </asp:Label>
                        </span>
                        <label class="label-1">
                            First Name*</label>
                        <asp:TextBox ID="txtBillingFirstName" runat="server" MaxLength="14" CssClass="input-1"></asp:TextBox>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:RequiredFieldValidator ID="rfvBillingLastName" runat="server" Display="Dynamic"
                                ControlToValidate="txtBillingLastName"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBillingLastName" runat="server" Visible="false"></asp:Label>
                        </span>
                        <label class="label-1">
                            Last Name*</label>
                        <asp:TextBox ID="txtBillingLastName" runat="server" MaxLength="14" CssClass="input-1"></asp:TextBox>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:RequiredFieldValidator ID="rfvBillingAddress1" runat="server" Display="Dynamic"
                                ControlToValidate="txtBillingAddress1"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBillingAddress1Error" runat="server" Visible="false"></asp:Label></span>
                        <label class="label-1">
                            Billing Address*</label>
                        <asp:TextBox ID="txtBillingAddress1" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                    </div>
                    <div class="form_line clearfix">
                        <label class="label-1">
                            Address 2
                        </label>
                        <asp:TextBox ID="txtBillingAddress2" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:RequiredFieldValidator ID="rfvBillingCity" runat="server" Display="Dynamic"
                                ControlToValidate="txtBillingCity"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBillingCityError" runat="server" Visible="false"></asp:Label>
                        </span>
                        <label class="label-1">
                            City*</label>
                        <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="30" CssClass="input-1"></asp:TextBox>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:Label ID="lblBillingStateError" runat="server" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvBillingState" runat="server" Display="Dynamic"
                                ControlToValidate="ddlBillingState"></asp:RequiredFieldValidator>
                        </span>
                        <label class="label-1">
                            State*</label>
                        <asp:DropDownList ID="ddlBillingState" runat="server" DataTextField="NAME" CssClass="input-1"
                            size="1" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:Label ID="lblBillingCountryError" runat="server" Visible="false"></asp:Label></span>
                        <label class="label-1">
                            Country*</label>
                        <asp:DropDownList ID="ddlBillingCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                            AutoPostBack="true" OnSelectedIndexChanged="BillingCountry_SelectedIndexChanged"
                            CssClass="input-1">
                        </asp:DropDownList>
                    </div>
                    <div class="form_line clearfix">
                        <span class="error-1">
                            <asp:RequiredFieldValidator ID="rfvBillingZipCode" runat="server" Display="Dynamic"
                                ControlToValidate="txtBillingZipCode"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBillingZiPError" runat="server" Visible="false"></asp:Label>
                        </span>
                        <label class="label-1">
                            Zip Code*</label>
                        <asp:TextBox ID="txtBillingZipCode" runat="server" MaxLength="7" CssClass="input-1"></asp:TextBox>
                    </div>
                </asp:Panel>
            </div>
            <div class="payment_section">
                <div class="billingsection_header">
                    Payment Information</div>
                <div class="form_line_2">
                    <a target="_blank" href="https://www.mcafeesecure.com/RatingVerify?ref=www.stonedine.com">
                        <img width="65" height="37" border="0" src="//images.scanalert.com/meter/www.stonedine.com/63.gif"
                            alt="McAfee Secure sites help keep you safe from identity theft, credit card fraud, spyware, spam, viruses and online scams"
                            oncontextmenu="alert('Copying Prohibited by Law - McAfee Secure is a Trademark of McAfee, Inc.'); return false;">
                    </a>&nbsp;</div>
                <div class="form_line_2 clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic" ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Credit Card*</label>
                    <asp:DropDownList ID="ddlCCType" runat="server" CssClass="input-1">
                    </asp:DropDownList>
                </div>
                 <div class="form_line_2 clearfix">
               <div class="error-1">
                <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                            ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                    ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Expiration Date*</label>
              
                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-2">
                    
                </asp:DropDownList>
                <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="text-2">
                    
                </asp:DropDownList>
            </div>
                <div class="form_line_2 clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblCCNumberError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Card Number*</label>
                    <asp:TextBox ID="txtCCNumber1" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCCNumber1" runat="server" Display="Dynamic" ErrorMessage="*" />
                    <asp:TextBox ID="txtCCNumber2" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCCNumber2" runat="server" Display="Dynamic" ErrorMessage="*" />
                    <asp:TextBox ID="txtCCNumber3" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCCNumber3" runat="server" Display="Dynamic" ErrorMessage="*" />
                    <asp:TextBox ID="txtCCNumber4" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtCCNumber4" runat="server" Display="Dynamic" ErrorMessage="*" />
                </div>
                <div class="form_line_2 clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server"
                            Display="Dynamic" />
                        <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Card Verification*:
                    </label>
                    <asp:TextBox ID="txtCvv" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox>
                </div>
                <div class="form_line_2 clearfix" style="padding: 20px 0" runat="server" visible="false" >
                    <asp:CheckBox ID="ckbxSpecial" runat="server" CssClass="input-4" AutoPostBack="true"
                        Checked="true" />
                    <label class="label-2">
                        Yes, I'd like to be the first to know about special discounts, promotions and new product alerts!
                    </label>
                </div>
                <div class="form_line_2 clearfix">
                    <asp:Label ID="lblBtnMessage" runat="server" ForeColor="Red" />
                    <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="/content/images/yes_btn.png" CssClass="submit_btn" OnClientClick="MM_showHideLayers('mask', '', 'show');" OnClick="imgBtn_OnClick" />
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- end payment info section and end BILLING SECTION -->
        </div>
        <!-- end Rootontent -->
    </ContentTemplate>
</asp:UpdatePanel>
