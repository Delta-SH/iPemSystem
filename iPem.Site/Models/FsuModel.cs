using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FsuModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("编号")]
        public string code { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("IP")]
        public string ip { get; set; }

        [ExcelDisplayName("端口")]
        public int port { get; set; }

        [ExcelDisplayName("最后离线时间")]
        public string last { get; set; }

        [ExcelDisplayName("状态改变时间")]
        public string change { get; set; }

        [ExcelDisplayName("状态")]
        public string status { get; set; }

        [ExcelDisplayName("备注")]
        public string comment { get; set; }
    }
}