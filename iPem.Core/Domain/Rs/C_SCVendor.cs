using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// SC厂家表
    /// </summary>
    [Serializable]
    public partial class C_SCVendor : BaseEntity {
        /// <summary>
        /// SC厂家编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// SC厂家名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
    }
}
