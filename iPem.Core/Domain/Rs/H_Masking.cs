using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 告警屏蔽表
    /// </summary>
    [Serializable]
    public partial class H_Masking : BaseEntity {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 屏蔽类型
        /// </summary>
        public EnmMaskType Type { get; set; }

        /// <summary>
        /// 屏蔽时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
