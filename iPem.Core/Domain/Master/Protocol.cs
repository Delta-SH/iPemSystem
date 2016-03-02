﻿using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a protocol
    /// </summary>
    [Serializable]
    public partial class Protocol : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device type
        /// </summary>
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the sub device type
        /// </summary>
        public int SubDevTypeId { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
