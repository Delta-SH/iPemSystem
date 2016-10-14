using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500103 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("站点名称")]
        public string name { get; set; }

        [ExcelDisplayName("站点类型")]
        public string type { get; set; }

        [ExcelDisplayName("高温告警时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("温度测点总数")]
        public int total { get; set; }

        [ExcelDisplayName("统计时长")]
        public string time { get; set; }

        [ExcelDisplayName("温控系统可用度")]
        public string rate { get; set; }
    }
}