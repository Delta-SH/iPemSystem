using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a duty
    /// </summary>
    [Serializable]
    public partial class Duty : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the level
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
