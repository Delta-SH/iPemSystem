using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ActAlmCondition {
        public string baseNode { get; set; }

        public string seniorNode { get; set; }

        public string[] stationTypes { get; set; }

        public string[] roomTypes { get; set; }

        public string[] subDeviceTypes { get; set; }

        public string[] subLogicTypes { get; set; }

        public string[] points { get; set; }

        public int[] levels { get; set; }

        public int[] confirms { get; set; }

        public int[] reservations { get; set; }

        public string keywords { get; set; }
    }
}