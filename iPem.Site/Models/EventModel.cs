using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class EventModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("级别")]
        public string level { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("日志摘要")]
        public string shortMessage { get; set; }

        [ExcelDisplayName("详细信息")]
        public string fullMessage { get; set; }

        [ExcelDisplayName("客户端地址")]
        public string ip { get; set; }

        [ExcelDisplayName("页面Url")]
        public string page { get; set; }

        [ExcelDisplayName("相关Url")]
        public string referrer { get; set; }

        [ExcelDisplayName("用户名称")]
        public string user { get; set; }

        [ExcelDisplayName("记录日期")]
        public string created { get; set; }
    }
}