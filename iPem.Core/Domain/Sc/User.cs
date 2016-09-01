using System;
using iPem.Core.Enum;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents an user
    /// </summary>
    [Serializable]
    public partial class User : BaseEntity {
        /// <summary>
        /// Gets or sets the role id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public EnmPasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets the salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the limit date
        /// </summary>
        public DateTime LimitDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the last password changed date
        /// </summary>
        public DateTime LastPasswordChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt count
        /// </summary>
        public int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password date
        /// </summary>
        public DateTime FailedPasswordDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has been locked
        /// </summary>
        public Boolean IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the last lockout date
        /// </summary>
        public DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the employee id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public Boolean Enabled { get; set; }
    }
}
