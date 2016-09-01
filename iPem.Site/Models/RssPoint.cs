using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public partial class RssPoint {
        public string device { get; set; }

        public string point { get; set; }
    }
}