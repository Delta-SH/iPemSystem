using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;
using iPem.Site.Models;
using iPem.Site.Extensions;
using iPem.Core.Enum;
using iPem.Core;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext {
        /// <summary>
        /// Gets or sets whether the user has been authenticated
        /// </summary>
        Boolean IsAuthenticated { get; }
        
        /// <summary>
        /// Gets or sets the current identifier
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        Store Store { get; }

        /// <summary>
        /// Gets or sets the current role
        /// </summary>
        MsDomain.Role CurrentRole { get; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        MsDomain.User CurrentUser { get; }

        /// <summary>
        /// Gets or sets the current user profile
        /// </summary>
        ProfileValues CurrentProfile { get; set; }

        /// <summary>
        /// Gets or sets the current webservice values
        /// </summary>
        WsValues CurrentWsValues { get; set; }

        /// <summary>
        /// Gets or sets the associated employee
        /// </summary>
        RsDomain.Employee AssociatedEmployee { get; }

        /// <summary>
        /// Gets or sets the associated areas
        /// </summary>
        List<RsDomain.Area> AssociatedAreas { get; }

        /// <summary>
        /// Gets or sets the associated stations
        /// </summary>
        List<RsDomain.Station> AssociatedStations { get; }

        /// <summary>
        /// Gets or sets the associated rooms
        /// </summary>
        List<RsDomain.Room> AssociatedRooms { get; }

        /// <summary>
        /// Gets or sets the associated devices
        /// </summary>
        List<RsDomain.Device> AssociatedDevices { get; }

        /// <summary>
        /// Gets or sets the associated fsus
        /// </summary>
        List<RsDomain.Fsu> AssociatedFsus { get; }

        /// <summary>
        /// Gets or sets the associated menus
        /// </summary>
        List<MsDomain.Menu> AssociatedMenus { get; }

        /// <summary>
        /// Gets or sets the associated operations
        /// </summary>
        Dictionary<EnmOperation, string> AssociatedOperations { get; }

        /// <summary>
        /// Gets or sets the associated area attributes
        /// </summary>
        Dictionary<string, AreaAttributes> AssociatedAreaAttributes { get; }

        /// <summary>
        /// Gets or sets the associated station attributes
        /// </summary>
        Dictionary<string, StationAttributes> AssociatedStationAttributes { get; }

        /// <summary>
        /// Gets or sets the associated room attributes
        /// </summary>
        Dictionary<string, RoomAttributes> AssociatedRoomAttributes { get; }

        /// <summary>
        /// Gets or sets the associated device attributes
        /// </summary>
        Dictionary<string, DeviceAttributes> AssociatedDeviceAttributes { get; }

        /// <summary>
        /// Gets or sets the associated point attributes
        /// </summary>
        Dictionary<string, PointAttributes> AssociatedPointAttributes { get; }

        /// <summary>
        /// Gets or sets the associated rss points
        /// </summary>
        List<IdValuePair<DeviceAttributes, PointAttributes>> AssociatedRssPoints { get; }

        /// <summary>
        /// Gets the parent areas
        /// </summary>
        /// <param name="current">current area</param>
        /// <param name="include">include</param>
        /// <returns></returns>
        List<RsDomain.Area> GetParentsInArea(RsDomain.Area current, bool include = true);

        /// <summary>
        /// Gets the parent stations
        /// </summary>
        /// <param name="current">current station</param>
        /// <param name="include">include</param>
        /// <returns></returns>
        List<RsDomain.Station> GetParentsInStation(RsDomain.Station current, bool include = true);
    }
}
