using System;
using System.Collections.Generic;

namespace iPem.Site.Models.BI {
    public partial class ActDataDeviceTemplate {
        public string Id { get; set; }

        public string Code { get; set; }

        public List<string> Values { get; set; }
    }
}