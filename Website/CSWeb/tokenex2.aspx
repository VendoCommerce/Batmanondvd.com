<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tokenex2.aspx.cs" Inherits="CSWeb.tokenex2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <!--######  TokenEx JavaScript file  ######
    #Description: the file provided by TokenEx to perform the Encryption -->
    <script type="text/javascript" src="https://test-api.tokenex.com/inpage/js/TokenEx-Lite.js"></script>
    <script type="text/javascript">
    <!--our encryption function-->
    function encryptCCnumber() {
        try {
            var creditCard = document.getElementById("txtCreditCard").value;

            //insert your custom credit card validation logic here.

            //grab the public key from the hidden field
            var key = document.getElementById('TxEncryptionKey').value;
            //encrypt the data
            var cipherText = TxEncrypt(key, creditCard);
            //add a new field to our form.
            var myin = document.createElement("input");
            myin.type = 'hidden';
            myin.name = 'tokenex_cipherText';
            myin.value = cipherText;
            document.getElementById('Form1').appendChild(myin);
            //remove the clear text credit card from the form
            document.getElementById("txtCreditCard").value = "";
            document.getElementById("hlToken").value = cipherText;
        }
        catch (e) {
            //display error message and prevent the post back
            var form = document.getElementById('Form1')
            var div = document.createElement("div");
            div.innerHTML = '<div id=\"error\" class=\"alert alert-error\"><strong>' + e + ' </strong><div class=\"pull-left\"><img src=\"img/RedX.png\" /></div><br /></div>';
            form.insertBefore(div, form.firstChild);
            return false;
        }
    }
    </script>
    <style type="text/css">
        #txtCipher {
            width: 231px;
        }
    </style>

</head>
<body>
    <form id="Form1" runat="server">

               <!--######  Encryption Key  ######
            #Description: This element contains your public key to be use to perform the encryption.-->
            <input runat="server" id="TxEncryptionKey" name="TxEncryptionKey" class="tokenex_encryptionkey" type="hidden" 
                value="MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvWpIQFjQQCPpaIlJKpeg
						irp5kLkzLB1AxHmnLk73D3TJbAGqr1QmlsWDBtMPMRpdzzUM7ZwX3kzhIuATV4Pe
						7RKp3nZlVmcrT0YCQXBrTwqZNh775z58GP2kZs+gVfNqBampJPzSB/hB62KkByhE
						Cn6grrRjiAVwJyZVEvs/2vrxaEpO+aE16emtX12RgI5JdzdOiNyZEQteU6zRBRJE
						ocPWVxExaOpVVVJ5+UnW0LcalzA+lRGRTrQJ5JguAPiAOzRPTK/lYFFpCAl/F8wt
						oAVG1c8zO2NcQ0Pko+fmeidRFxJ/did2btV+9Mkze3mBphwFmvnxa35LF+Cs/XJHDwIDAQAB" />
            <div class="form-group input-prepend">
                <label class="control-label" for="Data">Credit card number</label>
                <div class="controls">
                    <span class="add-on"><i class="icon-th"></i></span>
                    <!--######  Sensitive Data  ######
                    #Description: this element contains the sensitive data that should be encrypted. -->
                    <input runat="server" name="txtCreditCard" type="text" value="5500000000000004" id="txtCreditCard" />
                    <%--<img src="img/credit_cards.png" />--%>
                </div>
            </div>
            <div class="form-group input-prepend">
                <label class="control-label" for="ExpData">Expiration date</label>
                <div class="controls">
                    <span class="add-on"><i class="icon-calendar"></i></span>
                    <input runat="server" name="txtExpDate" type="text" value="0416" id="txtExpDate" />
                </div>
            </div>
            <div class="form-group input-prepend">
                <label class="control-label" for="Amount">Dollar Amount</label>
                <div class="controls">
                    <span class="add-on"><i class="icon-th"></i></span>
                    <input runat="server" name="txtAmount" type="text" value="5000" id="txtAmount" />
                </div>
            </div>
               <asp:HiddenField ID="hlToken" runat="server" />
            <br />
            <br />
            <div class="controls text-center">
                <!--###### Form Submit  ######
                #Description: With client Side Encryption-Lite, you have the ability to simply call our encryption function without relying on a form submit. 
                    In this example we are doing this in the encryptCCnumber() function -->
                &nbsp;<asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" OnClientClick="return encryptCCnumber();" />
&nbsp;<br />
                <br />
                Token: <input runat="server" name="txtCipher" type="text"  id="txtCipher" /><br />
                <br />
                CC#:
                <asp:TextBox ID="txtNoCC" runat="server" Width="155px"></asp:TextBox>
&nbsp;<br />
                <br />
                <asp:Button ID="btnTokenize" runat="server" OnClick="btnTokenize_Click" Text="Tokenize" />
                <br />
                <br />
                Tokenization Result:<br />
                <asp:Label ID="lblResult" runat="server"></asp:Label>
                <br />
                <asp:TextBox ID="txtTokenID" runat="server" Height="20px" Width="401px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnDeTokenize" runat="server" OnClick="btnDeTokenize_Click" Text="DeTokenize" />
                <br />
                <br />
                Original Value:
                <br />
                <asp:TextBox ID="txtOriginal" runat="server" Width="177px"></asp:TextBox>
                <br />

            </div>

    
        
               <hr />
               <p>
                   &nbsp;</p>
               <asp:Button ID="btnAuthorize" runat="server" OnClick="btnAuthorize_Click" Text="Authorize" />
               <br />
               <br />
               <asp:TextBox ID="txtAuthorizeResult" runat="server" Width="188px"></asp:TextBox>
               <br />
               <asp:Label ID="lblTransResult" runat="server"></asp:Label>

    
        
    </form>
</body>
</html>
