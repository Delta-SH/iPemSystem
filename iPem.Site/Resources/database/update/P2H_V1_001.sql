--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_ACabinet]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_ACabinet]') AND type in (N'U'))
DROP TABLE [dbo].[V_ACabinet]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_ACabinet](
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[Category] [int] NOT NULL,
	[Value] [float] NOT NULL,
	[ValueTime] [datetime] NOT NULL,
	[AValue] [float] NULL,
	[AValueTime] [datetime] NULL,
	[BValue] [float] NULL,
	[BValueTime] [datetime] NULL,
	[CValue] [float] NULL,
	[CValueTime] [datetime] NULL
) ON [PRIMARY]

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除表[dbo].[V_Cuted]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cuted]') AND type in (N'U'))
DROP TABLE [dbo].[V_Cuted]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除表[dbo].[V_Cutting]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cutting]') AND type in (N'U'))
DROP TABLE [dbo].[V_Cutting]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_BatCurve]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatCurve]') AND type in (N'U'))
DROP TABLE [dbo].[V_BatCurve]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_BatCurve](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[PackId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[PType] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
	[ValueTime] [datetime] NOT NULL,
	[ProcTime] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_BatTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatTime]') AND type in (N'U'))
DROP TABLE [dbo].[V_BatTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_BatTime](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PackId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[ProcTime] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Elec]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Elec]') AND type in (N'U'))
DROP TABLE [dbo].[V_Elec]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Elec](
	[Id] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[FormulaType] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
 CONSTRAINT [PK_V_Elec] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC,
	[Type] ASC,
	[FormulaType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Offline]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Offline]') AND type in (N'U'))
DROP TABLE [dbo].[V_Offline]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Offline](
	[Id] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[FormulaType] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
 CONSTRAINT [PK_V_Offline] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC,
	[Type] ASC,
	[FormulaType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_ACabinet]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_ACabinet]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_ACabinet]') AND name = N'[ClusteredIndex]')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_ACabinet]
(
	[ValueTime] ASC,
	[Category] ASC,
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_BatCurve]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatCurve]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_BatCurve]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_BatCurve]
(
	[ValueTime] ASC,
	[DeviceId] ASC,
	[PackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatCurve]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_BatCurve]') AND name = N'NonClusteredIndex')
CREATE NONCLUSTERED INDEX [NonClusteredIndex] ON [dbo].[V_BatCurve]
(
	[ProcTime] ASC,
	[DeviceId] ASC,
	[PackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_BatTime]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatTime]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_BatTime]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_BatTime]
(
	[StartTime] ASC,
	[DeviceId] ASC,
	[PackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_BatTime]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_BatTime]') AND name = N'NonClusteredIndex')
CREATE NONCLUSTERED INDEX [NonClusteredIndex] ON [dbo].[V_BatTime]
(
	[ProcTime] ASC,
	[DeviceId] ASC,
	[PackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Elec]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Elec]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Elec]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_Elec]
(
	[StartTime] ASC,
	[Type] ASC,
	[FormulaType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Offline]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Offline]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Offline]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_Offline]
(
	[StartTime] ASC,
	[Type] ASC,
	[FormulaType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO