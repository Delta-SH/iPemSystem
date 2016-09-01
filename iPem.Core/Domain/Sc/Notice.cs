using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents a notice
    /// </summary>
    [Serializable]
    public class Notice : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the created time
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
