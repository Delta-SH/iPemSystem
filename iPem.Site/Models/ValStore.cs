using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ValStore<T> where T : class {
        public T Current { get; set; }

        public P_Point Point { get; set; }

        public D_Device Device { get; set; }

        public S_Room Room { get; set; }

        public S_Station Station { get; set; }

        public A_Area Area { get; set; }

        public String AreaFullName { get; set; }
    }
}