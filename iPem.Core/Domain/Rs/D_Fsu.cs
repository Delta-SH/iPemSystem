using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Fsu信息表
    /// </summary>
    [Serializable]
    public partial class D_Fsu : D_Device {
        /// <summary>
        /// SC厂家编号
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// SC厂家名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FtpUid { get; set; }

        /// <summary>
        /// FTP密码
        /// </summary>
        public string FtpPwd { get; set; }

        /// <summary>
        /// FTP文件路径
        /// </summary>
        public string FtpFilePath { get; set; }

        /// <summary>
        /// FTP权限
        /// </summary>
        public int FtpAuthority { get; set; }
    }
}
