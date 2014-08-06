<%@ Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.UserControls.BillingForm" %>
<div class="cartB">
    <div class="form_line clearfix">
        <div class="error-1">
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
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
            AutoPostBack="true" OnSelectedIndexChanged="Country_SelectedIndexChanged" CssClass="text-1">
        </asp:DropDownList>
    </div>
    <div class="form_line clearfix">
        <div class="error-1">
            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" Display="Dynamic" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
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
            <asp:RequiredFieldValidator ID="rfvCity" runat="server" Display="Dynamic" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
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
            <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" Display="Dynamic" ControlToValidate="txtZipCode"></asp:RequiredFieldValidator>
            <asp:Label ID="lblZiPError" runat="server" Visible="false"></asp:Label></div>
        <label class="label-1">
            Zip Code*</label>
        <asp:TextBox ID="txtZipCode" runat="server" MaxLength="7" CssClass="text-1"></asp:TextBox>
    </div>
    <div class="form_line clearfix">
        <div class="error-1">
            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="Dynamic"
                ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
            <asp:Label ID="lblPhoneNumberError" runat="server" Visible="false"></asp:Label></div>
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
            <asp:Label ID="lblQuantityList" runat="server" Visible="false"></asp:Label></div>
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
   
    <div class="form_line_btn">
        <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="/content/images/yes_btn.png" OnClick="imgBtn_OnClick" />
    </div>
    <div class="form_line_guarantee">
        <a href="#guarantee" class="guarantee">View 30-Day Guarantee</a></div>
</div>
