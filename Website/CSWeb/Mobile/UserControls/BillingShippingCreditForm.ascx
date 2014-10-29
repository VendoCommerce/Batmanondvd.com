<%@ Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.UserControls.BillingShippingCreditForm" CodeBehind="BillingShippingCreditForm.ascx.cs" %>
<%@ Register Src="../../UserControls/Tokenex.ascx" TagName="Tokenex" TagPrefix="uc2" %>
<asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>
<asp:LinkButton ID="refresh" runat="server" CausesValidation="false"></asp:LinkButton>
<asp:Repeater runat="server" ID="rptShoppingCart" OnItemDataBound="rptShoppingCart_OnItemDataBound"
    OnItemCommand="rptShoppingCart_OnItemCommand">
    <HeaderTemplate>
        <div class="greenbar">Shopping Cart</div>
        <div class="cart_table clearfix" style="margin-bottom: 0;">
            <div class="cart_text_hdr">
                <h4 class="pad0">Item</h4>
            </div>
            <div class="product_price_hdr" class="pad0" style="padding-left: 8px;">
                <h4>Price</h4>
            </div>
        </div>
        <div class="horizontal_dots"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="cart_table clearfix">
            <div class="cart_image">
                <asp:Image runat="server" ID="imgProduct" />
            </div>
            <div class="cart_text">
                <div class="basket_title">
                    <asp:Label runat="server" ID="lblSkuCode"></asp:Label>
                </div>
                <div class="basket_description">
                    <asp:Label runat="server" ID='lblSkuDescription'></asp:Label>
                </div>
            </div>
            <div class="product_price">
                <asp:Label runat="server" ID="lblSkuInitialPrice"></asp:Label>*
                <td runat="server" width="1%" id='holderRemove' visible="false">
                    <asp:ImageButton ID="btnRemoveItem" runat="server" CommandName="delete" CausesValidation="false"
                        CssClass="ucRemoveButtonOverlay" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/delete.gif" />
                </td>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:Panel runat="server" ID="pnlUpgrade" Visible="false">
    <div class="upgrade_offer clearfix">
        <div class="arrow-up"></div>
        <!-- arrow -->
        <div class="fleft f28 webfont1bold" style="width: 440px; padding-right: 30px; line-height: 1.2em;">
            <asp:Label runat="server" ID="lblUpgrade"></asp:Label>
        </div>
        <div class="fleft" style="width: 182px; padding-top: 9px;">
            <asp:ImageButton ID="imgUpgrade" runat="server" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_upgrade.png" CausesValidation="False" OnCommand="imgUpgrade_Command" />
        </div>


    </div>
</asp:Panel>
<asp:Panel ID="pnlTotal" runat="server">
    <asp:PlaceHolder runat="server" ID="holderTaxAndShipping">
        <div class="horizontal_dots">
        </div>



        <div class="cart_totals clearfix">
            <div class="cart_totals_left">
                <strong>Subtotal:</strong><br />
                Shipping:<br />
                Tax:<br />
            </div>
            <div class="cart_totals_right">
                <asp:Literal runat="server" ID='lblSubtotal'></asp:Literal><br />
                <div>
                    <asp:Literal runat="server" ID="lblShipping" Visible="false"></asp:Literal>$0.00<span class="crossout"></span>
                </div>
                <asp:Literal runat="server" ID="lblTax"></asp:Literal><br />

                <%--                <asp:Literal runat="server" ID="lblRushShipping" Visible="false"></asp:Literal>
                <table>
                    <tr id='holderRushShippingTotal' runat="server">
                        <td class='cartSubtotalTitle' align="right" colspan="3">
                            Rush Shipping:
                        </td>
                        <td class='cartSubtotalValue' align="center">
                        </td>
                    </tr>
                    <tr id='holderRushShipping' runat="server" visible="false">
                        <td colspan="4" class="rushShipping">
                            <asp:CheckBox runat="server" ID="chkIncludeRushShipping" OnCheckedChanged="chkIncludeRushShipping_OnCheckedChanged"
                                AutoPostBack="true" Text="Rush Shipping" />
                        </td>
                    </tr>
                </table>--%>
            </div>
            <div class="clear" style="height: 14px;"></div>
            <div class="horizontal_dots" style="width: 240px; left: -8px; display: none;"></div>
            <div class="cart_totals_left black"><strong>Today's Payment:</strong></div>
            <div class="cart_totals_right black">
                <asp:Literal runat="server" ID="lblOrderTotal"></asp:Literal>
            </div>
            <div class="clear" style="height: 50px;"></div>
        </div>
    </asp:PlaceHolder>
