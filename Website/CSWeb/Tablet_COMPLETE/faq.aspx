<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet_COMPLETE.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8"><meta name="viewport" content="width=device-width, maximum-scale=1.0">
<title>Batman | Classic TV Series Frequently Asked Questions  | As Seen on TV - FAQs</title>
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
<link href="/styles/global_big2complete_tablet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="clearfix">
        <div class="content_hdr">
            <h2>Holy Bat Logic, Batman!</h2>
            <h3 class="f28">Puzzled? Baffled by a riddle? No question is too hard for the <br />
                world's greatest detective!
            </h3>
        </div>
    </div>

    <div class="faq">
        <h4 class="bold f20 pad6">FAQ</h4>
        <ul class="faq_qs">
            <li><a href="#q1">What is contained in the Classic Batman Collection?</a></li>
            <li><a href="#q2">Is Classic Batman Collection available in Blu-Ray™?</a></li>
            <li><a href="#q3">What is contained in the Complete Classic Batman Collection?</a></li>
            <li><a href="#q4">Can I order more than one collection?</a></li>
            <li><a href="#q5">What is installment billing?</a></li>
            <li><a href="#q6">What if I still have questions about the product or my order?</a></li>
            <li><a href="#q7">How long does it take my order to arrive?</a></li>
            <li><a href="#q8">What qualifies as a business day?</a></li>
            <li><a href="#q9">Although I selected 2-day or next day shipping, I didn't receive my package on Saturday or Sunday as I expected. Why?</a></li>
        </ul>

        <dl>
            <dt id="q1">What is contained in the Classic Batman Collection?</dt>
            <dd>12 DVDs containing 64 episodes (all of Season One and Season Two, Part I on nine DVDs), plus a bonus disc featuring over 3 hours of all new bonus features, the Adam West Naked DVD, and the original 1966 Batman Movie on DVD. You will also receive an extensive episode guide, a show script and a personal letter from Adam West. This collection is NOT sold at any stores and is only available at batmanondvd.com.</dd>

            <dt id="q2">Is Classic Batman Collection available in Blu-Ray™?</dt>
            <dd>No, unfortunately it is not but the Complete Classic Batman Collection is available on either DVD or Blu-Ray. This collection includes all 120 episodes of the series.</dd>

            <dt id="q3">What is contained in the Complete Classic Batman Collection?</dt>
            <dd>20 DVDs containing all 120 episodes (all three seasons on 17 DVDs) plus a bonus disc featuring over 3 hours of all new bonus features, the Adam West Naked DVD, and the original 1966 Batman Movie on DVD. You will also receive a detailed episode guide, a show script and a personal letter from Adam West.  All this will be housed and packaged in your very own collectors box! This collection is available on DVD or Blu-Ray (the Blu-Ray will contain a total of 15 DVDs). The Complete Classic Batman Collection is NOT sold at any stores and is only available at batmanondvd.com.</dd>

            <dt id="q4">Can I order more than one collection? </dt>
            <dd>For a limited time only, and as a special holiday offer, we are pleased to offer a 30% discount on each collection you order after the first one. Additional collections must be ordered at the same time as your initial purchase. Maximum units per person is 10.  We can also giftwrap your order for a nominal price.</dd>

            <dt id="q5">What is installment billing?  </dt>
            <dd>For orders that are under $500 we offer an extended payment plan of five equal payments. Any applicable shipping, handling, and 
taxes will be charged with your first installment at the time your order ships. Subsequent installments are billed monthly to the original form of payment. </dd>

            <dt id="q6">What if I still have questions about the product or my order? </dt>
            <dd style="line-height: 21px;">To Reach Customer Service <br />
                Call: 1-800-839-3005 <br />
                Hours: Monday Through Friday 8A-8P EST, Saturday 9A-6P EST<br />
                Email: <a href="mailto:batmanondvd@datapakservices.com" style="color: #000; text-decoration: none;">batmanondvd@datapakservices.com</a>
            </dd>

            <dt id="q7">How long does it take my order to arrive?</dt>
            <dd>Most orders are shipped within two-three business days. Orders placed before November 6, 2014 will be held to arrive on November 11, 2014, the street date for this release.  You will receive an email with a tracking number the morning after your package ships. </dd>

            <dt id="q8">What qualifies as a business day?</dt>
            <dd>Our business days are Monday through Friday, excluding U.S. holidays. Shipments are not delivered on Saturdays or Sundays.</dd>

            <dt id="q9">Although I selected 2-day or next day shipping, I didn't receive my package on Saturday or Sunday as I expected. Why?</dt>
            <dd>Shipments are not delivered on Saturday or Sunday. We do our best to ship all in-stock orders placed by 1PM ET the same day we receive them. However, we ask to allow one full business day for your order to be processed by our warehouse. Also, please note that our carriers deliver on business days, Monday through Friday, not including holidays, Saturday or Sunday. Please take this into consideration when placing your order. </dd>

        </dl>

    </div>


    <!--#include file="bottomcta.html"-->
</div>


<!--#include file="footer.html"-->

    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />
</form>
</body>
</html>
