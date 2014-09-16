using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CSPaymentProvider
{

    public struct VOIDRequest
    {
        public string TxRefNum ;
        public string TxRefIdx ;
        public int OrderID ;
    }

    public class Request
    {
        #region Public Properties

        public CreditCardType CardType { get; set; }
        public string CardCvv { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address2 { get; set; }
        public string CompanyName { get; set; }
        public string Duty { get; set; }
        public string Freight { get; set; }
        public string TaxExempt { get; set; }
        public string Tax { get; set; }
        public string ShippingTotal { get; set; }
        public string SubTotal { get; set; }
        public string PoNumber { get; set; }
        public string AuthCode { get; set; }
        public string TransactionID { get; set; }
        public string ECheckType { get; set; }
        public string BankAcctName { get; set; }
        public string BankName { get; set; }
        public string BankAcctType { get; set; }
        public string BankAcctNumber { get; set; }
        public string BankAbaCode { get; set; }
        public string CurrencyCode { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToZipCode { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToCompany { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToFirstName { get; set; }
        public string TransactionDescription { get; set; }
        public string InvoiceNumber { get; set; }
        public string Email { get; set; }
        public string CustomerTaxID { get; set; }
        public string IPAddress { get; set; }
        public string CustomerID { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string RequestID { get; set; }
        public PaymentRequestType RequestType { get; set; }
        public PaymentMethodType MethodType { get; set; }
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public Hashtable AdditionalInfo { get; set; }
        public List<PaymentSku> SkuItems { get; set; }

        #endregion

    }

    public enum CreditCardType
    {
        Visa,
        Mastercard,
        AmericanExpress,
        Discover
    }

    public enum PaymentMethodType
    {
        CreditCard,
        Check
    }

    public enum PaymentRequestType
    {
        auth,
        sale,
        Credit
    }

    /// <summary>
    /// Use a smaller copy just for use within this project. (CSBusiness already depends CSPaymentProvider, so we cannot make them reverse dependent.)
    /// </summary>
    public class PaymentSku
    {
        public string Title { get; set; }
        public string SkuCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal FullPrice { get; set; }
        public decimal InitialPrice { get; set; }
        public int Quantity { get; set; }
    }
}
