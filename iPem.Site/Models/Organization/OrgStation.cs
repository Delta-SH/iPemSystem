using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgStation {
        public S_Station Current { get; set; }

        public List<OrgRoom> Rooms { get; set; }
    }
}