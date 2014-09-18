<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSWeb.index" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


* {
	margin: 0;
	padding: 0;	
	font-family: arial, sans-serif;
	box-sizing: border-box;
	-moz-box-sizing: border-box;
}
a {
	color: #ff7100;	
	text-decoration: none;
	transition: all .3s ease-in-out;
	-o-transition: all .3s ease-in-out;
	-moz-transition: all .3s ease-in-out;
	-webkit-transition: all .3s ease-in-out;
}

        .auto-style1 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
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
    </form>
</body>
</html>
