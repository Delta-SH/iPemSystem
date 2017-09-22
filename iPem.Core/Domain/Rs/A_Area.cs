using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 区域信息表
    /// </summary>
    [Serializable]
    public partial class A_Area : BaseEntity {
        /// <summary>
        /// 区域编号(国家行政代码)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区域类型(省、市、县)
        /// </summary>
        public IdValuePair<int, string> Type { get; set; }

        /// <summary>
        /// 上级区域，0表示顶级区域
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 所属厂家
        /// </summary>
        public C_SCVendor Vendor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态(启用、禁用)
        /// </summary>
        public bool Enabled { get; set; }
    }
}
