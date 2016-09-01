using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400207 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("停电次数")]
        public int count { get; set; }

        [ExcelDisplayName("停电时长(分钟)")]
        public double interval { get; set; }

        [ExcelIgnore]
        public IEnumerable<ShiDianDetailModel> details { get; set; }
    }
}