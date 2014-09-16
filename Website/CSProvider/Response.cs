using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CSPaymentProvider
{
    public class Response
    {
        public TransactionAvsResponse AvsResponse { get; set; }
        public string GatewayRequestRaw { get; set; }
        public string GatewayResponseRaw { get; set; }
        public string TimeStamp { get; set; }
        public string TransactionID { get; set; }
        public string AuthCode { get; set; }
        public string MerchantDefined1 { get; set; }
        public string MerchantDefined2 { get; set; }
        public TransactionResponseType ResponseType { get; set; }
        public TransactionCvvResponse CvvResponse { get; set; }
        public string ReasonText { get; set; }
        public Hashtable AdditionalInfo { get; set; }
                
    }

    public enum TransactionResponseType
    {
        Approved,
        Denied,
        Error
    }

    public enum TransactionAvsResponse {
        Match,
        NoMatch,
        NA,
        Error
    }

    public enum TransactionCvvResponse
    {
        Match,
        NoMatch,
        NA,
        Error
    }

}
