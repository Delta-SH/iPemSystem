using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class AlmStore<T> where T : class {
        public T Current { get; set; }

        public string PointName { get; set; }

        public string DeviceName { get; set; }

        public string DeviceTypeId { get; set; }

        public string SubDeviceTypeId { get; set; }

        public string SubLogicTypeId { get; set; }

        public string RoomName { get; set; }

        public string RoomTypeId { get; set; }

        public string StationName { get; set; }

        public string StationTypeId { get; set; }

        public string AreaName { get; set; }

        public string AreaFullName { get; set; }

        public string SubCompany { get; set; }
    }
}