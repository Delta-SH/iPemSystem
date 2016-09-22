using iPem.Core.NPOI;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400403 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string fsuid { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("告警级别")]
        public string level { get; set; }

        [ExcelIgnore]
        public int levelid { get; set; }

        [ExcelDisplayName("开始时间")]
        public string startDate { get; set; }

        [ExcelDisplayName("结束时间")]
        public string endDate { get; set; }

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

        [ExcelDisplayName("触发值")]
        public string startValue { get; set; }

        [ExcelDisplayName("结束值")]
        public string endValue { get; set; }

        [ExcelDisplayName("告警描述")]
        public string comment { get; set; }

        [ExcelDisplayName("告警历时")]
        public string interval { get; set; }

        [ExcelDisplayName("触发频次")]
        public int frequency { get; set; }

        [ExcelDisplayName("结束方式")]
        public string endType { get; set; }

        [ExcelDisplayName("确认状态")]
        public string confirmed { get; set; }

        [ExcelDisplayName("确认人员")]
        public string confirmer { get; set; }

        [ExcelDisplayName("确认时间")]
        public string confirmedtime { get; set; }

        [ExcelDisplayName("工程预约")]
        public string project { get; set; }
    }
}