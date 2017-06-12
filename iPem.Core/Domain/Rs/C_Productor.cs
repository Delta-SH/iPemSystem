using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 生产厂家表
    /// </summary>
    [Serializable]
    public partial class C_Productor : BaseEntity {
        /// <summary>
        /// 厂家编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// 厂家电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 厂家传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 厂家地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

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