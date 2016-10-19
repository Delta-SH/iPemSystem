using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Chart500301 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("空调")]
        public double kt { get; set; }

        [ExcelDisplayName("照明")]
        public double zm { get; set; }

        [ExcelDisplayName("办公")]
        public double bg { get; set; }

        [ExcelDisplayName("设备")]
        public double sb { get; set; }

        [ExcelDisplayName("开关电源")]
        public double kgdy { get; set; }

        [ExcelDisplayName("UPS")]
        public double ups { get; set; }

        [ExcelDisplayName("其他")]
        public double qt { get; set; }
    }
}