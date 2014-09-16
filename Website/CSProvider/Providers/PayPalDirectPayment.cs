using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Web;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.Manager;

namespace CSPaymentProvider.Providers
{
    public class PayPalDirectPayment : IPaymentProvider
    {
        private static GatewaySettings gatewaySettings = null;
        //private static string errRequiredNode = "A required gateway node does not exist: {0}";

        private class GatewaySettings
        {
            public string CurrencyCode { get; set; }
            public string DelimChar { get; set; }
            public string DelimData { get; set; }
            public string Mode { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string Signature { get; set; }
            public string Version { get; set; }
            public string ErrorLanguage { get; set; }
            public string PaymentAction { get; set; }
            public string RedirectUrl { get; set; }
            public string AppID { get; set; }
            public string Timeout { get; set; }
            public string SkuItems { get; set; }
        }

        public PaymentProviderType Type
        {
            get { return PaymentProviderType.PayPalDirectPayment; }
        }

        public void Initialize(System.Xml.Linq.XElement xmlSettings)
        {
            try
            {
                gatewaySettings = new GatewaySettings();

                if (xmlSettings.Attribute("User") != null)
                    gatewaySettings.User = xmlSettings.Attribute("User").Value;

                if (xmlSettings.Attribute("Password") != null)
                    gatewaySettings.Password = xmlSettings.Attribute("Password").Value;

                if (xmlSettings.Attribute("Signature") != null)
                    gatewaySettings.Signature = xmlSettings.Attribute("Signature").Value;

                if (xmlSettings.Attribute("AppID") != null)
                    gatewaySettings.AppID = xmlSettings.Attribute("AppID").Value;

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

                if (xmlSettings.Attribute("Timeout") != null)
                    gatewaySettings.Timeout = xmlSettings.Attribute("Timeout").Value;

                if (xmlSettings.Attribute("Mode") != null)
                    gatewaySettings.Mode = xmlSettings.Attribute("Mode").Value;

                if (string.IsNullOrEmpty(gatewaySettings.Mode))
                {
                    throw new PaymentProviderException("Mode cannot be null - need: sandbox or live");
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

        public Response PerformRequest(Request request)
        {
            // Create request object
            DoDirectPaymentRequestType paypalRequest = new DoDirectPaymentRequestType();

            DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
            paypalRequest.DoDirectPaymentRequestDetails = requestDetails;

            // (Optional) How you want to obtain payment. It is one of the following values:
            // * Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // * Sale – This is a final sale for which you are requesting payment (default).
            // Note: Order is not allowed for Direct Payment.
            requestDetails.PaymentAction = (PaymentActionCodeType)
                Enum.Parse(typeof(PaymentActionCodeType), gatewaySettings.PaymentAction.ToUpper());

            // (Required) Information about the credit card to be charged.
            CreditCardDetailsType creditCard = new CreditCardDetailsType();
            requestDetails.CreditCard = creditCard;
            PayerInfoType payer = new PayerInfoType();
            // (Optional) First and last name of buyer.
            PersonNameType name = new PersonNameType();
            name.FirstName = request.FirstName;
            name.LastName = request.LastName;
            payer.PayerName = name;

            // (Required) Details about the owner of the credit card.
            creditCard.CardOwner = payer;

            // (Required) Credit card number.
            creditCard.CreditCardNumber = request.CardNumber;
            // (Optional) Type of credit card. For UK, only Maestro, MasterCard, Discover, and Visa are allowable. For Canada, only MasterCard and Visa are allowable and Interac debit cards are not supported. It is one of the following values:
            // * Visa
            // * MasterCard
            // * Discover
            // * Amex
            // * Maestro: See note.
            // Note: If the credit card type is Maestro, you must set currencyId to GBP. In addition, you must specify either StartMonth and StartYear or IssueNumber.
            creditCard.CreditCardType = (CreditCardTypeType)
                Enum.Parse(typeof(CreditCardTypeType), UpdateCreditCardType(request.CardType));
            // Card Verification Value, version 2. Your Merchant Account settings determine whether this field is required. To comply with credit card processing regulations, you must not store this value after a transaction has been completed.
            // Character length and limitations: For Visa, MasterCard, and Discover, the value is exactly 3 digits. For American Express, the value is exactly 4 digits.
            creditCard.CVV2 = request.CardCvv;
            // (Required) Credit card expiration month.
            creditCard.ExpMonth = request.ExpireDate.Month;
            // (Required) Credit card expiration year.
            creditCard.ExpYear = request.ExpireDate.Year;

            requestDetails.PaymentDetails = new PaymentDetailsType();
            // (Optional) Your URL for receiving Instant Payment Notification (IPN) about this transaction. If you do not specify this value in the request, the notification URL from your Merchant Profile is used, if one exists.
            // Important: The notify URL applies only to DoExpressCheckoutPayment. This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            //requestDetails.PaymentDetails.NotifyURL = "";

            // (Optional) Buyer's shipping address information. 
            AddressType billingAddr = new AddressType();

            billingAddr.Name = request.FirstName + " " + request.LastName;
            // (Required) First street address.
            billingAddr.Street1 = request.Address1;
            // (Optional) Second street address.
            billingAddr.Street2 = request.Address2;
            // (Required) Name of city.
            billingAddr.CityName = request.City;
            // (Required) State or province.
            billingAddr.StateOrProvince = request.State;
            // (Required) Country code.
            billingAddr.Country = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), request.Country);
            // (Required) U.S. ZIP code or other country-specific postal code.
            billingAddr.PostalCode = request.ZipCode;

            // (Optional) Phone number.
            billingAddr.Phone = request.Phone;

            payer.Address = billingAddr;

            AddressType shippingAddr = new AddressType();

            shippingAddr.Name = request.ShipToFirstName + " " + request.ShipToLastName;
            shippingAddr.Street1 = request.ShipToAddress;
            shippingAddr.CityName = request.ShipToCity;
            shippingAddr.StateOrProvince = request.ShipToState;
            shippingAddr.PostalCode = request.ShipToZipCode;
            shippingAddr.Country = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), request.ShipToCountry);

