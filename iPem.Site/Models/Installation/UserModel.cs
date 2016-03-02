using System;
using Newtonsoft.Json;

namespace iPem.Site.Models.Installation {
    [Serializable]
    public class UserModel {
        [JsonIgnore]
        public string id { get; set; }

        public string name { get; set; }

        public string pwd { get; set; }

        public string comment { get; set; }

        [JsonIgnore]
        public bool enabled { get; set; }
    }
}