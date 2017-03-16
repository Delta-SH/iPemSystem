using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class ActAlm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the alarm identifier
        /// </summary>
        public string SerialNo { get; set; }
        
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
        /// Gets or sets the nmalarm identifier
        /// </summary>
        public string NMAlarmId { get; set; }

        /// <summary>
        /// Gets or sets the alarm datetime
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// Gets or sets the alarm level
        /// </summary>
        public EnmLevel AlarmLevel { get; set; }

        /// <summary>
        /// Gets or sets the alarm flag
        /// </summary>
        public EnmFlag AlarmFlag { get; set; }

        /// <summary>
        /// Gets or sets the alarm descripetion
        /// </summary>
        public string AlarmDesc { get; set; }

        /// <summary>
        /// Gets or sets the alarm value
        /// </summary>
        public double AlarmValue { get; set; }

        /// <summary>
        /// Gets or sets the alarm remark
        /// </summary>
        public string AlarmRemark { get; set; }

        /// <summary>
        /// Gets or sets the frequency
        /// </summary>
        public int Frequency { get; set; }
    }
}
