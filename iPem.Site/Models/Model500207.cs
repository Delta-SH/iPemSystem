using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500207 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("开关电源一次下电告警总时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("开关电源套数")]
        public int count { get; set; }

        [ExcelDisplayName("统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("直流系统可用度")]
        public string rate { get; set; }
    }
}