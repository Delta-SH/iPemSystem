using System;

namespace iPem.Site.Models {
    public class ExtDirectArgs {
        public string action { get; set; }

        public string method { get; set; }

        public int tid { get; set; }

        public string type { get; set; }

        public object data { get; set; }
    }
}