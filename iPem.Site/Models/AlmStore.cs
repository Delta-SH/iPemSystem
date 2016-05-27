using iPem.Core.Domain.History;
using iPem.Site.Infrastructure;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class AlmStore<T> where T: class {
        public T Current { get; set; }

        public PointAttributes Point { get; set; }

        public DeviceAttributes Device { get; set; }
    }
}