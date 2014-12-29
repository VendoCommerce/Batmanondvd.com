<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionList.aspx.cs" Inherits="CSWeb.Admin.VersionList" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Versions</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<span id="pageid" class="versions"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-list"></i> Versions</li>
</ul>
<h3 class="page-header page-header-top">Versions</h3>

<div class="row-fluid" style="margin-bottom: 12px">
<asp:ValidationSummary CssClass="text-error" ID="valError" runat="server" ShowSummary="True" DisplayMode="List" />
</div>
               <!-- Toolbar -->
                    <div class="push">
             <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew"><i class="icon-plus"></i> Add New Version</asp:LinkButton> <a href="VersionCategoryList.aspx" class="btn btn-primary"><i class="icon-th"></i> Traffic Categories</a>
                   
      </div>
                    <!-- END Toolbar -->
       
                
    <table class="table table-striped table-bordered">
<thead>
  <tr>
 <th class="span4">Version Name</th>
 <th class="span3">Report-Friendly Name</th>
 <th class="span2">Traffic Category</th>
 <th class="span2 text-center">Status</th>
 <th class="span1 text-center">Options</th>
 </tr><tbody>
        <asp:DataList runat="server" ID="dlVersionList" OnItemCommand="dlVersionList_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="span4">
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
                    <td class="span3">
                        <asp:Literal runat="server" ID='lblShortName'></asp:Literal>
                    </td>
                    <td class="span2">
                        <asp:Literal runat="server" ID='lblCategoy'></asp:Literal>
                    </td>
                    <td class="span2 text-center">
                        <asp:Literal runat="server" ID='lblStatus'></asp:Literal>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit this version"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-mini btn-danger" ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this version?')" ToolTip="Delete this version"><i class="icon-remove"></i></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td class="span4">
                        <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Version Name is a required field."
                            ControlToValidate="txtEditTitle">*</asp:RequiredFieldValidator>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"title")%>' MaxLength="50" CssClass="input-medium"></asp:TextBox>
                    </td>
                    <td class="span3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Report-Friendly Name is a required field." ControlToValidate="txtEditShortName">*</asp:RequiredFieldValidator>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"shortName")%>' MaxLength="50" CssClass="input-medium"></asp:TextBox>
                    </td>
                    <td class="span2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Category is a required field." ControlToValidate="ddlEditCategory">*</asp:RequiredFieldValidator>
                        <asp:DropDownList runat="server" ID='ddlEditCategory' CssClass="input-medium" />
                    </td>
                    <td class="span2 text-center"><div class="form-inline"><label class="checkbox"><asp:CheckBox ID="cbVisible"   runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Visible") %>'></asp:CheckBox>&nbsp;Active?</label></div>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-mini btn-success" ToolTip="Save Changes"><i class="icon-save"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-mini btn-danger" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel Changes"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:DataList>
        <asp:PlaceHolder ID="pnlAddCategory" runat="server" Visible="False">
            <tr>
                <td class="span4">
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Version Name is a required field."
                        ControlToValidate="txtTitle">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtTitle" runat="server" MaxLength="50" CssClass="input-medium" />
                </td>
                <td class="span3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Report-Friendly Name is a required field." ControlToValidate="txtShortName">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtShortName" runat="server" CssClass="input-medium" MaxLength="50"></asp:TextBox>
                </td>
                <td class="span2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Category is a required field." ControlToValidate="ddlCategory">*</asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" ID='ddlCategory' CssClass="input-medium" />
                </td>
                <td class="span2 text-center">
                    <asp:CheckBox ID="cbVisible" runat="server" />
                </td>
                <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="True" CssClass="btn btn-mini btn-success" ToolTip="Save Changes"><i class="icon-save"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" runat="server" CommandName="Cancel" OnCommand="btnAction_Command" CausesValidation="False" CssClass="btn btn-mini btn-danger" ToolTip="Cancel Changes"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                </td>
            </tr>
        </asp:PlaceHolder>
     </tbody>
    </table>

 </div>
                <!-- END Page Content -->

</asp:Content>
