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
    public class Cielo : IPaymentProvider
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
                    //header
                    xml.WriteRaw(@"mensagem=<?xml version=""1.0"" encoding=""ISO-8859-1""?>");
                    //root node
                    xml.WriteStartElement("requisicao-transacao");
                    //
                    xml.WriteAttributeString("id", "a97ab62a-7956-41ea-b03f-c2e9f612c293");
                    xml.WriteAttributeString("versao", "1.3.0");
                    //
                    xml.WriteStartElement("dados-ec");
                    xml.WriteWhitespace("\n");

                    xml.WriteElementString("numero", gatewaySettings.MerchantId);
                    xml.WriteElementString("chave", gatewaySettings.TransactionKey);

                    xml.WriteEndElement();//End source

                    xml.WriteStartElement("dados-portador");//Start creditCard
                    xml.WriteWhitespace("\n");
                    xml.WriteElementString("numero", request.CardNumber);
                    xml.WriteElementString("validade", request.ExpireDate.ToString("yyyyMM"));
                    xml.WriteElementString("indicador", "0");
                    xml.WriteElementString("codigo-seguranca", request.CardCvv);
                    //xml.WriteElementString("token", "");
                    //xml.WriteElementString("taxa-embarque", "");
                    xml.WriteEndElement();//End creditCard


                    xml.WriteStartElement("dados-pedido");//Start billing info and item info
                    xml.WriteElementString("numero", request.InvoiceNumber);
                    xml.WriteElementString("valor", (request.Amount * 100).ToString());
                    xml.WriteElementString("moeda", "986");
                    xml.WriteElementString("data-hora", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                    xml.WriteElementString("descricao", string.Format("[origem:{0}]", request.IPAddress));
                    xml.WriteElementString("idioma", "PT");
                    //xml.WriteElementString("soft-descriptor", request.TransactionDescription);

                    //xml.WriteElementString("fullName", request.FirstName + " " + request.LastName);
                    //xml.WriteElementString("address1", request.Address1);
                    //xml.WriteElementString("address2", request.Address2);
                    //xml.WriteElementString("city", request.City);
                    //xml.WriteElementString("state", request.State);
                    //xml.WriteElementString("country", request.Country);
                    //xml.WriteElementString("postalCode", request.ZipCode);

                    xml.WriteEndElement();//End billingInfo

                    xml.WriteStartElement("forma-pagamento");//CC Type info
                    xml.WriteElementString("bandeira", request.BankAcctType.ToLower());// UpdateCreditCardType(request.CardType));
                    xml.WriteElementString("produto", "1");
                    xml.WriteElementString("parcelas", "1");
                    xml.WriteEndElement();//End CC Type info
                    xml.WriteElementString("url-retorno", "");
                    xml.WriteElementString("autorizar", "3");
                    xml.WriteElementString("capturar", "false");
                    xml.WriteElementString("bin", request.CardNumber.Substring(0, 6));
                    xml.WriteElementString("gerar-token", "false");
                    xml.WriteStartElement("avs");
                    //Build Address String
                    string avsAddress = string.Format("<dados-avs> <endereco>{0}</endereco> <complemento>{1}</complemento> <numero>{2}</numero> <bairro>{3}</bairro> <cep>{4}</cep> </dados-avs>",
                        request.Address1, request.Address2, "", "", request.Phone);
                    xml.WriteCData(avsAddress);
                    xml.WriteEndElement();//End avs 

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

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(GatewayResponse);
                //if (response.GatewayResponseRaw.Contains("<erro"))
                XmlNamespaceManager resolver = new XmlNamespaceManager(doc.NameTable);
                resolver.AddNamespace("c", "http://ecommerce.cbmp.com.br");
                string status = doc.SelectSingleNode("c:transacao/c:autorizacao/c:codigo", resolver).InnerText;

                switch (status)
                {
                    case "1":
                    case "4":
                        response.ResponseType = TransactionResponseType.Approved;

                        response.TransactionID = doc.SelectSingleNode("c:transacao/c:tid", resolver).InnerText;
                        response.AuthCode = doc.SelectSingleNode("c:transacao/c:pan", resolver).InnerText;

                        break;
                    case "5":
                        response.ResponseType = TransactionResponseType.Denied;

                        response.TransactionID = doc.SelectSingleNode("c:transacao/c:tid", resolver).InnerText;
                        response.AuthCode = doc.SelectSingleNode("c:transacao/c:pan", resolver).InnerText;

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
            if (value == CreditCardType.Visa) { returnValue = "visa"; }
            if (value == CreditCardType.AmericanExpress) { returnValue = "amex"; }
            if (value == CreditCardType.Discover) { returnValue = "discover"; }
            if (value == CreditCardType.Mastercard) { returnValue = "mastercard"; }

            return returnValue;
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
