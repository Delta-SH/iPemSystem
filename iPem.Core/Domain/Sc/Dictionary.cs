using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents a dictionary
    /// </summary>
    [Serializable]
    public partial class Dictionary : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the values
        /// </summary>
        public string ValuesJson { get; set; }

        /// <summary>
        /// Gets or sets the values
        /// </summary>
        public byte[] ValuesBinary { get; set; }

        /// <summary>
        /// Gets or sets the last updated date
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }
}
