using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Web
{
    [Serializable]
    public class AjaxTotalsResponse
    {   
        public decimal? subTotal;
        public decimal? shippingHandling;
        public decimal? total;
        public decimal? tax;
        public decimal? discount;        
     
        public string SubTotalDisplay
        {
            get
            {                
                return GetDisplayString(subTotal);
            }
        }

       
        public string ShippingHandlingDisplay
        {
            get
            {                
                return GetDisplayString(shippingHandling);
            }
        }

        public string TotalDisplay
        {
            get
            {                
                return GetDisplayString(total);
            }
        }

        public string TaxDisplay
        {
            get
            {
                return GetDisplayString(tax);
            }
        }

        public string DiscountDisplay
        {
            get
            {
                return GetDisplayString(discount);
            }
        }

        public static string GetDisplayString(decimal? value)
        {
            return value.HasValue ? value.Value.ToString("C") : string.Empty;
        }
    }
}
