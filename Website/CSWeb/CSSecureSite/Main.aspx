<%@ Page Language="c#" CodeBehind="Main.aspx.cs" MasterPageFile="AdminSite.master"
    Inherits="CSWeb.Admin.Main" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Main Dashboard</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<span id="pageid" class="maindash">
<div id="page-content">
<ul id="nav-info" class="clearfix">
<li><i class="icon-home"></i></li>
</ul>
<div class="row-fluid">
<div class="span12">
<div class="dash-tile dash-tile-2x">
<!--<div class="dash-tile-header">
<i class="icon-file-alt"></i> Pages
</div>-->
<div class="dash-tile-content">
<div class="dash-tile-content-inner-fluid">
<ul id="dash-example-tabs" class="nav nav-tabs" data-toggle="tabs">
<asp:PlaceHolder ID="pnlHeader" runat="server" Visible="false">
<li class="active"><a href="#dash-example-tabs-catalog"><i class="icon-star"></i> Catalog</a></li>

<li><a href="#dash-example-tabs-customers"><i class="icon-group"></i> Users</a></li>
<li><a href="#dash-example-tabs-orders"><i class="icon-shopping-cart"></i> Order Manager</a></li>
<li><a href="#dash-example-tabs-support"><i class="glyphicon-life_preserver"></i> Support</a></li>
</asp:PlaceHolder>
<li id="reporttab"><a href="#dash-example-tabs-reports"><i class="icon-table"></i> Reports</a></li>
</ul>
<div class="tab-content">
<asp:PlaceHolder ID="pnlBody" runat="server" Visible="false">
<div class="tab-pane active" id="dash-example-tabs-catalog">
<ul class="thumbnails" data-toggle="gallery-options">
<li>

<a href="SitePrefItem.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-cog"></i></span><span class="dash-text">Site Preferences</span>
</a>
</li>
<li>

<a href="VersionList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-list"></i></span><span class="dash-text">Versions</span>
</a>
</li>
<li>

<a href="categoryList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-th"></i></span><span class="dash-text">Product Categories</span>
</a>
</li>
<li>

<a href="SKUList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-barcode"></i></span><span class="dash-text">SKUs</span>
</a>
</li>
<li>

<a href="Shipping.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-truck"></i></span><span class="dash-text">Shipping Options</span>
</a>
</li>
<li>

<a href="CountryList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-globe"></i></span><span class="dash-text">Country</span>
</a>
</li>
<li>

<a href="TaxList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-money"></i></span><span class="dash-text">Tax</span>
</a>
</li>
<li>

<a href="PathList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-gift"></i></span><span class="dash-text">Upsells</span>
</a>
</li>
<li>

<a href="CouponList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-tag"></i></span><span class="dash-text">Coupons</span>
</a>
</li>
<li>

<a href="EmailList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-envelope-alt"></i></span><span class="dash-text">Email Manager</span>
</a>
</li>
<li>

<a href="PaymentProviderList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-credit-card"></i></span><span class="dash-text">Payment Provider</span>
</a>
</li>
<li>

<a href="FulfillmentHouseProviderList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-road"></i></span><span class="dash-text">Fulfillment House</span>
</a>
</li>
<li>

<a href="cacheutility.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-repeat"></i></span><span class="dash-text">Cache Utility</span>
</a>
</li>
<li>

<a href="ErrorLog.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-warning-sign"></i></span><span class="dash-text">Error Log</span>
</a>
</li>
<li>

<a href="ResourceList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-check"></i></span><span class="dash-text">Resources & Validation</span>
</a>
</li>

<li>

<a href="Attributes.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-sort-by-attributes"></i></span><span class="dash-text">Attributes Manager</span>
</a>
</li>
</ul>
</div>
<div class="tab-pane" id="dash-example-tabs-customers">
<ul class="thumbnails" data-toggle="gallery-options">
<li>

<a href="customerList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-user"></i></span><span class="dash-text">Customers</span>
</a>
</li>
<li>

<a href="users.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-key"></i></span><span class="dash-text">Administrators</span>
</a>
</li>
</ul>
</div>
<div class="tab-pane" id="dash-example-tabs-orders">
<ul class="thumbnails" data-toggle="gallery-options">
<li>

<a href="OrderList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-shopping-cart"></i></span><span class="dash-text">Orders</span>
</a>
</li>
<li>

<a href="CustomFieldList.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-pencil"></i></span><span class="dash-text">Custom Fields</span>
</a>
</li>
</ul>
</div>
<div class="tab-pane" id="dash-example-tabs-support">
<ul class="thumbnails" data-toggle="gallery-options">
<li>
<a href="docs_formpage.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-edit"></i></span><span class="dash-text">Adding Form Page</span>
</a>
</li>
<li>
<a href="docs_tablepage.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-table"></i></span><span class="dash-text">Adding Table Page</span>
</a>
</li>
<li>
<a href="docs_reportpage.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-bar-chart"></i></span><span class="dash-text">Adding Report Page</span>
</a>
</li>
<li>
<a href="docs_icons.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-tint"></i></span><span class="dash-text">Icon Reference</span>
</a>
</li>
</ul>
</div>
</asp:PlaceHolder>
<div class="tab-pane" id="dash-example-tabs-reports">
<ul class="thumbnails" data-toggle="gallery-options">
<li>

<a href="sreports.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-star-empty"></i></span><span class="dash-text">Standard Report</span>
</a>
</li>
<li>

<a href="versionreport.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-list"></i></span><span class="dash-text">Version Report</span>
</a>
</li>
<li>

<a href="sidreport.aspx" class="dash-icons"><span class="iconwrap"><i class="icon-bullhorn"></i></span><span class="dash-text">SID Report</span>
</a>
</li>
</ul>
</div>
</div>
</div>
</div>
</div>
</div>
</div>



</div>



          
      
           
 
</asp:Content>
