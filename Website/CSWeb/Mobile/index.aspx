<%@Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=640px, initial-scale=.5, maximum-scale=.5" />
<title></title>


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
<!--#include file="header_home.html"-->

<div class="content">

<div class="hometop"><img src="<%= GetDynamicVersionData("homepageimage")%>" width="640" height="1421" usemap="#Maphome" class="block" />
  <map name="Maphome">
    <area shape="rect" coords="337,133,625,313" href="/content/video/ctavideo.mp4">
    <area shape="rect" coords="0,0,1000,1000" href="choose.aspx">
    <area shape="rect" coords="5,795,635,883" href="tel:1<%= GetCleanPhoneNumber(GetDynamicVersionData("phone"))%>">
    <area shape="rect" coords="3,887,628,971" href="howitworks.aspx">
    <area shape="rect" coords="5,979,634,1059" href="testimonials.aspx">
    <area shape="rect" coords="4,1066,634,1151" href="faq.aspx">
    <area shape="rect" coords="2,1157,629,1240" href="privacy.aspx">
    <area shape="rect" coords="2,1246,629,1330" href="terms.aspx">
    <area shape="rect" coords="2,1336,632,1419" href="mailto:[enter your email address]?subject=Email Reminder from Wise Food Supply&body=Just sending a reminder to order from Wise! Simply visit http://www.wisetvoffer.com to get comfort foods when you need them the most.">
  </map>
  
  
   <img src="<%= GetDynamicVersionData("ctaimage")%>" alt="imageName" class="block" usemap="#Mapcta" />
  <map name="Mapcta">
    <area shape="rect" coords="12,671,627,819" href="cart.aspx">
  </map>
    
</div>
</div>
<!--#include file="footer.html"-->
</div>

<uc:TrackingPixels ID="TrackingPixels" runat="server" />
</form>
</body>
</html>
