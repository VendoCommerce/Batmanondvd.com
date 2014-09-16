<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Root.Store.index" EnableSessionState="True" CodeBehind="~/index1.aspx.cs" %>


<%@ Register src="UserControls/ShippingForm.ascx" tagname="ShippingForm" tagprefix="uc1" %>


<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <link href="/styles/global.css" rel="stylesheet" type="text/css" media="all" />
</head>
<body>

	<form id="form1" runat="server">

	Welcome to Batman On DVD !!!<br />
        <p>
            <asp:DropDownList ID="ddlSimple" runat="server">
                <asp:ListItem Value="110">Classic Batman Collection on DVD</asp:ListItem>
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
            <asp:DropDownList ID="ddlComplete" runat="server">
                <asp:ListItem Value="111">Complete Classic Batman Collection on DVD</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:LinkButton ID="lbComplete" runat="server"><img class="prod_continue" src="http://dz97amgy09dju.cloudfront.net/images/B3/continue_btn.png" /></asp:LinkButton>
        </p>
    </form>


</body>
</html>
