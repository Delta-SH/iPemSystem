using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FsuPointModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string fsuid { get; set; }

        [ExcelDisplayName("FSU编码")]
        public string fsucode { get; set; }

        [ExcelDisplayName("所属FSU")]
        public string fsu { get; set; }

        [ExcelDisplayName("所属厂家")]
        public string vendor { get; set; }

        [ExcelIgnore]
        public string deviceid { get; set; }

        [ExcelIgnore]
        public string devicecode { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelIgnore]
        public string pointid { get; set; }

        [ExcelIgnore]
        public string pointcode { get; set; }

        [ExcelIgnore]
        public string pointnumber { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelIgnore]
        public int typeid { get; set; }

        [ExcelDisplayName("信号类型")]
        public string type { get; set; }

        [ExcelDisplayName("绝对阀值")]
        public string absolute { get; set; }

        [ExcelDisplayName("百分比阀值")]
        public string relative { get; set; }

        [ExcelDisplayName("存储时间间隔（分钟）")]
        public string interval { get; set; }

        [ExcelDisplayName("存储参考时间")]
        public string reftime { get; set; }

        [ExcelIgnore]
        public bool remote { get; set; }
    }
}