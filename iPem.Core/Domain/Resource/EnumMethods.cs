using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents an enum methods
    /// </summary>
    [Serializable]
    public partial class EnumMethods : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the method number
        /// </summary>
        public int MethNo { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the device
        /// </summary>
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }
    }
}
