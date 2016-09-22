using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500101 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("设备数量")]
        public int devCount { get; set; }

        [ExcelDisplayName("设备告警时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("设备统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("设备完好率")]
        public string rate { get; set; }
    }
}