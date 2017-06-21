using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHFsu {
        public D_Fsu Current { get; set; }

        public List<SSHDevice> Devices { get; set; }
    }
}