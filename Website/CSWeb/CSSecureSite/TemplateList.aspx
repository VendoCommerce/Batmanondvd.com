<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="CSWeb.Admin.TemplateList" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Upsell Templates</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <span id="pageid" class="upsell"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="pathlist.aspx"><i class="icon-gift"></i> Upsell Path List</a></li>
<li><i class="icon-list-alt"></i> Upsell Templates</li>
</ul>
<h3 class="page-header page-header-top">Upsell Templates</h3>

<p>
 <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error" ValidationGroup="valError" DisplayMode="List" />
 </p>

<div class="push">
<asp:HyperLink ID="lbItemAdd" runat="server" CssClass="btn btn-success" NavigateUrl="TemplateItem.aspx" Text="Add Template"><i class="icon-plus"></i> Add Template</asp:HyperLink>
</div>

<asp:DataList runat="server" ID="dlTemplateList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemCommand="dlTemplateList_ItemCommand" OnItemDataBound="dlTemplateList_ItemDataBound">
        <HeaderTemplate>
            <table class="table table-bordered table-striped">
               <thead>
                <tr>
                    <th>                    
                        Title&nbsp;&nbsp;<button class="btn btn-mini btn-inverse" data-toggle="tooltip" title="Click template below to view template details"><i class="icon-info-sign"></i></button>                       
                    </th>
                    <th>
                      Date Created
                    </th>
                    <th>
                        Expiration Date
                    </th>
                    <th class="span1 text-center">
                        Options
                    </th>
                </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr id="holderExpireDate" runat="server">
                <td>
                <a href="TemplateItem.aspx?templateId=<%# DataBinder.Eval(Container.DataItem, "TemplateId") %>">
                    <%# DataBinder.Eval(Container.DataItem, "Title") %>
                    </a>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "CreateDate")%>
                </td>
                <td>
                    <asp:Label ID="lblExpireDate" runat="server" />
                </td>
                <td class="span1 text-center"><div class="btn-group">
            <asp:LinkButton ID="lbCopy" CssClass="btn btn-mini btn-primary" runat="server" CausesValidation="True" CommandName="Copy"><i class="icon-copy"></i></asp:LinkButton> <asp:LinkButton ID="lbRemove" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this template?')" CommandName="Delete" CssClass="btn btn-mini btn-danger"><i class="icon-remove"></i></asp:LinkButton>
            </div>
                </td>
            </tr>
        </ItemTemplate>
    </asp:DataList>
</tbody>
</table>
</div>
</asp:Content>
