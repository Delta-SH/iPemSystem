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

        [ExcelDisplayName("开关电源能耗")]
        public string dyFormulas { get; set; }

        [ExcelDisplayName("开关电源运算方式")]
        public int dyCompute { get; set; }

        [ExcelDisplayName("开关电源能耗备注")]
        public string dyRemarks { get; set; }

        [ExcelDisplayName("UPS能耗")]
        public string upsFormulas { get; set; }

        [ExcelDisplayName("UPS运算方式")]
        public int upsCompute { get; set; }

        [ExcelDisplayName("UPS能耗备注")]
        public string upsRemarks { get; set; }

        [ExcelDisplayName("IT设备能耗")]
        public string itFormulas { get; set; }

        [ExcelDisplayName("IT设备运算方式")]
        public int itCompute { get; set; }

        [ExcelDisplayName("IT设备能耗备注")]
        public string itRemarks { get; set; }

        [ExcelDisplayName("其他能耗")]
        public string qtFormulas { get; set; }

        [ExcelDisplayName("其他能耗运算方式")]
        public int qtCompute { get; set; }

        [ExcelDisplayName("其他能耗备注")]
        public string qtRemarks { get; set; }

        [ExcelDisplayName("总能耗")]
        public string ttFormulas { get; set; }

        [ExcelDisplayName("总能耗运算方式")]
        public int ttCompute { get; set; }

        [ExcelDisplayName("总能耗备注")]
        public string ttRemarks { get; set; }

        [ExcelDisplayName("停电标识")]
        public string tdFormulas { get; set; }

        [ExcelDisplayName("停电备注")]
        public string tdRemarks { get; set; }

        [ExcelDisplayName("温度标识")]
        public string wdFormulas { get; set; }

        [ExcelDisplayName("湿度标识")]
        public string sdFormulas { get; set; }

        [ExcelDisplayName("温度备注")]
        public string wdRemarks { get; set; }

        [ExcelDisplayName("发电标识")]
        public string fdFormulas { get; set; }

        [ExcelDisplayName("油机发电量")]
        public string yjFormulas { get; set; }

        [ExcelDisplayName("发电量运算方式")]
        public int yjCompute { get; set; }

        [ExcelDisplayName("发电量备注")]
        public string yjRemarks { get; set; }

        [ExcelDisplayName("变压器能耗")]
        public string byFormulas { get; set; }

        [ExcelDisplayName("变压器运算方式")]
        public int byCompute { get; set; }

        [ExcelDisplayName("线损能耗")]
        public string xsFormulas { get; set; }

        [ExcelDisplayName("线损运算方式")]
        public int xsCompute { get; set; }

        [ExcelDisplayName("变压器备注")]
        public string byRemarks { get; set; }
    }
}