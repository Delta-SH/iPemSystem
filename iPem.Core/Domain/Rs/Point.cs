using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
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
        /// Gets or sets the sub logic type
        /// </summary>
        public SubLogicType SubLogicType { get; set; }

        /// <summary>
        /// Gets or sets the logic type
        /// </summary>
        public LogicType LogicType { get; set; }

        /// <summary>
        /// Gets or sets the unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the alarm description
        /// </summary>
        public string AlarmComment { get; set; }

        /// <summary>
        /// Gets or sets the normal description
        /// </summary>
        public string NormalComment { get; set; }

        /// <summary>
        /// Gets or sets the alarm level
        /// </summary>
        public EnmAlarmLevel AlarmLevel { get; set; }

        /// <summary>
        /// Gets or sets the trigger type
        /// </summary>
        public int TriggerTypeId { get; set; }

        /// <summary>
        /// Gets or sets the interpret
        /// </summary>
        public string Interpret { get; set; }

        /// <summary>
        /// Gets or sets the alarm limit
        /// </summary>
        public double AlarmLimit { get; set; }

        /// <summary>
        /// Gets or sets the alarm return diff
        /// </summary>
        public double AlarmReturnDiff { get; set; }

        /// <summary>
        /// Gets or sets the alarm recovery delay
        /// </summary>
        public int AlarmRecoveryDelay { get; set; }

        /// <summary>
        /// Gets or sets the alarm delay
        /// </summary>
        public int AlarmDelay { get; set; }

        /// <summary>
        /// Gets or sets the save period
        /// </summary>
        public int SavedPeriod { get; set; }

        /// <summary>
        /// Gets or sets the static period
        /// </summary>
        public int StaticPeriod { get; set; }

        /// <summary>
        /// Gets or sets the absolute threshold
        /// </summary>
        public double AbsoluteThreshold { get; set; }

        /// <summary>
        /// Gets or sets the per threshold
        /// </summary>
        public double PerThreshold { get; set; }

        /// <summary>
        /// Gets or sets the extend set
        /// </summary>
        public string ExtSet1 { get; set; }

        /// <summary>
        /// Gets or sets the extend set
        /// </summary>
        public string ExtSet2 { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
