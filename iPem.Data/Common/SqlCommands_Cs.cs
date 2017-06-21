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
        public const string Sql_A_AAlarm_Repository_GetAlarmsInArea = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [AreaId] = @AreaId AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInStation = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [StationId] = @StationId AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInRoom = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [RoomId] = @RoomId AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInDevice = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [DeviceId] = @DeviceId AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarmsInSpan = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [AlarmTime] BETWEEN @Start AND @End AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetAllAlarms = @"SELECT * FROM [dbo].[A_AAlarm] ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetPrimaryAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [PrimaryId] = @PrimaryId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetRelatedAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [RelatedId] = @RelatedId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_GetFilterAlarms = @"SELECT * FROM [dbo].[A_AAlarm] WHERE [FilterId] = @FilterId ORDER BY [AlarmTime] DESC;";
        public const string Sql_A_AAlarm_Repository_Confirm = @"UPDATE [dbo].[A_AAlarm] SET [Confirmed] = @Confirmed,[Confirmer] = @Confirmer,[ConfirmedTime] = @ConfirmedTime WHERE [Id] = @Id;";

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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [AreaId] = ''' + @AreaId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StationId] = ''' + @StationId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [RoomId] = ''' + @RoomId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PointId] = ''' + @PointId + N''' AND [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [PrimaryId] IS NULL AND [RelatedId] IS NULL AND [FilterId] IS NULL AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        /// Fsu操作日志表
        /// </summary>
        public const string Sql_H_FsuEvent_Repository_GetEvents = @"SELECT [FsuId],[EventType],[EventDesc] AS [Message],[EventTime] FROM [dbo].[H_FsuEvent] WHERE [EventTime] BETWEEN @Start AND @End;";
        public const string Sql_H_FsuEvent_Repository_GetEventsInType = @"SELECT [FsuId],[EventType],[EventDesc] AS [Message],[EventTime] FROM [dbo].[H_FsuEvent] WHERE [EventTime] BETWEEN @Start AND @End AND [EventType] = @EventType;";

        /// <summary>
        /// 资管接口站点表
        /// </summary>
        public const string Sql_H_IStation_Repository_GetStationsInType = @"SELECT [Id],[Name],[Type],[Parent],[CreatedTime] FROM [dbo].[H_IStation] WHERE [Type] = @Type;";
        public const string Sql_H_IStation_Repository_GetStationsInParent = @"SELECT [Id],[Name],[Type],[Parent],[CreatedTime] FROM [dbo].[H_IStation] WHERE [Parent] = @Parent;";
        public const string Sql_H_IStation_Repository_GetStations = @"SELECT [Id],[Name],[Type],[Parent],[CreatedTime] FROM [dbo].[H_IStation];";

        /// <summary>
        /// 资管接口设备表
        /// </summary>
        public const string Sql_H_IDevice_Repository_GetDevicesInType = @"SELECT [Id],[Name],[Type],[ParentId],[CreatedTime] FROM [dbo].[H_IDevice] WHERE [Type]=@Type;";
        public const string Sql_H_IDevice_Repository_GetDevicesInParent = @"SELECT [Id],[Name],[Type],[ParentId],[CreatedTime] FROM [dbo].[H_IDevice] WHERE [ParentId]=@ParentId;";
        public const string Sql_H_IDevice_Repository_GetDevices = @"SELECT [Id],[Name],[Type],[ParentId],[CreatedTime] FROM [dbo].[H_IDevice];";

        /// <summary>
        /// 实时性能数据表
        /// </summary>
        public const string Sql_V_AMeasure_Repository_GetMeasure = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [DeviceId] = @DeviceId AND [SignalId] = @SignalId AND [SignalNumber] = @SignalNumber;";        
        public const string Sql_V_AMeasure_Repository_GetMeasuresInArea = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [AreaId] = @AreaId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInStation = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [StationId] = @StationId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInRoom = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [RoomId] = @RoomId;";
        public const string Sql_V_AMeasure_Repository_GetMeasuresInDevice = @"SELECT * FROM [dbo].[V_AMeasure] WHERE [DeviceId] = @DeviceId;";
        public const string Sql_V_AMeasure_Repository_GetMeasures = @"SELECT * FROM [dbo].[V_AMeasure];";

        /// <summary>
        /// 蓄电池放电测值表
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
		        SELECT * FROM HisBat ORDER BY [ValueTime];'
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N''' AND [ValueTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
		        SELECT * FROM HisBat ORDER BY [ValueTime];'
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
		        SELECT * FROM HisBat ORDER BY [ValueTime];'
        END

        EXECUTE sp_executesql @SQL;";

        /// <summary>
        /// 蓄电池后备时长表
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisBatTime AS
		        (
			        ' + @SQL + N'
		        )
		        SELECT * FROM hisBatTime ORDER BY [StartTime];'
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N''' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH hisBatTime AS
		        (
			        ' + @SQL + N'
		        )
		        SELECT * FROM hisBatTime ORDER BY [StartTime];'
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
	        SET @SQL = N';WITH hisBatTime AS
		        (
			        ' + @SQL + N'
		        )
		        SELECT * FROM hisBatTime ORDER BY [StartTime];'
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [Id]=''' + @Id + N''' AND [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [Id]=''' + @Id + N''' AND [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [FormulaType]=' + CAST(@FormulaType AS NVARCHAR) + N' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [FormulaType]=' + CAST(@FormulaType AS NVARCHAR) + N' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [Type]=' + CAST(@Type AS NVARCHAR) + N' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [AreaId] = ''' + @AreaId + N''' AND [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [StationId] = ''' + @StationId + N''' AND [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [RoomId] = ''' + @RoomId + N''' AND [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N''' AND [UpdateTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N''' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
        public const string Sql_V_Load_Repository_GetLoadsInPoint = @"
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
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ''' + @DeviceId + N''' AND [PointId] = ''' + @PointId + N''' AND  [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
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
    }
}
