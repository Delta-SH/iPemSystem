using System;
using System.Collections.Generic;

namespace iPem.Core.Caching {
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager {
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        T Get<T>(string key);

        /// <summary>
        /// 获取HASH缓存对象
        /// </summary>
        T GetFromHash<T>(string hashId, string key);

        /// <summary>
        /// 批量获取HASH缓存对象
        /// </summary>
        IList<T> GetAllFromHash<T>(string hashId);

        /// <summary>
        /// 设置缓存对象
        /// </summary>
        void Set(string key, object data);

        /// <summary>
        /// 设置缓存对象
        /// </summary>
        void Set(string key, object data, TimeSpan cacheTime);

        /// <summary>
        /// 设置HASH缓存对象
        /// </summary>
        void SetInHash(string hashId, string key, object data);

        /// <summary>
        /// 设置HASH缓存对象
        /// </summary>
        void SetInHash(string hashId, string key, object data, TimeSpan cacheTime);

        /// <summary>
        /// 批量设置HASH缓存对象
        /// </summary>
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data);

        /// <summary>
        /// 批量设置HASH缓存对象
        /// </summary>
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, object>> data, TimeSpan cacheTime);

        /// <summary>
        /// 判断缓存对象是否存在
        /// </summary>
        bool IsSet(string key);

        /// <summary>
        /// 判断HASH缓存对象是否存在
        /// </summary>
        bool IsHashSet(string hashId, string key);

        /// <summary>
        /// 删除指定键的缓存对象
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// 根据正则表达式匹配删除缓存对象
        /// </summary>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 删除指定键的HASH缓存对象
        /// </summary>
        void RemoveHash(string hashId, string key);

        /// <summary>
        /// 清空缓存对象
        /// </summary>
        void Clear();

        /// <summary>
        /// 获得缓存临界锁
        /// </summary>
        IDisposable AcquireLock(string key, long timeOut = 30);

        /// <summary>
        /// 添加缓存集合对象到List
        /// </summary>
        void AddItemsToList<T>(string key, IEnumerable<T> values);

        /// <summary>
        /// 添加缓存集合对象到List
        /// </summary>
        void AddItemsToList<T>(string key, IEnumerable<T> values, TimeSpan cacheTime);

        /// <summary>
        /// 从List中获取缓存集合对象
        /// </summary>
        IEnumerable<T> GetItemsFromList<T>(string key);

        /// <summary>
        /// 添加缓存集合对象到Set
        /// </summary>
        void AddItemsToSet<T>(string key, IEnumerable<T> values);

        /// <summary>
        /// 添加缓存集合对象到Set
        /// </summary>
        void AddItemsToSet<T>(string key, IEnumerable<T> values, TimeSpan cacheTime);

        /// <summary>
        /// 从Set中获取缓存集合对象
        /// </summary>
        IEnumerable<T> GetItemsFromSet<T>(string key);

        /// <summary>
        /// 判断Set缓存对象是否存在
        /// </summary>
        bool SetContainsItem<T>(string key, T item);
    }
}