﻿using iPem.Core.NPOI;
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

        [ExcelDisplayName("角色备注")]
        public string comment { get; set; }

        [ExcelDisplayName("角色状态")]
        [ExcelBooleanNameAttribute(True = "有效", False = "禁用")]
        public bool enabled { get; set; }

        [ExcelIgnore]
        public string[] menus { get; set; }

        [ExcelIgnore]
        public string[] areas { get; set; }

        [ExcelIgnore]
        public string[] permissions { get; set; }
    }
}