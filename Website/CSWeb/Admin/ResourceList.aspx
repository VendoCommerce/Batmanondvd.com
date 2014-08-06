<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceList.aspx.cs" Inherits="CSWeb.Admin.ResourceList"
    MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Validation</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <span id="pageid" class="validation"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-check"></i> Resources & Validation</li>
</ul>
<h3 class="page-header page-header-top">Resources & Validation
<div style="margin-left: 10px; display: inline; font-style: normal;">
<asp:Label ID="lblSuccess" runat="server" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Visible="false" CssClass="label label-important"></asp:Label>
</div>
</h3>
               
                
                
    <div class="row-fluid" style="margin-bottom: 12px">            
    <asp:ValidationSummary CssClass="text-error" ID="valError" runat="server"
        ShowSummary="True" DisplayMode="List" /></div>
 <div class="push">
 <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew" Text="Add New Resource"><i class="icon-plus"></i> Add New Validation</asp:LinkButton> <asp:LinkButton ID="imgSave" CssClass="btn btn-primary" OnCommand="btnSave_OnClick" CommandName="Save" runat="server" ValidationGroup="valError"><i class="icon-refresh"></i> Reset Resource Cache</asp:LinkButton>
</div>
         
    <table class="table table-striped table-bordered">
       <thead>
        <tr>
            <th>
                Key
            </th>
            <th>
                Value
            </th>
            <th>
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlItemList" RepeatLayout="Flow" RepeatDirection="Horizontal"
            OnItemCommand="dlItem_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Key") %>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Value") %>
                    </td>
                    <td><div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-success" ToolTip="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this validation?')" CommandName="Delete" CssClass="btn btn-mini btn-danger" ToolTip="Delete"><i class="icon-remove"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="valName" runat="server" Display="Dynamic" ErrorMessage="Key is a required field." ControlToValidate="txtEditKeyName">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEditKeyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Key")%>' MaxLength="100"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Value is a required field." ControlToValidate="txtEditValueName">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEditValueName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Value")%>' MaxLength="1000"></asp:TextBox>
                    </td>
                    <td><div class="btn-group">
                        <asp:LinkButton ID="lbSave" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-mini btn-success" ToolTip="Save"><i class="icon-save"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-mini btn-danger" ToolTip="Cancel"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:DataList>
        <asp:PlaceHolder ID="pnlAddCategory" runat="server" Visible="False">
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Key is a required field." ControlToValidate="txtKeyName">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtKeyName" runat="server" MaxLength="100" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Value is a required field." ControlToValidate="txtValueName">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtValueName" runat="server" MaxLength="1000" />
                </td>
                <td><div class="btn-group">
                    <asp:LinkButton ID="lbSave" runat="server" CssClass="btn btn-mini btn-success" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="True" ToolTip="Save"><i class="icon-save"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" CssClass="btn btn-mini btn-danger" runat="server" CommandName="Cancel" OnCommand="btnAction_Command" CausesValidation="False" ToolTip="Cancel"><i class="icon-ban-circle"></i></asp:LinkButton>
                </div>
                </td>
            </tr>
        </asp:PlaceHolder>
        </tbody>
    </table>

<%--<p>
    
</p>
--%>

</div>
</asp:Content>
