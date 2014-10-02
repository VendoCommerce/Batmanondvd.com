<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choose.aspx.cs" Inherits="CSWeb.Mobile.choose" EnableSessionState="True" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:RadioButton ID="rbClassic" runat="server" Text="Classic Collection" GroupName="SKU" />
        <br />
        <asp:RadioButton ID="rbComplete" runat="server" Text="Complete Series" GroupName="SKU" />
        <br />
        <br />
        <asp:linkbutton ID="imgContinue" runat="server" OnClick="imgContinue_Click"><img class="prod_continue" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_continue.png" /></asp:linkbutton>
        <br />
        <br />
        <asp:Label ID="lblPrompt" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
