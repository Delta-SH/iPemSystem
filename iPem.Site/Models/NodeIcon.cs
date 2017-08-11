using System;

namespace iPem.Site.Models {
    [Serializable]
    public class NodeIcon {
        public string id { get; set; }

        public int level { get; set; }

        public int type { get; set; }
    }
}