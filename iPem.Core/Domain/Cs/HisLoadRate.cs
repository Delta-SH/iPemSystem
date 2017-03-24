using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisLoadRate : BaseEntity {
        public string AreaId { get; set; }

        public string StationId { get; set; }

        public string RoomId { get; set; }

        public string DeviceId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Value { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}