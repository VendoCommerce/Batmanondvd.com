<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tokenex.ascx.cs" Inherits="CSWeb.UserControls.Tokenex" %>

<header>
    <!--TokenEx JavaScript file-->
    <script type="text/javascript" src="<%= TokenExJSFile %>"></script>
    <script type="text/javascript">
        function encryptCCnumber() {
            try {
                MM_showHideLayers('mask', '', 'show');
                var creditCard = document.getElementById("txtCCNumber1").value;
                if (creditCard == "" | creditCard == "XXXXXXXXXXXXXXXX") return true;
                var key = document.getElementById('TxEncryptionKey').value;
                var apiKey = document.getElementById('hlApiKey').value;
                var tokenExId = document.getElementById('hlTokenExID').value;
                var tokenExAPIUrl = document.getElementById('hlTokenExAPIUrl').value;
                var cipherText = TxEncrypt(key, creditCard);
                //Comment for JSON
                document.getElementById("txtCCNumber1").value = "XXXXXXXXXXXXXXXX";
                document.getElementById("hlEncryptedCCNum").value = cipherText;
                ////var requestString = "{\"APIKey\":\"@apiKey@\",\"TokenExID\":\"@tokenExID@\",\"EcryptedData\":\"@encryptedData@\",\"TokenScheme\":2}";
                ////////requestString = requestString.replace("@apiKey@", apiKey);
                ////////requestString = requestString.replace("@tokenExID@", tokenExId);
                ////////requestString = requestString.replace("@encryptedData@", cipherText);

                ////////////// construct an HTTP request
                ////////var xhr = new XMLHttpRequest({ mozSystem: true });
                ////////xhr.open('POST', tokenExAPIUrl, false);
                ////////xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
                ////////xhr.setRequestHeader('Accept', 'application/json; charset=UTF-8');

                ////////////// send the collected data as JSON
                ////////xhr.send(requestString);

                ////////if (xhr.readyState == 4 && xhr.status == 200) {
                ////////    var responseObject = JSON.parse(xhr.responseText);
                ////////    document.getElementById("hlToken").value = responseObject.Token;
                ////////    document.getElementById("txtCCNumber1").value = "XXXXXXXXXXXXXXXX";
                ////////    //alert(responseObject.Token);
                ////////}

                __doPostBack('bscfShippingBillingCreditForm$imgBtn', '');
            } catch (e) {
                //alert(e.message);
                //document.getElementById("lblCCNumberError").textContent = e.message;
                return false;
            }
        }
    </script>
</header>

<asp:HiddenField ID="TxEncryptionKey" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hlTokenExAPIUrl" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hlApiKey" runat="server" ClientIDMode="Static" Value="mUyKbhAEKF6jG6EAWXvx" />
<asp:HiddenField ID="hlTokenExID" runat="server" ClientIDMode="Static" Value="7655146737828306" />
<asp:HiddenField ID="hlEncryptedCCNum" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hlToken" runat="server" ClientIDMode="Static" />


