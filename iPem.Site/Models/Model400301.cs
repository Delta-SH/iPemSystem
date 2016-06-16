using iPem.Core.NPOI;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400301 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("信号测值")]
        public string value { get; set; }

        [ExcelDisplayName("测值时间")]
        public string time { get; set; }

        [ExcelDisplayName("信号阈值")]
        public string threshold { get; set; }

        [ExcelIgnore]
        public int state { get; set; }

        [ExcelColor]
        [ExcelDisplayName("信号状态")]
        public string stateDisplay { get; set; }

        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}