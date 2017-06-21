using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHStation {
        public S_Station Current { get; set; }

        public List<SSHRoom> Rooms { get; set; }
    }
}