using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500202 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("本月监控设备数量")]
        public int current { get; set; }

        [ExcelDisplayName("上月监控设备数量")]
        public int last { get; set; }

        [ExcelDisplayName("关键监控测点接入率")]
        public string rate { get; set; }
    }
}