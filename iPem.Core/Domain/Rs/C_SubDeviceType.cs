using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 设备子类表
    /// </summary>
    [Serializable]
    public partial class C_SubDeviceType : BaseEntity {
        /// <summary>
        /// 设备子类编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设备子类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 所属设备类型编号
        /// </summary>
        public string DeviceTypeId { get; set; }
    }
}