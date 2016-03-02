using System;
using Newtonsoft.Json;

namespace iPem.Site.Models.Installation {
    [Serializable]
    public class DbModel {
        public int id { get; set; }

        public string ipv4 { get; set; }

        public int port { get; set; }

        public string uid { get; set; }

        public string pwd { get; set; }

        public int crdnew { get; set; }

        public string name { get; set; }

        public string path { get; set; }

        public string oname { get; set; }

        [JsonProperty(PropertyName = "checked")]
        public bool uncheck { get; set; }
    }
}