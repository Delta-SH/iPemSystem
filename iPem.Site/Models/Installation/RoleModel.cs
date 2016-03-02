using System;
using Newtonsoft.Json;

namespace iPem.Site.Models.Installation {
    [Serializable]
    public class RoleModel {
        [JsonIgnore]
        public string id { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public string comment { get; set; }

        [JsonIgnore]
        public bool enabled { get; set; }
    }
}