using iPem.Core.NPOI;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400302 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("结束时间")]
        public string end { get; set; }

        [ExcelIgnore]
        public double maxvalue { get; set; }

        [ExcelDisplayName("最大测值")]
        public string maxdisplay { get; set; }

        [ExcelDisplayName("最大时间")]
        public string maxtime { get; set; }

        [ExcelIgnore]
        public double minvalue { get; set; }

        [ExcelDisplayName("最小测值")]
        public string mindisplay { get; set; }

        [ExcelDisplayName("最小时间")]
        public string mintime { get; set; }

        [ExcelIgnore]
        public double avgvalue { get; set; }

        [ExcelDisplayName("平均测值")]
        public string avgdisplay { get; set; }

        [ExcelDisplayName("统计数量")]
        public int total { get; set; }
    }
}