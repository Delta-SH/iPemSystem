using iPem.Core.Domain.Resource;
using System;

namespace iPem.Site.Infrastructure {
    public class DeviceAttributes {
        /// <summary>
        /// Area
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// Station
        /// </summary>
        public Station Station { get; set; }

        /// <summary>
        /// Room
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// Fsu
        /// </summary>
        public Fsu Fsu { get; set; }

        /// <summary>
        /// Current
        /// </summary>
        public Device Current { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public DeviceType Type { get; set; }

        /// <summary>
        /// SubType
        /// </summary>
        public SubDeviceType SubType { get; set; }
    }
}