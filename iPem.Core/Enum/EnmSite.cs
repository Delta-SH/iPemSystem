namespace iPem.Core.Enum {
    /// <summary>
    /// 结果类型
    /// </summary>
    public enum EnmResult {
        /// <summary>
        /// 失败
        /// </summary>
        Failure,
        /// <summary>
        /// 成功
        /// </summary>
        Success
    }

    /// <summary>
    /// 表单操作类型
    /// </summary>
    public enum EnmAction {
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }

    /// <summary>
    /// 信号操作类型
    /// </summary>
    public enum EnmPermission {
        /// <summary>
        /// 遥控
        /// </summary>
        Control,
        /// <summary>
        /// 遥调
        /// </summary>
        Adjust,
        /// <summary>
        /// 告警确认
        /// </summary>
        Confirm,
        /// <summary>
        /// 告警阈值
        /// </summary>
        Threshold
    }

    /// <summary>
    /// 系统层级结构
    /// System Hierarchy
    /// </summary>
    public enum EnmSSH {
        /// <summary>
        /// 区域
        /// </summary>
        Area,
        /// <summary>
        /// 站点
        /// </summary>
        Station,
        /// <summary>
        /// 机房
        /// </summary>
        Room,
        /// <summary>
        /// Fsu
        /// </summary>
        Fsu,
        /// <summary>
        /// 设备
        /// </summary>
        Device,
        /// <summary>
        /// 信号
        /// </summary>
        Point
    }

    /// <summary>
    /// 信号类型
    /// </summary>
    public enum EnmPoint {
        /// <summary>
        /// 告警-0
        /// </summary>
        AL = 0,
        /// <summary>
        /// 遥控-1
        /// </summary>
        DO = 1,
        /// <summary>
        /// 遥调-2
        /// </summary>
        AO = 2,
        /// <summary>
        /// 遥测-3
        /// </summary>
        AI = 3,
        /// <summary>
        /// 遥信-4
        /// </summary>
        DI = 4
    }

    /// <summary>
    /// 信号状态
    /// </summary>
    public enum EnmState {
        /// <summary>
        /// 正常数据
        /// </summary>
        Normal,
        /// <summary>
        /// 一级告警
        /// </summary>
        Level1,
        /// <summary>
        /// 二级告警
        /// </summary>
        Level2,
        /// <summary>
        /// 三级告警
        /// </summary>
        Level3,
        /// <summary>
        /// 四级告警
        /// </summary>
        Level4,
        /// <summary>
        /// 操作事件
        /// </summary>
        Opevent,
        /// <summary>
        /// 无效数据
        /// </summary>
        Invalid
    }

    /// <summary>
    /// 告警等级
    /// </summary>
    public enum EnmAlarm {
        /// <summary>
        /// 无告警
        /// </summary>
        Level0,
        /// <summary>
        /// 一级告警
        /// </summary>
        Level1,
        /// <summary>
        /// 二级告警
        /// </summary>
        Level2,
        /// <summary>
        /// 三级告警
        /// </summary>
        Level3,
        /// <summary>
        /// 四级告警
        /// </summary>
        Level4,
    }

    /// <summary>
    /// 告警确认状态
    /// </summary>
    public enum EnmConfirm {
        /// <summary>
        /// 未确认
        /// </summary>
        Unconfirmed,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed
    }

    /// <summary>
    /// 告警工程状态
    /// </summary>
    public enum EnmReservation {
        /// <summary>
        /// 非工程告警
        /// </summary>
        UnReservation,
        /// <summary>
        /// 工程告警
        /// </summary>
        Reservation
    }

    /// <summary>
    /// 人资层级结构
    /// </summary>
    public enum EnmHRH {
        /// <summary>
        /// 部门
        /// </summary>
        Department,
        /// <summary>
        /// 员工
        /// </summary>
        Employee
    }

    /// <summary>
    /// 设备类型层级结构
    /// </summary>
    public enum EnmDTH{
        /// <summary>
        /// 设备类型
        /// </summary>
        DevType,
        /// <summary>
        /// 设备子类型
        /// </summary>
        SubDevType
    }

    /// <summary>
    /// 逻辑分类层级结构
    /// </summary>
    public enum EnmLTH {
        /// <summary>
        /// 设备类型
        /// </summary>
        DevType,
        /// <summary>
        /// 逻辑分类
        /// </summary>
        Logic,
        /// <summary>
        /// 逻辑子类
        /// </summary>
        SubLogic
    }

    /// <summary>
    /// 信号层级结构
    /// </summary>
    public enum EnmPTH {
        /// <summary>
        /// 设备类型
        /// </summary>
        DevType,
        /// <summary>
        /// 信号
        /// </summary>
        Point
    }

    /// <summary>
    /// 时间层级结构
    /// </summary>
    public enum EnmPDH {
        Year,
        Month,
        Week,
        Day
    }

    /// <summary>
    /// 断站-0
    /// 停电-1
    /// 发电-2
    /// </summary>
    public enum EnmCutType {
        Off,
        Cut,
        Power
    }
}
