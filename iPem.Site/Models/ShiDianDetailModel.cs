using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ShiDianDetailModel {
        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("结束时间")]
        public string end { get; set; }

        [ExcelDisplayName("历时(分钟)")]
        public double interval { get; set; }
    }
}