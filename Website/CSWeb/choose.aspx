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
<body>
    <form id="form1" runat="server">
    <!--#include file="header.html"-->
        <div class="container">
    
	Welcome to Batman On DVD !!!<br />
        <p>
            <asp:DropDownList ID="ddlSimple" runat="server">
                <asp:ListItem Value="111">Complete Classic Batman Collection on DVD</asp:ListItem>
                <asp:ListItem Value="113">Complete Classic Batman Collection on DVD (MultiPay)</asp:ListItem>
                <asp:ListItem Value="114">Complete Classic Batman Collection on BD</asp:ListItem>
                <asp:ListItem Value="115">Complete Classic Batman Collection on BD (MultiPay)</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:LinkButton ID="lbSimple" runat="server" OnClick="lbSimple_Click"><img class="prod_continue" src="http://dz97amgy09dju.cloudfront.net/images/B3/continue_btn.png" /></asp:LinkButton>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:DropDownList ID="ddlComplete" runat="server">
                <asp:ListItem Value="110">Classic Batman Collection on DVD (Single Pay)</asp:ListItem>
                <asp:ListItem Value="112">Classic Batman Collection on DVD (MultiPay)</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:LinkButton ID="lbComplete" runat="server" OnClick="lbComplete_Click"><img class="prod_continue" src="http://dz97amgy09dju.cloudfront.net/images/B3/continue_btn.png" /></asp:LinkButton>
        </p>
    
    </div>


        <!--#include file="footer.html"-->
    </form>
</body>
</html>
