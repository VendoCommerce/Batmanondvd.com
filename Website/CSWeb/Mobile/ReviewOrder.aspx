<%@Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewOrder.aspx.cs" Inherits="CSWeb.Mobile.Store.ReviewOrder" EnableViewState="true" EnableSessionState="True" %>

<%@ Register Src="UserControls/ReviewOrder.ascx" TagName="Form" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <title></title>
    <link href="/Styles/global_C2.css" rel="stylesheet" type="text/css" media="all" />    
</head>
<body>
    <form id="reviewform" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <uc1:Form ID="Form1" runat="server" />
    </form>
</body>
</html>
