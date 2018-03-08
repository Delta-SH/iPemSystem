using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400202 {
        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelColor]
        [ExcelDisplayName("告警级别")]
        public string level { get; set; }

        [ExcelDisplayName("开始时间")]
        public string starttime { get; set; }

        [ExcelDisplayName("结束时间")]
        public string endtime { get; set; }

        [ExcelDisplayName("告警名称")]
        public string name { get; set; }

        [ExcelDisplayName("管理编号")]
        public string nmalarmid { get; set; }

        [ExcelDisplayName("告警历时")]
        public string interval { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("维护厂家")]
        public string supporter { get; set; }

        [ExcelDisplayName("确认状态")]
        public string confirmed { get; set; }

        [ExcelDisplayName("确认人员")]
        public string confirmer { get; set; }

        [ExcelDisplayName("确认时间")]
        public string confirmedtime { get; set; }

        [ExcelDisplayName("工程状态")]
        public string reservation { get; set; }

        [ExcelDisplayName("告警翻转")]
        public int reversalcount { get; set; }

        [ExcelIgnore]
        public string areaid { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelIgnore]
        public string roomid { get; set; }

        [ExcelIgnore]
        public string fsuid { get; set; }

        [ExcelIgnore]
        public string deviceid { get; set; }

        [ExcelIgnore]
        public string pointid { get; set; }

        [ExcelIgnore]
        public int levelid { get; set; }

        [ExcelIgnore]
        public string reversalid { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}