using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FtpLoginModel {
        [ExcelDisplayName("用户名称")]
        public string user { get; set; }

        [ExcelDisplayName("登录密码")]
        public string password { get; set; }

        [ExcelDisplayName("原始报文")]
        public string package { get; set; }
    }
}