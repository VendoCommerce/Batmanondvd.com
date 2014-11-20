<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1" />
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Clips</title>

<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script type="text/javascript" src="/Scripts/global_mobile.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />
 
<body>
 <form runat="server" id="fm1">
<div class="container">

<!--#include file="popups.html"-->
<!--#include file="header.html"-->

    <div class="content">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/logo_batman2.png" width="244" height="196" alt="Batman" style="position: absolute; top: 120px; left: 392px;" />
        <h2 class="f44" style="padding-top: 30px;">Check Out Your Favorite Caped <br />
            Crusaders - Back and Better <br />
            than Ever!
        </h2>
        <h3 style="line-height: 1.2em;">Watch Batman and his Boy Wonder <br />
            engage in hilarious hijinks as they <br />
            protect the streets of Gotham from <br />
            abominable archenemies wearing over-the-top <br />
            costumes that leave us laughing. It’s simply feel good fun!</h3>

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
                        width: 468, height: 284,
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

        

        <h3 class="f44 black pad20 text-center">Click to View Clips</h3>
        
        <ul class="clipslist clearfix">
            <li><a href="javascript: void(0);" class="test1"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_1.png" alt="Click to watch video" /></a></li>
            <li><a href="javascript: void(0);" class="test2"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_2.png" alt="Click to watch video" /></a></li>
            <li><a href="javascript: void(0);" class="test3"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_3.png" alt="Click to watch video" /></a></li>
            <li><a href="javascript: void(0);" class="test4"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_4.png" alt="Click to watch video" /></a></li>
            <li><a href="javascript: void(0);" class="test5"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_5.png" alt="Click to watch video" /></a></li>
            <li><a href="javascript: void(0);" class="test6"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_6.png" alt="Click to watch video" /></a></li>
        </ul>



    </div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>
