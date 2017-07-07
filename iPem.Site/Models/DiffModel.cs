using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class DiffModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("告警门限值")]
        public string threshold { get; set; }

        [ExcelDisplayName("告警等级")]
        public string level { get; set; }

        [ExcelDisplayName("网管告警编号")]
        public string nmid { get; set; }

        [ExcelDisplayName("绝对阀值")]
        public string absolute { get; set; }

        [ExcelDisplayName("百分比阀值")]
        public string relative { get; set; }

        [ExcelDisplayName("存储时间间隔")]
        public string interval { get; set; }

        [ExcelDisplayName("存储参考时间")]
        public string reftime { get; set; }

        [ExcelDisplayName("屏蔽信号")]
        public string masked { get; set; }
    }
}