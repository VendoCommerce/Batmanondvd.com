<%@ Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Tablet_CALLOUT.UserControls.ShippingBillingCreditForm" CodeBehind="ShippingBillingCreditForm.ascx.cs" %>
<%@ Register Src="ShoppingCartControl.ascx" TagName="ShoppingCartControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Tokenex.ascx" TagName="Tokenex" TagPrefix="uc2" %>
<script type="text/javascript" src="/Scripts/autoTab.js"></script>
<script type="text/javascript">

    var pointerToMicrosoftValidator = ValidatorUpdateIsValid;
    ValidatorUpdateIsValid = function () {
        pointerToMicrosoftValidator();
        if (Page_IsValid) {
            //MM_showHideLayers('mask', '', 'show');

        } else {
            //window.scrollTo(0, document.body.scrollHeight);
           MM_showHideLayers('mask', '', 'hide');
        }
        // do something after Microsoft finishes 
    }
</script>
<asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>


<h2>Today’s Special Offer, Batman!</h2>
<h3 style="padding-bottom: 27px;">The Classic Batman TV Series is Finally Available For You To Own!</h3>

<uc1:ShoppingCartControl ID="ShoppingCartControl1" runat="server" />
<%--<asp:UpdatePanel ID="upBillingForm" runat="server">
    <ContentTemplate>--%>
