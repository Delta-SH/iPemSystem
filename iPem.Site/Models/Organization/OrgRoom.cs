using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgRoom {
        public S_Room Current { get; set; }

        public List<OrgFsu> Fsus { get; set; }

        public List<OrgDevice> Devices { get; set; }
    }
}