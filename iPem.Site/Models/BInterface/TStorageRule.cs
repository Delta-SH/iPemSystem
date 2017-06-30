using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.BInterface {
    public partial class TStorageRule {
        public string Id { get; set; }

        public string SignalNumber { get; set; }

        public EnmBIPoint Type { get; set; }

        public string AbsoluteVal { get; set; }

        public string RelativeVal { get; set; }

        public string StorageInterval { get; set; }

        public string StorageRefTime { get; set; }
    }
}