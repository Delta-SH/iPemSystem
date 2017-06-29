using iPem.Core.Domain.Cs;
using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400203 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("一级告警")]
        public int level1 { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms1 { get; set; }

        [ExcelDisplayName("二级告警")]
        public int level2 { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms2 { get; set; }

        [ExcelDisplayName("三级告警")]
        public int level3 { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms3 { get; set; }

        [ExcelDisplayName("四级告警")]
        public int level4 { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms4 { get; set; }

        [ExcelDisplayName("总计")]
        public int total { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms { get; set; }
    }
}