using iPem.Core.Domain.Rs;
using System;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class ShiDianModel {
        [ScriptIgnore]
        public Device device { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public double interval { get; set; }

        public string timespan { get; set; }
    }
}