<%@Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.UserControls.BillingShippingCreditForm" CodeBehind="BillingShippingCreditForm.ascx.cs" %>
<%@ Register src="ShoppingCartControl.ascx" tagname="ShoppingCartControl" tagprefix="uc1" %>
<asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>
<uc1:ShoppingCartControl ID="ShoppingCartControl1" runat="server" />
<asp:UpdatePanel ID="upBillingForm" runat="server">
    <ContentTemplate>    
    <a name="tryitnow" id="tryitnow"></a>
        <div class="greenbar">Shipping Information</div>
        <div class="cart_mobile" style="padding-top: 50px;"> 
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic"
                        ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
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
                        ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Last Name*</label>
                <asp:TextBox ID="txtLastName" runat="server" MaxLength="14" CssClass="text-1" placeholder="Last Name"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
                    <asp:Label ID="lblEmailError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Email Address*</label>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="text-1" placeholder="Email Address"></asp:TextBox>
            </div>
          
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic"
                        ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblAddress1Error" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                     Address*</label>
                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="30" CssClass="text-1 billingad1" placeholder="Address"></asp:TextBox>
                 
            </div>
         
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic"
                        ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCityError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    City*</label>
                <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="text-1" placeholder="City"></asp:TextBox>
            </div>
        
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:Label ID="lblStateError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    State*</label>
                <asp:DropDownList ID="ddlState" runat="server" DataTextField="Abbreviation" CssClass="text-1"
                    size="1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" Display="Dynamic"
                        ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblZiPError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Zip Code*</label>
                <asp:TextBox ID="txtZipCode" runat="server" MaxLength="7" CssClass="text-1" placeholder="ZIP Code"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                        ControlToValidate="txtPhoneNumber1"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPhoneNumberError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Phone*</label>
                <asp:TextBox ID="txtPhoneNumber1" runat="server" MaxLength="10" CssClass="text-1" placeholder="Phone"></asp:TextBox>
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
            AutoPostBack="true" Checked="true" /><label class="label-3" for="bsfcBillingShippingCreditForm_cbShippingSame">
                  Check if Shipping Address is Different than Billing Address
</label>          
            </div>
               <asp:Panel ID="pnlShippingAddress" runat="server" Visible="false">
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingFirstName" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingFirstName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingFirstName" runat="server" Visible="false">
                        </asp:Label>
                    </div>
                    <label class="label-1">
                        First Name*</label>
                    <asp:TextBox ID="txtShippingFirstName" runat="server" MaxLength="14" CssClass="text-1" placeholder="First Name"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingLastName" runat="server" Display="Dynamic" ControlToValidate="txtShippingLastName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingLastName" runat="server" Visible="false"></asp:Label>
                    </div>
                    <label class="label-1">
                        Last Name*</label>
                    <asp:TextBox ID="txtShippingLastName" runat="server" MaxLength="14" CssClass="text-1" placeholder="Last Name"></asp:TextBox>
                </div>                
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingAddress1" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingAddress1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingAddress1Error" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        Shipping Address*</label>
                    <asp:TextBox ID="txtShippingAddress1" runat="server" MaxLength="30" CssClass="text-1" placeholder="Billing Address"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingCity" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingCity"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingCityError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        City*</label>
                    <asp:TextBox ID="txtShippingCity" runat="server" MaxLength="30" CssClass="text-1" placeholder="City"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblShippingStateError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        State*</label>
                    <asp:DropDownList ID="ddlShippingState" runat="server" DataTextField="Abbreviation" CssClass="text-1" size="1" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingZipCode" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingZipCode"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingZiPError" runat="server" Visible="false"></asp:Label></div>
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
                     <asp:TextBox ID="txtCCNumber1" runat="server"  CssClass="text-1" MaxLength="16" placeholder="Credit Card Number"></asp:TextBox>
               
            </div>
<div class="form_line clearfix">
               <div class="error-1">
                <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                            ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                    ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Expiration Date*</label>
              
                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-3">
                    <asp:ListItem Value="" Text="">Exp. Month</asp:ListItem>
                    <asp:ListItem Value="1">01</asp:ListItem>
                    <asp:ListItem Value="2">02</asp:ListItem>
                    <asp:ListItem Value="3">03</asp:ListItem>
                    <asp:ListItem Value="4">04</asp:ListItem>
                    <asp:ListItem Value="5">05</asp:ListItem>
                    <asp:ListItem Value="6">06</asp:ListItem>
                    <asp:ListItem Value="7">07</asp:ListItem>
                    <asp:ListItem Value="8">08</asp:ListItem>
                    <asp:ListItem Value="9">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="text-3">
                    <asp:ListItem Value="" Text="">Exp. Year</asp:ListItem>
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                    <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem Value="2016">2016</asp:ListItem>
                    <asp:ListItem Value="2017">2017</asp:ListItem>
                    <asp:ListItem Value="2018">2018</asp:ListItem>
                    <asp:ListItem Value="2019">2019</asp:ListItem>
                    <asp:ListItem Value="2020">2020</asp:ListItem>
                </asp:DropDownList>
            </div>    
                    <div class="form_line clearfix">
             <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Card Verification*
                </label>
               
                <asp:TextBox ID="txtCvv" runat="server" CssClass="text-2" MaxLength="4" placeholder="CVV"></asp:TextBox>
                        <a href="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/cvv.png" class="cvv" style="position: relative; left: 32px;">What is this?</a>
            </div>
                        <div class="form_line clearfix">
                  <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                            ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Credit Card*</label>
          
                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-1">
                </asp:DropDownList>
            </div>
             <div class="form_line clearfix" style="padding: 10px 0 0 0">
                <div class="error-2">
                    </div>
                      
                  
     <asp:CheckBox ID="xyz" runat="server" CssClass="checkbox-left2" Checked="true" /><label class="label-3" for="bsfcBillingShippingCreditForm_xyz">
                Yes, I would like to receive updates and special offers from Warner Brothers. See Return Policy for Details
</label>          
            </div>

        </div><!-- end cart_mobile -->
        <div class="clear"></div>

            <div class="form_line_btn">
                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_complete_order.png" OnClick="imgBtn_OnClick" />
            </div>
        <div class="cart_mobile">
            <div class="form_line_guarantee" style="display: none;"><a href="returns.aspx" target="_blank">View 30-Day Guarantee</a></div>
                             
            <div class="multipay_txt">*You will be charged today for your first of 5 monthly payments of $19.99. Applicable taxes will be included in your first payment (this should not display if the consumer selects a 1-pay option). Shipping is FREE. If you are not satisfied with your purchase for any reason, simply return it within 60 days to receive a full refund. Restrictions may apply. Please refer to our Return Policy (link to Return Policy page)</div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
