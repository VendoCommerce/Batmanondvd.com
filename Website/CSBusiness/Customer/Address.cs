using CSBusiness.Security;
using System;


namespace CSBusiness.CustomerManagement
{
    /// <summary>
    /// Represents an address
    /// </summary>
    [Serializable]
    public  class Address 
    {
        #region Properties
        /// <summary>
        /// Gets or sets the address identifier
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the address is billing or shipping
        /// </summary>
        public bool IsBillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [SensitiveDataAttribute]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [SensitiveDataAttribute]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [SensitiveDataAttribute]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [SensitiveDataAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        [SensitiveDataAttribute]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the company
        /// </summary>
        [SensitiveDataAttribute]
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        [SensitiveDataAttribute]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        [SensitiveDataAttribute]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        [SensitiveDataAttribute]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int StateProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        [SensitiveDataAttribute]
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the State Name
        /// </summary>
        [SensitiveDataAttribute]
        public string StateProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the Country code
        /// </summary>
        public string CountryCode { get; set; }
        #endregion 

        [IsEncryptedAttribute]
        public bool IsEncrpyed { get; set; }


    }

}
