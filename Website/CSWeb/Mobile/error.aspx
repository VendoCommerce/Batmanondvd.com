<%@Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=640, initial-scale=.5, maximum-scale=.5" />
<title></title>

<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript" src="/Scripts/global.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form runat="server" id="fm1">

<div class="container">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="content">
<h2>Oops!</h2>
<p>We're sorry! We've encountered an error on our site and the Webmaster has been notified. Please go to <a href="http://www.wisetvoffer.com">www.wisetvoffer.com</a> or contact us at (800) 216-4467 to finalize your order! </p>

</div>
<!--#include file="footer.html"-->
</div>

<uc:TrackingPixels ID="TrackingPixels" runat="server" />
</form>
</body>
</html>
