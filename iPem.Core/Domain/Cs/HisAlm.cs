using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisAlm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

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
        /// Gets or sets the alarm level
        /// </summary>
        public EnmLevel AlmLevel { get; set; }

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
        public EnmAlarmEndType EndType { get; set; }
    }
}
