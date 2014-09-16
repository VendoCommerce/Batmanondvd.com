using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.Utils;
using CSData;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Shipping
{
    internal class SkuBasedShippingCalculator : IShippingCalculator
    {
		bool _isRushShipping;
		internal SkuBasedShippingCalculator(bool isRushShipping)
		{
			_isRushShipping = isRushShipping;
		}

        public void Calculate(Cart cart, int prefID)
        {
			List<SkuShipping> skuShippingItems = ShippingDAL.GetSkuShipping(_isRushShipping, prefID);
            decimal shippingCost = 0;

            int totalShippingSettings = skuShippingItems.Count;
            for (int i = 0; i < totalShippingSettings; i++)
            {
                SkuShipping currentSetting = skuShippingItems[i];
                if (cart.CartItems.Exists(c => c.SkuId == currentSetting.SkuId))
                {
                    bool withQuantity = false;
                    Sku s = new Sku();
                    s.SkuId = currentSetting.SkuId;
                    s.LoadAttributeValues();
                    if (s.AttributeValues.ContainsKey("shippingwithquantity"))
                    {
                        if (s.AttributeValues["shippingwithquantity"].BooleanValue)
                        {
                            withQuantity = true;
                        }
                    }
                    if (withQuantity)
                    {
                        foreach (Sku st in cart.CartItems)
                        {
                            if(st.SkuId == currentSetting.SkuId)
                            {
                                shippingCost += currentSetting.Cost * st.Quantity;
                            }
                        }
                        
                    }
                    else
                    {
                        shippingCost += currentSetting.Cost;
                    }
                    
                }
            }

            // add additional shipping charge by specified key
            ShippingCharge shippingCharge = ShippingDAL.GetShippingCharge(prefID, cart.ShippingChargeKey);
            if (shippingCharge != null)
                cart.AdditionalShippingCharge = shippingCharge.Cost;           

            if (_isRushShipping)
            {
                cart.RushShippingCost = shippingCost;
            }
            else
                cart.ShippingCost = shippingCost;
        }
    }
}
