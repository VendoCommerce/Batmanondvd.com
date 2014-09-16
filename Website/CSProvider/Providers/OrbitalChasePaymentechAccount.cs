using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using CSPaymentProvider;
using CSPaymentProvider.net.paymentech.wsvar;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Configuration;

namespace CSPaymentProvider.Providers
{
    public class OrbitalChasePaymentechAccount : IPaymentProvider
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
            public string TerminalId { get; set; }
            public string IndustryType { get; set; }
            public string Bin { get; set; }
            public string MerchantEmail { get; set; }
            public string RelayResponse { get; set; }
            public string TestMode { get; set; }
            public string MerchantId { get; set; }
            public string TransactionURL { get; set; }
            public string Version { get; set; }
            public string RequestType { get; set; }
            public bool TransactionTest { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string SafetechMerchantId { get; set; }

        }
        #endregion

        #region Public Methods

        public Response PerformRequest(Request request)
        {
            Exception ex;
            Response response = new Response();
            string requestXML = string.Empty;
            try
            {
                response = this.BuildRequest(request);
            }
            catch (Exception exception2)
            {
                ex = exception2;
                //throw new GatewayException("An exception occured while getting the response stream for the gateway", ex);
            }
            return response;

        }

        //VOIDS a transaction
        public Response PerformVoidRequest(Request voidRequest)
        {
            Response response = new Response();
            PaymentechGateway server = new PaymentechGateway();
            server.Url = gatewaySettings.TransactionURL;
            ReversalElement reversalEl = new ReversalElement();
            //Default values coming from config file.
            reversalEl.adjustedAmount = string.Empty; //Should be empty for VOID, otherwise specifiy for partial refund
            reversalEl.bin = gatewaySettings.Bin;
            reversalEl.merchantID = gatewaySettings.MerchantId;
            reversalEl.terminalID = gatewaySettings.TerminalId;
            reversalEl.bin = gatewaySettings.Bin;
            reversalEl.version = gatewaySettings.Version;
            if (gatewaySettings.User.Length > 0)
            {
                reversalEl.orbitalConnectionUsername = gatewaySettings.User;
            }
            if (gatewaySettings.Password.Length > 0)
            {
                reversalEl.orbitalConnectionPassword = gatewaySettings.Password;
            }
            reversalEl.txRefIdx = voidRequest.TransactionID;
            reversalEl.txRefNum = voidRequest.AuthCode;
            reversalEl.orderID = voidRequest.InvoiceNumber.ToString();
            //Perform void request
            try
            {
                ReversalResponseElement reversalResponse = server.Reversal(reversalEl);

                Console.WriteLine("ProcStatus: " +
                reversalResponse.procStatus);
                Console.WriteLine("ApprovalStatus: " +
                reversalResponse.approvalStatus);

                if (reversalResponse.respCode.Equals("00") && reversalResponse.approvalStatus.Equals("1"))
                {
                    response.ResponseType = TransactionResponseType.Approved;
                }
                else if (reversalResponse.approvalStatus.Equals("0"))
                {
                    response.ResponseType = TransactionResponseType.Denied;
                }
                else
                {
                    response.ResponseType = TransactionResponseType.Error;
                }
                response.ReasonText = reversalResponse.procStatusMessage;
                response.TransactionID = reversalResponse.txRefNum;
                response.GatewayResponseRaw = Serialize(reversalResponse);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                response.ResponseType = TransactionResponseType.Error;

                response.GatewayResponseRaw = "Exception message: " + ex.Message;
            }

            return response;

        }
        public Response PerformValidationRequest(Request request)
        {

            Response response = new Response();

           
            PaymentechGateway server = new PaymentechGateway();
            server.Url = gatewaySettings.TransactionURL;
            

            SafetechFraudAnalysisRequestElement authBean1 = new SafetechFraudAnalysisRequestElement();
            NewOrderRequestElement authBean = new NewOrderRequestElement();
            //Default values coming from config file.
            authBean1.merchantID = gatewaySettings.MerchantId;
            authBean1.terminalID = gatewaySettings.TerminalId;

            authBean1.bin = gatewaySettings.Bin;
            authBean1.version = gatewaySettings.Version;
            if (gatewaySettings.User.Length > 0)
            {
                authBean1.orbitalConnectionUsername = gatewaySettings.User;
            }
            if (gatewaySettings.Password.Length > 0)
            {
                authBean1.orbitalConnectionPassword = gatewaySettings.Password;
            }

            //Specific values per customer.

            BaseElementsType bs = new BaseElementsType();
            FraudAnalysisType fat = new FraudAnalysisType();


            bs.orderID = request.CustomerID;
            bs.amount = (request.Amount * 100).ToString();
            bs.ccAccountNum = request.CardNumber;
            bs.ccExp = request.ExpireDate.ToString("yyyyMM");
            bs.ccCardVerifyNum = request.CardCvv;
            bs.avsName = request.FirstName + " " + request.LastName;
            bs.avsAddress1 = request.Address1;
            bs.avsCity = request.City;
            bs.avsZip = request.ZipCode;
            bs.avsState = request.State;
            bs.comments = request.TransactionDescription;
            bs.avsCountryCode = request.Country;
            bs.customerEmail = request.Email;
            bs.avsPhone = request.Phone;
            bs.avsDestPhoneNum = request.Phone;
            bs.avsDestName = request.ShipToFirstName + " " + request.ShipToLastName;
            bs.avsDestAddress1 = request.Address1;
            bs.avsDestCity = request.ShipToCity;
            bs.avsDestCountryCode = request.ShipToCountry;
            bs.avsDestZip = request.ShipToZipCode;
            bs.customerIpAddress = request.IPAddress;

            fat.fraudScoreIndicator = "1";
            fat.rulesTrigger = "Y";
            fat.safetechMerchantID = gatewaySettings.SafetechMerchantId;

            fat.cashValueOfFencibleItems = (request.Amount * 100).ToString();

            authBean1.baseElements = bs;
            authBean1.fraudAnalysis = fat;
            string s = Serialize(authBean1);
            //System.IO.File.WriteAllText(@"c:\BatchProcesses\StonedineSafetTechTest\WriteReq.txt", s);
            try
            {
                SafetechFraudAnalysisResponseElement responseBean1 = server.SafetechFraudAnalysis(authBean1);


                string s2 = Serialize(responseBean1);
                response.GatewayResponseRaw = s2;
                response.MerchantDefined1 = s;
                //System.IO.File.WriteAllText(@"c:\BatchProcesses\StonedineSafetTechTest\WriteRes.txt", s2);
                Console.WriteLine("ProcStatus: " +
                responseBean1.procStatus);
                Console.WriteLine("ApprovalStatus: " +
                responseBean1.approvalStatus);
                Console.ReadLine();

                if (responseBean1.fraudAnalysisResponse.autoDecisionResponse.ToUpper().Equals("A") && Convert.ToInt32(responseBean1.fraudAnalysisResponse.riskScore) <= 50)
                {
                    response.ResponseType = TransactionResponseType.Approved;
                }
                else if (!responseBean1.fraudAnalysisResponse.autoDecisionResponse.ToUpper().Equals("A"))
                {
                    response.ResponseType = TransactionResponseType.Denied;
                }
                else
                {
                    response.ResponseType = TransactionResponseType.Error;
                }
                response.ReasonText = responseBean1.fraudAnalysisResponse.fraudStatusCode;
                response.TransactionID = responseBean1.fraudAnalysisResponse.riskInquiryTransactionID;
                response.AvsResponse = TransactionAvsResponse.Match;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                Console.WriteLine(ex.Message);
                response.ResponseType = TransactionResponseType.Error;
                Console.ReadLine();
            }
            return response;

        }
        protected Response BuildRequest(Request request)
        {
            Response response = new Response();
            PaymentechGateway server = new PaymentechGateway();
            server.Url = gatewaySettings.TransactionURL;
            NewOrderRequestElement authBean = new NewOrderRequestElement();
            //Default values coming from config file.
            authBean.merchantID = gatewaySettings.MerchantId;
            authBean.terminalID = gatewaySettings.TerminalId;
            authBean.industryType = gatewaySettings.IndustryType;
            authBean.bin = gatewaySettings.Bin;
            authBean.version = gatewaySettings.Version;
            authBean.transType = gatewaySettings.RequestType;
            if (gatewaySettings.User.Length > 0)
            {
                authBean.orbitalConnectionUsername = gatewaySettings.User;
            }
            if (gatewaySettings.Password.Length > 0)
            {
                authBean.orbitalConnectionPassword = gatewaySettings.Password;
            }
            //Specific values per customer.
            authBean.orderID = request.InvoiceNumber;
            authBean.amount = (request.Amount * 100).ToString();
            authBean.ccAccountNum = request.CardNumber;
            authBean.ccExp = request.ExpireDate.ToString("yyyyMM");
            authBean.ccCardVerifyNum = request.CardCvv;
            authBean.avsName = request.FirstName + " " + request.LastName;
            authBean.avsAddress1 = request.Address1;
            authBean.avsCity = request.City;
            authBean.avsZip = request.ZipCode;
            authBean.avsCountryCode = request.Country;
            authBean.customerPhone = request.Phone;
            authBean.customerName = request.FirstName + " " + request.LastName;
            authBean.customerEmail = request.Email;
            
            response.GatewayRequestRaw = Serialize(authBean);

            try
            {
                NewOrderResponseElement responseBean =
                server.NewOrder(authBean);

                Console.WriteLine("ProcStatus: " +
                responseBean.procStatus);
                Console.WriteLine("ApprovalStatus: " +
                responseBean.approvalStatus);

                if (responseBean.respCode.Equals("00") && responseBean.approvalStatus.Equals("1"))
                {
                    response.ResponseType = TransactionResponseType.Approved;
                }
                else if (responseBean.approvalStatus.Equals("0"))
                {
                    response.ResponseType = TransactionResponseType.Denied;
                }
                else
                {
                    response.ResponseType = TransactionResponseType.Error;
                }
                response.ReasonText = responseBean.respCodeMessage;
                response.TransactionID = responseBean.txRefNum;
                response.AuthCode = responseBean.authorizationCode;
                response.MerchantDefined1 = responseBean.txRefIdx;
                response.GatewayResponseRaw = Serialize(responseBean);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                response.ResponseType = TransactionResponseType.Error;

                response.GatewayResponseRaw = "Exception message: " + ex.Message;
            }
            return response;
        }

