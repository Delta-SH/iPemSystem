﻿using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class ActAlmModel {
        [ExcelDisplayName("序号")]
        public int id { get; set; }

        [ExcelDisplayName("告警级别")]
        public string level { get; set; }

        [ExcelIgnore]
        public int levelValue { get; set; }

        [ExcelDisplayName("告警时间")]
        public string start { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("设备类型")]
        public string devType { get; set; }

        [ExcelDisplayName("设备名称")]
        public string device { get; set; }

        [ExcelDisplayName("逻辑分类")]
        public string logic { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("告警描述")]
        public string comment { get; set; }

        [ExcelDisplayName("触发值")]
        public string value { get; set; }

        [ExcelDisplayName("触发频次")]
        public int frequency { get; set; }

        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}