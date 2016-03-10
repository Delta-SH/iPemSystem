using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Core {
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
        /// Gets or sets the associated area attributes
        /// </summary>
        Dictionary<string, AreaAttributes> AssociatedAreaAttributes { get; }

        /// <summary>
        /// Gets or sets the associated station attributes
        /// </summary>
        Dictionary<string, StationAttributes> AssociatedStationAttributes { get; }
    }
}