        public Response PerformVoidSettledRequest(Request request)
        {
            Response response = new Response();
            return response;
        }

        private Response ParseResponse(string GatewayResponse)
        {

            Response response = new Response();
            XmlDocument xmlDoc = new XmlDocument();
            response.GatewayResponseRaw = GatewayResponse;
            xmlDoc.LoadXml(GatewayResponse);

            XmlNode nodeResponse = xmlDoc.SelectSingleNode("litleOnlineResponse/authorizationResponse");
            if (nodeResponse != null)
            {
                response.AuthCode = nodeResponse["authCode"].InnerText;
                response.TransactionID = nodeResponse["litleTxnId"].InnerText;
                response.ReasonText = nodeResponse["message"].InnerText;

                if (nodeResponse["response"].InnerText.ToString() == "000")
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
                if (xmlSettings.Attribute("user") != null)
                    gatewaySettings.User = xmlSettings.Attribute("user").Value;

                if (xmlSettings.Attribute("password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("password").Value;

                if (xmlSettings.Attribute("transactionUrl") != null)
                    gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

                if (xmlSettings.Attribute("merchantId") != null)
                    gatewaySettings.MerchantId = xmlSettings.Attribute("merchantId").Value;

                if (xmlSettings.Attribute("terminalId") != null)
                    gatewaySettings.TerminalId = xmlSettings.Attribute("terminalId").Value;

                if (xmlSettings.Attribute("industryType") != null)
                    gatewaySettings.IndustryType = xmlSettings.Attribute("industryType").Value;

                if (xmlSettings.Attribute("bin") != null)
                    gatewaySettings.Bin = xmlSettings.Attribute("bin").Value;

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

                if (xmlSettings.Attribute("safetechmerchantid") != null)
                    gatewaySettings.SafetechMerchantId = xmlSettings.Attribute("safetechmerchantid").Value;
                

                if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
                {
                    throw new PaymentProviderException("TransactionURL cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.TerminalId))
                {
                    throw new PaymentProviderException("TerminalId cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.IndustryType))
                {
                    throw new PaymentProviderException("IndustryType cannot be null");
                }

                if (string.IsNullOrEmpty(gatewaySettings.Bin))
                {
                    throw new PaymentProviderException("Bin cannot be null");
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

        public static string Serialize(object data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, data);

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        #endregion
    }
}
