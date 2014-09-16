using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSData;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Shipping
{
    internal class OrderValueShippingCalculator : IShippingCalculator
    {
		bool _isRushShipping;
		internal OrderValueShippingCalculator(bool isRushShipping)
		{
			_isRushShipping = isRushShipping;
		}

        public void Calculate(Cart cart, int prefID)
        {
			List<ShippingOrderValue> shippingSettings = ShippingDAL.GetShippingOrderValue(ShippingOptionType.TotalAmount, _isRushShipping, prefID).OrderBy(s => s.OrderTotal).ToList();

            decimal cartSubtotal = cart.SubTotal;
        

            int totalShippingSettings = shippingSettings.Count;
            for (int i = 0; i < totalShippingSettings; i++)
            { 
                ShippingOrderValue currentSetting = shippingSettings[i];
				if (cartSubtotal <= currentSetting.OrderTotal ||
					i == totalShippingSettings - 1)
                {
                    // add additional shipping charge by specified key
                    ShippingCharge shippingCharge = ShippingDAL.GetShippingCharge(prefID, cart.ShippingChargeKey);
                    if (shippingCharge != null)
                        cart.AdditionalShippingCharge = shippingCharge.Cost; 

                    if (_isRushShipping)
                    {
                        cart.RushShippingCost = currentSetting.Cost;
                    }
                    else
                        cart.ShippingCost = currentSetting.Cost;
                    break;
                }
            }
        }
    }
}
