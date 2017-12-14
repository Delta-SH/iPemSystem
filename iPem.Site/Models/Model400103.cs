using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400103 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("机房名称")]
        public string name { get; set; }

        [ExcelDisplayName("机房类型")]
        public string type { get; set; }

        [ExcelDisplayName("产权")]
        public string property { get; set; }

        [ExcelDisplayName("地址")]
        public string address { get; set; }

        [ExcelDisplayName("楼层")]
        public int? floor { get; set; }

        [ExcelDisplayName("长度")]
        public string length { get; set; }

        [ExcelDisplayName("宽度")]
        public string width { get; set; }

        [ExcelDisplayName("高度")]
        public string height { get; set; }

        [ExcelDisplayName("楼面荷载")]
        public string floorLoad { get; set; }

        [ExcelDisplayName("走线架高度")]
        public string lineHeigth { get; set; }

        [ExcelDisplayName("机房面积")]
        public string square { get; set; }

        [ExcelDisplayName("可使用面积")]
        public string effeSquare { get; set; }

        [ExcelDisplayName("消防设备")]
        public string fireFighEuip { get; set; }

        [ExcelDisplayName("业主联系人")]
        public string owner { get; set; }

        [ExcelDisplayName("查询电话")]
        public string queryPhone { get; set; }

        [ExcelDisplayName("动力代维")]
        public string powerSubMain { get; set; }

        [ExcelDisplayName("传输代维")]
        public string tranSubMain { get; set; }

        [ExcelDisplayName("环境代维")]
        public string enviSubMain { get; set; }

        [ExcelDisplayName("消防代维")]
        public string fireSubMain { get; set; }

        [ExcelDisplayName("空调代维")]
        public string airSubMain { get; set; }

        [ExcelDisplayName("维护负责人")]
        public string contact { get; set; }

        [ExcelDisplayName("描述")]
        public string comment { get; set; }

        [ExcelDisplayName("状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }
    }
}