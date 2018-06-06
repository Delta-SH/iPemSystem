using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 停电、发电信息类
    /// </summary>
    [Serializable]
    public partial class V_Offline : BaseEntity {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型(站点、机房、设备)
        /// </summary>
        public EnmSSH Type { get; set; }

        /// <summary>
        /// 停电、发电公式类型
        /// </summary>
        public EnmFormula FormulaType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 发电量
        /// </summary>
        public double Value { get; set; }
    }
}
