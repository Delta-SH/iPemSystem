using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Fsu扩展信息
    /// </summary>
    [Serializable]
    public partial class D_ExtFsu : BaseEntity {
        /// <summary>
        /// Fsu编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// SC组编号
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Fsu IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Fsu端口
        /// </summary>
        public int Port { get; set; }

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

        /// <summary>
        /// Fsu注册时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// Fsu离线时间
        /// </summary>
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态(在线、离线)
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 升级状态
        /// </summary>
        public EnmUpgradeStatus UpgradeStatus { get; set; }

        /// <summary>
        /// 升级结果
        /// </summary>
        public string UpgradeResult { get; set; }

        /// <summary>
        /// 升级时间
        /// </summary>
        public DateTime UpgradeTime { get; set; }

        /// <summary>
        /// 升级人
        /// </summary>
        public string Upgrader { get; set; }
    }
}
