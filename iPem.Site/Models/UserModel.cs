using iPem.Core.Enum;
using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class UserModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelIgnore]
        public string id { get; set; }

        [ExcelDisplayName("用户名称")]
        public string uid { get; set; }

        [ExcelIgnore]
        public string roleId { get; set; }

        [ExcelDisplayName("隶属角色")]
        public string roleName { get; set; }

        [ExcelIgnore]
        public string empId { get; set; }

        [ExcelDisplayName("隶属员工")]
        public string empName { get; set; }

        [ExcelDisplayName("员工工号")]
        public string empNo { get; set; }

        [ExcelIgnore]
        public int sex { get; set; }

        [ExcelDisplayName("性别")]
        public string sexName { get; set; }

        [ExcelDisplayName("联系电话")]
        public string mobile { get; set; }

        [ExcelDisplayName("Email")]
        public string email { get; set; }

        [ExcelIgnore]
        public string password { get; set; }

        [ExcelDisplayName("创建日期")]
        public string created { get; set; }

        [ExcelDisplayName("有效日期")]
        public string limited { get; set; }

        [ExcelDisplayName("最后登录日期")]
        public string lastLogined { get; set; }

        [ExcelDisplayName("密码更改日期")]
        public string lastPasswordChanged { get; set; }

        [ExcelDisplayName("锁定状态")]
        [ExcelBooleanNameAttribute(True = "锁定", False = "正常")]
        public bool isLockedOut { get; set; }

        [ExcelDisplayName("最后锁定日期")]
        public string lastLockedout { get; set; }

        [ExcelDisplayName("用户备注")]
        public string comment { get; set; }

        [ExcelDisplayName("用户状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }
    }
}