using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents a StationType
    /// </summary>
    public partial class StationType : BaseEntity {
        /// <summary>
        ///Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }
    }
}