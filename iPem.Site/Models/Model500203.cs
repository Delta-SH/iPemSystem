﻿using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500203 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("本月标示站点总数")]
        public int current { get; set; }

        [ExcelDisplayName("上月站点总数")]
        public int last { get; set; }

        [ExcelDisplayName("站点标识率")]
        public string rate { get; set; }
    }
}