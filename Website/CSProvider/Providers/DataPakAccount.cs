using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using CSPaymentProvider;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CSPaymentProvider.Providers
{
    public class DataPakAccount : IPaymentProvider
    {
        #region Private Members

        private static GatewaySettings gatewaySettings = null;
        //private static string errRequiredNode = "A required gateway node does not exist: {0}";

        private class GatewaySettings
        {
            public string CurrencyCode { get; set; }
            public string DelimChar { get; set; }
            public string DelimData { get; set; }
            public string DeviceType { get; set; }
            public string DuplicateWindow { get; set; }
            public string EmailCustomer { get; set; }
            public string EncapChar { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string MarketType { get; set; }
            public string MerchantEmail { get; set; }
            public string RelayResponse { get; set; }
            public string TestMode { get; set; }
            public string MerchantId { get; set; }
            public string TransactionKey { get; set; }
            public string TransactionURL { get; set; }
            public string Version { get; set; }
            public string RequestType { get; set; }
            public string Method { get; set; }
            public bool TransactionTest { get; set; }

        }
        #endregion

        #region Public Methods

        public PaymentProviderType Type
        {
            get { return PaymentProviderType.DataPakAccount; }
        }

        public void Initialize(System.Xml.Linq.XElement xmlSettings)
        {
            try
            {
                gatewaySettings = new GatewaySettings();

                //get transaction url attribute
                if (xmlSettings.Attribute("transactionUrl") != null)
                    gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

                if (xmlSettings.Attribute("transactionKey") != null)
                    gatewaySettings.TransactionKey = xmlSettings.Attribute("transactionKey").Value;

                if (xmlSettings.Attribute("delimitedData") != null)
                    gatewaySettings.DelimData = xmlSettings.Attribute("delimitedData").Value;

                if (xmlSettings.Attribute("DelimChar") != null)
                    gatewaySettings.DelimChar = xmlSettings.Attribute("DelimChar").Value;

                if (xmlSettings.Attribute("version") != null)
                    gatewaySettings.Version = xmlSettings.Attribute("version").Value;

                if (xmlSettings.Attribute("transactionTest") != null)
                    gatewaySettings.TestMode = xmlSettings.Attribute("transactionTest").Value;

                if (xmlSettings.Attribute("deviceType") != null)
                    gatewaySettings.DeviceType = xmlSettings.Attribute("deviceType").Value;

                if (xmlSettings.Attribute("marketType") != null)
                    gatewaySettings.MarketType = xmlSettings.Attribute("marketType").Value;

                if (xmlSettings.Attribute("requestType") != null)
                    gatewaySettings.RequestType = xmlSettings.Attribute("requestType").Value;

                if (xmlSettings.Attribute("merchantId") != null)
                    gatewaySettings.MerchantId = xmlSettings.Attribute("merchantId").Value;

                if (xmlSettings.Attribute("user") != null)
                    gatewaySettings.User = xmlSettings.Attribute("user").Value;

                if (xmlSettings.Attribute("password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("password").Value;
                
                if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                {
                    throw new PaymentProviderException("TransactionURL cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.MerchantId))
                {
                    throw new PaymentProviderException("MerchantId cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.User))
                {
                    throw new PaymentProviderException("User cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.Password))
                {
                    throw new PaymentProviderException("Password cannot be null");
                }
                
                if (string.IsNullOrEmpty(gatewaySettings.DelimData))
                {
                    gatewaySettings.DelimData = "TRUE";
                }

                if (string.IsNullOrEmpty(gatewaySettings.DelimChar))
                {
                    gatewaySettings.DelimChar = "|";
                }

                if (string.IsNullOrEmpty(gatewaySettings.TestMode))
                {
                    gatewaySettings.TestMode = "FALSE";
                }
            }
            catch (Exception ex)
            {
                throw new PaymentProviderException("An error occured while reading the gateway settings", ex);
            }
        }

        public Response PerformRequest(Request request)
        {
            Exception ex;
            Response response = new Response();
            StreamWriter streamWriter = null;
            StreamReader streamReader = null;
            string strPost = string.Empty;

            strPost = this.BuildRequestPost(request);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                streamWriter = new StreamWriter(objRequest.GetRequestStream());
                streamWriter.Write(strPost);
            }
            catch (Exception exception1)
            {
                ex = exception1;
                //throw new GatewayException("An exception occured while getting the request stream for the gateway", ex);
            }
            finally
            {
                streamWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            try
            {
                streamReader = new StreamReader(objResponse.GetResponseStream());
                response = this.ParseResponse(streamReader.ReadToEnd(), strPost);
            }
            catch (Exception exception2)
            {
                ex = exception2;
                //throw new GatewayException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {
                streamReader.Close();
            }
            return response;

        }

        protected string BuildRequestPost(Request request)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {
                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    xml.WriteStartElement("forwardIt");
                        xml.WriteStartElement("source");
                        xml.WriteWhitespace("\n");

                            xml.WriteElementString("merchantId", gatewaySettings.MerchantId);
                            xml.WriteElementString("username", gatewaySettings.User);
                            xml.WriteElementString("password", gatewaySettings.Password);
                                        
                        xml.WriteEndElement();//End source

                        xml.WriteStartElement("ccAuthorization");
                        xml.WriteWhitespace("\n");

                            xml.WriteElementString("referenceNumber", request.InvoiceNumber);
                            xml.WriteElementString("amount", request.Amount.ToString());

                            xml.WriteStartElement("billingInfo");
                            xml.WriteWhitespace("\n");

                                xml.WriteElementString("fullName", request.FirstName + " " + request.LastName);
                                xml.WriteElementString("address1", request.Address1);
                                xml.WriteElementString("address2", request.Address2);
                                xml.WriteElementString("city", request.City);
                                xml.WriteElementString("state", request.State);
                                xml.WriteElementString("country", request.Country);
                                xml.WriteElementString("postalCode", request.ZipCode);

                            xml.WriteEndElement();//End billingInfo

                            xml.WriteStartElement("creditCard");
                            xml.WriteWhitespace("\n");

                                xml.WriteElementString("number", request.CardNumber);
                                xml.WriteElementString("type", UpdateCreditCardType(request.CardType));
                                xml.WriteElementString("expirationMonth", request.ExpireDate.ToString("MM"));
                                xml.WriteElementString("expirationYear", request.ExpireDate.ToString("yyyy"));
                                xml.WriteElementString("cvn", request.CardCvv);

                            xml.WriteEndElement();//End creditCard

                        xml.WriteEndElement();//End ccAuthorization

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End forwardIt 

                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;
        }

        private Response ParseResponse(string GatewayResponse, string request)
        {
            Response response = new Response();

            response.GatewayRequestRaw = request;
            response.GatewayResponseRaw = GatewayResponse;

            try
            {
                XDocument doc = XDocument.Parse(GatewayResponse);
                
                string status = doc.XPathSelectElement("//Response").Value;

                switch (status)
                {
                    case "900":
                        response.ResponseType = TransactionResponseType.Approved;

                        response.TransactionID = doc.XPathSelectElement("//transactionId").Value;
                        response.AuthCode = doc.XPathSelectElement("//authCode").Value;

                        break;
                    case "950":
                        response.ResponseType = TransactionResponseType.Denied;

                        break;
                    default:
                        response.ResponseType = TransactionResponseType.Error;

                        break;
                }                
            }
            catch
            {
                response.ResponseType = TransactionResponseType.Error;                
            }

            return response;
        }

        public static string UpdateCreditCardType(CreditCardType value)
        {
            string returnValue = "";
            if (value == CreditCardType.Visa) { returnValue = "VI"; }
            if (value == CreditCardType.AmericanExpress) { returnValue = "AM"; }
            if (value == CreditCardType.Discover) { returnValue = "DI"; }
            if (value == CreditCardType.Mastercard) { returnValue = "MA"; }

            return returnValue.ToUpper();
        }

        public Response PerformVoidRequest(Request request)
        {

            Response response = new Response();
            return response;

        }

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

        #endregion
    }
}
