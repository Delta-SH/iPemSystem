using iPem.Core.Enum;
using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class RoleModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("角色标识")]
        public string id { get; set; }

        [ExcelDisplayName("角色名称")]
        public string name { get; set; }

        [ExcelIgnore]
        public EnmSSH type { get; set; }

        [ExcelDisplayName("角色类型")]
        public string typeName { get; set; }

        [ExcelDisplayName("角色备注")]
        public string comment { get; set; }

        [ExcelDisplayName("角色状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelDisplayName("短信功能")]
        [ExcelBooleanNameAttribute(True = "启用", False = "禁用")]
        public bool sms { get; set; }

        [ExcelDisplayName("语音功能")]
        [ExcelBooleanNameAttribute(True = "启用", False = "禁用")]
        public bool voice { get; set; }

        [ExcelIgnore]
        public string[] menus { get; set; }

        [ExcelIgnore]
        public string[] permissions { get; set; }

        [ExcelIgnore]
        public string[] authorizations { get; set; }

        [ExcelIgnore]
        public int[] SMSLevels { get; set; }

        [ExcelIgnore]
        public string SMSDevices { get; set; }

        [ExcelIgnore]
        public string SMSSignals { get; set; }

        [ExcelIgnore]
        public int[] voiceLevels { get; set; }

        [ExcelIgnore]
        public string voiceDevices { get; set; }

        [ExcelIgnore]
        public string voiceSignals { get; set; }
    }
}