using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ProjectModel {
        [ExcelDisplayName("序号")]
        public int Index { get; set; }

        [ExcelDisplayName("工程标识")]
        public string Id { get; set; }

        [ExcelDisplayName("工程名称")]
        public string Name { get; set; }

        [ExcelDisplayName("开始时间")]
        public string StartTime { get; set; }

        [ExcelDisplayName("结束时间")]
        public string EndTime { get; set; }

        [ExcelDisplayName("负责人员")]
        public string Responsible { get; set; }

        [ExcelDisplayName("联系电话")]
        public string ContactPhone { get; set; }

        [ExcelDisplayName("施工公司")]
        public string Company { get; set; }

        [ExcelDisplayName("创建人员")]
        public string Creator { get; set; }

        [ExcelDisplayName("创建时间")]
        public string CreatedTime { get; set; }

        [ExcelDisplayName("备注信息")]
        public string Comment { get; set; }

        [ExcelDisplayName("工程状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool Enabled { get; set; }
    }
}