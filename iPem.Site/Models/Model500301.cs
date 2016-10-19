using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500301 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("空调能耗(kW·h)")]
        public double kt { get; set; }

        [ExcelDisplayName("照明能耗(kW·h)")]
        public double zm { get; set; }

        [ExcelDisplayName("办公能耗(kW·h)")]
        public double bg { get; set; }

        [ExcelDisplayName("设备能耗(kW·h)")]
        public double sb { get; set; }

        [ExcelDisplayName("开关电源能耗(kW·h)")]
        public double kgdy { get; set; }

        [ExcelDisplayName("UPS能耗(kW·h)")]
        public double ups { get; set; }

        [ExcelDisplayName("其他能耗(kW·h)")]
        public double qt { get; set; }

        [ExcelDisplayName("总能耗(kW·h)")]
        public double zl { get; set; }

        [ExcelDisplayName("空调能耗占比")]
        public string ktrate { get; set; }

        [ExcelDisplayName("照明能耗占比")]
        public string zmrate { get; set; }

        [ExcelDisplayName("办公能耗占比")]
        public string bgrate { get; set; }

        [ExcelDisplayName("设备能耗占比")]
        public string sbrate { get; set; }

        [ExcelDisplayName("开关电源能耗占比")]
        public string kgdyrate { get; set; }

        [ExcelDisplayName("UPS能耗占比")]
        public string upsrate { get; set; }

        [ExcelDisplayName("其他能耗占比")]
        public string qtrate { get; set; }
    }
}