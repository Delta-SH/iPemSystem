using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents an room
    /// </summary>
    [Serializable]
    public partial class Room : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///  Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
