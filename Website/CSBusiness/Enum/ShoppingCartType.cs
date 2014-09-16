
namespace CSBusiness.ShoppingManagement
{
    /// <summary>
    /// Represents the tax display type enumeration
    /// </summary>
    public enum ShoppingCartType : int
    {
        SingleCheckout = 1,      
        ShippingCreditCheckout = 2,
        BillingShippingCreditCheckout = 2,
        SingleCheckout_AddProduct = 3
    }
}