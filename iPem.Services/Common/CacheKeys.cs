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
        public const string Cs_ProtocolsRepository = "ipems:global:cs-protocols@repository";

        //resource repository
        public const string Rs_AreasRepository = "ipems:global:rs-areas@repository";
        public const string Rs_BrandsRepository = "ipems:global:rs-brands@repository";
        public const string Rs_DepartmentRepository = "ipems:global:rs-departments@repository";
        public const string Rs_DevicesRepository = "ipems:global:rs-devices@repository";
        public const string Rs_DeviceTypeRepository = "ipems:global:rs-device-type@repository";
        public const string Rs_DutiesRepository = "ipems:global:rs-duties@repository";
        public const string Rs_EmployeesRepository = "ipems:global:rs-employees@repository";
        public const string Rs_EnumMethodsRepository = "ipems:global:rs-enum-methods@repository";
        public const string Rs_LogicTypesRepository = "ipems:global:rs-logic-types@repository";
        public const string Rs_ProductorsRepository = "ipems:global:rs-productors@repository";
        public const string Rs_RoomsRepository = "ipems:global:rs-rooms@repository";
        public const string Rs_RoomTypesRepository = "ipems:global:rs-room-types@repository";
        public const string Rs_StationsInAreaPattern = "ipems:global:rs-stations-area@{0}@repository";
        public const string Rs_StationsRepository = "ipems:global:rs-stations@repository";
        public const string Rs_StationTypesRepository = "ipems:global:rs-station-types@repository";
        public const string Rs_SubCompaniesRepository = "ipems:global:rs-sub-companies@repository";
        public const string Rs_SubDeviceTypesRepository = "ipems:global:rs-sub-device-types@repository";
        public const string Rs_SuppliersRepository = "ipems:global:rs-suppliers@repository";
        public const string Rs_UnitsRepository = "ipems:global:rs-units@repository";

        //role result
        public const string Rl_UsersResultPattern = "ipems:global:role-users@{0}@result";
        public const string Rl_MenusResultPattern = "ipems:global:role-menus@{0}@result";
        public const string Rl_AreasResultPattern = "ipems:global:role-areas@{0}@result";
        public const string Rl_StationsResultPattern = "ipems:global:role-stations@{0}@result";
        public const string Rl_RoomsResultPattern = "ipems:global:role-rooms@{0}@result";
        public const string Rl_DevicesResultPattern = "ipems:global:role-devices@{0}@result";
        public const string Rl_OperationsResultPattern = "ipems:global:role-operations@{0}@result";
        public const string Rl_AreaAttributesResultPattern = "ipems:global:role-area-attributes@{0}@result";
        public const string Rl_StationAttributesResultPattern = "ipems:global:role-station-attributes@{0}@result";
        public const string Rl_DeviceAttributesResultPattern = "ipems:global:role-device-attributes@{0}@result";

        //user result
        public const string Ur_RssPointsResultPattern = "ipems:global:user-rss-points@{0}@result";
    }

    /// <summary>
    /// Site cache keys class
    /// </summary>
    public static partial class SiteCacheKeys {
        public const string Site_RolesResultPattern = "ipems:site:role@{0}@result";
        public const string Site_UsersResultPattern = "ipems:site:user@{0}@result";
        public const string Site_RssPointsResultPattern = "ipems:site:rss-points@{0}@result";
    }

    /// <summary>
    /// Cached interval class
    /// </summary>
    public static partial class CachedIntervals {
        public static readonly TimeSpan Site_StoreIntervals = TimeSpan.FromSeconds(300);
        public static readonly TimeSpan Site_ResultIntervals = TimeSpan.FromSeconds(600);

        public static readonly TimeSpan Global_RoleIntervals = TimeSpan.FromSeconds(1800);
        public static readonly TimeSpan Global_UserIntervals = TimeSpan.FromSeconds(1800);
    }
}
