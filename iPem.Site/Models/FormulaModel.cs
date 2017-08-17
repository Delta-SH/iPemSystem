using iPem.Core.Enum;
using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FormulaModel {
        [ExcelDisplayName("空调能耗")]
        public string ktFormulas { get; set; }

        [ExcelDisplayName("空调运算方式")]
        public int ktCompute { get; set; }

        [ExcelDisplayName("空调能耗备注")]
        public string ktRemarks { get; set; }

        [ExcelDisplayName("照明能耗")]
        public string zmFormulas { get; set; }

        [ExcelDisplayName("照明运算方式")]
        public int zmCompute { get; set; }

        [ExcelDisplayName("照明能耗备注")]
        public string zmRemarks { get; set; }

        [ExcelDisplayName("办公能耗")]
        public string bgFormulas { get; set; }

        [ExcelDisplayName("办公运算方式")]
        public int bgCompute { get; set; }

        [ExcelDisplayName("办公能耗备注")]
        public string bgRemarks { get; set; }

        [ExcelDisplayName("IT设备能耗")]
        public string sbFormulas { get; set; }

        [ExcelDisplayName("IT设备运算方式")]
        public int sbCompute { get; set; }

        [ExcelDisplayName("IT设备能耗备注")]
        public string sbRemarks { get; set; }

        [ExcelDisplayName("开关电源能耗")]
        public string kgdyFormulas { get; set; }

        [ExcelDisplayName("开关电源运算方式")]
        public int kgdyCompute { get; set; }

        [ExcelDisplayName("开关电源能耗备注")]
        public string kgdyRemarks { get; set; }

        [ExcelDisplayName("UPS能耗")]
        public string upsFormulas { get; set; }

        [ExcelDisplayName("UPS运算方式")]
        public int upsCompute { get; set; }

        [ExcelDisplayName("UPS能耗备注")]
        public string upsRemarks { get; set; }

        [ExcelDisplayName("其他能耗")]
        public string qtFormulas { get; set; }

        [ExcelDisplayName("其他能耗运算方式")]
        public int qtCompute { get; set; }

        [ExcelDisplayName("其他能耗备注")]
        public string qtRemarks { get; set; }

        [ExcelDisplayName("总能耗")]
        public string zlFormulas { get; set; }

        [ExcelDisplayName("总能耗运算方式")]
        public int zlCompute { get; set; }

        [ExcelDisplayName("总能耗备注")]
        public string zlRemarks { get; set; }
    }
}