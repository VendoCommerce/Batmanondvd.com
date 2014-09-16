using System.Web;
using CSBusiness.Preference;
using CSCore.DataHelper;


namespace CSBusiness.Cache
{
    /// <summary>
    /// Summary description for SitePreference.
    /// </summary>
    public class SitePreferenceCache : BaseCache
    {
        public SitePreferenceCache(HttpContext context)
            : base(context)
        {
            if (context != null)
                InitLocalKeys();
        }


        /// <summary>
        /// Retrieve data that is going to be stored in cache
        /// </summary>
        /// <returns></returns>
        protected override object GetData()
        {
            SitePreference list = CSFactory.GetCartPrefrence();
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
