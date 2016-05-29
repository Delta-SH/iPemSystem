﻿using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlCommands_Hs {
        //active alarm repository
        public const string Sql_ActAlm_Repository_GetEntitiesByDevice = @"SELECT AT.[Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM [dbo].[A_Act] AT LEFT OUTER JOIN [dbo].[A_Extend] AE ON AT.[Id] = AE.[Id] WHERE [DeviceId] = @DeviceId ORDER BY [StartTime] DESC;";
        public const string Sql_ActAlm_Repository_GetEntitiesByLevel = @"
        DECLARE @SplitTable TABLE([Data] [INT] NOT NULL);
        DECLARE @Pos INT;
        SET @Pos = CHARINDEX(@Delimiter, @Levels);
        WHILE(@Pos > 0) 
        BEGIN
            INSERT @SplitTable([Data]) VALUES(CAST(LEFT(@Levels, @Pos - 1) AS INT));
            SELECT @Levels = STUFF(@Levels, 1, @Pos, ''), @Pos = CHARINDEX(@Delimiter, @Levels);
        END
        IF(LEN(@Levels) > 0)
        BEGIN
            INSERT @SplitTable([Data]) VALUES(CAST(@Levels AS INT));
        END

        SELECT AT.[Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM [dbo].[A_Act] AT 
        INNER JOIN @SplitTable ST ON AT.[AlmLevel] = ST.[Data] 
        LEFT OUTER JOIN [dbo].[A_Extend] AE ON AT.[Id] = AE.[Id]
        ORDER BY [StartTime] DESC;";
        public const string Sql_ActAlm_Repository_GetEntitiesByTime = @"SELECT AT.[Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM [dbo].[A_Act] AT LEFT OUTER JOIN [dbo].[A_Extend] AE ON AT.[Id] = AE.[Id] WHERE [StartTime] > @Start AND [StartTime] <= @End ORDER BY [StartTime] DESC;";
        public const string Sql_ActAlm_Repository_GetEntities = @"SELECT AT.[Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM [dbo].[A_Act] AT LEFT OUTER JOIN [dbo].[A_Extend] AE ON AT.[Id] = AE.[Id] ORDER BY [StartTime] DESC;";

        //history alarm repository
        public const string Sql_HisAlm_Repository_GetEntitiesByDevice = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_Hist'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
            IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(@tbName) AND type in (N'U'))
            BEGIN
                IF(@tableCnt>0)
                BEGIN
                SET @SQL += N' 
                UNION ALL 
                ';
                END
        			
                SET @SQL += N'SELECT * FROM ' + @tbName + N' WHERE [DeviceId] = ' + @DeviceId + N' AND [StartTime] BETWEEN ''' + CONVERT(NVARCHAR,@Start,120) + N''' AND ''' + CONVERT(NVARCHAR,@End,120) + N'''';
                SET @tableCnt += 1;
            END
            SET @tpDate = DATEADD(MM,1,@tpDate);
        END

        IF(@tableCnt>0)
        BEGIN
	        SET @SQL = N';WITH HisAlm AS
		        (
			        ' + @SQL + N'
		        )
		        SELECT HA.*,[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM HisAlm HA LEFT OUTER JOIN [dbo].[A_Extend] AE ON HA.[Id] = AE.[Id];'
        END

        EXECUTE sp_executesql @SQL;";
        public const string Sql_HisAlm_Repository_GetEntities = @"
        DECLARE @tpDate DATETIME, 
                @tbName NVARCHAR(255),
                @tableCnt INT = 0,
                @SQL NVARCHAR(MAX) = N'';

        SET @tpDate = @Start;
        WHILE(DATEDIFF(MM,@tpDate,@End)>=0)
        BEGIN
            SET @tbName = N'[dbo].[A_Hist'+CONVERT(VARCHAR(6),@tpDate,112)+ N']';
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
	        SET @SQL = N';WITH HisAlm AS
		        (
			        ' + @SQL + N'
		        )
		        SELECT HA.*,[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer] FROM HisAlm HA LEFT OUTER JOIN [dbo].[A_Extend] AE ON HA.[Id] = AE.[Id];'
        END

        EXECUTE sp_executesql @SQL;";
        
        //alarm extend repository
        public const string Sql_AlmExtend_Repository_Update = @"
        UPDATE [dbo].[A_Extend] SET [ProjectId] = @ProjectId,[ConfirmedStatus] = @ConfirmedStatus,[ConfirmedTime] = @ConfirmedTime,[Confirmer] = @Confirmer WHERE [Id] = @Id;
        IF(@@ROWCOUNT = 0)
        BEGIN
	        INSERT INTO [dbo].[A_Extend]([Id],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer]) VALUES(@Id, @ProjectId, @ConfirmedStatus, @ConfirmedTime, @Confirmer);
        END";

        public const string Sql_AlmExtend_Repository_UpdateConfirm = @"
        UPDATE [dbo].[A_Extend] SET [ConfirmedStatus] = @ConfirmedStatus,[ConfirmedTime] = @ConfirmedTime,[Confirmer] = @Confirmer WHERE [Id] = @Id;
        IF(@@ROWCOUNT = 0)
        BEGIN
	        INSERT INTO [dbo].[A_Extend]([Id],[ProjectId],[ConfirmedStatus],[ConfirmedTime],[Confirmer]) VALUES(@Id, NULL, @ConfirmedStatus, @ConfirmedTime, @Confirmer);
        END";

        //active value repository
        public const string Sql_ActValue_Repository_GetEntitiesByDevice = @"SELECT [DeviceCode],[DeviceId],[PointId],[MeasuredVal],[SetupVal],[Status],[RecordTime] FROM [dbo].[V_Act] WHERE [DeviceId] = @DeviceId;";
        public const string Sql_ActValue_Repository_GetEntitiesByDevices = @"
        DECLARE @SplitTable TABLE([Id] [varchar](100) NOT NULL);
        DECLARE @Pos INT;
        SET @Pos = CHARINDEX(@Delimiter, @Devices);
        WHILE(@Pos > 0) 
        BEGIN
            INSERT @SplitTable([Id]) VALUES(LEFT(@Devices, @Pos - 1));
            SELECT @Devices = STUFF(@Devices, 1, @Pos, ''), @Pos = CHARINDEX(@Delimiter, @Devices);
        END
        IF(LEN(@Devices) > 0)
        BEGIN
            INSERT @SplitTable([Id]) VALUES(@Devices);
        END

        SELECT [DeviceCode],[DeviceId],[PointId],[MeasuredVal],[SetupVal],[Status],[RecordTime] FROM [dbo].[V_Act] VA
        INNER JOIN @SplitTable ST ON VA.[DeviceId] = ST.[Id] ORDER BY [DeviceId],[PointId];";
        public const string Sql_ActValue_Repository_GetEntities = @"SELECT [DeviceCode],[DeviceId],[PointId],[MeasuredVal],[SetupVal],[Status],[RecordTime] FROM [dbo].[V_Act];";
        public const string Sql_ActValue_Repository_Insert = @"
        UPDATE [dbo].[V_Act] SET [MeasuredVal] = @MeasuredVal,[SetupVal] = @SetupVal,[Status] = @Status,[RecordTime] = @RecordTime WHERE [DeviceId] = @DeviceId AND [PointId] = @PointId;
        IF(@@ROWCOUNT = 0)
        BEGIN
            INSERT INTO [dbo].[V_Act]([DeviceCode],[DeviceId],[PointId],[MeasuredVal],[SetupVal],[Status],[RecordTime]) VALUES(@DeviceCode,@DeviceId,@PointId,@MeasuredVal,@SetupVal,@Status,@RecordTime);
        END";
        public const string Sql_ActValue_Repository_Clear = @"TRUNCATE TABLE [dbo].[V_Act];";
    }
}