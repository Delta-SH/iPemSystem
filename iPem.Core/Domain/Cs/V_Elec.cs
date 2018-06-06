using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 能耗数据表
    /// </summary>
    [Serializable]
    public partial class V_Elec : BaseEntity {
        /// <summary>
        /// 编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EnmSSH Type { get; set; }

        /// <summary>
        /// 公式类型
        /// </summary>
        public EnmFormula FormulaType { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 能耗值
        /// </summary>
        public double Value { get; set; }
    }
}
