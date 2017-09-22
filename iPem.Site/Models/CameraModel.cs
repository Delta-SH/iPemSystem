using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class CameraModel {
        public string id { get; set; }

        public string name { get; set; }

        public string ip { get; set; }

        public int port { get; set; }

        public string uid { get; set; }

        public string pwd { get; set; }

        public string comment { get; set; }

        public List<ChannelModel> channels { get; set; }
    }
}