using System;

namespace iPem.Site.Models {
    [Serializable]
    public class MarkerModel {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public double lng { get; set; }

        public double lat { get; set; }

        public int level { get; set; }

        public int alm1 { get; set; }

        public int alm2 { get; set; }

        public int alm3 { get; set; }

        public int alm4 { get; set; }
    }
}