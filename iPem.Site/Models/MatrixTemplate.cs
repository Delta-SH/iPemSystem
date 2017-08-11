using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class MatrixTemplate {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string[] points { get; set; }
    }
}