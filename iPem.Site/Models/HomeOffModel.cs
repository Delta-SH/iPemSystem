using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeOffModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属厂家")]
        public string vendor { get; set; }

        [ExcelDisplayName("Fsu名称")]
        public string name { get; set; }

        [ExcelDisplayName("离线时间")]
        public string time { get; set; }

        [ExcelDisplayName("离线时长")]
        public string interval { get; set; }
    }
}