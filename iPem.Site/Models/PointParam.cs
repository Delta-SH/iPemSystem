using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class PointParam {
        [ExcelDisplayName("序号")]
        public long index { get; set; }

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

        [ExcelDisplayName("参数类型")]
        public string type { get; set; }

        [ExcelColor]
        [ExcelDisplayName("当前参数")]
        public string current { get; set; }

        [ExcelDisplayName("标准参数")]
        public string normal { get; set; }

        [ExcelIgnore]
        public bool diff { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}