using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlCommands_Rs {
        //employee repository
        public const string Sql_Employee_Repository_GetEntity = @"SELECT [Id],[EmpNo],[Name],[EngName],[UsedName],[Sex],[DeptId],[DutyId],[ICardId],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving] AS [IsLeft],[EntryTime],[RetireTime],[IsFormal],[Remarks],[CreateMan] AS [Creater],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[Enabled] FROM [dbo].[U_Empolyee] WHERE [Id] = @Id;";
        public const string Sql_Employee_Repository_GetEntityByNo = @"SELECT [Id],[EmpNo],[Name],[EngName],[UsedName],[Sex],[DeptId],[DutyId],[ICardId],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving] AS [IsLeft],[EntryTime],[RetireTime],[IsFormal],[Remarks],[CreateMan] AS [Creater],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[Enabled] FROM [dbo].[U_Empolyee] WHERE [EmpNo] = @EmpNo;";
        public const string Sql_Employee_Repository_GetEntities = @"SELECT [Id],[EmpNo],[Name],[EngName],[UsedName],[Sex],[DeptId],[DutyId],[ICardId],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving] AS [IsLeft],[EntryTime],[RetireTime],[IsFormal],[Remarks],[CreateMan] AS [Creater],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[Enabled] FROM [dbo].[U_Empolyee] ORDER BY [Name];";
        public const string Sql_Employee_Repository_GetEntitiesByDept = @"SELECT [Id],[EmpNo],[Name],[EngName],[UsedName],[Sex],[DeptId],[DutyId],[ICardId],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving] AS [IsLeft],[EntryTime],[RetireTime],[IsFormal],[Remarks],[CreateMan] AS [Creater],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[Enabled] FROM [dbo].[U_Empolyee] WHERE [DeptId] = @DeptId ORDER BY [Name];";

        //department repository
        public const string Sql_Department_Repository_GetEntity = @"SELECT [Id],[Name],[Code],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Department] WHERE [ID] = @Id;";
        public const string Sql_Department_Repository_GetEntityByCode = @"SELECT [Id],[Name],[Code],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Department] WHERE [Code] = @Code;";
        public const string Sql_Department_Repository_GetEntities = @"SELECT [Id],[Name],[Code],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Department] ORDER BY [Name];";

        //area repository
        public const string Sql_Area_Repository_GetEntity = @"SELECT [Id],[Name],[NodeLevel],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[ParentId],[Desc] AS [Comment],[Enabled] FROM [dbo].[A_Area] WHERE [Id]=@Id;";
        public const string Sql_Area_Repository_GetEntities = @"SELECT [Id],[Name],[NodeLevel],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[ParentId],[Desc] AS [Comment],[Enabled] FROM [dbo].[A_Area];";

        //station repository
        public const string Sql_Station_Repository_GetEntity = @"SELECT [Id],[Name],[StaTypeId],[Longitude],[Latitude],[Altitude],[CityElecLoadTypeId],[CityElecCap],[CityElecLoad],[Contact],[LineRadiusSize],[LineLength],[SuppPowerTypeId],[TranInfo],[TranContNo],[TranPhone],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[ParentId],[AreaId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Station] WHERE [Id]=@Id;";
        public const string Sql_Station_Repository_GetEntities = @"SELECT [Id],[Name],[StaTypeId],[Longitude],[Latitude],[Altitude],[CityElecLoadTypeId],[CityElecCap],[CityElecLoad],[Contact],[LineRadiusSize],[LineLength],[SuppPowerTypeId],[TranInfo],[TranContNo],[TranPhone],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[ParentId],[AreaId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Station];";
        public const string Sql_Station_Repository_GetEntitiesInParent = @"SELECT [Id],[Name],[StaTypeId],[Longitude],[Latitude],[Altitude],[CityElecLoadTypeId],[CityElecCap],[CityElecLoad],[Contact],[LineRadiusSize],[LineLength],[SuppPowerTypeId],[TranInfo],[TranContNo],[TranPhone],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[ParentId],[AreaId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Station] WHERE [AreaId]=@AreaId;";

        //room repository
        public const string Sql_Room_Repository_GetEntity = @"SELECT [Id],[Name],[RoomTypeId],[PropertyId],[Address],[Floor],[Length],[Width],[Heigth],[FloorLoad],[LineHeigth],[Square],[EffeSquare],[FireFighEuip],[Owner],[QueryPhone],[PowerSubMain],[TranSubMain],[EnviSubMain],[FireSubMain],[AirSubMain],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[StationId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Room] WHERE [Id]=@Id;";
        public const string Sql_Room_Repository_GetEntitiesByParent = @"SELECT [Id],[Name],[RoomTypeId],[PropertyId],[Address],[Floor],[Length],[Width],[Heigth],[FloorLoad],[LineHeigth],[Square],[EffeSquare],[FireFighEuip],[Owner],[QueryPhone],[PowerSubMain],[TranSubMain],[EnviSubMain],[FireSubMain],[AirSubMain],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[StationId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Room] WHERE [StationId]=@Parent;";
        public const string Sql_Room_Repository_GetEntities = @"SELECT [Id],[Name],[RoomTypeId],[PropertyId],[Address],[Floor],[Length],[Width],[Heigth],[FloorLoad],[LineHeigth],[Square],[EffeSquare],[FireFighEuip],[Owner],[QueryPhone],[PowerSubMain],[TranSubMain],[EnviSubMain],[FireSubMain],[AirSubMain],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[StationId],[Desc] AS [Comment],[Enabled] FROM [dbo].[S_Room];";

        //device repository
        public const string Sql_Device_Repository_GetEntity = @"SELECT [Id],[Name],[Code],[DeviceTypeId],[SubDeviceTypeId],[SysName],[SysCode],[Model],[ProdId],[BrandId],[SuppId],[SubCompId],[StartTime],[ScrapTime],[StatusId],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[RoomId],[Desc] AS [Comment],[Enabled] FROM [dbo].[D_Device] WHERE [Id]=@Id;";
        public const string Sql_Device_Repository_GetEntitiesByParent = @"SELECT [Id],[Name],[Code],[DeviceTypeId],[SubDeviceTypeId],[SysName],[SysCode],[Model],[ProdId],[BrandId],[SuppId],[SubCompId],[StartTime],[ScrapTime],[StatusId],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[RoomId],[Desc] AS [Comment],[Enabled] FROM [dbo].[D_Device] WHERE [RoomId]=@Parent;";
        public const string Sql_Device_Repository_GetEntities = @"SELECT [Id],[Name],[Code],[DeviceTypeId],[SubDeviceTypeId],[SysName],[SysCode],[Model],[ProdId],[BrandId],[SuppId],[SubCompId],[StartTime],[ScrapTime],[StatusId],[Contact],[CreateMan] AS [Creator],[CreateTime] AS [CreatedTime],[ModifyMan] AS [Modifier],[ModifyTime] AS [ModifiedTime],[RoomId],[Desc] AS [Comment],[Enabled] FROM [dbo].[D_Device];";

        //brand repository
        public const string Sql_Brand_Repository_GetEntity = @"SELECT [Id],[Name],[ProductorId],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Brand] WHERE [Id] = @Id;";
        public const string Sql_Brand_Repository_GetEntities = @"SELECT [Id],[Name],[ProductorId],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Brand] ORDER BY [Name];";

        //device status repository
        public const string Sql_DeviceStatus_Repository_GetEntity = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_DeviceStatus] WHERE [Id] = @Id;";
        public const string Sql_DeviceStatus_Repository_GetEntities = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_DeviceStatus] ORDER BY [Id];";

        //device type repository
        public const string Sql_DeviceType_Repository_GetEntity = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_DeviceType] WHERE [Id] = @Id;";
        public const string Sql_DeviceType_Repository_GetEntities = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_DeviceType] ORDER BY [Id];";

        //duty repository
        public const string Sql_Duty_Repository_GetEntity = @"SELECT [Id],[Name],[Level],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Duty] WHERE [Id] = @Id;";
        public const string Sql_Duty_Repository_GetEntities = @"SELECT [Id],[Name],[Level],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Duty] ORDER BY [Id];";

        //enum methods repository
        public const string Sql_EnumMethods_Repository_GetEntity = @"SELECT [Id],[MethNo],[Name],[DeviceTypeId],[Desc] AS [Comment] FROM [dbo].[C_EnumMethods] WHERE [Id] = @Id;";
        public const string Sql_EnumMethods_Repository_GetEntities = @"SELECT [Id],[MethNo],[Name],[DeviceTypeId],[Desc] AS [Comment] FROM [dbo].[C_EnumMethods] ORDER BY [Id];";

        //logic type repository
        public const string Sql_LogicType_Repository_GetEntity = @"SELECT [Id],[Name] FROM [dbo].[C_LogicType] WHERE [Id] = @Id;";
        public const string Sql_LogicType_Repository_GetEntities = @"SELECT [Id],[Name] FROM [dbo].[C_LogicType] ORDER BY [Id];";

        //productor repository
        public const string Sql_Productor_Repository_GetEntity = @"SELECT [Id],[Name],[EngName],[Phone],[Fax],[Address],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Productor] WHERE [Id]=@Id;";
        public const string Sql_Productor_Repository_GetEntities = @"SELECT [Id],[Name],[EngName],[Phone],[Fax],[Address],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Productor] ORDER BY [Name];";

        //room type repository
        public const string Sql_RoomType_Repository_GetEntity = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_RoomType] WHERE [Id]=@Id;";
        public const string Sql_RoomType_Repository_GetEntities = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_RoomType] ORDER BY [Id];";

        //station type repository
        public const string Sql_StationType_Repository_GetEntity = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_StationType] WHERE [Id]=@Id;";
        public const string Sql_StationType_Repository_GetEntities = @"SELECT [Id],[Name],[Desc] AS [Comment] FROM [dbo].[C_StationType] ORDER BY [Id];";

        //sub company repository
        public const string Sql_SubCompany_Repository_GetEntity = @"SELECT [Id],[Name],[linkMan] AS [Contact],[Phone],[Fax],[Address],[Level],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_SubCompany] WHERE [Id]=@Id;";
        public const string Sql_SubCompany_Repository_GetEntities = @"SELECT [Id],[Name],[linkMan] AS [Contact],[Phone],[Fax],[Address],[Level],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_SubCompany] ORDER BY [Name];";

        //sub device type repository
        public const string Sql_SubDeviceType_Repository_GetEntity = @"SELECT [Id],[Name],[DeviceTypeId],[Desc] AS [Comment] FROM [dbo].[C_SubDeviceType] WHERE [Id]=@Id;";
        public const string Sql_SubDeviceType_Repository_GetEntities = @"SELECT [Id],[Name],[DeviceTypeId],[Desc] AS [Comment] FROM [dbo].[C_SubDeviceType] ORDER BY [Id];";

        //supplier repository
        public const string Sql_Supplier_Repository_GetEntity = @"SELECT [Id],[Name],[linkMan] AS [Contact],[Phone],[Fax],[Address],[Level],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Supplier] WHERE [Id]=@Id;";
        public const string Sql_Supplier_Repository_GetEntities = @"SELECT [Id],[Name],[linkMan] AS [Contact],[Phone],[Fax],[Address],[Level],[PostalCode],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Supplier] ORDER BY [Name];";

        //unit repository
        public const string Sql_Unit_Repository_GetEntity = @"SELECT [Id],[Name],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Unit] WHERE [Id]=@Id;";
        public const string Sql_Unit_Repository_GetEntities = @"SELECT [Id],[Name],[Desc] AS [Comment],[Enabled] FROM [dbo].[C_Unit] ORDER BY [Id];";

        //user repository
        public const string Sql_User_Repository_GetEntity = @"SELECT [Id],[Uid],[Pwd],[PwdFormat],[PwdSalt],[OpLevel],[OnlineTime],[LimitTime],[CreateTime],[LastLoginTime],[LastPwdChangedTime],[FailedPwdAttemptCount],[FailedPwdTime],[IsLockedOut],[LastLockoutTime],[Remark],[EmpId],[Enabled] FROM [dbo].[U_User] WHERE [Id]=@Id;";
        public const string Sql_User_Repository_GetEntities = @"SELECT [Id],[Uid],[Pwd],[PwdFormat],[PwdSalt],[OpLevel],[OnlineTime],[LimitTime],[CreateTime],[LastLoginTime],[LastPwdChangedTime],[FailedPwdAttemptCount],[FailedPwdTime],[IsLockedOut],[LastLockoutTime],[Remark],[EmpId],[Enabled] FROM [dbo].[U_User] ORDER BY [Uid];";
    }
}
