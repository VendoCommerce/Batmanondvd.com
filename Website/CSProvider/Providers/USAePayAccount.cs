using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;


namespace CSPaymentProvider.Providers
{
    public class USAePayAccount : IPaymentProvider
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
            try
            {
                response = ParseResponse(BuildRequestPost(request));
            }
            catch (Exception ex)
            {
                throw new PaymentProviderException("An exception occured while getting the response stream for the gateway", ex);
            }
            finally
            {

            }

            return response;

        }
        #endregion
        public Response PerformVoidRequest(Request request)
        {

            Response response = new Response();
            return response;

        }

        protected USAePayAPI.USAePay BuildRequestPost(Request request)
        {

            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();

            //Create parameters from objProvider settings

            //Init object with our source key and pin from usaepay.com
            usaepay.SourceKey = gatewaySettings.SourceKey;
            usaepay.Pin = gatewaySettings.Pin;
            usaepay.GatewayURL = gatewaySettings.TransactionURL;

            //Create parameters from request

            usaepay.BillingFirstName = request.FirstName;
            usaepay.BillingLastName = request.LastName;
            usaepay.BillingStreet = request.Address1;
            usaepay.BillingStreet2 = request.Address2;
            usaepay.BillingZip = request.ZipCode;
            usaepay.BillingState = request.State;
            usaepay.BillingCountry = request.CompanyName;
            usaepay.BillingPhone = request.Phone;

            usaepay.ShippingFirstName = request.ShipToFirstName;
            usaepay.ShippingLastName = request.ShipToLastName;
            usaepay.ShippingStreet = request.ShipToAddress;
            usaepay.ShippingZip = request.ShipToZipCode;
            usaepay.ShippingState = request.ShipToState;
            usaepay.ShippingCountry = request.ShipToCountry;


            usaepay.Amount = Convert.ToDecimal(request.Amount);
            usaepay.Description = request.TransactionDescription;
            usaepay.CardHolder = request.FirstName + " " + request.LastName;
            usaepay.CardNumber = request.CardNumber;
            usaepay.Cavv = request.CardCvv;
            usaepay.CardExp = request.ExpireDate.ToString("MMyy"); // Format specify. Make sure its 4 digit
            usaepay.CustID = request.CustomerID;
            usaepay.OrderID = request.InvoiceNumber;

            try
            {
                if (gatewaySettings.RequestType.ToLower().Equals("preauth"))
                {
                    usaepay.AuthOnly(); //PreAuth Transaction
                }
                else if (gatewaySettings.RequestType.ToLower().Equals("sale"))
                {
                    usaepay.Sale(); //Posting Transaction
                }
                else if (gatewaySettings.RequestType.ToLower().Equals("capture"))
                {
                    usaepay.Capture();//Posting Transaction
                }
                else
                {
                    usaepay.Sale();// By default we will consider sale. //Posting Transaction
                }

            }
            catch (Exception ex)
            {

                throw new PaymentProviderException(string.Format("An exception occured while creating the post parameters: {0}", ex.Message), ex);
            }

            return usaepay;

        }

        private Response ParseResponse(USAePayAPI.USAePay usaepay)
        {
            Response response = new Response();
            response.AuthCode = usaepay.AuthCode;
            response.TransactionID = usaepay.ResultRefNum;
            response.ReasonText = usaepay.ErrorCode + "" + usaepay.ErrorMesg;
            response.GatewayResponseRaw = usaepay.RawResult;            
            if (usaepay.ResultCode == "A")
            {
                response.ResponseType = TransactionResponseType.Approved;
            }
            else if (usaepay.ResultCode == "D")
            {
                response.ResponseType = TransactionResponseType.Denied;
            }
            else
            {
                response.ResponseType = TransactionResponseType.Error;
            }

            return response;

        }

        private class GatewaySettings
        {
            #region Public Properties
            public string CurrencyCode { get; set; }
            public string MerchantEmail { get; set; }
            public string EmailCustomer { get; set; }
            public string DuplicateWindow { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string TransactionKey { get; set; }
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
            public string SourceKey { get; set; }
            public string Pin { get; set; }

            #endregion
        }

        public PaymentProviderType Type
        {
            get { return PaymentProviderType.USAePayAccount; }
        }

        public void Initialize(XElement xmlSettings)
        {            
            try
            {
                gatewaySettings = new GatewaySettings();

                if (xmlSettings.Attribute("transactionUrl") != null)
                    gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

                if (xmlSettings.Attribute("login") != null)
                    gatewaySettings.Login = xmlSettings.Attribute("login").Value;

                if (xmlSettings.Attribute("password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("password").Value;

                if (xmlSettings.Attribute("transactionKey") != null)
                    gatewaySettings.TransactionKey = xmlSettings.Attribute("transactionKey").Value;

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

                if (xmlSettings.Attribute("sourceKey") != null)
                    gatewaySettings.SourceKey = xmlSettings.Attribute("sourceKey").Value;

                if (xmlSettings.Attribute("pin") != null)
                    gatewaySettings.Pin = xmlSettings.Attribute("pin").Value;

                if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                {
                    throw new PaymentProviderException("TransactionURL cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.SourceKey))
                {
                    throw new PaymentProviderException("Source Key cannot be null");
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
