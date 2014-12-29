<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" MasterPageFile="AdminSite.master"
    Inherits="CSWeb.Admin.CategoryList" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Product Categories</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <span id="pageid" class="prodcat"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-th"></i> Product Categories</li>
</ul>
<h3 class="page-header page-header-top">Product Categories</h3>

<div class="row-fluid" style="margin-bottom: 12px">
    <asp:ValidationSummary CssClass="text-error" ID="valError" runat="server" ShowSummary="True" DisplayMode="List" /></div>
 <div class="push">
<asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew"><i class="icon-plus"></i> Add Product Category</asp:LinkButton>
</div>
 <table class="table table-striped table-bordered">
<thead>
<tr>
  <th>Product Category Name</th>
 <th class="span2 text-center">Status</th>
 <th class="span2 text-center">Order</th>
 <th class="span1 text-center">Options</th>
</tr>
</thead>
 <tbody>
<asp:DataList runat="server" ID="dlCategoryList" OnItemCommand="dlCategory_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlCategoryList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
                    <td class="span2 text-center">
                        <asp:Literal runat="server" ID='lblStatus'></asp:Literal>
                    </td>
                    <td class="span2 text-center">
                        <asp:Literal runat="server" ID='lblOrder'></asp:Literal>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-mini btn-danger" ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this category?')" ToolTip="Delete"><i class="icon-remove"></i></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </ItemTemplate>
            
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Product Category Name is a required field." ControlToValidate="txtEditCategory">*</asp:RequiredFieldValidator>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditCategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"title")%>' MaxLength="100"></asp:TextBox>
                    </td>
                    <td class="span2 text-center"><div class="form-inline"><label class="checkbox"> <asp:CheckBox ID="cbVisible" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Visible") %>'></asp:CheckBox>&nbsp;Active?</label></div>
                    </td>
                    <td class="span2 text-center">
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txEdittorder" runat="server" CssClass="input-mini text-center" MaxLength="2" Text='<%#DataBinder.Eval(Container.DataItem,"orderNo")%>' />
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
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Category Name is a required field." ControlToValidate="txtCategory">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtCategory" runat="server" MaxLength="100" />
                </td>
                <td class="span2 text-center">&nbsp;
                </td>
                <td class="span2 text-center">
                  <asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtorder" Type="Integer" ErrorMessage="* Order number must be an integer." Operator="DataTypeCheck" ValidationGroup="valError" ValueToCompare="0">*</asp:CompareValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtorder" runat="server" MaxLength="2" CssClass="input-mini text-center" />
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
