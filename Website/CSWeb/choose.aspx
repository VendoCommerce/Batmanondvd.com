<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choose.aspx.cs" Inherits="CSWeb.choose" EnableSessionState="True" %>

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
    <!--#include file="header_cart.html"-->
        <div class="container_cart">
            <h2>Lorem Ipsum Dolor Sit Amet!</h2>
            <h3 class="pad20">Lorem Ipsum Dolor Sit Amet Consectetur Adipiscing Elit Sed Do Eiusmod</h3>


            <div class="chooseprod">
                <h2>Classic Batman Collection on DVD</h2>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/prod_choose_1.jpg" alt="" class="block" style="margin-bottom: 12px;" />
                <div class="choose_content">
                    <p class="text-center f16 pad6">Payment Type</p>
                    <p class="text-center pad6">
                        <asp:DropDownList ID="ddlComplete" runat="server" class="prodselect">
                            <asp:ListItem Value="110">Classic Batman Collection on DVD (SP)</asp:ListItem>
                            <asp:ListItem Value="112">Classic Batman Collection on DVD (MP)</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p class="text-center pad20 f10">plus $12<sup>99</sup> S&amp;H</p>
                    <p>Awesomeness described</p>
                    <ul>
                        <li>Awesome things listed</li>
                        <li>Awesome things listed</li>
                    </ul>
                    <p class="text-center pad12">
                        <asp:LinkButton ID="lbComplete" runat="server" OnClick="lbComplete_Click"><img class="prod_continue" src="http://dz97amgy09dju.cloudfront.net/images/B3/continue_btn.png" /></asp:LinkButton>
                    </p>
                    <p class="text-center"><strong><a href="#guarantee" class="guarantee">Guarantee!</a></strong></p>
                    <p class="text-center">
                        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/ssl.png" alt="SSL Secured Online Ordering" /></p>
                </div>
            </div>
            

            <div class="chooseprod" style="margin: 0;">
                <h2><span class="green">Complete</span> Classic Batman Collection on DVD</h2>
                <img src="//d1kg9stb0ddjcv.cloudfront.net/images/prod_choose_2.jpg" alt="" class="block" style="margin-bottom: 12px;" />
                <div class="choose_content bg_greengradient">
                    <img src="//d1kg9stb0ddjcv.cloudfront.net/images/flag_bestvalue.png" alt="Best Value" style="position:absolute;
top: 24px; left: -17px;" />
                    <p class="text-center f16 pad6">Payment Type</p>
                    <p class="text-center pad6">
                        <asp:DropDownList ID="ddlSimple" runat="server" class="prodselect">
                            <asp:ListItem Value="111">Complete Classic Batman Collection on DVD</asp:ListItem>
                            <asp:ListItem Value="113">Complete Classic Batman Collection on DVD (MP)</asp:ListItem>
                            <asp:ListItem Value="114">Complete Classic Batman Collection on BD</asp:ListItem>
                            <asp:ListItem Value="115">Complete Classic Batman Collection on BD (MP)</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p class="text-center pad20 f10">plus $12<sup>99</sup> S&amp;H</p>
                    <p>Awesomeness described more</p>
                    <ul>
                        <li>Awesome things listed more</li>
                        <li>Awesome things listed more</li>
                    </ul>
                    <p class="text-center pad12">
                        <asp:LinkButton ID="lbSimple" runat="server" OnClick="lbSimple_Click"><img class="prod_continue" src="http://dz97amgy09dju.cloudfront.net/images/B3/continue_btn.png" /></asp:LinkButton>
                    </p>
                    <p class="text-center"><strong><a href="#guarantee" class="guarantee">Guarantee!</a></strong></p>
                    <p class="text-center">
                        <img src="//d1kg9stb0ddjcv.cloudfront.net/images/ssl.png" alt="SSL Secured Online Ordering" /></p>
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