</asp:Panel>
<div class="cart_offer" style="display: none;">
    <strong>*Offer Details:</strong> offer details
</div>
<asp:UpdatePanel ID="upBillingForm" runat="server">
    <ContentTemplate>
        <a name="tryitnow" id="tryitnow"></a>
        <div class="greenbar">Shipping Information</div>
        <div class="cart_mobile" style="padding-top: 50px;">
            <div class="form_line clearfix">
                <div class="error-1">
                    <uc2:Tokenex ID="ucTokenex" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic"
                        ControlToValidate="txtFirstName" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblFirstNameError" runat="server" Visible="false">
                    </asp:Label>
                </div>
                <label class="label-1">
                    First Name*</label>
                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="14" CssClass="text-1" placeholder="First Name"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic"
                        ControlToValidate="txtLastName" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Last Name*</label>
                <asp:TextBox ID="txtLastName" runat="server" MaxLength="14" CssClass="text-1" placeholder="Last Name"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
                    <asp:Label ID="lblEmailError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Email Address*</label>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="text-1" placeholder="Email Address"></asp:TextBox>
            </div>

            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic"
                        ControlToValidate="txtAddress1" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblAddress1Error" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Address*</label>
                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="30" CssClass="text-1 billingad1" placeholder="Address"></asp:TextBox>

            </div>

            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic"
                        ControlToValidate="txtCity" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCityError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    City*</label>
                <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="text-1" placeholder="City"></asp:TextBox>
            </div>

            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:Label ID="lblStateError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    State*</label>
                <asp:DropDownList ID="ddlState" runat="server" DataTextField="Abbreviation" CssClass="text-1"
                    size="1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" Display="Dynamic"
                        ControlToValidate="txtZipCode" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblZiPError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Zip Code*</label>
                <asp:TextBox ID="txtZipCode" runat="server" MaxLength="7" CssClass="text-1" placeholder="ZIP Code"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                        ControlToValidate="txtPhoneNumber1" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPhoneNumberError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Phone*</label>
                <asp:TextBox ID="txtPhoneNumber1" runat="server" MaxLength="14" CssClass="text-1" placeholder="Phone"></asp:TextBox>
                <asp:TextBox ID="txtPhoneNumber2" runat="server" Visible="false" MaxLength="3" CssClass="text-4"></asp:TextBox>
                <asp:TextBox ID="txtPhoneNumber3" runat="server" Visible="false" MaxLength="4" CssClass="text-4"></asp:TextBox>
            </div>
            <div class="form_line clearfix" style="padding-bottom: 0">
                <label class="label-1"></label>
            </div>


            <div style="height: 26px;"></div>


        </div>
        <div class="greenbar">Payment Information</div>
        <div style="height: 50px;"></div>

        <div class="cart_mobile">
            <div class="form_line clearfix" style="padding: 10px 0 16px 0">

                <%-- <asp:RadioButtonList ID="rblShippingDifferent" runat="server" OnSelectedIndexChanged="rblShippingDifferent_CheckedChanged"
        CssClass="text-5" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="124">
        <asp:ListItem Value="true">Yes</asp:ListItem>
        <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
    </asp:RadioButtonList>--%>
                <asp:CheckBox ID="cbShippingSame" runat="server" CssClass="checkbox-left" OnCheckedChanged="cbShippingSame_CheckedChanged"
                    AutoPostBack="true" Checked="false" /><label class="label-3" for="bsfcBillingShippingCreditForm_cbShippingSame">
                        Check if Shipping Address is Different than Billing Address
                    </label>
            </div>
            <asp:Panel ID="pnlShippingAddress" runat="server" Visible="false">
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingFirstName" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingFirstName" Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingFirstName" runat="server" Visible="false">
                        </asp:Label>
                    </div>
                    <label class="label-1">
                        First Name*</label>
                    <asp:TextBox ID="txtShippingFirstName" runat="server" MaxLength="14" CssClass="text-1" placeholder="First Name"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingLastName" runat="server" Display="Dynamic" ControlToValidate="txtShippingLastName" Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingLastName" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        Last Name*</label>
                    <asp:TextBox ID="txtShippingLastName" runat="server" MaxLength="14" CssClass="text-1" placeholder="Last Name"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingAddress1" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingAddress1" Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingAddress1Error" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        Shipping Address*</label>
                    <asp:TextBox ID="txtShippingAddress1" runat="server" MaxLength="30" CssClass="text-1" placeholder="Billing Address"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingCity" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingCity" Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingCityError" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        City*</label>
                    <asp:TextBox ID="txtShippingCity" runat="server" MaxLength="30" CssClass="text-1" placeholder="City"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblShippingStateError" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        State*</label>
                    <asp:DropDownList ID="ddlShippingState" runat="server" DataTextField="Abbreviation" CssClass="text-1" size="1" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingZipCode" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingZipCode" Enabled="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingZiPError" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        Zip Code*</label>
                    <asp:TextBox ID="txtShippingZipCode" runat="server" MaxLength="7" CssClass="text-1" placeholder="ZIP Code"></asp:TextBox>
                </div>
            </asp:Panel>

            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/creditcards.jpg" alt="We accept Visa, MasterCard, AmericanExpress and Discover cards." class="block" style="margin: 40px 0 30px 0;" />


            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:Label ID="lblCCNumberError" runat="server" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCCNumber1" runat="server" Display="Dynamic" ErrorMessage="Please enter card number" />
                </div>
                <label class="label-1">
                    Card Number*</label>
                <asp:TextBox ID="txtCCNumber1" runat="server" CssClass="text-1" MaxLength="16" placeholder="Credit Card Number" ClientIDMode="Static"></asp:TextBox>

            </div>

            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                        ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                        ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Expiration Date*</label>

                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-3">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="text-3">
                </asp:DropDownList>
            </div>
            <div class="form_line_2 clearfix" style="padding-top: 5px;">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server"
                        Display="Dynamic" />
                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Card Verification*
                </label>
                <asp:TextBox ID="txtCvv" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                        ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Credit Card*</label>

                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix" style="padding: 10px 0 0 0">
                <div class="error-2">
                </div>


                <asp:CheckBox ID="xyz" runat="server" CssClass="checkbox-left2" Checked="true" /><label class="label-3" for="bsfcBillingShippingCreditForm_xyz">
                    Yes, I would like to receive updates and special offers from Warner Bros. See <a href="http://www.warnerbros.com/privacy/policy.html" target="_blank">Privacy Policy</a> for Details.
                </label>
            </div>

        </div>
        <!-- end cart_mobile -->
        <div class="clear"></div>

        <div class="form_line_btn">
            <p class="f24" style="padding: 0 0 16px 14px; line-height: 1.3em;">By clicking here, I agree to the <a href="http://www.warnerbros.com/privacy/terms.html" target="_blank">Terms of Service</a>, <a href="http://www.warnerbros.com/privacy/policy.html" target="_blank">Privacy Policy</a> and <a href="returns.aspx" target="_blank">Return Policy</a>.</p>
            <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_complete_order.png" OnClick="imgBtn_OnClick" OnClientClick="return encryptCCnumber();" />
        </div>
        <div class="cart_mobile">
            <div class="form_line_guarantee" style="display: none;"><a href="returns.aspx" target="_blank">View 30-Day Guarantee</a></div>

            <div class="multipay_txt">
                <asp:Literal runat="server" ID="ltOfferTerms"></asp:Literal>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
