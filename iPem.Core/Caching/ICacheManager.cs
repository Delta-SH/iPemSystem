using System;
using System.Collections.Generic;

namespace iPem.Core.Caching {
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T GetFromHash<T>(string hashId, string key);

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="hashId">The hashId of the value to get.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        IList<T> GetAllFromHash<T>(string hashId);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        void Set(string key, object data);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        void Set(string key, object data, TimeSpan cacheTime);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        void SetInHash(string hashId, string key, object data);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        void SetInHash(string hashId, string key, object data, TimeSpan cacheTime);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="hashId">hashId</param>
        /// <param name="key">key</param>
        /// <param name="data">object</param>
        /// <param name="cacheTime">cache time</param>
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data, TimeSpan cacheTime);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool IsSet(string key);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        bool IsHashSet(string hashId, string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void RemoveHash(string hashId, string key);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void Clear();
    }
}