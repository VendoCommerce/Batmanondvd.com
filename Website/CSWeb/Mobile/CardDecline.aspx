<%@Page Language="C#" AutoEventWireup="true" CodeBehind="CardDecline.aspx.cs" Inherits=" CSWeb.Mobile.Store.CardDecline" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>
<%@ Register Src="UserControls/CardDecline.ascx" TagName="Form" TagPrefix="uc1" %>
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="apple-mobile-web-app-capable" content="yes">

<meta name="viewport" content="width=640px, initial-scale=.5, maximum-scale=.5" />
<title>Kyrobak - Order Confirmation</title>
<meta name="description" content="Kyrobak - Guaranteed to Relieve Back Pain!  Doctor Recommended - As Seen on TV!"/>
<meta name="keywords" content="Kyrobak, Kyro, Back Pain Relief, 100% Risk Free, Continuous Passive Motion, CPM, Oscillation Therapy, Doctor Recommended, 60 Day Trial"/>
<script type="text/javascript" src="/scripts/NoBack.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript" src="/Scripts/global_mobile.js"></script>
<link href="../styles/global_mobile.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
<!--#include file="popups.html"-->
<!--#include file="header_upsell.html"-->

<div class="content">

        <uc1:Form id="ucCardDecline" runat="server" />

</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />
<!--#include file="footer.html"-->
</div>


    </form>
</body>
</html>
