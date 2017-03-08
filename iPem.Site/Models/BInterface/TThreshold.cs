using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BInterface {
    public partial class TThreshold {
        public string Id { get; set; }

        public string SignalNumber { get; set; }

        public EnmPoint Type { get; set; }

        public string Threshold { get; set; }

        public EnmLevel AlarmLevel { get; set; }

        public string NMAlarmID { get; set; }
    }
}