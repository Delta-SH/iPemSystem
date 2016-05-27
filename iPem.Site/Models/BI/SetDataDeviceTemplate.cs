using System;
using System.Collections.Generic;

namespace iPem.Site.Models.BI {
    public partial class SetDataDeviceTemplate {
        public string Id { get; set; }

        public string Code { get; set; }

        public List<TSemaphore> Values { get; set; }
    }
}