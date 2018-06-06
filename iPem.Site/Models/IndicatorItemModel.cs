using System;

namespace iPem.Site.Models {
    [Serializable]
    public class IndicatorItemModel {
        public int id { get; set; }

        public string name { get; set; }

        public string icon { get; set; }

        public string color { get; set; }
    }
}