using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class ChannelModel {
        public string id { get; set; }

        public string name { get; set; }

        public int mask { get; set; }

        public int channel { get; set; }

        public bool zero { get; set; }
        
        public string comment { get; set; }
    }
}