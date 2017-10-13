using System;

namespace iPem.Core {
    /// <summary>
    /// 键值数据类
    /// </summary>
    [Serializable]
    public class Kv<T1, T2> {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Kv() { }

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public Kv(T1 key, T2 value) {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// 键
        /// </summary>
        public T1 Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T2 Value { get; set; }
    }
}
