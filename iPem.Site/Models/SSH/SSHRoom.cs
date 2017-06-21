using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHRoom {
        public S_Room Current { get; set; }

        public List<SSHFsu> Fsus { get; set; }

        public List<SSHDevice> Devices { get; set; }
    }
}