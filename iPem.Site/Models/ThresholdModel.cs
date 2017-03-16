using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPem.Site.Models {
    [Serializable]
    public class ThresholdModel {
        public string id { get; set; }

        public string number { get; set; }

        public int type { get; set; }

        public string threshold { get; set; }

        public string level { get; set; }

        public string nmid { get; set; }
    }
}