using System;
using System.Collections;
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
    public class PaymentX : IPaymentProvider
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
            public string merchantKey { get; set; }
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

                if (xmlSettings.Attribute("merchantKey") != null)
                    gatewaySettings.merchantKey = xmlSettings.Attribute("merchantKey").Value;

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

           

            //Sample credit card charge
            Hashtable prms = new Hashtable();
            prms.Add("TransactionType", gatewaySettings.RequestType);
            prms.Add("MerchantID", gatewaySettings.MerchantId);
            prms.Add("MerchantKey", gatewaySettings.merchantKey);
            prms.Add("CardNumber", request.CardNumber);
            prms.Add("CVV2",request.CardCvv);
            prms.Add("ExpirationDateMMYY", request.ExpireDate.ToString("MMyy"));
            prms.Add("TransactionAmount", request.Amount.ToString());
            prms.Add("BillingNameFirst", request.FirstName);
            prms.Add("BillingNameLast", request.LastName);
            prms.Add("BillingCountry", request.Country);
            
            if(request.InvoiceNumber != null)
                prms.Add("ReferenceNumber", request.InvoiceNumber);
            
            if(request.IPAddress != null)
                prms.Add("ClientIPAddress", request.IPAddress);
            
            prms.Add("BillingFullName", request.FirstName + " " + request.LastName);
            if (request.Address2 != null && request.Address2.Length > 1)
            {
                prms.Add("BillingAddress", request.Address1 + " " + request.Address2);
            }
            else
            {
                prms.Add("BillingAddress", request.Address1);
            }

            prms.Add("BillingZipCode", request.ZipCode);
            prms.Add("BillingCity", request.City);
            prms.Add("BillingState", request.State);

            String postdata = string.Empty;
            foreach (DictionaryEntry prm in prms)
            {
                postdata += prm.Key + "=" + prm.Value + "&";
            }
            postdata = postdata.TrimEnd('&');
            strXml = postdata.TrimEnd('&');

            return strXml;
        }

        private Response ParseResponse(string GatewayResponse, string request)
        {
            Response response = new Response();

            response.GatewayRequestRaw = request;
            response.GatewayResponseRaw = GatewayResponse;

            try
            {
                
                char[] charTxt = new char[1];
                charTxt[0] = System.Convert.ToChar('&');
                char[] charTxt1 = new char[1];
                charTxt1[0] = System.Convert.ToChar('=');
                string[] aryGatewayResponse = GatewayResponse.Split(charTxt);
                
                Hashtable resp = new Hashtable();


                for (int i = 0; i < aryGatewayResponse.Length; i++)
                {
                    try
                    {
                        resp.Add(aryGatewayResponse[i].Split(charTxt1)[0], aryGatewayResponse[i].Split(charTxt1)[1]); 
                    }
                    catch 
                    {
                        
                        
                    }
                    
                }

                if (resp.ContainsKey("StatusID"))
                    {
                        if (resp["StatusID"].ToString().Equals("0"))
                        {
                            response.ResponseType = TransactionResponseType.Approved;
                        }
                        else
                        {
                            response.ResponseType = TransactionResponseType.Denied;
                        }
                        response.AuthCode = resp["AuthorizationCode"].ToString();
                        response.TransactionID = resp["TransactionID"].ToString();
                        response.ReasonText = "(" + aryGatewayResponse + ")";
                        return response;
                    }
                    else
                    {
                        response.ResponseType = TransactionResponseType.Error;
                    }
                response.ResponseType = TransactionResponseType.Approved;
                response.ReasonText = System.String.Concat("Unknown Error (", aryGatewayResponse, ")");

                return response;
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
