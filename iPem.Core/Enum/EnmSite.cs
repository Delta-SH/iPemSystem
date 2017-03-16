namespace iPem.Core.Enum {
    /// <summary>
    /// Represents the result enumeration
    /// </summary>
    public enum EnmResult {
        Failure,
        Success
    }

    /// <summary>
    /// Represents ajax action enumeration
    /// </summary>
    public enum EnmAction {
        Add,
        Edit,
        Delete
    }

    /// <summary>
    /// Represents the operation enumeration
    /// </summary>
    public enum EnmOperation {
        Control,
        Adjust,
        Confirm,
        Threshold
    }

    /// <summary>
    /// Represents the organization enumeration
    /// </summary>
    public enum EnmOrganization {
        Area,
        Station,
        Room,
        Device,
        Point
    }

    /// <summary>
    /// Represents the point enumeration
    /// </summary>
    /// <remarks>
    /// 4-遥信信号（DI）
    /// 3-遥测信号（AI）
    /// 1-遥控信号（DO）
    /// 2-遥调信号（AO）
    /// </remarks>
    public enum EnmPoint {
        DI=4,
        AI=3,
        DO=1,
        AO=2
    }

    /// <summary>
    /// Represents the point state enumeration
    /// </summary>
    public enum EnmState {
        Normal,
        Invalid
    }

    /// <summary>
    /// Represents the alarm level enumeration
    /// </summary>
    public enum EnmLevel {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4
    }

    /// <summary>
    /// Represents the alarm flag enumeration
    /// </summary>
    public enum EnmFlag {
        Begin,
        End
    }

    /// <summary>
    /// Represents the alarm end type enumeration
    /// </summary>
    /// <remarks>
    /// Normal - 正常结束
    /// UpLevel - 升级结束
    /// Filter - 过滤结束
    /// Mask - 手动屏蔽结束
    /// NodeRemove - 节点删除
    /// DeviceRemove - 设备删除
    /// </remarks>
    public enum EnmAlarmEndType {
        Normal,
        UpLevel,
        Filter,
        Mask,
        NodeRemove,
        DeviceRemove
    }

    /// <summary>
    /// Represents the alarm confirm enumeration
    /// </summary>
    public enum EnmConfirm {
        Unconfirmed,
        Confirmed
    }

    /// <summary>
    /// Represents the human resourse enumeration
    /// </summary>
    public enum EnmHR {
        Department,
        Employee
    }

    /// <summary>
    /// Represents the device type tree enumeration
    /// </summary>
    public enum EnmDeviceTypeTree {
        DevType,
        SubDevType
    }

    /// <summary>
    /// Represents the logic tree enumeration
    /// </summary>
    public enum EnmLogicTree {
        DevType,
        Logic,
        SubLogic
    }

    /// <summary>
    /// Represents the point tree enumeration
    /// </summary>
    public enum EnmPointTree {
        DevType,
        SubDevType,
        Point
    }

    /// <summary>
    /// Represents the period enumeration
    /// </summary>
    public enum EnmPeriod {
        Year,
        Month,
        Week,
        Day
    }
}
