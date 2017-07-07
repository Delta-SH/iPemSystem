using iPem.Core.Domain.Cs;
using System;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class iSSHDevice {
        public H_IDevice Current { get; set; }

        public H_IStation iStation { get; set; }

        public H_IArea iArea { get; set; }
    }
}