using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents an role
    /// </summary>
    [Serializable]
    public partial class Role : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets the super id
        /// </summary>
        public static Guid SuperId {
            get { return new Guid("A0000001-0000-0000-0000-F00000000001"); }
        }
    }
}
