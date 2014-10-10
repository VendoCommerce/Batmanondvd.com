<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Mobile.Store.index" EnableSessionState="True" %>
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

        <a href="https://d1kg9stb0ddjcv.cloudfront.net/video/Aunt_Harriet.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/tv_clips.jpg" alt="Click to watch video" class="block" style="margin: 0 auto;" /></a>

        <h3 class="f44 black pad20 text-center">Click to View Clips</h3>
        
        <ul class="clipslist clearfix">
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/Aunt_Harriet.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_1.png" alt="Click to watch video" /></a></li>
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/RiddlerFight.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_2.png" alt="Click to watch video" /></a></li>
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/Flying_Blind.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_3.png" alt="Click to watch video" /></a></li>
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/Cloud_Mens_Minds.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_4.png" alt="Click to watch video" /></a></li>
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/CatFight.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_5.png" alt="Click to watch video" /></a></li>
            <li><a href="https://d1kg9stb0ddjcv.cloudfront.net/video/MinstrelCliffHanger.mp4" target="_blank"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_6.png" alt="Click to watch video" /></a></li>
        </ul>



    </div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>
