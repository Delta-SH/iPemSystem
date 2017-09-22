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

        /// <summary>
        /// Cache
        /// </summary>
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
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T GetFromHash<T>(string hashId, string key) {
            return (T)Cache[string.Format("{0}:{1}", hashId, key)];
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual IList<T> GetAllFromHash<T>(string hashId) {
            var values = new List<T>();
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (enumerator.Key.ToString().StartsWith(string.Format("{0}:", hashId))) {
                    values.Add((T)enumerator.Value);
                }
            }

            return values;
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void Set(string key, object data) {
            Set(key, data, _cacheTime);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void Set(string key, object data, TimeSpan cacheTime) {
            Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, cacheTime, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void SetInHash(string hashId, string key, object data) {
            SetInHash(hashId, key, data, _cacheTime);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void SetInHash(string hashId, string key, object data, TimeSpan cacheTime) {
            Cache.Insert(string.Format("{0}:{1}", hashId, key), data, null, Cache.NoAbsoluteExpiration, cacheTime, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data) {
            SetRangeInHash(hashId, data, _cacheTime);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data, TimeSpan cacheTime) {
            foreach (var kv in data) {
                Cache.Insert(string.Format("{0}:{1}", hashId, kv.Key), kv.Value, null, Cache.NoAbsoluteExpiration, cacheTime, CacheItemPriority.Default, null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsSet(string key) {
            return (Cache[key] != null);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsHashSet(string hashId, string key) {
            return (Cache[string.Format("{0}:{1}", hashId, key)] != null);
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
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void RemoveHash(string hashId, string key) {
            Cache.Remove(string.Format("{0}:{1}", hashId, key));
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
