using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a project
    /// </summary>
    [Serializable]
    public partial class Project : BaseEntity {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the startTime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the endTime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the responsible
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// Gets or sets the contactPhone
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Gets or sets the createdTime
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}