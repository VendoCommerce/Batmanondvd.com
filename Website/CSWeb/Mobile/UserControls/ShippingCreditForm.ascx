<%@Control Language="C#" AutoEventWireup="true" CodeBehind="ShippingCreditForm.ascx.cs" Inherits="CSWeb.Mobile.UserControls.ShippingCreditForm" %>
    <%@ Register Src="ShoppingCartControl.ascx" TagName="ShoppingCartControl"
    TagPrefix="uc" %>
    <asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>




     

<img src="/Content/Images/CS/mobile/form2_top.png" style="z-index: 20;position:relative;" />
<div class="cartB" style="border: none; background: url(../content/images/cs/mobile/form2_bg.jpg) repeat-x #4c7ab8; margin-top: -13px; z-index: 4;"> 

                <div class="form_line clearfix">
                <div class="error-2">
                    </div>
                    
                    <asp:CheckBox ID="cbBillingDifferent" runat="server" CssClass="checkbox-left" OnCheckedChanged="cbShippingDifferent_CheckedChanged"
                    AutoPostBack="true" Checked="true" />
                <label class="label-2">
                  Billing Address is the same as the Shipping Address</label>
                
            </div>                  
            
            <asp:Panel ID="pnlShippingAddress" runat="server" Visible="false">
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingFirstName" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingFirstName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblFirstNameError" runat="server" Visible="false">
                        </asp:Label>
                    </div>
                   
                    <asp:TextBox ID="txtShippingFirstName" runat="server" MaxLength="60" CssClass="text-1" placeholder="First Name"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingLastName" runat="server" Display="Dynamic" ControlToValidate="txtShippingLastName"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLastNameError" runat="server" Visible="false"></asp:Label>
                    </div>
                  
                    <asp:TextBox ID="txtShippingLastName" runat="server" MaxLength="60" CssClass="text-1" placeholder="Last Name"></asp:TextBox>
                </div>                
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingAddress1" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingAddress1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingAddress1Error" runat="server" Visible="false"></asp:Label></div>
                   
                    <asp:TextBox ID="txtShippingAddress1" runat="server" MaxLength="30" CssClass="text-1" placeholder="Address"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                    </div>
                   
                    <asp:TextBox ID="txtShippingAddress2" runat="server" MaxLength="30" CssClass="text-1" placeholder="Address 2"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingCity" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingCity"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingCityError" runat="server" Visible="false"></asp:Label></div>
                  
                    <asp:TextBox ID="txtShippingCity" runat="server" MaxLength="30" CssClass="text-1"  placeholder="City"></asp:TextBox>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblShippingCountryError" runat="server" Visible="false"></asp:Label></div>
                    <
                    <asp:DropDownList ID="ddlShippingCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                        AutoPostBack="true" OnSelectedIndexChanged="ShippingCountry_SelectedIndexChanged"
                        CssClass="text-1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:Label ID="lblShippingStateError" runat="server" Visible="false"></asp:Label></div>
                  
                    <asp:DropDownList ID="ddlShippingState" runat="server" DataTextField="NAME" CssClass="text-1" size="1" OnSelectedIndexChanged="ShippingState_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="rfvShippingZipCode" runat="server" Display="Dynamic"
                            ControlToValidate="txtShippingZipCode"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblShippingZiPError" runat="server" Visible="false"></asp:Label></div>
                  
                    <asp:TextBox ID="txtShippingZipCode" runat="server" MaxLength="7" CssClass="text-1" placeholder="Zip Code"></asp:TextBox>
                </div>
            </asp:Panel>
            <div class="form_line clearfix">
               
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                            ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label></div>
                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-1">
                </asp:DropDownList>
            </div>  
            <div class="form_line clearfix">
             
                <div class="error-2">
                <asp:RequiredFieldValidator ID="rfvCCNumber" ControlToValidate="txtCCNumber" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblCCNumberError" runat="server" Visible="false"></asp:Label></div>
                <asp:TextBox ID="txtCCNumber" runat="server" CssClass="text-1" MaxLength="16" placeholder="Credit Card Number"></asp:TextBox>
                
            </div>
            <div class="form_line clearfix">
               
                <div class="error-2">
                <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                            ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                    ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label></div>
                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-3">
                    <asp:ListItem Value="" Text="">Exp Month</asp:ListItem>
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
                    <asp:ListItem Value="" Text="">Exp Year</asp:ListItem>                    
                    <asp:ListItem Value="2013">2013</asp:ListItem>
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                    <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem Value="2016">2016</asp:ListItem>
                    <asp:ListItem Value="2017">2017</asp:ListItem>
                    <asp:ListItem Value="2018">2018</asp:ListItem>
                    <asp:ListItem Value="2019">2019</asp:ListItem>
                    <asp:ListItem Value="2020">2020</asp:ListItem>
                    <asp:ListItem Value="2021">2021</asp:ListItem>
                    <asp:ListItem Value="2022">2022</asp:ListItem>
                    <asp:ListItem Value="2023">2023</asp:ListItem>
                </asp:DropDownList>
            </div>
          
            <div class="form_line clearfix">
              
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                <asp:TextBox ID="txtCvv" runat="server" CssClass="text-4" MaxLength="4" placeholder="CVV"></asp:TextBox>
            </div>
            <div class="form_line clearfix">            
                                <div class="error-2">
                         
                        <asp:Label ID="lblTerms" runat="server" Visible="false"></asp:Label></div>
                          <asp:CheckBox ID="cbTeams" Checked="true" runat="server" CssClass="checkbox-left" />    
                           <label class="label-2">
                  I agree to the terms and conditions.</label>
                      
             
                
            </div>
            <div class="form_line_btn">
                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="/content/images/cs/mobile/try_it_now_btn.png"
                    CssClass="form_line_center" OnClick="imgBtn_OnClick" />
            </div>
           
           
        </div>




    
