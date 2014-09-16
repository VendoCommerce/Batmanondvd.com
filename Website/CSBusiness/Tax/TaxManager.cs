using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Preference;
using System.Web;
using CSBusiness.Cache;

namespace CSBusiness.Tax
{
    public class TaxManager : ITaxCalculator
    {
        public decimal Calculate(ShoppingManagement.Cart cart)
        {
            decimal taxToReturn = 0;
            SitePreference list = CSFactory.GetCartPrefrence();
            decimal taxableAmount = cart.SubTotalTax;
            if (list.IncludeShippingCostInTaxCalculation)
            {
                taxableAmount += cart.ShippingCost;
				if (cart.ShippingMethod == Shipping.UserShippingMethodType.Rush)
				{
					taxableAmount += cart.RushShippingCost;
				}
            }

            //If this returns a value, it means country has states and we need to 
            //find tax for states
			if (cart.ShippingAddress.CountryId > 0)
			{
            //CodeReview By Sri on 09/15: Need to change TaxRegionCache Object
                TaxRegion countryRegion = null, stateRegion = null, zipRegion = null;

                //Comments on 11/2: pulling data from Cache object
                TaxregionCache cache = new TaxregionCache(HttpContext.Current);
                List<TaxRegion> taxRegions = (List<TaxRegion>)cache.Value;
				
				countryRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == 0 && string.IsNullOrEmpty(t.ZipCode));
                stateRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == cart.ShippingAddress.StateProvinceId && string.IsNullOrEmpty(t.ZipCode));
                zipRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == cart.ShippingAddress.StateProvinceId 
                    && t.ZipCode == cart.ShippingAddress.ZipPostalCode);

				//Tax regions are always returned by country
				//taxRegions = CSFactory.GetTaxByCountry(cart.ShippingAddress.CountryId);				
                if (zipRegion != null)
                {
                    taxToReturn = taxableAmount * zipRegion.Value / 100;
                }
                else if (stateRegion != null)
                {
                    taxToReturn = taxableAmount * stateRegion.Value / 100;
                }
			    else if (countryRegion != null)
                {
				    taxToReturn = taxableAmount * countryRegion.Value / 100;
                }
			}

            return Math.Round(taxToReturn, 2);
        }
        public decimal CalculateFullPrice(ShoppingManagement.Cart cart)
        {
            decimal taxToReturn = 0;
            SitePreference list = CSFactory.GetCartPrefrence();
            decimal taxableAmount = cart.SubTotalFullPrice;
            if (list.IncludeShippingCostInTaxCalculation)
            {
                taxableAmount += cart.ShippingCost;
                if (cart.ShippingMethod == Shipping.UserShippingMethodType.Rush)
                {
                    taxableAmount += cart.RushShippingCost;
                }
            }

            //If this returns a value, it means country has states and we need to 
            //find tax for states
            if (cart.ShippingAddress.CountryId > 0)
            {
                //CodeReview By Sri on 09/15: Need to change TaxRegionCache Object
                TaxRegion countryRegion = null, stateRegion = null, zipRegion = null;

                //Comments on 11/2: pulling data from Cache object
                TaxregionCache cache = new TaxregionCache(HttpContext.Current);
                List<TaxRegion> taxRegions = (List<TaxRegion>)cache.Value;

                countryRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == 0 && string.IsNullOrEmpty(t.ZipCode));
                stateRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == cart.ShippingAddress.StateProvinceId && string.IsNullOrEmpty(t.ZipCode));
                zipRegion = taxRegions.FirstOrDefault(t => t.CountryId == cart.ShippingAddress.CountryId && t.StateId == cart.ShippingAddress.StateProvinceId
                    && t.ZipCode == cart.ShippingAddress.ZipPostalCode);

                //Tax regions are always returned by country
                //taxRegions = CSFactory.GetTaxByCountry(cart.ShippingAddress.CountryId);
                if (zipRegion != null)
                {
                    taxToReturn = taxableAmount * zipRegion.Value / 100;
                }
                else if (stateRegion != null)
                {
                    taxToReturn = taxableAmount * stateRegion.Value / 100;
                }
                else if (countryRegion != null)
                {
                    taxToReturn = taxableAmount * countryRegion.Value / 100;
                }
            }
            return Math.Round(taxToReturn, 2);
        }
    }
}
