using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class DetailModel400106 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelIgnore]
        public string card { get; set; }

        [ExcelDisplayName("设备名称")]
        public string name { get; set; }
    }
}