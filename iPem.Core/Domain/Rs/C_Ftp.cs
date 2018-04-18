using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// FTP信息表
    /// </summary>
    [Serializable]
    public partial class C_Ftp : BaseEntity {
        /// <summary>
        /// FTP编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// FTP名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// FTP类型
        /// 0 表示FTP升级服务器
        /// </summary>
        public EnmFtp Type { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// FTP工作目录
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
