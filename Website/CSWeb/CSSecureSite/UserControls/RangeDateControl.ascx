<%@ Control Language="C#" AutoEventWireup="true" 
    Inherits="CSWeb.Admin.UserControls.RangeDateControl" EnableViewState="True"%>
<%@ Register TagPrefix="usercontrols" TagName="DateControl" Src="DateControl.ascx" %>


<div class="form">

<asp:PlaceHolder ID="placeHolderDropDown" runat="server">
   <div class="control-group">
    <asp:DropDownList ID="dropDownListDate" runat="server" CssClass="input-medium">
    </asp:DropDownList>
    <asp:Label ID="labelDropDownSelectionSuffix" runat="server"></asp:Label>
<span class="help-inline"><div class="badge">OR</div></span>
</div>
</asp:PlaceHolder>
  <div class="control-group pull-left" style="margin-right: 10px">
<asp:Label ID="labelStart" runat="Server" />
<usercontrols:DateControl id="dateControlStart" runat="server" DisplayCalenderImage="true" />
</div>

 <div class="control-group pull-left">
<asp:Label ID="labelEnd" runat="Server" />
<usercontrols:DateControl id="dateControlEnd" runat="server" DisplayCalenderImage="true" />

<asp:CompareValidator ID="compareValidatorDateFields" runat="server" ControlToValidate="dateControlEnd" CssClass="text-error" ControlToCompare="dateControlStart" Operator="GreaterThanEqual" Type="Date" ErrorMessage="'To' date must be greater than 'From' date." Display="Dynamic">*</asp:CompareValidator>
</div>

<div class="clearfix"></div>
</div>