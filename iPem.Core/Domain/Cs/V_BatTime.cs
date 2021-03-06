﻿using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 蓄电池后备时长表
    /// </summary>
    [Serializable]
    public partial class V_BatTime : BaseEntity {
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
        /// 电池组号
        /// </summary>
        public int PackId { get; set; }

        /// <summary>
        /// 电池状态
        /// </summary>
        public EnmBatStatus Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 过程时间
        /// </summary>
        public DateTime ProcTime { get; set; }
    }
}