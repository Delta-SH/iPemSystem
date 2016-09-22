using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    public class ChartsModel {
        public int index { get; set; }

        public string name { get; set; }

        public string comment { get; set; }

        public List<ChartModel> models { get; set; }
    }
}