using CSBusiness.ShoppingManagement;

namespace CSBusiness.Shipping
{
    public interface IShippingCalculator
    {
		void Calculate(Cart cart, int prefID);
    }
}
