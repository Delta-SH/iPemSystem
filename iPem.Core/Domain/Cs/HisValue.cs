using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisValue : BaseEntity {
        /// <summary>
        /// Gets or sets the area identifier
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// Gets or sets the station identifier
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// Gets or sets the room identifier
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Gets or sets the fsu identifier
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// Gets or sets the signal identifier
        /// </summary>
        public string SignalId { get; set; }

        /// <summary>
        /// Gets or sets the signal number
        /// </summary>
        public string SignalNumber { get; set; }

        /// <summary>
        /// Gets or sets the point type
        /// </summary>
        public EnmPoint PointType { get; set; }

        /// <summary>
        /// Gets or sets the record type
        /// </summary>
        public int RecordType { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public EnmState State { get; set; }

        /// <summary>
        /// Gets or sets the datetime
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
