using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400205 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("工程数量")]
        public int count { get; set; }

        [ExcelDisplayName("平均历时")]
        public string interval { get; set; }

        [ExcelDisplayName("超时工程数量")]
        public int timeout { get; set; }

        [ExcelDisplayName("超时工程占比")]
        public string rate { get; set; }

        [ExcelIgnore]
        public List<ProjectModel> projects { get; set; }
    }
}