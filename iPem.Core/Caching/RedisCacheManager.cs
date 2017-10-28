using iPem.Core.Extensions;
using Newtonsoft.Json;
using NServiceKit.Redis;
using NServiceKit.Redis.Support;
using System;
using System.Collections.Generic;
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
        /// CacheTime interval
        /// </summary>
        private static TimeSpan _cacheTime = TimeSpan.FromSeconds(300);

        /// <summary>
        /// JsonSerializerSettings
        /// </summary>
        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { 
            NullValueHandling = NullValueHandling.Ignore, 
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //序列化所有字段，包括含有[IgnoreJson]属性的字段,但不包括含有[JsonRedisIgnore]属性的字段
            ContractResolver = new IgnoreJsonAttributesResolver() 
        };

        /// <summary>
        /// Create client manager
        /// </summary>
        private void CreateClientManager() {
            var config = iPem.Core.Configuration.RedisConfig.GetConfig();
            if (config != null) {
                var writeServers = config.WriteServerList.Split(new char[] { ',' });
                var readServers = config.ReadServerList.Split(new char[] { ',' });

                _clientManager = new PooledRedisClientManager(writeServers, readServers, new RedisClientManagerConfig { MaxWritePoolSize = config.MaxWritePoolSize, MaxReadPoolSize = config.MaxReadPoolSize, AutoStart = config.AutoStart });
                _cacheTime = TimeSpan.FromSeconds(config.LocalCacheTime);
            }
        }

        /// <summary>
        /// Cache
        /// </summary>
        protected IRedisClient Cache {
            get {
                if(_clientManager == null) 
                    CreateClientManager();

                if (_clientManager == null)
                    throw new iPemException("初始化PooledRedisClientManager错误");

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
                    return JsonConvert.DeserializeObject<T>(client.GetValue(key), _jsonSettings);
                }
            }

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
            using (var client = Cache) {
                if (client.HashContainsEntry(hashId, key)) {
                    return JsonConvert.DeserializeObject<T>(client.GetValueFromHash(hashId, key), _jsonSettings);
                }
            }

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
            using (var client = Cache) {
                if (client.ContainsKey(hashId)) {
                    var valueStrings = client.GetHashValues(hashId);
                    var values = new List<T>();
                    foreach (var valueString in valueStrings) {
                        values.Add(JsonConvert.DeserializeObject<T>(valueString, _jsonSettings));
                    }

                    return values;
                }
            }

            return default(List<T>);
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
            using (var client = Cache) {
                var valueString = JsonConvert.SerializeObject(data, _jsonSettings);
                client.SetEntry(key, valueString, cacheTime);
            }
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
            using (var client = Cache) {
                var valueString = JsonConvert.SerializeObject(data, _jsonSettings);
                client.SetEntryInHash(hashId, key, valueString);
                client.ExpireEntryIn(hashId, cacheTime);
            }
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
            using (var client = Cache) {
                var valueStrings = new List<KeyValuePair<string, string>>();
                foreach (var value in data) {
                    valueStrings.Add(new KeyValuePair<string, string>(value.Key, JsonConvert.SerializeObject(value.Value, _jsonSettings)));
                }

                client.SetRangeInHash(hashId, valueStrings);
                client.ExpireEntryIn(hashId, cacheTime);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsSet(string key) {
            using (var client = Cache) {
                return client.ContainsKey(key);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public virtual bool IsHashSet(string hashId, string key) {
            using (var client = Cache) {
                return client.HashContainsEntry(hashId, key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key) {
            using (var client = Cache) {
                client.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern) {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            using (var client = Cache) {
                var keys = client.GetAllKeys();
                foreach (var key in keys) {
                    if (regex.IsMatch(key)) {
                        client.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void RemoveHash(string hashId, string key) {
            using (var client = Cache) {
                client.RemoveEntryFromHash(hashId, key);
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
