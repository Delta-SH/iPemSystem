namespace iPem.Core.Enum {
    /// <summary>
    /// 结果类型
    /// </summary>
    public enum EnmResult {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefine = -1,
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
        Threshold,
        /// <summary>
        /// 预约审核
        /// </summary>
        Check
    }

    /// <summary>
    /// 系统层级结构
    /// System Hierarchy
    /// </summary>
    public enum EnmSSH {
        /// <summary>
        /// 全部
        /// </summary>
        Root = -1,
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
    /// 告警流水状态
    /// </summary>
    public enum EnmFlag {
        /// <summary>
        /// 开始告警
        /// </summary>
        Begin,
        /// <summary>
        /// 结束告警
        /// </summary>
        End
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
    public enum EnmDTH {
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
        Day,
        Hour
    }

    /// <summary>
    /// 电池状态
    /// </summary>
    public enum EnmBatStatus {
        /// <summary>
        /// 放电
        /// </summary>
        Discharge,
        /// <summary>
        /// 充电
        /// </summary>
        Charge
    }

    public enum EnmBatPoint {
        /// <summary>
        /// 电池总电压信号
        /// </summary>
        DCZDY,
        /// <summary>
        /// 电池总电流信号
        /// </summary>
        DCZDL,
        /// <summary>
        /// 电池电压信号
        /// </summary>
        DCDY,
        /// <summary>
        /// 电池温度信号
        /// </summary>
        DCWD
    }

    /// <summary>
    /// 告警屏蔽类型
    /// </summary>
    public enum EnmMaskType {
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
        /// FSU
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
    /// 参数类型
    /// </summary>
    public enum EnmPointParam {
        AbsThreshold,
        PerThreshold,
        SavedPeriod,
        StorageRefTime,
        AlarmLimit,
        AlarmLevel,
        AlarmDelay,
        AlarmRecoveryDelay,
        AlarmFiltering,
        AlarmInferior,
        AlarmConnection,
        AlarmReversal
    }

    /// <summary>
    /// 用户自定义参数类型
    /// </summary>
    public enum EnmProfile {
        /// <summary>
        /// 用户关注信号
        /// </summary>
        Follow,
        /// <summary>
        /// 综合测值模版
        /// </summary>
        Matrix,
        /// <summary>
        /// 告警筛选条件
        /// </summary>
        Condition,
        /// <summary>
        /// 告警语音播报
        /// </summary>
        Speech
    }

    /// <summary>
    /// FTP类型
    /// </summary>
    public enum EnmFtp {
        /// <summary>
        /// FTP主服务器
        /// </summary>
        Master
    }

    /// <summary>
    /// FSU升级状态
    /// </summary>
    public enum EnmUpgradeStatus {
        Ready,
        Running,
        Success,
        Failure
    }

    /// <summary>
    /// 角色功能
    /// </summary>
    public enum EnmRoleConfig {
        /// <summary>
        /// 短信功能
        /// </summary>
        SMS = 1,
        /// <summary>
        /// 语音功能
        /// </summary>
        Voice = 2
    }

    /// <summary>
    /// 虚拟信号分类
    /// </summary>
    public enum EnmVSignalCategory {
        /// <summary>
        /// 普通虚拟信号
        /// </summary>
        Category01 = 1001,
        /// <summary>
        /// 能耗虚拟信号
        /// </summary>
        Category02 = 1002,
        /// <summary>
        /// 列头柜分路功率
        /// </summary>
        Category03 = 1003,
        /// <summary>
        /// 列头柜分路电流
        /// </summary>
        Category04 = 1004,
        /// <summary>
        /// 列头柜分路电量
        /// </summary>
        Category05 = 1005
    }
}
