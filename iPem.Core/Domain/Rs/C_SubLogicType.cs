using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 逻辑子类表
    /// </summary>
    [Serializable]
    public partial class C_SubLogicType : BaseEntity {
        /// <summary>
        /// 逻辑子类编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 逻辑子类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 所属逻辑分类编号
        /// </summary>
        public string LogicTypeId { get; set; }
    }
}