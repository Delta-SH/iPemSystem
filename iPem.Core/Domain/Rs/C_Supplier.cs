using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 供应商表
    /// </summary>
    [Serializable]
    public partial class C_Supplier : BaseEntity {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 供应商联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 供应商电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 供应商传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 供应商级别
        /// </summary>
        public string Level { get; set; }

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