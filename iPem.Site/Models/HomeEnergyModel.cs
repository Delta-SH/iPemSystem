using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeEnergyModel {
        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("空调")]
        public double kt { get; set; }

        [ExcelDisplayName("照明")]
        public double zm { get; set; }

        [ExcelDisplayName("办公")]
        public double bg { get; set; }

        [ExcelDisplayName("IT设备")]
        public double it { get; set; }

        [ExcelDisplayName("开关电源")]
        public double dy { get; set; }

        [ExcelDisplayName("UPS")]
        public double ups { get; set; }

        [ExcelDisplayName("其他")]
        public double qt { get; set; }

        [ExcelDisplayName("总能耗")]
        public double tt { get; set; }

        [ExcelDisplayName("PUE")]
        public double pue { get; set; }

        [ExcelDisplayName("能效")]
        public double eer { get; set; }
    }
}