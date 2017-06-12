using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// Fsu操作日志表
    /// </summary>
    [Serializable]
    public partial class H_FsuEvent : BaseEntity {
        /// <summary>
        /// Fsu编码
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public EnmFsuEvent EventType { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime EventTime { get; set; }
    }
}
