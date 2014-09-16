using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Collections;
using System.Web;


namespace CSPaymentProvider.Providers
{
    public class PayPalExpressCheckoutAccount : IPaymentProvider
    {
        #region Private Members

        private static GatewaySettings gatewaySettings = null;
        //private static string errRequiredNode = "A required gateway node does not exist: {0}";

        private class GatewaySettings
        {
            public string CurrencyCode { get; set; }
            public string DelimChar { get; set; }
            public string DelimData { get; set; }
            public string TransactionUrl { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string Signature { get; set; }
            public string Version { get; set; }
            public string ErrorLanguage { get; set; }
            public string PaymentAction { get; set; }
            public string RedirectUrl { get; set; }
        }
        #endregion

        #region Public Methods


        public Response PerformVoidSettledRequest(Request request)
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
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionUrl);
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
        public Response PerformVoidRequest(Request request)
        {

            Response response = new Response();
            return response;

        }
        public Response PerformValidationRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
        protected string BuildRequest(Request request)
        {
            string strDataItem = "{0}={1}";
            List<string> lRequest = new List<string>();
            string strUser = gatewaySettings.User;
            string strPassword = gatewaySettings.Password;
            string strSignature = gatewaySettings.Signature;
            string strMethod = gatewaySettings.Signature;
            string strReturnUrl = string.Empty;
            string strCancelUrl = string.Empty;
            string strToken = string.Empty;
            ArrayList alUnwantedInfo = new ArrayList();

            Hashtable htAdditionalInfo = new Hashtable();
            try
            {
                if (request.AdditionalInfo != null)
                {
                    htAdditionalInfo = request.AdditionalInfo;
                }
                if (htAdditionalInfo.ContainsKey("Method"))
                {
                    strMethod = htAdditionalInfo["Method"].ToString();
                    alUnwantedInfo.Add("Method");
                }
                if (htAdditionalInfo.ContainsKey("APIUser"))
                {
                    strUser = htAdditionalInfo["APIUser"].ToString();
                    alUnwantedInfo.Add("APIUser");
                }
                if (htAdditionalInfo.ContainsKey("APIPassword"))
                {
                    strPassword = htAdditionalInfo["APIPassword"].ToString();
                    alUnwantedInfo.Add("APIPassword");
                }
                if (htAdditionalInfo.ContainsKey("APISignature"))
                {
                    strSignature = htAdditionalInfo["APISignature"].ToString();
                    alUnwantedInfo.Add("APISignature");
                }
                
                if (htAdditionalInfo.ContainsKey("ReturnUrl"))
                {
                    strReturnUrl = htAdditionalInfo["ReturnUrl"].ToString();
                    alUnwantedInfo.Add("ReturnUrl");
                }
                if (htAdditionalInfo.ContainsKey("CancelUrl"))
                {
                    strCancelUrl = htAdditionalInfo["CancelUrl"].ToString();
                    alUnwantedInfo.Add("CancelUrl");
                }
                if (htAdditionalInfo.ContainsKey("Token"))
                {
                    strToken = htAdditionalInfo["Token"].ToString();
                    alUnwantedInfo.Add("Token");
                }


                lRequest.Add(string.Format(strDataItem, "USER", strUser));
                lRequest.Add(string.Format(strDataItem, "PWD", strPassword));
                lRequest.Add(string.Format(strDataItem, "SIGNATURE", strSignature));
                lRequest.Add(string.Format(strDataItem, "VERSION", gatewaySettings.Version));
                lRequest.Add(string.Format(strDataItem, "METHOD", strMethod));
                switch (strMethod.ToLower())
                {
                    case "setexpresscheckout":
                        {
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_AMT", request.Amount.ToString("N2")));
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_CURRENCYCODE", request.CurrencyCode));
                            lRequest.Add(string.Format(strDataItem, "RETURNURL", strReturnUrl));
                            lRequest.Add(string.Format(strDataItem, "CANCELURL", strCancelUrl));
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_PAYMENTACTION", gatewaySettings.PaymentAction));

                            foreach (string s in htAdditionalInfo.Keys)
                            {
                                if (!alUnwantedInfo.Contains(s))
                                    lRequest.Add(string.Format(strDataItem, s.ToUpper(), htAdditionalInfo[s]));
                            }
                            break;
                        }
                    case "getexpresscheckoutdetails":
                        {
                            lRequest.Add(string.Format(strDataItem, "TOKEN", strToken));
                            break;
                        }
                    case "doexpresscheckoutpayment":
                        {
                            lRequest.Add(string.Format(strDataItem, "TOKEN", strToken));
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_PAYMENTACTION", gatewaySettings.PaymentAction));
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_AMT", request.Amount.ToString("N2")));
                            lRequest.Add(string.Format(strDataItem, "PAYMENTREQUEST_0_CURRENCYCODE", request.CurrencyCode));
                            lRequest.Add(string.Format(strDataItem, "PAYERID", request.InvoiceNumber));

                            foreach (string s in htAdditionalInfo.Keys)
                            {
                                if (!alUnwantedInfo.Contains(s))
                                    lRequest.Add(string.Format(strDataItem, s.ToUpper(), htAdditionalInfo[s]));
                            }
                            break;
                        }
                    case "refundtransaction":
                        {
                            
                            foreach (string s in htAdditionalInfo.Keys)
                            {
                                if (!alUnwantedInfo.Contains(s))
                                    lRequest.Add(string.Format(strDataItem, s.ToUpper(), htAdditionalInfo[s]));
                            }
                            break;
                        }
                }

            }
            catch
            {
            }
            return string.Join("&", lRequest.ToArray());
        }

        private Response ParseResponse(string GatewayResponse, string request)
        {
            Response response = new Response();
            response.GatewayResponseRaw = GatewayResponse;
            response.GatewayRequestRaw = request;
            char[] charTxt = new char[1];
            charTxt[0] = System.Convert.ToChar(gatewaySettings.DelimChar);
            string[] aryGatewayResponse = GatewayResponse.Split(charTxt);
            Hashtable hGatewayResponse = new Hashtable();
            foreach (string element in aryGatewayResponse)
            {
                hGatewayResponse.Add(HttpUtility.UrlDecode(element.Split('=')[0].ToString()), HttpUtility.UrlDecode(element.Split('=')[1].ToString()));
            }
            response.AdditionalInfo = hGatewayResponse;
            if (aryGatewayResponse.Length > 0)
            {
                switch (hGatewayResponse["ACK"].ToString().ToLower())
                {
                    case "success":
                        //Approved
                        response.ResponseType = TransactionResponseType.Approved;
                        if (hGatewayResponse["PAYMENTINFO_0_TRANSACTIONID"] != null)
                            response.TransactionID = hGatewayResponse["PAYMENTINFO_0_TRANSACTIONID"].ToString();
                        else
                            response.TransactionID = hGatewayResponse["CORRELATIONID"].ToString();
                        response.AuthCode = hGatewayResponse["CORRELATIONID"].ToString();
                        if (hGatewayResponse["TOKEN"] != null)
                            response.MerchantDefined1 = gatewaySettings.RedirectUrl + hGatewayResponse["TOKEN"];
                        break;
                    case "failure":
                        //Declined
                        response.ResponseType = TransactionResponseType.Denied;
                        break;
                    default:
                        //Error
                        response.ResponseType = TransactionResponseType.Error;
                        break;
                }
            }
            else
            {
                response.ResponseType = TransactionResponseType.Error;
            }
            hGatewayResponse = null;
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
                if (xmlSettings.Attribute("TransactionUrl") != null)
                    gatewaySettings.TransactionUrl = xmlSettings.Attribute("TransactionUrl").Value;

                if (xmlSettings.Attribute("User") != null)
                    gatewaySettings.User = xmlSettings.Attribute("User").Value;

                if (xmlSettings.Attribute("Password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("Password").Value;

                if (xmlSettings.Attribute("Signature") != null)
                    gatewaySettings.Signature = xmlSettings.Attribute("Signature").Value;

                if (xmlSettings.Attribute("Version") != null)
                    gatewaySettings.Version = xmlSettings.Attribute("Version").Value;

                if (xmlSettings.Attribute("PaymentAction") != null)
                    gatewaySettings.PaymentAction = xmlSettings.Attribute("PaymentAction").Value;


                if (xmlSettings.Attribute("ErrorLanguage") != null)
                    gatewaySettings.ErrorLanguage = xmlSettings.Attribute("ErrorLanguage").Value;

                if (xmlSettings.Attribute("CurrencyCode") != null)
                    gatewaySettings.CurrencyCode = xmlSettings.Attribute("CurrencyCode").Value;

                if (xmlSettings.Attribute("RedirectUrl") != null)
                    gatewaySettings.RedirectUrl = xmlSettings.Attribute("RedirectUrl").Value;

                if (xmlSettings.Attribute("DelimChar") != null)
                    gatewaySettings.DelimChar = xmlSettings.Attribute("DelimChar").Value;

                if (string.IsNullOrEmpty(gatewaySettings.TransactionUrl))
                {
                    throw new PaymentProviderException("TransactionUrl cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.User))
                {
                    throw new PaymentProviderException("User cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.Password))
                {
                    throw new PaymentProviderException("Password cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.Signature))
                {
                    throw new PaymentProviderException("Signature cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.DelimData))
                {
                    gatewaySettings.DelimData = "TRUE";
                }

                if (string.IsNullOrEmpty(gatewaySettings.DelimChar))
                {
                    gatewaySettings.DelimChar = "|";
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
