using System;
using System.Web.Caching;
using System.Web;




namespace CSBusiness.Cache
{
	/// <summary>
	/// Base class for all specific cache class.
	/// </summary>
	public class BaseCache
	{

		protected string globalKey;
		protected string[] localKeys;
		protected string cacheKey;
		private object cacheData;
		private HttpContext context;
		/// <summary>
		/// The key used to identify a type of cache
		/// </summary>
		public string GlobalKey
		{
			get {return globalKey;}
		}
		/// <summary>
		/// The key used to identify a value in Cache object,
		/// and also the file name used for validation
		/// </summary>
		public string[] LocalKeys
		{
			get {return localKeys;}

		}

        //lockValue
        private static object lockValue = new object();
		/// <summary>
		/// Retrieve the value of data stored in cache, as identified by the LocalKey
		/// </summary>
		public object Value
		{
			get
			{
				cacheData = context.Cache[cacheKey];
                if (cacheData == null)
                {
                    cacheData = LoadCache();
                }

 			return cacheData;
			}
		}


		public BaseCache(HttpContext context)
		{
			this.context = context;
			globalKey = this.GetType().Name.ToString();
		}


        public  void RemoveCacheKey()
        {
            object cacheData = this.context.Cache[cacheKey];
            if (cacheData != null)
            {
                this.context.Cache.Remove(cacheKey);
            }
        }

        public void RemoveCacheKey(string cacheKey)
        {
            object cacheData = this.context.Cache[cacheKey];
            if (cacheData != null)
            {
                this.context.Cache.Remove(cacheKey);
            }
        }
	
			/// <summary>
		/// Retrieve data from database, or other sources.
		/// </summary>
		/// <returns></returns>
		protected virtual object GetData()
		{
			return null;
		}
		protected virtual void InitLocalKeys(){}
		/// <summary>
		/// Pass in data, and store the data into ASP.NET Cache object,.
		/// </summary>
		/// <remarks>
		/// This should only be called if this specific Cache class did not define its
		/// own way of loading data in the GetData() method.
		/// </remarks> 
		/// <param name="cacheValue">Cache data</param>
		public void LoadCache(object cacheValue)
		{
			CacheItemPriority cachePriority = TranslatePriority(3);
            context.Cache.Insert(cacheKey, cacheValue, null, DateTime.Now.AddHours(1), TimeSpan.Zero, cachePriority, null);

		}
		/// <summary>
		/// Load data into ASP.NET Cache object, using the GetData() defined in this specific Cache class.
		/// </summary>
		public object LoadCache()
		{
			object cacheValue = GetData();
			if (cacheValue != null)
				LoadCache(cacheValue);
            return cacheValue;
		}
		
		
		
		private CacheItemPriority TranslatePriority(int priority)
		{
			CacheItemPriority cachePriority = CacheItemPriority.Normal;
			switch (priority)
			{
				case 1:
					cachePriority = CacheItemPriority.Low;
					break;
				case 2:
					cachePriority = CacheItemPriority.BelowNormal;
					break;
				case 3:
					cachePriority = CacheItemPriority.Normal;
					break;
				case 4:
					cachePriority = CacheItemPriority.AboveNormal;
					break;
				case 5:
					cachePriority = CacheItemPriority.High;
					break;
				case 6:
					cachePriority = CacheItemPriority.NotRemovable;
					break;
				default:
					cachePriority = CacheItemPriority.Normal;
					break;
			}
			return cachePriority;
		}
	}
}
