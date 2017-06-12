using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 部门信息表
    /// </summary>
    [Serializable]
    public partial class C_Department : BaseEntity {
        /// <summary>
        /// 部门编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级部门，0表示顶级部门
        /// </summary>
        public string ParentId { get; set; }

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
