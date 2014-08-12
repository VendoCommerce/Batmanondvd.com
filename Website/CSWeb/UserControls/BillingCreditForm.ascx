<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillingCreditForm.ascx.cs"
    Inherits="CSWeb.Root.UserControls.BillingCreditForm" %>
    <%@ Register src="Tokenex.ascx" tagname="Tokenex" tagprefix="uc1" %>
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
    <%@ Register Src="ShoppingCartControl.ascx" TagName="ShoppingCartControl"
    TagPrefix="uc" %>
<asp:ScriptManager runat="server" ID="smss1">
</asp:ScriptManager>
<asp:UpdatePanel ID="upBillingForm" runat="server">
    <ContentTemplate>
<div class="left">

        <h2 class="webfont1">Shopping Cart</h2>
        
        <uc:ShoppingCartControl ID="ShoppingCartControl1" runat="server" OnUpdateShipping="Shipping_OnUpdateShipping"></uc:ShoppingCartControl>     

</div>
<div class="right">
    <div><img src="/Content/Images/form_cart_top.png" width="300" height="164" style="display: block;" /></div>
        <div class="cartB">
            <h2 style="display: none;">
                Billing Address</h2>
            <div class="form_line clearfix">
                <div class="error-2">
                    <uc1:Tokenex ID="ucTokenex" runat="server" />
                    </div>
                <asp:CheckBox ID="cbBillingDifferent" runat="server" CssClass="checkbox-right" OnCheckedChanged="cbBillingDifferent_CheckedChanged"
                    AutoPostBack="true" Checked="true" />
                <label class="label-4">
                    My billing information is the same as shipping</label>
                
            </div>
            <asp:Panel ID="pnlBillingAddress" runat="server" Visible="false">
                
                
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblCountryError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Country*</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                        AutoPostBack="true" OnSelectedIndexChanged="Country_SelectedIndexChanged"
                        CssClass="text-1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic"
                            ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblFirstNameError" runat="server" Visible="false">
                        </asp:Label>
                    </div>
                    <label class="label-1">
                        First Name*</label>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        Last Name*</label>
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic"
                            ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblAddress1Error" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Billing Address*</label>
                    <asp:TextBox ID="txtAddress1" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                    </div>
                    <label class="label-1">
                    </label>
                    <asp:TextBox ID="txtAddress2" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic"
                            ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblCityError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        City*</label>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" Display="Dynamic"
                        ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblStateError" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        State*</label>
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="NAME" CssClass="text-1" size="1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix" style="padding-bottom: 12px;">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" Display="Dynamic"
                            ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblZiPError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Zip Code*</label>
                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="7" CssClass="text-1"></asp:TextBox>
                </div>
            </asp:Panel>
            <div class="form_line clearfix">
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                            ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label></div>
                <label class="label-2">
                    Credit Card Type*</label>
                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="RfvCCNumberError" ControlToValidate="txtCCNumber1" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblCCNumberError" runat="server" Visible="false" ClientIDMode="Static"></asp:Label>
                </div>
                <label class="label-2">
                    Card Number*</label>
                <asp:TextBox ID="txtCCNumber1" runat="server" CssClass="text-1" MaxLength="16" ClientIDMode="Static"></asp:TextBox>
                        
            </div>
            <div class="form_line clearfix">
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblEmailError" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-2">
                    Card Verification* 
                </label>
                
                <asp:TextBox ID="txtCvv" runat="server" CssClass="text-4" MaxLength="4"></asp:TextBox> &nbsp;&nbsp;<a class="cvv" href="/content/images/cvv.png">What's this?</a>
            </div>
            <div class="form_line clearfix">
                <label class="label-2">
                    Exp Date*</label>
                <div class="error-2">
                <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                            ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                    ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label></div>
                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-3">
                    
                </asp:DropDownList>
                <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="text-3">
                    
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix" style="padding-top: 5px;">
                <input type="checkbox" checked="checked" class="checkbox-right" style="float: left; width: 20px; margin-bottom: 20px;" />
                
                <label class="label-3">
                    I would like to get product updates and <br />
                    special offers.</label>
                
            </div>
            <div class="form_line_btn">
                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="/content/images/yes_btn.png"
                    CssClass="form_line_center" OnClick="imgBtn_OnClick" OnClientClick="return encryptCCnumber();" />
            </div>
            <div class="form_line_guarantee">
                <a href="#guarantee" class="guarantee">90-Day Money-Back Guarantee</a></div>
            <p style="text-align: center;"><img src="../Content/Images/norton.jpg" alt="Norton Secured powered by VeriSign" /></p>
        </div>
          </div>
    <!-- end cartB -->
</div>
<!-- end cart_right -->


</ContentTemplate>
</asp:UpdatePanel>
    
 