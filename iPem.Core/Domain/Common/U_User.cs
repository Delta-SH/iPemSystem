using System;
using iPem.Core.Enum;

namespace iPem.Core.Domain.Common {
    /// <summary>
    /// 用户信息表
    /// </summary>
    [Serializable]
    public partial class U_User : BaseEntity {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码格式(0未加密，1加密)
        /// </summary>
        public EnmPasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// 散列盐
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 限制时间
        /// </summary>
        public DateTime LimitedDate { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// 最后密码修改时间
        /// </summary>
        public DateTime LastPasswordChangedDate { get; set; }

        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// 登录失败时间
        /// </summary>
        public DateTime FailedPasswordDate { get; set; }

        /// <summary>
        /// 是否被锁定
        /// </summary>
        public Boolean IsLockedOut { get; set; }

        /// <summary>
        /// 上次锁定时间
        /// </summary>
        public DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 关联员工编号
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean Enabled { get; set; }

    }
}
