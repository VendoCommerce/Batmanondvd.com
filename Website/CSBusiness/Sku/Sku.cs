using System;
using System.Collections;
using System.Data;
using CSBusiness.Attributes;

namespace CSBusiness
{
    [Serializable]
    public class Sku : Attributes.ObjectAttribute
    {
        public const string objectName = "SKU";

        public int SkuId { get; set; }
        public string Title { get; set; }
        public string SkuCode { get; set; }
        public string OfferCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string EmailDescription { get; set; } 
        public int? CategoryId { get; set; }
        public int? StockQty { get; set; }
        public decimal FullPrice { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal Weight { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsTaxable { get; set; }
        public decimal TaxableFullAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Quantity { get; set; }
        public string SkuTitleCode { get; set; }
        public bool Visible { get; set; }
        public string ImagePath { get; set; }

        //order Side
        public decimal TotalPrice { get; set; }
        public bool IsUpSell { get; set; }
        public int OrderId { get; set; }

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
                return SkuId;
            }
        }
    }
}
