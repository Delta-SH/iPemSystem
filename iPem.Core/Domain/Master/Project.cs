using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a project
    /// </summary>
    [Serializable]
    public partial class Project : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start datetime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end datetime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the responsible
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// Gets or sets the contact phone
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
        /// Gets or sets the created datetime
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
