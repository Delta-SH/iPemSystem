using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents an extend alarm class
    /// </summary>
    [Serializable]
    public partial class ExtAlarm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// Gets or sets the alarm datetime
        /// </summary>
        public DateTime Time { get; set; }

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
