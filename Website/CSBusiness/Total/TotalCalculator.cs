using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Total
{
    public class TotalCalculator : ITotalCalculator
    {
        public void CalculateTotal(Cart cart, out decimal subTotalAmount, out decimal subTotalFullAmount, out decimal subTotalTaxAmount)
        {
            subTotalAmount = 0M;
            subTotalFullAmount = 0M;
            subTotalTaxAmount = 0M;

            foreach (Sku item in cart.CartItems)
            {
                subTotalAmount += (item.InitialPrice * item.Quantity);
                subTotalFullAmount += (item.FullPrice * item.Quantity);
                subTotalTaxAmount += (item.TaxableFullAmount * item.Quantity);
            }
        }
    }
}
