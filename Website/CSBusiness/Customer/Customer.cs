using System;
using CSBusiness.Tax;
using CSBusiness.Resolver;
using CSBusiness.Security;

namespace CSBusiness.CustomerManagement
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    [Serializable]
    public  class Customer 
    {
        #region Fields

        private Address billingAddress;
        private Address shippingAddress;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [SensitiveDataAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        [SensitiveDataAttribute]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt key
        /// </summary>
        public string SaltKey { get; set; }

        /// <summary>
        /// Gets or sets the salt key
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the billing address identifier
        /// </summary>
        public int BillingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
        public int ShippingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
        public int UserTypeId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the tax display type identifier
        /// </summary>
        public int TaxDisplayTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is tax exempt
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is administrator
        /// </summary>
        public bool IsAdmin { get; set; }

       
        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        [SensitiveDataAttribute]
        public string FirstName { get; set; }

        [SensitiveDataAttribute]
        public string LastName { get; set; }
        

        /// <summary>
        /// Gets or sets the date and time of customer registration
        /// </summary>
        public DateTime RegistrationDate { get; set; }


        #endregion

        #region Custom Properties

         //<summary>
         //Gets the last billing address
         //</summary>
        [SensitiveDataAttribute]
        public Address BillingAddress
        {
            get
            {
                if (billingAddress == null)
                    billingAddress = CSResolve.Resolve<ICustomerService>().GetAddressById(this.BillingAddressId);

               return billingAddress;
            }
            set
            {
                billingAddress = value;
            }
        }

        //<summary>
        //Gets the  shipping address
        //</summary>
        [SensitiveDataAttribute]
        public Address ShippingAddress
        {
            get
            {
                if (shippingAddress == null)
                    shippingAddress = CSResolve.Resolve<ICustomerService>().GetAddressById(this.ShippingAddressId);

                return shippingAddress;
            }
            set
            {
                shippingAddress = value;
            }
        }
       
        /// <summary>
        /// Gets the customer full name
        /// </summary>
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(this.FirstName))
                    return this.LastName;
                else
                    return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
      

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [SensitiveDataAttribute]
        public string PhoneNumber
        {
            get;

            set;

        }


        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId
        {
            get;
            set;

        }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int StateProvinceId
        {
            get;
            set;
        }

        [IsEncryptedAttribute]
        public bool IsEncrpyed { get; set; }

        #endregion


    }
}