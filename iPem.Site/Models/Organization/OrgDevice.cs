using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models.Organization {
    [Serializable]
    public partial class OrgDevice {
        public D_Device Current { get; set; }

        public OrgProtocol Protocol { get; set; }
    }
}