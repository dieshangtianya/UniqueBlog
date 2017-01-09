using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace UniqueBlog.Controllers.Cache
{
	public interface ICache
	{
		/// <summary>
		/// Get a cache object according to the cache key
		/// </summary>
		/// <param name="cacheKey">Cache key</param>
		/// <returns></returns>
		object Get(string cacheKey);

		/// <summary>
		/// Get all keys of the current cache
		/// </summary>
		/// <returns></returns>
		List<string> GetCacheKeys();

		/// <summary>
		/// Set cache object with a key
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="cacheObject"></param>
		void Set(string cacheKey, object cacheObject);

		/// <summary>
		/// Set cache object with a key and expiration date
		/// </summary>
		/// <param name="cacheKey">Key</param>
		/// <param name="cacheObject">Object will be cached</param>
		/// <param name="expiration">Absolute expiration datetime</param>
		void Set(string cacheKey, object cacheObject, DateTime expiration);

		/// <summary>
		/// Set cache object with a key and slidding expiration date
		/// </summary>
		/// <param name="cacheKey">Key</param>
		/// <param name="cacheObject">Object will be cached</param>
		/// <param name="expiration">Slidding expiration timespan</param>
		void Set(string cacheKey, object cacheObject, TimeSpan sliddingExpiration);

		/// <summary>
		/// Set cache object with a key, absolute expiration date and priority
		/// </summary>
		/// <param name="cacheKey">Key</param>
		/// <param name="cacheObject">Object will be cached</param>
		/// <param name="expiration">Absolute expiration datetime</param>
		/// <param name="priority">Priority option</param>
		void Set(string cacheKey, object cacheObject, DateTime expiration, CacheItemPriority priority);

		/// <summary>
		/// Set cache object with a key, slidding expiration date and priority
		/// </summary>
		/// <param name="cacheKey">Key</param>
		/// <param name="cacheObject">Object will be cached</param>
		/// <param name="sliddingExpiration">Slidding expiration timespan</param>
		/// <param name="priority">Priority option</param>
		void Set(string cacheKey, object cacheObject, TimeSpan sliddingExpiration, CacheItemPriority priority);

		/// <summary>
		/// Delete a cache object by the key
		/// </summary>
		/// <param name="cacheKey">key</param>
		void Remove(string cacheKey);

		/// <summary>
		/// Determine whether there is an cache key
		/// </summary>
		/// <param name="cacheKey"></param>
		bool Exist(string cacheKey);

		/// <summary>
		/// Flush all the cache objects
		/// </summary>
		void Flush();
	}
}
