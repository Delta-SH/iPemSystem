using System;

namespace iPem.Core {
    /// <summary>
    /// 键值数据类
    /// </summary>
    [Serializable]
    public class ValuesPair<T1, T2, T3> {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ValuesPair() { }

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="id">键</param>
        /// <param name="value">值</param>
        public ValuesPair(T1 value1, T2 value2, T3 value3) {
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
        }

        /// <summary>
        /// 值
        /// </summary>
        public T1 Value1 { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T2 Value2 { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T3 Value3 { get; set; }
    }
}
