﻿using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 历史性能数据表
    /// </summary>
    [Serializable]
    public partial class V_HMeasure : BaseEntity {
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
        /// 信号类型
        /// </summary>
        public int Type { get; set; }

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
