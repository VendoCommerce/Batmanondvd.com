<%@ Control Language="C#" AutoEventWireup="true" Inherits="CSWeb.Admin.UserControls.RangeDateControl" EnableViewState="True"%>

<%@ Register TagPrefix="usercontrols" TagName="DateControl" Src="DateControl.ascx" %>

  
                
        
                
       
                <label><asp:Label ID="labelStart" runat="Server" /></label> <usercontrols:datecontrol id="dateControlStart" runat="server" />&nbsp;&nbsp;<label> <asp:Label ID="labelEnd" runat="Server" /></label> <usercontrols:datecontrol id="dateControlEnd" runat="server" /><asp:CompareValidator ID="compareValidatorDateFields" runat="server" ControlToValidate="dateControlEnd" ControlToCompare="dateControlStart" Operator="GreaterThanEqual" Type="Date" ErrorMessage="'To' date must be greater than 'From' date." CssClass="text-error" Display="Dynamic">*</asp:CompareValidator>&nbsp;&nbsp;<div class="badge">OR</div>&nbsp;&nbsp;
           
    <asp:PlaceHolder ID="placeHolderDropDown" runat="server">
        <asp:DropDownList ID="dropDownListDate" runat="server" CssClass="input-medium"></asp:DropDownList><asp:Label ID="labelDropDownSelectionSuffix" runat="server"></asp:Label>
                </asp:PlaceHolder>            
		







