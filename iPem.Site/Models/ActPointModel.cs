﻿using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class ActPointModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("信号标准化名称")]
        public string name { get; set; }

        [ExcelDisplayName("信号类型")]
        public string type { get; set; }

        [ExcelDisplayName("信号测值")]
        public string value { get; set; }

        [ExcelDisplayName("单位/描述")]
        public string unit { get; set; }

        [ExcelDisplayName("信号状态")]
        public string status { get; set; }

        [ExcelDisplayName("测值时间")]
        public string time { get; set; }

        [ExcelIgnore]
        public string deviceid { get; set; }

        [ExcelIgnore]
        public string pointid { get; set; }

        [ExcelIgnore]
        public int typeid { get; set; }

        [ExcelIgnore]
        public int statusid { get; set; }

        [ExcelIgnore]
        public bool followed { get; set; }

        [ExcelIgnore]
        public bool followedOnly { get; set; }

        [ExcelIgnore]
        public string timestamp { get; set; }
    }
}