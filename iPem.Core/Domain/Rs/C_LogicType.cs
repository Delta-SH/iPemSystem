using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 逻辑分类表
    /// </summary>
    [Serializable]
    public partial class C_LogicType {
        /// <summary>
        /// 逻辑分类编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 逻辑分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属设备类型编号
        /// </summary>
        public string DeviceTypeId { get; set; }
    }
}
