using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FormulaModel {
        [ExcelDisplayName("空调电量公式")]
        public string ktFormulas { get; set; }

        [ExcelDisplayName("空调电量公式备注")]
        public string ktRemarks { get; set; }

        [ExcelDisplayName("照明电量公式")]
        public string zmFormulas { get; set; }

        [ExcelDisplayName("照明电量公式备注")]
        public string zmRemarks { get; set; }

        [ExcelDisplayName("办公电量公式")]
        public string bgFormulas { get; set; }

        [ExcelDisplayName("办公电量公式备注")]
        public string bgRemarks { get; set; }

        [ExcelDisplayName("设备电量公式")]
        public string sbFormulas { get; set; }

        [ExcelDisplayName("设备电量公式备注")]
        public string sbRemarks { get; set; }

        [ExcelDisplayName("开关电源电量公式")]
        public string kgdyFormulas { get; set; }

        [ExcelDisplayName("开关电源电量公式备注")]
        public string kgdyRemarks { get; set; }

        [ExcelDisplayName("UPS电量公式")]
        public string upsFormulas { get; set; }

        [ExcelDisplayName("UPS电量公式备注")]
        public string upsRemarks { get; set; }

        [ExcelDisplayName("其他电量公式")]
        public string qtFormulas { get; set; }

        [ExcelDisplayName("其他电量公式备注")]
        public string qtRemarks { get; set; }

        [ExcelDisplayName("总电量公式")]
        public string zlFormulas { get; set; }

        [ExcelDisplayName("总电量公式备注")]
        public string zlRemarks { get; set; }

        [ExcelDisplayName("室内温度公式")]
        public string snwdFormulas { get; set; }

        [ExcelDisplayName("室内温度公式备注")]
        public string snwdRemarks { get; set; }

        [ExcelDisplayName("室内湿度公式")]
        public string snsdFormulas { get; set; }

        [ExcelDisplayName("室内湿度公式备注")]
        public string snsdRemarks { get; set; }

        [ExcelDisplayName("PUE公式")]
        public string pueFormulas { get; set; }

        [ExcelDisplayName("PUE公式备注")]
        public string pueRemarks { get; set; }
    }
}