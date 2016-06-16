using iPem.Core.NPOI;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400303 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("开始时间")]
        public string start { get; set; }

        [ExcelDisplayName("电池测值")]
        public string value { get; set; }

        [ExcelDisplayName("测值时间")]
        public string time { get; set; }
    }
}