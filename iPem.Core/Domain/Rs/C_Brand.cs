using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 设备品牌表
    /// </summary>
    [Serializable]
    public partial class C_Brand : BaseEntity {
        /// <summary>
        /// 品牌编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生产厂家编码(外键)
        /// </summary>
        public string ProductorId { get; set; }

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
