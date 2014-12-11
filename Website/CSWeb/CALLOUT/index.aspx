<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.CALLOUT.index" EnableSessionState="True" %>

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
<link href="/styles/global_big1callout.css" rel="stylesheet" type="text/css" />
<script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
</head>
<body id="home">
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->
<div class="container_home">
    <div class="home1 ">
        <h2 class="f46 textshadow normal white webfont1" style="line-height: 1.0em;">Relive Your Favorite Moments
            <span class="block f50 bold caps">From The Original Batman!</span>
        </h2>
        <h3 class="f28 textshadow normal yellow webfont1">The original Batman TV series is <span class="bold">finally available on DVD!</span></h3>
        <div class="home1b">
            <ul class="top_promo_list">
                <li>64 Original Broadcast Episodes &ndash;<br /> Fully Remastered</li>
                <li>Never-Released <em>Behind The Scenes</em>
                    Bonus Footage</li>
                <li>Over 30 Hours of Entertainment   <span class="iblock">+ Bonus Items</span></li>
                <li>Includes all your Favorite Villains   and Celebrity Guest Stars!</li>
            </ul>
        </div>
        <a href="#included_complete" class="included_complete home1includedlink"></a>
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/callout/giftbag.png" width="332" height="212" alt="Special Offers For Gift Giving: Exclusive Batman Gift Bag; Special Discount On 2nd Set - Available after checkout!" style="position: absolute; top: 317px; left: 466px;" />
    </div>


    <div class="home_cta1 clearfix">
        <div class="home_cta_left">
            <h3 class="textshadow yellow caps webfont1 f32">Great Scott! Don't Miss Out!</h3>
            <h3 class="textshadow white caps webfont1bold f42">Act Now &amp; <span class="uncaps">Get</span> Free Shipping!</h3>
        </div>
        <div class="home_cta_right">
            <a href="choose.aspx" class="iblock btn_shadow_1">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big1/btn_ordernow.png" alt="Order Now" class="block" /></a>
            <p><a href="#included" class="included white f12 textshadow scored">See What's Included</a></p>
        </div>
    </div>


    <div class="home2">
        <div class="home2_left">
            <h2 class="f46 yellow webfont1bold pad12" style="line-height: 1;"><span class="webfont1">Don't Wait,</span> Get the Collection Now!</h2>
            <p class="f20 white" style="line-height: 28px; width: 290px;">They protected the streets of Gotham and flew into our collective hearts with their hilarious hijinks, over-the-top costumes and simply feel good fun. What are you waiting for? It’s the Bat Signal!</p>
        </div>
        <div class="home2_right">
            <div class="home_tv">
                <div class="hometvpiece_1"></div>
                <div class="hometvpiece_2"></div>
                <div id="homecta"></div>
                <script type='text/javascript'>
                    jwplayer('homecta').setup({
                        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Batman_lp-low2.mp4',
                        autostart: true,
                        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/big1/vidposter_home.jpg',
                        controls: true,
                        width: 396, height: 272,
                        stretching: 'exactfit',
                        skin: '/scripts/jwplayer/bekle.xml'

                    });
                    </script>
                <p class="f15 white pad0 text-center" style="padding-top: 3px;">The Sixties Classic is back and better than ever!</p>
            </div>
        </div>
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/callout/promo_addsets.png" alt="Get Additional Sets at a Reduced Price! Available at checkout." width="183" height="295" style="position: absolute; top: 26px; left: 485px;" />
        
    </div>



    <div class="home3">
        <h2 class="yellow f44 textshadow webfont1bold pad6" style="line-height: 1.05;">The Caped Crusader &amp; the Boy Wonder!</h2>
        <p class="f25 webfont1 white pad12" style="line-height: 1.1;">Your Favorite Dynamic Duo is Back and
            <br />
            Better Than Ever!</p>
        <p class="f20 white pad12" style="line-height: 1.3;">TV's iconic partners, along with a legion of abominable archenemies can now be seen in a whole new POW-erful way. For the first time ever, the 1960’s mega-hit is available on DVD so you can relive all your favorite moments or share with a whole new generation! <a href="choose.aspx" class="yellow unscored" style="margin-left: 6px;"><span class="scored">Act Now</span> ›</a></p>
        <ul class="top_promo_list webfont1bold f18">
            <li>Take A Stroll Down Memory Lane </li>
            <li>See the original Dynamic Duo</li>
            <li>Perfect for the Holiday <br />Gift Giving Season!</li>
        </ul>
    </div>

    <div class="home4">
        <h3 class="f50 webfont1 yellow textshadow" style="padding-bottom: 25px;">Your Favorite Episodes <span class="webfont1bold">Fully Remastered!</span></h3>
        <p class="f18 white textshadow" style="line-height: 1.3; width: 414px;">Now available on DVD, this pop-culture phenom has been remastered so you can relive your favorite episodes or introduce a whole new generation of Bat-Fans. Best of all, Batman and Robin will be appear even more dynamic, vivid and POW-erful than you remember. Same Bat time, Same bat Channel…. <strong>Only more BAT-tastic.</strong></p>
    </div>

    <div class="home_cta1 clearfix">
        <div class="home_cta_left">
            <h3 class="textshadow yellow caps webfont1 f32">See Batman &amp; Robin Like Never Before!</h3>
            <h3 class="textshadow white caps webfont1bold f38" style="letter-spacing: 1px;">Order Today &amp; <span class="uncaps">Get</span> Free Shipping!</h3>
        </div>
        <div class="home_cta_right">
            <a href="choose.aspx" class="iblock btn_shadow_1">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big1/btn_ordernow.png" alt="Order Now" class="block" /></a>
            <p><a href="#included" class="included white f12 textshadow scored">See What's Included</a></p>
        </div>
    </div>

    <div class="home5">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big1/home5.jpg" width="1213" height="172" class="block" />
    </div>

    <div class="home6">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big1/bam_over3hours.png" width="545" height="314" class="block home6_bam" />
        <h2 class="f46 webfont1 yellow textshadow"><span class="webfont1bold">All New</span> Bonus Content!</h2>
        <p class="f18 white lh24" style="position: relative; z-index: 25;">Holy Hallucination! 3 Hours of Bonus Footage? Believe it Bat-Fans. Sneak behind the scenes to witness never before seen footage, hear stories from your favorite villains and guest stars, and get up close and personal with the legend himself-Adam West! Only available in this exclusive collection.</p>
    </div>



    <!--#include file="bottomcta.html"-->
</div><!-- END container -->


<!--#include file="footer.html"-->
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />

</form>
</body>
</html>
