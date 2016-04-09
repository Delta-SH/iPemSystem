using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class ActPointModel {
        [ExcelIgnore]
        public string key { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备类型")]
        public string devType { get; set; }

        [ExcelIgnore]
        public string devId { get; set; }

        [ExcelDisplayName("设备名称")]
        public string devName { get; set; }

        [ExcelDisplayName("逻辑分类")]
        public string logic { get; set; }

        [ExcelDisplayName("信号ID")]
        public string id { get; set; }

        [ExcelDisplayName("信号名称")]
        public string name { get; set; }

        [ExcelIgnore]
        public int type { get; set; }

        [ExcelDisplayName("信号类型")]
        public string typeDisplay { get; set; }

        [ExcelIgnore]
        public double value { get; set; }

        [ExcelDisplayName("信号测值")]
        public string valueDisplay { get; set; }

        [ExcelIgnore]
        public int status { get; set; }

        [ExcelDisplayName("信号状态")]
        public string statusDisplay { get; set; }

        [ExcelIgnore]
        public string timestamp { get; set; }
    }
}