            requestDetails.PaymentDetails.ShipToAddress = shippingAddr;

            // (Required) The total cost of the transaction to the buyer. If shipping cost and tax charges are known, include them in this value. If not, this value should be the current subtotal of the order. If the transaction includes one or more one-time purchases, this field must be equal to the sum of the purchases. This field must be set to a value greater than 0.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), gatewaySettings.CurrencyCode);

            BasicAmountType paymentAmount = new BasicAmountType(currency, request.Amount.ToString("N2"));

            requestDetails.PaymentDetails.OrderTotal = paymentAmount;
            requestDetails.PaymentDetails.ItemTotal = new BasicAmountType(currency, Convert.ToDouble(request.SubTotal ?? "0").ToString("N2"));
            requestDetails.PaymentDetails.ShippingTotal = new BasicAmountType(currency, Convert.ToDouble(request.ShippingTotal ?? "0").ToString("N2"));
            requestDetails.PaymentDetails.TaxTotal = new BasicAmountType(currency, Convert.ToDouble(request.Tax ?? "0").ToString("N2"));

            // add skus
            List<PaymentDetailsItemType> items = new List<PaymentDetailsItemType>();

            foreach (PaymentSku sku in request.SkuItems)
            {
                PaymentDetailsItemType item = new PaymentDetailsItemType();

                item.Amount = new BasicAmountType(currency, (sku.InitialPrice * sku.Quantity).ToString("N2"));
                item.Quantity = sku.Quantity;
                item.Name = sku.Title;
                item.Number = sku.SkuCode;
                item.Description = sku.LongDescription;
                                                
                items.Add(item);
            }

            requestDetails.PaymentDetails.PaymentDetailsItem = items;

            // Invoke the API
            DoDirectPaymentReq wrapper = new DoDirectPaymentReq();
            wrapper.DoDirectPaymentRequest = paypalRequest;
            // Create the PayPalAPIInterfaceServiceService service object to make the API call

            Dictionary<string, string> config = new Dictionary<string, string>();

            // enforce 30000 minimum for timeout specification (30 secs)
            int timeout = 30000;
            timeout = Math.Max(timeout, Convert.ToInt32(gatewaySettings.Timeout ?? "0"));

            config.Add("account0.apiUsername", gatewaySettings.User);
            config.Add("account0.apiPassword", gatewaySettings.Password);
            config.Add("account0.apiSignature", gatewaySettings.Signature);
            config.Add("account0.applicationId", gatewaySettings.AppID);
            config.Add("connectionTimeout", timeout.ToString());
            config.Add("mode", gatewaySettings.Mode);

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);

            // # API call 
            // Invoke the DoDirectPayment method in service wrapper object  
            DoDirectPaymentResponseType paypalResponse = service.DoDirectPayment(wrapper);

            // Check for API return status
            return ParseResponse(service, paypalResponse);
        }

        private Response ParseResponse(PayPalAPIInterfaceServiceService service, DoDirectPaymentResponseType paypalResponse)
        {
            Response response = new Response();
            Hashtable hGatewayResponse = new Hashtable();

            HttpContext CurrContext = HttpContext.Current;

            response.GatewayRequestRaw = service.getLastRequest();
            response.GatewayResponseRaw = service.getLastResponse();

            switch (paypalResponse.Ack.ToString().ToLower())
            {
                case "success":
                    response.ResponseType = TransactionResponseType.Approved;
                    response.TransactionID = paypalResponse.TransactionID;
                    response.AuthCode = paypalResponse.CorrelationID;
                    hGatewayResponse.Add("PaymentStatus", paypalResponse.PaymentStatus.ToString());

                    if (paypalResponse.PendingReason != null)
                    {
                        hGatewayResponse.Add("PendingReason", paypalResponse.PendingReason.ToString());
                    }
                    break;
                case "failure":
                    response.ResponseType = TransactionResponseType.Denied;
                    break;
                default:
                    response.ResponseType = TransactionResponseType.Error;
                    break;

            }

            hGatewayResponse.Add("ACK", paypalResponse.Ack.ToString());

            response.AdditionalInfo = hGatewayResponse;

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

        public Response PerformVoidSettledRequest(Request request)
        {
            Response response = new Response();
            return response;
        }

        public static string UpdateCreditCardType(CreditCardType value)
        {
            string returnValue = "";
            if (value == CreditCardType.Visa) { returnValue = "Visa"; }
            if (value == CreditCardType.AmericanExpress) { returnValue = "Amex"; }
            if (value == CreditCardType.Discover) { returnValue = "Discover"; }
            if (value == CreditCardType.Mastercard) { returnValue = "MasterCard"; }

            return returnValue.ToUpper();
        }
    }
}
