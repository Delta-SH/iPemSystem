using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents an alarm extend class
    /// </summary>
    [Serializable]
    public partial class ExtAlm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the fsu identifier
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// Gets or sets the start datetime
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end datetime
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the confirmed status
        /// </summary>
        public EnmConfirm Confirmed { get; set; }

        /// <summary>
        /// Gets or sets the confirmer
        /// </summary>
        public string Confirmer { get; set; }

        /// <summary>
        /// Gets or sets the confirmed datetime
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }
    }
}
