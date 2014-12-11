<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.Tablet_BIG2.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/TrackingPixels.ascx" TagPrefix="uc1" TagName="TrackingPixels" %>


<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, maximum-scale=1.0">
<title>Batman | Cast Listing of Classic TV Series | As Seen on TV - Cast</title>
<meta name="description" content="Batman Classic TV Series - Now available on DVD & Bluray - Limited Time Offer!" />
<meta name="keywords" content="Batman Classic TV Series, DVD, Bluray, Warner Home Video, Adam West, As Seen on TV, Limited Time Offer, Robin, Joker, Riddler, Penguin, Catwoman, Original TV Series" />
<script src="//cdn.optimizely.com/js/77045885.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="../styles/global_tablet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="clearfix">
        <div class="content_logo"><a href="index.aspx"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo.png" width="194" height="156" alt="Batman Classic TV Series" class="block" /></a></div>
        <div class="content_hdr">
            <h2 class="f37 pad12 scored">A Cast of Characters</h2>
            <h2 class="f35">Diabolical Foes Are No Match for This Duo! <br />Delight in the Great Capers and Fabulous Foibles.</h2>
            <h3 style="padding-right: 10px;">From the raucous Riddler, to the persuasive Penguin, to the calculating Catwoman, watch as they attempt to confound our Caped Crusaders. Now completely remastered, all of the originality, crime fighting action, and arch-villainy but perfectly pixilated on DVD like never before. &nbsp;<a href="choose.aspx" class="webfont1bold">Get it now!<span style="display: inline-block; text-decoration: none;"> &nbsp;›</span></a></h3>
        </div>
    </div>
    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/skyline.png" alt="" class="block" />
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
        <div class="cast_member">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_catwoman.jpg" width="275" height="208" alt="Catwoman" class="block" />
            <h3>Catwoman</h3>
            <h4>Julie Newmar</h4>
            <p>A classy cat burglar rather than a traditional villain. The character is Batman's "purrrfect" love interest.</p>
        </div>
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
        <div class="cast_member">
            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/cast_riddler.jpg" width="275" height="208" alt="The Riddler" class="block" />
            <h3>The Riddler</h3>
            <h4>Frank Gorshin</h4>
            <p>The Riddler AKA "Count of Conundrums" baffled Batman &amp; Robin with his Riddles.</p>
        </div>
    </div>


    <!--#include file="bottomcta.html"-->
</div>


<!--#include file="footer.html"-->
    <uc1:TrackingPixels runat="server" ID="TrackingPixels" />
</form>
</body>
</html>
