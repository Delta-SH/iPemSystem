using System;

namespace iPem.Services.Common {
    /// <summary>
    /// Global cache keys class
    /// </summary>
    public static partial class GlobalCacheKeys {
        //master repository
        public const string Cs_RolesRepository = "ipems:global:cs-roles@repository";
        public const string Cs_MenusRepository = "ipems:global:cs-menus@repository";
        public const string Cs_UsersRepository = "ipems:global:cs-users@repository";
        public const string Cs_AreasRepository = "ipems:global:cs-areas@repository";
        public const string Cs_StationsRepository = "ipems:global:cs-stations@repository";
        public const string Cs_RoomsRepository = "ipems:global:cs-rooms@repository";
        public const string Cs_DevicesRepository = "ipems:global:cs-devices@repository";
        public const string Cs_PointsInTypePattern = "ipems:global:cs-points-type@{0}@repository";
        public const string Cs_PointsInProtcolPattern = "ipems:global:cs-points-protcol@{0}@repository";
        public const string Cs_PointsRepository = "ipems:global:cs-points@repository";
        public const string Cs_ProtocolsInDevTypePattern = "ipems:global:cs-protocols-device-type@{0}@repository";
        public const string Cs_ProtocolsRepository = "ipems:global:cs-protocols@repository";

        //resource repository
        public const string Rs_AreasRepository = "ipems:global:rs-areas@repository";
        public const string Rs_BrandsRepository = "ipems:global:rs-brands@repository";
        public const string Rs_DepartmentRepository = "ipems:global:rs-departments@repository";
        public const string Rs_EmployeesRepository = "ipems:global:rs-employee@repository";
        public const string Rs_EmployeesInDepartmentPattern = "ipems:global:rs-employee@{0}@repository";
        
        
        public const string Rs_StationsInAreaPattern = "ipems:global:rs-station-area@{0}@repository";
        public const string Rs_ChildrenInStationPattern = "ipems:global:rs-station-children@{0}-{1}-{2}@repository";
        public const string Rs_StationsRepository = "ipems:global:rs-station@repository";
        public const string Rs_RoomsInParentPattern = "ipems:global:rs-room-station@{0}@repository";
        public const string Rs_RoomsRepository = "ipems:global:rs-room@repository";
        public const string Rs_DevicesInParentPattern = "ipems:global:rs-device-room@{0}@repository";
        public const string Rs_DevicesRepository = "ipems:global:rs-device@repository";
        
        public const string Rs_DeviceStatusRepository = "ipems:global:rs-device-status@repository";
        public const string Rs_DeviceTypeRepository = "ipems:global:rs-device-type@repository";
        public const string Rs_DutiesRepository = "ipems:global:rs-duty@repository";
        public const string Rs_EnumMethodsRepository = "ipems:global:rs-enum-methods@repository";
        public const string Rs_LogicTypesRepository = "ipems:global:rs-logic-type@repository";
        public const string Rs_ProductorsRepository = "ipems:global:rs-productor@repository";
        public const string Rs_RoomTypesRepository = "ipems:global:rs-room-type@repository";
        public const string Rs_StationTypesRepository = "ipems:global:rs-station-type@repository";
        public const string Rs_SubCompaniesRepository = "ipems:global:rs-sub-company@repository";
        public const string Rs_SubDeviceTypesRepository = "ipems:global:rs-sub-device-type@repository";
        public const string Rs_SuppliersRepository = "ipems:global:rs-supplier@repository";
        public const string Rs_UnitsRepository = "ipems:global:rs-unit@repository";

        //role result
        public const string Rl_UsersResultPattern = "ipems:global:role-users@{0}@result";
        public const string Rl_MenusResultPattern = "ipems:global:role-menus@{0}@result";
        public const string Rl_AreasResultPattern = "ipems:global:role-areas@{0}@result";
        public const string Rl_StationsResultPattern = "ipems:global:role-stations@{0}@result";
        public const string Rl_RoomsResultPattern = "ipems:global:role-rooms@{0}@result";
        public const string Rl_DevicesResultPattern = "ipems:global:role-devices@{0}@result";
        public const string Rl_AreaAttributesResultPattern = "ipems:global:role-area-attributes@{0}@result";
        public const string Rl_StationAttributesResultPattern = "ipems:global:role-station-attributes@{0}@result";
    }

    /// <summary>
    /// User cache keys class
    /// </summary>
    public static partial class UserCacheKeys {
        public const string U_RolesResultPattern = "ipems:user:role@{0}@result";
        public const string U_UsersResultPattern = "ipems:user:user@{0}@result";
        public const string U_ActAlmResultPattern = "ipems:user:actalm@{0}@result";
    }

    public static partial class CachedIntervals {
        public static readonly TimeSpan AppStoreIntervals = TimeSpan.FromSeconds(300);
        public static readonly TimeSpan AppRoleIntervals = TimeSpan.FromSeconds(1200);
        public static readonly TimeSpan ActAlmIntervals = TimeSpan.FromSeconds(15);
    }
}
