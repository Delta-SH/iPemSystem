using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ReservationModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("预约名称")]
        public string name { get; set; }

        [ExcelDisplayName("预期开始时间")]
        public string expStartDate { get; set; }

        [ExcelDisplayName("实际开始时间")]
        public string startDate { get; set; }

        [ExcelDisplayName("结束时间")]
        public string endDate { get; set; }

        [ExcelIgnore]
        public string projectId { get; set; }

        [ExcelDisplayName("关联工程")]
        public string projectName { get; set; }

        [ExcelDisplayName("创建人员")]
        public string creator { get; set; }

        [ExcelDisplayName("创建账号")]
        public string user { get; set; }

        [ExcelDisplayName("创建时间")]
        public string createdTime { get; set; }

        [ExcelDisplayName("备注信息")]
        public string comment { get; set; }

        [ExcelDisplayName("预约状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelIgnore]
        public int statusId { get; set; }

        [ExcelDisplayName("审核状态")]
        public string status { get; set; }

        [ExcelIgnore]
        public string[] nodes { get; set; }
    }
}