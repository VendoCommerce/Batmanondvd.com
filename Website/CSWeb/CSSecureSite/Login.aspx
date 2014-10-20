<%@ Page Title="Log In" Language="C#" AutoEventWireup="true" EnableViewState="true"
    CodeBehind="Login.aspx.cs" Inherits="CSWeb.Account.Login" EnableSessionState="True" %>

<!doctype html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>
        <%=siteName %>
        | Admin Login</title>
<link href="css/bootstrap.css" rel="stylesheet" media="screen">
<link rel="stylesheet" type="text/css" href="css/plugins.css">
<link rel="stylesheet" type="text/css" href="css/main.css">
<link rel="stylesheet" type="text/css" href="css/themes.css">
<link href="css/global.css" rel="stylesheet" type="text/css" />
<script src="js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
</head>
<body class="login">
<form runat="server" defaultbutton="LoginButton" id="form1">
<div id="login-container">
<div id="login-logo">
<a href="">
<img src="img/Logo.png" alt="logo">
</a>
</div>
<div id="login-buttons">
<h5 class="page-header-sub"><%=siteName %> Administration</h5>
</div>
<div id="login-form" class="form-inline">
<p class="text-center"><asp:Image ID="imgLogo" runat="server" /></p>
<div class="control-group">
<div class="controls">
    <span class="text-error"><asp:Literal id="liErrorMessage" runat="server" /></span>
    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"  ErrorMessage="* Username is required." ToolTip="Username is required." ValidationGroup="LoginUserValidationGroup" Display="Dynamic" CssClass="text-error" ></asp:RequiredFieldValidator>
<div class="input-append">

 <asp:TextBox ID="UserName" runat="server" ReadOnly="false" TabIndex="1" placeholder="Username..." AutoCompleteType="Disabled"></asp:TextBox>
<span class="add-on"><i class="icon-user"></i></span>
</div>

</div>
</div>
<div class="control-group">
<div class="controls"><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="* Password is required." ToolTip="Password is required." ValidationGroup="LoginUserValidationGroup" CssClass="text-error" Display="Dynamic"></asp:RequiredFieldValidator>
<div class="input-append"> 
<asp:TextBox ID="Password" runat="server" TextMode="Password" ReadOnly="false" TabIndex="2" placeholder="Password..."  AutoCompleteType="Disabled"></asp:TextBox>
<span class="add-on"><i class="icon-asterisk"></i></span>
</div>

</div>
</div>
<div class="control-group remove-margin clearfix">
                    <div class="btn-group pull-right">
                        
  
<asp:LinkButton ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="LoginUserValidationGroup" OnCommand="btnAction_Command" TabIndex="3" CssClass="btn btn-small btn-success"><i class="icon-arrow-right"></i> Login</asp:LinkButton>
                    </div>
                    <div class="input-switch switch-small pull-left" data-toggle="tooltip" title="Remember me" data-on="success" data-off="danger" data-on-label="<i class='icon-ok icon-white'></i>" data-off-label="<i class='icon-remove'></i>">
                        <input type="checkbox">
                    </div>
                </div>
</div>
</div>



    </form>
    
    
    
     <!-- Jquery library from Google ... -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
        <!-- ... but if something goes wrong get Jquery from local file -->
        <script>!window.jQuery && document.write(unescape('%3Cscript src="js/vendor/jquery-1.9.1.min.js"%3E%3C/script%3E'));</script>

        <!-- Bootstrap.js -->
        <script src="js/vendor/bootstrap.min.js"></script>

        <!--
        Include Google Maps API for global use.
        If you don't want to use  Google Maps API globally, just remove this line and the gmaps.js plugin from js/plugins.js (you can put it in a seperate file)
        Then iclude them both in the pages you would like to use the google maps functionality
        -->
        <script src="http://maps.google.com/maps/api/js?sensor=true"></script>

        <!-- Jquery plugins and custom javascript code -->
        <script src="js/plugins.js"></script>
        <script src="js/main.js"></script>
</body>
</html>
