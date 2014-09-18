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
            if (order.SkuItems.Count<Sku>(x => { return x.SkuCode == "Shipping"; }) > 0)
                return order.SkuItems.First<Sku>(x => { return x.SkuCode == "Shipping"; });
            return null;
        }

        public static Sku GetGiftSku(Cart cart)
        {
            if (cart.CartItems.Count<Sku>(x => { return x.SkuCode == "Gift"; })>0)
            return cart.CartItems.First<Sku>(x => { return x.SkuCode == "Gift"; });
            return null;
        }

        public static decimal GetSubTotal(Order order)
        {
            return  order.SubTotal-GetShippingCharge(order);
        }

        private static int TotalSkus(Cart cart)
        {
            return cart.CartItems.Count<Sku>(x => {return x.SkuCode !="Shipping" && x.SkuCode !="Gift";});
        }

        public static void SetGiftWrap(ClientCartContext cartContext)
        {
            Sku giftSku = GetGiftSku(cartContext.CartInfo);
            int totalSkus = TotalSkus(cartContext.CartInfo);
            if (giftSku != null)
            {
                cartContext.CartInfo.AddOrUpdate(giftSku.SkuId, totalSkus, true, true, false);
                cartContext.CartInfo.Compute();
                new OrderManager().UpdateOrderAfterUpSell(cartContext.OrderId,cartContext.CartInfo);

            }
        }

    }
}
