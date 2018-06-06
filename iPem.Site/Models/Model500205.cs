using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500205 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("蓄电池后备时长合格基站数量")]
        public int count { get; set; }

        [ExcelDisplayName("已完成放电基站数量")]
        public int total { get; set; }

        [ExcelDisplayName("蓄电池后备时长合格率")]
        public string rate { get; set; }
    }
}