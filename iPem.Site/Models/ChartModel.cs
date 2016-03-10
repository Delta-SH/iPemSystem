using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ChartModel {
        public string name { get; set; }

        public int value { get; set; }

        public string comment { get; set; }
    }
}