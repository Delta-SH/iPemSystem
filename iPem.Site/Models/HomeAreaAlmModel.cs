using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeAreaAlmModel {
        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("全部告警")]
        public int total { get; set; }

        [ExcelDisplayName("一级告警")]
        public int level1 { get; set; }

        [ExcelDisplayName("二级告警")]
        public int level2 { get; set; }

        [ExcelDisplayName("三级告警")]
        public int level3 { get; set; }

        [ExcelDisplayName("四级告警")]
        public int level4 { get; set; }
    }
}