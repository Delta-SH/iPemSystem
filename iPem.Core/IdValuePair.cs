using System;

namespace iPem.Core {
    /// <summary>
    /// 键值数据类
    /// </summary>
    [Serializable]
    public class IdValuePair<T1, T2> {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IdValuePair() { }

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="id">键</param>
        /// <param name="value">值</param>
        public IdValuePair(T1 id, T2 value) {
            this.Id = id;
            this.Value = value;
        }

        /// <summary>
        /// 键
        /// </summary>
        public T1 Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T2 Value { get; set; }
    }
}
