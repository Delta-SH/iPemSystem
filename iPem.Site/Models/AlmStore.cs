using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class AlmStore<T> where T : class {
        public T Current { get; set; }

        public ExtAlm ExtSet { get; set; }

        public Point Point { get; set; }

        public Device Device { get; set; }

        public Room Room { get; set; }

        public Station Station { get; set; }

        public Area Area { get; set; }
    }
}