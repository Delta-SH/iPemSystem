using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500403 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("超出规定确认时长的告警数量")]
        public int count { get; set; }

        [ExcelDisplayName("告警总数")]
        public int total { get; set; }

        [ExcelDisplayName("告警确认及时率")]
        public string rate { get; set; }
    }
}