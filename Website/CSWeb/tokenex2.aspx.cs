using CSWeb.TokenEx_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb
{
    public partial class tokenex2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            txtCipher.Value = hlToken.Value;
            txtNoCC.Text = txtCreditCard.Value;
        }

        protected void btnTokenize_Click(object sender, EventArgs e)
        {
            //create our client
            var client = new TokenServicesClient();
            //create our token action
            var action = new TokenizeFromEncryptedValueAction();

            action.APIKey = "mUyKbhAEKF6jG6EAWXvx";
            action.TokenExID = "7655146737828306";
            action.EcryptedData = txtCipher.Value;
            action.TokenScheme = TokenTypeEnum.nTOKEN;

            //call to Tokenize Method
            var result = client.TokenizeFromEncryptedValue(action);
            //is our call was a success, show the token else show an error
            lblResult.Text=result.Success.ToString() + " : " + result.Error + result.Token + Environment.NewLine;
            txtTokenID.Text = result.Token;
        }

        protected void btnDeTokenize_Click(object sender, EventArgs e)
        {
            //create our client
            var client = new TokenServicesClient();
            //create our token action
            var action = new DetokenizeAction();

            action.APIKey = "mUyKbhAEKF6jG6EAWXvx";
            action.TokenExID = "7655146737828306";
            action.Token = txtTokenID.Text;
            //action.TokenScheme = TokenTypeEnum.nTOKEN;

            //call to Tokenize Method
            var result = client.Detokenize(action);
            //is our call was a success, show the token else show an error
           txtOriginal.Text = result.Success.ToString() + " : " + result.Error + result.Value + Environment.NewLine;

        }

        protected void btnAuthorize_Click(object sender, EventArgs e)
        {
            //create our client
            TokenServicesClient client = new TokenServicesClient();
            //create our token action
            ProcessTransationAction action = new ProcessTransationAction();
            //your tokenex credentials
            action.APIKey = "mUyKbhAEKF6jG6EAWXvx";
            action.TokenExID = "7655146737828306";
            //is the TransactionRequest json or xml;
            action.TransactionRequestFormat = TransactionRequestFormatEnum.XML;
            //what type of transaction we are performing
            action.TransactionType = TransactionTypeEnum.Authorize;
            //Get xml request
            string xmlRequest = System.IO.File.ReadAllText("j:\\tokenexrequest.xml");
            //Replace live values:
            xmlRequest = xmlRequest.Replace("[TOKEN]", txtTokenID.Text);
            xmlRequest = xmlRequest.Replace("[AMOUNT]", txtAmount.Value);
            action.TransactionRequest = xmlRequest;

                //@"<?xml version=""1.0"" encoding=""utf-8"" ?>" +
                //"<Transaction>" +
                //"  <ExactID>AF8871-05</ExactID>" +
                //"  <Password>7nb86t1z</Password>" +
                //"  <Card_Number>5454545454545454</Card_Number>" +
                //"  <CardHoldersName>ARTHUR DIGBY SELLERS</CardHoldersName>" +
                //"  <Transaction_Type>00</Transaction_Type>" +
                //"  <Expiry_Date>0915</Expiry_Date>" +
                //"  <DollarAmount>168.67</DollarAmount>" +
                //"  <Address>" +
                //"    <Address1>21 Jump Street</Address1>" +
                //"    <City>Los Angeles</City>" +
                //"    <Zip>90210</Zip>" +
                //"    <PhoneNumber>5557891234</PhoneNumber>" +
                //"    <PhoneType>W</PhoneType>" +
                //"  </Address>" +
                //"</Transaction>" ;
            
            //call the web service
            ResultOfProcessTransaction result = client.ProcessTransaction(action);
            //is our call was a success, show the authorization code else show an error
            if (result.Success)
            {
               lblTransResult.Text=string.Format("Successfull call, authorization result is '{0}' <br> Other info are '{1}'", result.Authorization,result.Message);
            }
            else
            {
                lblTransResult.Text=string.Format("error is {0}", result.Error);
            }
        }
    }
}