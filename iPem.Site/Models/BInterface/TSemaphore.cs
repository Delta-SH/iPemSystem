using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BInterface {
    public partial class TSemaphore {
        public string Id { get; set; }

        public string SignalNumber { get; set; }

        public EnmBIPoint Type { get; set; }

        public string MeasuredVal { get; set; }

        public string SetupVal { get; set; }

        public EnmBIState Status { get; set; }

        public DateTime Time { get; set; }
    }
}