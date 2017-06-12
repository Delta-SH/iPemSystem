using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 职位信息表
    /// </summary>
    [Serializable]
    public partial class C_Duty : BaseEntity {
        /// <summary>
        /// 职位编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 职位等级
        /// </summary>
        public string Level { get; set; }

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
