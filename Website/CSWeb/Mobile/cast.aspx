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
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/tv.jpg" width="176" height="145" alt="" style="position: absolute; top: 42px; left: 455px;" />
        <h2 class="f37 pad12 scored">A Cast of Characters</h2>
        <h2 class="f37" style="padding-top: 0;">Diabolical Foes Are No <br />
            Match for This Duo!<br />
            Delight in the Great Capers <br />
            and Fabulous Foibles.</h2>
        <h3>From the raucous Riddler, to the persuasive Penguin, to the calculating Catwoman, watch as they attempt to confound our Caped Crusaders. Now completely remastered, all of the originality, crime fighting action, and arch-villainy but perfectly pixilated on DVD like never before. &nbsp;<a href="choose.aspx" class="webfont1bold">Get it now!<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></h3>

        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/mobile/skyline.png" alt="" class="block" />


        <div class="cast clearfix">
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_batman.jpg" width="275" height="208" alt="Batman" class="block" />
                <h3>Batman</h3>
                <h4>Adam West</h4>
                <p>The secret identity of Bruce Wayne, an American billionaire, philanthropist and ordinary human being who adopts the form of a 'bat' to conceal his identity while terrorizing evil-doers.</p>
            </div>
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_robin.jpg" width="275" height="208" alt="Robin" class="block" />
                <h3>Robin</h3>
                <h4>Burt Ward</h4>
                <p>Batman's aid in crime fighting. Robin is best known for his "holy" phrases.</p>
            </div>
        </div>
        <div class="cast clearfix">
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_catwoman.jpg" width="275" height="208" alt="Catwoman" class="block" />
                <h3>Catwoman</h3>
                <h4>Julie Newmar</h4>
                <p>A classy cat burglar rather than a traditional villain. The character is Batman's "purrrfect" love interest.</p>
            </div>
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_riddler.jpg" width="275" height="208" alt="The Riddler" class="block" />
                <h3>The Riddler</h3>
                <h4>Frank Gorshin</h4>
                <p>The Riddler AKA "Count of Conundrums" baffled Batman &amp; Robin with his Riddles.</p>
            </div>
        </div>
        <div class="cast clearfix" style="padding-bottom: 50px;">
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_joker.jpg" width="275" height="208" alt="The Joker" class="block" />
                <h3>The Joker</h3>
                <h4>Cesar Romero</h4>
                <p>The Joker AKA the "Clown Prince of Crime." The antithesis of Batman in personality and appearance, the Joker is his perfect adversary.</p>
            </div>
            <div class="cast_member">
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_penguin.jpg" width="275" height="208" alt="Penguin" class="block" />
                <h3>Penguin</h3>
                <h4>Burgess Meredith</h4>
                <p>The waddling bird of fowl play. A mobster and thief, he fancies himself as being a "gentleman of crime."</p>
            </div>
        </div>


    </div>
<!--#include file="bottomcta.html"-->
<!--#include file="footer.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>
