using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 系统参数表
    /// </summary>
    [Serializable]
    public partial class M_Dictionary : BaseEntity {
        /// <summary>
        /// 参数编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值，JSON格式
        /// </summary>
        public string ValuesJson { get; set; }

        /// <summary>
        /// 参数值，二进制格式
        /// </summary>
        public byte[] ValuesBinary { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }
}
