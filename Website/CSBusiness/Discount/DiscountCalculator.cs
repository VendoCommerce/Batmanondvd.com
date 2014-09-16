using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;
using CSBusiness.Preference;

namespace CSBusiness.Discount
{
    public class DiscountCalculator : IDiscountCalculator
    {
        public bool CalculateDiscount(Cart cart)
        {
            cart.DiscountAmount = 0;

            if (cart.DiscountCode != null)
            {
                SitePreference prefObject = CSFactory.GetCacheSitePref();
                List<CouponInfo> couponList = prefObject.CouponItems;
                CouponInfo foundItem = couponList.FirstOrDefault(x => x.Title.ToLower() == cart.DiscountCode.ToLower());
                if (foundItem != null)
                {
                    if ((int)foundItem.DiscountType == 1)
                    {
                        if (foundItem.IncludeShipping)
                            cart.DiscountAmount = Math.Round(cart.Total * (foundItem.Discount / 100), 2);
                        else
                            cart.DiscountAmount = Math.Round(cart.SubTotal * (foundItem.Discount / 100), 2);
                    }
                    else if ((int)foundItem.DiscountType == 2)
                    {
                        if (foundItem.IncludeShipping)
                        {
                            if (cart.Total >= foundItem.TotalAmount)
                                cart.DiscountAmount = foundItem.Discount;
                            else
                                cart.DiscountAmount = 0;
                         }  
                        else
                        {
                           if (cart.SubTotal >= foundItem.TotalAmount)
                                cart.DiscountAmount = foundItem.Discount;
                            else
                                cart.DiscountAmount = 0;
                        
                        }
                        
                    }
                    else if ((int)foundItem.DiscountType == 5)
                    {
                        cart.DiscountAmount = cart.ShippingCost;
                        if (foundItem.Discount > 0)
                        {
                            if (foundItem.IncludeShipping)
                                cart.DiscountAmount += Math.Round(cart.Total * (foundItem.Discount / 100), 2);
                            else
                                cart.DiscountAmount += Math.Round(cart.SubTotal * (foundItem.Discount / 100), 2);
                        }
                        else if (foundItem.TotalAmount > 0)
                        {
                            cart.DiscountAmount += foundItem.TotalAmount;
                        }
                    }
                    else //item level coupon
                    {
                        List<CouponItems> items = foundItem.ItemsDiscount;
                        foreach (CouponItems itemInfo in items)
                        {
                            if (itemInfo != null)
                            {

                                if (cart.CartItems.FirstOrDefault(x => x.SkuId == itemInfo.SkuId) != null)
                                {
                                    Sku RelatedItem = cart.CartItems.FirstOrDefault(x => x.SkuId == itemInfo.RelatedSkuId);
                                    if (RelatedItem != null)
                                    {
                                        if ((int)itemInfo.DiscountType == 1)
                                            cart.DiscountAmount = Math.Round(RelatedItem.InitialPrice * (itemInfo.DiscountAmount / 100), 2);
                                        else
                                        {
                                            if (cart.Total >= itemInfo.DiscountAmount)
                                                cart.DiscountAmount = itemInfo.DiscountAmount;
                                            else
                                                cart.DiscountAmount = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (cart.DiscountAmount > 0)
                    return true;
            }

            return false;
        }
    }
}
