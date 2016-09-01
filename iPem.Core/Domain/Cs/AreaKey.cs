using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class AreaKey : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///  Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the area is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
