using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class ActValue : BaseEntity {
        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// Gets or sets the measured value
        /// </summary>
        public float MeasuredVal { get; set; }

        /// <summary>
        /// Gets or sets the setup value
        /// </summary>
        public float SetupVal { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public EnmPointStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the record datetime
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
