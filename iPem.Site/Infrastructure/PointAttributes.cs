using iPem.Core.Domain.Master;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Site.Infrastructure {
    public class PointAttributes {
        /// <summary>
        /// Current
        /// </summary>
        public Point Current { get; set; }

        /// <summary>
        /// SubLogicType
        /// </summary>
        public SubLogicType SubLogicType { get; set; }

        /// <summary>
        /// LogicType
        /// </summary>
        public LogicType LogicType { get; set; }
    }
}