<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet_BIG1.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1.0">
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
<link href="../styles/global_tablet.css" rel="stylesheet" type="text/css" />
<script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
</head>
<body>
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->
<div class="container">
    <div class="clearfix" style="height: 136px; overflow: visible;">
        <div class="content_logo"><a href="index.aspx" style="position: absolute;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo.png" width="194" height="156" alt="Batman Classic TV Series" class="block" /></a></div>
        <div class="content_hdr" style="padding-top: 28px;">
            <h2 class="f50">Holy Classic Collection Batman!</h2>
            <h3 class="f26">For the First Time Ever &mdash; Digitally Remastered <span class="webfont1bold">Plus 3 FREE Bonus DVDs!</span></h3>
        </div>
    </div>
    <div class="home1 ">
        <div class="home1a">
            <div class="homevideowrap">
                <div class="tvpiece_1"></div>
                <div class="tvpiece_2"></div>
                <div class="tvpiece_3"></div>
                <div class="tvpiece_4"></div>
                <div class="tvpiece_center"></div>

                <div id="test1">
                    <div id="ctavideo"></div>
                    <script type='text/javascript'>
                        jwplayer('ctavideo').setup({
                            file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Batman_lp-low2.mp4',
                            autostart: false,
                            image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/vidposter_home.jpg',
                            controls: true,
                            width: 332, height: 254,
                            stretching: 'exactfit',
                            skin: '/scripts/jwplayer/bekle.xml'

                        });
                    </script>
                </div>
            </div>
            <a href="#included_complete" class="included_complete" style="position: absolute; top: -16px; left: 402px; z-index: 30;">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/callout_completeseries.png" width="153" height="152" alt="Complete Series Available - Learn More" /></a>
        </div>
        <div class="home1b">
            <h3 class="webfont1bold f24" style="line-height: 1.1em; padding-bottom: 9px;">Your 12 DVD Classic Collection Features Over 30 HOURS OF ENTERTAINMENT!</h3>
            <ul class="top_promo_list f20" style="padding-right: 30px;">
                <li>64 of your Favorite Original Broadcast <br />Episodes Fully Remastered</li>
                <li>Over 3 Hours of ALL NEW bonus content</li>
                <li>Adam West Naked DVD: Watch as Adam Takes You Behind the Scenes of Your Favorite Episodes!</li>
                <li>The Original 1966 Batman Movie on DVD <br />Starring Adam West & Burt Ward</li>
                <li>Detailed Episode Guide</li>
                <li>Your Own Show Script from the Episode <span style="white-space: nowrap;">'The Joker is Wild'</span></li>
                <li>Personal Letter from Adam West to You!</li>
            </ul>
        </div>
        <div class="clear"></div>
        <div class="home1c clearfix">
            <div class="fleft" style="width: 608px;">
                <h3><img src="//d1kg9stb0ddjcv.cloudfront.net/images/hdr_greatscott.png" alt="'Great Scott!' Don't Wait, Act Now! Only 5 Easy Payments of $19.99 + FREE S&H!" /></h3>
            </div>
            <div class="fleft" style="width: 320px; padding-top: 3px;">
                <a href="choose.aspx">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_ordernow.png" alt="Order Now" /></a>
            </div>
        </div>
    </div>
    <div class="home2top">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/burst_pow2.png" alt="POW! 64 Episodes, 3 Bonus Hours + More!" style="position: absolute; top: 9px; left: 542px; z-index: 10;" />
        <h2 class="f36 webfont1bold green pad0">The Caped Crusader and the Boy Wonder!</h2>
        <h3>Your Favorite Dynamic Duo is Back and Better Than Ever!</h3>
        <p class="f16 lh20" style="width: 565px;">TV's iconic partners, along with a legion of abominable archenemies can now be seen in a whole new POW-erful way. For the first time ever, the 1960’s mega-hit is available on DVD so you can relive all your favorite moments or share with a whole new generation!</p>
        <ul class="top_promo_list webfont1bold f20">
                <li>Take Yourself Back to Simpler Times </li>
                <li>Includes all your Favorite Villains and Celebrity Guest Stars</li>
                <li>Detailed Episode Guide Gives Noteworthy Insights</li>
                <li>Perfect for the Holiday Gift Giving Season!</li>
            </ul>
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
    <div class="home3">
        <div class="home3a">
            <h2 class="green f36 pad0">Don’t Pussy-Foot Around!</h2>
            <h3 class="pad20">Get the Entire First Season - Complete with Cliffhangers, Bat-Gadgets and Alter Egos!</h3>
            <p class="f16 lh20" style="padding-right: 35px;">With perfectly pitched scripts and the iconic <br />
                high-camp cleverness of "Pow!," "Thwack!" and "Zap!" graphics, Batman leapt off the pages of the comics and into our living rooms to become one of the biggest hits in television history!</p>
        </div>
        <div class="home3b">
            <div class="slideshow"
                data-cycle-timeout="5000"
                data-cycle-swipe="true"
                data-cycle-slides="> div"
                data-cycle-next="#clipnext"
                data-cycle-prev="#clipprev">

                <div><img src="//d1kg9stb0ddjcv.cloudfront.net/images/homeslide_1.jpg" class="block" />
                    <div style="width: 387px; padding-top: 18px;">
                <h2 class="text-center caps green f28 pad6">Your Favorite Diabolical Foes</h2>
                <p class="f16 lh20 text-center" style="padding: 0 20px;">From the raucous Riddler to the calculating Catwoman, watch as they attempt to confound
