using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400106 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("姓名")]
        public string name { get; set; }

        [ExcelDisplayName("性别")]
        public string sex { get; set; }

        [ExcelDisplayName("部门")]
        public string dept { get; set; }

        [ExcelDisplayName("身份证号")]
        public string icard { get; set; }

        [ExcelDisplayName("身份证住址")]
        public string icardAddress { get; set; }

        [ExcelDisplayName("现住地址")]
        public string address { get; set; }

        [ExcelDisplayName("公司名称")]
        public string company { get; set; }

        [ExcelDisplayName("所属项目")]
        public string project { get; set; }

        [ExcelDisplayName("办公电话")]
        public string workPhone { get; set; }

        [ExcelDisplayName("移动电话")]
        public string mobilePhone { get; set; }

        [ExcelDisplayName("邮箱")]
        public string email { get; set; }

        [ExcelDisplayName("负责人")]
        public string empName { get; set; }

        [ExcelDisplayName("备注")]
        public string remarks { get; set; }

        [ExcelDisplayName("状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelIgnore]
        public string cardId { get; set; }

        [ExcelDisplayName("关联卡号")]
        public string decimalCard { get; set; }

        [ExcelDisplayName("授权设备")]
        public int devCount { get; set; }
    }
}