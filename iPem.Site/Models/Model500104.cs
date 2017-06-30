using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500104 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("站点名称")]
        public string station { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("采集设备数量")]
        public int devCount { get; set; }

        [ExcelDisplayName("采集设备告警中断时长")]
        public string almTime { set; get; }

        [ExcelDisplayName("统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("监控可用度")]
        public string rate { get; set; }
    }
}