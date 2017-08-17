using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500306 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("站点名称")]
        public string name { get; set; }

        [ExcelDisplayName("统计时段")]
        public string period { get; set; }

        [ExcelDisplayName("设备能耗(kW·h)")]
        public double device { get; set; }

        [ExcelDisplayName("总能耗(kW·h)")]
        public double total { get; set; }

        [ExcelDisplayName("PUE")]
        public double pue { get; set; }

        [ExcelDisplayName("能效")]
        public double eer { get; set; }
    }
}