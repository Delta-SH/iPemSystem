using System;
using System.Collections.Generic;

namespace iPem.Site.Models.BInterface {
    public partial class TDevConf {
        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public string DeviceType { get; set; }

        public string DeviceSubType { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public string RatedCapacity { get; set; }

        public string Version { get; set; }

        public string BeginRunTime { get; set; }

        public string DevDescribe { get; set; }

        public string ConfRemark { get; set; }

        public List<TSignal> Signals { get; set; }
    }
}