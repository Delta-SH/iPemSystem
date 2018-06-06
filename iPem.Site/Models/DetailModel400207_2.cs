using iPem.Core.Domain.Rs;
using iPem.Core.NPOI;
using System;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class DetailModel400207_2 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("机房名称")]
        public string name { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("结束时间")]
        public string end { get; set; }

        [ExcelDisplayName("停电时长")]
        public string interval { get; set; }
    }
}