using iPem.Core.NPOI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400210 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string employeeCode { get; set; }

        [ExcelDisplayName("刷卡人员")]
        public string employeeName { get; set; }

        [ExcelDisplayName("人员类型")]
        public string employeeType { get; set; }

        [ExcelDisplayName("所属部门")]
        public string department { get; set; }

        [ExcelIgnore]
        public string cardId { get; set; }

        [ExcelDisplayName("刷卡卡号")]
        public string decimalCard { get; set; }

        [ExcelDisplayName("刷卡次数")]
        public int count { get; set; }

        [JsonIgnore]
        [ExcelIgnore]
        [ScriptIgnore]
        public List<DetailModel400210> details { get; set; }
    }
}