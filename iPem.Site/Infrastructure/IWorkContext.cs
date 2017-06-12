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
        /// Gets whether the user has been authenticated
        /// </summary>
        Boolean IsAuthenticated { get; }
        
        /// <summary>
        /// Gets the current identifier
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets the current store
        /// </summary>
        Store Store { get; }

        /// <summary>
        /// Gets the current role
        /// </summary>
        U_Role Role { get; }

        /// <summary>
        /// Gets the current user
        /// </summary>
        U_User User { get; }

        /// <summary>
        /// Gets the current employee
        /// </summary>
        U_Employee Employee { get; }

        /// <summary>
        /// Gets the current user profile
        /// </summary>
        ProfileValues Profile { get; }

        /// <summary>
        /// Gets the current webservice values
        /// </summary>
        WsValues WsValues { get; }

        /// <summary>
        /// Gets the current speech values
        /// </summary>
        TsValues TsValues { get; }

        /// <summary>
        /// Gets the current report values
        /// </summary>
        RtValues RtValues { get; }

        /// <summary>
        /// Gets the current menus
        /// </summary>
        List<U_Menu> Menus { get; }

        /// <summary>
        /// Gets the current operations
        /// </summary>
        HashSet<EnmOperation> Operations { get; }

        List<C_LogicType> LogicTypes { get; }

        List<C_SubLogicType> SubLogicTypes { get; }

        List<C_DeviceType> DeviceTypes { get; }

        List<C_SubDeviceType> SubDeviceTypes { get; }

        List<C_RoomType> RoomTypes { get; }

        List<C_StationType> StationTypes { get; }

        List<C_EnumMethod> AreaTypes { get; }

        List<P_Point> Points { get; }

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

        List<AlmStore<A_AAlarm>> ActAlmStore { get; }

        List<AlmStore<A_AAlarm>> GetActAlmStore(List<A_AAlarm> alarms);

        List<AlmStore<A_HAlarm>> GetHisAlmStore(List<A_HAlarm> alarms, DateTime start, DateTime end);
    }
}
