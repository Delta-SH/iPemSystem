using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 代维公司表
    /// </summary>
    [Serializable]
    public partial class C_SubCompany : BaseEntity {
        /// <summary>
        /// 代维公司编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 代维公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代维公司联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 代维公司电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 代维公司传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 代维公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 代维公司类别(1,2,3,4类)
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