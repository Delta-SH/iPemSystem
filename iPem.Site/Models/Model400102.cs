using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400102 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("站点名称")]
        public string name { get; set; }

        [ExcelDisplayName("站点类型")]
        public string type { get; set; }

        [ExcelDisplayName("经度")]
        public string longitude { get; set; }

        [ExcelDisplayName("纬度")]
        public string latitude { get; set; }

        [ExcelDisplayName("海拔标高")]
        public string altitude { get; set; }

        [ExcelDisplayName("市电引入方式")]
        public string cityelecloadtype { get; set; }

        [ExcelDisplayName("市电容")]
        public string cityeleccap { get; set; }

        [ExcelDisplayName("市电引入")]
        public string cityelecload { get; set; }

        [ExcelDisplayName("维护责任人")]
        public string contact { get; set; }

        [ExcelDisplayName("线径")]
        public string lineradiussize { get; set; }

        [ExcelDisplayName("线缆长度")]
        public string linelength { get; set; }

        [ExcelDisplayName("供电性质")]
        public string supppowertype { get; set; }

        [ExcelDisplayName("转供信息")]
        public string traninfo { get; set; }

        [ExcelDisplayName("供电合同号")]
        public string trancontno { get; set; }

        [ExcelDisplayName("供电站电话")]
        public string tranphone { get; set; }

        [ExcelDisplayName("描述")]
        public string comment { get; set; }

        [ExcelDisplayName("状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }
    }
}