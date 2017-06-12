using System;
using iPem.Core.Enum;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Serializable]
    public partial class H_WebEvent : BaseEntity {
        /// <summary>
        /// 日志编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public EnmEventLevel Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public EnmEventType Type { get; set; }

        /// <summary>
        /// 日志摘要
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 来源URL
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// 相关URL
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// 触发用户
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}