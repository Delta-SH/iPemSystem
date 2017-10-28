using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Web.Script.Serialization;

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

        [JsonIgnore]
        [ScriptIgnore]
        [ExcelDisplayName("详细信息")]
        public string fullMessage { get; set; }

        [ExcelDisplayName("客户端IP")]
        public string ip { get; set; }

        [ExcelDisplayName("请求URL")]
        public string page { get; set; }

        [ExcelDisplayName("关联URL")]
        public string referrer { get; set; }

        [ExcelDisplayName("用户名称")]
        public string user { get; set; }

        [ExcelDisplayName("记录时间")]
        public string created { get; set; }
    }
}