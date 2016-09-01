using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisStaticRepository {

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>history static list</returns>
        List<HisStatic> GetEntities(string device, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <param name="point">the point identifier</param>
        /// <returns>history static list</returns>
        List<HisStatic> GetEntities(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific datetime
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisStatic> GetEntities(DateTime start, DateTime end);

    }
}
