<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.BIG3.index" EnableSessionState="True" %>

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
<link href="/styles/global_big3.css" rel="stylesheet" type="text/css" />
<script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
</head>
<body id="home">
<form id="form1" runat="server">
<div class="container_wrap">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->
<div class="container">
    <div class="clearfix" style="height: 136px; overflow: visible;">
        <div class="content_logo"><a href="index.aspx" style="position: absolute;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo.png" width="194" height="156" alt="Batman Classic TV Series" class="block" /></a></div>
        <div class="content_hdr" style="padding: 24px 0 0 22px; width: 100%;">
            <h2 class="f37">Relive Your Favorite Moments From the ORIGINAL Batman!</h2>
            <h3 class="f26">The original Batman television series is finally available on DVD!</span></h3>
        </div>
    </div>
    <div class="home1 ">
        <div class="home1a">
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
            <a href="#included_complete" class="included_complete" style="position: absolute; top: -14px; left: 130px; z-index: 30;">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/splash_completeseries.png" width="205" height="112" alt="Complete Series Available - Learn More" /></a>
        </div>
        <div class="home1b">
            <ul class="top_promo_list">
                <li>64 Original Broadcast Episodes   <span class="iblock">– Fully Remastered</span></li>
                <li>Never-Released Behind The Scenes  Bonus Footage </li>
                <li>Over 30 Hours of Entertainment  <span class="iblock">+ Bonus Items</span></li>
                <li>Take a Trip Down Memory Lane!</li>
            </ul>
            <a href="#included" class="included white f11 scored" style="position: absolute; top: 303px; left: 275px; z-index: 30;">See What's Included</a>
        </div>
        <div class="clear"></div>
        
    </div>
    
    <div class="home_cta clearfix">
            <div class="home_cta_left">
                <h3>Great Scott! Don't Miss Out!</h3>
                <h4>Act Now &amp; Get <span class="purple">Free Shipping!</span></h4>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/batman_onphone.png" width="139" height="101" alt="Batman" class="block" style="position: absolute; top: -30px; left: 489px;" />
            </div>
            <div class="home_cta_right">
                <a href="choose.aspx" class="iblock btn_shadow_1">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big3/btn_ordernow.png" width="249" height="47" alt="Order Now" class="block" /></a>
                <p><a href="#included" class="included">See What's Included</a></p>
            </div>
    </div>


    <div class="home3 clearfix">
        <div class="home3a">
            <h2 class="webfont1 f38 pad12" style="line-height: 1;">Your Favorite Episodes<br />                <span class="webfont1bold f48">Fully Remastered!</span></h2>
            <p class="f15 lh22">Same Bat time, Same Bat channel, more Bat-tastic. Imagine your favorite duo only more <strong>vivid, vibrant, and POW-erful</strong> than you remember! Relive your favorite episodes and share with a whole new generation withthis exclusive DVD set.</p>
            <p class="f23 webfont1bold pad0">See Batman & Robin like never before!</p>
            <p class="f15 lh22">Order today and get <strong>FREE SHIPPING!</strong> <a href="choose.aspx" class="purple unscored bold" style="margin-left: 6px;"><span class="scored">Order Now</span> ›</a></p>
        </div>
        <div class="fleft">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/orig_remaster.jpg" alt="" />
        </div>
        
    </div>
    
    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big3/home3reel.jpg" width="960" height="166" alt="" class="block" />

    <div class="home2">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big3/burst_pow2.png" alt="POW! 64 Episodes, 3 Bonus Hours + More!" style="position: absolute; top: 30px; left: 544px; z-index: 10;" />
        <h2 class="f36 webfont1bold white pad0">Join the Dynamic Duo on Action-Packed Adventures!</h2>
        <h3 class="f22 white">Watch Batman and Robin foil Gotham’s villains in digital clarity!</h3>
        <p class="f16 lh20 white" style="width: 565px;">TV's iconic partners, along with a legion of abominable archenemies can now be seen in a whole new POW-erful way. For the first time ever, the 1960’s mega-hit is available on DVD so you can relive all your favorite moments or share with a whole new generation!</p>
        <ul class="top_promo_list2 webfont1bold f20">
            <li>Includes all your Favorite Villains and Celebrity Guest Stars</li>
            <li>Detailed Episode Guide Gives Noteworthy Insights</li>
            <li>Perfect for the Holiday Gift Giving Season!</li>
        </ul>
        <div class="home2_yellowbox">
            <h2 class="f30 pad6">Don’t Wait, Get the Collection Now!</h2>
            <p class="f16 lh20" style="width: 530px;">They protected the streets of Gotham and flew into our collective hearts with their hilarious hijinks, over-the-top costumes and simply feel good fun. What are you waiting for? It’s the Bat Signal! <a href="choose.aspx" class="red unscored bold" style="margin-left: 6px;"><span class="scored">Act Now</span> ›</a></p>
        </div>
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big3/prod_classic_collection.png" alt="The Classic Batman Collection!" style="position: absolute; top: 316px; left: 69px; z-index: 10;" />
        <a href="#included" class="included white f10 webfont2bold scored" style="position: absolute; top: 641px; left: 242px; z-index: 30;">See What's Included</a>
    </div>


    <div class="home2bottom">
        <div style="position: absolute; top: 274px; left: 68px; width: 334px;">
            <h2 class="f36 white pad0">Holy Special Delivery!</h2>
            <h3 class="white">Don't Wait, Get the Collection Now!</h3>
        </div>
        
        <div style="position: absolute; top: 276px; left: 404px; width: 505px;">
            <p class="f16 lh20 white">They protected the streets of Gotham and flew into our collective hearts with their hilarious hijinks, over –the-top costumes and simply feel good fun. What are you waiting for? It’s the Bat Signal! <a href="choose.aspx" class="bold">Act Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
        </div>
    </div>
    <div class="home5">
        <h2 class="f48 text-center pad20">Go Behind the Scenes of Your Favorite Show!</h2>
        <div class="home5b">
            <p class="f16 lh22">3 Hours of Bonus Footage? It’s not a joke Bat-Fans! Now’s your chance to sneak behind the scenes and witness never before seen footage, hear secrets from your favorite guest stars and villains. Get a peak at the making of the episodes, learn about the outrageous costumes and delve into Batman and Robin Memorabilia.  </p>
            <ul>
                <li>Get up close and personal with Adam West</li>
                <li>Enjoy a roundtable chat with the famous stars</li>
                <li>Learn how they brought fiction to life with art & design</li>
                <li>Exclusive looks at rare Batman Collectibles</li>
            </ul>
        </div>
        
    </div>

    <div class="home_cta clearfix">
            <div class="home_cta_left">
                <h3>Great Scott! Don't Miss Out!</h3>
                <h4>Act Now &amp; Get <span class="purple">Free Shipping!</span></h4>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big2/logo_batman_text.png" width="144" height="83" alt="Batman" class="block" style="position: absolute; top: -3px; left: 498px; z-index: 50;" />
            </div>
            <div class="home_cta_right">
                <a href="choose.aspx" class="iblock btn_shadow_1">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/big3/btn_ordernow.png" width="249" height="47" alt="Order Now" class="block" /></a>
                <p><a href="#included" class="included">What's Included</a></p>
            </div>
    </div>



    <!--#include file="bottomcta.html"-->
</div><!-- END container -->


<!--#include file="footer.html"-->
</div>
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />

</form>
</body>
</html>
