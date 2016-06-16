using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ChartModel {
        public int index { get; set; }

        public string name { get; set; }

        public double value { get; set; }

        public string comment { get; set; }
    }
}