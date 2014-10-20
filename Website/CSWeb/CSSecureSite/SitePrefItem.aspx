<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SitePrefItem.aspx.cs" Inherits="CSWeb.Admin.SitePrefItem" MasterPageFile="AdminSite.master" %>

<%@ Register TagPrefix="usercontrols1" TagName="DateControl" Src="usercontrols/DateControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="attributes" Src="UserControls/Attributes.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Site Preferences</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <span id="pageid" class="sitepref"></span>
<div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-cog"></i> Site Preferences</li>
</ul>
<h3 class="page-header page-header-top">Site Preferences</h3>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
    
<div class="push">
<a class="btn btn-primary" href="Shipping.aspx"><i class="icon-truck"></i> View Shipping</a> <a class="btn btn-warning" href="CountryList.aspx"><i class="icon-globe"></i> View Country List</a> <a class="btn btn-success" href="TaxList.aspx"><i class="icon-money"></i> View Tax List</a>


</div>
<p><asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error" DisplayMode="List" /></p>
    <div class="form-horizontal form-box">

 <h4 class="form-box-header">Site Options 
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Visible="false" CssClass="label label-important"></asp:Label></div></h4>
<div class="form-box-content">

<div class="control-group">
<label class="control-label">Site Name</label>
<div class="controls">
<asp:TextBox ID="txtSiteName" runat="server" CssClass="input-large" Text="Default."  MaxLength="200"/>
</div>
</div>

<div class="control-group">
<label class="control-label">Site URL</label>
<div class="controls">
 <asp:TextBox ID="txtSiteUrl" runat="server" CssClass="input-large" Text="Default."  MaxLength="200"/>
 </div>
</div>

 <asp:TextBox Visible="false" ID="txtTitle" runat="server" Text="Default."  MaxLength="200" />
         
<div class="control-group">
<label class="control-label">Site Logo</label>
<div class="controls"><asp:TextBox ID="txtImagePath" runat="Server" CssClass="input-large" MaxLength="200" /><span class="help-inline"><code>(Example: /content/images/admin_logo.jpg)</code></span>
</div>
</div>
                
<div class="control-group">
<label class="control-label">Order Process Option</label>
<div class="controls"><asp:DropDownList runat="server" ID="ddlOrderProcessList" runat="server" /></div></div>

<div class="control-group">
<label class="control-label">Shipping Option</label>
<div class="controls"><label class="checkbox"><asp:CheckBox ID="CbShippingOption" runat="server" AutoPostBack="false" Text="Include shipping cost in tax calculation." /></label>
                </div>
                </div>
                
<div class="control-group">
<label class="control-label">GeoTargeting Option</label>
<div class="controls"><label class="checkbox"><asp:CheckBox ID="cbGeoTarget" runat="server" AutoPostBack="false" Text="Turn on GeoTargeting Service." /></label>
                </div>
                </div>
                
                
<div class="control-group">
<label class="control-label">Payment Gateway</label>
<div class="controls"><label class="checkbox"><asp:CheckBox ID="cbPaymentGateway" runat="server" AutoPostBack="false" Text="Turn on Payment Gateway Service." /></label>
                </div>
                </div>


<div class="control-group">
<label class="control-label">Fulfillment House</label>
<div class="controls"><label class="checkbox"><asp:CheckBox ID="cbFulfillmentHouse" runat="server" AutoPostBack="false" Text="Turn on Payment Fulfillment House Service." /></label>
                </div>
                </div>
                
                
<div class="control-group">
<label class="control-label">Path Order Calc Date</label>
<div class="controls">
<usercontrols1:DateControl ID="dateControlStart" runat="server" IsRequired="true" ErrorMessage="Please Enter a valid date" ValidationEnabled="true" />
</div>
</div>

<div class="control-group">
<label class="control-label">Archive Data</label>
<div class="controls"><asp:TextBox ID="txtDays" runat="Server" Width="10%" MaxLength="3" />
<asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtDays" ID="Requiredfieldvalidator1" ValidationGroup="valError" ErrorMessage="*Archive Data is a required field.">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDays" Type="integer" ErrorMessage="*Archive Data must be an interger." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="error" ValueToCompare="0">*</asp:CompareValidator>
</div></div>
</div>

 <h4 class="form-box-header">Attributes</h4>
<div class="form-box-content">

      
 <uc:attributes ID="ucAttributes" runat="server" />
           
           
<tr runat="server" visible="false">
<td>Currency:
<asp:TextBox ID="tblCurrency" runat="server" Width="30%" MaxLength="2" /></td>
</tr>
 
<div class="form-actions">
 <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" CommandName="Cancel" CausesValidation="false" CssClass="btn btn-danger" OnCommand="btnAction_Command" TabIndex="10">
 <i class="icon-ban-circle"></i> Cancel</asp:LinkButton> 
 
 <asp:LinkButton runat="server" ID="btnSave" Text="Save" CommandName="Save" OnCommand="btnAction_Command" CssClass="btn btn-success" CausesValidation="true" TabIndex="11">
 <i class="icon-save"></i> Save</asp:LinkButton> 
</div>
</div>
</div>
</div>


</asp:Content>
