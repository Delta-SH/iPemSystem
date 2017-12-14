using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class MaskingModel {
        [ExcelDisplayName("序号")]
        public long index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("屏蔽范围")]
        public string name { get; set; }

        [ExcelDisplayName("屏蔽类型")]
        public string type { get; set; }

        [ExcelDisplayName("屏蔽时间")]
        public string time { get; set; }
    }
}