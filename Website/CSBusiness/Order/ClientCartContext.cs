using System;
using CSBusiness.CustomerManagement;
using CSBusiness.Payment;
using CSBusiness.ShoppingManagement;
using System.Collections.Generic;
using CSBusiness.Attributes;

namespace CSBusiness
{
    [Serializable]
    public class ClientCartContext
    {

        public ClientCartContext(){}

        public string RequestParam { get; set; }
        public string ReferalParam { get; set; }
        public string IpAddress { get; set; }
        public int OrderId { get; set; }
        public int CustomerGuid { get; set; }
        public int VersionId { get; set; }
        public int CartAbandonmentId { get; set; }
        public Dictionary<string, AttributeValue> OrderAttributeValues { get; set; }

        private bool tntVarsLoaded = false;
        public bool TnTVarsLoaded
        {
            get
            {
                return tntVarsLoaded;
            }
            set
            {
                tntVarsLoaded = value;
            }
        }

        public Customer CustomerInfo
        {
            get;
            set;
        }

        public PaymentInformation PaymentInfo
        {
            get;
            set;
        }

        public Cart CartInfo
        {
            get;
            set;
        }

        public void ResetData()
        {
            
                this.CustomerInfo = null;
                this.PaymentInfo = null;
                this.CartInfo.DiscountCode = string.Empty;
                this.OrderAttributeValues = null;          
        }

        public void EmptyData()
        {

            this.CustomerInfo = null;
            this.PaymentInfo = null;
            this.CartInfo = null;
            //this.CartInfo.DiscountCode = string.Empty;
            this.OrderId = 0;
          
        }
    }
}
