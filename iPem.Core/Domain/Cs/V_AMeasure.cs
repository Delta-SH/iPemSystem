using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 实时性能数据表
    /// </summary>
    [Serializable]
    public partial class V_AMeasure : BaseEntity {
        /// <summary>
        /// 区域编码(第三级区域)
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 机房编码
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// FSU编码
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号标准化编码
        /// </summary>
        public string SignalId { get; set; }

        /// <summary>
        /// 信号顺序号
        /// </summary>
        public string SignalNumber { get; set; }

        /// <summary>
        /// 信号描述(单位)
        /// </summary>
        public string SignalDesc { get; set; }

        /// <summary>
        /// 监测值状态
        /// </summary>
        public EnmState Status { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 测值时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
