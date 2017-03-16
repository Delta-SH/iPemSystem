using System;

namespace iPem.Site.Models {
    [Serializable]
    public class BadgeModel {
        public int notices { get; set; }

        public int alarms { get; set; }
    }
}