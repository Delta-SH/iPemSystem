using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 站点断站、停电、发电列表
    /// </summary>
    [Serializable]
    public partial class V_Cuted : BaseEntity {
        /// <summary>
        /// 告警编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 告警类型
        /// </summary>
        public EnmCutType Type { get; set; }

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
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
