using System;

namespace iPem.Core.Domain.Resource {
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
        /// Gets or sets the department name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the department code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the type desc
        /// </summary>
        public string TypeDesc { get; set; }

        /// <summary>
        /// Gets or sets the phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the post code
        /// </summary>
        public string PostCode { get; set; }

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
