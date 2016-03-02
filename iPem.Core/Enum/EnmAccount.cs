namespace iPem.Core.Enum {
    /// <summary>
    /// Represents password format enumeration
    /// </summary>
    public enum EnmPasswordFormat {
        Clear,
        Hashed
    }

    /// <summary>
    /// Represents the license status enumeration
    /// </summary>
    public enum EnmLicenseStatus {
        Invalid,
        Expired,
        Evaluation,
        Licensed
    }

    /// <summary>
    /// Represents the employee sex enumeration
    /// </summary>
    public enum EnmSex {
        Male,
        Female
    }

    /// <summary>
    /// Represents the employee degree enumeration
    /// </summary>
    public enum EnmDegree {
        High,
        College,
        Bachelor,
        Master,
        Doctor,
        Other
    }

    /// <summary>
    /// Represents the employee marriage enumeration
    /// </summary>
    public enum EnmMarriage {
        Single,
        Married,
        Other
    }

    /// <summary>
    /// Represents the user login result enumeration
    /// </summary>
    public enum EnmLoginResults {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Account dies not exist
        /// </summary>
        NotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been enabled
        /// </summary>
        NotEnabled = 4,
        /// <summary>
        /// Account has been expired 
        /// </summary>
        Expired = 5,
        /// <summary>
        /// Account has been locked 
        /// </summary>
        Locked = 6,
        /// <summary>
        /// Role dies not exist
        /// </summary>
        RoleNotExist = 7,
        /// <summary>
        /// Role have not been enabled
        /// </summary>
        RoleNotEnabled = 8
    }

    /// <summary>
    /// Represents change password result enumeration
    /// </summary>
    public enum EnmChangeResults {
        /// <summary>
        /// Change successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 2
    }
}
