using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400405 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("分路编号")]
        public string name { get; set; }

        [ExcelDisplayName("上月底数据")]
        public double last { get; set; }

        [ExcelDisplayName("本月底数据")]
        public double current { get; set; }

        [ExcelDisplayName("本月底数据(备)")]
        public double currentB { get; set; }

        [ExcelDisplayName("本月底数据(主)")]
        public double currentA { get; set; }

        [ExcelDisplayName("本月用电量(Kwh)")]
        public double total { get; set; }
    }
}