<asp:UpdatePanel runat="server" ID="upBillingForm">
    <ContentTemplate>

        <div class="Displayontent_B clearfix" style="margin-top: 35px;">
            <uc2:Tokenex ID="ucTokenex" runat="server" />


            <h3 class="webfont1bold arrow_down_hdr pad6">Where Would You Like Us To Send Your Order?</h3>
            <div class="horizontal_dots" style="margin-bottom: 20px;"></div>

            <table style="width: 100%;">
                <tr>
                    <td valign="top" style="width: 476px;">
                        <div class="cart_left">

                            <div class="cartB">

                                <h4>Shipping Information</h4>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvShippingFirstName" runat="server" Display="Dynamic"
                                            ControlToValidate="txtShippingFirstName" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblShippingFirstName" runat="server" Visible="false">
                                        </asp:Label>
                                    </div>
                                    <label class="label-1">
                                        First Name*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingFirstName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvShippingLastName" runat="server" Display="Dynamic" ControlToValidate="txtShippingLastName" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblShippingLastName" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        Last Name*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingLastName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvShippingAddress1" runat="server" Display="Dynamic"
                                            ControlToValidate="txtShippingAddress1" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblShippingAddress1Error" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        Shipping Address*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingAddress1" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                                    

                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1"></div>
                                    <label class="label-1">
                                        Address 2</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingAddress2" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                                </div>

                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvShippingCity" runat="server" Display="Dynamic"
                                            ControlToValidate="txtShippingCity" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblShippingCityError" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        City*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingCity" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                                </div>
                                <%--<div class="form_line clearfix" runat="server">
                    <div class="error-1">
                        <asp:Label ID="lblShippingCountryError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Country*</label>
                    <asp:DropDownList ID="ddlShippingCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                                      AutoPostBack="true" OnSelectedIndexChanged="ShippingCountry_SelectedIndexChanged"
                                      CssClass="text-1">
                    </asp:DropDownList>
                </div>--%>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:Label ID="lblShippingStateError" runat="server" Visible="false"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvShippingState" runat="server" Display="Dynamic"
                                            ControlToValidate="ddlShippingState" Enabled="False"></asp:RequiredFieldValidator>
                                    </div>
                                    <label class="label-1">
                                        State*</label>
                                    <asp:DropDownList ID="ddlShippingState" runat="server" DataTextField="NAME" CssClass="text-1" size="1" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvShippingZipCode" runat="server" Display="Dynamic"
                                            ControlToValidate="txtShippingZipCode" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblShippingZiPError" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        ZIP*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingZipCode" runat="server" MaxLength="7" CssClass="text-1"></asp:TextBox>
                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                            Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
                                        <asp:Label ID="lblEmailError" runat="server"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        Email*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtEmail" runat="server" MaxLength="100" CssClass="text-1"></asp:TextBox>
                                </div>
                                <div class="form_line clearfix">
                                    <div class="error-1">
                                        <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                                            ControlToValidate="txtPhoneNumber1" Enabled="False"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblPhoneNumberError" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <label class="label-1">
                                        Phone*</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtPhoneNumber1" runat="server" MaxLength="10" CssClass="text-1"></asp:TextBox>
                                    <%--<asp:TextBox  AutoCompleteType="Disabled" ID="txtPhoneNumber2" runat="server" MaxLength="3" CssClass="text-4"></asp:TextBox>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtPhoneNumber3" runat="server" MaxLength="4" CssClass="text-4"></asp:TextBox>--%>
                                </div>

                                <asp:Panel ID="pnlQuantity" runat="server" Visible="false">
                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:Label ID="lblQuantityList" runat="server" Visible="false"></asp:Label>
                                        </div>
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

                                <div class="form_line clearfix" style="padding: 12px 0 12px 122px;">
                                    <asp:CheckBox ID="cbShippingSame" runat="server" CssClass="checkbox-left" OnCheckedChanged="cbShippingSame_CheckedChanged" AutoPostBack="true" Checked="true" />
                                    <label class="label-3" for="bscfShippingBillingCreditForm_cbShippingSame">
                                        Billing information is the same as Shipping
                                    </label>


                                </div>
                                <asp:Panel ID="pnlShippingAddress" runat="server" Visible="false">
                                    <h4>Billing Address</h4>
                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic"
                                                ControlToValidate="txtFirstName" Enabled="False"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblFirstNameError" runat="server" Visible="false">
                                            </asp:Label>
                                        </div>
                                        <label class="label-1">
                                            First Name*</label>
                                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtFirstName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                                    </div>
                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic"
                                                ControlToValidate="txtLastName" Enabled="False"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <label class="label-1">
                                            Last Name*</label>
                                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtLastName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                                    </div>

                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAddress1" Enabled="False"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblAddress1Error" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <label class="label-1">
                                            Billing Address*</label>
                                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtAddress1" runat="server" MaxLength="30" CssClass="text-1 billingad1"></asp:TextBox>

                                    </div>
                                <div class="form_line clearfix">
                                    <div class="error-1"></div>
                                    <label class="label-1">
                                        Address 2</label>
                                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtAddress2" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                                </div>
                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic"
                                                ControlToValidate="txtCity" Enabled="False"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblCityError" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <label class="label-1">
                                            City*</label>
                                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtCity" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                                    </div>
                                    <div class="form_line clearfix">
                                        <div class="error-1">
                                            <asp:Label ID="lblStateError" runat="server" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvState" runat="server" Display="Dynamic"
                                                ControlToValidate="ddlState" Enabled="False"></asp:RequiredFieldValidator>
                                        </div>
                                        <label class="label-1">
                                            State*</label>
                                        <asp:DropDownList ID="ddlState" runat="server" DataTextField="NAME" CssClass="text-1"
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
                                            ZIP*</label>
                                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtZipCode" runat="server" MaxLength="7" CssClass="text-1"></asp:TextBox>
                                    </div>


                                </asp:Panel>

                                <div class="form_line clearfix" runat="server" visible="False">
                                    <label class="label-1">
                                        Additional Shipping Charge</label>
                                    <div class="error-1">
                                        <asp:DropDownList ID="ddlAdditionShippingCharge" runat="server" CssClass="text-1">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>





                    <td valign="top">
                        <div class="cart_right">
                            <h4>Payment Information</h4>

                            <div class="form_line clearfix" style="padding-bottom: 20px;">
                                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/ssl.png" alt="SSL Secured Online Ordering" style="margin-right: 20px;" />
                                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/creditcards.jpg" alt="We accept Visa, MasterCard, Discover and American Express" />
                            </div>

                            <div class="form_line clearfix" runat="server" visible="true">
                                <div class="error-1">
                                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label>
                                </div>
                                <label class="label-1">
                                    Card Type*</label>

                                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-1">
                                </asp:DropDownList>
                            </div>

                            <div class="form_line clearfix">
                                <div class="error-1">
                                    <asp:RequiredFieldValidator ID="rfvCreditCard" ControlToValidate="txtCCNumber1" runat="server" Display="Dynamic" ErrorMessage="Please enter valid card number" Enabled="False" />
                                    <asp:Label ID="lblCCNumberError" runat="server" Visible="false"></asp:Label>
                                </div>
                                <label class="label-1">
                                    Card Number*</label>

                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtCCNumber1" runat="server" CssClass="text-1" MaxLength="16" ClientIDMode="Static"></asp:TextBox>


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
                            <div class="form_line_2 clearfix" style="padding-top: 5px;visibility: hidden">
                                <div class="error-1">
                                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server"
                                        Display="Dynamic" Enabled="False" />
                                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                                <label class="label-1">
                                    Card Verification*
                                </label>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtCvv" runat="server" CssClass="input-2" MaxLength="4"></asp:TextBox>
                            </div>
                            <div class="form_line clearfix" style="padding: 16px 0 30px 114px;">
                                <asp:CheckBox ID="chkOptIn" runat="server" Checked="true" CssClass="checkbox-left2" />
                                &nbsp;Yes, I would like to receive updates and special offers from Warner Bros. See <a href="http://www.warnerbros.com/privacy/policy.html" target="_blank">Privacy Policy</a> for Details.
                            </div>

                            <div class="form_line_btn">
                                <p class="f12 pad6" style="padding-left: 8px;">By clicking here, I agree to the <a href="http://www.warnerbros.com/privacy/terms.html" target="_blank">Terms of Service</a>, <a href="http://www.warnerbros.com/privacy/policy.html" target="_blank">Privacy Policy</a> and <a href="returns.aspx" target="_blank">Return Policy</a>.</p>
                                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/big1/btn_completeorder.png" OnClick="imgBtn_OnClick" OnClientClick="return encryptCCnumber();" CssClass="btn_fade" />
                            </div>
                            <div class="form_line_guarantee" style="display: none;"><a href="#guarantee" class="guarantee">90-Day Money-Back Guarantee!</a></div>
                            
                            
                            &nbsp;</div>
                    </td>
                </tr>
            </table>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<!-- end cartB -->

<!-- end cart_right -->


<!-- end Displayontent -->
<%--</ContentTemplate>
</asp:UpdatePanel>--%>