using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BI {
    public partial class TSemaphore {
        public string Id { get; set; }

        public int Type { get; set; }

        public double MeasuredVal { get; set; }

        public double SetupVal { get; set; }

        public int Status { get; set; }

        public DateTime RecordTime { get; set; }
    }
}