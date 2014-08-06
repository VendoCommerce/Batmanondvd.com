<%@ Control Language="C#" AutoEventWireup="true" 
    Inherits="CSWeb.Admin.UserControls.RangeDateControlReport" EnableViewState="True"%>
<%@ Register TagPrefix="usercontrols1" TagName="DateControl" Src="DateControlReport.ascx" %>



<div class="controls text-center" style="margin-left: 0">
<asp:PlaceHolder ID="placeHolderDropDown" runat="server">
    <asp:DropDownList ID="dropDownListDate" runat="server" CssClass="input-medium">
    </asp:DropDownList>
    <asp:Label ID="labelDropDownSelectionSuffix" runat="server"></asp:Label>
<span class="help-inline"><div class="badge">OR</div></span>

</asp:PlaceHolder>
</div>
<div class="controls text-center" style="margin-left: 0; margin-bottom: 6px;">

<div class="input-append input-prepend">
<span class="add-on"><asp:Label ID="labelStart" runat="Server" /></span>
<usercontrols1:DateControl id="dateControlStart" class="input-daterange" runat="server" DisplayCalenderImage="true" />
<span class="add-on"><i class="icon-calendar"></i></span>
<%--<usercontrols:datecontrol id="dateControlStart" runat="server" DisplayCalenderImage="true" />--%>
</div>
</div>
<div class="controls text-center" style="margin-left: 0"> 
<div class="input-append input-prepend">
<span class="add-on"><asp:Label ID="labelEnd" runat="Server" /></span>
<usercontrols1:DateControl id="dateControlEnd" class="input-daterange" runat="server" DisplayCalenderImage="true" />
<span class="add-on"><i class="icon-calendar"></i></span>
</div>
</div>

<asp:CompareValidator ID="compareValidatorDateFields" runat="server" ControlToValidate="dateControlEnd" CssClass="text-error" ControlToCompare="dateControlStart" Operator="GreaterThanEqual" Type="Date" ErrorMessage="'To' date must be greater than 'From' date." Display="Dynamic">*</asp:CompareValidator>

