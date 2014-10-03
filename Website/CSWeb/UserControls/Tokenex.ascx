<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tokenex.ascx.cs" Inherits="CSWeb.UserControls.Tokenex" %>

<header>
    <!--TokenEx JavaScript file-->
    <script type="text/javascript" src="https://test-api.tokenex.com/inpage/js/TokenEx-Lite.js"></script>
    <script type="text/javascript">
        function encryptCCnumber() {
            try {
                var creditCard = document.getElementById("txtCCNumber1").value;
                var key = document.getElementById('TxEncryptionKey').value;
                var apiKey = document.getElementById('hlApiKey').value;
                var tokenExId = document.getElementById('hlTokenExID').value;
                var cipherText = TxEncrypt(key, creditCard);
                document.getElementById("txtCCNumber1").value = "XXXXXXXXXXXXXXXX";
                //document.getElementById("hlEncryptedCCNum").value = cipherText;
                var requestString = "{\"APIKey\":\"@apiKey@\",\"TokenExID\":\"@tokenExID@\",\"EcryptedData\":\"@encryptedData@\",\"TokenScheme\":2}";
                requestString = requestString.replace("@apiKey@", apiKey);
                requestString = requestString.replace("@tokenExID@", tokenExId);
                requestString = requestString.replace("@encryptedData@", cipherText);
                
                // construct an HTTP request
                var xhr = new XMLHttpRequest({ mozSystem: true });
                xhr.open('POST', 'https://test-api.tokenex.com/TokenServices.svc/REST/TokenizeFromEncryptedValue', true);
                xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
                xhr.setRequestHeader('Accept', 'application/json; charset=UTF-8');

                // send the collected data as JSON
                xhr.send(requestString);
                
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        var responseObject = JSON.parse(xhr.responseText);
                        document.getElementById("hlToken").value = responseObject.Token;
                    }

            } catch (e) {
                document.getElementById("lblCCNumberError").value = e;
                return false;
            } 
        }
    </script>
</header>

<!--This element contains Tekonex public key to be use to perform the encryption.-->
<asp:HiddenField ID="TxEncryptionKey" runat="server" ClientIDMode="Static" Value="MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvWpIQFjQQCPpaIlJKpeg
						irp5kLkzLB1AxHmnLk73D3TJbAGqr1QmlsWDBtMPMRpdzzUM7ZwX3kzhIuATV4Pe
						7RKp3nZlVmcrT0YCQXBrTwqZNh775z58GP2kZs+gVfNqBampJPzSB/hB62KkByhE
						Cn6grrRjiAVwJyZVEvs/2vrxaEpO+aE16emtX12RgI5JdzdOiNyZEQteU6zRBRJE
						ocPWVxExaOpVVVJ5+UnW0LcalzA+lRGRTrQJ5JguAPiAOzRPTK/lYFFpCAl/F8wt
						oAVG1c8zO2NcQ0Pko+fmeidRFxJ/did2btV+9Mkze3mBphwFmvnxa35LF+Cs/XJHDwIDAQAB" />
<!--This element contains Tekonex ApiKey to be use to perform the encryption.-->

<asp:HiddenField ID="hlApiKey" runat="server" ClientIDMode="Static" Value="mUyKbhAEKF6jG6EAWXvx" />
<!--This element contains Tekonex TokenExId to be use to perform the encryption.-->
<asp:HiddenField ID="hlTokenExID" runat="server" ClientIDMode="Static" Value="7655146737828306" />
<!--This element contains Tekonex Encrypted CCnum Store field to be use to perform the encryption.-->
<asp:HiddenField ID="hlEncryptedCCNum" runat="server" ClientIDMode="Static" />
<!--This element contains Tekonex Encrypted CCnum Token Field Store field to be use to perform the encryption.-->
<asp:HiddenField ID="hlToken" runat="server" ClientIDMode="Static" />
