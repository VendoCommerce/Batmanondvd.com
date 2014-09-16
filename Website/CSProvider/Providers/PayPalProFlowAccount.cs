using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;


namespace CSPaymentProvider.Providers
{
    public class PayPalProFlowAccount : IPaymentProvider
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
            public string Partner { get; set; }
            public string MerchantEmail { get; set; }
            public string RelayResponse { get; set; }
            public string TestMode { get; set; }
            public string Vendor{ get; set; }
            public string TransactionURL { get; set; }
            public string Version { get; set; }
            public string RequestType { get; set; }            
            public bool TransactionTest { get; set; }
            public string Tender { get; set; }
        }
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
                Exception ex;
                Response response = new Response();
                StreamWriter streamWriter = null;
                StreamReader streamReader = null;
                string strPost = string.Empty;

                strPost = this.BuildRequest(request);
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
                    response = this.ParseResponse(streamReader.ReadToEnd());
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
            public Response PerformVoidRequest(Request request)
            {

                Response response = new Response();
                return response;

            }
            protected string BuildRequest(Request request)
            {
                string NameValueFormat = "{0}={1}";

                List<string> aryPostParams = new List<string>();

                //Create parameters from objProvider settings
                aryPostParams.Add(string.Format(NameValueFormat, "user", gatewaySettings.User));
                aryPostParams.Add(string.Format(NameValueFormat, "pwd", gatewaySettings.Password));
                aryPostParams.Add(string.Format(NameValueFormat, "partner", gatewaySettings.Partner));
                aryPostParams.Add(string.Format(NameValueFormat, "vendor", gatewaySettings.Vendor));
                aryPostParams.Add(string.Format(NameValueFormat, "tender", gatewaySettings.Tender));
                aryPostParams.Add(string.Format(NameValueFormat, "trxtype", gatewaySettings.RequestType));

                //Create parameters from request

                try
                {
                    aryPostParams.Add(string.Format("amt={0:.00}", request.Amount));
                    aryPostParams.Add(string.Format(NameValueFormat, "comment1", request.TransactionDescription));
                    aryPostParams.Add(string.Format("expdate={0:####}", request.ExpireDate.ToString("MMyy")));
                    aryPostParams.Add(string.Format(NameValueFormat, "acct", request.CardNumber));
                    aryPostParams.Add(string.Format(NameValueFormat, "cvv2", request.CardCvv));
                    aryPostParams.Add(string.Format(NameValueFormat, "invnum", request.InvoiceNumber));

                    if (request.CustomerID.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "CustomerNum", request.CustomerID));
                    }
                    if (request.FirstName.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "firstname", request.FirstName));
                    }
                    if (request.FirstName.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "lastname", request.LastName));
                    }
                    if (request.Address1.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "street", request.Address1 + (string.IsNullOrEmpty(request.Address2) == false ? "+" + request.Address2 : string.Empty)));
                    }
                    if (request.ZipCode.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "zip", request.ZipCode));
                    }
                    if (request.City.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "city", request.City));
                    }
                    if (request.State.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "state", request.State));
                    }
                    if (request.Country.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "billtocountry", request.Country));
                    }
                }
                catch
                {
                }
                return string.Join("&", aryPostParams.ToArray());
            }

            private Response ParseResponse(string GatewayResponse)
            {
                Response response = new Response();
                response.GatewayResponseRaw = GatewayResponse;
                char[] charTxt = new char[1];
                charTxt[0] = System.Convert.ToChar(gatewaySettings.DelimChar);
                string[] aryGatewayResponse = GatewayResponse.Split(charTxt);
                if (aryGatewayResponse.Length > 0)
                {
                    switch (aryGatewayResponse[0].Split('=')[1])
                    {
                        case "0":
                        case "000":
                            //Approved
                            response.ResponseType = TransactionResponseType.Approved;
                            break;
                        case "12":
                            //Declined
                            response.ResponseType = TransactionResponseType.Denied;
                            break;
                        default:
                            //Error
                            response.ResponseType = TransactionResponseType.Error;
                            break;
                    }
                    response.ReasonText = aryGatewayResponse[2].Split('=')[1];
                    response.TransactionID = aryGatewayResponse[1].Split('=')[1];
                    response.AuthCode = aryGatewayResponse[3].Split('=')[1];
                }
                else
                {
                    response.ResponseType = TransactionResponseType.Error;                    
                }

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

                    if (xmlSettings.Attribute("partner") != null)
                        gatewaySettings.Partner = xmlSettings.Attribute("partner").Value;

                    if (xmlSettings.Attribute("vendor") != null)
                        gatewaySettings.Vendor = xmlSettings.Attribute("vendor").Value;

                    if (xmlSettings.Attribute("tender") != null)
                        gatewaySettings.Tender = xmlSettings.Attribute("tender").Value;

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

                    if (string.IsNullOrEmpty(gatewaySettings.Partner))
                    {
                        throw new PaymentProviderException("Partner cannot be null");
                    }
                    
                    if (string.IsNullOrEmpty(gatewaySettings.Vendor))
                    {
                        throw new PaymentProviderException("Vendor cannot be null");
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
