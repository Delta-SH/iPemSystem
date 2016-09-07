using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class AppointmentModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("编号")]
        public string id { get; set; }

        [ExcelDisplayName("开始时间")]
        public string startDate { get; set; }

        [ExcelDisplayName("结束时间")]
        public string endDate { get; set; }

        [ExcelIgnore]
        [ExcelDisplayName("工程编号")]
        public string projectId { get; set; }

        [ExcelDisplayName("工程名称")]
        public string projectName { get; set; }

        [ExcelDisplayName("创建人")]
        public string creator { get; set; }

        [ExcelDisplayName("创建时间")]
        public string createdTime { get; set; }

        [ExcelDisplayName("备注信息")]
        public string comment { get; set; }

        [ExcelDisplayName("预约状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelIgnore]
        public string[] nodes { get; set; }
    }
}