<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDecline.aspx.cs" Inherits="CSWeb.PS_A1.Store.CardDecline" EnableSessionState="True" %>
<%@ Register Src="UserControls/CardDecline.ascx" TagName="Form" TagPrefix="uc1" %>
 
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>
<!doctype html>
<html>
<head id="Head1" runat="server">
<meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV </title>
<meta name="description" content=""/>
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
    <form id="form1" runat="server">
<!--#include file="header_cart.html"-->
<div class="container_cart">
        <uc1:Form id="ucCardDecline" runat="server" />
</div>

<!--#include file="footer.html"-->
 <uc:TrackingPixels ID="TrackingPixels" runat="server" />
    </form>
</body>
</html>
