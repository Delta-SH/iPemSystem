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
        public string id { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备类型")]
        public string devType { get; set; }

        [ExcelDisplayName("设备名称")]
        public string device { get; set; }

        [ExcelDisplayName("逻辑分类")]
        public string logic { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelIgnore]
        public int levelValue { get; set; }

        [ExcelDisplayName("告警级别")]
        public string levelDisplay { get; set; }

        [ExcelDisplayName("开始时间")]
        public string startTime { get; set; }

        [ExcelDisplayName("结束时间")]
        public string endTime { get; set; }

        [ExcelDisplayName("告警历时")]
        public string interval { get; set; }

        [ExcelDisplayName("触发值")]
        public string startValue { get; set; }

        [ExcelDisplayName("结束值")]
        public string endValue { get; set; }

        [ExcelDisplayName("告警描述")]
        public string almComment { get; set; }

        [ExcelDisplayName("正常描述")]
        public string normalComment { get; set; }

        [ExcelDisplayName("触发频次")]
        public int frequency { get; set; }

        [ExcelDisplayName("结束方式")]
        public string endType { get; set; }

        [ExcelDisplayName("工程预约")]
        public string project { get; set; }

        [ExcelDisplayName("确认状态")]
        public string confirmedStatus { get; set; }

        [ExcelDisplayName("确认时间")]
        public string confirmedTime { get; set; }

        [ExcelDisplayName("确认人员")]
        public string confirmer { get; set; }
    }
}