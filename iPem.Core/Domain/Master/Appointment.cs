using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents an appointment
    /// </summary>
    [Serializable]
    public partial class Appointment : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  Gets or sets the start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///  Gets or sets the end time
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///  Gets or sets the project id
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        ///  Gets or sets the creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///  Gets or sets the created time
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///  Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appointment is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}