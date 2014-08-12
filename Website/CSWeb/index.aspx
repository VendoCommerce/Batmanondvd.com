<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Root.Store.index" EnableSessionState="True" %>


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
            &nbsp;</p>
        <uc1:ShippingForm ID="ShippingForm1" runat="server" RedirectUrl="addproduct.aspx" />
    </form>


</body>
</html>
