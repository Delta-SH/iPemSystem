﻿using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FsuModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelIgnore]
        public bool _status { get; set; }

        [ExcelDisplayName("编号")]
        public string code { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("厂家")]
        public string vendor { get; set; }

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

        [ExcelDisplayName("离线时间")]
        public string last { get; set; }

        [ExcelDisplayName("注册时间")]
        public string change { get; set; }

        [ExcelDisplayName("状态")]
        public string status { get; set; }

        [ExcelDisplayName("备注")]
        public string comment { get; set; }

        [ExcelDisplayName("执行状态")]
        public string exestatus { get; set; }

        [ExcelDisplayName("执行说明")]
        public string execomment { get; set; }

        [ExcelDisplayName("执行时间")]
        public string exetime { get; set; }

        [ExcelDisplayName("执行人")]
        public string exer { get; set; }
    }
}