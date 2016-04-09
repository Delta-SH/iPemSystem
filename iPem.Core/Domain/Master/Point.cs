using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a point
    /// </summary>
    [Serializable]
    public partial class Point : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public EnmPoint Type { get; set; }

        /// <summary>
        /// Gets or sets the logic type
        /// </summary>
        public int LogicTypeId { get; set; }

        /// <summary>
        /// Gets or sets the device type
        /// </summary>
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the station type
        /// </summary>
        public int StaTypeId { get; set; }

        /// <summary>
        /// Gets or sets the unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the alarm time description
        /// </summary>
        public string AlarmTimeDesc { get; set; }

        /// <summary>
        /// Gets or sets the normal time description
        /// </summary>
        public string NormalTimeDesc { get; set; }

        /// <summary>
        /// Gets or sets the alarm level
        /// </summary>
        public EnmAlarmLevel AlarmLevel { get; set; }

        /// <summary>
        /// Gets or sets the trigger type
        /// </summary>
        public string TriggerType { get; set; }

        /// <summary>
        /// Gets or sets the interpret
        /// </summary>
        public string Interpret { get; set; }

        /// <summary>
        /// Gets or sets the alarm limit
        /// </summary>
        public double? AlarmLimit { get; set; }

        /// <summary>
        /// Gets or sets the alarm return diff
        /// </summary>
        public double? AlarmReturnDiff { get; set; }

        /// <summary>
        /// Gets or sets the alarm recovery delay
        /// </summary>
        public int? AlarmRecoveryDelay { get; set; }

        /// <summary>
        /// Gets or sets the alarm delay
        /// </summary>
        public int? AlarmDelay { get; set; }

        /// <summary>
        /// Gets or sets the save period
        /// </summary>
        public int? SavedPeriod { get; set; }

        /// <summary>
        /// Gets or sets the absolute threshold
        /// </summary>
        public double? AbsoluteThreshold { get; set; }

        /// <summary>
        /// Gets or sets the per threshold
        /// </summary>
        public double? PerThreshold { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
