using System;

namespace iPem.Site.Models {
    public class ExtDirectResult {
        public string action { get; set; }

        public string method { get; set; }

        public int tid { get; set; }

        public string type { get; set; }

        public object result { get; set; }

        public string message { get; set; }
    }
}