using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models{
    [Serializable]
    public class Model500105 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("站点名称")]
        public string name { get; set; }

        [ExcelDisplayName("站点类型")]
        public string type { get; set; }

        [ExcelDisplayName("告警时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("市电路数")]
        public int count { get; set; }

        [ExcelDisplayName("统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("市电可用度")]
        public string rate { get; set; }
    }
}