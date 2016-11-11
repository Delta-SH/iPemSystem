using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500208 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("站点通信中断告警总时长")]
        public string almTime { get; set; }

        [ExcelDisplayName("站点总数")]
        public int count { get; set; }

        [ExcelDisplayName("统计时长")]
        public string cntTime { get; set; }

        [ExcelDisplayName("监控故障处理及时率")]
        public string rate { get; set; }
    }
}