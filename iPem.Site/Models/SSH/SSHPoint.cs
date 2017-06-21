using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHPoint {
        public P_Point Current { get; set; }

        public P_SubPoint SubPoint { get; set; }
    }
}