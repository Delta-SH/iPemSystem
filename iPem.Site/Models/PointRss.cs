using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public partial class PointRss {
        public string[] stationtypes { get; set; }

        public string[] roomtypes { get; set; }

        public string[] devicetypes { get; set; }

        public string[] logictypes { get; set; }

        public int[] pointtypes { get; set; }

        public string pointnames { get; set; }
    }
}