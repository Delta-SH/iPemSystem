using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400205 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

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