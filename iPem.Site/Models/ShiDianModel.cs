using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ShiDianModel {
        public string DeviceId { get; set; }

        public string PointId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}