using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400208_2 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("设备名称")]
        public string name { get; set; }

        [ExcelDisplayName("设备类型")]
        public string type { get; set; }

        [ExcelDisplayName("发电次数")]
        public int count { get; set; }

        [ExcelDisplayName("发电时长")]
        public string interval { get; set; }

        [ExcelDisplayName("发电量")]
        public string value { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        [ScriptIgnore]
        public List<DetailModel400208> details { get; set; }
    }
}