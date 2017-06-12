using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgProtocol {
        public P_Protocol Current { get; set; }

        public List<P_Point> Points { get; set; }
    }
}