using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 信号信息
    /// </summary>
    [Serializable]
    public partial class D_Signal {
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 机房编码
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 机房名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号名称
        /// </summary>
        public string PointName { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public EnmPoint PointType { get; set; }

        /// <summary>
        /// 当前参数
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// 标准参数
        /// </summary>
        public string Normal { get; set; }
    }
}
