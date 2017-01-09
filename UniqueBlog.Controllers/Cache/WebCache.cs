using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace UniqueBlog.Controllers.Cache
{
	public class WebCache : ICache
	{
		private static System.Web.Caching.Cache cache;

		private TimeSpan _timeSpan = new TimeSpan(0, 30, 0);

		static WebCache()
		{
			cache = HttpContext.Current.Cache;
		}

		public object Get(string cacheKey)
		{
			return cache.Get(cacheKey);
		}

		public List<string> GetCacheKeys()
		{
			List<string> keys = new List<string>();
			IDictionaryEnumerator enumerator = cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				keys.Add(enumerator.Key.ToString());
			}

			return keys;
		}

		public void Set(string cacheKey, object cacheObject)
		{
			this.Set(cacheKey, cacheObject, _timeSpan);
		}

		public void Set(string cacheKey, object cacheObject, DateTime expiration)
		{
			this.Set(cacheKey, cacheObject, expiration, CacheItemPriority.Normal);
		}

		public void Set(string cacheKey, object cacheObject, TimeSpan expiration)
		{
			this.Set(cacheKey, cacheObject, expiration, CacheItemPriority.Normal);
		}

		public void Set(string cacheKey, object cacheObject, DateTime expiration, CacheItemPriority priority)
		{
			cache.Insert(cacheKey, cacheObject, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration, priority, null);
		}

		public void Set(string cacheKey, object cacheObject, TimeSpan sliddingExpiration, CacheItemPriority priority)
		{
			cache.Insert(cacheKey, cacheObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, sliddingExpiration, priority, null);
		}

		public void Remove(string cacheKey)
		{
			if(this.Exist(cacheKey))
				cache.Remove(cacheKey);
		}

		public bool Exist(string cacheKey)
		{
			if (cache[cacheKey] != null)
				return true;
			else
				return false;
		}

		public void Flush()
		{
			foreach(string cacheKey in this.GetCacheKeys())
			{
				this.Remove(cacheKey);
			}
		}
	}
}
