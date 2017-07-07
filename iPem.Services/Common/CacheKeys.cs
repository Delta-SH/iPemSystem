using System;

namespace iPem.Services.Common {
    /// <summary>
    /// 缓存Keys
    /// </summary>
    public static partial class GlobalCacheKeys {
        /// <summary>
        /// 应用库缓存
        /// </summary>
        public const string Sc_MenusRepository = @"ipems:global:sc-menus@repository";

        /// <summary>
        /// 资源库缓存
        /// </summary>
        public const string Rs_LogicTypesRepository = @"ipems:global:rs-logic-types@repository";
        public const string Rs_SubLogicTypesRepository = @"ipems:global:rs-sub-logic-types@repository";
        public const string Rs_DeviceTypeRepository = @"ipems:global:rs-device-type@repository";
        public const string Rs_SubDeviceTypesRepository = @"ipems:global:rs-sub-device-types@repository";
        public const string Rs_RoomTypesRepository = @"ipems:global:rs-room-types@repository";
        public const string Rs_StationTypesRepository = @"ipems:global:rs-station-types@repository";
        public const string Rs_SCVendorRepository = @"ipems:global:rs-sc-vendors@repository";

        /// <summary>
        /// 全量系统层级结构缓存
        /// </summary>
        public const string SSH_Areas = @"ipems:global:ssh-areas@application";
        public const string SSH_Stations = @"ipems:global:ssh-stations@application";
        public const string SSH_Rooms = @"ipems:global:ssh-rooms@application";
        public const string SSH_Fsus = @"ipems:global:ssh-fsus@application";
        public const string SSH_Devices = @"ipems:global:ssh-devices@application";
        public const string SSH_Groups = @"ipems:global:ssh-groups@{0}@application";
        public const string SSH_Protocols = @"ipems:global:ssh-protocols@application";
        public const string SSH_Points = @"ipems:global:ssh-points@application";
        public const string SSH_SubPoints = @"ipems:global:ssh-subpoints@application";

        /// <summary>
        /// 角色系统层级结构缓存
        /// </summary>
        public const string SSH_AreasPattern = @"ipems:global:ssh-areas@{0}@application";
        public const string SSH_StationsPattern = @"ipems:global:ssh-stations@{0}@application";
        public const string SSH_RoomsPattern = @"ipems:global:ssh-rooms@{0}@application";
        public const string SSH_FsusPattern = @"ipems:global:ssh-fsus@{0}@application";
        public const string SSH_DevicesPattern = @"ipems:global:ssh-devices@{0}@application";
        public const string SSH_iDevicesPattern = @"ipems:global:ssh-idevices@{0}@application";
        public const string SSH_iStationsPattern = @"ipems:global:ssh-istations@{0}@application";
        public const string SSH_iAreasPattern = @"ipems:global:ssh-iareas@{0}@application";

        /// <summary>
        /// 角色菜单缓存
        /// </summary>
        public const string Global_MenusInRolePattern = @"ipems:global:role-menus@{0}@application";

        /// <summary>
        /// 活动告警缓存
        /// </summary>
        public const string Global_ActiveAlarms = @"ipems:global:active-alarms@application";

        /// <summary>
        /// FTP日志查询结果缓存
        /// </summary>
        public const string Global_FsuFtpFiles = @"ipems:global:fsu-ftp-files@{0}@site";

        /// <summary>
        /// Report查询结果缓存
        /// </summary>
        public const string Report_Cache_400201 = @"ipems:report:400201@{0}@user";
        public const string Report_Cache_400202 = @"ipems:report:400202@{0}@user";
        public const string Report_Cache_400203 = @"ipems:report:400203@{0}@user";
        public const string Report_Cache_400204 = @"ipems:report:400204@{0}@user";
        public const string Report_Cache_400207 = @"ipems:report:400207@{0}@user";
        public const string Report_Cache_400208 = @"ipems:report:400208@{0}@user";
        public const string Report_Cache_400401 = @"ipems:report:400401@{0}@user";
        public const string Report_Cache_400402 = @"ipems:report:400402@{0}@user";
        public const string Report_Cache_400403 = @"ipems:report:400403@{0}@user";

        /// <summary>
        /// Fsu相关查询结果缓存
        /// </summary>
        public const string Fsu_Cache_Points = @"ipems:fsu:points@{0}@user";
        public const string Fsu_Cache_AlarmPoints = @"ipems:fsu:alarm-points@{0}@user";
        public const string Fsu_Cache_ParamDiff = @"ipems:fsu:param-diff@{0}@user";
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

        /// <summary>
        /// 查询结果缓存时间
        /// </summary>
        public static readonly TimeSpan Global_SiteResult_Intervals = TimeSpan.FromSeconds(600);
    }
}
