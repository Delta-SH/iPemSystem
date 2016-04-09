using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents an user profile
    /// </summary>
    [Serializable]
    public partial class UserProfile {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the value json
        /// </summary>
        public string ValuesJson { get; set; }

        /// <summary>
        /// Gets or sets the value binary
        /// </summary>
        public byte[] ValuesBinary { get; set; }

        /// <summary>
        /// Gets or sets the last updated date
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }
}