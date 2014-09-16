using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;


namespace CSPaymentProvider.Providers
{
    public class ePayAccount : IPaymentProvider
    {

        #region Private Members

        private static GatewaySettings gatewaySettings = null;
        //private static string errRequiredNode = "A required gateway node does not exist: {0}";

        #endregion

        #region Public Methods

        public Response PerformVoidSettledRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
        public Response PerformValidationRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
        public Response PerformRequest(Request request)
        {

            Response response = new Response();
            StreamWriter streamWriter = null;
            StreamReader streamReader = null;
            string strPost = string.Empty;


            strPost = BuildRequestPost(request);

            //Initialize & populate HTTP request object
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);

            //set header attributes
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {

                //write post for request to url
                streamWriter = new StreamWriter(objRequest.GetRequestStream());
                streamWriter.Write(strPost);

            }
            catch (Exception ex)
            {
                //error while writing post
                throw new PaymentProviderException("An exception occured while getting the request stream for the gateway", ex);
            }
            finally
            {
                streamWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            try
            {

                //read response from request
                streamReader = new StreamReader(objResponse.GetResponseStream());

                response = ParseResponse(streamReader.ReadToEnd());

            }
            catch (Exception ex)
            {
                throw new PaymentProviderException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {
                streamReader.Close();
            }

            return response;

        }
        #endregion
        public Response PerformVoidRequest(Request request)
        {

            Response response = new Response();
            return response;

        }

        protected string BuildRequestPost(Request request)
        {

            string NameValueFormat = "{0}={1}";

            List<string> aryPostParams = new List<string>();


            //Create parameters from objProvider settings
            aryPostParams.Add(string.Format(NameValueFormat, "ePayAccountNum", gatewaySettings.EPayAccountNum));
            aryPostParams.Add(string.Format(NameValueFormat, "password", gatewaySettings.Password));
            aryPostParams.Add(string.Format(NameValueFormat, "transactionCode", gatewaySettings.TransactionCode));


            //Create parameters from request

            aryPostParams.Add(string.Format(NameValueFormat, "orderNum", request.InvoiceNumber));

            try
            {

                if ((request.RequestType == PaymentRequestType.Credit || request.RequestType == PaymentRequestType.sale) && request.TransactionID.Trim().Length == 0)
                {
                    throw new PaymentProviderException("This request type requires a valid transaction id");
                }



                if (request.Amount > 0)
                {
                    aryPostParams.Add(string.Format("transactionAmount={0:.00}", request.Amount));
                }
                //load optional params
                aryPostParams.Add(string.Format(NameValueFormat, "CVV2", request.CardCvv));
                aryPostParams.Add(string.Format("expirationDate={0:####}", request.ExpireDate.ToString("MMyy")));
                aryPostParams.Add(string.Format(NameValueFormat, "cardAccountNum", request.CardNumber));
                aryPostParams.Add(string.Format(NameValueFormat, "InstallmentNum", "01"));
                aryPostParams.Add(string.Format(NameValueFormat, "InstallmentOf", "02"));

                if (request.CustomerID.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CustomerNum", request.CustomerID));
                if (request.FirstName.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CardHolderName", request.FirstName + "+" + request.LastName));
                if (request.Address1.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CardHolderAddress", request.Address1 + (string.IsNullOrEmpty(request.Address2) == false ? "+" + request.Address2 : string.Empty)));
                if (request.City.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CardHolderCity", request.City));
                if (request.State.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CardHolderState", request.State));
                if (request.ZipCode.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "CardHolderZip", request.ZipCode));

                bool testMode = false;
                bool.TryParse(gatewaySettings.TestMode.ToString(), out testMode);
                aryPostParams.Add(string.Format(NameValueFormat, "testTransaction", (testMode == true ? "Y" : "N")));


            }
            catch (Exception ex)
            {

                throw new PaymentProviderException(string.Format("An exception occured while creating the post parameters: {0}", ex.Message), ex);
            }

            return string.Join("&", aryPostParams.ToArray());

        }

        private Response ParseResponse(string GatewayResponse)
        {

            Response response = new Response();

            response.GatewayResponseRaw = GatewayResponse;

            string[] aryGatewayResponse = GatewayResponse.Split(Convert.ToChar(gatewaySettings.DelimChar));


            if (aryGatewayResponse.Length > 0)
            {
                switch (aryGatewayResponse[10])
                {
                    case "00":
                    case "000":

                        //Approved
                        response.ResponseType = TransactionResponseType.Approved;
                        break;
                    case "15":
                        //Declined
                        response.ResponseType = TransactionResponseType.Denied;
                        break;
                    default:
                        //Error
                        response.ResponseType = TransactionResponseType.Error;
                        break;
                }


                response.ReasonText = aryGatewayResponse[3];
                response.TransactionID = aryGatewayResponse[14];
                response.AuthCode = aryGatewayResponse[13];
            }
            else
            {
                response.ResponseType = TransactionResponseType.Error;
                response.ReasonText = "Unknown Error (" + aryGatewayResponse + ")";
            }

            return response;

        }

        private class GatewaySettings
        {

            //#region Private Members

            //private string m_Login;
            //private string m_Password;
            //private string m_TransactionKey;
            //private string m_DelimData = "TRUE";
            //private string m_DelimChar;
            //private string m_EncapChar;
            //private string m_Version;
            //private string m_RelayResponse = "FALSE";
            //private string m_TransactionURL;
            //private string m_TestMode = "TRUE";
            //private string m_DuplicateWindow;
            //private string m_EmailCustomer;
            //private string m_MerchantEmail;
            //private string m_CurrencyCode;
            //private string m_MarketType = "";
            //private string m_DeviceType = "";
            //private string m_RequestType = "";
            //#endregion

            #region Public Properties
            public string CurrencyCode { get; set; }
            public string MerchantEmail { get; set; }
            public string EmailCustomer { get; set; }
            public string DuplicateWindow { get; set; }
            public string EPayAccountNum { get; set; }
            public string Password { get; set; }
            public string TransactionCode { get; set; }
            public string DelimData { get; set; }
            public string DelimChar { get; set; }
            public string EncapChar { get; set; }
            public string Version { get; set; }
            public string RelayResponse { get; set; }
            public string TransactionURL { get; set; }
            public string TestMode { get; set; }
            public string MarketType { get; set; }
            public string DeviceType { get; set; }
            public string RequestType { get; set; }

            #endregion
        }

        public PaymentProviderType Type
        {
            get { return PaymentProviderType.EPayAccount; }
        }

        public void Initialize(XElement xmlSettings)
        {
            try
            {
                gatewaySettings = new GatewaySettings();

                if (xmlSettings.Attribute("transactionUrl") != null)
                    gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

                if (xmlSettings.Attribute("ePayAccountNum") != null)
                    gatewaySettings.EPayAccountNum = xmlSettings.Attribute("ePayAccountNum").Value;

                if (xmlSettings.Attribute("password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("password").Value;

                if (xmlSettings.Attribute("transactionCode") != null)
                    gatewaySettings.TransactionCode = xmlSettings.Attribute("transactionCode").Value;

                if (xmlSettings.Attribute("delimitedData") != null)
                    gatewaySettings.DelimData = xmlSettings.Attribute("delimitedData").Value;

                if (xmlSettings.Attribute("delimitedCharacter") != null)
                    gatewaySettings.DelimChar = xmlSettings.Attribute("delimitedCharacter").Value;

                if (xmlSettings.Attribute("version") != null)
                    gatewaySettings.Version = xmlSettings.Attribute("version").Value;

                if (xmlSettings.Attribute("transactionTest") != null)
                    gatewaySettings.TestMode = xmlSettings.Attribute("transactionTest").Value;

                if (xmlSettings.Attribute("requestType") != null)
                    gatewaySettings.RequestType = xmlSettings.Attribute("requestType").Value;

                if (xmlSettings.Attribute("deviceType") != null)
                    gatewaySettings.DeviceType = xmlSettings.Attribute("deviceType").Value;

                if (xmlSettings.Attribute("marketType") != null)
                    gatewaySettings.MarketType = xmlSettings.Attribute("marketType").Value;


                if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                {
                    throw new PaymentProviderException("TransactionURL cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.EPayAccountNum))
                {
                    throw new PaymentProviderException("Login cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.TransactionCode))
                {
                    throw new PaymentProviderException("TransactionKey cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.DelimData))
                {
                    gatewaySettings.DelimData = "TRUE";
                }

                if (string.IsNullOrEmpty(gatewaySettings.DelimChar))
                {
                    gatewaySettings.DelimData = "|";
                }

                if (string.IsNullOrEmpty(gatewaySettings.TestMode))
                {
                    gatewaySettings.DelimData = "FALSE";
                }


            }
            catch (Exception ex)
            {
                throw new PaymentProviderException("An error occured while reading the gateway settings", ex);
            }
        }
    }
}
