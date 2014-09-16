using System.Collections.Generic;
using System.Web;
using CSCore.DataHelper;


namespace CSBusiness.Cache
{
    /// <summary>
    /// Summary description for StateCache.
    /// </summary>
    public class StateCache : BaseCache
    {
        public StateCache(HttpContext context)
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
            List<StateProvince> list = StateManager.GetAllStates(0);
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
