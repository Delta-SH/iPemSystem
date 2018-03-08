using iPem.Core.Extensions;
using Newtonsoft.Json;
using NServiceKit.Redis;
using NServiceKit.Redis.Support;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace iPem.Core.Caching {
    /// <summary>
    /// Redis缓存帮助类
    /// </summary>
    public partial class RedisCacheManager : ICacheManager {
        private static PooledRedisClientManager _clientManager;
        private static TimeSpan _cacheTime = TimeSpan.FromSeconds(86400);
        private static JsonSerializerSettings _jSetting = new JsonSerializerSettings { 
            NullValueHandling = NullValueHandling.Ignore, 
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new IgnoreJsonAttributesResolver() //序列化所有字段，包括含有[IgnoreJson]属性的字段,但不包括含有[JsonRedisIgnore]属性的字段
        };

        private void CreateClientManager() {
            var config = Configuration.RedisConfig.GetConfig();
            if (config != null) {
                var writeServers = config.WriteServerList.Split(new char[] { ',' });
                var readServers = config.ReadServerList.Split(new char[] { ',' });

                _clientManager = new PooledRedisClientManager(writeServers, readServers, new RedisClientManagerConfig { MaxWritePoolSize = config.MaxWritePoolSize, MaxReadPoolSize = config.MaxReadPoolSize, AutoStart = config.AutoStart });
                _cacheTime = TimeSpan.FromSeconds(config.LocalCacheTime);
            }
        }

        protected IRedisClient Cache {
            get {
                if(_clientManager == null) 
                    CreateClientManager();

                if (_clientManager == null)
                    throw new iPemException("初始化PooledRedisClientManager错误");

                return _clientManager.GetClient();
            }
        }

        public virtual T Get<T>(string key) {
            using(var client = Cache) {
                if(client.ContainsKey(key)) {
                    return JsonConvert.DeserializeObject<T>(client.GetValue(key), _jSetting);
                }
            }

            return default(T);
        }

        public virtual T GetFromHash<T>(string hashId, string key) {
            using (var client = Cache) {
                if (client.HashContainsEntry(hashId, key)) {
                    return JsonConvert.DeserializeObject<T>(client.GetValueFromHash(hashId, key), _jSetting);
                }
            }

            return default(T);
        }

        public virtual IList<T> GetAllFromHash<T>(string hashId) {
            using (var client = Cache) {
                if (client.ContainsKey(hashId)) {
                    var valueStrings = client.GetHashValues(hashId);
                    var values = new List<T>();
                    foreach (var valueString in valueStrings) {
                        values.Add(JsonConvert.DeserializeObject<T>(valueString, _jSetting));
                    }

                    return values;
                }
            }

            return default(List<T>);
        }

        public virtual void Set(string key, object data) {
            Set(key, data, _cacheTime);
        }

        public virtual void Set(string key, object data, TimeSpan cacheTime) {
            using (var client = Cache) {
                var valueString = JsonConvert.SerializeObject(data, _jSetting);
                client.SetEntry(key, valueString, cacheTime);
            }
        }

        public virtual void SetInHash(string hashId, string key, object data) {
            SetInHash(hashId, key, data, _cacheTime);
        }

        public virtual void SetInHash(string hashId, string key, object data, TimeSpan cacheTime) {
            using (var client = Cache) {
                var valueString = JsonConvert.SerializeObject(data, _jSetting);
                client.SetEntryInHash(hashId, key, valueString);
                client.ExpireEntryIn(hashId, cacheTime);
            }
        }

        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data) {
            SetRangeInHash(hashId, data, _cacheTime);
        }

        public virtual void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data, TimeSpan cacheTime) {
            using (var client = Cache) {
                var valueStrings = new List<KeyValuePair<string, string>>();
                foreach (var value in data) {
                    valueStrings.Add(new KeyValuePair<string, string>(value.Key, JsonConvert.SerializeObject(value.Value, _jSetting)));
                }

                client.SetRangeInHash(hashId, valueStrings);
                client.ExpireEntryIn(hashId, cacheTime);
            }
        }

        public virtual bool IsSet(string key) {
            using (var client = Cache) {
                return client.ContainsKey(key);
            }
        }

        public virtual bool IsHashSet(string hashId, string key) {
            using (var client = Cache) {
                return client.HashContainsEntry(hashId, key);
            }
        }

        public virtual void Remove(string key) {
            using (var client = Cache) {
                client.Remove(key);
            }
        }

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

        public virtual void RemoveHash(string hashId, string key) {
            using (var client = Cache) {
                client.RemoveEntryFromHash(hashId, key);
            }
        }

        public virtual void Clear() {
            using(var client = Cache) {
                client.FlushDb();
            }
        }

        public virtual IDisposable AcquireLock(string key, long timeOut = 30) {
            using (var client = Cache) {
                return client.AcquireLock(key, TimeSpan.FromSeconds(timeOut));
            }
        }

        public virtual void AddItemsToList<T>(string key, IEnumerable<T> values) {
            AddItemsToList<T>(key, values, _cacheTime);
        }

        public virtual void AddItemsToList<T>(string key, IEnumerable<T> values, TimeSpan cacheTime) {
            var data = new List<string>();
            foreach (var value in values) {
                var valueString = JsonConvert.SerializeObject(value, _jSetting);
                data.Add(valueString);
            }

            if (data.Count > 0) {
                using (var client = Cache) {
                    client.AddRangeToList(key, data);
                    client.ExpireEntryIn(key, cacheTime);
                }
            }
        }

        public virtual IEnumerable<T> GetItemsFromList<T>(string key) {
            var data = new List<T>();
            using (var client = Cache) {
                if (client.ContainsKey(key)) {
                    var values = client.GetAllItemsFromList(key);
                    foreach (var value in values) {
                        data.Add(JsonConvert.DeserializeObject<T>(value, _jSetting));
                    }
                }
            }

            return data;
        }

        public virtual void AddItemsToSet<T>(string key, IEnumerable<T> values) {
            AddItemsToSet<T>(key, values, _cacheTime);
        }

        public virtual void AddItemsToSet<T>(string key, IEnumerable<T> values, TimeSpan cacheTime) {
            var data = new List<string>();
            foreach (var value in values) {
                var valueString = JsonConvert.SerializeObject(value, _jSetting);
                data.Add(valueString);
            }

            if (data.Count > 0) {
                using (var client = Cache) {
                    client.AddRangeToSet(key, data);
                    client.ExpireEntryIn(key, cacheTime);
                }
            }
        }

        public virtual IEnumerable<T> GetItemsFromSet<T>(string key) {
            var data = new List<T>();
            using (var client = Cache) {
                if (client.ContainsKey(key)) {
                    var values = client.GetAllItemsFromSet(key);
                    foreach (var value in values) {
                        data.Add(JsonConvert.DeserializeObject<T>(value, _jSetting));
                    }
                }
            }

            return data;
        }

        public virtual bool SetContainsItem<T>(string key, T item) {
            var valueString = JsonConvert.SerializeObject(item, _jSetting);
            using (var client = Cache) {
                return client.SetContainsItem(key, valueString);
            }
        }
    }
}
