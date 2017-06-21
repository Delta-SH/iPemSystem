using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Site.Models.SSH;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class PointStore<T> where T : class {
        public T Current { get; set; }

        public EnmPoint Type { get; set; }

        public SSHDevice Device { get; set; }

        public SSHArea Area { get; set; }

        public bool Followed { get; set; }

        public bool FollowedOnly { get; set; }
    }
}