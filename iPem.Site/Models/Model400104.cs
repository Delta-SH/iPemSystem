using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400104 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备名称")]
        public string name { get; set; }

        [ExcelDisplayName("设备类型")]
        public string type { get; set; }

        [ExcelDisplayName("设备子类")]
        public string subType { get; set; }

        [ExcelDisplayName("系统名称")]
        public string sysName { get; set; }

        [ExcelDisplayName("系统编号")]
        public string sysCode { get; set; }

        [ExcelDisplayName("型号")]
        public string model { get; set; }

        [ExcelDisplayName("生产厂家")]
        public string productor { get; set; }

        [ExcelDisplayName("品牌")]
        public string brand { get; set; }

        [ExcelDisplayName("供应商")]
        public string supplier { get; set; }

        [ExcelDisplayName("维护厂家")]
        public string subCompany { get; set; }

        [ExcelDisplayName("开始使用时间")]
        public string startTime { get; set; }

        [ExcelDisplayName("预计报废时间")]
        public string scrapTime { get; set; }

        [ExcelDisplayName("使用状态")]
        public string status { get; set; }

        [ExcelDisplayName("维护负责人")]
        public string contact { get; set; }

        [ExcelDisplayName("描述")]
        public string comment { get; set; }

        [ExcelDisplayName("状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }
    }
}