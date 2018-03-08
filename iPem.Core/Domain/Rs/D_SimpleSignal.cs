using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 信号信息
    /// </summary>
    [Serializable]
    public partial class D_SimpleSignal {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public EnmPoint PointType { get; set; }

        /// <summary>
        /// 信号名称
        /// </summary>
        public string PointName { get; set; }

        /// <summary>
        /// 单位/描述
        /// </summary>
        public string UnitState { get; set; }
    }
}
