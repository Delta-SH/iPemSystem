using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeAlmModel {
        [ExcelDisplayName("全部告警")]
        public int total { get; set; }

        [ExcelDisplayName("一级告警")]
        public int total1 { get; set; }

        [ExcelDisplayName("二级告警")]
        public int total2 { get; set; }

        [ExcelDisplayName("三级告警")]
        public int total3 { get; set; }

        [ExcelDisplayName("四级告警")]
        public int total4 { get; set; }

        [ExcelDisplayName("区域告警")]
        public List<HomeAreaAlmModel> alarms { get; set; }
    }
}