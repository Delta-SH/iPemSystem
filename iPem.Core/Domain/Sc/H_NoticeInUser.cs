using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 用户消息映射表
    /// </summary>
    [Serializable]
    public class H_NoticeInUser : BaseEntity {
        /// <summary>
        /// 消息编号
        /// </summary>
        public Guid NoticeId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Readed { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        public DateTime ReadTime { get; set; }
    }
}
