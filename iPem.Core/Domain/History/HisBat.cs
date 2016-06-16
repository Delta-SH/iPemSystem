using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.History {
    /// <summary>
    /// Represents an history battery
    /// </summary>
    [Serializable]
    public partial class HisBat : BaseEntity {
        /// <summary>
        /// Gets or sets the device code
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// Gets or sets the datetime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the datetime
        /// </summary>
        public DateTime ValueTime { get; set; }
    }
}
