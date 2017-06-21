using System;

namespace iPem.Services.Common {
    /// <summary>
    /// 缓存Keys
    /// </summary>
    public static partial class GlobalCacheKeys {
        /// <summary>
        /// 应用库缓存
        /// </summary>
        public const string Sc_MenusRepository = "ipems:global:sc-menus@repository";

        /// <summary>
        /// 资源库缓存
        /// </summary>
        public const string Rs_LogicTypesRepository = "ipems:global:rs-logic-types@repository";
        public const string Rs_SubLogicTypesRepository = "ipems:global:rs-sub-logic-types@repository";
        public const string Rs_DeviceTypeRepository = "ipems:global:rs-device-type@repository";
        public const string Rs_SubDeviceTypesRepository = "ipems:global:rs-sub-device-types@repository";
        public const string Rs_RoomTypesRepository = "ipems:global:rs-room-types@repository";
        public const string Rs_StationTypesRepository = "ipems:global:rs-station-types@repository";
        public const string Rs_SCVendorRepository = "ipems:global:rs-sc-vendors@repository";

        /// <summary>
        /// 全量系统层级结构缓存
        /// </summary>
        public const string SSH_Areas = "ipems:global:ssh-areas@application";
        public const string SSH_Stations = "ipems:global:ssh-stations@application";
        public const string SSH_Rooms = "ipems:global:ssh-rooms@application";
        public const string SSH_Fsus = "ipems:global:ssh-fsus@application";
        public const string SSH_Devices = "ipems:global:ssh-devices@application";
        public const string SSH_Protocols = "ipems:global:ssh-protocols@application";
        public const string SSH_Points = "ipems:global:ssh-points@application";
        public const string SSH_SubPoints = "ipems:global:ssh-subpoints@application";

        /// <summary>
        /// 角色系统层级结构缓存
        /// </summary>
        public const string SSH_AreasPattern = "ipems:global:ssh-areas@{0}@application";
        public const string SSH_StationsPattern = "ipems:global:ssh-stations@{0}@application";
        public const string SSH_RoomsPattern = "ipems:global:ssh-rooms@{0}@application";
        public const string SSH_FsusPattern = "ipems:global:ssh-fsus@{0}@application";
        public const string SSH_DevicesPattern = "ipems:global:ssh-devices@{0}@application";

        /// <summary>
        /// 角色菜单缓存
        /// </summary>
        public const string Global_MenusInRolePattern = "ipems:global:role-menus@{0}@application";

        /// <summary>
        /// 活动告警缓存
        /// </summary>
        public const string Global_ActiveAlarms = "ipems:global:active-alarms@application";        
    }

    /// <summary>
    /// Cached interval class
    /// </summary>
    public static partial class CachedIntervals {
        /// <summary>
        /// 默认缓存时间
        /// </summary>
        public static readonly TimeSpan Global_Default_Intervals = TimeSpan.FromSeconds(3600);

        /// <summary>
        /// 用户登录相关信息缓存时间
        /// </summary>
        public static readonly TimeSpan Store_Intervals = TimeSpan.FromSeconds(600);

        /// <summary>
        /// 活动告警缓存时间
        /// </summary>
        public static readonly TimeSpan Global_ActiveAlarm_Intervals = TimeSpan.FromSeconds(10);
    }
}
