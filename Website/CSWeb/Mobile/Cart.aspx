<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.cart" EnableSessionState="True" MaintainScrollPositionOnPostback="true"  Async="true"%>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>
<%@ Register Src="UserControls/BillingShippingCreditForm.ascx" TagName="BillingShippingCreditForm" TagPrefix="uc" %>
<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1" />
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Checkout</title>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript" src="/Scripts/global_mobile.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery.cookie.min.js"></script>
    <script type="text/javascript" src="/Scripts/cstracking.js"></script>
</head>
 
<body>
 <form runat="server" id="fm1">
  
<div class="container">
<!--#include file="popups.html"-->
<!--#include file="header_cart.html"-->

<div class="content">

 <uc:BillingShippingCreditForm ID="bsfcBillingShippingCreditForm" runat="server" RedirectUrl="AddProduct.aspx" />
 </div>
 <!--#include file="footer.html"-->
</div>

<uc:TrackingPixels ID="TrackingPixels" runat="server" />
</form>
</body>
</html>
