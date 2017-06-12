using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 自定义枚举表
    /// </summary>
    [Serializable]
    public partial class C_EnumMethod : BaseEntity {
        /// <summary>
        /// 编号，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 枚举编号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Comment { get; set; }
    }
}
