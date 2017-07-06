using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeCuttingModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("站点名称")]
        public string station { get; set; }

        [ExcelDisplayName("停电时间")]
        public string time { get; set; }

        [ExcelDisplayName("停电时长")]
        public string interval { get; set; }
    }
}