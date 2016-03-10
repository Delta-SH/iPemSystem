using NServiceKit.Redis;
using NServiceKit.Redis.Support;
using System;
using System.Text.RegularExpressions;

namespace iPem.Core.Caching {
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class RedisCacheManager : ICacheManager {
        /// <summary>
        /// PooledRedisClientManager
        /// </summary>
        private static PooledRedisClientManager _clientManager;

        /// <summary>
        /// ObjectSerializer
        /// </summary>
        private static ObjectSerializer _objectSerializer;

        /// <summary>
        /// CacheTime Interval
        /// </summary>
        private static TimeSpan _cacheTime = TimeSpan.FromSeconds(300);

        /// <summary>
        /// Create ClientManager
        /// </summary>
        private void CreateClientManager() {
            var config = iPem.Core.Configuration.RedisConfig.GetConfig();
            if(config != null) {
                var writeServers = config.WriteServerList.Split(new char[] { ',' });
                var readServers = config.ReadServerList.Split(new char[] { ',' });
                    _cacheTime = TimeSpan.FromSeconds(config.LocalCacheTime);

                _objectSerializer = new ObjectSerializer();
                _clientManager = new PooledRedisClientManager(writeServers, readServers, new RedisClientManagerConfig { MaxWritePoolSize = config.MaxWritePoolSize, MaxReadPoolSize = config.MaxReadPoolSize, AutoStart = config.AutoStart });
            }
        }

        protected IRedisClient Cache {
            get {
                if(_clientManager == null) 
                    CreateClientManager();

                return _clientManager.GetClient();
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key) {
            using(var client = Cache) {
                if(client.ContainsKey(key)) {
                    //return (T)_objectSerializer.Deserialize(client.Get<byte[]>(key));
                    return client.Get<T>(key);
                }
            }

            return default(T);
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
            using(var client = Cache) {
                //client.Set<byte[]>(key, _objectSerializer.Serialize(data), TimeSpan.FromSeconds(cacheTime)); 
                client.Set<T>(key, data, cacheTime);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key) {
            using(var client = Cache) {
                return client.ContainsKey(key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key) {
            using(var client = Cache) {
                client.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern) {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            using(var client = Cache) {
                var keys = client.GetAllKeys();
                foreach(var key in keys) {
                    if(regex.IsMatch(key)) {
                        client.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear() {
            using(var client = Cache) {
                client.FlushDb();
            }
        }
    }
}
