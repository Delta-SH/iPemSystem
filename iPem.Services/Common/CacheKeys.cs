using System;

namespace iPem.Services.Common {
    /// <summary>
    /// Global cache keys class
    /// </summary>
    public static partial class GlobalCacheKeys {
        //sc repository
        public const string Sc_MenusRepository = "ipems:global:sc-menus@repository";

        //rs repository
        public const string Rs_LogicTypesRepository = "ipems:global:rs-logic-types@repository";
        public const string Rs_SubLogicTypesRepository = "ipems:global:rs-sub-logic-types@repository";
        public const string Rs_DeviceTypeRepository = "ipems:global:rs-device-type@repository";
        public const string Rs_SubDeviceTypesRepository = "ipems:global:rs-sub-device-types@repository";
        public const string Rs_RoomTypesRepository = "ipems:global:rs-room-types@repository";
        public const string Rs_StationTypesRepository = "ipems:global:rs-station-types@repository";

        //organization all result 
        public const string Og_Points = "ipems:global:og-points@organization";
        public const string Og_Protocols = "ipems:global:og-protocols@organization";
        public const string Og_Devices = "ipems:global:og-devices@organization";
        public const string Og_Fsus = "ipems:global:og-fsus@organization";
        public const string Og_Rooms = "ipems:global:og-rooms@organization";
        public const string Og_Stations = "ipems:global:og-stations@organization";
        public const string Og_Areas = "ipems:global:og-areas@organization";

        //organization role result 
        public const string Og_RoleAreasPattern = "ipems:global:og-role-areas@{0}@organization";
        public const string Og_RoleStationsPattern = "ipems:global:og-role-stations@{0}@organization";
        public const string Og_RoleRoomsPattern = "ipems:global:og-role-rooms@{0}@organization";
        public const string Og_RoleFsusPattern = "ipems:global:og-role-fsus@{0}@organization";
        public const string Og_RoleDevicesPattern = "ipems:global:og-role-devices@{0}@organization";

        //role result
        public const string Rl_MenusResultPattern = "ipems:global:role-menus@{0}@result";
        public const string Rl_OperationsResultPattern = "ipems:global:role-operations@{0}@result";
    }

    /// <summary>
    /// Cached interval class
    /// </summary>
    public static partial class CachedIntervals {
        public static readonly TimeSpan Store_Intervals = TimeSpan.FromSeconds(600);
        public static readonly TimeSpan Global_Intervals = TimeSpan.FromSeconds(3600);
    }
}
