using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Site.Models;
using iPem.Site.Models.Organization;
using System;
using System.Collections.Generic;

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
        Role Role { get; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        User User { get; }

        /// <summary>
        /// Gets or sets the current employee
        /// </summary>
        Employee Employee { get; }

        /// <summary>
        /// Gets or sets the current user profile
        /// </summary>
        ProfileValues Profile { get; set; }

        /// <summary>
        /// Gets or sets the current webservice values
        /// </summary>
        WsValues WsValues { get; set; }

        /// <summary>
        /// Gets or sets the current menus
        /// </summary>
        List<Menu> Menus { get; }

        /// <summary>
        /// Gets or sets the current operations
        /// </summary>
        HashSet<EnmOperation> Operations { get; }

        List<LogicType> LogicTypes { get; }

        List<SubLogicType> SubLogicTypes { get; }

        List<DeviceType> DeviceTypes { get; }

        List<SubDeviceType> SubDeviceTypes { get; }

        List<RoomType> RoomTypes { get; }

        List<StationType> StationTypes { get; }

        List<EnumMethods> AreaTypes { get; }

        List<Point> Points { get; }

        List<OrgProtocol> Protocols { get; }

        List<OrgDevice> Devices { get; }

        List<OrgFsu> Fsus { get; }

        List<OrgRoom> Rooms { get; }

        List<OrgStation> Stations { get; }

        List<OrgArea> Areas { get; }

        List<OrgArea> RoleAreas { get; }

        List<OrgStation> RoleStations { get; }

        List<OrgRoom> RoleRooms { get; }

        List<OrgFsu> RoleFsus { get; }

        List<OrgDevice> RoleDevices { get; }

        List<AlmStore<ActAlm>> ActAlmStore { get; }

        List<AlmStore<ActAlm>> GetActAlmStore(List<ActAlm> alarms);

        List<AlmStore<HisAlm>> GetHisAlmStore(List<HisAlm> alarms, DateTime start, DateTime end);
    }
}
