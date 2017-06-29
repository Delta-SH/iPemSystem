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
        Login,
        Logout,
        Operating,
        Exception,
        Other
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
