<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Return Policy</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="../styles/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="clearfix">
        <div class="content_logo"><a href="index.aspx"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo.png" width="194" height="156" alt="Batman Classic TV Series" class="block" /></a></div>
        <div class="content_hdr">
            <h2 class="f50">Return Policy</h2>
            <h3>A true crimefighter is prepared for everything!</h3>
        </div>
    </div>
    
    <div style="padding: 16px 66px 30px 66px;">
        <h4 class="f18 bold pad12">Returns</h4>
        
        <p>At Warner Bros. Home Entertainment (WBHE), our goal is your complete satisfaction. You may return any new item purchased directly from the WBHE Classic Batman Campaign for any reason within 30 days for a refund. The item must be in the original, unopened factory shrink wrap package to be accepted back for a refund. You can also return any damaged or defective item within 30 days for a replacement. For items purchased from October 20, 2014 to December 16, 2014, returns will be accepted through January 31, 2015.  </p>

        <p>Please note we cannot accept any Warner Bros. merchandise (including Blu-ray discs and DVDs) purchased at any other retailers including the WBShop.</p>
        
        <p>For refunds, please include a copy of the packing slip and write the reason for the return. We recommend using carrier that provides you with a tracking number, such as UPS, FedEx or USPS Priority Mail. Any shipping and handling charges you incur in shipping product to us will not be refunded. All returns should be sent to the following address: </p>
        
        <p style="line-height: 21px;">
            BATMAN ON DVD<br />
            ATTN: RETURNS<br />
            1093 HIGHVIEW DRIVE<br />
            WEBBERVILLE, MICHIGAN 48892
        </p>
        
        <p>For damaged or defective items, contact our Customer Service Department at <a href="mailto:batmanondvd@datapakservices.com" style="color: #000; text-decoration: none;">batmanondvd@datapakservices.com</a>, or call toll free Monday through Friday 8A – 8P EST or Saturday 9A-6P EST at 800-839-3005, and we will issue a “Return Authorization”. A damaged or defective title can only be replaced for the same item.</p>
        
        <p>Should you return to us an item after the 60 day period you will not receive a credit nor will your product be returned to you. </p>

        <h4 class="f18 bold pad12">Questions</h4>
        
        <p>If you have any questions, please contact our Customer Service Department at <a href="mailto:batmanondvd@datapakservices.com" style="color: #000; text-decoration: none;">batmanondvd@datapakservices.com</a>, or call toll free Monday through Friday 8A-8P EST or Saturday 9A-6P EST at 800-839-3005.</p>
        
    </div>


    <!--#include file="bottomcta.html"-->
</div>


<!--#include file="footer.html"-->

    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />
</form>
</body>
</html>
