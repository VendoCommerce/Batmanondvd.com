<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="CSWeb.Admin.OrderDetail" MasterPageFile="AdminSite.master" EnableViewState="True" EnableSessionState="True" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc" TagName="attributes" Src="UserControls/Attributes.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Order Details</title>
       <style type="text/css">
        /*.modalBackground
        {
            background-color: transparent;
            background-position: center;
            padding: 0;
        }
        .modalPopup
        {
            position: relative;            
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: #CCCCCC;
            padding: 1px;
            width: 720px;
            height: auto;
        }*/
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <span id="pageid" class="orders"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-shopping-cart"></i> Order Manager</li>
<li><a href="orderlist.aspx"><i class="icon-shopping-cart"></i> Orders</a></li>
<li><i class="icon-info-sign"></i> Order Details</li>
</ul>
<h3 class="page-header page-header-top">Order Details</h3>
   
  <h4 style="margin-bottom: 20px">Version: <span class="text-info"><em><%= Version %></em></span><div class="pull-right" style="margin-bottom: 6px; font-weight: normal;"><asp:LinkButton ID="lbEditOrder" runat="server" CssClass="btn btn-primary" Visible="False"><i class="icon-pencil"></i> Edit Order Info</asp:LinkButton></div></h4>
       
   <div class="row-fluid">
   <div class="span4">
   <table class="table table-bordered table-condensed">
   
	<thead><tr><th colspan="2">Shipping Info</th></tr></thead>
    <tbody>
    <tr><td><strong>Name</strong></td><td><asp:Literal ID="LiteralName" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>Address</strong></td><td><asp:Literal ID="LiteralAddress" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>Address 2</strong></td><td><asp:Literal ID="LiteralAddress2" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>City</strong></td><td><asp:Literal ID="LiteralCity" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>State</strong></td><td><asp:Literal ID="LiteralState" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>Zip Code</strong></td><td><asp:Literal ID="LiteralZip" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>Country</strong></td><td><asp:Literal ID="LiteralCountry" runat="server"></asp:Literal></td></tr>
    <tr><td><strong>Email Address</strong></td><td><asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></td></tr>
</tbody>             
 </table>            
     </div>
     <div class="span4">           
          <table class="table table-bordered table-condensed">
   
	<thead><tr><th colspan="2">Billing Info</th></tr></thead>
    <tbody>         
<tr><td><strong>Name</strong></td><td><asp:Literal ID="LiteralName_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>Address</strong></td><td><asp:Literal ID="LiteralAddress_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>Address 2</strong></td><td><asp:Literal ID="LiteralAddress2_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>City</strong></td><td><asp:Literal ID="LiteralCity_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>State</strong></td><td><asp:Literal ID="LiteralState_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>Zip Code</strong></td><td><asp:Literal ID="LiteralZip_b" runat="server"></asp:Literal></td></tr>
<tr><td><strong>Country</strong></td><td><asp:Literal ID="LiteralCountry_b" runat="server"></asp:Literal></td></tr>
   </tbody>             
 </table>         
            
   </div>
            <div class="span4">  
    <table class="table table-bordered table-condensed">
	<thead><tr><th colspan="2">Payment Info</th></tr></thead>
    <tbody>     
