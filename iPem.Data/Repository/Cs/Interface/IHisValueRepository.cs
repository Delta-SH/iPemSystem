using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisValueRepository {
        /// <summary>
        /// Gets entities from the repository by the specific area identifier
        /// </summary>
        /// <param name="area">the area identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByArea(string area, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific station identifier
        /// </summary>
        /// <param name="station">the station identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByStation(string station, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="room">the room identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByRoom(string room, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByDevice(string device, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <param name="point">the point identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific point identifier
        /// </summary>
        /// <param name="point">the point identifier</param>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByPoint(string point, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific point identifiers
        /// </summary>
        /// <param name="points">the points array</param>
        /// <param name="start">the start time</param>
        /// <param name="end">the end time</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntitiesByPoint(string[] points, DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific datetime
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history value list</returns>
        List<HisValue> GetEntities(DateTime start, DateTime end);
    }
}
