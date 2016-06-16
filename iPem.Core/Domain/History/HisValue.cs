using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.History {
    /// <summary>
    /// Represents an history value
    /// </summary>
    [Serializable]
    public partial class HisValue : BaseEntity {
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
        /// Gets or sets the type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the threshold
        /// </summary>
        public double Threshold { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public EnmPointStatus State { get; set; }

        /// <summary>
        /// Gets or sets the datetime
        /// </summary>
        public DateTime Time { get; set; }
    }
}