our Caped Crusaders. <a href="cast.aspx" class="bold">See It Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
            </div>
                </div>
                <div><img src="//d1kg9stb0ddjcv.cloudfront.net/images/homeslide_2.jpg" class="block" />
                    <div style="width: 387px; padding-top: 18px;">
                <h2 class="text-center caps green f28 pad6">Your Favorite Diabolical Foes</h2>
                <p class="f16 lh20 text-center" style="padding: 0 20px;">From the raucous Riddler to the calculating Catwoman, watch as they attempt to confound
our Caped Crusaders. <a href="cast.aspx" class="bold">See It Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
            </div>
                </div>
                
                <div><img src="//d1kg9stb0ddjcv.cloudfront.net/images/homeslide_3.jpg" class="block" />
                    <div style="width: 387px; padding-top: 18px;">
                <h2 class="text-center caps green f28 pad6">Your Favorite Diabolical Foes</h2>
                <p class="f16 lh20 text-center" style="padding: 0 20px;">From the raucous Riddler to the calculating Catwoman, watch as they attempt to confound
our Caped Crusaders. <a href="cast.aspx" class="bold">See It Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
            </div>
                </div>
                
                <div><img src="//d1kg9stb0ddjcv.cloudfront.net/images/homeslide_4.jpg" class="block" />
                    <div style="width: 387px; padding-top: 18px;">
                <h2 class="text-center caps green f28 pad6">Your Favorite Diabolical Foes</h2>
                <p class="f16 lh20 text-center" style="padding: 0 20px;">From the raucous Riddler to the calculating Catwoman, watch as they attempt to confound
our Caped Crusaders. <a href="cast.aspx" class="bold">See It Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
            </div>
                </div>
                
                <div><img src="//d1kg9stb0ddjcv.cloudfront.net/images/homeslide_5.jpg" class="block" />
                    <div style="width: 387px; padding-top: 18px;">
                <h2 class="text-center caps green f28 pad6">Your Favorite Diabolical Foes</h2>
                <p class="f16 lh20 text-center" style="padding: 0 20px;">From the raucous Riddler to the calculating Catwoman, watch as they attempt to confound
our Caped Crusaders. <a href="cast.aspx" class="bold">See It Now<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></p>
            </div>
                </div>

            </div>
            <a href="#" id="clipprev">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_slider_prev.png" alt="Previous" class="block" /></a>
            <a href="#" id="clipnext">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_slider_next.png" alt="Next" class="block" /></a>

            
            
        </div>
    </div>



    <!--#include file="bottomcta.html"-->
</div><!-- END container -->


<!--#include file="footer.html"-->
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />

</form>
</body>
</html>
