using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a logic type
    /// </summary>
    [Serializable]
    public partial class LogicType {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }
    }
}
