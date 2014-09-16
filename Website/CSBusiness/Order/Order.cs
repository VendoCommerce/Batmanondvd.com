using System;
using CSBusiness.Resolver;
using CSBusiness.CustomerManagement;
using System.Collections.Generic;
using CSBusiness.Payment;

namespace CSBusiness.OrderManagement
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    [Serializable]
    public class Order : Attributes.ObjectAttribute
    {
        public const string objectName = "Order";

        #region Fields

        private Customer customerInfo;
       
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int OrderId { get; set; }

        
        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the SubTotal
        /// </summary>
        public decimal FullPriceSubTotal { get; set; }

        /// <summary>
        /// Gets or sets the SubTotal
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the Tax
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the ShippingCost
        /// </summary>
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the ShippingCost
        /// </summary>
        public decimal RushShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the AdditionalShippingCharge
        /// </summary>
        public decimal AdditionalShippingCharge { get; set; }

        /// <summary>
        /// Gets or sets the OrderStatusId
        /// </summary>
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Gets or sets the OrderStatusId
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the SubTotal
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>

        public string VersionName { get; set; }

      
        /// <summary>
        /// Gets or sets the DiscountCode
        /// </summary>
        /// 

        public string DiscountCode { get; set; }

        /// <summary>
        /// Gets or sets the ShippingCost
        /// </summary>
        public decimal DiscountAmount { get; set; }

       
        public int BillingAddressId { get; set; }

        public string IpAddress { get; set; }

        public List<Sku> SkuItems { get; set; }

        public PaymentInformation CreditInfo { get; set; }

        public List<OrderCustomField> CustomFiledInfo { get; set; }

        #endregion

        #region Custom Properties

       
        public Customer CustomerInfo
        {
            get
            {
                if (customerInfo == null)
                     customerInfo = CSResolve.Resolve<ICustomerService>().GetCustomerDetails(this.CustomerId);

                 return customerInfo;

            }
            set
            {
                customerInfo = value;
            
            }
         
        }

        // attribute properties        
        public override string ObjectName
        {
            get
            {
                return objectName;
            }
        }

        public override int ItemId
        {
            get
            {
                return OrderId;
            }
        }
       #endregion

        #region NewColumns

        /// <summary>
        /// Gets or sets the FullPriceTax
        /// </summary>
        public decimal FullPriceTax { get; set; }

        /// <summary>
        /// Gets or sets the Friendly Order Status
        /// </summary>
        public string OrderStatus { get; set; }
        #endregion


    }
}