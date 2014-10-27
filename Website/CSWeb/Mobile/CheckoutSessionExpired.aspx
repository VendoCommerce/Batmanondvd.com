<%@Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutSessionExpired.aspx.cs" Inherits=" CSWeb.Mobile.Store.CheckoutSessionExpired" MasterPageFile="/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV</title>
<meta name="description" content=""/>
<meta name="keywords" content=""/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global_mobile.js"></script>
<link href="../styles/global_mobile.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="container">

<!--#include file="popups.html"-->
<!--#include file="header.html"-->

    <div class="content">
        <div style="padding-top: 40px;">
        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo_big.png" width="383" height="217" alt="Batman Classic TV Series" class="block" style="margin: 0 auto;" />

        <div class="clearfix">
            
            <div>
                <h2 class="f50">Unable to process request<br />
                    because your session is expired. <br />Please try again.
                </h2>
                <h3 class="f30" style="line-height: 1.2em; width: 500px;">Better <a href="index.aspx" class="black">race back to the Batcave</a>!</h3>
            </div>
            <div>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/jokersdown.jpg" width="609" height="397" alt="Sad Joker" class="block" style="margin: 0 auto;" />
            </div>
        </div>

        <div style="height: 335px;"></div>


    </div>

    </div>

<!--#include file="footer.html"-->
</div>

  </asp:Content>
