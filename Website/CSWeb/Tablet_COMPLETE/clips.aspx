<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet_COMPLETE.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8"><meta name="viewport" content="width=device-width, maximum-scale=1.0">
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Clips</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/jquery.cycle2.min.js"></script>
<script src="/Scripts/jquery.cycle2.carousel.min.js"></script>
<script src="/Scripts/jquery.cycle2.swipe.min.js"></script>
<script src="/Scripts/global.js"></script>
<link href="/styles/global_big2complete_tablet.css" rel="stylesheet" type="text/css" />
<script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
</head>
<body id="clips">
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="clearfix">
        <div class="content_hdr" style="padding-bottom: 30px; width: 800px;">
            <h2 class="f42">Check Out Your Favorite Caped Crusaders <br />- Back and Better than Ever!</h2>
            <h3>Watch Batman and his Boy Wonder engage in hilarious hijinks as they protect the streets of Gotham from abominable archenemies wearing over-the-top costumes that leave us laughing. It’s simply feel good fun!</h3>
        </div>
    </div>


    <div style="width: 887px; padding: 8px 0 30px 71px;">
        <div class="slideshow"
            data-cycle-fx="carousel"
            data-cycle-timeout="0"
            data-cycle-slides="> div"
            data-cycle-carousel-visible="6"
            data-allow-wrap=false
            data-cycle-next="#clipnext"
            data-cycle-prev="#clipprev">
            
            <div><a href="#" class="test1"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_1.png" /></a></div>
            <div><a href="#" class="test2"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_2.png" /></a></div>
            <div><a href="#" class="test3"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_3.png" /></a></div>
            <div><a href="#" class="test4"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_4.png" /></a></div>
            <div><a href="#" class="test5"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_5.png" /></a></div>
            <div><a href="#" class="test6"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/sm_thumb_6.png" /></a></div>

        </div>


        <a href="#" id="clipprev">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_slider_prev.png" alt="Previous" class="block" /></a>
        <a href="#" id="clipnext">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_slider_next.png" alt="Next" class="block" /></a>
    </div>

    
    <div class="clips clearfix">
        <div class="videowrap">
            <div class="tvpiece_1"></div>
            <div class="tvpiece_2"></div>
            <div class="tvpiece_3"></div>
            <div class="tvpiece_4"></div>
            <div class="tvpiece_center"></div>
            
            <div id="test1">
                <div id="videotest1"></div>
                <script type='text/javascript'>
                    jwplayer('videotest1').setup({
                        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Aunt_Harriet.mp4',
                        autostart: false,
                        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/vidposter_1.jpg',
                        controls: true,
                        width: 416, height: 250,
                        stretching: 'exactfit',
                        skin: '/scripts/jwplayer/bekle.xml'

                    });
                </script>
            </div>

            <div id="test2" style="display: none">
                <div id="videotest2"></div>
            </div>
            <div id="test3" style="display: none">
                <div id="videotest3"></div>
            </div>
            <div id="test4" style="display: none">
                <div id="videotest4"></div>
            </div>
            <div id="test5" style="display: none">
                <div id="videotest5"></div>
            </div>
            <div id="test6" style="display: none">
                <div id="videotest6"></div>
            </div>
            <div id="test7" style="display: none">
                <div id="videotest7"></div>
            </div>
        </div>
    </div>

    

    <!--#include file="bottomcta.html"-->
</div>


<!--#include file="footer.html"-->
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />
</form>
</body>
</html>
