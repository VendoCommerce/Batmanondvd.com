using System.Web;
using CSBusiness.Preference;
using System.Collections.Generic;
using CSCore.DataHelper;


namespace CSBusiness.Cache
{
    /// <summary>
    /// Summary description for OrgUnitCache.
    /// </summary>
    public class TaxregionCache : BaseCache
    {
        public TaxregionCache(HttpContext context)
            : base(context)
        {
            if (context != null)
                InitLocalKeys();
        }


        /// <summary>
        /// Retrieve data that is going to be stored in cache
        /// </summary>
        /// <returns>OrgUnitTable (must cast to Cornerstone.Data.Common.OrgUnitTable)</returns>
        protected override object GetData()
        {
            List<TaxRegion> list = CSFactory.GetTaxByCountry(-1);
            return list;
        }

        /// <summary>
        /// Generate file dependency keys, the first key is used as cache key by default
        /// </summary>
        protected override void InitLocalKeys()
        {
            localKeys = new string[1];
            localKeys[0] = "Site";
            cacheKey = localKeys[0].ToString() + "_" + globalKey;
        }


    }
}
