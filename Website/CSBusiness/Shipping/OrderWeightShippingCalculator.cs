using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;
using CSData;
using CSBusiness.Preference;

namespace CSBusiness.Shipping
{
    internal class OrderWeightShippingCalculator : IShippingCalculator
    {
		bool _isRushShipping;
		internal OrderWeightShippingCalculator(bool isRushShipping)
		{
			_isRushShipping = isRushShipping;
		}

		public void Calculate(Cart cart, int prefID)
        {
			List<ShippingOrderValue> shippingSettings = ShippingDAL.GetShippingOrderValue(ShippingOptionType.Weight, _isRushShipping, prefID).OrderBy(s => s.OrderTotal).ToList();

            bool withQuantity = false;
            decimal cartWeight;
            SitePreference sp = CSFactory.GetCacheSitePref();
           if (!sp.AttributeValuesLoaded)
           {
               sp.LoadAttributeValues();   
           }
            if (sp.AttributeValues.ContainsKey("qtyinweightbasedshipping"))
            {
                if (sp.AttributeValues["qtyinweightbasedshipping"].BooleanValue)
                {
                    withQuantity = true;
                }
            }
            if (withQuantity)
            {
                cartWeight = cart.CartItems.Sum(c => c.Weight * c.Quantity);

            }
            else
            {
                cartWeight = cart.CartItems.Sum(c => c.Weight);
            }
           
            int totalShippingSettings = shippingSettings.Count;
            for (int i = 0; i < totalShippingSettings; i++)
            { 
                ShippingOrderValue currentSetting = shippingSettings[i];
                if(cartWeight <= currentSetting.OrderTotal ||
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
