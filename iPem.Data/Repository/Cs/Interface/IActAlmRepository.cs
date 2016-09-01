using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IActAlmRepository {
        /// <summary>
        /// Gets entities from the repository by the specific area identifier
        /// </summary>
        /// <param name="area">the area identifier</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntitiesInArea(string area);

        /// <summary>
        /// Gets entities from the repository by the specific station identifier
        /// </summary>
        /// <param name="station">the station identifier</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntitiesInStation(string station);

        /// <summary>
        /// Gets entities from the repository by the specific room identifier
        /// </summary>
        /// <param name="room">the room identifier</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntitiesInRoom(string room);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>active alarm list</returns>
        List<ActAlm> GetEntitiesInDevice(string device);

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
