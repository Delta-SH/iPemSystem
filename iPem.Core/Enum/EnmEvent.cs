namespace iPem.Core.Enum {
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum EnmEventLevel {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum EnmEventType {
        Login = 100,
        Logout = 101,
        Control = 200,
        Adjust = 201,
        Other = 999
    }

    /// <summary>
    /// Fsu操作类型
    /// </summary>
    public enum EnmFsuEvent {
        Undefined,
        FTP,
        FSU
    }
}
