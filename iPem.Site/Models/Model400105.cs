using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400105 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("工号")]
        public string empNo { get; set; }

        [ExcelDisplayName("姓名")]
        public string name { get; set; }

        [ExcelDisplayName("英文名")]
        public string engName { get; set; }

        [ExcelDisplayName("曾用名")]
        public string usedName { get; set; }

        [ExcelDisplayName("性别")]
        public string sex { get; set; }

        [ExcelDisplayName("部门")]
        public string dept { get; set; }

        [ExcelDisplayName("职务")]
        public string duty { get; set; }

        [ExcelDisplayName("身份证号")]
        public string icard { get; set; }

        [ExcelDisplayName("出生日期")]
        public string birthday { get; set; }

        [ExcelDisplayName("学位")]
        public string degree { get; set; }

        [ExcelDisplayName("婚姻状况")]
        public string marriage { get; set; }

        [ExcelDisplayName("国籍")]
        public string nation { get; set; }

        [ExcelDisplayName("省份")]
        public string provinces { get; set; }

        [ExcelDisplayName("籍贯")]
        public string native { get; set; }

        [ExcelDisplayName("地址")]
        public string address { get; set; }

        [ExcelDisplayName("邮编")]
        public string postalCode { get; set; }

        [ExcelDisplayName("座机电话")]
        public string addrPhone { get; set; }

        [ExcelDisplayName("办公电话")]
        public string workPhone { get; set; }

        [ExcelDisplayName("移动电话")]
        public string mobilePhone { get; set; }

        [ExcelDisplayName("邮箱")]
        public string email { get; set; }

        [ExcelDisplayName("任职状态")]
        [ExcelBooleanNameAttribute(True = "离职", False = "在职")]
        public bool leaving { get; set; }

        [ExcelDisplayName("入职时间")]
        public string entryTime { get; set; }

        [ExcelDisplayName("退休时间")]
        public string retireTime { get; set; }

        [ExcelDisplayName("编制")]
        [ExcelBooleanNameAttribute(True = "正式员工", False = "非正式员工")]
        public bool isFormal { get; set; }

        [ExcelDisplayName("备注")]
        public string remarks { get; set; }

        [ExcelDisplayName("状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelDisplayName("关联卡号")]
        public string cardId { get; set; }

        [ExcelIgnore]
        public string cardHex { get; set; }

        [ExcelDisplayName("授权设备")]
        public int devCount { get; set; }
    }
}