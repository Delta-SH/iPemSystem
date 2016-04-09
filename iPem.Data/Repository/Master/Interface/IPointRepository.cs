using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Point repository interface
    /// </summary>
    public partial interface IPointRepository {

        /// <summary>
        /// Gets entities from the repository by the specific device
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>the point list</returns>
        List<Point> GetEntitiesByDevice(string device);

        /// <summary>
        /// Gets entities from the repository by the specific types
        /// </summary>
        /// <param name="type">the point type array</param>
        /// <returns>the point list</returns>
        List<Point> GetEntitiesByType(int type);

        /// <summary>
        /// Gets entities from the repository by the specific protcol
        /// </summary>
        /// <param name="protcol">the protcol</param>
        /// <returns>the point list</returns>
        List<Point> GetEntitiesByProtcol(int protcol);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>the point list</returns>
        List<Point> GetEntities();

    }
}
