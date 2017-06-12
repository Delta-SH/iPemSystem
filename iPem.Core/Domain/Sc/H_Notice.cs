using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 系统消息表
    /// </summary>
    [Serializable]
    public class H_Notice : BaseEntity {
        /// <summary>
        /// 消息编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
