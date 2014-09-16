using CSBusiness.Shipping;
using CSData;
using System;
using System.Collections.Generic;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Preference
{
    [Serializable]
    public class SitePreference : Attributes.ObjectAttribute
    {
        public const string objectName = "SitePref";

        public int PrefID { get; set; }
		public int ShippingPrefID { get; set; }
		public int RushShippingPrefID { get; set; }
        public ShippingOptionType ShippingOptionId { get; set; }
        public ShippingOptionType RushShippingOptionID { get; set; }
        public bool IncludeShippingCostInTaxCalculation { get; set; }
        public bool GeoLocationService { get; set; }
        public decimal? FlatShippingCost {get;set;}
        public decimal? RushShippingCost { get; set; }
        public bool? IncludeRushShipping { get; set; }
        public string Culture {get;set;}
        public DateTime PathOrderDate { get; set; }
        public OrderProcessTypeEnum OrderProcessType { get; set; }
        public List<Version> VersionItems { get; set; }
        public List<CouponInfo> CouponItems { get; set; }
        public bool PaymentGatewayService { get; set; }
        public bool FulfillmentHouseService { get; set; }

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
                return PrefID;
            }
        }
    }
}
