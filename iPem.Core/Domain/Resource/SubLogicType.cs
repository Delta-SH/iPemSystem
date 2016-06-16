using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents a logic type
    /// </summary>
    [Serializable]
    public partial class SubLogicType {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string LogicTypeId { get; set; }
    }
}