using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisFtp : BaseEntity {
        public string FsuId { get; set; }

        public EnmFtpEvent EventType { get; set; }

        public string Message { get; set; }

        public DateTime EventTime { get; set; }
    }
}
