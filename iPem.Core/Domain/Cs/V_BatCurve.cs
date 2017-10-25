using iPem.Core.Enum;
using System;

namespace iPem.Core {
    /// <summary>
    /// 电池充放电曲线表
    /// </summary>
    [Serializable]
    public partial class V_BatCurve {
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
        /// 电池组号
        /// </summary>
        public int PackId { get; set; }

        /// <summary>
        /// 电池状态
        /// </summary>
        public EnmBatStatus Type { get; set; }

        /// <summary>
        /// 放电开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 测值时间
        /// </summary>
        public DateTime ValueTime { get; set; }

        /// <summary>
        /// 充放电过程的开始时间
        /// </summary>
        public DateTime ProcTime { get; set; }
    }
}