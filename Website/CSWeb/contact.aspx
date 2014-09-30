<%--<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Root.Contact" EnableSessionState="True" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.index" EnableSessionState="True" %>

<%@ Register Src="UserControls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc" %>


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
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/jquery.cycle2.min.js"></script>
<script src="/Scripts/jquery.cycle2.carousel.min.js"></script>
<script src="/Scripts/jquery.cycle2.swipe.min.js"></script>
<script src="/Scripts/global.js"></script>
<link href="../styles/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!--#include file="popups.html"-->
<!--#include file="header.html"-->

<div class="container">
    <div class="clearfix">
        <div class="content_logo"><a href="#"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/logo.png" width="194" height="156" alt="Batman Classic TV Series" class="block" /></a></div>
        <div class="content_hdr">
            <h2 class="f50">To the Bat-Phone!</h2>
            <h3 class="f28">Or send an email from your Bat-Computer.</h3>
        </div>
    </div>



        
    <div style="padding: 16px 40px 30px 66px;">

        <p class="f20"><strong>To order, call:</strong> 1 (800) XXX-XXXX</p>

        <p class="f16" style="line-height: 30px;"><strong style="text-decoration: underline;">Customer Service</strong><br /><strong>Phone:</strong> 1-800-839-3005<br /><strong>Hours:</strong> Monday Through Friday 8A-8P EST, Saturday 9A-6P EST<br /><strong>Email:</strong> <a href="mailto:batmanondvd@datapakservices.com" class="caps" style="color: #000;">batmanondvd@datapakservices.com</a></p>


    <form runat="server" id="billing1">
            <asp:ScriptManager ID="Scriptmanager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
                    <asp:Panel ID="Panel_Contactus" runat="server">
                        <uc:ContactUs ID="ucContactUs" runat="server" Visible="false" />
                    </asp:Panel>
                     
      </ContentTemplate>
    </asp:UpdatePanel>
    </form>
                </div>
                
            <div class="clear"></div>


   <!--#include file="bottomcta.html"-->
</div>




<!--#include file="footer.html"-->
</body>
</html>
