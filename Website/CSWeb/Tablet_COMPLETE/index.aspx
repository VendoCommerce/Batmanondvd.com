<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet_COMPLETE.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Home</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/jquery.cycle2.min.js"></script>
<script src="/Scripts/jquery.cycle2.swipe.min.js"></script>
<script src="/Scripts/global.js"></script>
<link href="/styles/global_big2complete.css" rel="stylesheet" type="text/css" />
<script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
</head>
<body id="home">
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->
<div class="container">
    <div class="home1">
        <div class="home1a">
            <h2 class="f44 pad6">The Original Batman is Finally Back!</h2>
            <h3 class="f27">The original television series is finally available on DVD!</span></h3>
        </div>

        <div class="home1_img"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/batman_robin.png" width="453" height="434" alt="" /></div>
        
        <div class="home1b">
            <ul class="top_promo_list">
                <li>All the Original Broadcast Episodes
   <span class="iblock">– Fully Remastered</span></li>
                <li>Never-Released Behind The Scenes
  Bonus Footage </li>
                <li>Over 50 Hours of Entertainment
  <span class="iblock">+ Bonus Items</span></li>
                <li>Includes all your Favorite Villains
  and Celebrity Guest Stars!</li>
            </ul>
        </div>

        <div class="clear"></div>
        
    </div>

    <div class="home_cta clearfix">
            <div class="home_cta_left">
                <h3>Great Scott! Don't Miss Out!</h3>
                <h4>Act Now &amp; Get <span class="blue2">Free Shipping!</span></h4>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/batman_onphone.png" width="139" height="101" alt="Batman" class="block" style="position: absolute; top: -30px; left: 489px;" />
            </div>
            <div class="home_cta_right">
                <a href="choose.aspx" class="iblock btn_shadow_1">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/btn_ordernow.png" width="249" height="47" alt="Order Now" class="block" /></a>
                <p><a href="#included_complete" class="included_complete">See What's Included</a></p>
            </div>
    </div>

    <div class="home2">
        <div class="homevideowrap">
            <div class="home_tvpiece_1"></div>
            <div class="home_tvpiece_2"></div>
            <div class="home_tvpiece_3"></div>
            <div class="home_tvpiece_4"></div>
            <div class="home_tvpiece_center"></div>

            <div id="homevid">
                <div id="ctavideo"></div>
                <script type='text/javascript'>
                    jwplayer('ctavideo').setup({
                        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Batman_lp-low2.mp4',
                        autostart: true,
                        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/vidposter_home.jpg',
                        controls: true,
                        width: 256, height: 189,
                        stretching: 'exactfit',
                        skin: '/scripts/jwplayer/bekle.xml'

                    });
                </script>
            </div>
        </div>
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/Tablet_COMPLETE/pow_120episodes.png" alt="POW! 120 Episodes, 3 Free Bonus Discs - 50+ Hours of Entertainment!" style="position: absolute; top: 362px; left: 535px; z-index: 10;" />
        <h2 class="f36 webfont1bold white pad0">The Caped Crusader and the Boy Wonder!</h2>
        <h3 class="white">Your Favorite Dynamic Duo is Back and Better Than Ever!</h3>
        <p class="f16 lh20 white pad12" style="width: 534px;">TV's iconic partners, along with a legion of abominable archenemies can now be seen in a whole new POW-erful way. For the first time ever, the 1960’s mega-hit is available on DVD so you can relive all your favorite moments or share with a whole new generation!</p>
        <ul class="top_promo_list2">
            <li>Take A Stroll Down Memory Lane </li>
            <li>See the original Dynamic Duo</li>
            <li>Perfect for the Holiday Gift Giving Season!</li>
        </ul>
        <a href="#included_complete" class="included_complete white webfont2bold f10" style="position: absolute; top: 466px; left: 323px;">See What's Included</a>
        <div class="home2_yellowbox">
            <h2 class="f30 pad6">Don’t Wait, Get the Collection Now!</h2>
            <p class="f16 lh20" style="width: 510px;">They protected the streets of Gotham and flew into our collective hearts with their hilarious hijinks, over-the-top costumes and simply feel good fun. What are you waiting for? It’s the Bat Signal! <a href="choose.aspx" class="red unscored bold" style="margin-left: 6px;"><span class="scored">Act Now</span> ›</a></p>
        </div>
    </div>


    <div class="home3 clearfix">
        <div class="home3a">
            <h2 class="webfont1 f38 pad12" style="line-height: 1;">Your Favorite Episodes<br />
                <span class="webfont1bold f48">Fully Remastered!</span></h2>
            <p class="f15 lh22">Same Bat time, Same Bat channel, more Bat-tastic. Imagine your favorite duo only more vivid, vibrant, and POW-erful than you remember! Relive your favorite episodes and share with a whole new generation with
this exclusive DVD set.</p>
            <p class="f23 webfont1bold pad0">See Batman & Robin like never before!</p>
            <p class="f15 lh22">Order today and get <strong>FREE SHIPPING!</strong> <a href="choose.aspx" class="red unscored bold" style="margin-left: 6px;"><span class="scored">Order Now</span> ›</a></p>
        </div>
        <div class="fleft">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/orig_remaster.jpg" alt="" />
        </div>
        
    </div>

    <div class="home4">
        <h2 class="f48">All New Bonus Content</h2>
        <p class="f15 lh20 white" style="width: 365px; margin-top: 123px;">Holy Hallucination! 3 Hours of Bonus Footage? Believe it Bat-Fans! Sneak behind the scenes, witness never before seen footage, hear secrets from your favorite guest stars, and get up close and personal with the legend himself-Adam West! Only available with this special offer.</p>
    </div>

    <div class="home_cta clearfix">
            <div class="home_cta_left">
                <h3>Great Scott! Don't Miss Out!</h3>
                <h4>Act Now &amp; Get <span class="blue2">Free Shipping!</span></h4>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/logo_batman_text.png" width="144" height="83" alt="Batman" class="block" style="position: absolute; top: -3px; left: 498px; z-index: 50;" />
            </div>
            <div class="home_cta_right">
                <a href="choose.aspx" class="iblock btn_shadow_1">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/btn_ordernow.png" width="249" height="47" alt="Order Now" class="block" /></a>
                <p><a href="#included_complete" class="included_complete">What's Included</a></p>
            </div>
    </div>



    <!--#include file="bottomcta.html"-->
</div><!-- END container -->


<!--#include file="footer.html"-->
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />

</form>
</body>
</html>
