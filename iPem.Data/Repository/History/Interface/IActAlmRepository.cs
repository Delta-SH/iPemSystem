using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.History {
    public partial interface IActAlmRepository {

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntities(string device);

        /// <summary>
        /// Gets entities from the repository by the specific alarm levels
        /// </summary>
        /// <param name="levels">the alarm level array</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntities(int[] levels);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntities();

    }
}
