using iPem.Core.NPOI;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400201 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备类型")]
        public string devType { get; set; }

        [ExcelDisplayName("设备名称")]
        public string devName { get; set; }

        [ExcelDisplayName("逻辑分类")]
        public string logic { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("信号类型")]
        public string type { get; set; }

        [ExcelDisplayName("信号测值")]
        public string value { get; set; }

        [ExcelDisplayName("测值时间")]
        public string timestamp { get; set; }

        [ExcelIgnore]
        public int status { get; set; }

        [ExcelColor]
        [ExcelDisplayName("信号状态")]
        public string statusDisplay { get; set; }

        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}