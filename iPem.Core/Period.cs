using System;

namespace iPem.Core {
    /// <summary>
    /// 周期时段类
    /// </summary>
    [Serializable]
    public class Period {
        /// <summary>
        /// 时段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
    }
}
