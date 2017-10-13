namespace iPem.Core.Enum {
    /// <summary>
    /// 登录密码格式
    /// </summary>
    public enum EnmPasswordFormat {
        /// <summary>
        /// 不加密
        /// </summary>
        Clear,
        /// <summary>
        /// 加密
        /// </summary>
        Hashed
    }

    /// <summary>
    /// 序列号状态
    /// </summary>
    public enum EnmLicenseStatus {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
        /// <summary>
        /// 过期
        /// </summary>
        Expired,
        /// <summary>
        /// 评估
        /// </summary>
        Evaluation,
        /// <summary>
        /// 正常
        /// </summary>
        Licensed
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum EnmSex {
        /// <summary>
        /// 男
        /// </summary>
        Male,
        /// <summary>
        /// 女
        /// </summary>
        Female
    }

    /// <summary>
    /// 人员
    /// </summary>
    public enum EnmEmpType {
        /// <summary>
        /// 正式员工
        /// </summary>
        Employee,
        /// <summary>
        /// 外协人员
        /// </summary>
        OutEmployee
    }

    /// <summary>
    /// 学历
    /// </summary>
    public enum EnmDegree {
        /// <summary>
        /// 高中
        /// </summary>
        High,
        /// <summary>
        /// 大专
        /// </summary>
        College,
        /// <summary>
        /// 本科
        /// </summary>
        Bachelor,
        /// <summary>
        /// 硕士
        /// </summary>
        Master,
        /// <summary>
        /// 博士或博士后
        /// </summary>
        Doctor,
        /// <summary>
        /// 其他
        /// </summary>
        Other
    }

    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum EnmMarriage {
        /// <summary>
        /// 单身
        /// </summary>
        Single,
        /// <summary>
        /// 已婚
        /// </summary>
        Married,
        /// <summary>
        /// 其他
        /// </summary>
        Other
    }

    /// <summary>
    /// 用户登录结果
    /// </summary>
    public enum EnmLoginResults {
        /// <summary>
        /// 登录成功
        /// </summary>
        Successful = 1,
        /// <summary>
        /// 账户不存在
        /// </summary>
        NotExist = 2,
        /// <summary>
        /// 密码错误
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// 账户被禁用
        /// </summary>
        NotEnabled = 4,
        /// <summary>
        /// 账户已过期
        /// </summary>
        Expired = 5,
        /// <summary>
        /// 账户被锁定
        /// </summary>
        Locked = 6,
        /// <summary>
        /// 角色不存在
        /// </summary>
        RoleNotExist = 7,
        /// <summary>
        /// 角色被禁用
        /// </summary>
        RoleNotEnabled = 8
    }

    /// <summary>
    /// 更新密码结果
    /// </summary>
    public enum EnmChangeResults {
        /// <summary>
        /// 更新成功
        /// </summary>
        Successful = 1,
        /// <summary>
        /// 原始密码错误
        /// </summary>
        WrongPassword = 2
    }
}
