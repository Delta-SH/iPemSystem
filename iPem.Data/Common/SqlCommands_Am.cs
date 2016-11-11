using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlCommands_Am {
        //device repository
        public const string Sql_Device_Repository_GetEntitiesByType = @"SELECT [Id],[Name],[Type],[ParentId],[CreatedTime] FROM [dbo].[AM_Devices] WHERE [Type] = @Type ORDER BY [Name];";
        public const string Sql_Device_Repository_GetEntities = @"SELECT [Id],[Name],[Type],[ParentId],[CreatedTime] FROM [dbo].[AM_Devices] ORDER BY [Name];";

        //station repository
        public const string Sql_Station_Repository_GetEntitiesByType = @"SELECT [Id],[Name],[Type],[Parent],[CreatedTime] FROM [dbo].[AM_Stations] WHERE [Type]=@Type ORDER BY [Name];";
        public const string Sql_Station_Repository_GetEntities = @"SELECT [Id],[Name],[Type],[Parent],[CreatedTime] FROM [dbo].[AM_Stations] ORDER BY [Name];";
    }
}