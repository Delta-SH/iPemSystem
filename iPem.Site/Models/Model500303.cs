using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500303 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("时段")]
        public string period { get; set; }

        [ExcelDisplayName("当前能耗(kW·h)")]
        public double current { get; set; }

        [ExcelDisplayName("去年同期(kW·h)")]
        public double last { get; set; }

        [ExcelDisplayName("同比增量(kW·h)")]
        public double increase { get; set; }

        [ExcelDisplayName("同比增幅")]
        public string rate { get; set; }
    }
}