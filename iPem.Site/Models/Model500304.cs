using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500304 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("时段")]
        public string period { get; set; }

        [ExcelDisplayName("当前能耗(kW·h)")]
        public double current { get; set; }

        [ExcelDisplayName("前期能耗(kW·h)")]
        public double last { get; set; }

        [ExcelDisplayName("环比增量(kW·h)")]
        public double increase { get; set; }

        [ExcelDisplayName("环比增幅")]
        public string rate { get; set; }
    }
}