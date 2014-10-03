﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choose.aspx.cs" Inherits="CSWeb.Mobile.choose" EnableSessionState="True" %>

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
<form id="form1" runat="server">
<div class="container">

<!--#include file="popups.html"-->
<!--#include file="header_cart.html"-->

<div class="content">
    
    <h2 class="text-center pad6" style="padding-top: 28px;">Holy Gadget Choices Batman!</h2>
    <h3 class="text-center f35">Which Collection Is For You?</h3>

    <div class="choosebox1 clearfix">
        <div class="fleft" style="width: 40px;">
            <asp:RadioButton ID="rbClassic" runat="server" GroupName="SKU" />
        </div>

        <label class="chooselabel_1" for="rbClassic">
            <span class="block f34 webfont1bold pad12">Classic Batman Collection</span>
            <span class="block f43 webfont1bold blue" style="line-height: 48px;"><span class="black">+ </span>FREE SHIPPING
                <br />
                &amp; HANDLING!
            </span>
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/prod_choose_1.png" width="237" height="174" alt="Classic Batman Collection" style="position: absolute; top: 68px; left: 347px;" />
        </label>
        <div class="clear"></div>
        <p class="view text-center pad6"><a href="javascript:void(0);" class="opendetails gray unscored">view details</a></p>

        <div class="details" style="display: none">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/choose_divider.png" style="position: relative; left: -15px;" />
            <p class="text-center pad20"><a class="gray unscored closedetails" href="javascript:void(0);">view less</a></p>

            <p>Your 12 DVD Classic Collection Features <br />
                        Over 30 HOURS OF ENTERTAINMENT!</p>
                    <p class="pad6">INCLUDES:</p>
                    <ul>
                        <li>64 of your favorite original broadcast episodes fully remastered in HD </li>
                        <li>Over 3 Hours of ALL NEW bonus materials</li>
                        <li>Adam West Naked on DVD: Watch as Adam takes you   behind the scenes of your favorite epidodes!</li>
                        <li>The Original 1966 Batman Movie DVD starring <br />  Adam West and Burt Ward</li>
                        <li>Detailed Episode Guide</li>
                        <li>Your Own Show Script from the Episode  <span style="white-space: nowrap">‘The Joker is Wild’</span></li>
                        <li>Personal Letter from Adam West to you!</li>
                    </ul>
        </div>

    </div>
    


    <div class="choosebox2 clearfix">
        <div class="fleft" style="width: 40px;">
            <asp:RadioButton ID="rbComplete" runat="server" GroupName="SKU" />
        </div>
        <label class="chooselabel_1" for="rbComplete">
            <span class="block f34 webfont1bold pad12" style="line-height: 43px;"><span class="green">Complete </span>Classic
                <br />
                Batman Collection</span>
            <span class="block f43 webfont1bold blue" style="line-height: 48px;"><span class="black">+ </span>FREE SHIPPING
                <br />
                &amp; HANDLING!
            </span>
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/flag_bestvalue.png" width="231" height="122" alt="Best Value" style="position: absolute; top: -43px; left: 362px;" />
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/prod_choose_2.png" width="237" height="145" alt="Complete Classic Batman Collection" style="position: absolute; top: 107px; left: 347px;" />
        </label>
        <div class="clear"></div>
        <p class="view text-center pad6"><a href="javascript:void(0);" class="opendetails gray unscored">view details</a></p>

        <div class="details" style="display: none">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/choose_divider.png" style="position: relative; left: -15px;" />
            <p class="text-center pad20"><a class="gray unscored closedetails" href="javascript:void(0);">view less</a></p>

            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/burst_bluray.png" style="position: absolute; top: 184px; left: 320px;" />
            <p style="padding-right: 50px;">Your Complete Collection Features Over 50 HOURS OF ENTERTAINMENT And comes in a Classic Collector’s Box for displaying your Collection!</p>
            <p class="pad6">INCLUDES:</p>
            <ul>
                <li>120 of your favorite <br />
                    original broadcast episodes <br />
                    fully remastered</li>
                <li>Over 3 Hours of ALL NEW bonus materials</li>
                <li>Adam West Naked on DVD: Watch as Adam takes you   behind the scenes of your favorite epidodes!</li>
                <li>The Original 1966 Batman Movie DVD starring
                    <br />
                    Adam West and Burt Ward</li>
                <li>Detailed Episode Guide</li>
                <li>Your Own Show Script from the Episode  <span style="white-space: nowrap">‘The Joker is Wild’</span></li>
                <li>Personal Letter from Adam West to you!</li>
            </ul>
        </div>
    </div>
    <br />
    <asp:LinkButton ID="imgContinue" runat="server" OnClick="imgContinue_Click"><img class="prod_continue" src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_continue.png" /></asp:LinkButton>
    <p class="f24" style="line-height: 32px; padding-bottom: 90px;"><strong>Offer Details: </strong> You will be charged today for your first of 5 monthly payments of $19.99. Applicable taxes will be included in your first payment (this should not display if the consumer selects a 1-pay option). Shipping is FREE. If you are not satisfied with your purchase for any reason, simply return it within 60 days to receive a full refund. Restrictions may apply. Please refer to our Return Policy (link to Return Policy page)</p>
    <asp:Label ID="lblPrompt" runat="server"></asp:Label>



</div>

<!--#include file="footer.html"-->
</div>
</form>
</body>
</html>
