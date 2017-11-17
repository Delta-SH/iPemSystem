using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static partial class SqlCommands_Sc {
        /// <summary>
        /// 组态配置表
        /// </summary>
        public const string Sql_G_Page_Repository_GetEntity = @"SELECT * FROM [dbo].[G_Pages] WHERE [Name] = @Name;";
        public const string Sql_G_Page_Repository_ExistEntity = @"SELECT COUNT(1) AS [Count] FROM [dbo].[G_Pages] WHERE [Name] = @Name;";
        public const string Sql_G_Page_Repository_GetEntities = @"SELECT * FROM [dbo].[G_Pages];";
        public const string Sql_G_Page_Repository_GetEntitiesInRole = @"SELECT * FROM [dbo].[G_Pages] WHERE [RoleId] = @RoleId;";
        public const string Sql_G_Page_Repository_GetEntitiesInObj = @"SELECT * FROM [dbo].[G_Pages] WHERE [RoleId] = @RoleId AND [ObjId] = @ObjId AND [ObjType] = @ObjType;";
        public const string Sql_G_Page_Repository_GetNamesInObj = @"SELECT [Name] FROM [dbo].[G_Pages] WHERE [RoleId] = @RoleId AND [ObjId] = @ObjId AND [ObjType] = @ObjType;";
        public const string Sql_G_Page_Repository_Insert = @"INSERT INTO [dbo].[G_Pages]([RoleId],[Name],[IsHome],[Content],[ObjId],[ObjType],[Remark]) VALUES(@RoleId,@Name,@IsHome,@Content,@ObjId,@ObjType,@Remark);";
        public const string Sql_G_Page_Repository_Update = @"UPDATE [dbo].[G_Pages] SET [RoleId] = @RoleId,[IsHome] = @IsHome,[Content] = @Content,[ObjId] = @ObjId,[ObjType] = @ObjType,[Remark] = @Remark WHERE [Name] = @Name;";
        public const string Sql_G_Page_Repository_Delete = @"DELETE FROM [dbo].[G_Pages] WHERE [Name] = @Name;";
        public const string Sql_G_Page_Repository_ClearInRole = @"DELETE FROM [dbo].[G_Pages] WHERE [RoleId] = @RoleId;";
        public const string Sql_G_Page_Repository_Clear = @"DELETE FROM [dbo].[G_Pages];";

        /// <summary>
        /// 组态模板表
        /// </summary>
        public const string Sql_G_Template_Repository_GetEntity = @"SELECT * FROM [dbo].[G_Templates] WHERE [Name] = @Name;";
        public const string Sql_G_Template_Repository_ExistEntity = @"SELECT COUNT(1) AS [Count] FROM [dbo].[G_Templates] WHERE [Name] = @Name;";
        public const string Sql_G_Template_Repository_GetEntities = @"SELECT * FROM [dbo].[G_Templates];";
        public const string Sql_G_Template_Repository_GetNames = @"SELECT [Name] FROM [dbo].[G_Templates];";
        public const string Sql_G_Template_Repository_Insert = @"INSERT INTO [dbo].[G_Templates]([Name],[Content],[Remark]) VALUES(@Name,@Content,@Remark);";
        public const string Sql_G_Template_Repository_Update = @"UPDATE [dbo].[G_Templates] SET [Content] = @Content,[Remark] = @Remark WHERE [Name] = @Name;";
        public const string Sql_G_Template_Repository_Delete = @"DELETE FROM [dbo].[G_Templates] WHERE [Name] = @Name;";
        public const string Sql_G_Template_Repository_Clear = @"DELETE FROM [dbo].[G_Templates];";

        /// <summary>
        /// 组态图片表
        /// </summary>
        public const string Sql_G_Image_Repository_GetEntity = @"SELECT * FROM [dbo].[G_Images] WHERE [Name] = @Name;";
        public const string Sql_G_Image_Repository_ExistEntity = @"SELECT COUNT(1) AS [Count] FROM [dbo].[G_Images] WHERE [Name] = @Name;";
        public const string Sql_G_Image_Repository_GetEntities = @"SELECT * FROM [dbo].[G_Images];";
        public const string Sql_G_Image_Repository_GetNames = @"SELECT [Name],[Type],[UpdateMark],[Remark] FROM [dbo].[G_Images];";
        public const string Sql_G_Image_Repository_GetContents = @"SELECT [Name],[Type],[Content],[UpdateMark],[Remark] FROM [dbo].[G_Images];";
        public const string Sql_G_Image_Repository_GetThumbnails = @"SELECT [Name],[Type],[Thumbnail],[UpdateMark],[Remark] FROM [dbo].[G_Images];";
        public const string Sql_G_Image_Repository_Insert = @"INSERT INTO [dbo].[G_Images]([Name],[Type],[Content],[Thumbnail],[UpdateMark],[Remark]) VALUES(@Name,@Type,@Content,@Thumbnail,@UpdateMark,@Remark);";
        public const string Sql_G_Image_Repository_Update = @"UPDATE [dbo].[G_Images] SET [Type] = @Type,[Content] = @Content,[Thumbnail] = @Thumbnail,[UpdateMark] = @UpdateMark,[Remark] = @Remark WHERE [Name] = @Name;";
        public const string Sql_G_Image_Repository_Delete = @"DELETE FROM [dbo].[G_Images] WHERE [Name] = @Name;";
        public const string Sql_G_Image_Repository_Clear = @"DELETE FROM [dbo].[G_Images];";

        /// <summary>
        /// 系统消息表
        /// </summary>
        public const string Sql_H_Notice_Repository_GetNotice = @"SELECT * FROM [dbo].[H_Notices] WHERE [Id]=@Id;";
        public const string Sql_H_Notice_Repository_GetNotices = @"SELECT * FROM [dbo].[H_Notices] ORDER BY [CreatedTime] DESC;";
        public const string Sql_H_Notice_Repository_GetNoticesInSpan = @"SELECT * FROM [dbo].[H_Notices] WHERE [CreatedTime] BETWEEN @Start AND @End ORDER BY [CreatedTime] DESC;";        
        public const string Sql_H_Notice_Repository_GetNoticesInUser = @"SELECT N.* FROM [dbo].[H_Notices] N INNER JOIN [dbo].[H_NoticesInUsers] NIU ON N.[Id]=NIU.[NoticeId] AND NIU.[UserId]=@UserId ORDER BY [CreatedTime] DESC;";
        public const string Sql_H_Notice_Repository_GetUnreadNotices = @"SELECT N.* FROM [dbo].[H_Notices] N INNER JOIN [dbo].[H_NoticesInUsers] NIU ON N.[Id]=NIU.[NoticeId] WHERE N.[Enabled] = 1 AND NIU.[Readed] = 0 AND NIU.[UserId]=@UserId;";
        public const string Sql_H_Notice_Repository_Insert = @"INSERT INTO [dbo].[H_Notices]([Id],[Title],[Content],[CreatedTime],[Enabled]) VALUES(@Id,@Title,@Content,@CreatedTime,@Enabled);";
        public const string Sql_H_Notice_Repository_Update = @"UPDATE [dbo].[H_Notices] SET [Title] = @Title,[Content] = @Content,[CreatedTime] = @CreatedTime,[Enabled] = @Enabled WHERE [Id] = @Id;";
        public const string Sql_H_Notice_Repository_Delete = @"DELETE FROM [dbo].[H_NoticesInUsers] WHERE [NoticeId]=@Id;DELETE FROM [dbo].[H_Notices] WHERE [Id]=@Id;";

        /// <summary>
        /// 用户消息映射表
        /// </summary>
        public const string Sql_H_NoticeInUser_Repository_GetNoticesInUsers = @"SELECT * FROM [dbo].[H_NoticesInUsers];";
        public const string Sql_H_NoticeInUser_Repository_GetNoticesInUser = @"SELECT * FROM [dbo].[H_NoticesInUsers] WHERE [UserId]=@UserId;";
        public const string Sql_H_NoticeInUser_Repository_Insert = @"INSERT INTO [dbo].[H_NoticesInUsers]([NoticeId],[UserId],[Readed],[ReadTime]) VALUES(@NoticeId,@UserId,@Readed,@ReadTime);";
        public const string Sql_H_NoticeInUser_Repository_Update = @"UPDATE [dbo].[H_NoticesInUsers] SET [Readed] = @Readed,[ReadTime] = @ReadTime WHERE [UserId] = @UserId AND [NoticeId] = @NoticeId;";
        public const string Sql_H_NoticeInUser_Repository_Delete = @"DELETE FROM [dbo].[H_NoticesInUsers] WHERE [UserId] = @UserId AND [NoticeId] = @NoticeId;";

        /// <summary>
        /// 系统日志表
        /// </summary>
        public const string Sql_H_WebEvent_Repository_GetWebEvents = @"SELECT * FROM [dbo].[H_WebEvents] WHERE [CreatedTime] BETWEEN @StartTime AND @EndTime ORDER BY [CreatedTime];";
        public const string Sql_H_WebEvent_Repository_Insert = @"INSERT INTO [dbo].[H_WebEvents]([Id],[Level],[Type],[ShortMessage],[FullMessage],[IpAddress],[PageUrl],[ReferrerUrl],[UserId],[CreatedTime]) VALUES(@Id,@Level,@Type,@ShortMessage,@FullMessage,@IpAddress,@PageUrl,@ReferrerUrl,@UserId,@CreatedTime);";
        public const string Sql_H_WebEvent_Repository_Delete = @"DELETE FROM [dbo].[H_WebEvents] WHERE [Id] = @Id;";

        /// <summary>
        /// 系统参数表
        /// </summary>
        public const string Sql_M_Dictionary_Repository_GetDictionary = @"SELECT * FROM [dbo].[M_Dictionary] WHERE [Id]=@Id;";
        public const string Sql_M_Dictionary_Repository_GetDictionaries = @"SELECT * FROM [dbo].[M_Dictionary];";
        public const string Sql_M_Dictionary_Repository_Update = @"UPDATE [dbo].[M_Dictionary] SET [ValuesJson] = @ValuesJson,[ValuesBinary] = @ValuesBinary,[LastUpdatedDate] = @LastUpdatedDate WHERE [Id] = @Id;";

        /// <summary>
        /// 能耗公式表
        /// </summary>
        public const string Sql_M_Formula_Repository_GetFormula = @"SELECT * FROM [dbo].[M_Formulas] WHERE [Id]=@Id AND [Type]=@Type AND [FormulaType]=@FormulaType;";
        public const string Sql_M_Formula_Repository_GetFormulas = @"SELECT * FROM [dbo].[M_Formulas] WHERE [Id]=@Id AND [Type]=@Type;";
        public const string Sql_M_Formula_Repository_GetAllFormulas = @"SELECT * FROM [dbo].[M_Formulas];";
        public const string Sql_M_Formula_Repository_Save = @"
        UPDATE [dbo].[M_Formulas] SET [ComputeType]=@ComputeType,[Formula]=@Formula,[Comment]=@Comment,[CreatedTime]=@CreatedTime WHERE [Id]=@Id AND [Type]=@Type AND [FormulaType]=@FormulaType;
        IF(@@ROWCOUNT = 0)
        BEGIN
	        INSERT INTO [dbo].[M_Formulas]([Id],[Type],[FormulaType],[ComputeType],[Formula],[Comment],[CreatedTime]) VALUES(@Id,@Type,@FormulaType,@ComputeType,@Formula,@Comment,@CreatedTime);
        END";

        /// <summary>
        /// 节点(区域、站点、机房、设备)预约映射表
        /// </summary>
        public const string Sql_M_NodesInReservation_Repository_GetNodesInReservations = @"SELECT * FROM [dbo].[M_NodesInReservation];";
        public const string Sql_M_NodesInReservation_Repository_GetNodesInReservationsInType = @"SELECT * FROM [dbo].[M_NodesInReservation] WHERE [NodeType]=@NodeType;";
        public const string Sql_M_NodesInReservation_Repository_GetNodesInReservationsInReservation = @"SELECT * FROM [dbo].[M_NodesInReservation] WHERE [ReservationId]=@ReservationId;";
        public const string Sql_M_NodesInReservation_Repository_Insert = @"INSERT INTO [dbo].[M_NodesInReservation]([ReservationId],[NodeId],[NodeType]) VALUES(@ReservationId,@NodeId,@NodeType);";
        public const string Sql_M_NodesInReservation_Repository_Delete = @"DELETE FROM [dbo].[M_NodesInReservation] WHERE [ReservationId]=@ReservationId;";

        /// <summary>
        /// 工程信息表
        /// </summary>
        public const string Sql_M_Project_Repository_GetProjects = @"SELECT * FROM [dbo].[M_Projects] ORDER BY [CreatedTime];";
        public const string Sql_M_Project_Repository_GetValidProjects = @"SELECT * FROM [dbo].[M_Projects] WHERE [EndTime]>GETDATE() ORDER BY [CreatedTime];";
        public const string Sql_M_Project_Repository_GetProjectsInSpan = @"SELECT * FROM [dbo].[M_Projects] WHERE [StartTime]>=@StartTime AND [EndTime]<=@EndTime ORDER BY [Name];";
        public const string Sql_M_Project_Repository_GetProject = @"SELECT * FROM [dbo].[M_Projects] WHERE [Id]=@Id";
        public const string Sql_M_Project_Repository_Insert = @"INSERT INTO [dbo].[M_Projects]([Id],[Name],[StartTime],[EndTime],[Responsible],[ContactPhone],[Company],[Creator],[CreatedTime],[Comment],[Enabled]) VALUES(@Id,@Name,@StartTime,@EndTime,@Responsible,@ContactPhone,@Company,@Creator,@CreatedTime,@Comment,@Enabled);";
        public const string Sql_M_Project_Repository_Update = @"UPDATE [dbo].[M_Projects] SET [Name]=@Name,[StartTime]=@StartTime,[EndTime]=@EndTime,[Responsible]=@Responsible,[ContactPhone]=@ContactPhone,[Company]=@Company,[Comment]=@Comment,[Enabled]=@Enabled WHERE [Id]=@Id";

        /// <summary>
        /// 工程预约表
        /// </summary>
        public const string Sql_M_Reservation_Repository_GetReservations = @"SELECT * FROM [dbo].[M_Reservations] ORDER BY [CreatedTime];";
        public const string Sql_M_Reservation_Repository_GetReservationsInSpan = @"SELECT * FROM [dbo].[M_Reservations] WHERE [StartTime]>=@startTime and [EndTime]<=@endTime ORDER BY [CreatedTime];";
        public const string Sql_M_Reservation_Repository_GetReservation = @"SELECT * FROM [dbo].[M_Reservations] WHERE [Id]=@Id;";
        public const string Sql_M_Reservation_Repository_Insert = @"INSERT INTO [dbo].[M_Reservations]([Id],[Name],[StartTime],[EndTime],[ProjectId],[Creator],[CreatedTime],[Comment],[Enabled]) VALUES(@Id,@Name,@StartTime,@EndTime,@ProjectId,@Creator,@CreatedTime,@Comment,@Enabled);";
        public const string Sql_M_Reservation_Repository_Update = @"UPDATE [dbo].[M_Reservations] SET [Name]=@Name,[StartTime]=@StartTime,[EndTime]=@EndTime,[ProjectId]=@ProjectId,[Creator]=@Creator,[CreatedTime]=@CreatedTime,[Comment]=@Comment,[Enabled]=@Enabled WHERE [Id]=@Id;";
        public const string Sql_M_Reservation_Repository_Delete = @"DELETE FROM [dbo].[M_Reservations] WHERE [Id]=@Id;";

        /// <summary>
        /// 关注信号表
        /// </summary>
        public const string Sql_U_FollowPoint_Repository_GetFollowPointsInUser = @"SELECT * FROM [dbo].[U_FollowPoints] WHERE [UserId]=@UserId;";
        public const string Sql_U_FollowPoint_Repository_GetFollowPoints = @"SELECT * FROM [dbo].[U_FollowPoints];";
        public const string Sql_U_FollowPoint_Repository_Insert = @"INSERT INTO [dbo].[U_FollowPoints]([UserId],[DeviceId],[PointId]) VALUES(@UserId,@DeviceId,@PointId);";
        public const string Sql_U_FollowPoint_Repository_Delete = @"DELETE FROM [dbo].[U_FollowPoints] WHERE [UserId] = @UserId AND [DeviceId] = @DeviceId AND [PointId] = @PointId;";

        /// <summary>
        /// 系统菜单表
        /// </summary>
        public const string Sql_U_Menu_Repository_GetMenu = @"SELECT * FROM [dbo].[U_Menus] WHERE [Id] = @Id;";
        public const string Sql_U_Menu_Repository_GetMenusInRole = @"SELECT M.* FROM [dbo].[U_Menus] M INNER JOIN [dbo].[U_MenusInRoles] MR ON M.[Id] = MR.[MenuId] AND MR.[RoleId] = @RoleId AND M.[Enabled]=1 ORDER BY M.[LastId],M.[Index];";
        public const string Sql_U_Menu_Repository_GetMenus = @"SELECT * FROM [dbo].[U_Menus] WHERE [Enabled]=1 ORDER BY [LastId],[Index];";
        public const string Sql_U_Menu_Repository_Insert = @"INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(@Id,@Name,@Icon,@Url,@Comment,@Index,@LastId,@Enabled);";
        public const string Sql_U_Menu_Repository_Update = @"UPDATE [dbo].[U_Menus] SET [Name] = @Name,[Icon] = @Icon,[Url] = @Url,[Comment] = @Comment,[Index] = @Index,[LastId] = @LastId,[Enabled] = @Enabled WHERE [Id] = @Id;";
        public const string Sql_U_Menu_Repository_Delete = @"DELETE FROM [dbo].[U_Menus] WHERE [Id]=@Id;";

        /// <summary>
        /// 角色对象关系映射表
        /// </summary>
        public const string Sql_U_EntitiesInRole_Repository_GetEntitiesInRole = @"
        SELECT * FROM [dbo].[U_MenusInRoles] WHERE [RoleId]=@RoleId;
        SELECT * FROM [dbo].[U_AreasInRoles] WHERE [RoleId]=@RoleId;
        SELECT * FROM [dbo].[U_StationsInRoles] WHERE [RoleId]=@RoleId;
        SELECT * FROM [dbo].[U_RoomsInRoles] WHERE [RoleId]=@RoleId;
        SELECT * FROM [dbo].[U_PermissionsInRoles] WHERE [RoleId]=@RoleId;";
        public const string Sql_U_EntitiesInRole_Repository_Insert1 = @"INSERT INTO [dbo].[U_MenusInRoles]([RoleId],[MenuId]) VALUES(@RoleId,@MenuId);";
        public const string Sql_U_EntitiesInRole_Repository_Insert2 = @"INSERT INTO [dbo].[U_AreasInRoles]([RoleId],[AreaId]) VALUES(@RoleId,@AreaId);";
        public const string Sql_U_EntitiesInRole_Repository_Insert3 = @"INSERT INTO [dbo].[U_StationsInRoles]([RoleId],[StationId]) VALUES(@RoleId,@StationId);";
        public const string Sql_U_EntitiesInRole_Repository_Insert4 = @"INSERT INTO [dbo].[U_RoomsInRoles]([RoleId],[RoomId]) VALUES(@RoleId,@RoomId);";
        public const string Sql_U_EntitiesInRole_Repository_Insert5 = @"INSERT INTO [dbo].[U_PermissionsInRoles]([RoleId],[Permission]) VALUES(@RoleId, @Permission);";
        public const string Sql_U_EntitiesInRole_Repository_Delete = @"
        DELETE FROM [dbo].[U_MenusInRoles] WHERE [RoleId]=@RoleId;
        DELETE FROM [dbo].[U_AreasInRoles] WHERE [RoleId]=@RoleId;
        DELETE FROM [dbo].[U_StationsInRoles] WHERE [RoleId]=@RoleId;
        DELETE FROM [dbo].[U_RoomsInRoles] WHERE [RoleId]=@RoleId;
        DELETE FROM [dbo].[U_PermissionsInRoles] WHERE [RoleId]=@RoleId;";

        /// <summary>
        /// 用户自定义信息表
        /// </summary>
        public const string Sql_U_Profile_Repository_GetProfile = @"SELECT * FROM [dbo].[U_Profile] WHERE [UserId] = @UserId;";
        public const string Sql_U_Profile_Repository_Save = @"
        UPDATE [dbo].[U_Profile] SET [ValuesJson] = @ValuesJson,[ValuesBinary] = @ValuesBinary,[LastUpdatedDate] = @LastUpdatedDate WHERE [UserId] = @UserId;
        IF(@@ROWCOUNT = 0)
        BEGIN
	        INSERT INTO [dbo].[U_Profile]([UserId],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(@UserId,@ValuesJson,@ValuesBinary,@LastUpdatedDate);
        END";
        public const string Sql_U_Profile_Repository_Delete = @"DELETE FROM [dbo].[U_Profile] WHERE [UserId]=@UserId;";

        /// <summary>
        /// 角色信息表
        /// </summary>
        public const string Sql_U_Role_Repository_GetRoleById = @"SELECT * FROM [dbo].[U_Roles] WHERE [Id]=@Id;";
        public const string Sql_U_Role_Repository_GetRoleByName = @"SELECT * FROM [dbo].[U_Roles] WHERE [Name]=@Name;";
        public const string Sql_U_Role_Repository_GetRoleByUid = @"SELECT UR.* FROM [dbo].[U_Roles] UR INNER JOIN [dbo].[U_UsersInRoles] UIR ON UR.[Id] = UIR.[RoleId] WHERE UIR.[UserId] = @UserId;";
        public const string Sql_U_Role_Repository_GetRoles = @"SELECT * FROM [dbo].[U_Roles] ORDER BY [Name];";
        public const string Sql_U_Role_Repository_Insert = @"INSERT INTO [dbo].[U_Roles]([Id],[Name],[Comment],[Enabled]) VALUES(@Id,@Name,@Comment,@Enabled);";
        public const string Sql_U_Role_Repository_Update = @"UPDATE [dbo].[U_Roles] SET [Name] = @Name,[Comment] = @Comment,[Enabled] = @Enabled WHERE [Id]=@Id;";
        public const string Sql_U_Role_Repository_Delete = @"
        DELETE FROM [dbo].[U_UsersInRoles] WHERE [RoleId]=@Id;
        DELETE FROM [dbo].[U_MenusInRoles] WHERE [RoleId]=@Id;
        DELETE FROM [dbo].[U_AreasInRoles] WHERE [RoleId]=@Id;
        DELETE FROM [dbo].[U_PermissionsInRoles] WHERE [RoleId]=@Id;
        DELETE FROM [dbo].[U_Roles] WHERE [Id]=@Id;";

        /// <summary>
        /// 用户信息表
        /// </summary>
        public const string Sql_U_User_Repository_GetUserById = @"SELECT [RoleId],[Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] WHERE U.[Id]=@Id;";
        public const string Sql_U_User_Repository_GetUserByName = @"SELECT [RoleId],[Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] WHERE U.[Uid]=@Uid;";
        public const string Sql_U_User_Repository_GetUsers = @"SELECT [RoleId],[Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] ORDER BY [Uid];";
        public const string Sql_U_User_Repository_GetUsersInRole = @"SELECT [RoleId],[Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] AND UR.[RoleId] = @RoleId ORDER BY [Uid];";
        public const string Sql_U_User_Repository_Insert = @"
        INSERT INTO [dbo].[U_Users]([Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled]) VALUES(@Id,@Uid,@Password,@PasswordFormat,@PasswordSalt,@CreatedDate,@LimitedDate,@LastLoginDate,@LastPasswordChangedDate,@FailedPasswordAttemptCount,@FailedPasswordDate,@IsLockedOut,@LastLockoutDate,@Comment,@EmployeeId,@Enabled);
        INSERT INTO [dbo].[U_UsersInRoles]([RoleId],[UserId]) VALUES(@RoleId, @Id);";
        public const string Sql_U_User_Repository_Update = @"
        UPDATE [dbo].[U_Users] SET [Uid] = @Uid,[CreatedDate] = @CreatedDate,[LimitedDate] = @LimitedDate,[LastLoginDate] = @LastLoginDate,[FailedPasswordAttemptCount] = @FailedPasswordAttemptCount,[FailedPasswordDate] = @FailedPasswordDate,[IsLockedOut] = @IsLockedOut,[LastLockoutDate] = @LastLockoutDate,[Comment] = @Comment,[EmployeeId] = @EmployeeId,[Enabled] = @Enabled WHERE [Id] = @Id;
        UPDATE [dbo].[U_UsersInRoles] SET [RoleId] = @RoleId WHERE [UserId] = @Id;";
        public const string Sql_U_User_Repository_Delete = @"
        DELETE FROM [dbo].[U_UsersInRoles] WHERE [UserId]=@Id;
        DELETE FROM [dbo].[U_Profile] WHERE [UserId]=@Id;
        DELETE FROM [dbo].[U_Users] WHERE [Id]=@Id;";
        public const string Sql_U_User_Repository_ChangePassword = @"UPDATE [dbo].[U_Users] SET [Password] = @Password,[PasswordFormat] = @PasswordFormat,[PasswordSalt] = @PasswordSalt,[LastPasswordChangedDate] = GETDATE() WHERE [Id] = @Id;";
        public const string Sql_U_User_Repository_SetLastLoginDate = @"UPDATE [dbo].[U_Users] SET [LastLoginDate] = @LastLoginDate,[FailedPasswordAttemptCount] = 0 WHERE [Id] = @Id;";
        public const string Sql_U_User_Repository_SetFailedPasswordDate = @"UPDATE [dbo].[U_Users] SET [FailedPasswordAttemptCount] = ISNULL([FailedPasswordAttemptCount], 0) + 1,[FailedPasswordDate] = @FailedPasswordDate WHERE [Id] = @Id;";
        public const string Sql_U_User_Repository_SetLockedOut = @"UPDATE [dbo].[U_Users] SET [IsLockedOut] = @IsLockedOut,[LastLockoutDate] = @LastLockoutDate WHERE [Id] = @Id;";

        /// <summary>
        /// 脚本升级表
        /// </summary>
        public const string Sql_H_DBScript_Repository_GetEntities = @"SELECT * FROM [dbo].[H_DBScript] ORDER BY [ID];";
        public const string Sql_H_DBScript_Repository_Insert = @"INSERT INTO [dbo].[H_DBScript]([ID],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,@Name,@Creator,@CreatedTime,@Executor,@ExecutedTime,@Comment);";
        public const string Sql_H_DBScript_Repository_Update = @"UPDATE [dbo].[H_DBScript] SET [ExecuteUser] = @Executor WHERE [ID] = @Id;";
        public const string Sql_H_DBScript_Repository_Delete = @"DELETE FROM [dbo].[H_DBScript] WHERE [ID] = @Id;";
    }
}
