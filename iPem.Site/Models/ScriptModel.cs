using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ScriptModel {
        [ExcelDisplayName("脚本编码")]
        public string id { get; set; }

        [ExcelDisplayName("脚本名称")]
        public string name { get; set; }

        [ExcelDisplayName("创建人")]
        public string creator { get; set; }

        [ExcelDisplayName("创建时间")]
        public string createdtime { get; set; }

        [ExcelDisplayName("执行人")]
        public string executor { get; set; }

        [ExcelDisplayName("执行时间")]
        public string executedtime { get; set; }

        [ExcelDisplayName("备注")]
        public string comment { get; set; }
    }
}