<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tokenex.ascx.cs" Inherits="CSWeb.UserControls.Tokenex" %>

<header>
    <!--TokenEx JavaScript file-->
    <script type="text/javascript" src="https://test-api.tokenex.com/inpage/js/TokenEx-Lite.js"></script>
    <script type="text/javascript">
        function encryptCCnumber() {
            try {
                var creditCard = document.getElementById("txtCCNumber1").value;
                    //+ document.getElementById("txtCCNumber2").value +
                    //document.getElementById("txtCCNumber3").value +
                    //document.getElementById("txtCCNumber4").value;

                //grab the public key from the hidden field
                var key = document.getElementById('TxEncryptionKey').value;
                //encrypt the data
                var cipherText = TxEncrypt(key, creditCard);
                //remove the clear text credit card from the form
                document.getElementById("txtCCNumber1").value = "XXXXXXXXXXXXXXXX";
                //document.getElementById("txtCCNumber2").value = "XXXX";
                //document.getElementById("txtCCNumber3").value = "XXXX";
                //document.getElementById("txtCCNumber4").value = "";
                document.getElementById("hlEncryptedCCNum").value = cipherText;
            }
            catch (e) {
                //display error message and prevent the post back
                //var form = document.getElementById('form1')
                //var div = document.createElement("div");
                //div.innerHTML ='<div id=\"error\" class=\"alert alert-error\"><strong>' + e + ' </strong><div class=\"pull-left\"><img src=\"img/RedX.png\" /></div><br /></div>';
                document.getElementById("lblCCNumberError").value = e;
                //form.insertBefore(div, form.firstChild);
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

<asp:HiddenField ID="hlEncryptedCCNum" runat="server" ClientIDMode="Static" />
