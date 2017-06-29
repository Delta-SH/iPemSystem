using iPem.Core.NPOI;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400208 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("站点类型")]
        public string type { get; set; }

        [ExcelDisplayName("发电次数")]
        public int count { get; set; }

        [ExcelDisplayName("发电时长")]
        public string interval { get; set; }

        [ScriptIgnore]
        [ExcelIgnore]
        public List<ShiDianModel> details { get; set; }
    }
}