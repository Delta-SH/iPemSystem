using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeServerModel {
        [ExcelDisplayName("CPU")]
        public double cpu { get; set; }

        [ExcelDisplayName("内存")]
        public double memory { get; set; }

        [ExcelDisplayName("时间")]
        public string time { get; set; }
    }
}