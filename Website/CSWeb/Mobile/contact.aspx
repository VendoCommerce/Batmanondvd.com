﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=640, initial-scale=.5, maximum-scale=1" />
<title>Batman Classics on DVD</title>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script type="text/javascript" src="/Scripts/global_mobile.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />

</head>
 
<body>
 <form runat="server" id="fm1">
<div class="container">

<!--#include file="popups.html"-->
<!--#include file="header.html"-->

    <div class="content">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/logo_batman2.png" width="178" height="143" alt="Batman" style="position: absolute; top: 10px; left: 458px;" />
        <h2 class="f50 pad0" style="padding-top: 20px;">To the Bat-Phone!</h2>
            <h3 class="f30">Or send an email from your Bat-Computer.</h3>

        <p class="f40" style="line-height: 49px;"><strong>To order, call:</strong> <br />
            1 (800) XXX-XXXX</p>

        <p class="f30 pad20" style="line-height: 42px;"><strong style="text-decoration: underline;">Customer Service</strong></p>


    </div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>