using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BI {
    public partial class TSemaphore {
        public string Id { get; set; }

        public int Type { get; set; }

        public float MeasuredVal { get; set; }

        public float SetupVal { get; set; }

        public int Status { get; set; }

        public DateTime RecordTime { get; set; }
    }
}