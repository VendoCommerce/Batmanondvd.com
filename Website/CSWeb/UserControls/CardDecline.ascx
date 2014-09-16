<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardDecline.ascx.cs" Inherits="CSWeb.Root.UserControls.CardDecline" %>

<%@ Register src="Tokenex.ascx" tagname="Tokenex" tagprefix="uc1" %>

<asp:ScriptManager runat="server" ID="sm1">
</asp:ScriptManager>
<uc1:Tokenex ID="ucTokenex" runat="server"  />
<asp:UpdatePanel ID="upShippingForm" runat="server">
    <ContentTemplate>
 
      <div id="receipt_content"  style="margin: 0 auto; height: auto; width: 900px; position:relative;padding: 30px 60px;color: #000;">
    <p class="f18 red bold pad20">Sorry, but there was a problem with your credit card.</p>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="receipt_table1">
<tr class="horzline1">

                <td valign="top" class="pad0 black">
                   <strong>Product</strong>
                </td>
                <td valign="top" class="pad0 text-center black">
                    <strong>Quantity</strong>
                </td>
                <td valign="top" class="pad0 black">
                    <strong>Total</strong>
                </td>
            </tr>
              <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <ItemTemplate>
                      <tr>
                             <td width="76%" valign="top" style="padding-bottom: 20px">
                                <%# DataBinder.Eval(Container.DataItem, "LongDescription")%>
                            </td>
                            <td width="12%" valign="top" align="center">
                                <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                            </td>
                        
                             <td width="12%" valign="top">
                                $<%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "TotalPrice")), 2).ToString()%></td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:DataList>

           
            <asp:Literal ID="LiteralTableRows" runat="server"></asp:Literal>
          <tr class="horzline2">
                <td valign="top">&nbsp;
                    
                </td>
                <td valign="top">
                    Subtotal:<br />
                    S &amp; H:
                    <br />
                     <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                        Rush S &amp; H:<br />
                    </asp:Panel>
                    Tax:
                    <br />
            <asp:Panel ID="pnlPromotionLabel" runat="server" Visible="false">
                Discount:<br />
            </asp:Panel>
                    Total:
                </td>
                <td valign="top">
                    $<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                    $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                    <asp:Panel ID="pnlRush" runat="server" Visible="false">
                    $<asp:Literal ID="LiteralRushShipping" runat="server"></asp:Literal><br />
                    </asp:Panel>
                    $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
            <asp:Panel ID="pnlPromotionalAmount" runat="server" Visible="false">
                <asp:Label runat="server" ID="lblPromotionPrice"></asp:Label><br />
            </asp:Panel>
                    $<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal>
                 </td>
            </tr>
        </table>


        <div class="cartB">
        <div class="form_line clearfix">
    <div class="error-2">
        <asp:Label ID="Label2" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label></div>
    <label class="label-2">
        Do you want to review your shipping address?</label><div
            class="clear">
        </div>
    <asp:RadioButtonList ID="rblUpdateShippingAddress" runat="server" OnSelectedIndexChanged="rblUpdateShippingAddress_CheckedChanged"
        CssClass="checkbox-right" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="124">
        <asp:ListItem Value="true">Yes</asp:ListItem>
        <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
    </asp:RadioButtonList>
</div>
        <asp:Panel ID="pnlShippingAddress" runat="server" Visible="false">
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvShippingFirstName" runat="server" Display="Dynamic"
                        ControlToValidate="txtShippingFirstName"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingFirstNameError" runat="server" Visible="false">
                    </asp:Label>
                </div>
                <label class="label-1">
                    First Name*</label>
                <asp:TextBox ID="txtShippingFirstName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvShippingLastName" runat="server" Display="Dynamic"
                        ControlToValidate="txtShippingLastName"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingLastNameError" runat="server" Visible="false"></asp:Label>
                </div>
                <label class="label-1">
                    Last Name*</label>
                <asp:TextBox ID="txtShippingLastName" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:Label ID="lblShippingCountryError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Country*</label>
                <asp:DropDownList ID="ddlShippingCountry" runat="server" DataTextField="NAME" DataValueField="COUNTRYID"
                    AutoPostBack="true" OnSelectedIndexChanged="ShippingCountry_SelectedIndexChanged"
                    CssClass="text-1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvShippingAddress1" runat="server" Display="Dynamic"
                        ControlToValidate="txtShippingAddress1"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingAddress1Error" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Shipping Address*</label>
                <asp:TextBox ID="txtShippingAddress1" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                </div>
                <label class="label-1">
                </label>
                <asp:TextBox ID="txtShippingAddress2" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvShippingCity" runat="server" Display="Dynamic"
                        ControlToValidate="txtShippingCity"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingCityError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    City*</label>
                <asp:TextBox ID="txtShippingCity" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:Label ID="lblShippingStateError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    State*</label>
                <asp:DropDownList ID="ddlShippingState" runat="server" DataTextField="NAME" CssClass="text-1"
                    size="1">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvShippingZipCode" runat="server" Display="Dynamic"
                        ControlToValidate="txtShippingZipCode"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingZiPError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Zip Code*</label>
                <asp:TextBox ID="txtShippingZipCode" runat="server" MaxLength="7" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                        ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblShippingPhoneNumberError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Phone*</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
            </div>
            <div class="form_line clearfix">
                <div class="error-1">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
                    <asp:Label ID="lblEmailError" runat="server" Visible="false"></asp:Label></div>
                <label class="label-1">
                    Email*</label>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="text-1"></asp:TextBox>
            </div>
                <div class="form_line clearfix">
                    <div class="error-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                            ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label1" runat="server" Visible="false">
                        </asp:Label>
                    </div>
                    <label class="label-1">
                        First Name*</label>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="14" CssClass="text-1"></asp:TextBox>
                </div>
                         
        </asp:Panel>
        <div class="form_line clearfix">
    <div class="error-2">
        <asp:Label ID="LabelError" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label></div>
    <label class="label-2">
        Do you want to review your billing address?</label><div
            class="clear">
        </div>
    <asp:RadioButtonList ID="rblUpdateBillingAddress" runat="server" OnSelectedIndexChanged="rblUpdateBillingAddress_CheckedChanged"
        CssClass="checkbox-right" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="124">
        <asp:ListItem Value="true">Yes</asp:ListItem>
        <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
    </asp:RadioButtonList>
