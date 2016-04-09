using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public partial class PointRss {
        public int[] stationtypes { get; set; }

        public int[] roomtypes { get; set; }

        public int[] devicetypes { get; set; }

        public int[] logictypes { get; set; }

        public int[] pointtypes { get; set; }

        public string pointnames { get; set; }
    }
}