using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model500204 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("区域名称")]
        public string name { get; set; }

        [ExcelDisplayName("区域类型")]
        public string type { get; set; }

        [ExcelDisplayName("开关电源带载率合格套数")]
        public int count { get; set; }

        [ExcelDisplayName("开关电源总套数")]
        public int total { get; set; }

        [ExcelDisplayName("开关电源带载合格率")]
        public string rate { get; set; }
    }
}