using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a StationType
    /// </summary>
    [Serializable]
    public partial class StationType : BaseEntity {
        /// <summary>
        ///Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

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