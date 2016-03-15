using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.History {
    /// <summary>
    /// Represents an active alarm
    /// </summary>
    [Serializable]
    public partial class ActAlm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device code
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// Gets or sets the alarm flag
        /// </summary>
        public EnmAlarmFlag AlmFlag { get; set; }

        /// <summary>
        /// Gets or sets the alarm level
        /// </summary>
        public EnmAlarmLevel AlmLevel { get; set; }

        /// <summary>
        /// Gets or sets the frequency
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Gets or sets the alarm descripetion
        /// </summary>
        public string AlmDesc { get; set; }

        /// <summary>
        /// Gets or sets the normal descripretion
        /// </summary>
        public string NormalDesc { get; set; }

        /// <summary>
        /// Gets or sets the start datetime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end datetime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the start value
        /// </summary>
        public double StartValue { get; set; }

        /// <summary>
        /// Gets or sets the end value
        /// </summary>
        public double EndValue { get; set; }

        /// <summary>
        /// Gets or sets the value unit
        /// </summary>
        public string ValueUnit { get; set; }

        /// <summary>
        /// Gets or sets the end type
        /// </summary>
        public int EndType { get; set; }
    }
}
