using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ProjectModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("工程标识")]
        public string id { get; set; }

        [ExcelDisplayName("工程名称")]
        public string name { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("结束时间")]
        public string end { get; set; }

        [ExcelDisplayName("负责人员")]
        public string responsible { get; set; }

        [ExcelDisplayName("联系电话")]
        public string contact { get; set; }

        [ExcelDisplayName("施工公司")]
        public string company { get; set; }

        [ExcelDisplayName("创建人员")]
        public string creator { get; set; }

        [ExcelDisplayName("创建时间")]
        public string createdtime { get; set; }

        [ExcelDisplayName("备注信息")]
        public string comment { get; set; }

        [ExcelDisplayName("工程状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }
    }
}