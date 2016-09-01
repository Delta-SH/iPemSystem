using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgProtocol {
        public Protocol Current { get; set; }

        public List<Point> Points { get; set; }
    }
}