using iPem.Core.Domain.Cs;
using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400204 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        public List<AlmStore<A_HAlarm>> alarms { get; set; }
    }
}