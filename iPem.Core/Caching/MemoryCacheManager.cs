using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace iPem.Core.Caching {
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager {
        /// <summary>
        /// CacheTime Interval
        /// </summary>
        private static TimeSpan _cacheTime = TimeSpan.FromSeconds(300);

        /// <summary>
        /// Cache
        /// </summary>
        protected ObjectCache Cache {
            get {
                return MemoryCache.Default;
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
            foreach (var item in Cache) {
                if (item.Key.StartsWith(string.Format("{0}:", hashId))) {
                    values.Add((T)item.Value);
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
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + cacheTime;
            Cache.Add(new CacheItem(key, data), policy);
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
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + cacheTime;
            Cache.Add(new CacheItem(string.Format("{0}:{1}", hashId, key), data), policy);
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
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now + cacheTime;
                Cache.Add(new CacheItem(string.Format("{0}:{1}", hashId, kv.Key), kv.Value), policy);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key) {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsHashSet(string hashId, string key) {
            return (Cache.Contains(string.Format("{0}:{1}", hashId, key)));
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
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove) {
                Remove(key);
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
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}