using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSData;

namespace CSBusiness.Shipping
{
    internal class FlatShippingCalculator : IShippingCalculator
    {
		bool _isRushShipping;
		internal FlatShippingCalculator(bool isRushShipping)
		{
			_isRushShipping = isRushShipping;
		}

		public void Calculate(ShoppingManagement.Cart cart, int prefID)
        {
			ShippingPref shippingGetShippingPref = ShippingDAL.GetShippingPref().Where(p => p.PrefId == prefID).FirstOrDefault();
            if (shippingGetShippingPref != null)
            {
                if (cart.SubTotal > 0) //Sri: Reset shipping calculation if there are no items in the cart
                {                    
                    // add additional shipping charge by specified key
                    ShippingCharge shippingCharge = ShippingDAL.GetShippingCharge(prefID, cart.ShippingChargeKey);
                    if (shippingCharge != null)
                        cart.AdditionalShippingCharge = shippingCharge.Cost;

                    if (_isRushShipping)
                    {
                        cart.RushShippingCost = shippingGetShippingPref.RushShippingCost;
                    }
                    else
                        cart.ShippingCost = (shippingGetShippingPref.flatShipping ?? 0);
                }
            }
            else
            {
                throw new InvalidOperationException("Missing shipping preferences");
            }
        }
    }
}
