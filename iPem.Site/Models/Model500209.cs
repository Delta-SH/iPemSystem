using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500209 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("蓄电池1小时以上放电站点总数")]
        public int current { get; set; }

        [ExcelDisplayName("系统站点总数")]
        public int last { get; set; }

        [ExcelDisplayName("蓄电池核对性放电及时率")]
        public string rate { get; set; }
    }
}