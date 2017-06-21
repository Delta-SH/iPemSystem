using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHDevice {
        public D_Device Current { get; set; }

        public SSHProtocol Protocol { get; set; }
    }
}