using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 计量单位表
    /// </summary>
    [Serializable]
    public partial class C_Unit : BaseEntity {
        /// <summary>
        /// 单位编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }

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