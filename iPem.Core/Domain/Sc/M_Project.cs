using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 工程信息表
    /// </summary>
    [Serializable]
    public partial class M_Project : BaseEntity {
        /// <summary>
        /// 工程编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 工程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 工程负责人
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 工程公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

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