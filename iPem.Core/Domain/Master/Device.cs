using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a device
    /// </summary>
    [Serializable]
    public partial class Device : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///  Gets or sets the code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///  Gets or sets the protcol identifier
        /// </summary>
        public string ProtcolId { get; set; }

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
