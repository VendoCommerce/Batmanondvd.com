<%@Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1" />
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
    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/logo_batman2.png" width="244" height="196" alt="Batman" style="position: absolute; top: 26px; left: 404px;" />
    <h2 class="f44 pad20" style="padding-top: 30px;">Holy Classic <br />
        Collection Batman!
    </h2>
    <h3 style="line-height: 1.2em;">For the First Time Ever – Digitally <br />
        Remastered <strong>Plus 3 FREE Bonus DVDs!</strong></h3>

    <div>
    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/home1_new.jpg" width="640" height="517" alt="Batman" class="block" usemap="#Map_home1" />
    <map name="Map_home1" id="Map_home1">
        <area shape="circle" coords="128,86,86" href="#included_complete" class="included_complete" alt="Learn More" />
        <area shape="rect" coords="268,318,460,356" href="#included" alt="See What's Included" class="included" />
    </map>

        <div class="homevideowrap">
                <div class="home_tvpiece_1"></div>
                <div class="home_tvpiece_2"></div>
                <div class="home_tvpiece_3"></div>
                <div class="home_tvpiece_4"></div>
                <div class="home_tvpiece_center"></div>

                <div id="homevid1">
                    <div id="ctavideo"></div>
                    <script type='text/javascript'>
                        jwplayer('ctavideo').setup({
                            file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Batman_lp-low.mp4',
                            autostart: false,
                            image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/vidposter_home.jpg',
                            controls: true,
                            width: 264, height: 199,
                            stretching: 'exactfit',
                            skin: '/scripts/jwplayer/bekle.xml'

                        });
                    </script>
                </div>
            </div>
    </div>
    <div class="fleft" style="width: 330px; padding-left: 13px;">
        <a href="tel:18006732909"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_clicktocall.png" width="296" height="81" alt="Click to Call" class="block" /></a>
    </div>
    
    <div class="fleft" style="width: 296px;">
        <a href="choose.aspx"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/btn_ordernow_home.png" width="296" height="81" alt="Order Now" class="block" /></a>
    </div>
    <div class="clear"></div>
    <h2 class="text-center f38 pad6" style="padding: 35px 0 0 0;">Your 12 DVD Classic Collection Features<br />
        Over 30 HOURS OF ENTERTAINMENT!
    </h2>
    <div class="batman_home">
        <div class="purplebox">
            <ul>
                    <li> 64 of your Favorite Original Broadcast Episodes  Fully Remastered </li>
                    <li>Over 3 Hours of ALL NEW bonus content</li>
                    <li>
                        Adam West Naked on DVD: Watch as Adam Takes You 
                        Behind the Scenes of Your Favorite Episodes!
                    </li>
                    <li>The Original 1966 Batman Movie Starring Adam West & Burt Ward</li>
                    <li>Detailed Episode Guide</li>
                    <li>Your Own Show Script from the Episode <span style="white-space: nowrap;">‘The Joker is Wild’</span></li>
                    <li>Personal Letter from Adam West to You!</li>
                </ul>
        </div>
    </div>

    <div class="home_capedcrusader">
        <h2 class="f34 pad6 green">The Caped Crusader and the Boy Wonder!</h2>
        <h3 class="f31 pad20">Your Favorite Dynamic Duo is Back and Better Than Ever!</h3>
        <p class="f25" style="padding-right: 50px; line-height: 32px;">TV's iconic partners, along with a legion of abominable archenemies can now be seen in a whole new POW-erful way. For the first time ever, the 1960’s mega-hit is available on DVD so you can relive all your favorite moments or share with a whole new generation!</p>
    </div>
    <div class="home_skyline">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/burst_pow.png" width="382" height="346" alt="POW! 64 Episodes, 3 FREE Bonus Discs - 30+ Hours of Entertainment!" style="position: absolute; top: 10px; left: 258px;" />
        <ul>
            <li>Take Yourself Back to  <br />
  Simpler Times</li>
            <li>Includes all your <br />
  Favorite Villains and <br />
  Celebrity Guest Stars</li>
            <li>Detailed Episode Guide <br />
  Gives Noteworthy  <br />
  Insights</li>
            <li>Perfect for the Holiday <br />
  Gift Giving Season!</li>
        </ul>
    </div>

    <div style="background: #231f20; margin-bottom: 4px;">
        <h2 class="f63 white pad0" style="padding-top: 0; line-height: 68px;">Holy Special Delivery!</h2>
        <h3 class="f39 white">Don't Wait, Get the Collection Now!</h3>
        <p class="white">They protected the streets of Gotham and flew into our collective hearts with their hilarious hijinks, over-the-top costumes and simply feel good fun. What are you waiting for? It’s the Bat Signal!</p>
        <p class="f40 bold"><a href="choose.aspx">Act Now<span style="display: inline-block; text-decoration: none; padding-left: 10px;"> ›</span></a></p>
    </div>

</div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

<uc:TrackingPixels ID="TrackingPixels" runat="server" />
</form>
</body>
</html>
