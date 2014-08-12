using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSPaymentProvider;
using CSBusiness;
using System.IO;
using System.Xml;
using CSBusiness.OrderManagement;
using CSWeb.TokenEx_Test;
using CSWeb.App_Code.Tokenization;

namespace CSWeb.Tokenization
{
    [Serializable]
    public class TokenexProcessor
    {
        //API parameters
        string APIKey = string.Empty;
        string TokenExID = string.Empty;
        string GatewayName = string.Empty;
        string GatewayLogin = string.Empty;
        string GatewayPassword = string.Empty;

        //Setting up required values for each call
        public TokenexProcessor()
        {
            APIKey = "mUyKbhAEKF6jG6EAWXvx";
            TokenExID = "7655146737828306";
            GatewayName = "FirstdataE4Gateway";
            GatewayLogin = "AF8871-05";
            GatewayPassword = "7nb86t1z";
        }

        /// <summary>
        /// Instance can only be obtained from this method, which uses cache if possible.
        /// </summary>
        /// <returns>New instance of the class</returns>
        public static TokenexProcessor GetInstance()
        {
            if (HttpContext.Current.Session["Tokenex"] != null)
                return (TokenexProcessor)HttpContext.Current.Session["Tokenex"];
            else
            {
                TokenexProcessor tp = new TokenexProcessor();
                HttpContext.Current.Session["Tokenex"] = tp;
                return tp;
            }
        }

        /// <summary>
        /// Sends the encrypted card number to Tokenex for tokenization
        /// </summary>
        /// <returns>Token ID</returns>
        public string Tokenize(string encryptedValue)
        {
            //create our client
            var client = new TokenServicesClient();
            //create our token action
            var action = new TokenizeFromEncryptedValueAction();

            action.APIKey = APIKey;
            action.TokenExID = TokenExID;
            action.EcryptedData = encryptedValue;
            action.TokenScheme = TokenTypeEnum.nTOKEN;

            //call to Tokenize Method
            var result = client.TokenizeFromEncryptedValue(action);
            if (result.Success)
                return result.Token;
            else
                return "ERROR";
        }

        /// <summary>
        /// Performs a Process Transaction request on First Data Gateway.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response PerformAuthRequest(Request request)
        {
            //create our client
            TokenServicesClient client = new TokenServicesClient();
            //create our token action
            ProcessTransationAction action = new ProcessTransationAction();
            //your tokenex credentials
            action.APIKey = APIKey;
            action.TokenExID = TokenExID;
            //is the TransactionRequest json or xml;
            action.TransactionRequestFormat = TransactionRequestFormatEnum.XML;
            //what type of transaction we are performing
            action.TransactionType = TransactionTypeEnum.Authorize;
            //Get xml request
            string requestXML = RequestGenerator.GetAuthorizationRequest(request);
            requestXML = requestXML.Replace("[GATEWAYNAME]", GatewayName);
            requestXML = requestXML.Replace("[LOGIN]", GatewayLogin);
            requestXML = requestXML.Replace("[PASSWORD]", GatewayPassword);
            action.TransactionRequest = requestXML;
            //call the web service
            ResultOfProcessTransaction result = client.ProcessTransaction(action);
            //Generate and populate response object
            Response response = new Response();
            response.GatewayRequestRaw = requestXML;
            response.GatewayResponseRaw =GetRawResponse(result);
            //if our call was a success, save authorization code
            response.TransactionID = result.ReferenceNumber;
            response.AuthCode = result.Authorization;
            if (result.Success)
                response.ResponseType = TransactionResponseType.Approved;
            else if (result.Error != null && result.Error.Length > 0)
            {
                response.ResponseType = TransactionResponseType.Error;
                response.ReasonText = result.Error;
            }
            else
            {
                response.ResponseType = TransactionResponseType.Denied;
                response.ReasonText = result.Error;
            }

            return response;
        }

        public string GetRawResponse(ResultOfProcessTransaction result)
        {
             return string.Format("Result:'{0}' Auth Code:'{1}' Message:'{2}' Error:'{3}' Ref #:'{4}' Test:{5})",
               result.Success.ToString(), result.Authorization, result.Message, result.Error, result.ReferenceNumber, result.Test.ToString());
        }
    }
}