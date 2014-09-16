using CSBusiness.ShoppingManagement;

namespace CSBusiness.Tax
{
    public interface ITaxCalculator
    {
        decimal Calculate(Cart cart);
        decimal CalculateFullPrice(Cart cart);
    }
}