</div>
            <asp:Panel ID="pnlBillingAddress" runat="server" Visible="false">
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
                        <asp:Label ID="lblStateError" runat="server" Visible="false"></asp:Label></div>
                    <label class="label-1">
                        State*</label>
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="NAME" CssClass="text-1" size="1">
                    </asp:DropDownList>
                </div>
                <div class="form_line clearfix">
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
                <label class="label-3">
                    Credit Card*</label>
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCCType" runat="server" Display="Dynamic"
                            ControlToValidate="ddlCCType"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCCType" runat="server" Visible="false"></asp:Label></div>
                <asp:DropDownList ID="ddlCCType" runat="server" CssClass="text-2">
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <label class="label-3">
                    Exp Date*</label>
                <div class="error-2">
                <asp:RequiredFieldValidator ID="rfvExpMonth" runat="server" Display="Dynamic"
                            ControlToValidate="ddlExpMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpYear" runat="server" Display="Dynamic"
                    ControlToValidate="ddlExpYear"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblExpDate" runat="server" Visible="false"></asp:Label></div>
                <asp:DropDownList ID="ddlExpMonth" runat="server" CssClass="text-3">
                                <asp:ListItem Text="01" Value="1" />
                                <asp:ListItem Text="02" Value="2"  />
                                <asp:ListItem Text="03"  Value="3" />
                                <asp:ListItem Text="04"  Value="4" />
                                <asp:ListItem Text="05"  Value="5" />
                                <asp:ListItem Text="06"  Value="6" />
                                <asp:ListItem Text="07"  Value="7" />
                                <asp:ListItem Text="08"  Value="8" />
                                <asp:ListItem Text="09"  Value="9" />
                                <asp:ListItem Text="10"  Value="10" />
                                <asp:ListItem Text="11"  Value="11" />
                                <asp:ListItem Text="12"  Value="12" />
                </asp:DropDownList>
                <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="text-3">
                                <asp:ListItem Text="2014"/>
                                <asp:ListItem Text="2015"/>
                                <asp:ListItem Text="2016"/>
                                <asp:ListItem Text="2017"/>
                                <asp:ListItem Text="2018"/>
                                <asp:ListItem Text="2019"/>
                                <asp:ListItem Text="2020"/>
                                <asp:ListItem Text="2021"/>
                                <asp:ListItem Text="2022"/>
                                <asp:ListItem Text="2023"/>
                                <asp:ListItem Text="2024"/>
                                <asp:ListItem Text="2025"/>
                </asp:DropDownList>
            </div>
            <div class="form_line clearfix">
                <label class="label-3">
                    Card Number*</label>
                <div class="error-2">
                    <asp:Label ID="lblCCNumberError" runat="server" Visible="false" ClientIDMode="Static"></asp:Label></div>
               <asp:TextBox ID="txtCCNumber1" runat="server" MaxLength="16" ClientIDMode="Static"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCCNumber1" runat="server" Display="Dynamic" ErrorMessage="*" />
            </div>
            <div class="form_line clearfix">
                <label class="label-3">
                    Card Verification #* 
                </label>
                <div class="error-2">
                    <asp:RequiredFieldValidator ID="rfvCVV" ControlToValidate="txtCvv" runat="server" Display="Dynamic" />
                    <asp:Label ID="lblCvvError" runat="server" Visible="false"></asp:Label></div>
                <asp:TextBox ID="txtCvv" runat="server" CssClass="text-4" MaxLength="4"></asp:TextBox>
            </div>
          
            <div class="form_line_btn">
                <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="/content/images/try_it_now_btn.png" OnClick="imgBtn_OnClick" OnClientClick="return encryptCCnumber();" />
            </div>
  
        </div>
   </div>

    </ContentTemplate>
</asp:UpdatePanel>
