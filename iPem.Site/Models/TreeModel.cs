using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    public class TreeModel {
        public string id { get; set; }

        public string text { get; set; }

        public string href { get; set; }

        public string icon { get; set; }

        [JsonProperty(PropertyName = "checked")]
        public bool? selected { get; set; }

        public bool expanded { get; set; }

        public bool leaf  { get; set; }

        public List<CustomAttribute> attributes { get; set; }

        public List<TreeModel> data { get; set; }
    }
}