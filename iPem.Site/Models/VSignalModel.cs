using iPem.Core.NPOI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPem.Site.Models {
    [Serializable]
    public class VSignalModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string dev { get; set; }

        [ExcelDisplayName("编码")]
        public string id { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelIgnore]
        public int typevalue { get; set; }

        [ExcelDisplayName("单位/状态")]
        public string unit { get; set; }

        [ExcelDisplayName("存储周期")]
        public int saved { get; set; }

        [ExcelDisplayName("统计周期")]
        public int stats { get; set; }

        [ExcelDisplayName("公式")]
        public string formula { get; set; }

        [ExcelDisplayName("信号分类")]
        public string categoryName { get; set; }

        [ExcelIgnore]
        public int category { get; set; }

        [ExcelDisplayName("备注")]
        public string remark { get; set; }
    }
}