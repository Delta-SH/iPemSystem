using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlCommands_Hs {
        //A_Act
        public const string Sql_ActAlm_Repository_GetEntitiesByDevice = @"SELECT [Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType] FROM [dbo].[A_Act] WHERE [DeviceId] = @DeviceId ORDER BY [StartTime] DESC;";
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

        SELECT [Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType] 
        FROM [dbo].[A_Act] AT INNER JOIN @SplitTable ST ON AT.[AlmLevel] = ST.[Data] ORDER BY [StartTime] DESC;";
        public const string Sql_ActAlm_Repository_GetEntitiesByTime = @"SELECT [Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType] FROM [dbo].[A_Act] WHERE [StartTime] > @Start AND [StartTime] <= @End ORDER BY [StartTime] DESC;";
        public const string Sql_ActAlm_Repository_GetEntities = @"SELECT [Id],[DeviceCode],[DeviceId],[PointId],[AlmFlag],[AlmLevel],[Frequency],[AlmDesc],[NormalDesc],[StartTime],[EndTime],[StartValue],[EndValue],[ValueUnit],[EndType] FROM [dbo].[A_Act] ORDER BY [StartTime] DESC;";
    }
}
