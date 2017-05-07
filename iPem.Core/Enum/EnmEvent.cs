namespace iPem.Core.Enum {
    /// <summary>
    /// Represents an event level
    /// </summary>
    public enum EnmEventLevel {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// Represents an event type
    /// </summary>
    public enum EnmEventType {
        Login,
        Logout,
        Operating,
        Exception,
        Other
    }

    public enum EnmFtpEvent {
        Undefined,
        FTP
    }
}
