using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Site.Models.SSH;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class PointStore<T> where T : class {
        public T Current { get; set; }

        public EnmPoint Type { get; set; }

        public string DeviceId { get; set; }

        public string DeviceCode { get; set; }

        public string DeviceName { get; set; }

        public string FsuId { get; set; }

        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public string StationId { get; set; }

        public string StationName { get; set; }

        public string AreaId { get; set; }

        public string AreaName { get; set; }

        public bool Followed { get; set; }

        public bool FollowedOnly { get; set; }
    }
}