<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1" />
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV</title>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script type="text/javascript" src="/Scripts/global_mobile.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />

</head>
 
<body id="page500">
 <form runat="server" id="fm1">
<div class="container">

<!--#include file="popups.html"-->
<!--#include file="header.html"-->

    <div class="content">
        <div style="padding-top: 40px;">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo_big.png" width="383" height="217" alt="Batman Classic TV Series" class="block" style="margin: 0 auto;" />

        <div class="clearfix">
            
            <div>
                <h2 class="f70">Temporarily down...<br />
                    But we'll be back!
                </h2>
                <h3 class="f35" style="padding-bottom: 25px;">An ace crimefighter needs his equipment to be in top working condition, which means occassional maintenance from time to time.</h3>
                <h3 class="f35" style="padding-bottom: 25px;">Be sure to check back soon!</h3>
                <p class="f49 bold" style="line-height: 62px;">SAME BAT-TIME!<br />                    SAME BAT-SITE!</p>
            </div>
            <div>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/jokersdown.jpg" width="609" height="397" alt="Sad Joker" class="block" style="margin: 0 auto;" />
            </div>
        </div>

        <div style="height: 335px;"></div>


    </div>

    </div>

<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>
