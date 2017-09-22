using System;
using System.Collections.Generic;

namespace iPem.Core.Caching {
    /// <summary>
    /// Represents a manager for disable cache
    /// </summary>
    public partial class DisableCacheManager : ICacheManager {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key) {
            return default(T);
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T GetFromHash<T>(string hashId, string key) {
            return default(T);
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual IList<T> GetAllFromHash<T>(string hashId) {
            return default(List<T>);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void Set(string key, object data) {
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void Set(string key, object data, TimeSpan cacheTime) {
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void SetInHash(string hashId, string key, object data) {
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void SetInHash(string hashId, string key, object data, TimeSpan cacheTime) {
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data) {
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data, TimeSpan cacheTime) {
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsSet(string key) {
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsHashSet(string hashId, string key) {
            return false;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key) {
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern) {
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void RemoveHash(string hashId, string key) {
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear() {
        }
    }
}
