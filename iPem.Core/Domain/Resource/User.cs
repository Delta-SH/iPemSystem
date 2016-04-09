using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Resource {
    // <summary>
    /// Represents an user
    /// </summary>
    public partial class User : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public int PwdFormat { get; set; }

        /// <summary>
        /// Gets or sets the password salt
        /// </summary>
        public string PwdSalt { get; set; }

        /// <summary>
        /// Gets or sets the operation level
        /// </summary>
        public int OpLevel { get; set; }

        /// <summary>
        /// Gets or sets the online time
        /// </summary>
        public DateTime OnlineTime { get; set; }

        /// <summary>
        /// Gets or sets the limit time
        /// </summary>
        public DateTime LimitTime { get; set; }

        /// <summary>
        /// Gets or sets the created time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last login datetime
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets the last password changed time
        /// </summary>
        public DateTime LastPwdChangedTime { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt count
        /// </summary>
        public int FailedPwdAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password datetime
        /// </summary>
        public DateTime FailedPwdTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been locked out.
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the last lockout datetime
        /// </summary>
        public DateTime LastLockoutTime { get; set; }

        /// <summary>
        /// Gets or sets the remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier
        /// </summary>
        public string EmpId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
