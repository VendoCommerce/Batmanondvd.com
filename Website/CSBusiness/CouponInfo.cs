using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Coupon;

namespace CSBusiness
{
    public class CouponInfo
    {

        public int CouponId { get; set; }
        public string Title { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Active { get; set; }
        public int CreateDate { get; set; }
        public CouponTypeEnum DiscountType { get; set; }
        public List<CouponItems> ItemsDiscount { get; set; }
        public bool IncludeShipping { get; set; }
    }

    public class CouponItems
    {
        public int CouponId { get; set; }
        public int SkuId { get; set; }
        public int RelatedSkuId { get; set; }
        public decimal DiscountAmount { get; set; }
        public CouponTypeEnum DiscountType { get; set; }
    }
}

