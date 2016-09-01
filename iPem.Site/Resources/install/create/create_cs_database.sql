/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/08/18
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Act]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Act]') AND type in (N'U'))
DROP TABLE [dbo].[A_Act]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[A_Act](
	[Id] [varchar](100) NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[AlmFlag] [int] NOT NULL,
	[AlmLevel] [int] NOT NULL,
	[Frequency] [int] NOT NULL,
	[AlmDesc] [varchar](100) NOT NULL,
	[NormalDesc] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[StartValue] [float] NOT NULL,
	[EndValue] [float] NOT NULL,
	[ValueUnit] [varchar](20) NOT NULL,
	[EndType] [int] NOT NULL,
CONSTRAINT [PK_A_Act] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Hist]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Hist]') AND type in (N'U'))
DROP TABLE [dbo].[A_Hist]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[A_Hist](
	[Id] [varchar](100) NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[AlmLevel] [int] NOT NULL,
	[Frequency] [int] NOT NULL,
	[AlmDesc] [varchar](100) NOT NULL,
	[NormalDesc] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[StartValue] [float] NOT NULL,
	[EndValue] [float] NOT NULL,
	[ValueUnit] [varchar](20) NOT NULL,
	[EndType] [int] NOT NULL,
CONSTRAINT [PK_A_Hist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_DBScript]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_DBScript]') AND type in (N'U'))
DROP TABLE [dbo].[H_DBScript]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[H_DBScript](
	[Id] [int] NOT NULL,
	[Name] [varchar](200) NULL,
	[Type] [int] NULL,
	[CreateName] [varchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[ExecuteTime] [datetime] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_H_DBScript] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Area]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Area]') AND type in (N'U'))
DROP TABLE [dbo].[M_Area]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Area](
	[Id] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_M_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Device]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Device]') AND type in (N'U'))
DROP TABLE [dbo].[M_Device]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Device](
	[Id] [varchar](100) NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_M_Device] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Fsu]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Fsu]') AND type in (N'U'))
DROP TABLE [dbo].[M_Fsu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Fsu](
	[Id] [varchar](100) NOT NULL,
	[IP] [varchar](20) NULL,
	[Port] [int] NULL,
	[Status] [bit] NOT NULL,
	[ChangeTime] [datetime] NOT NULL,
	[LastTime] [datetime] NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_M_Fsu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Room]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Room]') AND type in (N'U'))
DROP TABLE [dbo].[M_Room]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Room](
	[Id] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_M_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Station]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Station]') AND type in (N'U'))
DROP TABLE [dbo].[M_Station]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Station](
	[Id] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_M_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Alm]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Alm]') AND type in (N'U'))
DROP TABLE [dbo].[V_Alm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Alm](
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
	[ValueTime] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Bat]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Bat]') AND type in (N'U'))
DROP TABLE [dbo].[V_Bat]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Bat](
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
	[ValueTime] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Hist]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Hist]') AND type in (N'U'))
DROP TABLE [dbo].[V_Hist]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Hist](
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[Value] [float] NOT NULL,
	[Threshold] [float] NOT NULL,
	[State] [int] NOT NULL,
	[Time] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Static]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Static]') AND type in (N'U'))
DROP TABLE [dbo].[V_Static]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Static](
	[DeviceCode] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[MaxTime] [datetime] NOT NULL,
	[MinTime] [datetime] NOT NULL,
	[MaxValue] [float] NOT NULL,
	[MinValue] [float] NOT NULL,
	[AvgValue] [float] NOT NULL,
	[Total] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO