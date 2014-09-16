using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;
using System.Text;
using System.Web;
using System.Collections;


namespace CSPaymentProvider.Providers
{
    public class PayPalAdaptivePaymentAccount : IPaymentProvider
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
            public string EmailCustomer { get; set; }
            public string EncapChar { get; set; }
            public string ApiEndPoint { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string Signature { get; set; }
            public string Version { get; set; }
            public string ErrorLanguage { get; set; }
            public string DetailLevel { get; set; }
            public string RequestDataBinding { get; set; }
            public string ResponseDataBinding { get; set; }
            public string AppID { get; set; }
            public string PartnerName { get; set; }
            public string ActionType { get; set; }
            public string FeesPayer { get; set; }
            public string RedirectUrl { get; set; }
            public string ReceiverEmail { get; set; }
            public string ApiEndPointUri { get; set; }
            
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
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.ApiEndPoint + "/" + gatewaySettings.ApiEndPointUri);
                objRequest.Headers.Add("X-PAYPAL-SECURITY-USERID", gatewaySettings.User);
                objRequest.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", gatewaySettings.Password);
                objRequest.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", gatewaySettings.Signature);
                objRequest.Headers.Add("X-PAYPAL-SERVICE-VERSION", gatewaySettings.Version);
                objRequest.Headers.Add("X-PAYPAL-APPLICATION-ID", gatewaySettings.AppID);
                objRequest.Headers.Add("X-PAYPAL-REQUEST-DATA-FORMAT", gatewaySettings.RequestDataBinding);
                objRequest.Headers.Add("X-PAYPAL-RESPONSE-DATA-FORMAT", gatewaySettings.ResponseDataBinding);
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
            public Response PerformValidationRequest(Request request)
            {
                Response response = new Response();
                return response;
            }
            protected string BuildRequest(Request request)
            {
                StringBuilder sRequest = new StringBuilder();
                Hashtable htAdditionalInfo = new Hashtable();
                string strReturnUrl = string.Empty;
                string strCancelUrl = string.Empty;
                string strMemo = string.Empty;
                string strFeesPayer = string.Empty;
                string strSenderEmail = string.Empty;
                string strReceiverEmail = gatewaySettings.ReceiverEmail;
                string strReceiverEmailPrimary = "true";
                string strReceiverAmount = request.Amount.ToString();
                string strReceiverEmail2 = string.Empty;
                string strReceiverEmailPrimary2 = "false";
                string strReceiverAmount2 = "0";
                string strReceiver2AppendToRequest = "false";
                string strReceiverEmail3 = string.Empty;
                string strReceiverEmailPrimary3 = "false";
                string strReceiverAmount3 = "0";
                string strReceiver3AppendToRequest = "false";
                string strReceiverEmail4 = string.Empty;
                string strReceiverEmailPrimary4 = "false";
                string strReceiverAmount4 = "0";
                string strReceiver4AppendToRequest = "false";
                string strTrackingID = System.Guid.NewGuid().ToString();
                string strActionType = gatewaySettings.ActionType;
                string strPayKey = "";
                string strRequireShippingAddressSelection = "true";
                string strIpnNotificationUrl = "true";
                gatewaySettings.ApiEndPointUri = gatewaySettings.ActionType;
                try
                {
                    if (request.AdditionalInfo != null)
                    {
                        htAdditionalInfo = request.AdditionalInfo;
                    }

                    if (htAdditionalInfo.ContainsKey("ActionType"))
                    {
                        strActionType = htAdditionalInfo["ActionType"].ToString();
                        gatewaySettings.ApiEndPointUri = strActionType;
                    }
                    if (htAdditionalInfo.ContainsKey("IpnNotificationUrl"))
                    {
                        strIpnNotificationUrl = htAdditionalInfo["IpnNotificationUrl"].ToString();                        
                    }

                    if (htAdditionalInfo.ContainsKey("PayKey"))
                    {
                        strPayKey = htAdditionalInfo["PayKey"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("RequireShippingAddressSelection"))
                    {
                        strRequireShippingAddressSelection = htAdditionalInfo["RequireShippingAddressSelection"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReturnUrl"))
                    {
                        strReturnUrl = htAdditionalInfo["ReturnUrl"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("CancelUrl"))
                    {
                        strCancelUrl = htAdditionalInfo["CancelUrl"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("Memo"))
                    {
                        strMemo = htAdditionalInfo["Memo"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("FeesPayer"))
                    {
                        strFeesPayer = htAdditionalInfo["FeesPayer"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("SenderEmail"))
                    {
                        strSenderEmail = htAdditionalInfo["SenderEmail"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmail"))
                    {
                        strReceiverEmail = htAdditionalInfo["ReceiverEmail"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmailPrimary"))
                    {
                        strReceiverEmailPrimary = htAdditionalInfo["ReceiverEmailPrimary"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverAmount"))
                    {
                        strReceiverAmount = htAdditionalInfo["ReceiverAmount"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmail2"))
                    {
                        strReceiverEmail2 = htAdditionalInfo["ReceiverEmail2"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmailPrimary2"))
                    {
                        strReceiverEmailPrimary2 = htAdditionalInfo["ReceiverEmailPrimary2"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverAmount2"))
                    {
                        strReceiverAmount2 = htAdditionalInfo["ReceiverAmount2"].ToString();
                    }
                    
                    if (htAdditionalInfo.ContainsKey("Receiver2AppendToRequest"))
                    {
                        strReceiver2AppendToRequest = htAdditionalInfo["Receiver2AppendToRequest"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmail3"))
                    {
                        strReceiverEmail3 = htAdditionalInfo["ReceiverEmail3"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmailPrimary3"))
                    {
                        strReceiverEmailPrimary3 = htAdditionalInfo["ReceiverEmailPrimary3"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverAmount3"))
                    {
                        strReceiverAmount3 = htAdditionalInfo["ReceiverAmount3"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("Receiver3AppendToRequest"))
                    {
                        strReceiver3AppendToRequest = htAdditionalInfo["Receiver3AppendToRequest"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmail4"))
                    {
                        strReceiverEmail4 = htAdditionalInfo["ReceiverEmail4"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverEmailPrimary4"))
                    {
                        strReceiverEmailPrimary4 = htAdditionalInfo["ReceiverEmailPrimary4"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("ReceiverAmount4"))
                    {
                        strReceiverAmount4 = htAdditionalInfo["ReceiverAmount4"].ToString();
                    }

                    if (htAdditionalInfo.ContainsKey("Receiver4AppendToRequest"))
                    {
                        strReceiver4AppendToRequest = htAdditionalInfo["Receiver4AppendToRequest"].ToString();
                    }

                    switch (strActionType.ToLower())
                    {
                        case "paymentdetails":
                            {
                                sRequest.Append("requestEnvelope.errorLanguage=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.ErrorLanguage));
                                sRequest.Append("&requestEnvelope.detailLevel=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.DetailLevel));
                                // clientDetails fields
                                sRequest.Append("&payKey=");
                                sRequest.Append(HttpUtility.UrlEncode(strPayKey));
                                break;
                            }
                        case "getshippingaddresses":
                            {
                                sRequest.Append("requestEnvelope.errorLanguage=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.ErrorLanguage));
                                sRequest.Append("&requestEnvelope.detailLevel=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.DetailLevel));
                                // clientDetails fields
                                sRequest.Append("&key=");
                                sRequest.Append(HttpUtility.UrlEncode(strPayKey));                                
                                break;
                            }
                        case "setpaymentoptions":
                            {
                                sRequest.Append("requestEnvelope.errorLanguage=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.ErrorLanguage));
                                sRequest.Append("&requestEnvelope.detailLevel=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.DetailLevel));
                                // clientDetails fields
                                sRequest.Append("&payKey=");
                                sRequest.Append(HttpUtility.UrlEncode(strPayKey));
                                //sRequest.Append("&clientDetails.deviceId=");
                                //sRequest.Append(HttpUtility.UrlEncode(sDeviceID));
                                sRequest.Append("&senderOptions.requireShippingAddressSelection=");
                                sRequest.Append(HttpUtility.UrlEncode(strRequireShippingAddressSelection));                                
                                break;
                            }
                        default:
                            {
                                // requestEnvelope fields
                                sRequest.Append("requestEnvelope.errorLanguage=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.ErrorLanguage));
                                sRequest.Append("&requestEnvelope.detailLevel=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.DetailLevel));
                                // clientDetails fields
                                sRequest.Append("&clientDetails.applicationId=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.AppID));
                                //sRequest.Append("&clientDetails.deviceId=");
                                //sRequest.Append(HttpUtility.UrlEncode(sDeviceID));
                                sRequest.Append("&clientDetails.ipAddress=");
                                sRequest.Append(HttpUtility.UrlEncode(request.IPAddress));
                                sRequest.Append("&clientDetails.partnerName=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.PartnerName));
                                sRequest.Append("&ipnNotificationUrl=");
                                sRequest.Append(HttpUtility.UrlEncode(strIpnNotificationUrl));
                                // request specific data fields
                                sRequest.Append("&actionType=");
                                sRequest.Append(HttpUtility.UrlEncode(strActionType.ToUpper())); //Action type needs to be upper case when sent as parameter to Paypal
                                sRequest.Append("&cancelUrl=");
                                sRequest.Append(HttpUtility.UrlEncode(strCancelUrl));
                                sRequest.Append("&returnUrl=");
                                sRequest.Append(HttpUtility.UrlEncode(strReturnUrl));
                                sRequest.Append("&currencyCode=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.CurrencyCode));
                                sRequest.Append("&feesPayer=");
                                sRequest.Append(HttpUtility.UrlEncode(gatewaySettings.FeesPayer));
                                sRequest.Append("&memo=");
                                sRequest.Append(HttpUtility.UrlEncode(strMemo));
                                sRequest.Append("&receiverList.receiver(0).amount=");
                                sRequest.Append(HttpUtility.UrlEncode(strReceiverAmount));
                                sRequest.Append("&receiverList.receiver(0).email=");
                                sRequest.Append(HttpUtility.UrlEncode(strReceiverEmail));

                                if (strReceiver2AppendToRequest.ToLower().Equals("true"))
                                {
                                    // We set the 1st receiver as primary only if there are more than one receivers. 
                                    sRequest.Append("&receiverList.receiver(0).primary=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmailPrimary));

                                    sRequest.Append("&receiverList.receiver(1).amount=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverAmount2));
                                    sRequest.Append("&receiverList.receiver(1).email=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmail2));
                                    sRequest.Append("&receiverList.receiver(1).primary=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmailPrimary2));
                                }

                                if (strReceiver3AppendToRequest.ToLower().Equals("true"))
                                {
                                    sRequest.Append("&receiverList.receiver(2).amount=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverAmount3));
                                    sRequest.Append("&receiverList.receiver(2).email=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmail3));
                                    sRequest.Append("&receiverList.receiver(2).primary=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmailPrimary3));
                                }
                                if (strReceiver4AppendToRequest.ToLower().Equals("true"))
                                {
                                    sRequest.Append("&receiverList.receiver(3).amount=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverAmount4));
                                    sRequest.Append("&receiverList.receiver(3).email=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmail4));
                                    sRequest.Append("&receiverList.receiver(3).primary=");
                                    sRequest.Append(HttpUtility.UrlEncode(strReceiverEmailPrimary4));
                                }

                                //sRequest.Append("&senderEmail=");
                                //sRequest.Append(HttpUtility.UrlEncode(sSenderEmail));
                                sRequest.Append("&trackingId=");
                                sRequest.Append(HttpUtility.UrlEncode(strTrackingID));
                                break;
                            }
                }
                }
                catch
                {
                }
                return sRequest.ToString();
            }

            private Response ParseResponse(string GatewayResponse)
            {
                Response response = new Response();
                response.GatewayResponseRaw = GatewayResponse;
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
                    switch (hGatewayResponse["responseEnvelope.ack"].ToString().ToLower())
                    {
                        case "success":
                            //Approved
                            response.ResponseType = TransactionResponseType.Approved;
                            switch (gatewaySettings.ApiEndPointUri.ToLower())
                            {
                                case "paymentdetails":
                                    {
                                        break;
                                    }
                                case "getshippingaddresses":
                                    {
                                        break;
                                    }
                                case "setpaymentoptions":
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        response.ReasonText = aryGatewayResponse[5].Split('=')[1];
                                        response.TransactionID = aryGatewayResponse[4].Split('=')[1];
                                        response.AuthCode = aryGatewayResponse[4].Split('=')[1];
                                        response.MerchantDefined1 = gatewaySettings.RedirectUrl + aryGatewayResponse[4].Split('=')[1];
                                        break;
                                    }
                            }                            
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
                    if (xmlSettings.Attribute("ApiEndPoint") != null)
                        gatewaySettings.ApiEndPoint = xmlSettings.Attribute("ApiEndPoint").Value;

                    if (xmlSettings.Attribute("User") != null)
                        gatewaySettings.User = xmlSettings.Attribute("User").Value;

                    if (xmlSettings.Attribute("Password") != null)
                        gatewaySettings.Password = xmlSettings.Attribute("Password").Value;

                    if (xmlSettings.Attribute("Signature") != null)
                        gatewaySettings.Signature = xmlSettings.Attribute("Signature").Value;

                    if (xmlSettings.Attribute("Version") != null)
                        gatewaySettings.Version = xmlSettings.Attribute("Version").Value;

                    if (xmlSettings.Attribute("ErrorLanguage") != null)
                        gatewaySettings.ErrorLanguage = xmlSettings.Attribute("ErrorLanguage").Value;

                    if (xmlSettings.Attribute("DetailLevel") != null)
                        gatewaySettings.DetailLevel = xmlSettings.Attribute("DetailLevel").Value;

                    if (xmlSettings.Attribute("RequestDataBinding") != null)
                        gatewaySettings.RequestDataBinding = xmlSettings.Attribute("RequestDataBinding").Value;

                    if (xmlSettings.Attribute("ResponseDataBinding") != null)
                        gatewaySettings.ResponseDataBinding = xmlSettings.Attribute("ResponseDataBinding").Value;

                    if (xmlSettings.Attribute("AppID") != null)
                        gatewaySettings.AppID = xmlSettings.Attribute("AppID").Value;

                    if (xmlSettings.Attribute("PartnerName") != null)
                        gatewaySettings.PartnerName = xmlSettings.Attribute("PartnerName").Value;

                    if (xmlSettings.Attribute("CurrencyCode") != null)
                        gatewaySettings.CurrencyCode = xmlSettings.Attribute("CurrencyCode").Value;

                    if (xmlSettings.Attribute("ActionType") != null)
                        gatewaySettings.ActionType = xmlSettings.Attribute("ActionType").Value;

                    if (xmlSettings.Attribute("FeesPayer") != null)
                        gatewaySettings.FeesPayer = xmlSettings.Attribute("FeesPayer").Value;

                    if (xmlSettings.Attribute("RedirectUrl") != null)
                        gatewaySettings.RedirectUrl = xmlSettings.Attribute("RedirectUrl").Value;

                    if (xmlSettings.Attribute("ReceiverEmail") != null)
                        gatewaySettings.ReceiverEmail = xmlSettings.Attribute("ReceiverEmail").Value;

                    if (xmlSettings.Attribute("DelimChar") != null)
                        gatewaySettings.DelimChar = xmlSettings.Attribute("DelimChar").Value;         
                    
                    if (string.IsNullOrEmpty(gatewaySettings.ApiEndPoint))
                    {
                        throw new PaymentProviderException("ApiEndPoint cannot be null");
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

                    if (string.IsNullOrEmpty(gatewaySettings.ActionType))
                    {
                        throw new PaymentProviderException("ActionType cannot be null");
                    }
                    
                    if (string.IsNullOrEmpty(gatewaySettings.ReceiverEmail))
                    {
                        throw new PaymentProviderException("ReceiverEmail cannot be null");
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
