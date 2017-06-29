using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FsuEventModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("编号")]
        public string code { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("所属厂家")]
        public string vendor { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("日志类型")]
        public string type { get; set; }

        [ExcelDisplayName("日志信息")]
        public string message { get; set; }

        [ExcelDisplayName("日志时间")]
        public string time { get; set; }
    }
}