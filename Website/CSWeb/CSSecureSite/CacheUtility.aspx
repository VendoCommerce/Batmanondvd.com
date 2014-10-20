<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheUtility.aspx.cs" Inherits="CSWeb.Admin.CacheUtility" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Cache Utility</title>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <span id="pageid" class="cache"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-repeat"></i> Cache Utility</li>
</ul>
<h3 class="page-header page-header-top">Cache Utility</h3>


<p>Cached Items: <span class="text-info"><asp:Label ID="lbltext" runat="server" Text=""></asp:Label></span></p>

<div class="form-inline" style="margin-bottom: 20px">
<asp:DropDownList ID="ddlList" runat="Server" AutoPostBack="false" CssClass="input-large"></asp:DropDownList> <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-primary" OnCommand="btnAction_Command" CommandName="Cache"><i class="icon-repeat"></i> Reset Cache</asp:LinkButton>
</div>

<p>
<asp:LinkButton ID="lbViewCache" runat="server" OnClick="lbViewCache_Click" CssClass="btn btn-warning"><i class="icon-eye-open"></i> View Cache</asp:LinkButton></p>

<p>
Cached Content: 
</p>

<p>
<asp:Label ID="lblCacheContent" runat="server" Text="(Click View Cache link)" />
</p>

</div>
</asp:Content>

