using System;
using System.Collections.Generic;
using System.Linq;
using CSBusiness.Resolver;
using CSBusiness.CustomerManagement;
using CSBusiness.Shipping;
using CSBusiness.Tax;
using CSBusiness.Preference;
using CSBusiness.Discount;
using CSBusiness.Total;


namespace CSBusiness.ShoppingManagement
{
    [Serializable]
    public class Cart
    {
        private List<Sku> _items;
        private bool showQty;
        private decimal taxAmount = 0, subTotalAmount = 0, subTotalFullAmount = 0, subTotalTaxAmount = 0, discountAmout = 0, taxFullPrice;
        private string discountCode = String.Empty;
        private Address shippingAddress;        
        #region properties

        public UserShippingMethodType ShippingMethod
        {
            get;
            set;
        }

        public bool ShowQuantity
        {
            get { return showQty; }
            set { showQty = false; }
        }
        public int ItemCount
        {
            get { return _items.Count; }
        }

        public List<Sku> CartItems
        {
            get { return this._items; }
        }

        public decimal TaxCost
        {
            get { return taxAmount; }

        }

        public decimal ShippingCost
        {
            get;
            set;
        }

        public string DiscountCode
        {
            get { return discountCode; }
            set { discountCode = value; }

        }

        public decimal DiscountAmount
        {
            get { return discountAmout; }
            set { discountAmout = value; }
        }

        public decimal RushShippingCost
        {
            get;
            set;
        }

        public decimal AdditionalShippingCharge
        {
            get;
            set;
        }

        public decimal SubTotal
        {
            get
            {
                if (subTotalAmount == 0)
                    this.CalculateTotal();

                return subTotalAmount;
            }
        }

        public decimal SubTotalTax
        {
            get
            {
                if (subTotalTaxAmount == 0)
                    this.CalculateTotal();

                return subTotalTaxAmount;
            }
        }

        public decimal SubTotalFullPrice
        {
            get
            {
                if (subTotalFullAmount == 0)
                    this.CalculateTotal();

                return subTotalFullAmount;
            }
        }

        public decimal Total
        {
            get
            {
                decimal total = (this.SubTotal + this.TaxCost + this.ShippingCost + this.AdditionalShippingCharge);
                if (ShippingMethod == UserShippingMethodType.Rush)
                    total += RushShippingCost;

                if (this.DiscountAmount > 0)
                {
                    if (total >= DiscountAmount)
                        total = total - DiscountAmount;
                    else
                        total = 0;
                }

                return total;
            }
        }

        public Address ShippingAddress
        {
            get { return shippingAddress; }
            set { shippingAddress = value; }
        }

        /// <summary>
        /// Specify additional shipping charge key. 
        /// </summary>
        public string ShippingChargeKey
        {
            get;
            set;
        }

        #endregion properties

        public Cart()
        {
            _items = new List<Sku>();
            ShippingMethod = UserShippingMethodType.Standard;
        }


        #region Methods

        public void AddItem(Sku sku, int quantity, bool visible, bool isUpsell)
        {
            sku.Quantity = quantity;
            sku.IsUpSell = isUpsell;
            sku.Visible = visible;
            this.CartItems.Add(sku);
        }

        /// <summary>
        /// Adds Sku only if it does not already exist in cart.
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="quantity"></param>
        /// <param name="visible"></param>
        /// <param name="isUpsell"></param>
        public void AddItem(int skuId, int quantity, bool visible, bool isUpsell)
        {
            if (!SkuExists(skuId))
            {
                Sku Item = CSResolve.Resolve<ISkuService>().GetSkuByID(skuId);
                Item.Quantity = quantity;
                Item.IsUpSell = isUpsell;
                Item.Visible = visible;
                this.CartItems.Add(Item);
            }
        }

        /// <summary>
        /// Adds Sku is cart does not already have it, or updates if it does.
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="quantity"></param>
        /// <param name="visible"></param>
        /// <param name="isUpsell"></param>
        /// <param name="quantityIncrement">True to add *on* to the quantity of Sku, false to treat quantity as total (for update only). Does not get used for new Sku.</param>
        public void AddOrUpdate(int skuId, int quantity, bool visible, bool isUpsell, bool quantityIncrement)
        {
            if (!SkuExists(skuId))
            {
                Sku Item = CSResolve.Resolve<ISkuService>().GetSkuByID(skuId);
                Item.Quantity = quantity;
                Item.IsUpSell = isUpsell;
                Item.Visible = visible;
                this.CartItems.Add(Item);
            }
            else
            {
                int skuIndex = this.CartItems.IndexOf(this.CartItems.Single(x =>
                {
                    return x.SkuId == skuId;
                }));

                this.CartItems[skuIndex].Quantity = quantityIncrement ? this.CartItems[skuIndex].Quantity += quantity : quantity;
                this.CartItems[skuIndex].Visible = visible;
                this.CartItems[skuIndex].IsUpSell = isUpsell;
            }
        }

        public bool SkuExists(int skuId)
        {
            Sku Item = this.CartItems.FirstOrDefault(x => x.SkuId == skuId);
            if (Item != null)
            {

                return true;
            }
            return false;
        }

        public void RemoveSku(int skuId)
        {
            Sku Item = this.CartItems.FirstOrDefault(x => x.SkuId == skuId);
            if (Item != null)
            {
                this.CartItems.Remove(Item);
            }

        }

        /// <summary>
        /// Deprecated. Use RemoveSku and then call Compute.
        /// </summary>
        /// <param name="skuId"></param>        
        public void UpdateSku(int skuId)
        {
            Sku Item = this.CartItems.FirstOrDefault(x => x.SkuId == skuId);
            if (Item != null)
            {
                this.CartItems.Remove(Item);
            }
            Compute();
        }

        /// <summary>
        /// Deprecated. Use RemoveSku.
        /// </summary>
        /// <param name="skuId"></param>
        public void UpdateSkuOnly(int skuId)
        {
            Sku Item = this.CartItems.FirstOrDefault(x => x.SkuId == skuId);
            if (Item != null)
            {
                this.CartItems.Remove(Item);
            }

        }

        public void Compute()
        {
            CalculateTotal();
            CalculateShipping();
            CalculateTax();
            CalculateDiscount();
        }

        private void CalculateShipping()
        {
            this.ShippingCost = 0;
            CSResolve.Resolve<IShippingCalculator>().Calculate(this, 0);
        }

        private void CalculateTax()
        {
            this.taxAmount = 0;
            this.taxFullPrice = 0;
            if (shippingAddress != null)
            {
                taxAmount = CSResolve.Resolve<ITaxCalculator>().Calculate(this);
                taxFullPrice = CSResolve.Resolve<ITaxCalculator>().CalculateFullPrice(this);
            }
        }

        private void CalculateTotal()
        {
            CSResolve.Resolve<ITotalCalculator>().CalculateTotal(this, out subTotalAmount, out subTotalFullAmount, out subTotalTaxAmount);
        }

        public bool CalculateDiscount()
        {
            this.DiscountAmount = 0;
            return CSResolve.Resolve<IDiscountCalculator>().CalculateDiscount(this);
        }

        #endregion Methods

        #region NewMethods

        public decimal TaxFullPrice
        {
            get { return taxFullPrice; }

        }
        #endregion
    }



}
