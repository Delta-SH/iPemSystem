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
        Confirm
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
    /// 0-遥信信号（DI）
    /// 1-遥测信号（AI）
    /// 2-遥控信号（DO）
    /// 3-遥调信号（AO）
    /// </remarks>
    public enum EnmPoint {
        DI,
        AI,
        DO,
        AO
    }

    /// <summary>
    /// Represents the point status enumeration
    /// </summary>
    public enum EnmPointStatus {
        Normal,
        Level1,
        Level2,
        Level3,
        Level4,
        Opevent,
        Invalid
    }

    /// <summary>
    /// Represents the alarm level enumeration
    /// </summary>
    public enum EnmAlarmLevel {
        NoAlarm,
        Level1,
        Level2,
        Level3,
        Level4
    }

    /// <summary>
    /// Represents the alarm flag enumeration
    /// </summary>
    public enum EnmAlarmFlag {
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
    /// Represents the alarm confirm status enumeration
    /// </summary>
    public enum EnmConfirmStatus {
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
}
