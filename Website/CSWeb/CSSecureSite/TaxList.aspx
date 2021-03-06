﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxList.aspx.cs" Inherits="CSWeb.Admin.TaxList"
    MasterPageFile="AdminSite.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Tax</title>
  
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {

            var key = window.event ? evt.keyCode : evt.which;
            if (key > 31 && (key < 48 || key > 57))
                return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <span id="pageid" class="tax"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-money"></i> Tax</li>
</ul>
<h3 class="page-header page-header-top">
   Tax
   

</h3> 
    
   <ul>
   <li>Conversion Systems uses a hierarchical tax rate system.</li>
   <li>You can configure tax rates at any level of the hierarchy.</li>
   </ul>
    
   <p><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></p>

   <p>
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
      </p>  
        
   <div class="push">
 <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success"><i class="icon-plus"></i> Add a Country</asp:LinkButton>
    </div>
    
    
    <asp:UpdatePanel ID="UPCountryList" runat="Server" UpdateMode="Always">
        <ContentTemplate>
            <table class="table table-bordered table-striped">
			<thead>
                <tr>
                    <th>
                        Country
                    </th>
                    <th>
                        Tax %
                    </th>
                    <th>
                        Options
                    </th>
                </tr>
                </thead>
                <tbody>
                <asp:DataList runat="server" ID="dlCountryList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlCountryList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox  AutoCompleteType="Disabled" runat="server" ID='txtOrderNo' MaxLength="5" CssClass="input-mini"></asp:TextBox> %
                                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtOrderNo" ID="valReqCartDesc" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Tax % is a required field.">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmpValPercentage" runat="server" ControlToValidate="txtOrderNo" Type="Double" ErrorMessage="Tax % must be in a double format." Operator="GreaterThanEqual" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:HyperLink ID="hlAddState" runat="Server" CssClass="btn btn-mini btn-info"><i class="icon-plus"></i> Add States</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:DataList>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeThePopup" TargetControlID="lbItemAdd" PopupControlID="pnlModalPopUpPanel" CancelControlID="btnCancelModalPopup" PopupDragHandleControlID="pnlModalPopUpPanel" />
    <!-- Panel ID should match with ModalPopupExtender PopupControlID and Dummy Button should match with TargetControlID -->
	<asp:Panel ID="pnlModalPopUpPanel" runat="server" CssClass="modalPopup">
 
 <h4><asp:Literal ID="litHeader" Text="Add Country Tax" runat="server" /></h4>
<p>
		<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-error"
			ValidationGroup="valErrorPopup" DisplayMode="List" />
            </p>
            
              <div class="form-horizontal well">
              
              <div class="control-group">
              
              <label class="control-label">Country</label>
			<div class="controls"><asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="false" CssClass="input-medium"></asp:DropDownList>
                  </div>
                  </div>
                  
                         <div class="control-group">
              
              <label class="control-label">Tax</label>
			<div class="controls"> 
                 <asp:TextBox  AutoCompleteType="Disabled" ID="txtPercentage" runat="server" CausesValidation="true" MaxLength="5" CssClass="input-mini" /> %
					<asp:RequiredFieldValidator ID="reqValPercentage" runat="server" ControlToValidate="txtPercentage" ErrorMessage="Percentage is required." ValidationGroup="valErrorPopup" CssClass="text-error">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpValPercentage" runat="server" ControlToValidate="txtPercentage" Type="Double" ErrorMessage="Percentage must be in a double format." Operator="GreaterThanEqual" ValidationGroup="valErrorPopup" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator>
                   </div>
                  </div>
                  
             <div class="control-group">
<div class="controls">       
					<asp:LinkButton ID="btnCancelModalPopup" runat="server" CausesValidation="false" CssClass="btn btn-danger"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton> <asp:LinkButton ID="btnChooseProduct" runat="server" OnClick="btnAdd_Country" CssClass="btn btn-success" ValidationGroup="valErrorPopup"><i class="icon-save"></i> Save</asp:LinkButton>
                    </div>
          </div>
          </div>
              
     </asp:Panel>
  
   
   <p>
       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" OnCommand="btnSave_OnClick" CssClass="btn btn-danger"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
    <asp:LinkButton ID="imgSave" CssClass="btn btn-success" OnCommand="btnSave_OnClick" CommandName="Save" CausesValidation ="True" runat="server" ValidationGroup="valError"></i> Save</asp:LinkButton>
</p>

</div>
   
</asp:Content>
