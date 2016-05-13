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
    public enum EnmPoint {
        DI,
        AI,
        AO,
        DO
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
        Level1 = 1,
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
    /// Represents the human resourse enumeration
    /// </summary>
    public enum EnmHR {
        Department,
        Employee
    }
}