<tr><td><strong>Authorization Code</strong></td><td><%= AuthorizationCode%></td></tr>
<tr><td><strong>Transaction Code</strong></td><td><%= TransactionCode%></td></tr>
<tr><td><strong>Credit Card Name</strong></td><td><%= CreditCardName%></td></tr>
<tr><td><strong>Credit Card Number (Last 4)</strong></td><td><%= CreditCardLast4 %></td></tr>
<tr><td><strong>Credit Card CVV</strong></td><td><%= CreditCardCSC %></td></tr>
<tr><td><strong>Credit Card Expiration Date</strong></td><td><%= CreditCardExpireDate %></td></tr>
      </tbody>             
 </table>   
 </div>
   </div>
   
   
    <table class="table table-striped table-bordered">
        <thead>
        <tr>
            <th>Description
            </th>
            <th class="span3 text-center">Quantity
            </th>
            <th class="span3">Total
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Title")%>
                        -
                        <%# DataBinder.Eval(Container.DataItem, "SkuId")%>
                    </td>
                    <td class="span3 text-center">
                        <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                    </td>
                    <td>
                        $<%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "FullPrice"))%></td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
  
        
        
        <tr>
            <td></td>
            <td class="text-right" style="font-weight: bold">
                Subtotal<br>
                Shipping & Processing<br>
                <asp:Panel ID="pnlRushLabel" runat="server" Visible="false">
                    <strong>Rush Shipping & Processing</strong><br>
                </asp:Panel>
               Tax<br>
               Total
            </td>
            <td style="font-weight: bold">
                $<asp:Literal ID="LiteralSubTotal" runat="server"></asp:Literal><br />
                $<asp:Literal ID="LiteralShipping" runat="server"></asp:Literal><br />
                <asp:Panel ID="pnlRush" runat="server" Visible="false">
                $<asp:Literal ID="LiteralRushShipping" runat="server"></asp:Literal><br />
                </asp:Panel>
                $<asp:Literal ID="LiteralTax" runat="server"></asp:Literal><br />
                $<asp:Literal ID="LiteralTotal" runat="server"></asp:Literal>
            </td>
        </tr>
        
      </tbody></table>
    
    
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeThePopup" TargetControlID="lbEditOrder"
        PopupControlID="pnlModalPopUpPanel" CancelControlID="btnCancelModalPopup" PopupDragHandleControlID="pnlModalPopUpPanel" Y="20" />
        
        
    <asp:Panel ID="pnlModalPopUpPanel" runat="server" CssClass="modalPopup" Scrollbars="auto" Visible="False">
    <asp:LinkButton ID="btnCancelModalPopup2" CssClass="btn btn-mini btn-danger pull-right" runat="server" Text="X" CausesValidation="false" />
    
    <h4><asp:Literal ID="litHeader" Text="Edit Order Information" runat="server" />&nbsp;&nbsp;<button class="btn btn-mini btn-inverse" title="Drag this popup or scroll down using mousewheel or browser scrollbar"><i class="icon-info-sign"></i></button></h4>
    
    <p><asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="UpdateValidationGroup" runat="server" CssClass="text-error" DisplayMode="List" ShowSummary="true" HeaderText="Please correct the following fields and try again." />
        <div class="row-fluid">
            <div class="span6 well" style="min-height: 530px">
                <h5>Shipping Info</h5>
                <div class="form-horizontal">
                    <div class="control-group">
                        <label class="control-label">
                        First Name</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingFirstName" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShippingFirstName" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Last Name</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingLastName" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShippingLastName" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Address</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingAddress" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtShippingAddress" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Address 2</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingAddress2" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtShippingAddress2" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        City</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingCity" runat="server" CssClass="input-medium" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtShippingCity" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        State</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlShippingState" runat="server" AutoPostBack="false" CssClass="input-medium" DataTextField="Name" DataValueField="StateProvinceId" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlShippingState" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Zip Code</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtShippingZipCode" runat="server" CssClass="input-medium" MaxLength="10" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtShippingZipCode" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtShippingZipCode" CssClass="text-error" Display="Dynamic" OnServerValidate="ZipCode_ServerValidate" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:CustomValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Country</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlShippingCountry" runat="server" AutoPostBack="true" CssClass="input-medium" DataTextField="Name" DataValueField="CountryId" OnSelectedIndexChanged="ddlShippingCountry_SelectedIndexChanged" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlShippingCountry" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Email Address</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtEmail" runat="server" CssClass="input-medium" MaxLength="250" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEmail" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtEmail" CssClass="text-error" Display="Dynamic" OnServerValidate="txtEmail_ServerValidate" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span6 well" style="min-height: 530px">
                <h5>Billing Info</h5>
                <div class="form-horizontal">
                    <div class="control-group">
                        <label class="control-label">
                        First Name</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingFirstName" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBillingFirstName" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Last Name</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingLastName" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBillingLastName" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Address</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingAddress" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBillingAddress" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Address 2</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingAddress2" runat="server" CssClass="input-medium" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBillingAddress2" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        City</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingCity" runat="server" CssClass="input-medium" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBillingCity" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        State</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlBillingState" runat="server" AutoPostBack="false" CssClass="input-medium" DataTextField="Name" DataValueField="StateProvinceId" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlBillingState" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Zip Code</label>
                        <div class="controls">
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtBillingZipCode" runat="server" CssClass="input-medium" MaxLength="10" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBillingZipCode" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtBillingZipCode" CssClass="text-error" Display="Dynamic" OnServerValidate="ZipCode_ServerValidate" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:CustomValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                        Country</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlBillingCountry" runat="server" AutoPostBack="true" CssClass="input-medium" DataTextField="Name" DataValueField="CountryId" OnSelectedIndexChanged="ddlBillingCountry_SelectedIndexChanged" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlBillingCountry" CssClass="text-error" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UpdateValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <h5>Attributes</h5>
        <uc:attributes ID="ucAttributes" runat="server" />
        <p>
            <asp:LinkButton ID="btnCancelModalPopup" runat="server" CausesValidation="false" CssClass="btn btn-danger"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
            <asp:LinkButton ID="btnSaveOrder" runat="server" CssClass="btn btn-success" OnClick="btnSaveOrder_Click" ValidationGroup="UpdateValidationGroup"><i class="icon-save"></i> Save</asp:LinkButton>
        </p>
    </p>
                    
    </asp:Panel>
    </div>
</asp:Content>
