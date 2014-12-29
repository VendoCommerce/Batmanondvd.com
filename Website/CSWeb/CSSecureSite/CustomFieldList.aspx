<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomFieldList.aspx.cs"
    Inherits="CSWeb.Admin.CustomFieldList" MasterPageFile="AdminSite.master" %>
    <asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<title>Custom Fields</title>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <span id="pageid" class="customfields"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-shopping-cart"></i> Order Manager</li>
<li><i class="icon-edit"></i> Custom Fields</li>
</ul>
<h3 class="page-header page-header-top">Custom Fields</h3>  

<p>
    <asp:ValidationSummary CssClass="text-error" ID="valError" runat="server" ShowSummary="True"
        DisplayMode="List" />
  </p>
                
          <div class="push">
          <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew"><i class="icon-plus"></i> Add New Custom Field</asp:LinkButton>
                </div>
                
        
    <table class="table table-bordered table-striped">
       <thead>
        <tr class="header">
            <th>
                Field Name
            </th>
            <th class="span3 text-center">
                Active
            </th>
            <th class="span1 text-center">
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlCustomFieldList" OnItemCommand="dlCustomFieldList_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlCustomFieldList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
                    <td class="span3 text-center">
                        <asp:Literal runat="server" ID='lblStatus'></asp:Literal>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit this version"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-mini btn-danger" ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this custom field?')" ToolTip="Delete this version"><i class="icon-remove"></i></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Title is a required field." CssClass="text-error" ControlToValidate="txtEditCustomField">*</asp:RequiredFieldValidator>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditCustomField" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FieldName")%>' MaxLength="100"></asp:TextBox>
                    </td>
                    <td class="span3 text-center"> 
                        
                    </td>
                    <td class="span1 text-center">
                      <div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-mini btn-success" ToolTip="Save Changes"><i class="icon-save"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-mini btn-danger" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel Changes"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:DataList>
        <asp:PlaceHolder ID="pnlAddCategory" runat="server" Visible="False">
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Field Name is a required field." ControlToValidate="txtCustomField">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtCustomField" runat="server" MaxLength="100" />
                </td>
                <td>
                </td>
                <td>
                   <div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="True" CssClass="btn btn-mini btn-success" ToolTip="Save Changes"><i class="icon-save"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" runat="server" CommandName="Cancel" OnCommand="btnAction_Command" CausesValidation="False" CssClass="btn btn-mini btn-danger" ToolTip="Cancel Changes"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                </td>
            </tr>
        </asp:PlaceHolder>
        </tbody>
    </table>
    </div>
</asp:Content>
