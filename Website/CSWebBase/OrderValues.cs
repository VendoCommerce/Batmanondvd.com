using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Resolver;
using CSBusiness.OrderManagement;
using CSBusiness.Cache;
using CSBusiness;
using System.Web.UI.WebControls;
using CSBusiness.Attributes;
using CSBusiness.ShoppingManagement;

namespace CSWebBase
{
    public class OrderValues
    {
        public static decimal GetShippingCharge(Order order)
        {
            Sku shippingSku = GetShippingSku(order);
            if (shippingSku != null)
                return shippingSku.FullPrice;
            return 0;
        }

        public static Sku GetShippingSku(Order order)
        {
            return  order.SkuItems.First<Sku>(x => { return x.SkuCode == "Shipping"; });
        }

        public static Sku GetGiftSku(Cart cart)
        {
            return cart.CartItems.First<Sku>(x => { return x.SkuCode == "Shipping"; });
        }

        public static decimal GetSubTotal(Order order)
        {
            return  order.SubTotal-GetShippingCharge(order);
        }

        private static int TotalSkus(Cart cart)
        {
            return cart.CartItems.Count<Sku>(x => {return x.SkuCode !="Shipping" && x.SkuCode !="Gift";});
        }

        public static void SetGiftWrap(Cart cart)
        {
            Sku giftSku = GetGiftSku(cart);
            int totalSkus = TotalSkus(cart);
            if (giftSku != null)
            cart.AddOrUpdate(giftSku.SkuId,totalSkus,true,true,false);
        }

    }
}
