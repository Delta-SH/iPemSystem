using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IPointRepository {

        /// <summary>
        /// Gets entities from the repository by the specific device
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>the point list</returns>
        List<Point> GetEntitiesByDevice(string device);

        /// <summary>
        /// Gets entities from the repository by the specific protocol
        /// </summary>
        /// <param name="protocol">the protocol</param>
        /// <returns>the point list</returns>
        List<Point> GetEntitiesByProtocol(string protocol);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>the point list</returns>
        List<Point> GetEntities();

    }
}
