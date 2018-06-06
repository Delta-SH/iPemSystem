using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500301 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("空调(kW·h)")]
        public double kt { get; set; }

        [ExcelDisplayName("照明(kW·h)")]
        public double zm { get; set; }

        [ExcelDisplayName("办公(kW·h)")]
        public double bg { get; set; }

        [ExcelDisplayName("IT设备(kW·h)")]
        public double it { get; set; }

        [ExcelDisplayName("开关电源(kW·h)")]
        public double dy { get; set; }

        [ExcelDisplayName("UPS(kW·h)")]
        public double ups { get; set; }

        [ExcelDisplayName("其他(kW·h)")]
        public double qt { get; set; }

        [ExcelDisplayName("总计(kW·h)")]
        public double tt { get; set; }

        [ExcelDisplayName("空调占比")]
        public string ktrate { get; set; }

        [ExcelDisplayName("照明占比")]
        public string zmrate { get; set; }

        [ExcelDisplayName("办公占比")]
        public string bgrate { get; set; }

        [ExcelDisplayName("IT设备占比")]
        public string itrate { get; set; }

        [ExcelDisplayName("开关电源占比")]
        public string dyrate { get; set; }

        [ExcelDisplayName("UPS占比")]
        public string upsrate { get; set; }

        [ExcelDisplayName("其他占比")]
        public string qtrate { get; set; }
    }
}