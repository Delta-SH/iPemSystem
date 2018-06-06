using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 列头柜数据表
    /// </summary>
    [Serializable]
    public partial class V_ACabinet {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号分类
        /// </summary>
        public EnmVSignalCategory Category { get; set; }

        /// <summary>
        /// 分路总值（A+B）
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 总值时间
        /// </summary>
        public DateTime ValueTime { get; set; }

        /// <summary>
        /// 分路A路值
        /// </summary>
        public double AValue { get; set; }

        /// <summary>
        /// A路值时间
        /// </summary>
        public DateTime AValueTime { get; set; }

        /// <summary>
        /// 分路B路值
        /// </summary>
        public double BValue { get; set; }

        /// <summary>
        /// B路值时间
        /// </summary>
        public DateTime BValueTime { get; set; }

        /// <summary>
        /// 分路C路值
        /// </summary>
        public double CValue { get; set; }

        /// <summary>
        /// C路值时间
        /// </summary>
        public DateTime CValueTime { get; set; }
    }
}
