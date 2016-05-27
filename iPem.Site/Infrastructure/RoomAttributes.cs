using iPem.Core.Domain.Resource;
using System;

namespace iPem.Site.Infrastructure {
    public class RoomAttributes {
        /// <summary>
        /// Area
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// Station
        /// </summary>
        public Station Station { get; set; }

        /// <summary>
        /// Current
        /// </summary>
        public Room Current { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public RoomType Type { get; set; }
    }
}