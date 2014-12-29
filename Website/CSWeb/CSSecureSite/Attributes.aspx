<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attributes.aspx.cs" Inherits="CSWeb.Admin.Attributes" MasterPageFile="AdminSite.master" EnableSessionState="True" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Attribute Manager</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <span id="pageid" class="attributes"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-sort-by-attributes"></i> Attribute Manager</li>
</ul>
<h3 class="page-header page-header-top">Attribute Manager</h3>
    
    <div class="row-fluid">
    <div class="span6">
    <h4>Objects</h4>
    <table class="table table-condensed table-striped table-bordered">
    <thead>
<tr>
  <th>Name</th>
 <th>Values Table Name</th>
 <th>Primary Key Column Name</th>
</tr>
</thead>
 <tbody>
    <asp:DataList ID="dgObjects" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <ItemTemplate>        
                 <tr> <td>
                    <%#Eval("Name")%>
                </td>
                <td>
              <%#Eval("ValuesTableName")%>
      </td>
      <td>  <%#Eval("PrimaryKeyColName")%>
 </td>
 </tr>
 </ItemTemplate>
    </asp:DataList>
</tbody></table>
  </div>
</div>

<div class="row-fluid" style="margin-bottom: 10px">
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" />
    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" />
    </div>
    
  <h4>Attributes<div class="pull-right" style="margin-bottom: 6px; font-weight: normal;"><asp:LinkButton ID="lbAddAttribute2" runat="server" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="false" CssClass="btn btn-success"><i class="icon-plus"></i> Add New Attribute</asp:LinkButton></div></h4>
        <table class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Description
                    </th>
                    <th class="span2">
                        Value Type
                    </th>
                    <th class="span2">
                        Object Associations
                    </th>
                    <th>Options                
                    </th>
            
                </tr>
            </thead>
            <tbody>
                <asp:DataList ID="dlAttributes" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemCommand="dlItem_ItemCommand" OnDataBinding="dlItem_DataBinding" OnItemDataBound="dlItem_ItemDataBound"> 
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litAttributeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' />
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                            </td>
                            <td class="span2">
                                <%# DataBinder.Eval(Container.DataItem, "ValueTypeName") %>
                            </td>
                            <td class="span2">
                                <asp:Literal ID="litAssociations" runat="server" />
                            </td>
                            <td><div class="btn-group">
                                    <asp:LinkButton ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" CssClass="btn btn-mini btn-success"><i class="icon-pencil"></i></asp:LinkButton><asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this Attribute?')" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-mini btn-danger"><i class="icon-remove"></i></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Attribute Name required" ControlToValidate="txtAttributeName">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtAttributeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' MaxLength="50" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Attribute Description required" ControlToValidate="txtDescription">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' MaxLength="100" />
                            </td>
                            <td class="span2">
                                <asp:DropDownList ID="ddlAttributeValueType" runat="server" />
                            </td>
                            <td class="span2">
                                <asp:Literal ID="litAssociations" runat="server" /> &nbsp;  
                                <asp:LinkButton ID="lbEditAttributeAssociation" runat="server" OnCommand="lbEditAttributeAssociation_Command" CssClass="btn btn-mini btn-success" ToolTip="Edit Associations" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AttributeId") + "," + DataBinder.Eval(Container.DataItem, "Name") %>'><i class="icon-pencil"></i></asp:LinkButton>
                            </td>
                            <td><div class="btn-group">
                                    <asp:LinkButton ID="lbSave" runat="server" CausesValidation="True" CommandName="Update" ToolTip="Save" CssClass="btn btn-mini btn-success"><i class="icon-save"></i></asp:LinkButton><asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel" CssClass="btn btn-mini btn-danger"><i class="icon-ban-circle"></i></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <FooterTemplate>
                
                        <tr id="addNewContainer" runat="server" visible="false">
                            <td>
                                <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Attribute Name required" ControlToValidate="txtAttributeName">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtAttributeName" runat="server" MaxLength="50" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Attribute Description required" ControlToValidate="txtDescription">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtDescription" runat="server" MaxLength="500" />
                            </td>
                            <td class="span2">
                                <asp:DropDownList ID="ddlAttributeValueType" runat="server" />                        
                            </td>
                            <td class="span2">
                                (associate to object after creation)
                            </td>
                            <td><div class="btn-group">
                                    <asp:LinkButton ID="lbInsert" runat="server" CausesValidation="True" CommandName="Insert" ToolTip="Save" CssClass="btn btn-mini btn-success"><i class="icon-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel" CssClass="btn btn-mini btn-danger"><i class="icon-ban-circle"></i></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <asp:LinkButton ID="lbAddAttribute" runat="server" CommandName="Add" CausesValidation="false" CssClass="btn btn-success"><i class="icon-plus"></i> Add New Attribute</asp:LinkButton>
                            </td>
                        </tr>--%>
                    </FooterTemplate>
                </asp:DataList>
                
                 <asp:PlaceHolder ID="pnlAddCategory" runat="server" Visible="False">
                <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="valName1" runat="server" Display="Dynamic" ErrorMessage="Attribute Name required" ControlToValidate="txtAttributeName1">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtAttributeName1" runat="server" MaxLength="50" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Attribute Description required" ControlToValidate="txtDescription1">*</asp:RequiredFieldValidator>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtDescription1" runat="server" MaxLength="500" />
                            </td>
                            <td class="span2">
                                <asp:DropDownList ID="ddlAttributeValueType1" runat="server" />                        
                            </td>
                            <td class="span2">
                                (associate to object after creation)
                            </td>
                            <td><div class="btn-group">
                                    <asp:LinkButton ID="lbInsert" runat="server" CausesValidation="True" CommandName="Insert" OnCommand="btnAction_Command"  ToolTip="Save" CssClass="btn btn-mini btn-success"><i class="icon-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" OnCommand="btnAction_Command"  ToolTip="Cancel" CssClass="btn btn-mini btn-danger"><i class="icon-ban-circle"></i></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
        </asp:PlaceHolder>
            </tbody>
        </table>
        
        
    <asp:LinkButton ID="lbAddAttribute" runat="server" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="false" CssClass="btn btn-success"><i class="icon-plus"></i> Add New Attribute</asp:LinkButton>
  
     


        <asp:LinkButton ID="btn" runat="server" style="visibility: hidden;" /> 
    <!-- dummy button for use by modal popup -->
    
    
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="btn"
        PopupControlID="pnlModalPopUpPanel" PopupDragHandleControlID="pnlModalPopUpPanel" />

    <asp:Panel ID="pnlModalPopUpPanel" runat="server" CssClass="modalPopup">
     
     
     <h4>Attributes Manager</h4>
        <h5>Attribute Name: <asp:Label ID="lblAttributeName" runat="server" Font-Bold="true" /></h5>
        
        <asp:HiddenField ID="hidAttributeId" runat="server" />
     
        <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th class="span2">Object</th>
                <th>Object Attribute Type</th>
                <th>Description</th>
                <th>Display Label</th>
            </tr>
            </thead>
            <tbody>
            <asp:DataList ID="dlAssociations" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" 
                OnItemDataBound="dlAssociations_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="span2"><div class="checkbox">
                            <asp:CheckBox ID="chkObjectAssociation" runat="server" />
                            <asp:HiddenField ID="hidObjectId" runat="server" />
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlObjectAttributeType" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtDescription" runat="server" MaxLength="500" />
                        </td>
                        <td>
                            <asp:TextBox  AutoCompleteType="Disabled" ID="txtDisplayLabel" runat="server" MaxLength="100" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </tbody>
        </table>

   <p>
        <asp:LinkButton ID="btnCancelModalPopup" CssClass="btn btn-danger" runat="server" CausesValidation="false" OnClick="btnCancelModalPopup_Click"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
        
        <asp:LinkButton ID="btnSaveOrder" runat="server" Text="Save" OnClick="btnSaveOrder_Click" CssClass="btn btn-success"><i class="icon-save"></i> Save</asp:LinkButton> 
 </p>
   
    </asp:Panel>



    <script type="text/javascript">
        var ctlId = '<asp:Literal ID="litFocusCtrlId" runat="server" />';
        if (ctlId != '' && document.getElementById(ctlId) != null)
            document.getElementById(ctlId).focus();
    </script>
    
    
    </div>
</asp:Content>

