/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/09/02
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Area]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Area]') AND type in (N'U'))
DROP TABLE [dbo].[A_Area]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_Area](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[NodeLevel] [int] NULL,
	[ParentId] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_A_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Brand]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Brand]') AND type in (N'U'))
DROP TABLE [dbo].[C_Brand]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Brand](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ProductorId] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Department]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Department]') AND type in (N'U'))
DROP TABLE [dbo].[C_Department]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Department](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeDesc] [varchar](512) NULL,
	[Phone] [varchar](40) NULL,
	[PostCode] [varchar](20) NULL,
	[ParentId] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_DeviceType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_DeviceType]') AND type in (N'U'))
DROP TABLE [dbo].[C_DeviceType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_DeviceType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_DeviceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Duty]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Duty]') AND type in (N'U'))
DROP TABLE [dbo].[C_Duty]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Duty](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Level] [varchar](40) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Duty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_EnumMethods]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_EnumMethods]') AND type in (N'U'))
DROP TABLE [dbo].[C_EnumMethods]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_EnumMethods](
	[Id] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeId] [int] NOT NULL,
	[Index] [int] NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_EnumMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_LogicType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_LogicType]') AND type in (N'U'))
DROP TABLE [dbo].[C_LogicType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_LogicType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[DeviceTypeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_C_LogicType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Productor]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Productor]') AND type in (N'U'))
DROP TABLE [dbo].[C_Productor]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Productor](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[EngName] [varchar](200) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](200) NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Productor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_RoomType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_RoomType]') AND type in (N'U'))
DROP TABLE [dbo].[C_RoomType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_RoomType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_RoomType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_StationType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_StationType]') AND type in (N'U'))
DROP TABLE [dbo].[C_StationType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_StationType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_StationType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubCompany]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubCompany]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubCompany]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubCompany](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[linkMan] [varchar](40) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](200) NULL,
	[Level] [int] NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_SubCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubDeviceType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubDeviceType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubDeviceType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
	[DeviceTypeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_C_SubDeviceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubLogicType]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubLogicType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubLogicType](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[LogicTypeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_C_SubLogicType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Supplier]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Supplier]') AND type in (N'U'))
DROP TABLE [dbo].[C_Supplier]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Supplier](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[linkMan] [varchar](40) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](160) NULL,
	[Level] [int] NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Unit]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Unit]') AND type in (N'U'))
DROP TABLE [dbo].[C_Unit]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Unit](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Device]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Device]') AND type in (N'U'))
DROP TABLE [dbo].[D_Device]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Device](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[SysName] [varchar](200) NOT NULL,
	[SysCode] [varchar](100) NULL,
	[SubDeviceTypeId] [varchar](100) NOT NULL,
	[Model] [varchar](20) NOT NULL,
	[ProdId] [varchar](100) NOT NULL,
	[BrandId] [varchar](100) NOT NULL,
	[SuppId] [varchar](100) NOT NULL,
	[SubCompId] [varchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[ScrapTime] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Contact] [varchar](40) NOT NULL,
	[Desc] [varchar](512) NULL,
	[ProtocolId] [varchar](100) NULL,
	[FsuId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_D_Device] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_FSU]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_FSU]') AND type in (N'U'))
DROP TABLE [dbo].[D_FSU]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_FSU](
	[DeviceId] [varchar](100) NOT NULL,
	[Uid] [varchar](20) NULL,
	[Pwd] [varchar](20) NULL,
	[FtpUid] [varchar](20) NULL,
	[FtpPwd] [varchar](20) NULL,
	[FtpFilePath] [varchar](20) NULL,
	[FtpAuthority] [int] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_D_FSU] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_Point]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Point]') AND type in (N'U'))
