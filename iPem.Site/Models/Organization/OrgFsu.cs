using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgFsu {
        public D_Fsu Current { get; set; }

        public List<OrgDevice> Devices { get; set; }
    }
}