using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgDevice {
        public Device Current { get; set; }

        public OrgProtocol Protocol { get; set; }
    }
}