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
    /// 门禁卡片类型
    /// </summary>
    public enum EnmCardType {
        /// <summary>
        /// 正式卡
        /// </summary>
        Formal,
        /// <summary>
        /// 临时卡
        /// </summary>
        Temporary
    }

    /// <summary>
    /// 门禁卡片状态
    /// </summary>
    public enum EnmCardStatus {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 冻结
        /// </summary>
        Freeze,
        /// <summary>
        /// 挂失
        /// </summary>
        Loss,
        /// <summary>
        /// 注销
        /// </summary>
        Cancel
    }

    /// <summary>
    /// 门禁刷卡备注
    /// </summary>
    public enum EnmRecRemark {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// 刷卡开门记录
        /// </summary>
        Remark0 = 0,
        /// <summary>
        /// 键入用户ID及个人密码开门的记录
        /// </summary>
        Remark1 = 1,
        /// <summary>
        /// 远程(由SU)开门记录
        /// </summary>
        Remark2 = 2,
        /// <summary>
        /// 手动开门记录
        /// </summary>
        Remark3 = 3,
        /// <summary>
        /// 联动开门记录
        /// </summary>
        Remark4 = 4,
        /// <summary>
        /// 报警 (或报警取消) 记录
        /// </summary>
        Remark5 = 5,
        /// <summary>
        /// SM掉电记录
        /// </summary>
        Remark6 = 6,
        /// <summary>
        /// 内部控制参数被修改的记录
        /// </summary>
        Remark7 = 7,
        /// <summary>
        /// 无效的用户卡刷卡记录
        /// </summary>
        Remark8 = 8,
        /// <summary>
        /// 用户卡的有效期已过
        /// </summary>
        Remark9 = 9,
        /// <summary>
        /// 当前时间该用户卡无进入权限
        /// </summary>
        Remark10 = 10,
        /// <summary>
        /// 用户在个人密码确认时，三次全部不正确
        /// </summary>
        Remark11 = 11,
        /// <summary>
        /// 有效的消防联动输入
        /// </summary>
        Remark34 = 34
    }

    /// <summary>
    /// 门禁刷卡类型
    /// </summary>
    public enum EnmRecType {
        /// <summary>
        /// 其他类型
        /// </summary>
        Other = -1,
        /// <summary>
        /// 正常开门
        /// </summary>
        Normal,
        /// <summary>
        /// 非法开门
        /// </summary>
        Illegal,
        /// <summary>
        /// 远程开门
        /// </summary>
        Remote
    }

    /// <summary>
    /// 门禁刷卡方向
    /// </summary>
    public enum EnmDirection {
        /// <summary>
        /// 进入
        /// </summary>
        OutToIn,
        /// <summary>
        /// 外出
        /// </summary>
        InToOut
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
