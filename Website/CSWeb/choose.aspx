﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choose.aspx.cs" Inherits="CSWeb.choose" EnableSessionState="True" %>

<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>BATMAN</title>
<meta name="description" content=""/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="../styles/global.css" rel="stylesheet" type="text/css" />
</head>
<body id="cart">
    <form id="form1" runat="server">
    <!--#include file="popups.html"-->
    <!--#include file="header_cart.html"-->
        <div class="container_cart">
            <h2>Holy Gadget Choices Batman!</h2>
            <h3 class="pad20">Which Collection Is For You?</h3>


            <div class="chooseprod">
                <div class="text-center">
                    <h2 class="pad20">Classic Batman Collection</h2>
                    <p class="text-center f16 pad6">Payment Type</p>
                    <p class="text-center pad12">
                        <asp:DropDownList ID="ddlComplete" runat="server" class="prodselect">
                            <asp:ListItem Value="110">Classic Batman Collection on DVD (SP)</asp:ListItem>
                            <asp:ListItem Value="112">Classic Batman Collection on DVD (MP)</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p class="text-center pad20 f21"><strong>+</strong> <span class="f21 caps webfont1bold blue">FREE SHIPPING & HANDLING!</span></p>
                    <p class="text-center pad12">
                        <asp:LinkButton ID="lbComplete" runat="server" OnClick="lbComplete_Click"><img class="prod_continue" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_continue.png" /></asp:LinkButton>
                    </p>
                </div>
                
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/prod_choose_1.png" alt="" class="block" style="margin-bottom: 10px;" />
                <div class="choose_content">
                    
                    <p class="f16 lh22 pad0" style="height: 78px;">Your 12 DVD Classic Collection Features <br />
                        Over 30 HOURS OF ENTERTAINMENT!</p>
                    <p>INCLUDES:</p>
                    <ul>
                        <li>64 of your favorite original broadcast episodes fully remastered in HD </li>
                        <li>Over 3 Hours of ALL NEW bonus materials</li>
                        <li>Adam West Naked on DVD: Watch as Adam takes you   behind the scenes of your favorite epidodes!</li>
                        <li>The Original 1966 Batman Movie DVD starring <br />  Adam West and Burt Ward</li>
                        <li>Detailed Episode Guide</li>
                        <li>Your Own Show Script from the Episode  <span style="white-space: nowrap">‘The Joker is Wild’</span></li>
                        <li>Personal Letter from Adam West to you!</li>
                    </ul>
                    <%--<p class="text-center"><strong><a href="#guarantee" class="guarantee">Guarantee!</a></strong></p>--%>
                    <p class="text-center" style="padding-top: 20px;">
                        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/ssl.png" alt="SSL Secured Online Ordering" /></p>
                </div>
            </div>
            

            <div class="chooseprod" style="margin: 0;">
                <div class="text-center">
                    <h2 class="pad20"><span class="green">Complete</span> Classic Batman Collection</h2>
                    
                    <p class="text-center f16 pad6">Payment Type</p>
                    
                    <div>
                        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/flag_bestvalue.png" alt="Best Value" style="position:absolute;
top: 0; left: -25px;" />
                        <p class="text-center pad12">
                        <asp:DropDownList ID="ddlSimple" runat="server" class="prodselect">
                            <asp:ListItem Value="111">Complete Classic Batman Collection on DVD</asp:ListItem>
                            <asp:ListItem Value="113">Complete Classic Batman Collection on DVD (MP)</asp:ListItem>
                            <asp:ListItem Value="114">Complete Classic Batman Collection on BD</asp:ListItem>
                            <asp:ListItem Value="115">Complete Classic Batman Collection on BD (MP)</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    </div>
                    <p class="text-center pad20 f21"><strong>+</strong> <span class="f21 caps webfont1bold blue">FREE SHIPPING & HANDLING!</span></p>
                    <p class="text-center pad12">
                        <asp:LinkButton ID="lbSimple" runat="server" OnClick="lbSimple_Click"><img class="prod_continue" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_continue.png" /></asp:LinkButton>
                    </p>
                </div>
                
                <div class="bg_greengradient">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/prod_choose_2.png" alt="" class="block" style="margin-bottom: 10px;" />
                    <div class="choose_content">
                        <p class="f16 pad0" style="height: 78px;">Your Complete Collection Features Over 50 HOURS OF ENTERTAINMENT And comes in a Classic Collector’s Box for displaying your Collection!</p>
                        <p>INCLUDES:</p>    
                        <ul>
                            <li>120 of your favorite original broadcast episodes fully remastered</li>
                            <li>Over 3 Hours of ALL NEW bonus materials</li>
                            <li>Adam West Naked on DVD: Watch as Adam takes you   behind the scenes of your favorite epidodes!</li>
                            <li>The Original 1966 Batman Movie DVD starring <br />  Adam West and Burt Ward</li>
                            <li>Detailed Episode Guide</li>
                            <li>Your Own Show Script from the Episode  <span style="white-space: nowrap">‘The Joker is Wild’</span></li>
                            <li>Personal Letter from Adam West to you!</li>
                        </ul>
                        <%--<p class="text-center"><strong><a href="#guarantee" class="guarantee">Guarantee!</a></strong></p>--%>
                        <p class="text-center" style="padding-top: 20px;">
                            <img src="//d1kg9stb0ddjcv.cloudfront.net/images/ssl.png" alt="SSL Secured Online Ordering" />
                        </p>
                    </div>
                </div>
                
            </div>


        
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        
        




            <div class="clear"></div>
    </div>


        <!--#include file="footer.html"-->
    </form>
</body>
</html>
