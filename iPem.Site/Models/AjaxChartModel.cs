using System;

namespace iPem.Site.Models {
    public class AjaxChartModel<T1, T2> {
        public bool success { get; set; }

        public string message { get; set; }

        public int total { get; set; }

        public T1 data { get; set; }

        public T2 chart { get; set; }
    }
}