using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class HomeConsumptionModel {
        public string id { get; set; }

        public ChartModel kt { get; set; }

        public ChartModel zm { get; set; }

        public ChartModel bg { get; set; }

        public ChartModel dy { get; set; }

        public ChartModel ups { get; set; }

        public ChartModel it { get; set; }

        public ChartModel qt { get; set; }

        public ChartModel tt { get; set; }

        public double wd { get; set; }

        public double sd { get; set; }

        public double pue { get; set; }

        public double curmonth { get; set; }

        public double lastmonth { get; set; }

        public double curyear { get; set; }

        public double lastyear { get; set; }

        public List<ChartsModel> dayline { get; set; }

        public List<ChartsModel> monthbar { get; set; }
    }
}