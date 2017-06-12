using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 信号模版表
    /// </summary>
    [Serializable]
    public partial class P_Protocol : BaseEntity {
        /// <summary>
        /// 模版编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 模版名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属设备类型
        /// </summary>
        public C_DeviceType DeviceType { get; set; }

        /// <summary>
        /// 所属设备子类
        /// </summary>
        public C_SubDeviceType SubDeviceType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
