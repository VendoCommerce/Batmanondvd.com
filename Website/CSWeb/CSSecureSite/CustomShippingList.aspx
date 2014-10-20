<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomShippingList.aspx.cs"
    Inherits="CSWeb.Admin.CustomShippingList" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Custom Shipping</title>
 </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
    <span id="pageid" class="shipping"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="shipping.aspx"><i class="icon-truck"></i> Shipping</a></li>
<li><i class="icon-list"></i> Custom Shipping List</li>
</ul>
<h3 class="page-header page-header-top">Custom Shipping List</h3>
  <div class="push">
       <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="CustomShipping.aspx"><i class="icon-plus"></i> Add Custom Shipping</asp:HyperLink>
         </div>
         
         
    <table class="table table-bordered table-striped">
       <thead>
        <tr>
            <th>
                Country
            </th>
            <th>
                State
            </th>
            <th>
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlVersionList" OnItemCommand="dlVersionList_ItemCommand"
            RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                               <asp:Literal runat="server" ID='lblCountryTitle'></asp:Literal>
                               <asp:Label runat="server" ID='lblPrefId' Visible="false"></asp:Label>
                    </td>
                    <td>
                           <asp:Literal runat="server" ID='lblStateTitle'></asp:Literal>
                    </td>
                    <td><div class="btn-group">
                        <asp:HyperLink ID="hlEdit" runat="Server" CssClass="btn btn-success btn-mini"><i class="icon-pencil"></i></asp:HyperLink> <asp:LinkButton ID="lbRemove" CssClass="btn btn-danger btn-mini" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this custom shipping option?')" CommandName="Delete"><i class="icon-remove"></i></asp:LinkButton>
                           </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
         </tbody>
       </table>
</div>

</asp:Content>
