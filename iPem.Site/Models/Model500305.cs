using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500305 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("时段")]
        public string period { get; set; }

        [ExcelDisplayName("站点A")]
        public string Aname { get; set; }

        [ExcelDisplayName("站点B")]
        public string Bname { get; set; }

        [ExcelDisplayName("站点A能耗(kW·h)")]
        public double Avalue { get; set; }

        [ExcelDisplayName("站点B能耗(kW·h)")]
        public double Bvalue { get; set; }

        [ExcelDisplayName("对比值A-B(kW·h)")]
        public double increase { get; set; }

        [ExcelDisplayName("对比幅度(A-B)/B")]
        public string rate { get; set; }
    }
}