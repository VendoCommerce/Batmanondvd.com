using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.ShoppingManagement;

namespace CSBusiness.Discount
{
    public interface IDiscountCalculator
    {
        bool CalculateDiscount(Cart cart);
    }
}
