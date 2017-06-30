using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BInterface {
    public partial class TSignal {
        public TSignalMeasurementId TSignalId { get; set; }

        public string SignalName { get; set; }

        public EnmBIPoint Type { get; set; }

        public string Threshold { get; set; }

        public EnmBILevel AlarmLevel { get; set; }

        public string NMAlarmID { get; set; }
    }
}