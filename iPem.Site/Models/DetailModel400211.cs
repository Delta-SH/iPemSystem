using iPem.Core;
using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class DetailModel400211 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("电池组名")]
        public string name { get; set; }

        [ExcelDisplayName("电池组号")]
        public int pack { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("结束时间")]
        public string end { get; set; }

        [ExcelDisplayName("放电时长")]
        public string interval { get; set; }

        [ExcelIgnore]
        public string proctime { get; set; }
    }
}