DROP TABLE [dbo].[P_Point]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_Point](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[UnitState] [varchar](160) NULL,
	[Number] [varchar](20) NOT NULL,
	[StationTypeId] [varchar](100) NOT NULL,
	[SubDeviceTypeId] [varchar](100) NOT NULL,
	[SubLogicTypeId] [varchar](100) NOT NULL,
	[AlarmTimeDesc] [varchar](40) NULL,
	[NormalTimeDesc] [varchar](40) NULL,
	[AlarmLevel] [int] NOT NULL,
	[TriggerTypeId] [int] NULL,
	[Comment] [varchar](512) NULL,
	[Interpret] [varchar](512) NULL,
	[AlarmLimit] [float] NULL,
	[AlarmReturnDiff] [float] NULL,
	[AlarmRecoveryDelay] [int] NULL,
	[AlarmDelay] [int] NULL,
	[SavedPeriod] [int] NULL,
	[AbsoluteThreshold] [float] NULL,
	[PerThreshold] [float] NULL,
	[StaticPeriod] [int] NULL,
	[Extend1] [varchar](512) NULL,
	[Extend2] [varchar](512) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_P_Point] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_PointsInProtocol]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]') AND type in (N'U'))
DROP TABLE [dbo].[P_PointsInProtocol]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_PointsInProtocol](
	[ProtocolId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_P_PointsInProtocol] PRIMARY KEY CLUSTERED 
(
	[ProtocolId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_Protocol]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Protocol]') AND type in (N'U'))
DROP TABLE [dbo].[P_Protocol]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_Protocol](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[SubDevTypeId] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_P_Protocol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[S_Room]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[S_Room]') AND type in (N'U'))
DROP TABLE [dbo].[S_Room]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[S_Room](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[RoomTypeId] [varchar](100) NOT NULL,
	[Floor] [int] NULL,
	[PropertyId] [int] NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Length] [varchar](20) NULL,
	[Width] [varchar](20) NULL,
	[Heigth] [varchar](20) NULL,
	[FloorLoad] [varchar](20) NULL,
	[LineHeigth] [varchar](20) NULL,
	[Square] [varchar](20) NULL,
	[EffeSquare] [varchar](20) NULL,
	[FireFighEuip] [varchar](200) NULL,
	[Owner] [varchar](40) NULL,
	[QueryPhone] [varchar](20) NULL,
	[PowerSubMain] [varchar](20) NULL,
	[TranSubMain] [varchar](20) NULL,
	[EnviSubMain] [varchar](20) NULL,
	[FireSubMain] [varchar](20) NULL,
	[AirSubMain] [varchar](20) NULL,
	[Contact] [varchar](40) NOT NULL,
	[Desc] [varchar](512) NULL,
	[StationId] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_S_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[S_Station]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[S_Station]') AND type in (N'U'))
DROP TABLE [dbo].[S_Station]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[S_Station](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[StaTypeId] [varchar](100) NOT NULL,
	[Longitude] [varchar](20) NULL,
	[Latitude] [varchar](20) NULL,
	[Altitude] [varchar](20) NULL,
	[CityElecLoad] [varchar](40) NOT NULL,
	[Contact] [varchar](20) NULL,
	[CityElecCap] [varchar](20) NOT NULL,
	[CityElecLoadTypeId] [int] NOT NULL,
	[LineRadiusSize] [varchar](20) NULL,
	[LineLength] [varchar](20) NULL,
	[SuppPowerTypeId] [int] NULL,
	[TranInfo] [varchar](250) NULL,
	[TranContNo] [varchar](40) NULL,
	[TranPhone] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
	[AreaId] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_S_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Employee]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Employee]') AND type in (N'U'))
DROP TABLE [dbo].[U_Employee]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Employee](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[EngName] [varchar](100) NULL,
	[UsedName] [varchar](200) NULL,
	[EmpNo] [varchar](20) NOT NULL,
	[DeptId] [varchar](100) NOT NULL,
	[DutyId] [varchar](100) NULL,
	[ICardId] [varchar](20) NULL,
	[Sex] [int] NOT NULL,
	[Birthday] [datetime] NULL,
	[Degree] [int] NULL,
	[Marriage] [int] NULL,
	[Nation] [varchar](40) NULL,
	[Provinces] [varchar](40) NULL,
	[Native] [varchar](80) NULL,
	[Address] [varchar](200) NULL,
	[PostalCode] [varchar](6) NULL,
	[AddrPhone] [varchar](20) NULL,
	[WorkPhone] [varchar](20) NULL,
	[MobilePhone] [varchar](20) NULL,
	[Email] [varchar](20) NULL,
	[Photo] [image] NULL,
	[Leaving] [bit] NULL,
	[EntryTime] [datetime] NULL,
	[RetireTime] [datetime] NULL,
	[IsFormal] [bit] NULL,
	[Remarks] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_U_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO