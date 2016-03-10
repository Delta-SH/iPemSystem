using HsDomain = iPem.Core.Domain.History;
using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ActAlmStore {
        public HsDomain.ActAlm alarm { get; set; }

        public MsDomain.Point point { get; set; }

        public RsDomain.LogicType logic { get; set; }

        public RsDomain.Device device { get; set; }

        public RsDomain.DeviceType devicetype { get; set; }

        public RsDomain.Room room { get; set; }

        public RsDomain.Station station { get; set; }

        public RsDomain.Area area { get; set; }
    }
}