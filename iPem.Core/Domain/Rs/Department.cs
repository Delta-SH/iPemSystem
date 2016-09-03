using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a department
    /// </summary>
    [Serializable]
    public partial class Department : BaseEntity {
        /// <summary>
        /// Gets or sets the department identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the department code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the department name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the department comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the department is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
