using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static partial class SqlCommands_Cs {
        /// <summary>
        /// 活动告警表
        /// </summary>
        public const string Sql_A_AAlarm_Repository_GetAlarmsInArea = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [AreaId] = @AreaId AND [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInStation = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [StationId] = @StationId AND [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInRoom = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [RoomId] = @RoomId AND [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInDevice = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [DeviceId] = @DeviceId AND [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInSpan = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [AlarmTime] BETWEEN @Start AND @End AND [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [Masked] = 0 AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetSystemAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [RoomId] = '-1' ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAllAlarmsInSpan = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [AlarmTime] BETWEEN @Start AND @End ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAllAlarms = @"SELECT * FROM [dbo].[A_AAlarm] ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetPrimaryAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [Masked] = 0 AND [PrimaryId] = @PrimaryId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetRelatedAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [Masked] = 0 AND [RelatedId] = @RelatedId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetFilterAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [Masked] = 0 AND [FilterId] = @FilterId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_Confirm = @"UPDATE [dbo].[A_AAlarm] SET [Confirmed] = @Confirmed,[Confirmer] = @Confirmer,[ConfirmedTime] = @ConfirmedTime WHERE [Id] = @Id;";
        public const string Sql_A_AAlarm_Repository_GetAlarm = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [Id] = @Id;";
        public const string Sql_A_AAlarm_Repository_DeleteAlarm = @"
        DELETE FROM [dbo].[A_AAlarm] WHERE [Id] = @Id;
        DELETE FROM [dbo].[A_IAlarm] WHERE [FsuId] = @FsuId AND [SerialNo] = @SerialNo;";


        /// <summary>
        /// 告警流水表
        /// </summary>
        public const string Sql_A_TAlarm_Repository_GetEntity = @"SELECT * FROM [dbo].[A_TAlarm] WHERE [FsuId] = @FsuId AND [SerialNo] = @SerialNo AND [AlarmFlag] = @AlarmFlag;";
        public const string Sql_A_TAlarm_Repository_GetEntities = @"SELECT * FROM [dbo].[A_TAlarm] ORDER BY [AlarmTime];";
        public const string Sql_A_TAlarm_Repository_Delete = @"DELETE FROM [dbo].[A_TAlarm] WHERE [FsuId] = @FsuId AND [SerialNo] = @SerialNo AND [AlarmFlag] = @AlarmFlag;";
        public const string Sql_A_TAlarm_Repository_Insert = @"
        IF NOT EXISTS( SELECT 1 FROM [dbo].[A_TAlarm] WHERE [FsuId]=@FsuId AND [SerialNo]=@SerialNo AND [AlarmFlag]=@AlarmFlag)
        BEGIN
	        INSERT INTO [dbo].[A_TAlarm]([FsuId],[DeviceId],[PointId],[SignalId],[SignalNumber],[SerialNo],[NMAlarmId],[AlarmTime],[AlarmLevel],[AlarmFlag],[AlarmDesc],[AlarmValue],[AlarmRemark]) 
	        VALUES(@FsuId,@DeviceId,@PointId,@SignalId,@SignalNumber,@SerialNo,@NMAlarmId,@AlarmTime,@AlarmLevel,@AlarmFlag,@AlarmDesc,@AlarmValue,@AlarmRemark);
        END";

        /// <summary>
        /// 历史告警表
        /// </summary>
        public const string Sql_A_HAlarm_Repository_GetAlarmsInArea = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [AreaId] = ''' + @AreaId + N''' AND [RoomId] <> ''-1'' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAlarmsInStation = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END

                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [StationId] = ''' + @StationId + N''' AND [RoomId] <> ''-1'' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAlarmsInRoom = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [RoomId] = ''' + @RoomId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAlarmsInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N''' AND [RoomId] <> ''-1'' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAlarmsInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END

                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [PointId] = ''' + @PointId + N''' AND [RoomId] <> ''-1'' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';        			
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE  [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [RoomId] <> ''-1'' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [Masked] = 0';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetAllAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetNonAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND ([RoomId] = ''-1'' OR [Masked] = 1)';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetPrimaryAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PrimaryId] = ''' + @PrimaryId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetRelatedAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [RelatedId] = ''' + @RelatedId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetFilterAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [FilterId] = ''' + @FilterId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_A_HAlarm_Repository_GetReversalAlarms = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_HAlarm'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ReversalId] = ''' + @ReversalId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Alarms AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM Alarms ORDER BY [StartTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 脚本升级表
        /// </summary>
        public const string Sql_H_DBScript_Repository_GetEntities = @"SELECT * FROM [dbo].[H_DBScript] ORDER BY [ID];";
        public const string Sql_H_DBScript_Repository_Insert = @"INSERT INTO [dbo].[H_DBScript]([ID],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,@Name,@Creator,@CreatedTime,@Executor,@ExecutedTime,@Comment);";
        public const string Sql_H_DBScript_Repository_Update = @"UPDATE [dbo].[H_DBScript] SET [ExecuteUser] = @Executor WHERE [ID] = @Id;";
        public const string Sql_H_DBScript_Repository_Delete = @"DELETE FROM [dbo].[H_DBScript] WHERE [ID] = @Id;";

        /// <summary>
        /// Fsu操作日志表
        /// </summary>
        public const string Sql_H_FsuEvent_Repository_GetEvents = @"SELECT [FsuId],[EventType],[EventDesc] AS [Message],[EventTime] FROM [dbo].[H_FsuEvent] WHERE [EventTime] BETWEEN @Start AND @End;";
        public const string Sql_H_FsuEvent_Repository_GetEventsInType = @"SELECT [FsuId],[EventType],[EventDesc] AS [Message],[EventTime] FROM [dbo].[H_FsuEvent] WHERE [EventTime] BETWEEN @Start AND @End AND [EventType] = @EventType;";

        /// <summary>
        /// 资管接口区域表
        /// </summary>
        public const string Sql_H_IArea_Repository_GetAreas = @"
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IArea{0}]') AND type in (N'U'))
        BEGIN
	        SELECT * FROM [dbo].[H_IArea{0}];
        END";

        /// <summary>
        /// 资管接口站点表
        /// </summary>
        public const string Sql_H_IStation_Repository_GetStations = @"
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IStation{0}]') AND type in (N'U'))
        BEGIN
	        SELECT * FROM [dbo].[H_IStation{0}];
        END";

        /// <summary>
        /// 资管接口设备表
        /// </summary>
        public const string Sql_H_IDevice_Repository_GetDevices = @"
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IDevice{0}]') AND type in (N'U'))
        BEGIN
	        SELECT * FROM [dbo].[H_IDevice{0}];
        END";

        /// <summary>
        /// 实时性能数据表
        /// </summary>
        public const string Sql_V_AMeasure_Repository_GetMeasure = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [DeviceId] = @DeviceId AND [PointId] = @PointId;";        
        public const string Sql_V_AMeasure_Repository_GetMeasuresInArea = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [AreaId] = @AreaId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInStation = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [StationId] = @StationId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInRoom = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [RoomId] = @RoomId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInDevice = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [DeviceId] = @DeviceId;";
        public const string Sql_V_AMeasure_Repository_GetMeasures = @"SELECT * FROM [dbo].[V_AMeasure];";

        /// <summary>
        /// 电池数据表
        /// </summary>
        public const string Sql_V_Bat_Repository_GetValuesInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Bat'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisBat AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisBat ORDER BY [StartTime],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Bat_Repository_GetValuesInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Bat'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisBat AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisBat ORDER BY [StartTime],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Bat_Repository_GetValues = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Bat'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisBat AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisBat ORDER BY [StartTime],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 电池充放电曲线表
        /// </summary>
        public const string Sql_V_BatCurve_Repository_GetProceduresInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatCurve'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ProcTime] = ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND [DeviceId]=''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM V ORDER BY [PointId],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatCurve_Repository_GetProceduresInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatCurve'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ProcTime] = ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND [DeviceId]=''' + @DeviceId + N''' AND [PointId]='''+ @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM V ORDER BY [ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatCurve_Repository_GetValuesInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatCurve'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId]=''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM V ORDER BY [PointId],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatCurve_Repository_GetValuesInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatCurve'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId]=''' + @DeviceId + N''' AND [PointId]='''+ @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM V ORDER BY [ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatCurve_Repository_GetValues = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatCurve'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM V ORDER BY [DeviceId],[PointId],[ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 电池充放电过程表
        /// </summary>
        public const string Sql_V_BatTime_Repository_GetValuesInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatTime'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM V ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatTime_Repository_GetValuesInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatTime'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM V ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatTime_Repository_GetValues = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatTime'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM V ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_BatTime_Repository_GetProcedures = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_BatTime'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH V AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT [DeviceId],[ProcTime],MIN([StartTime]) AS [StartTime],MAX([EndTime]) AS [EndTime] FROM V GROUP BY [DeviceId],[ProcTime] ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 断站、停电、发电表
        /// </summary>
        public const string Sql_V_Cut_Repository_GetCuttings = @"SELECT * FROM [dbo].[V_Cutting];";
        public const string Sql_V_Cut_Repository_GetCuttingsInType = @"SELECT * FROM [dbo].[V_Cutting] WHERE [Type]=@Type;";
        public const string Sql_V_Cut_Repository_GetCuteds = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Cuted'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Values AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM Values ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Cut_Repository_GetCutedsInType = @"
        DECLARE @tpDate DATETIME,
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Cuted'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        		
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [Type] = '+ CAST(@Type AS NVARCHAR);
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH Values AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM Values ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 能耗数据表
        /// </summary>
        public const string Sql_V_Elec_Repository_GetValues1 = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Elec'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [Id]=''' + @Id + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisElec AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisElec ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Elec_Repository_GetValues2 = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Elec'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [FormulaType]=' + CAST(@FormulaType AS NVARCHAR) + N' AND [Id]=''' + @Id + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisElec AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisElec ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Elec_Repository_GetValues3 = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Elec'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [FormulaType]=' + CAST(@FormulaType AS NVARCHAR);
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisElec AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisElec ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Elec_Repository_GetValues4 = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Elec'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [Type]=' + CAST(@Type AS NVARCHAR);
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisElec AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisElec ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Elec_Repository_GetValues5 = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Elec'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisElec AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisElec ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        
        /// <summary>
        /// 历史性能数据表
        /// </summary>
        public const string Sql_V_HMeasure_Repository_GetMeasuresInArea = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [AreaId] = ''' + @AreaId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_HMeasure_Repository_GetMeasuresInStation = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [StationId] = ''' + @StationId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_HMeasure_Repository_GetMeasuresInRoom = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [RoomId] = ''' + @RoomId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_HMeasure_Repository_GetMeasuresInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_HMeasure_Repository_GetMeasuresInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_HMeasure_Repository_GetMeasures = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_HMeasure'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisValue AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM HisValue ORDER BY [UpdateTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 历史性能数据统计表
        /// </summary>
        public const string Sql_V_Static_Repository_GetValuesInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Static'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisStatic AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisStatic ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Static_Repository_GetValuesInPoint = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Static'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisStatic AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisStatic ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Static_Repository_GetValues = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Static'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisStatic AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisStatic ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 开关电源带载率数据表
        /// </summary>
        public const string Sql_V_Load_Repository_GetLoadsInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Load'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId] = ''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisLoad AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisLoad ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_V_Load_Repository_GetLoads = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[V_Load'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisLoad AS
		    (
			    ' + @SQL + N'
		    )
		    SELECT * FROM hisLoad ORDER BY [StartTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 参数自检表
        /// </summary>
        public const string Sql_V_ParamDiff_Repository_GetDiffs = @"
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_ParamDiff{0}]') AND type in (N'U'))
        BEGIN
            SELECT * FROM [dbo].[V_ParamDiff{0}];
        END";

        /// <summary>
        /// 刷卡记录表
        /// </summary>
        public const string Sql_H_CardRecord_Repository_GetEntities = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_H_CardRecord_Repository_GetEntitiesInCard = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [CardId]=''' + @CardId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_H_CardRecord_Repository_GetEntitiesInDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [DeviceId]=''' + @DeviceId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_H_CardRecord_Repository_GetEntitiesInRoom = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [RoomId]=''' + @RoomId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_H_CardRecord_Repository_GetEntitiesInStation = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [StationId]=''' + @StationId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_H_CardRecord_Repository_GetEntitiesInArea = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[H_CardRecord'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PunchTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N''' AND [AreaId]=''' + @AreaId + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH CR AS
	        (
		        ' + @SQL + N'
	        )
	        SELECT * FROM CR ORDER BY [PunchTime] DESC;'
        END

        EXECUTE sp_executesql @SQL;";
    }
}
