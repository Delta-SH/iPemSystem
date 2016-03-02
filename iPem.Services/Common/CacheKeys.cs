using System;

namespace iPem.Services.Common {
    /// <summary>
    /// Global cache keys class
    /// </summary>
    public static partial class GlobalCacheKeys {
        //master repository
        public const string Cs_RolesRepository = "ipems:global:cs-role@repository";
        public const string Cs_AreasRepository = "ipems:global:cs-area@repository";
        public const string Cs_UsersInRolePattern = "ipems:global:cs-user@{0}@repository";
        public const string Cs_MenusInRolePattern = "ipems:global:cs-menu@{0}@repository";
        public const string Cs_AreasInRolePattern = "ipems:global:cs-area@{0}@repository";
        public const string Cs_StationsRepository = "ipems:global:cs-station@repository";
        public const string Cs_RoomsRepository = "ipems:global:cs-room@repository";
        public const string Cs_DevicesRepository = "ipems:global:cs-device@repository";
        public const string Cs_PointsInTypePattern = "ipems:global:cs-point-type@{0}@repository";
        public const string Cs_PointsInProtcolPattern = "ipems:global:cs-point-protcol@{0}@repository";
        public const string Cs_PointsInProtcolAndTypePattern = "ipems:global:cs-point@{0}-{1}@repository";
        public const string Cs_PointsRepository = "ipems:global:cs-point@repository";
        public const string Cs_ProtocolsInDevTypePattern = "ipems:global:cs-protocol-devtype@{0}@repository";
        public const string Cs_ProtocolsInDevTypeAndSubPattern = "ipems:global:cs-protocol@{0}-{1}@repository";
        public const string Cs_ProtocolsRepository = "ipems:global:cs-protocol@repository";

        //resource repository
        public const string Rs_EmployeesRepository = "ipems:global:rs-employee@repository";
        public const string Rs_EmployeesInDepartmentPattern = "ipems:global:rs-employee@{0}@repository";
        public const string Rs_DepartmentRepository = "ipems:global:rs-department@repository";
        public const string Rs_AreasRepository = "ipems:global:rs-area@repository";
        public const string Rs_StationsInAreaPattern = "ipems:global:rs-station-area@{0}@repository";
        public const string Rs_StationsInParentPattern = "ipems:global:rs-station-parent@{0}@repository";
        public const string Rs_StationsRepository = "ipems:global:rs-station@repository";
        public const string Rs_RoomsInParentPattern = "ipems:global:rs-room@{0}@repository";
        public const string Rs_RoomsRepository = "ipems:global:rs-room@repository";
        public const string Rs_DevicesInParentPattern = "ipems:global:rs-device@{0}@repository";
        public const string Rs_DevicesRepository = "ipems:global:rs-device@repository";
    }

    /// <summary>
    /// Site cache keys class
    /// </summary>
    public static partial class UserCacheKeys {
        public const string RolesResultPattern = "ipems:user:role@{0}@result";
        public const string UsersResultPattern = "ipems:user:user@{0}@result";
    }
}
