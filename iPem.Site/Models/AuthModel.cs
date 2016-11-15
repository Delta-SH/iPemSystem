using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class AuthModel {
        public string key { get; set; }

        public string name { get; set; }

        public string service { get; set; }
    }
}