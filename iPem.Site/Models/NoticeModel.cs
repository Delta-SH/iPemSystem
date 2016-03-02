using System;

namespace iPem.Site.Models {
    [Serializable]
    public class NoticeModel {
        public int index { get; set; }

        public string id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string created { get; set; }

        public bool enabled { get; set; }

        public string uid { get; set; }

        public bool readed { get; set; }

        public string readtime { get; set; }
    }
}