<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.BIG2.Store.cart" EnableSessionState="True" MaintainScrollPositionOnPostback="true" Async="true" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>
<%@ Register Src="UserControls/ShippingBillingCreditForm.ascx" TagName="ShippingBillingCreditForm" TagPrefix="uc" %> 
<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Checkout</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key="JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="/styles/global.css" rel="stylesheet" type="text/css" />
</head>

 
<body id="cart">
 <form runat="server" id="fm1">
  <!-- loader overlay front end -->
        <div id="mask" style="position:fixed; width: 100%; height: 100%; background: url(//d1kg9stb0ddjcv.cloudfront.net/images/mask_bg.png) repeat; top: 0; left: 0; z-index: 9000; visibility:hidden;">
            <div style="margin: 250px auto; position:relative; width: 312px; height: 170px; background: white; text-align:center;">

                <p style="margin: 0; padding: 20px 0 10px 0;">
         <img src="//d1kg9stb0ddjcv.cloudfront.net/images/loader.gif">
                </p>
                <p style="color: black; text-align:center; font-size: 12px; margin: 0 23px; line-height: 19px;">Your order is currently being processed. <br />
                    Please do not exit or refresh this page to ensure that your order is processed accurately.</p>

            </div>

        </div>
        <!-- end loader front end -->

<!--#include file="header_cart.html"-->
<div class="container_cart">
    <uc:ShippingBillingCreditForm ID="bscfShippingBillingCreditForm" runat="server" RedirectUrl="Store/AddProduct.aspx" />
</div>

<!--#include file="footer.html"-->
  <uc:TrackingPixels ID="TrackingPixels" runat="server" />
 </form>
</body>
</html>


