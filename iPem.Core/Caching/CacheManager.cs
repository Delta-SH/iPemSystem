using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace iPem.Core.Caching {
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class CacheManager : ICacheManager {
        /// <summary>
        /// CacheTime Interval
        /// </summary>
        private static TimeSpan _cacheTime = TimeSpan.FromSeconds(300);

        protected Cache Cache {
            get {
                return HttpRuntime.Cache;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key) {
            return (T)Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        public virtual void Set<T>(string key, T data) {
            Set<T>(key, data, _cacheTime);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set<T>(string key, T data, TimeSpan cacheTime) {
            Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, cacheTime, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key) {
            return (Cache[key] != null);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key) {
            Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern) {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (regex.IsMatch(enumerator.Key.ToString())) {
                    Cache.Remove(enumerator.Key.ToString());
                }
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear() {
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext()) {
                Cache.Remove(enumerator.Key.ToString());
            }
        }
    }
}
