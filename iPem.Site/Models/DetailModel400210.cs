using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class DetailModel400210 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备名称")]
        public string device { get; set; }

        [ExcelDisplayName("刷卡类型")]
        public string recType { get; set; }

        [ExcelDisplayName("刷卡描述")]
        public string remark { get; set; }

        [ExcelIgnore]
        public string cardId { get; set; }

        [ExcelDisplayName("刷卡卡号")]
        public string decimalCard { get; set; }

        [ExcelDisplayName("刷卡时间")]
        public string time { get; set; }
    }
}