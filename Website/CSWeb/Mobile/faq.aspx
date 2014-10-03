﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=640px, initial-scale=.5, maximum-scale=1" />
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
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/logo_batman2.png" width="244" height="196" alt="Batman" style="position: absolute; top: 63px; left: 392px;" />
        <h2 style="padding-top: 22px; padding-left: 14px;">Holy Bat Logic, Batman!
        </h2>
        <h3 class="f35" style="line-height: 1.2em; padding-left: 14px;">Puzzled? Baffled by a riddle? <br />


        <div class="faq">
        <h4>FAQ</h4>
        <ul class="faq_qs">
            <li><a href="#q1">What is contained in the Classic <br />Batman Collection?</a></li>
            <li><a href="#q2">Is Classic Batman Collection available in <br />Blu-Ray™?</a></li>
            <li><a href="#q3">What is contained in the Complete Classic <br />Batman Collection?</a></li>
            <li><a href="#q4">Can I order more than one collection?</a></li>
            <li><a href="#q5">What is installment billing?</a></li>
            <li><a href="#q6">What if I still have questions about the product <br />or my order?</a></li>
            <li><a href="#q7">How long does it take my order to arrive?</a></li>
            <li><a href="#q8">What qualifies as a business day?</a></li>
            <li><a href="#q9">Although I selected 2-day or next day shipping, <br />I didn't receive my package on Saturday or <br />Sunday as I expected. Why?</a></li>
        </ul>

        <dl>
            <dt id="q1">What is contained in the Classic <br />Batman Collection?</dt>
            <dd>12 dvds containing 64 episodes (all of Season One and Season Two, Part I on nine DVDs), plus a bonus disc featuring over 3 hours of all new bonus features, the Adam West Naked DVD, and the original 1966 Batman Movie on DVD. You will also receive an extensive episode guide, a show script and a personalized letter from Adam West. This collection is NOT sold at any stores and is only available at batmanondvd.com.</dd>

            <dt id="q2">Is Classic Batman Collection available <br />in Blu-Ray™?</dt>
            <dd>No, unfortunately it is not but the Complete Classic Batman Collection is available on either DVD or Blu-Ray. This collection includes all 120 episodes of the series.</dd>

            <dt id="q3">What is contained in the Complete <br />Classic Batman Collection?</dt>
            <dd>20 DVDs containing all 120 episodes (all three seasons on 17 DVDs) plus a bonus disc featuring over 3 hours of all new bonus features, the Adam West Naked DVD, and the original 1966 Batman Movie on DVD. You will also receive a detailed episode guide, a show script and a personalized letter from Adam West.  All this will be housed and packaged in your very own collectors box! This collection is available on DVD or Blu-Ray (the Blu-Ray will contain a total of 15 dvds). The Complete Classic Batman Collection is NOT sold at any stores and is only available at batmanondvd.com.</dd>

            <dt id="q4">Can I order more than one collection? </dt>
            <dd>For a limited time only, and as a special holiday offer, we are pleased to offer a 30% discount on each collection you order after the first one. Additional collections must be ordered at the same time as your initial purchase. Maximum units per person is 10.  We can also giftwrap your order for a nominal price.</dd>

            <dt id="q5">What is installment billing?  </dt>
            <dd>For orders that are under $500 we offer an extended payment plan of five equal payments. Any applicable shipping, handling, and 

            <dt id="q6">What if I still have questions about the product or my order? </dt>
            <dd>Please call our customer service department: <br />

            <dt id="q7">How long does it take my order to arrive?</dt>
            <dd>Most orders are shipped within two business days. You will receive an email with a tracking number the morning after your package ships.  </dd>

            <dt id="q8">What qualifies as a business day?</dt>
            <dd>Our business days are Monday through Friday, excluding U.S. holidays. Shipments are not delivered on Saturdays or Sundays.</dd>

            <dt id="q9">Although I selected 2-day or next day shipping, I didn't receive my package on Saturday or Sunday as I expected. Why?</dt>
            <dd>Shipments are not delivered on Saturday or Sunday. We do our best to ship all in-stock orders placed by 1PM ET the same day we receive them. However, we ask to allow one full business day for your order to be processed by our warehouse. Also, please note that our carriers deliver on business days, Monday through Friday, not including holidays, Saturday or Sunday. Please take this into consideration when placing your order. </dd>

        </dl>

    </div>

    </div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>