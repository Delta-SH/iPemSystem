using System;

namespace iPem.Services.Common {
    /// <summary>
    /// 缓存Keys
    /// </summary>
    public static partial class GlobalCacheKeys {
        /// <summary>
        /// 应用库缓存
        /// </summary>
        public const string Sc_MenusRepository = @"ipems:global:sc-menus";

        /// <summary>
        /// 资源库缓存
        /// </summary>
        public const string Rs_CamerasRepository = @"ipems:global:rs-cameras";
        public const string Rs_ChannelsRepository = @"ipems:global:rs-channels";
        public const string Rs_DevicesRepository = @"ipems:global:rs-devices";
        public const string Rs_DeviceTypeRepository = @"ipems:global:rs-device-type";
        public const string Rs_EmployeesRepository = @"ipems:global:rs-employees";
        public const string Rs_FsusRepository = @"ipems:global:rs-fsus";
        public const string Rs_GroupsRepository = @"ipems:global:rs-groups";
        public const string Rs_LogicTypesRepository = @"ipems:global:rs-logic-types";
        public const string Rs_OutEmployeesRepository = @"ipems:global:rs-out-employees";
        public const string Rs_PointsRepository = @"ipems:global:rs-points";
        public const string Rs_SubLogicTypesRepository = @"ipems:global:rs-sub-logic-types";
        public const string Rs_SubDeviceTypesRepository = @"ipems:global:rs-sub-device-types";
        public const string Rs_RoomsRepository = @"ipems:global:rs-rooms";
        public const string Rs_RoomTypesRepository = @"ipems:global:rs-room-types";
        public const string Rs_SCVendorRepository = @"ipems:global:rs-sc-vendors";
        public const string Rs_StationsRepository = @"ipems:global:rs-stations";
        public const string Rs_StationTypesRepository = @"ipems:global:rs-station-types";

        /// <summary>
        /// 系统缓存
        /// </summary>
        public const string SSH_Areas = @"ipems:global:ssh-areas";
        public const string SSH_AreasPattern = @"ipems:global:{0}:ssh-areas";
        public const string SSH_Authorizations = @"ipems:global:ssh-authorizations";
        public const string SSH_AuthorizationsPattern = @"ipems:global:{0}:ssh-authorizations";
        public const string Active_Alarms = @"ipems:global:active-alarms";
        public const string System_Alarms = @"ipems:global:system-alarms";
        public const string Auth_ConfigurationPattern = @"ipems:global:{0}:auth-configuration";
        public const string Dictionary_Ws = @"ipems:global:dictionary-ws";
        public const string Dictionary_Ts = @"ipems:global:dictionary-ts";
        public const string Dictionary_Rt = @"ipems:global:dictionary-rt";

        /// <summary>
        /// 用户应用缓存
        /// </summary>
        public const string FollowPointsPattern = @"ipems:user:{0}:follow-points";

        /// <summary>
        /// Report应用缓存
        /// </summary>
        public const string Report_400201 = @"ipems:report:{0}:400201";
        public const string Report_400202 = @"ipems:report:{0}:400202";
        public const string Report_400203 = @"ipems:report:{0}:400203";
        public const string Report_400204 = @"ipems:report:{0}:400204";
        public const string Report_400207 = @"ipems:report:{0}:400207";
        public const string Report_400208 = @"ipems:report:{0}:400208";
        public const string Report_400401 = @"ipems:report:{0}:400401";
        public const string Report_400402 = @"ipems:report:{0}:400402";
        public const string Report_400403 = @"ipems:report:{0}:400403";

        /// <summary>
        /// Fsu应用缓存
        /// </summary>
        public const string Fsu_Ftp_Files = @"ipems:fsu:{0}:fsu-ftp-files";
        public const string Fsu_Points = @"ipems:fsu:{0}:points";
        public const string Fsu_Alarm_Points = @"ipems:fsu:{0}:alarm-points";
        public const string Fsu_Param_Diff = @"ipems:fsu:{0}:param-diff";
    }

    /// <summary>
    /// 缓存周期
    /// </summary>
    public static partial class GlobalCacheInterval {
        /// <summary>
        /// 用户登录相关信息缓存周期
        /// </summary>
        public static readonly TimeSpan Store_Interval = TimeSpan.FromSeconds(600);

        /// <summary>
        /// 活动告警缓存周期
        /// </summary>
        public static readonly TimeSpan ActAlarm_Interval = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 查询结果缓存周期
        /// </summary>
        public static readonly TimeSpan SiteResult_Interval = TimeSpan.FromSeconds(600);
    }
}
