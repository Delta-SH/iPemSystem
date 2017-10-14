using System;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlText class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static partial class SqlCommands_Rs {
        /// <summary>
        /// 区域信息表
        /// </summary>
        public const string Sql_A_Area_Repository_GetArea = @"SELECT A.*,V.[Name] AS [VendorName] FROM [dbo].[A_Area] A LEFT OUTER JOIN [dbo].[C_SCVendor] V ON A.[VendorID]=V.[ID] WHERE [Id] = @Id;";
        public const string Sql_A_Area_Repository_GetAreas = @"SELECT A.*,V.[Name] AS [VendorName] FROM [dbo].[A_Area] A LEFT OUTER JOIN [dbo].[C_SCVendor] V ON A.[VendorID]=V.[ID] WHERE [Enabled] = 1;";

        /// <summary>
        /// 设备品牌表
        /// </summary>
        public const string Sql_C_Brand_Repository_GetBrand = @"SELECT * FROM [dbo].[C_Brand] WHERE [Id] = @Id;";
        public const string Sql_C_Brand_Repository_GetBrands = @"SELECT * FROM [dbo].[C_Brand] WHERE [Enabled] = 1;";

        /// <summary>
        /// 部门信息表
        /// </summary>
        public const string Sql_C_Department_Repository_GetDepartmentById = @"SELECT * FROM [dbo].[C_Department] WHERE [Id] = @Id;";
        public const string Sql_C_Department_Repository_GetDepartmentByCode = @"SELECT * FROM [dbo].[C_Department] WHERE [Code] = @Code;";
        public const string Sql_C_Department_Repository_GetDepartments = @"SELECT * FROM [dbo].[C_Department] WHERE [Enabled] = 1;";

        /// <summary>
        /// 设备类型/设备子类型信息表
        /// </summary>
        public const string Sql_C_DeviceType_Repository_GetDeviceType = @"SELECT * FROM [dbo].[C_DeviceType] WHERE [Id] = @Id;";
        public const string Sql_C_DeviceType_Repository_GetDeviceTypes = @"SELECT * FROM [dbo].[C_DeviceType] ORDER BY [Id];";
        public const string Sql_C_DeviceType_Repository_GetSubDeviceType = @"SELECT * FROM [dbo].[C_SubDeviceType] WHERE [Id]=@Id;";
        public const string Sql_C_DeviceType_Repository_GetSubDeviceTypes = @"SELECT * FROM [dbo].[C_SubDeviceType] ORDER BY [Id];";

        /// <summary>
        /// 职位信息表
        /// </summary>
        public const string Sql_C_Duty_Repository_GetDuty = @"SELECT * FROM [dbo].[C_Duty] WHERE [Id] = @Id;";
        public const string Sql_C_Duty_Repository_GetDuties = @"SELECT * FROM [dbo].[C_Duty] WHERE [Enabled] = 1;";

        /// <summary>
        /// 自定义枚举表
        /// </summary>
        public const string Sql_C_EnumMethod_Repository_GetEnumById = @"SELECT * FROM [dbo].[C_EnumMethods] WHERE [Id] = @Id;";
        public const string Sql_C_EnumMethod_Repository_GetEnumsByType = @"SELECT * FROM [dbo].[C_EnumMethods] WHERE [TypeId] = @TypeId AND [Desc] = @Comment ORDER BY [Index];";
        public const string Sql_C_EnumMethod_Repository_GetEnums = @"SELECT * FROM [dbo].[C_EnumMethods] ORDER BY [TypeId],[Desc],[Index];";

        /// <summary>
        /// SC组信息表
        /// </summary>
        public const string Sql_C_Group_Repository_GetGroup = @"SELECT * FROM [dbo].[C_Group] WHERE [Id] = @Id;";
        public const string Sql_C_Group_Repository_GetGroups = @"SELECT * FROM [dbo].[C_Group];";

        /// <summary>
        /// 逻辑分类/逻辑子类信息表
        /// </summary>
        public const string Sql_C_LogicType_Repository_GetLogicType = @"SELECT * FROM [dbo].[C_LogicType] WHERE [Id] = @Id;";
        public const string Sql_C_LogicType_Repository_GetSubLogicType = @"SELECT * FROM [dbo].[C_SubLogicType] WHERE [Id] = @Id;";
        public const string Sql_C_LogicType_Repository_GetLogicTypes = @"SELECT * FROM [dbo].[C_LogicType] ORDER BY [Id];";
        public const string Sql_C_LogicType_Repository_GetSubLogicTypes = @"SELECT * FROM [dbo].[C_SubLogicType] ORDER BY [Id];";

        /// <summary>
        /// 生产厂家表
        /// </summary>
        public const string Sql_C_Productor_Repository_GetProductor = @"SELECT * FROM [dbo].[C_Productor] WHERE [Id]=@Id;";
        public const string Sql_C_Productor_Repository_GetProductors = @"SELECT * FROM [dbo].[C_Productor] WHERE [Enabled]=1;";

        /// <summary>
        /// 机房类型表
        /// </summary>
        public const string Sql_C_RoomType_Repository_GetRoomType = @"SELECT * FROM [dbo].[C_RoomType] WHERE [Id]=@Id;";
        public const string Sql_C_RoomType_Repository_GetRoomTypes = @"SELECT * FROM [dbo].[C_RoomType] ORDER BY [Id];";

        /// <summary>
        /// SC厂家表
        /// </summary>
        public const string Sql_C_SCVendor_Repository_GetVendor = @"SELECT * FROM [dbo].[C_SCVendor] WHERE [Id]=@Id;";
        public const string Sql_C_SCVendor_Repository_GetVendors = @"SELECT * FROM [dbo].[C_SCVendor] ORDER BY [Id];";

        /// <summary>
        /// 站点类型表
        /// </summary>
        public const string Sql_C_StationType_Repository_GetStationType = @"SELECT * FROM [dbo].[C_StationType] WHERE [Id]=@Id;";
        public const string Sql_C_StationType_Repository_GetStationTypes = @"SELECT * FROM [dbo].[C_StationType] ORDER BY [Id];";

        /// <summary>
        /// 代维公司表
        /// </summary>
        public const string Sql_C_SubCompany_Repository_GetSubCompany = @"SELECT * FROM [dbo].[C_SubCompany] WHERE [Id]=@Id;";
        public const string Sql_C_SubCompany_Repository_GetSubCompanies = @"SELECT * FROM [dbo].[C_SubCompany] WHERE [Enabled] = 1;";

        /// <summary>
        /// 供应商表
        /// </summary>
        public const string Sql_C_Supplier_Repository_GetSupplier = @"SELECT * FROM [dbo].[C_Supplier] WHERE [Id]=@Id;";
        public const string Sql_C_Supplier_Repository_GetSuppliers = @"SELECT * FROM [dbo].[C_Supplier] WHERE [Enabled] = 1;";

        /// <summary>
        /// 计量单位表
        /// </summary>
        public const string Sql_C_Unit_Repository_GetUnit = @"SELECT * FROM [dbo].[C_Unit] WHERE [Id]=@Id;";
        public const string Sql_C_Unit_Repository_GetUnits = @"SELECT * FROM [dbo].[C_Unit] WHERE [Enabled] = 1;";

        /// <summary>
        /// 设备信息表
        /// </summary>
        public const string Sql_D_Device_Repository_GetDevice = @"
        SELECT D.[Id],D.[Code],D.[Name],D.[SysName],D.[SysCode],DT.[Id] AS [DeviceTypeId],DT.[Name] AS [DeviceTypeName],D.[SubDeviceTypeId],SD.[Name] AS [SubDeviceTypeName],D.[SubLogicTypeId],LT.[Name] AS [SubLogicTypeName],D.[VendorID],V.[Name] AS [VendorName],D.[Model],D.[ProdId],D.[BrandId],D.[SuppId],D.[SubCompId],D.[StartTime],D.[ScrapTime],D.[StatusId],D.[Version],D.[Contact],S.[AreaId],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[Id] AS [RoomId],R.[Name] AS [RoomName],D.[FsuId],D.[ProtocolId],D.[Desc] AS [Comment],D.[Index],D.[Enabled] FROM [dbo].[D_Device] D
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[C_SubDeviceType] SD ON D.[SubDeviceTypeId] = SD.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON SD.[DeviceTypeId] = DT.[Id]
        LEFT OUTER JOIN [dbo].[C_SubLogicType] LT ON D.[SubLogicTypeId] = LT.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON D.[VendorID] = V.[ID]
        WHERE D.[Id] = @Id;";
        public const string Sql_D_Device_Repository_GetDevicesInRoom = @"
        SELECT D.[Id],D.[Code],D.[Name],D.[SysName],D.[SysCode],DT.[Id] AS [DeviceTypeId],DT.[Name] AS [DeviceTypeName],D.[SubDeviceTypeId],SD.[Name] AS [SubDeviceTypeName],D.[SubLogicTypeId],LT.[Name] AS [SubLogicTypeName],D.[VendorID],V.[Name] AS [VendorName],D.[Model],D.[ProdId],D.[BrandId],D.[SuppId],D.[SubCompId],D.[StartTime],D.[ScrapTime],D.[StatusId],D.[Version],D.[Contact],S.[AreaId],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[Id] AS [RoomId],R.[Name] AS [RoomName],D.[FsuId],D.[ProtocolId],D.[Desc] AS [Comment],D.[Index],D.[Enabled] FROM [dbo].[D_Device] D
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[C_SubDeviceType] SD ON D.[SubDeviceTypeId] = SD.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON SD.[DeviceTypeId] = DT.[Id]
        LEFT OUTER JOIN [dbo].[C_SubLogicType] LT ON D.[SubLogicTypeId] = LT.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON D.[VendorID] = V.[ID]
        WHERE D.[RoomId]=@RoomId AND D.[Enabled] = 1 ORDER BY D.[Index];";
        public const string Sql_D_Device_Repository_GetDevicesInFsu = @"
        SELECT D.[Id],D.[Code],D.[Name],D.[SysName],D.[SysCode],DT.[Id] AS [DeviceTypeId],DT.[Name] AS [DeviceTypeName],D.[SubDeviceTypeId],SD.[Name] AS [SubDeviceTypeName],D.[SubLogicTypeId],LT.[Name] AS [SubLogicTypeName],D.[VendorID],V.[Name] AS [VendorName],D.[Model],D.[ProdId],D.[BrandId],D.[SuppId],D.[SubCompId],D.[StartTime],D.[ScrapTime],D.[StatusId],D.[Version],D.[Contact],S.[AreaId],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[Id] AS [RoomId],R.[Name] AS [RoomName],D.[FsuId],D.[ProtocolId],D.[Desc] AS [Comment],D.[Index],D.[Enabled] FROM [dbo].[D_Device] D
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[C_SubDeviceType] SD ON D.[SubDeviceTypeId] = SD.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON SD.[DeviceTypeId] = DT.[Id]
        LEFT OUTER JOIN [dbo].[C_SubLogicType] LT ON D.[SubLogicTypeId] = LT.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON D.[VendorID] = V.[ID]
        WHERE D.[FsuId]=@FsuId AND D.[Enabled] = 1 ORDER BY D.[Index];";
        public const string Sql_D_Device_Repository_GetDevices = @"
        SELECT D.[Id],D.[Code],D.[Name],D.[SysName],D.[SysCode],DT.[Id] AS [DeviceTypeId],DT.[Name] AS [DeviceTypeName],D.[SubDeviceTypeId],SD.[Name] AS [SubDeviceTypeName],D.[SubLogicTypeId],LT.[Name] AS [SubLogicTypeName],D.[VendorID],V.[Name] AS [VendorName],D.[Model],D.[ProdId],D.[BrandId],D.[SuppId],D.[SubCompId],D.[StartTime],D.[ScrapTime],D.[StatusId],D.[Version],D.[Contact],S.[AreaId],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[Id] AS [RoomId],R.[Name] AS [RoomName],D.[FsuId],D.[ProtocolId],D.[Desc] AS [Comment],D.[Index],D.[Enabled] FROM [dbo].[D_Device] D
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[C_SubDeviceType] SD ON D.[SubDeviceTypeId] = SD.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON SD.[DeviceTypeId] = DT.[Id]
        LEFT OUTER JOIN [dbo].[C_SubLogicType] LT ON D.[SubLogicTypeId] = LT.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON D.[VendorID] = V.[ID]
        WHERE D.[Enabled] = 1 ORDER BY D.[Index];";

        /// <summary>
        /// FSU信息表
        /// </summary>
        public const string Sql_D_Fsu_Repository_GetFsu = @"
        SELECT F.[DeviceID] AS [Id],F.[Code],F.[Name],A.[ID] AS [AreaId],A.[Name] AS [AreaName],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[ID] AS [RoomId],R.[Name] AS [RoomName],F.[Desc] AS [Comment],F.[VendorId],V.[Name] AS [VendorName],F.[IP],F.[Port],F.[Uid],F.[Pwd],F.[FtpUid],F.[FtpPwd],F.[FtpFilePath],F.[FtpAuthority]
        FROM [dbo].[D_FSU] F LEFT OUTER JOIN [dbo].[C_SCVendor] V ON F.[VendorId] = V.[Id]
        INNER JOIN [dbo].[S_Room] R ON F.[RoomId] = R.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        INNER JOIN [dbo].[A_Area] A ON S.[AreaID] = A.[ID]
        WHERE F.[DeviceId] = @Id;";
        public const string Sql_D_Fsu_Repository_GetFsusInRoom = @"
        SELECT F.[DeviceID] AS [Id],F.[Code],F.[Name],A.[ID] AS [AreaId],A.[Name] AS [AreaName],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[ID] AS [RoomId],R.[Name] AS [RoomName],F.[Desc] AS [Comment],F.[VendorId],V.[Name] AS [VendorName],F.[IP],F.[Port],F.[Uid],F.[Pwd],F.[FtpUid],F.[FtpPwd],F.[FtpFilePath],F.[FtpAuthority]
        FROM [dbo].[D_FSU] F LEFT OUTER JOIN [dbo].[C_SCVendor] V ON F.[VendorId] = V.[Id]
        INNER JOIN [dbo].[S_Room] R ON F.[RoomId] = R.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        INNER JOIN [dbo].[A_Area] A ON S.[AreaID] = A.[ID]
        WHERE R.[Id] = @RoomId;";
        public const string Sql_D_Fsu_Repository_GetFsus = @"
        SELECT F.[DeviceID] AS [Id],F.[Code],F.[Name],A.[ID] AS [AreaId],A.[Name] AS [AreaName],S.[Id] AS [StationId],S.[Name] AS [StationName],S.[StaTypeId],R.[ID] AS [RoomId],R.[Name] AS [RoomName],F.[Desc] AS [Comment],F.[VendorId],V.[Name] AS [VendorName],F.[IP],F.[Port],F.[Uid],F.[Pwd],F.[FtpUid],F.[FtpPwd],F.[FtpFilePath],F.[FtpAuthority]
        FROM [dbo].[D_FSU] F LEFT OUTER JOIN [dbo].[C_SCVendor] V ON F.[VendorId] = V.[Id]
        INNER JOIN [dbo].[S_Room] R ON F.[RoomId] = R.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        INNER JOIN [dbo].[A_Area] A ON S.[AreaID] = A.[ID];";
        public const string Sql_D_Fsu_Repository_GetExtFsu = @"
        SELECT [DeviceId] AS [Id],[IP],[Port],[ChangeTime],[LastTime],[Status],[Desc] AS [Comment],[GroupId] FROM [dbo].[D_Fsu] WHERE [DeviceId]=@Id;";
        public const string Sql_D_Fsu_Repository_GetExtFsus = @"
        SELECT [DeviceId] AS [Id],[IP],[Port],[ChangeTime],[LastTime],[Status],[Desc] AS [Comment],[GroupId] FROM [dbo].[D_Fsu];";
        public const string Sql_D_Fsu_Repository_UpdateFsus = @"UPDATE [dbo].[D_FSU] SET [RoomID] = D.[RoomID] FROM [dbo].[D_FSU] F INNER JOIN [dbo].[D_Device] D ON F.[DeviceID] = D.[ID];";

        /// <summary>
        /// 信号重定义表
        /// </summary>
        public const string Sql_D_RedefinePoint_Repository_GetRedefinePoint = @"SELECT * FROM [dbo].[D_RedefinePoint] WHERE [DeviceId] = @DeviceId AND [PointId] = @PointId;";
        public const string Sql_D_RedefinePoint_Repository_GetRedefinePointsInDevice = @"SELECT * FROM [dbo].[D_RedefinePoint] WHERE [DeviceId] = @DeviceId;";
        public const string Sql_D_RedefinePoint_Repository_GetRedefinePoints = @"SELECT * FROM [dbo].[D_RedefinePoint];";

        /// <summary>
        /// 配置同步表
        /// </summary>
        public const string Sql_H_Note_Repository_GetEntities = @"SELECT * FROM [dbo].[H_Note] WHERE [SysType] = 2;";
        public const string Sql_H_Note_Repository_Insert = @"
        IF NOT EXISTS (SELECT 1 FROM [dbo].[H_Note] WHERE [SysType]=@SysType AND CONVERT(VARCHAR(MAX),[Name]) = @Name)
        BEGIN
	        INSERT INTO [dbo].[H_Note]([SysType],[GroupID],[Name],[DtType],[OpType],[Time],[Desc]) VALUES(@SysType,@GroupID,@Name,@DtType,@OpType,@Time,@Desc);	
        END";
        public const string Sql_H_Note_Repository_Delete = @"DELETE FROM [dbo].[H_Note] WHERE [ID]=@Id;";
        public const string Sql_H_Note_Repository_Clear = @"DELETE FROM [dbo].[H_Note] WHERE [SysType] = 2;";

        /// <summary>
        /// 标准信号表
        /// </summary>
        public const string Sql_P_Point_Repository_GetPointsInDevice = @"
        SELECT P.[Id],P.[Code],P.[Name],P.[Type],P.[UnitState],P.[Number],P.[AlarmId],P.[DeviceTypeId],DT.[Name] AS [DeviceTypeName],P.[LogicTypeId],LT.[Name] AS [LogicTypeName],P.[DeviceEffect],P.[BusiEffect],P.[Comment],P.[Interpret],S.[Extend] AS [ExtSet],P.[Enabled] FROM [dbo].[D_Signal] S 
        INNER JOIN [dbo].[P_Point] P ON S.[PointID]=P.[ID]
        INNER JOIN [dbo].[C_LogicType] LT ON P.[LogicTypeId] = LT.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON P.[DeviceTypeId] = DT.[Id]
        WHERE S.[DeviceID] = @DeviceId AND P.[Enabled] = 1;";
        public const string Sql_P_Point_Repository_GetPointsInProtocol = @"
        SELECT P.[Id],P.[Code],P.[Name],P.[Type],P.[UnitState],P.[Number],P.[AlarmId],P.[DeviceTypeId],DT.[Name] AS [DeviceTypeName],P.[LogicTypeId],LT.[Name] AS [LogicTypeName],P.[DeviceEffect],P.[BusiEffect],P.[Comment],P.[Interpret],P.[Extend1] AS [ExtSet],P.[Enabled] FROM [dbo].[P_Point] P 
        INNER JOIN [dbo].[P_PointsInProtocol] PP ON PP.[PointId] = P.[Id] 
        INNER JOIN [dbo].[C_LogicType] LT ON P.[LogicTypeId] = LT.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON P.[DeviceTypeId] = DT.[Id]
        WHERE PP.[ProtocolId] = @ProtocolId AND P.[Enabled] = 1;";
        public const string Sql_P_Point_Repository_GetPoints = @"
        SELECT P.[Id],P.[Code],P.[Name],P.[Type],P.[UnitState],P.[Number],P.[AlarmId],P.[DeviceTypeId],DT.[Name] AS [DeviceTypeName],P.[LogicTypeId],LT.[Name] AS [LogicTypeName],P.[DeviceEffect],P.[BusiEffect],P.[Comment],P.[Interpret],P.[Extend1] AS [ExtSet],P.[Enabled] FROM [dbo].[P_Point] P 
        INNER JOIN [dbo].[C_LogicType] LT ON P.[LogicTypeId] = LT.[Id]
        INNER JOIN [dbo].[C_DeviceType] DT ON P.[DeviceTypeId] = DT.[Id]
        WHERE P.[Enabled] = 1;";
        public const string Sql_P_Point_Repository_GetSubPoint = @"
        SELECT PS.[PointId],PS.[StaTypeId],ST.[Name] AS [StaTypeName],PS.[AlarmLevel],PS.[AlarmLimit],PS.[AlarmReturnDiff],PS.[AlarmDelay],PS.[AlarmRecoveryDelay],PS.[TriggerTypeId],PS.[SavedPeriod],PS.[AbsoluteThreshold],PS.[PerThreshold],PS.[StaticPeriod],PS.[StorageInterval] FROM [dbo].[P_SubPoint] PS 
        INNER JOIN [dbo].[C_StationType] ST ON PS.[StaTypeId] = ST.[Id]
        WHERE PS.[PointId]=@PointId AND PS.[StaTypeId]=@StaTypeId;";
        public const string Sql_P_Point_Repository_GetSubPointsInPoint = @"
        SELECT PS.[PointId],PS.[StaTypeId],ST.[Name] AS [StaTypeName],PS.[AlarmLevel],PS.[AlarmLimit],PS.[AlarmReturnDiff],PS.[AlarmDelay],PS.[AlarmRecoveryDelay],PS.[TriggerTypeId],PS.[SavedPeriod],PS.[AbsoluteThreshold],PS.[PerThreshold],PS.[StaticPeriod],PS.[StorageInterval] FROM [dbo].[P_SubPoint] PS 
        INNER JOIN [dbo].[C_StationType] ST ON PS.[StaTypeId] = ST.[Id]
        WHERE PS.[PointId]=@PointId;";
        public const string Sql_P_Point_Repository_GetSubPoints = @"
        SELECT PS.[PointId],PS.[StaTypeId],ST.[Name] AS [StaTypeName],PS.[AlarmLevel],PS.[AlarmLimit],PS.[AlarmReturnDiff],PS.[AlarmDelay],PS.[AlarmRecoveryDelay],PS.[TriggerTypeId],PS.[SavedPeriod],PS.[AbsoluteThreshold],PS.[PerThreshold],PS.[StaticPeriod],PS.[StorageInterval] FROM [dbo].[P_SubPoint] PS 
        INNER JOIN [dbo].[C_StationType] ST ON PS.[StaTypeId] = ST.[Id];";

        /// <summary>
        /// 设备模版表
        /// </summary>
        public const string Sql_P_Protocol_Repository_GetProtocols = @"
        SELECT PP.[Id],PP.[Name],DT.[Id] AS [DeviceTypeId],DT.[Name] AS [DeviceTypeName],PP.[SubDeviceTypeId],SD.[Name] AS [SubDeviceTypeName],PP.[Desc] AS [Comment],PP.[Enabled] FROM [dbo].[P_Protocol] PP
        INNER JOIN [dbo].[C_SubDeviceType] SD ON PP.[SubDeviceTypeId] = SD.[Id] 
        INNER JOIN [dbo].[C_DeviceType] DT ON SD.[DeviceTypeId] = DT.[Id]
        WHERE PP.[Enabled] = 1;";

        /// <summary>
        /// 机房信息表
        /// </summary>
        public const string Sql_S_Room_Repository_GetRoom = @"
        SELECT R.[Id],R.[Code],R.[Name],R.[RoomTypeId],RT.[Name] AS [RoomTypeName],R.[VendorId],V.[Name] AS [VendorName],R.[Floor],R.[PropertyId],R.[Address],R.[Length],R.[Width],R.[Heigth],R.[FloorLoad],R.[LineHeigth],R.[Square],R.[EffeSquare],R.[FireFighEuip],R.[Owner],R.[QueryPhone],R.[PowerSubMain],R.[TranSubMain],R.[EnviSubMain],R.[FireSubMain],R.[AirSubMain],R.[Contact],S.[AreaId],R.[StationId],S.[Name] AS [StationName],R.[Desc] AS [Comment],R.[Enabled] FROM [dbo].[S_Room] R
        INNER JOIN [dbo].[C_RoomType] RT ON R.[RoomTypeId] = RT.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON R.[VendorID]=V.[ID]
        WHERE R.[Id]=@Id;";
        public const string Sql_S_Room_Repository_GetRoomsInStation = @"
        SELECT R.[Id],R.[Code],R.[Name],R.[RoomTypeId],RT.[Name] AS [RoomTypeName],R.[VendorId],V.[Name] AS [VendorName],R.[Floor],R.[PropertyId],R.[Address],R.[Length],R.[Width],R.[Heigth],R.[FloorLoad],R.[LineHeigth],R.[Square],R.[EffeSquare],R.[FireFighEuip],R.[Owner],R.[QueryPhone],R.[PowerSubMain],R.[TranSubMain],R.[EnviSubMain],R.[FireSubMain],R.[AirSubMain],R.[Contact],S.[AreaId],R.[StationId],S.[Name] AS [StationName],R.[Desc] AS [Comment],R.[Enabled] FROM [dbo].[S_Room] R
        INNER JOIN [dbo].[C_RoomType] RT ON R.[RoomTypeId] = RT.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON R.[VendorID]=V.[ID]
        WHERE R.[StationId]=@StationId AND R.[Enabled] = 1;";
        public const string Sql_S_Room_Repository_GetRooms = @"
        SELECT R.[Id],R.[Code],R.[Name],R.[RoomTypeId],RT.[Name] AS [RoomTypeName],R.[VendorId],V.[Name] AS [VendorName],R.[Floor],R.[PropertyId],R.[Address],R.[Length],R.[Width],R.[Heigth],R.[FloorLoad],R.[LineHeigth],R.[Square],R.[EffeSquare],R.[FireFighEuip],R.[Owner],R.[QueryPhone],R.[PowerSubMain],R.[TranSubMain],R.[EnviSubMain],R.[FireSubMain],R.[AirSubMain],R.[Contact],S.[AreaId],R.[StationId],S.[Name] AS [StationName],R.[Desc] AS [Comment],R.[Enabled] FROM [dbo].[S_Room] R
        INNER JOIN [dbo].[C_RoomType] RT ON R.[RoomTypeId] = RT.[Id]
        INNER JOIN [dbo].[S_Station] S ON R.[StationId] = S.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON R.[VendorID]=V.[ID]
        WHERE R.[Enabled] = 1;";

        /// <summary>
        /// 站点信息表
        /// </summary>
        public const string Sql_S_Station_Repository_GetStation = @"
        SELECT S.[Id],S.[Code],S.[Name],S.[StaTypeId],ST.[Name] AS [StaTypeName],S.[VendorId],V.[Name] AS [VendorName],S.[Longitude],S.[Latitude],S.[Altitude],S.[CityElecLoadTypeId],S.[CityElectNumber],S.[CityElecCap],S.[CityElecLoad],S.[Contact],S.[LineRadiusSize],S.[LineLength],S.[SuppPowerTypeId],S.[TranInfo],S.[TranContNo],S.[TranPhone],S.[AreaId],S.[Desc] AS [Comment],S.[Enabled] FROM [dbo].[S_Station] S 
        INNER JOIN [dbo].[C_StationType] ST ON S.[StaTypeId] = ST.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON S.[VendorID]=V.[ID]
        WHERE S.[Id]=@Id;";
        public const string Sql_S_Station_Repository_GetStationsInArea = @"
        SELECT S.[Id],S.[Code],S.[Name],S.[StaTypeId],ST.[Name] AS [StaTypeName],S.[VendorId],V.[Name] AS [VendorName],S.[Longitude],S.[Latitude],S.[Altitude],S.[CityElecLoadTypeId],S.[CityElectNumber],S.[CityElecCap],S.[CityElecLoad],S.[Contact],S.[LineRadiusSize],S.[LineLength],S.[SuppPowerTypeId],S.[TranInfo],S.[TranContNo],S.[TranPhone],S.[AreaId],S.[Desc] AS [Comment],S.[Enabled] FROM [dbo].[S_Station] S 
        INNER JOIN [dbo].[C_StationType] ST ON S.[StaTypeId] = ST.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON S.[VendorID]=V.[ID]
        WHERE S.[AreaId]=@AreaId AND S.[Enabled] = 1;";
        public const string Sql_S_Station_Repository_GetStations = @"
        SELECT S.[Id],S.[Code],S.[Name],S.[StaTypeId],ST.[Name] AS [StaTypeName],S.[VendorId],V.[Name] AS [VendorName],S.[Longitude],S.[Latitude],S.[Altitude],S.[CityElecLoadTypeId],S.[CityElectNumber],S.[CityElecCap],S.[CityElecLoad],S.[Contact],S.[LineRadiusSize],S.[LineLength],S.[SuppPowerTypeId],S.[TranInfo],S.[TranContNo],S.[TranPhone],S.[AreaId],S.[Desc] AS [Comment],S.[Enabled] FROM [dbo].[S_Station] S 
        INNER JOIN [dbo].[C_StationType] ST ON S.[StaTypeId] = ST.[Id]
        LEFT OUTER JOIN [dbo].[C_SCVendor] V ON S.[VendorID]=V.[ID]
        WHERE S.[Enabled] = 1;";

        /// <summary>
        /// 员工信息表
        /// </summary>
        public const string Sql_U_Employee_Repository_GetEmployeeById = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[EmployeeID]=@Id AND E.[TypeID] = @Type
        )
        SELECT E.*,D.[Name] AS [DeptName],U.[Name] AS [DutyName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_Employee] E
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN [dbo].[C_Duty] U ON E.[DutyID]=U.[ID]
        LEFT OUTER JOIN Cards C ON E.[ID] = C.[EmployeeID]
        WHERE E.[Id] = @Id;";
        public const string Sql_U_Employee_Repository_GetEmployeeByCode = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT E.*,D.[Name] AS [DeptName],U.[Name] AS [DutyName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_Employee] E
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN [dbo].[C_Duty] U ON E.[DutyID]=U.[ID]
        LEFT OUTER JOIN Cards C ON E.[ID] = C.[EmployeeID]
        WHERE E.[EmpNo] = @Code;";
        public const string Sql_U_Employee_Repository_GetEmployeesByDept = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT E.*,D.[Name] AS [DeptName],U.[Name] AS [DutyName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_Employee] E
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN [dbo].[C_Duty] U ON E.[DutyID]=U.[ID]
        LEFT OUTER JOIN Cards C ON E.[ID] = C.[EmployeeID]
        WHERE E.[DeptId] = @DeptId;";
        public const string Sql_U_Employee_Repository_GetEmployees = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT E.*,D.[Name] AS [DeptName],U.[Name] AS [DutyName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_Employee] E
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN [dbo].[C_Duty] U ON E.[DutyID]=U.[ID]
        LEFT OUTER JOIN Cards C ON E.[ID] = C.[EmployeeID];";

        /// <summary>
        /// 外协人员信息表
        /// </summary>
        public const string Sql_U_Employee_Repository_GetOutEmployeeById = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[EmployeeID]=@Id AND E.[TypeID] = @Type
        )
        SELECT OE.*,E.[Name] AS [EmpName],E.[DeptID],D.[Name] AS [DeptName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_OutEmployee] OE
        LEFT OUTER JOIN [dbo].[U_Employee] E ON OE.[EmpID]=E.[ID]
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN Cards C ON OE.[ID] = C.[EmployeeID]
        WHERE OE.[Id] = @Id;";
        public const string Sql_U_Employee_Repository_GetOutEmployeesByEmp = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT OE.*,E.[Name] AS [EmpName],E.[DeptID],D.[Name] AS [DeptName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_OutEmployee] OE
        LEFT OUTER JOIN [dbo].[U_Employee] E ON OE.[EmpID]=E.[ID]
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN Cards C ON OE.[ID] = C.[EmployeeID]
        WHERE OE.[EmpId] = @EmpId;";
        public const string Sql_U_Employee_Repository_GetOutEmployeesByDept = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT OE.*,E.[Name] AS [EmpName],E.[DeptID],D.[Name] AS [DeptName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_OutEmployee] OE
        INNER JOIN [dbo].[U_Employee] E ON OE.[EmpID]=E.[ID]
        INNER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN Cards C ON OE.[ID] = C.[EmployeeID]
        WHERE E.[DeptID] = @DeptId;";
        public const string Sql_U_Employee_Repository_GetOutEmployees = @"
        ;WITH Cards AS (
	        SELECT E.[EmployeeID],C.[Code],C.[HexCode] FROM [dbo].[M_Card] C 
	        INNER JOIN [dbo].[M_CardsInEmployee] E ON C.[ID]=E.[CardID]
	        WHERE E.[TypeID] = @Type
        )
        SELECT OE.*,E.[Name] AS [EmpName],E.[DeptID],D.[Name] AS [DeptName],C.[Code] AS [CardId],C.[HexCode] AS [CardHex] FROM [dbo].[U_OutEmployee] OE
        LEFT OUTER JOIN [dbo].[U_Employee] E ON OE.[EmpID]=E.[ID]
        LEFT OUTER JOIN [dbo].[C_Department] D ON E.[DeptID]=D.[ID]
        LEFT OUTER JOIN Cards C ON OE.[ID] = C.[EmployeeID];";

        /// <summary>
        /// 脚本升级表
        /// </summary>
        public const string Sql_H_DBScript_Repository_GetEntities = @"SELECT * FROM [dbo].[H_DBScript] ORDER BY [ID];";
        public const string Sql_H_DBScript_Repository_Insert = @"INSERT INTO [dbo].[H_DBScript]([ID],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,@Name,@Creator,@CreatedTime,@Executor,@ExecutedTime,@Comment);";
        public const string Sql_H_DBScript_Repository_Update = @"UPDATE [dbo].[H_DBScript] SET [ExecuteUser] = @Executor WHERE [ID] = @Id;";
        public const string Sql_H_DBScript_Repository_Delete = @"DELETE FROM [dbo].[H_DBScript] WHERE [ID] = @Id;";

        /// <summary>
        /// 摄像机信息表
        /// </summary>
        public const string Sql_V_Camera_Repository_GetEntity = @"
        SELECT C.*,D.[Name] AS [DeviceName],R.[ID] AS [RoomId],R.[Name] AS [RoomName],S.[ID] AS [StationId],S.[Name] AS [StationName],A.[ID] AS [AreaId],A.[Name] AS [AreaName] FROM [dbo].[V_Camera] C 
        INNER JOIN [dbo].[D_Device] D ON C.[DeviceID] = D.[ID]
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[A_Area] A ON S.[AreaID] = A.[ID]
        WHERE C.[ID] = @Id;";
        public const string Sql_V_Camera_Repository_GetEntities = @"
        SELECT C.*,D.[Name] AS [DeviceName],R.[ID] AS [RoomId],R.[Name] AS [RoomName],S.[ID] AS [StationId],S.[Name] AS [StationName],A.[ID] AS [AreaId],A.[Name] AS [AreaName] FROM [dbo].[V_Camera] C 
        INNER JOIN [dbo].[D_Device] D ON C.[DeviceID] = D.[ID]
        INNER JOIN [dbo].[S_Room] R ON D.[RoomID] = R.[ID]
        INNER JOIN [dbo].[S_Station] S ON R.[StationID] = S.[ID]
        INNER JOIN [dbo].[A_Area] A ON S.[AreaID] = A.[ID]
        WHERE C.[Enabled] = 1
        ORDER BY R.[Name],D.[Index];";

        /// <summary>
        /// 摄像机通道信息表
        /// </summary>
        public const string Sql_V_Channel_Repository_GetEntitiesInCamera = @"SELECT * FROM [dbo].[V_Channel] WHERE [CameraID]=@CameraId ORDER BY [Index];";
        public const string Sql_V_Channel_Repository_GetEntities = @"SELECT * FROM [dbo].[V_Channel] ORDER BY [CameraID],[Index];";

        /// <summary>
        /// 门禁设备授权表
        /// </summary>
        public const string Sql_M_Authorization_Repository_GetEntitiesInType = @"
        SELECT A.[DriverId],C.[Code] AS [CardId],C.[HexCode] AS [CardHex],A.[DeviceId],A.[BeginTime],A.[LimitTime] AS [EndTime],A.[Pwd] AS [Password] FROM [dbo].[M_Authorization] A
        INNER JOIN [dbo].[M_CardsInEmployee] CE ON A.[CardID]=CE.[CardID]
        INNER JOIN [dbo].[M_Card] C ON A.[CardID]=C.[ID]
        WHERE CE.[TypeID]=@Type ORDER BY C.[Code];";
        public const string Sql_M_Authorization_Repository_GetEntitiesInCard = @"
        SELECT A.[DriverId],C.[Code] AS [CardId],C.[HexCode] AS [CardHex],A.[DeviceId],A.[BeginTime],A.[LimitTime] AS [EndTime],A.[Pwd] AS [Password] FROM [dbo].[M_Authorization] A
        INNER JOIN [dbo].[M_Card] C ON A.[CardID]=C.[ID]
        WHERE C.[Code]=@CardId ORDER BY A.[DeviceID];";
        public const string Sql_M_Authorization_Repository_GetEntities = @"
        SELECT A.[DriverId],C.[Code] AS [CardId],C.[HexCode] AS [CardHex],A.[DeviceId],A.[BeginTime],A.[LimitTime] AS [EndTime],A.[Pwd] AS [Password] FROM [dbo].[M_Authorization] A
        INNER JOIN [dbo].[M_Card] C ON A.[CardID]=C.[ID] ORDER BY C.[Code];";
    }
}
