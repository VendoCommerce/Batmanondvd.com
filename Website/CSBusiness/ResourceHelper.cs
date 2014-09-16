using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSBusiness.Cache;
using System.Web;

namespace CSBusiness
{
    public class ResourceHelper
    {
        public static string GetResoureValue(string keyName)
        {
            string countryName = String.Empty;
            ResourceCache cache = new ResourceCache(HttpContext.Current);
            Dictionary<string, string> list = (Dictionary<string, string>) cache.Value;
            if (list.ContainsKey(keyName))
			{
				return list[keyName].ToString();
			}
			else
				return String.Empty;
        }

        public static void RemoveCache()
        {
            ResourceCache cache = new ResourceCache(HttpContext.Current);
            cache.RemoveCacheKey();
        }
    }
}
