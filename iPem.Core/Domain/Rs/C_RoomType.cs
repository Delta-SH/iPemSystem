using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 机房类型表
    /// </summary>
    [Serializable]
    public partial class C_RoomType : BaseEntity {
        /// <summary>
        /// 机房类型编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 机房类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
    }
}