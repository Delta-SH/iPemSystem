using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents a menu
    /// </summary>
    [Serializable]
    public partial class Menu : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the icon path
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the url path
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the parent id
        /// </summary>
        public int LastId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
