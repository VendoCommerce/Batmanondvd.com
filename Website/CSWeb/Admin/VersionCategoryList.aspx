<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionCategoryList.aspx.cs" Inherits="CSWeb.Admin.VersionCategoryList"   MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Version Traffic Category List</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <span id="pageid" class="versions"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="VersionList.aspx"><i class="icon-list"></i> Versions</a></li>
<li><i class="icon-th"></i> Traffic Categories</li>
</ul>
<h3 class="page-header page-header-top">Traffic Categories</h3>

<div class="row-fluid" style="margin-bottom: 12px">
    <asp:ValidationSummary CssClass="text-error" ID="valError" runat="server"
        ShowSummary="True" DisplayMode="List" />
        </div>
        
         <!-- Toolbar -->
<div class="push">
<asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew"><i class="icon-plus"></i> Add New Traffic Category</asp:LinkButton> <a href="versionlist.aspx" class="btn btn-primary"><i class="icon-arrow-left"></i> Back to Versions</a>
      </div>
      <!-- END Toolbar -->
        
  <table class="table table-striped table-bordered">
<thead>
<tr>
 <th>Category Name</th>
 <th class="span1 text-center">Options</th>
   </tr>
 </thead>
<tbody>
        <asp:DataList runat="server" ID="dlVersionList" OnItemCommand="dlVersionList_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
       

                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-mini btn-danger" ID="lbRemove" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this category?')"  CommandName="Delete" ToolTip="Delete"><i class="icon-remove"></i></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </ItemTemplate>
            
            
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Category Name is a required field." ControlToValidate="txtEditCategory">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEditCategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"title")%>' MaxLength="100"></asp:TextBox>
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
                <td>
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Category Name is a required field." ControlToValidate="txtCategory">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtCategory" runat="server" MaxLength="100" />
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

<p><a href="versionlist.aspx" class="btn btn-primary"><i class="icon-arrow-left"></i> Back to Versions</a></p>
 </div>
                <!-- END Page Content -->

</asp:Content>
