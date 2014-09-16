using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;
using System.Xml.Serialization;
using System.Text;
using CSCore.Logging;


namespace CSPaymentProvider.Providers
{
    public class LitleCorpAccountAccount : IPaymentProvider
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
            public string ReportGroup { get; set; }
            public string MerchantEmail { get; set; }
            public string RelayResponse { get; set; }
            public string TestMode { get; set; }
            public string MerchantId { get; set; }
            public string TransactionURL { get; set; }
            public string Version { get; set; }
            public string RequestType { get; set; }
            public string OrderSource { get; set; }
            public bool TransactionTest { get; set; }

        }
        #endregion

        #region Public Methods

        public Response PerformVoidSettledRequest(Request request)
        {
            Exception ex;
            Response response = new Response();
            StreamWriter streamWriter = null;
            StreamReader streamReader = null;
            string requestXML = string.Empty;

            requestXML = this.BuildVoidSettledRequest(request);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);
            objRequest.Method = "POST";
            objRequest.ContentLength = requestXML.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                streamWriter = new StreamWriter(objRequest.GetRequestStream());
                streamWriter.Write(requestXML);
            }
            catch (Exception exception1)
            {
                ex = exception1;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
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
                response = this.ParseVoidSettledResponse(streamReader.ReadToEnd(), requestXML);
            }
            catch (Exception exception2)
            {
                ex = exception2;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
                //throw new GatewayException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {
                streamReader.Close();
            }
            return response;
        }
        public Response PerformValidationRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
        public Response PerformRequest(Request request)
        {
            Exception ex;
            Response response = new Response();
            StreamWriter streamWriter = null;
            StreamReader streamReader = null;
            string requestXML = string.Empty;

            requestXML = this.BuildRequest(request);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);
            objRequest.Method = "POST";
            objRequest.ContentLength = requestXML.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                streamWriter = new StreamWriter(objRequest.GetRequestStream());
                streamWriter.Write(requestXML);
            }
            catch (Exception exception1)
            {
                ex = exception1;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
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
                response = this.ParseResponse(streamReader.ReadToEnd(), requestXML);
            }
            catch (Exception exception2)
            {
                ex = exception2;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
                //throw new GatewayException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {
                streamReader.Close();
            }
            return response;

        }
        public Response PerformVoidRequest(Request request)
        {

            Exception ex;
            Response response = new Response();
            StreamWriter streamWriter = null;
            StreamReader streamReader = null;
            string requestXML = string.Empty;

            requestXML = this.BuildVoidRequest(request);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);
            objRequest.Method = "POST";
            objRequest.ContentLength = requestXML.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                streamWriter = new StreamWriter(objRequest.GetRequestStream());
                streamWriter.Write(requestXML);
            }
            catch (Exception exception1)
            {
                ex = exception1;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
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
                response = this.ParseVoidResponse(streamReader.ReadToEnd(), requestXML);
            }
            catch (Exception exception2)
            {
                ex = exception2;
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
                //throw new GatewayException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {
                streamReader.Close();
            }
            return response;

        }
        protected string BuildRequest(Request request)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    xml.WriteStartElement("litleOnlineRequest");
                    xml.WriteAttributeString("version", gatewaySettings.Version);
                    xml.WriteAttributeString("xmlns", "http://www.litle.com/schema");
                    xml.WriteAttributeString("merchantId", gatewaySettings.MerchantId);

                    xml.WriteStartElement("authentication");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("user", gatewaySettings.User);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("password", gatewaySettings.Password);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    string xmlcontent;

                    if (request.RequestType.ToString().ToLower().Equals("sale"))
                    {
                        xmlcontent = "sale";
                    }
                    else if (request.RequestType.ToString().ToLower().Equals("auth"))
                    {
                        xmlcontent = "authorization";
                    }
                    else
                    {
                        xmlcontent = "authorization";
                    }
                    xml.WriteStartElement(xmlcontent);
                    xml.WriteAttributeString("id", request.InvoiceNumber);
                    xml.WriteAttributeString("reportGroup", gatewaySettings.ReportGroup);
                    xml.WriteAttributeString("customerId", request.CustomerID);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("orderId", request.InvoiceNumber);
                    xml.WriteWhitespace("\n");
                    if (request.Amount > 0)
                    {
                        xml.WriteElementString("amount", (request.Amount * 100).ToString()); // Amount Should never be sent in decimal value to Litle.
                        xml.WriteWhitespace("\n");
                    }
                    else
                    {
                        if (request.CardType.Equals(CreditCardType.AmericanExpress))
                        {
                            xml.WriteElementString("amount", "100"); // Amount Should never be sent in decimal value to Litle.
                            xml.WriteWhitespace("\n");
                        }
                        else
                        {
                            xml.WriteElementString("amount", (request.Amount * 100).ToString()); // Amount Should never be sent in decimal value to Litle.
                            xml.WriteWhitespace("\n");
                        }
                    }

                    xml.WriteElementString("orderSource", gatewaySettings.OrderSource);

                    xml.WriteStartElement("billToAddress");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("name", request.FirstName + " " + request.LastName);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("addressLine1", request.Address1);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("city", request.City);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("state", request.State);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("zip", request.ZipCode);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("email", request.Email);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("phone", request.Phone);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();

                    xml.WriteStartElement("card");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("type", UpdateCreditCardType(request.CardType));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("number", request.CardNumber);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("expDate", request.ExpireDate.ToString("MMyy"));
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("cardValidationNum", request.CardCvv);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End Authorization/Sale

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End litleOnlineRequest 

                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;

        }

        protected string BuildVoidRequest(Request request)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    xml.WriteStartElement("litleOnlineRequest");
                    xml.WriteAttributeString("version", gatewaySettings.Version);
                    xml.WriteAttributeString("xmlns", "http://www.litle.com/schema");
                    xml.WriteAttributeString("merchantId", gatewaySettings.MerchantId);

                    xml.WriteStartElement("authentication");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("user", gatewaySettings.User);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("password", gatewaySettings.Password);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("void");
                    xml.WriteAttributeString("id", request.InvoiceNumber);
                    xml.WriteAttributeString("reportGroup", gatewaySettings.ReportGroup);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("litleTxnId", request.TransactionID);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End Void

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End litleOnlineRequest 

                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;

        }


        protected string BuildVoidSettledRequest(Request request)
        {
            String strXml = String.Empty;
            using (StringWriter str = new StringWriter())
            {

                using (XmlTextWriter xml = new XmlTextWriter(str))
                {
                    //root node
                    xml.WriteStartElement("litleOnlineRequest");
                    xml.WriteAttributeString("version", gatewaySettings.Version);
                    xml.WriteAttributeString("xmlns", "http://www.litle.com/schema");
                    xml.WriteAttributeString("merchantId", gatewaySettings.MerchantId);

                    xml.WriteStartElement("authentication");
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("user", gatewaySettings.User);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("password", gatewaySettings.Password);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();
                    xml.WriteWhitespace("\n");

                    xml.WriteStartElement("credit");
                    xml.WriteAttributeString("id", request.InvoiceNumber);
                    xml.WriteAttributeString("reportGroup", gatewaySettings.ReportGroup);
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("litleTxnId", request.TransactionID);
                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End credit

                    xml.WriteWhitespace("\n");
                    xml.WriteEndElement();//End litleOnlineRequest 

                    //flush results to string object
                    strXml = str.ToString();
                }
            }
            return strXml;

        }

        private Response ParseResponse(string GatewayResponse, string request)
        {

            Response response = new Response();

            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineResponse));
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(GatewayResponse));
            litleOnlineResponse resultingMessage = (litleOnlineResponse)serializer.Deserialize(memStream);


            string avsresp = resultingMessage.authorizationResponse[0].fraudResult[0].avsResult;
            string cardvalid = resultingMessage.authorizationResponse[0].fraudResult[0].cardValidationResult;
            string respmessage = resultingMessage.authorizationResponse[0].message;

            if (resultingMessage.response.Equals("0") && respmessage.ToLower().Equals("approved") && cardvalid.ToLower().Equals("m"))
            {
                switch (avsresp)
                {
                    case "00":
                    case "01":
                    case "02":
                    case "10":
                    case "11":
                    case "14":
                        response.AvsResponse = TransactionAvsResponse.Match;
                        break;
                    default:
                        response.AvsResponse = TransactionAvsResponse.NoMatch;
                        break;
                }
                string avsresponse = response.AvsResponse.ToString();
                if (avsresponse.ToLower().Equals("match"))
                {
                    response.ResponseType = TransactionResponseType.Approved;
                }
                else
                {
                    response.ResponseType = TransactionResponseType.Denied;
                }

            }
            else
            {
                response.ResponseType = TransactionResponseType.Denied;
            }

            response.TransactionID = resultingMessage.authorizationResponse[0].litleTxnId;
            response.AuthCode = resultingMessage.authorizationResponse[0].authCode;
            response.GatewayResponseRaw = GatewayResponse;
            response.GatewayRequestRaw = request;

            return response;

        }

        private Response ParseVoidResponse(string GatewayResponse, string request)
        {

            Response response = new Response();

            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineResponse));
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(GatewayResponse));
            litleOnlineResponse resultingMessage = (litleOnlineResponse)serializer.Deserialize(memStream);
            
            string respmessage = resultingMessage.voidResponse[0].message;

            if (resultingMessage.response.Equals("0") && respmessage.ToLower().Equals("approved"))
            {
                
                    response.ResponseType = TransactionResponseType.Approved;
            }
            else
            {
                response.ResponseType = TransactionResponseType.Denied;
            }

            response.TransactionID = resultingMessage.voidResponse[0].litleTxnId;
            //response.AuthCode = resultingMessage.authorizationResponse[0].authCode;
            response.ReasonText = resultingMessage.response;
            response.GatewayResponseRaw = GatewayResponse;
            response.GatewayRequestRaw = request;

            return response;

        }

        private Response ParseVoidSettledResponse(string GatewayResponse, string request)
        {

            Response response = new Response();

            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineResponse));
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(GatewayResponse));
            litleOnlineResponse resultingMessage = (litleOnlineResponse)serializer.Deserialize(memStream);

            string respmessage = resultingMessage.creditResponse[0].message;

            if (resultingMessage.response.Equals("0") && respmessage.ToLower().Equals("approved"))
            {

                response.ResponseType = TransactionResponseType.Approved;
            }
            else
            {
                response.ResponseType = TransactionResponseType.Denied;
            }

            response.TransactionID = resultingMessage.voidResponse[0].litleTxnId;
            //response.AuthCode = resultingMessage.authorizationResponse[0].authCode;
            response.GatewayResponseRaw = GatewayResponse;
            response.GatewayRequestRaw = request;

            return response;

        }

        public PaymentProviderType Type
        {
            get { return PaymentProviderType.AuthorizeNetAccount; }
        }
        public void Initialize(System.Xml.Linq.XElement xmlSettings)
        {
            try
            {
                gatewaySettings = new GatewaySettings();

                //get transaction url attribute
                if (xmlSettings.Attribute("transactionUrl") != null)
                    gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

                if (xmlSettings.Attribute("user") != null)
                    gatewaySettings.User = xmlSettings.Attribute("user").Value;

                if (xmlSettings.Attribute("password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("password").Value;

                if (xmlSettings.Attribute("merchantId") != null)
                    gatewaySettings.MerchantId = xmlSettings.Attribute("merchantId").Value;

                if (xmlSettings.Attribute("reportGroup") != null)
                    gatewaySettings.ReportGroup = xmlSettings.Attribute("reportGroup").Value;

                if (xmlSettings.Attribute("orderSource") != null)
                    gatewaySettings.OrderSource = xmlSettings.Attribute("orderSource").Value;

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

                if (xmlSettings.Attribute("requestType") != null)
                    gatewaySettings.RequestType = xmlSettings.Attribute("requestType").Value;

                if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                {
                    throw new PaymentProviderException("TransactionURL cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.User))
                {
                    throw new PaymentProviderException("User cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.Password))
                {
                    throw new PaymentProviderException("Password cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.MerchantId))
                {
                    throw new PaymentProviderException("MerchantId cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.ReportGroup))
                {
                    throw new PaymentProviderException("ReportGroup cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.OrderSource))
                {
                    throw new PaymentProviderException("OrderSource cannot be null");
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
                CSCore.CSLogger.Instance.LogException(ex.Message, ex.InnerException);
                throw new PaymentProviderException("An error occured while reading the gateway settings", ex);
            }
        }
        public static string UpdateCreditCardType(CreditCardType value)
        {
            // VI VISA AX AMERICAN EXPRESS DI DISCOVER MC MASTER CARD 

            //AMEX
            //Discover
            //MasterCard
            //VISA
            string returnValue = "";
            if (value == CreditCardType.Visa) { returnValue = "VI"; }
            if (value == CreditCardType.AmericanExpress) { returnValue = "AX"; }
            if (value == CreditCardType.Discover) { returnValue = "DI"; }
            if (value == CreditCardType.Mastercard) { returnValue = "MC"; }

            return returnValue;
        }

        #endregion
    }
}
