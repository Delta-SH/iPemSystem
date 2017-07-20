using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 脚本升级表
    /// </summary>
    [Serializable]
    public partial class R_DBScript : BaseEntity {
        /// <summary>
        /// 脚本编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 脚本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string Executor { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutedTime { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Comment { get; set; }
    }
}
