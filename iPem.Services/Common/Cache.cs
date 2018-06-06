using System;

namespace iPem.Services.Common {
    /// <summary>
    /// 缓存Key
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
        public const string Rs_CardsRepository = @"ipems:global:rs-cards";
        public const string Rs_ChannelsRepository = @"ipems:global:rs-channels";
        public const string Rs_DepartmentsRepository = @"ipems:global:rs-departments";
        public const string Rs_DevicesRepository = @"ipems:global:rs-devices";
        public const string Rs_DeviceTypeRepository = @"ipems:global:rs-device-type";
        public const string Rs_EmployeesRepository = @"ipems:global:rs-employees";
        public const string Rs_FsusRepository = @"ipems:global:rs-fsus";
        public const string Rs_FtpsRepository = @"ipems:global:rs-ftps";
        public const string Rs_GroupsRepository = @"ipems:global:rs-groups";
        public const string Rs_LogicTypesRepository = @"ipems:global:rs-logic-types";
        public const string Rs_MaskingRepository = @"ipems:global:rs-maskings";
        public const string Rs_HashMaskingRepository = @"ipems:global:rs-hash-maskings";
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
        public const string System_SC_Alarms = @"ipems:global:system-sc-alarms";
        public const string System_FSU_Alarms = @"ipems:global:system-fsu-alarms";
        public const string Dictionary_Ws = @"ipems:global:dictionary-ws";
        public const string Dictionary_Ts = @"ipems:global:dictionary-ts";
        public const string Dictionary_Rt = @"ipems:global:dictionary-rt";

        /// <summary>
        /// 用户应用缓存
        /// </summary>
        public const string FollowPointsPattern = @"ipems:user:{0}:follow-points";
        public const string RedundantAlarmsPattern = @"ipems:user:{0}:maintenance-redundant-alarms";
        public const string MaskingPattern = @"ipems:user:{0}:maintenance-maskings";
        public const string PointParamPattern = @"ipems:user:{0}:maintenance-point-param";
        public const string MatrixTablePattern = @"ipems:user:{0}:matrix-table";

        /// <summary>
        /// 用户配置缓存
        /// </summary>
        public const string ProfileFollowPattern = @"ipems:profile:{0}:follow";
        public const string ProfileConditionPattern = @"ipems:profile:{0}:condition";
        public const string ProfileMatrixPattern = @"ipems:profile:{0}:matrix";

        /// <summary>
        /// Report应用缓存
        /// </summary>
        public const string Report_400201 = @"ipems:report:{0}:400201";
        public const string Report_400202 = @"ipems:report:{0}:400202";
        public const string Report_400203 = @"ipems:report:{0}:400203";
        public const string Report_400204 = @"ipems:report:{0}:400204";
        public const string Report_400207_1 = @"ipems:report:{0}:400207_1";
        public const string Report_400207_2 = @"ipems:report:{0}:400207_2";
        public const string Report_400208_1 = @"ipems:report:{0}:400208_1";
        public const string Report_400208_2 = @"ipems:report:{0}:400208_2";
        public const string Report_400208_3 = @"ipems:report:{0}:400208_3";
        public const string Report_400210 = @"ipems:report:{0}:400210";
        public const string Report_400211 = @"ipems:report:{0}:400211";
        public const string Report_400303 = @"ipems:report:{0}:400303";
        public const string Report_400401 = @"ipems:report:{0}:{1}:400401";
        public const string Report_400402 = @"ipems:report:{0}:{1}:400402";
        public const string Report_400403 = @"ipems:report:{0}:{1}:400403";
        public const string Report_400404 = @"ipems:report:{0}:400404";
        public const string Report_400405 = @"ipems:report:{0}:400405";
        public const string Report_500302 = @"ipems:report:{0}:500302";
        public const string Report_500303 = @"ipems:report:{0}:500303";
        public const string Report_500304 = @"ipems:report:{0}:500304";
        public const string Report_500305 = @"ipems:report:{0}:500305";
        public const string Report_500306 = @"ipems:report:{0}:500306";
        public const string Report_500307 = @"ipems:report:{0}:500307";

        /// <summary>
        /// Fsu应用缓存
        /// </summary>
        public const string Fsu_Points = @"ipems:fsu:{0}:points";
        public const string Fsu_Alarm_Points = @"ipems:fsu:{0}:alarm-points";
        public const string Fsu_Param_Diff = @"ipems:fsu:{0}:param-diff";

        /// <summary>
        /// FTP应用缓存
        /// </summary>
        public const string Ftp_Info_Cfg = @"ipems:ftp:info:{0}";
        public const string Ftp_Files_List = @"ipems:ftp:files:{0}";
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
        public static readonly TimeSpan Site_Interval = TimeSpan.FromSeconds(600);

        /// <summary>
        /// 包含钻取结果集的查询缓存周期
        /// </summary>
        public static readonly TimeSpan ReSet_Interval = TimeSpan.FromSeconds(900);
    }

    /// <summary>
    /// 缓存限制
    /// </summary>
    public static partial class GlobalCacheLimit {
        /// <summary>
        /// 默认缓存限制
        /// </summary>
        public static readonly int Default_Limit = 50000;

        /// <summary>
        /// 包含钻取结果集的查询缓存限制
        /// </summary>
        public static readonly int ReSet_Limit = 100000;
    }
}
