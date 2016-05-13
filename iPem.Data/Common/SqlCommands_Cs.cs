using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlCommands_Cs {
        //role repository
        public const string Sql_Role_Repository_GetEntity1 = @"SELECT [Id],[Name],[Comment],[Enabled] FROM [dbo].[U_Roles] WHERE [Id]=@Id;";
        public const string Sql_Role_Repository_GetEntity2 = @"SELECT [Id],[Name],[Comment],[Enabled] FROM [dbo].[U_Roles] WHERE [Name]=@Name;";
        public const string Sql_Role_Repository_GetEntityByUid = @"SELECT UR.[Id],UR.[Name],UR.[Comment],UR.[Enabled] FROM [dbo].[U_Roles] UR INNER JOIN [dbo].[U_UsersInRoles] UIR ON UR.[Id] = UIR.[RoleId] WHERE UIR.[UserId] = @UserId;";
        public const string Sql_Role_Repository_GetEntities = @"SELECT [Id],[Name],[Comment],[Enabled] FROM [dbo].[U_Roles] ORDER BY [Name];";
        public const string Sql_Role_Repository_Insert = @"INSERT INTO [dbo].[U_Roles]([Id],[Name],[Comment],[Enabled]) VALUES(@Id,@Name,@Comment,@Enabled);";
        public const string Sql_Role_Repository_Update = @"UPDATE [dbo].[U_Roles] SET [Name] = @Name,[Comment] = @Comment,[Enabled] = @Enabled WHERE [Id]=@Id;";
        public const string Sql_Role_Repository_Delete = @"DELETE FROM [dbo].[U_Roles] WHERE [Id]=@Id;DELETE FROM [dbo].[U_UsersInRoles] WHERE [RoleId]=@Id;";

        //user repository
        public const string Sql_User_Repository_GetEntity1 = @"SELECT [RoleId],[Id],[Uid],[Pwd] AS [Password],[PwdFormat] AS [PasswordFormat],[PwdSalt] AS [PasswordSalt],[CreateDate],[LimitDate],[LastLoginDate],[LastPwdChangedDate] AS [LastPasswordChangedDate],[FailedPwdAttemptCount] AS [FailedPasswordAttemptCount],[FailedPwdDate] AS [FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmpId] AS [EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] WHERE [Id]=@Id;";
        public const string Sql_User_Repository_GetEntity2 = @"SELECT [RoleId],[Id],[Uid],[Pwd] AS [Password],[PwdFormat] AS [PasswordFormat],[PwdSalt] AS [PasswordSalt],[CreateDate],[LimitDate],[LastLoginDate],[LastPwdChangedDate] AS [LastPasswordChangedDate],[FailedPwdAttemptCount] AS [FailedPasswordAttemptCount],[FailedPwdDate] AS [FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmpId] AS [EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] WHERE [Uid]=@Uid;";
        public const string Sql_User_Repository_GetEntities1 = @"SELECT [RoleId],[Id],[Uid],[Pwd] AS [Password],[PwdFormat] AS [PasswordFormat],[PwdSalt] AS [PasswordSalt],[CreateDate],[LimitDate],[LastLoginDate],[LastPwdChangedDate] AS [LastPasswordChangedDate],[FailedPwdAttemptCount] AS [FailedPasswordAttemptCount],[FailedPwdDate] AS [FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmpId] AS [EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] ORDER BY [Uid];";
        public const string Sql_User_Repository_GetEntities2 = @"SELECT [RoleId],[Id],[Uid],[Pwd] AS [Password],[PwdFormat] AS [PasswordFormat],[PwdSalt] AS [PasswordSalt],[CreateDate],[LimitDate],[LastLoginDate],[LastPwdChangedDate] AS [LastPasswordChangedDate],[FailedPwdAttemptCount] AS [FailedPasswordAttemptCount],[FailedPwdDate] AS [FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmpId] AS [EmployeeId],[Enabled] FROM [dbo].[U_Users] U INNER JOIN [dbo].[U_UsersInRoles] UR ON U.[Id] = UR.[UserId] AND UR.[RoleId] = @RoleId ORDER BY [Uid];";
        public const string Sql_User_Repository_Insert = @"
        INSERT INTO [dbo].[U_Users]([Id],[Uid],[Pwd],[PwdFormat],[PwdSalt],[CreateDate],[LimitDate],[LastLoginDate],[LastPwdChangedDate],[FailedPwdAttemptCount],[FailedPwdDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmpId],[Enabled]) VALUES(@Id,@Uid,@Password,@PasswordFormat,@PasswordSalt,@CreateDate,@LimitDate,@LastLoginDate,@LastPasswordChangedDate,@FailedPasswordAttemptCount,@FailedPasswordDate,@IsLockedOut,@LastLockoutDate,@Comment,@EmployeeId,@Enabled);
        INSERT INTO [dbo].[U_UsersInRoles]([RoleId],[UserId]) VALUES(@RoleId, @Id);";
        public const string Sql_User_Repository_Update = @"
        UPDATE [dbo].[U_Users] SET [Uid] = @Uid,[CreateDate] = @CreateDate,[LimitDate] = @LimitDate,[LastLoginDate] = @LastLoginDate,[FailedPwdAttemptCount] = @FailedPasswordAttemptCount,[FailedPwdDate] = @FailedPasswordDate,[IsLockedOut] = @IsLockedOut,[LastLockoutDate] = @LastLockoutDate,[Comment] = @Comment,[EmpId] = @EmployeeId,[Enabled] = @Enabled WHERE [Id] = @Id;
        UPDATE [dbo].[U_UsersInRoles] SET [RoleId] = @RoleId WHERE [UserId] = @Id;";
        public const string Sql_User_Repository_Delete = @"
        DELETE FROM [dbo].[U_Users] WHERE [Id] = @Id;
        DELETE FROM [dbo].[U_UsersInRoles] WHERE [UserId]=@Id;
        DELETE FROM [dbo].[U_Profile] WHERE [UserId]=@Id;";
        public const string Sql_User_Repository_ChangePwd = @"UPDATE [dbo].[U_Users] SET [Pwd] = @Password,[PwdFormat] = @PasswordFormat,[PwdSalt] = @PasswordSalt,[LastPwdChangedDate] = GETDATE() WHERE [Id] = @Id;";
        public const string Sql_User_Repository_Set_LastLoginDate = @"UPDATE [dbo].[U_Users] SET [LastLoginDate] = @LastLoginDate,[FailedPwdAttemptCount] = 0 WHERE [Id] = @Id;";
        public const string Sql_User_Repository_Set_FailedPasswordDate = @"UPDATE [dbo].[U_Users] SET [FailedPwdAttemptCount] = ISNULL([FailedPwdAttemptCount], 0) + 1,[FailedPwdDate] = @FailedPasswordDate WHERE [Id] = @Id;";
        public const string Sql_User_Repository_Set_LockedOut = @"UPDATE [dbo].[U_Users] SET [IsLockedOut] = @IsLockedOut,[LastLockoutDate] = @LastLockoutDate WHERE [Id] = @Id;";

        //user profile repository
        public const string Sql_Profile_Repository_GetEntity = @"SELECT [UserId],[ValuesJson],[ValuesBinary],[LastUpdatedDate] FROM [dbo].[U_Profile] WHERE [UserId] = @UserId;";
        public const string Sql_Profile_Repository_Save = @"
        UPDATE [dbo].[U_Profile] SET [ValuesJson] = @ValuesJson,[ValuesBinary] = @ValuesBinary,[LastUpdatedDate] = @LastUpdatedDate WHERE [UserId] = @UserId;
        IF(@@ROWCOUNT = 0)
        BEGIN
	        INSERT INTO [dbo].[U_Profile]([UserId],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(@UserId,@ValuesJson,@ValuesBinary,@LastUpdatedDate);
        END";
        public const string Sql_Profile_Repository_Delete = @"DELETE FROM [dbo].[U_Profile] WHERE [UserId]=@UserId;";
        
        //web events repository
        public const string Sql_WebEvent_Repository_GetEntities = @"SELECT [Id],[Level],[Type],[ShortMessage],[FullMessage],[IpAddress],[PageUrl],[ReferrerUrl],[UserId],[CreatedTime] FROM [dbo].[H_WebEvents] WHERE [CreatedTime] BETWEEN @StartTime AND @EndTime ORDER BY [CreatedTime];";
        public const string Sql_WebEvent_Repository_Insert = @"INSERT INTO [dbo].[H_WebEvents]([Id],[Level],[Type],[ShortMessage],[FullMessage],[IpAddress],[PageUrl],[ReferrerUrl],[UserId],[CreatedTime]) VALUES(@Id,@Level,@Type,@ShortMessage,@FullMessage,@IpAddress,@PageUrl,@ReferrerUrl,@UserId,@CreatedTime);";
        public const string Sql_WebEvent_Repository_Delete = @"DELETE FROM [dbo].[H_WebEvents] WHERE [Id] = @Id;";

        //menu repository
        public const string Sql_Menu_Repository_GetEntity = @"SELECT [Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled] FROM [dbo].[U_Menus] WHERE [Id] = @Id;";
        public const string Sql_Menu_Repository_GetEntities = @"SELECT [Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled] FROM [dbo].[U_Menus] ORDER BY [LastId],[Index];";
        public const string Sql_Menu_Repository_Insert = @"INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(@Id,@Name,@Icon,@Url,@Comment,@Index,@LastId,@Enabled);";
        public const string Sql_Menu_Repository_Update = @"UPDATE [dbo].[U_Menus] SET [Name] = @Name,[Icon] = @Icon,[Url] = @Url,[Comment] = @Comment,[Index] = @Index,[LastId] = @LastId,[Enabled] = @Enabled WHERE [Id] = @Id;";
        public const string Sql_Menu_Repository_Delete = @"DELETE FROM [dbo].[U_Menus] WHERE [Id]=@Id;";

        //menus in role repository
        public const string Sql_MenusInRole_Repository_GetEntities = @"
        SELECT UM.[Id],UM.[Name],UM.[Icon],UM.[Url],UM.[Comment],UM.[Index],UM.[LastId],UM.[Enabled] FROM [dbo].[U_Menus] UM 
        INNER JOIN [dbo].[U_MenusInRoles] MIR ON UM.[Id] = MIR.[MenuId] WHERE MIR.[RoleId]=@RoleId;";
        public const string Sql_MenusInRole_Repository_Insert = @"INSERT INTO [dbo].[U_MenusInRoles]([RoleId],[MenuId]) VALUES(@RoleId,@MenuId);";
        public const string Sql_MenusInRole_Repository_Delete = @"DELETE FROM [dbo].[U_MenusInRoles] WHERE [RoleId] = @RoleId;";

        //notice repository
        public const string Sql_Notice_Repository_GetEntity = @"SELECT [Id],[Title],[Content],[CreatedTime],[Enabled] FROM [dbo].[H_Notices] WHERE [Id]=@Id;";
        public const string Sql_Notice_Repository_GetEntities1 = @"SELECT [Id],[Title],[Content],[CreatedTime],[Enabled] FROM [dbo].[H_Notices] ORDER BY [CreatedTime] DESC;";
        public const string Sql_Notice_Repository_GetEntities2 = @"
        SELECT [Id],[Title],[Content],[CreatedTime],[Enabled] FROM [dbo].[H_Notices] N
        INNER JOIN [dbo].[H_NoticesInUsers] NIU ON N.[Id]=NIU.[NoticeId] AND NIU.[UserId]=@UserId
        ORDER BY [CreatedTime] DESC;";
        public const string Sql_Notice_Repository_GetUnreadCount = @"
        SELECT COUNT(1) AS [Count] FROM [dbo].[H_Notices] N
        INNER JOIN [dbo].[H_NoticesInUsers] NIU ON N.[Id]=NIU.[NoticeId]
        WHERE N.[Enabled] = 1 AND NIU.[UserId]=@UserId AND NIU.[Readed] = 0;";
        public const string Sql_Notice_Repository_Insert = @"INSERT INTO [dbo].[H_Notices]([Id],[Title],[Content],[CreatedTime],[Enabled]) VALUES(@Id,@Title,@Content,@CreatedTime,@Enabled);";
        public const string Sql_Notice_Repository_Update = @"UPDATE [dbo].[H_Notices] SET [Title] = @Title,[Content] = @Content,[CreatedTime] = @CreatedTime,[Enabled] = @Enabled WHERE [Id] = @Id;";
        public const string Sql_Notice_Repository_Delete = @"DELETE FROM [dbo].[H_NoticesInUsers] WHERE [NoticeId]=@Id;DELETE FROM [dbo].[H_Notices] WHERE [Id]=@Id;";

        //notices in users repository
        public const string Sql_NoticesInUsers_Repository_GetEntities1 = @"SELECT [NoticeId],[UserId],[Readed],[ReadTime] FROM [dbo].[H_NoticesInUsers];";
        public const string Sql_NoticesInUsers_Repository_GetEntities2 = @"SELECT [NoticeId],[UserId],[Readed],[ReadTime] FROM [dbo].[H_NoticesInUsers] WHERE [UserId]=@UserId;";
        public const string Sql_NoticesInUsers_Repository_Insert = @"INSERT INTO [dbo].[H_NoticesInUsers]([NoticeId],[UserId],[Readed],[ReadTime]) VALUES(@NoticeId,@UserId,@Readed,@ReadTime);";
        public const string Sql_NoticesInUsers_Repository_Update = @"UPDATE [dbo].[H_NoticesInUsers] SET [Readed] = @Readed,[ReadTime] = @ReadTime WHERE [UserId] = @UserId AND [NoticeId] = @NoticeId;";
        public const string Sql_NoticesInUsers_Repository_Delete = @"DELETE FROM [dbo].[H_NoticesInUsers] WHERE [UserId] = @UserId AND [NoticeId] = @NoticeId;";

        //areas in role repository
        public const string Sql_AreaInRole_Repository_GetEntities = @"SELECT [RoleId],[AreaId] FROM [dbo].[U_AreasInRoles] WHERE [RoleId]=@RoleId;";
        public const string Sql_AreaInRole_Repository_Insert = @"INSERT INTO [dbo].[U_AreasInRoles]([RoleId],[AreaId]) VALUES(@RoleId,@AreaId);";
        public const string Sql_AreaInRole_Repository_Delete = @"DELETE FROM [dbo].[U_AreasInRoles] WHERE [RoleId]=@RoleId;";

        //operate in role repository
        public const string Sql_OperateInRole_Repository_GetEntities = @"SELECT [RoleId],[OperateId] FROM [dbo].[U_OperateInRoles] WHERE [RoleId]=@RoleId;";
        public const string Sql_OperateInRole_Repository_Insert = @"INSERT INTO [dbo].[U_OperateInRoles]([RoleId],[OperateId]) VALUES(@RoleId,@OperateId);";
        public const string Sql_OperateInRole_Repository_Delete = @"DELETE FROM [dbo].[U_OperateInRoles] WHERE [RoleId]=@RoleId;";

        //area repository
        public const string Sql_Area_Repository_GetEntitiesByRole = @"SELECT [Id],[Desc] AS [Comment],[Enabled] FROM [dbo].[M_Area] A INNER JOIN [dbo].[U_AreasInRoles] AR ON A.[Id] = AR.[AreaId] WHERE AR.[RoleId] = @RoleId;";
        public const string Sql_Area_Repository_GetEntities = @"SELECT [Id],[Desc] AS [Comment],[Enabled] FROM [dbo].[M_Area];";
        public const string Sql_Area_Repository_Insert = @"INSERT INTO [dbo].[M_Area]([Id],[Desc],[Enabled]) VALUES(@Id,@Comment,@Enabled);";
        public const string Sql_Area_Repository_Delete = @"DELETE FROM [dbo].[M_Area] WHERE [Id]=@Id;";

        //station repository
        public const string Sql_Station_Repository_GetEntities = @"SELECT [Id],[Desc] AS [Comment],[Enabled] FROM [dbo].[M_Station];";

        //room repository
        public const string Sql_Room_Repository_GetEntities = @"SELECT [Id],[Desc] AS [Comment],[Enabled] FROM [dbo].[M_Room];";

        //device repository
        public const string Sql_Device_Repository_GetEntities = @"SELECT [Id],[Code],[ProtcolId],[Desc] AS [Comment],[Enabled] FROM [dbo].[M_Device];";

        //point repository
        public const string Sql_Point_Repository_GetEntitiesByDevice = @"
        SELECT P.[Id],P.[Name],P.[Type],P.[RoomTypeId],P.[DeviceTypeId],P.[LogicTypeId],P.[Unit],P.[AlarmTimeDesc],P.[NormalTimeDesc],
        P.[AlarmLevel],P.[TriggerTypeId],P.[Interpret],P.[AlarmLimit],P.[AlarmReturnDiff],P.[AlarmRecoveryDelay],P.[AlarmDelay],
        P.[SavedPeriod],P.[AbsoluteThreshold],P.[PerThreshold],P.[Comment],P.[Desc],P.[Enabled] FROM [dbo].[M_Device] DV 
        INNER JOIN [dbo].[P_PointsInProtcol] PP ON DV.[ProtcolId] = PP.[ProtcolId] AND DV.[Id] = @DeviceId
        INNER JOIN [dbo].[P_Point] P ON PP.[PointId] = P.[Id]
        ORDER BY P.[Type],P.[Id]";
        public const string Sql_Point_Repository_GetEntitiesByType = @"SELECT [Id],[Name],[Type],[RoomTypeId],[DeviceTypeId],[LogicTypeId],[Unit],[AlarmTimeDesc],[NormalTimeDesc],[AlarmLevel],[TriggerTypeId],[Interpret],[AlarmLimit],[AlarmReturnDiff],[AlarmRecoveryDelay],[AlarmDelay],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[Comment],[Desc],[Enabled] FROM [dbo].[P_Point] WHERE [Type] = @Type ORDER BY [Id];";
        public const string Sql_Point_Repository_GetEntitiesByProtcol = @"SELECT [Id],[Name],[Type],[RoomTypeId],[DeviceTypeId],[LogicTypeId],[Unit],[AlarmTimeDesc],[NormalTimeDesc],[AlarmLevel],[TriggerTypeId],[Interpret],[AlarmLimit],[AlarmReturnDiff],[AlarmRecoveryDelay],[AlarmDelay],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[Comment],[Desc],[Enabled] FROM [dbo].[P_Point] P INNER JOIN [dbo].[P_PointsInProtcol] PP ON P.[Id] = PP.[PointId] AND PP.[ProtcolId] = @ProtcolId ORDER BY [Type],[Id];";
        public const string Sql_Point_Repository_GetEntities = @"SELECT [Id],[Name],[Type],[RoomTypeId],[DeviceTypeId],[LogicTypeId],[Unit],[AlarmTimeDesc],[NormalTimeDesc],[AlarmLevel],[TriggerTypeId],[Interpret],[AlarmLimit],[AlarmReturnDiff],[AlarmRecoveryDelay],[AlarmDelay],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[Comment],[Desc],[Enabled] FROM [dbo].[P_Point] ORDER BY [Type],[Id];";

        //protocol repository
        public const string Sql_Protocol_Repository_GetEntitiesByDeviceType = @"SELECT [Id],[Name],[DeviceTypeId],[SubDevTypeId],[Desc] AS [Comment],[Enabled] FROM [dbo].[P_Protocol] WHERE [DeviceTypeId]=@DeviceTypeId;";
        public const string Sql_Protocol_Repository_GetEntitiesByDevAndSub = @"SELECT [Id],[Name],[DeviceTypeId],[SubDevTypeId],[Desc] AS [Comment],[Enabled] FROM [dbo].[P_Protocol] WHERE [DeviceTypeId]=@DeviceTypeId AND [SubDevTypeId]=@SubDevTypeId;";
        public const string Sql_Protocol_Repository_GetEntities = @"SELECT [Id],[Name],[DeviceTypeId],[SubDevTypeId],[Desc] AS [Comment],[Enabled] FROM [dbo].[P_Protocol];";

        //points in protocol
        public const string Sql_PointsInProtocol_Repository_GetEntities = @"SELECT [ProtcolId],[PointId] FROM [dbo].[P_PointsInProtcol];";
        public const string Sql_PointsInProtocol_Repository_GetEntitiesByProtocol = @"SELECT [ProtcolId],[PointId] FROM [dbo].[P_PointsInProtcol] WHERE [ProtcolId] = @ProtcolId;";

        //dictionary repository
        public const string Sql_Dictionary_Repository_GetEntity = @"SELECT [Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate] FROM [dbo].[M_Dictionary] WHERE [Id]=@Id;";
        public const string Sql_Dictionary_Repository_GetEntities = @"SELECT [Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate] FROM [dbo].[M_Dictionary];";
        public const string Sql_Dictionary_Repository_UpdateEntities = @"UPDATE [dbo].[M_Dictionary] SET [Name] = @Name,[ValuesJson] = @ValuesJson,[ValuesBinary] = @ValuesBinary,[LastUpdatedDate] = @LastUpdatedDate WHERE [Id] = @Id;";

        //project repository
        public const string Sql_Project_Repository_GetEntities = @"SELECT [Id],[Name],[StartTime],[EndTime],[Responsible],[ContactPhone],[Company],[Creator],[CreatedTime],[Comment],[Enabled] FROM [dbo].[M_Projects] ORDER BY [CreatedTime];";
        public const string Sql_Project_Repository_GetEntitiesByDate = @"SELECT [Id],[Name],[StartTime],[EndTime],[Responsible],[ContactPhone],[Company],[CreatedTime],[Creator],[Comment],[Enabled] FROM [dbo].[M_Projects] WHERE [StartTime]>=@starttime AND [EndTime]<=@endtime ORDER BY [Name];";
        public const string Sql_Project_Repository_GetEntity = "SELECT [Id],[Name],[StartTime],[EndTime],[Responsible],[ContactPhone],[Company],[CreatedTime],[Creator],[Comment],[Enabled] FROM [dbo].[M_Projects] WHERE [Id]=@Id";
        public const string Sql_Project_Repository_Insert = "INSERT INTO [dbo].[M_Projects]([Id],[Name],[StartTime],[EndTime],[Responsible],[ContactPhone],[Company],[Creator],[CreatedTime],[Comment],[Enabled]) VALUES(@Id,@Name,@StartTime,@EndTime,@Responsible,@ContactPhone,@Company,@Creator,@CreatedTime,@Comment,@Enabled);";
        public const string Sql_Project_Repository_Update = "UPDATE [dbo].[M_Projects] SET [Name]=@Name,[StartTime]=@StartTime,[EndTime]=@EndTime,[Responsible]=@Responsible,[ContactPhone]=@ContactPhone,[Company]=@Company,[Comment]=@Comment,[Enabled]=@Enabled WHERE [Id]=@Id";

        //appointment repository
        public const string Sql_Appointment_Repository_GetEntities = @"SELECT [Id],[StartTime],[EndTime],[ProjectId],[Creator],[CreatedTime],[Comment],[Enabled] FROM [dbo].[M_Appointments] ORDER BY [CreatedTime];";
        public const string Sql_Appointment_Repository_GetEntitiesByDate = @"SELECT [Id],[StartTime],[EndTime],[ProjectId],[Creator],[CreatedTime],[Comment],[Enabled] FROM [dbo].[M_Appointments] WHERE [StartTime]>=@startTime and [EndTime]<=@endTime ORDER BY [CreatedTime];";
        public const string Sql_Appointment_Repository_GetEntity = @"SELECT [Id],[StartTime],[EndTime],[ProjectId],[Creator],[CreatedTime],[Comment],[Enabled] FROM [dbo].[M_Appointments] WHERE [Id]=@Id;";
        public const string Sql_Appointment_Repository_Insert = @"INSERT INTO [dbo].[M_Appointments]([Id],[StartTime],[EndTime],[ProjectId],[Creator],[CreatedTime],[Comment],[Enabled]) VALUES(@Id,@StartTime,@EndTime,@ProjectId,@Creator,@CreatedTime,@Comment,@Enabled);";
        public const string Sql_Appointment_Repository_Update = @"UPDATE [dbo].[M_Appointments] SET [StartTime]=@StartTime,[EndTime]=@EndTime,[ProjectId]=@ProjectId,[Creator]=@Creator,[CreatedTime]=@CreatedTime,[Comment]=@Comment,[Enabled]=@Enabled WHERE [Id]=@Id;";
        public const string Sql_Appointment_Repository_Delete = @"DELETE FROM [dbo].[M_Appointments] WHERE [Id]=@Id;";

        //NodesInAppointment Repository
        public const string Sql_NodesInAppointment_Repository_GetEntities = @"SELECT [AppointmentId],[NodeId],[NodeType] FROM [dbo].[M_NodesInAppointment];";
        public const string Sql_NodesInAppointment_Repository_GetEntitiesByNodeType = @"SELECT [AppointmentId],[NodeId],[NodeType] FROM [dbo].[M_NodesInAppointment] WHERE [NodeType]=@NodeType;";
        public const string Sql_NodesInAppointment_Repository_GetEntitiesByAppointmentId = @"SELECT [AppointmentId],[NodeId],[NodeType] FROM [dbo].[M_NodesInAppointment] WHERE [AppointmentId]=@AppointmentId;";
        public const string Sql_NodesInAppointment_Repository_Insert = @"INSERT INTO [dbo].[M_NodesInAppointment]([AppointmentId],[NodeId],[NodeType]) VALUES(@AppointmentId,@NodeId,@NodeType);";
        public const string Sql_NodesInAppointment_Repository_Delete = @"DELETE FROM [dbo].[M_NodesInAppointment] WHERE [AppointmentId]=@AppointmentId;";
    }
}
