using System;

namespace iPem.Site.Models {
    [Serializable]
    public class GviewCtrlModel {
        public string deviceId { get; set; }

        public string deviceName { get; set; }

        public string pointId { get; set; }

        public string pointName { get; set; }

        public int pointType { get; set; }
    }
}