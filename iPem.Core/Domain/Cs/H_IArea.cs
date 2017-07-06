using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 资管接口区域表
    /// </summary>
    [Serializable]
    public partial class H_IArea : BaseEntity {
        /// <summary>
        /// 区域编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区域类型编号
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 区域类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 上级编号（当为根节点时，此值为0）
        /// </summary>
        public string ParentId { get; set; }
    }
}
