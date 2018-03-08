using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 用户订制信息表
    /// </summary>
    [Serializable]
    public partial class U_Profile {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 信息类型
        /// </summary>
        public EnmProfile Type { get; set; }

        /// <summary>
        /// 自定义信息，JSON格式
        /// </summary>
        public string ValuesJson { get; set; }

        /// <summary>
        /// 自定义信息，二进制格式
        /// </summary>
        public byte[] ValuesBinary { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }
}