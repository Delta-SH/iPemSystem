﻿using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models
{
    [Serializable]
    public class RedundantAlmModel
    {
        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("告警编号")]
        public string serialno { get; set; }

        [ExcelColor]
        [ExcelDisplayName("告警级别")]
        public string level { get; set; }

        [ExcelDisplayName("告警时间")]
        public string time { get; set; }

        [ExcelDisplayName("告警标准化名称")]
        public string name { get; set; }

        [ExcelDisplayName("管理编号")]
        public string nmalarmid { get; set; }

        [ExcelDisplayName("告警历时")]
        public string interval { get; set; }

        [ExcelDisplayName("信号名称")]
        public string point { get; set; }

        [ExcelDisplayName("所属设备")]
        public string device { get; set; }

        [ExcelDisplayName("所属机房")]
        public string room { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelDisplayName("代维公司")]
        public string supporter { get; set; }

        [ExcelDisplayName("代维负责人")]
        public string manager { get; set; }

        [ExcelDisplayName("确认状态")]
        public string confirmed { get; set; }

        [ExcelDisplayName("确认人员")]
        public string confirmer { get; set; }

        [ExcelDisplayName("确认时间")]
        public string confirmedtime { get; set; }

        [ExcelDisplayName("工程状态")]
        public string reservation { get; set; }

        [ExcelDisplayName("翻转告警")]
        public string reversal { get; set; }

        [ExcelDisplayName("主次告警")]
        public string primary { get; set; }

        [ExcelDisplayName("关联告警")]
        public string related { get; set; }

        [ExcelDisplayName("过滤告警")]
        public string filter { get; set; }

        [ExcelDisplayName("屏蔽告警")]
        public string masked { get; set; }

        [ExcelDisplayName("入库时间")]
        public string createdtime { get; set; }

        [ExcelIgnore]
        public int levelid { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        [ExcelBackground]
        public Color background { get; set; }
    }
}