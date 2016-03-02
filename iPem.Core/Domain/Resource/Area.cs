using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents an area
    /// </summary>
    [Serializable]
    public partial class Area : BaseEntity {
        /// <summary>
        /// Gets or sets the area identifier
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// Gets or sets the area name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the node level
        /// </summary>
        public int NodeLevel { get; set; }

        /// <summary>
        /// Gets or sets the creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Gets or sets the created time
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the modifier
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// Gets or sets the modified time
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the area's parent identifer
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the area's comment identifer
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the area is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
