using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.History {
    /// <summary>
    /// Represents an alarm extend class
    /// </summary>
    [Serializable]
    public partial class AlmExtend : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the confirmed status
        /// </summary>
        public EnmConfirmStatus ConfirmedStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmed datetime
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }

        /// <summary>
        /// Gets or sets the confirmer
        /// </summary>
        public string Confirmer { get; set; }
    }
}
