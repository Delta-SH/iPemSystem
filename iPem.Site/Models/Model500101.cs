using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500101 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("站点名称")]
        public string station { get; set; }

        [ExcelDisplayName("站点类型")]
        public string type { get; set; }

        [ExcelDisplayName("告警时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("蓄电池组数")]
        public int count { get; set; }

        [ExcelDisplayName("统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("直流系统可用度")]
        public string rate { get; set; }
    }
}