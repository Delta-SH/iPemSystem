using iPem.Core;
using iPem.Core.Domain.History;
using iPem.Core.Enum;
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
        /// Gets entities from the repository by the specific datetime
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntities(DateTime start, DateTime end);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntities();
    }
}
