using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents a sub device type
    /// </summary>
    public partial class SubDeviceType : BaseEntity {
        /// <summary>
        ///Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///Gets or sets type of the device
        /// </summary>
        public string DeviceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }
    }
}