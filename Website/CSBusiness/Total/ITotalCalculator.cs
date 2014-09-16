using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Total
{
    public interface ITotalCalculator
    {
        void CalculateTotal(Cart cart, out decimal subTotalAmount, out decimal subTotalFullAmount, out decimal subTotalTaxAmount);
    }
}
