using System;

namespace CSBusiness.Payment
{
    [Serializable]
    public class PaymentInformation
    {
        public decimal Amount { get; set; }
        public string AuthorizationCode { get; set; }
        public string ConfirmationCode { get; set; }
        public string TransactionCode { get; set; }
        public string CreditCardCSC { get; set; }
        public DateTime CreditCardExpired { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardNumber { get; set; }
        public int CreditCardType { get; set; }
     
    }
}






