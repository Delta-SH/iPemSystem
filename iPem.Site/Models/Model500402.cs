using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500402 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("超出规定处理时长的设备故障次数")]
        public int count { get; set; }

        [ExcelDisplayName("设备故障总次数")]
        public int total { get; set; }

        [ExcelDisplayName("设备完好率")]
        public string rate { get; set; }
    }
}