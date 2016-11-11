using System;

namespace iPem.Core.Domain.Am {
    [Serializable]
    public partial class AmDevice {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string ParentId { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}