using iPem.Core.Domain.Cs;
using System;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class iSSHStation {
        public H_IStation Current { get; set; }

        public H_IArea iArea { get; set; }
    }
}