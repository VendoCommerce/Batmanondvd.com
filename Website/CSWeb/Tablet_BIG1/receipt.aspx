<%@Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Tablet_BIG1.Store.receipt" EnableViewState="true" EnableSessionState="True" %>
<%@ Register Src="UserControls/CheckoutThankYouModule.ascx" TagName="Form"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>
 
<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1.0">
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Checkout</title>
<meta name="description" content=""/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key="JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<script src="/Scripts/NoBack.js"></script>
<link href="/styles/global_big1_tablet.css" rel="stylesheet" type="text/css" />
</head>
<body id="receipt">

  <!--#include file="header.html"-->

  <div class="container_cart">
     <uc1:Form ID="Form1" runat="server" />
  </div>

  <!--#include file="footer.html"-->

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />
</body>
</html>
  

