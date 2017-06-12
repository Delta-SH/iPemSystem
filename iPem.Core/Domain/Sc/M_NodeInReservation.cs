using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 节点(区域、站点、机房、设备)预约映射表
    /// </summary>
    [Serializable]
    public partial class M_NodeInReservation {
        /// <summary>
        /// 工程预约编号
        /// </summary>
        public string ReservationId { get; set; }

        /// <summary>
        /// 节点编号(区域、站点、机房、设备)
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public EnmSSH NodeType { get; set; }
    }
}