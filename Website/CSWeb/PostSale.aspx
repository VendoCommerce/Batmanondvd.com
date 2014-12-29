<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostSale.aspx.cs" EnableViewState="true"  Inherits="CSWeb.Root.Store.PostSale"
    EnableEventValidation="false" EnableSessionState="true" MaintainScrollPositionOnPostback="false" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %> 
<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>Batman | Classic TV Series Available on DVD and Bluray | As Seen on TV - Checkout</title>
<meta name="description" content=""/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key="JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<script src="/Scripts/NoBack.js"></script>
<link href="/styles/global.css" rel="stylesheet" type="text/css" />
    </head>
<body id="upsells">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <!--#include file="header_upsell.html"-->
    <asp:Panel ID="pnlManual" runat="server">
    
        <!-- show the content with yes or no button to process logic -->

    </asp:Panel>

    <div runat="server" id="mainContainer">
        
    </div>
    <asp:Button ID="btnYes" runat="server" Text="Button" OnClick="btnYes_OnClick" Visible="false" />
    <asp:Button ID="btnNo" runat="server" Text="Button" OnClick="btnNo_OnClick" Visible="false" />
    
    
<uc:TrackingPixels ID="TrackingPixels" runat="server" />
    </form>
    <script type="text/javascript">
        function validateForm() {
            var container = $('#mainContainer');
            var isValid = true;
            $('*[required=true]', container).each(function (a) {
                if (this.id == "") {
                    this.id = "required_" + a;
                }
                var errorMessageControlID = this.id + "_error";

                if ($("#" + errorMessageControlID).size() == 0) {
                    var message = this.getAttribute('error');
                    if (message == null) {
                        message = "*";
                    }
                    //Create control to display error message
                    $(this).after($('<span style="display:none;" class="error" id="' + errorMessageControlID + '">' + message + "</span>"));
                }

                if (!(typeof this.value === 'undefined') && this.value == "") {
                    isValid = isValid && false;
                    $("#" + errorMessageControlID).fadeIn();
                }
                else {
                    $("#" + errorMessageControlID).fadeOut();
                }
            });

            if (typeof (customValidateForm) == 'function') {
                isValid = isValid && customValidateForm();
            }

            return isValid            
        }
    </script>
</body>
</html>
