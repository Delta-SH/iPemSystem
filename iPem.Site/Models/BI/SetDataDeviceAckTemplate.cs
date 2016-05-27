using System;
using System.Collections.Generic;

namespace iPem.Site.Models.BI {
    public partial class SetDataDeviceAckTemplate {
        public string Id { get; set; }

        public string Code { get; set; }

        public List<string> Success { get; set; }

        public List<string> Failure { get; set; }
    }
}