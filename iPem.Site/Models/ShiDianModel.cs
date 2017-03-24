using iPem.Core.Domain.Rs;
using System;
using System.Web.Script.Serialization;

namespace iPem.Site.Models {
    [Serializable]
    public class ShiDianModel {
        public string start { get; set; }

        public string end { get; set; }

        public string timespan { get; set; }
    }
}