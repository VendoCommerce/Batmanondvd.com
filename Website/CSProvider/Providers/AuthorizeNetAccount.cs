using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;
using Microsoft.CSharp.RuntimeBinder;


namespace CSPaymentProvider.Providers
{
    public class AuthorizeNetAccount : IPaymentProvider
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
            public string Login { get; set; }
            public string AlternateLogin { get; set; }
            public string MarketType { get; set; }
            public string MerchantEmail { get; set; }
            public string RelayResponse { get; set; }
            public string TestMode { get; set; }
            public string TransactionKey { get; set; }
            public string AlternateTransactionKey { get; set; }
            public string TransactionURL { get; set; }
            public string Version { get; set; }
            public string RequestType { get; set; }
            public string Method { get; set; }
            public bool TransactionTest { get; set; }
            public string AlternateCredTrigger { get; set; }
            
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

            public static object GetProperty(object o, string member)
            {
                if (o == null) throw new ArgumentNullException("o");
                if (member == null) throw new ArgumentNullException("member");
                Type scope = o.GetType();
                IDynamicMetaObjectProvider provider = o as IDynamicMetaObjectProvider;
                if (provider != null)
                {
                    ParameterExpression param = Expression.Parameter(typeof(object));
                    DynamicMetaObject mobj = provider.GetMetaObject(param);
                    GetMemberBinder binder = (GetMemberBinder)Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, member, scope, new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(0, null) });
                    DynamicMetaObject ret = mobj.BindGetMember(binder);
                    BlockExpression final = Expression.Block(
                        Expression.Label(CallSiteBinder.UpdateLabel),
                        ret.Expression
                    );
                    LambdaExpression lambda = Expression.Lambda(final, param);
                    Delegate del = lambda.Compile();
                    return del.DynamicInvoke(o);
                }
                else
                {
                    return o.GetType().GetProperty(member, BindingFlags.Public | BindingFlags.Instance).GetValue(o, null);
                }
            }
            protected string BuildRequestPost(Request request)
            {
                string altTrigger = gatewaySettings.AlternateCredTrigger;
                string[] altTriggerArray = new string[2];
                if (altTrigger != null && altTrigger.Length > 2)
                {
                    altTriggerArray = altTrigger.Split('_');    
                }
                string xLogin = gatewaySettings.Login;
                string xTranKey = gatewaySettings.TransactionKey;

                
                    if (altTriggerArray[1] != null && altTriggerArray[0] != null && altTriggerArray[1].Length > 0  && altTriggerArray[0].Length > 0)
                    {
                        if (
                            altTriggerArray[1].ToLower()
                                              .Trim()
                                              .Equals(
                                                  GetProperty(request, altTriggerArray[0]).ToString().ToLower().Trim()))
                        {
                            xLogin = gatewaySettings.AlternateLogin;
                            xTranKey = gatewaySettings.AlternateTransactionKey;
                        }
                    }
                
                



                string NameValueFormat = "{0}={1}";
                List<string> aryPostParams = new List<string> {
            string.Format(NameValueFormat, "x_login", xLogin),
            string.Format(NameValueFormat, "x_tran_key", xTranKey),
            string.Format(NameValueFormat, "x_delim_data", gatewaySettings.DelimData),
            string.Format(NameValueFormat, "x_delim_char", gatewaySettings.DelimChar),
            string.Format(NameValueFormat, "x_relay_response", gatewaySettings.RelayResponse),
            string.Format(NameValueFormat, "x_type", gatewaySettings.RequestType),
            string.Format(NameValueFormat, "x_method", gatewaySettings.Method)
        };
                try
                {
                    if (request.Amount > 0.0)
                    {
                        aryPostParams.Add(string.Format("x_amount={0:.00}", request.Amount));
                    }
                    aryPostParams.Add(string.Format("x_exp_date={0:####}", request.ExpireDate.ToString("MMyyyy")));
                    aryPostParams.Add(string.Format(NameValueFormat, "x_card_num", request.CardNumber));
                    aryPostParams.Add(string.Format(NameValueFormat, "x_description", request.TransactionDescription));
                    aryPostParams.Add(string.Format(NameValueFormat, "x_invoice_num", request.InvoiceNumber));
                    if (request.FirstName.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_first_name", request.FirstName));
                    }
                    if (request.LastName.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_last_name", request.LastName));
                    }
                    if (request.Address1.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_address", request.Address1 + (!string.IsNullOrEmpty(request.Address2) ? ("+" + request.Address2) : string.Empty)));
                    }
                    if (request.City.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_city", request.City));
                    }
                    if (request.State.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_state", request.State));
                    }
                    if (request.ZipCode.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_zip", request.ZipCode));
                    }
                    if (request.Country.Trim().Length > 0)
                    {
                        aryPostParams.Add(string.Format(NameValueFormat, "x_country", request.Country));
                    }
                    bool testMode = false;
                    bool.TryParse(gatewaySettings.TransactionTest.ToString(), out testMode);
                    aryPostParams.Add(string.Format(NameValueFormat, "x_test_request", testMode ? "Y" : "N"));
                }
                catch
                {
                    //throw new GatewayException(string.Format("An exception occured while creating the post parameters: {0}", ex.Message), ex);
                }
                return string.Join("&", aryPostParams.ToArray());
            }

            private Response ParseResponse(string GatewayResponse)
            {

                Response response = new Response();
                response.GatewayResponseRaw = GatewayResponse;                
                char[] charTxt = new char[1];
                charTxt[0] = System.Convert.ToChar(AuthorizeNetAccount.gatewaySettings.DelimChar);
                string[] aryGatewayResponse = GatewayResponse.Split(charTxt);
                if (aryGatewayResponse.Length > 0)
                {
                    if (aryGatewayResponse[0].Equals("1"))
                    {
                        response.ResponseType = TransactionResponseType.Approved;
                    }
                    else
                    {
                        response.ResponseType = TransactionResponseType.Denied;
                    }
                    response.AuthCode = aryGatewayResponse[4];
                    response.TransactionID = aryGatewayResponse[6];
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

                    if (xmlSettings.Attribute("login") != null)
                        gatewaySettings.Login = xmlSettings.Attribute("login").Value;

                    if (xmlSettings.Attribute("Alternatelogin") != null)
                        gatewaySettings.AlternateLogin = xmlSettings.Attribute("Alternatelogin").Value;
            
                    if (xmlSettings.Attribute("transactionKey") != null)
                        gatewaySettings.TransactionKey = xmlSettings.Attribute("transactionKey").Value;

                    if (xmlSettings.Attribute("AlternatetransactionKey") != null)
                        gatewaySettings.AlternateTransactionKey = xmlSettings.Attribute("AlternatetransactionKey").Value;

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

                    if (xmlSettings.Attribute("x_method") != null)
                        gatewaySettings.Method = xmlSettings.Attribute("x_method").Value;

                    if (xmlSettings.Attribute("AlternateCredTrigger") != null)
                        gatewaySettings.AlternateCredTrigger = xmlSettings.Attribute("AlternateCredTrigger").Value;
              
                    if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                    {
                        throw new PaymentProviderException("TransactionURL cannot be null");
                    }

                    if (string.IsNullOrEmpty(gatewaySettings.Login))
                    {
                        throw new PaymentProviderException("Login cannot be null");
                    }

                    if (string.IsNullOrEmpty(gatewaySettings.TransactionKey))
                    {
                        throw new PaymentProviderException("TransactionKey cannot be null");
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

        #endregion
    }
}
