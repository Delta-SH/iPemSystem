using iPem.Core.Enum;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ActValue {
        public string DeviceId { get; set; }

        public string PointId { get; set; }

        public double? MeasuredVal { get; set; }

        public double? SetupVal { get; set; }

        public EnmState Status { get; set; }

        public DateTime Time { get; set; }
    }
}