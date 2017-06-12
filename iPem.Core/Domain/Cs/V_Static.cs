using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 历史性能数据统计表
    /// </summary>
    [Serializable]
    public partial class V_Static : BaseEntity {
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
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        public double AvgValue { get; set; }

        /// <summary>
        /// 最大值时间
        /// </summary>
        public DateTime MaxTime { get; set; }

        /// <summary>
        /// 最小值时间
        /// </summary>
        public DateTime MinTime { get; set; }

        /// <summary>
        /// 统计数量
        /// </summary>
        public int Total { get; set; }
    }
}