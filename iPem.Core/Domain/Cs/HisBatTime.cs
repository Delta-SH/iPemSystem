using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisBatTime : BaseEntity {
        public string DeviceId { get; set; }

        public DateTime Period { get; set; }

        public double Value { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}