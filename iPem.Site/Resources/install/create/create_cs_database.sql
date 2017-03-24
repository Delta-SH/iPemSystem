/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2017/03/15
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Alarm]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Alarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_Alarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[A_Alarm](
	[Id] [varchar](200) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NULL,
	[PointId] [varchar](100) NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](20) NULL,
	[NMAlarmId] [varchar](100) NULL,
	[AlarmTime] [datetime] NULL,
	[AlarmLevel] [int] NULL,
	[AlarmFlag] [int] NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmValue] [float] NULL,
	[AlarmRemark] [varchar](100) NULL,
 CONSTRAINT [PK_A_Alarm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[SerialNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_HAlarm]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_HAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_HAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[A_HAlarm](
	[Id] [varchar](200) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NULL,
	[PointId] [varchar](100) NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](20) NULL,
	[NMAlarmId] [varchar](100) NULL,
	[AlarmTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AlarmValue] [float] NULL,
	[EndValue] [float] NULL,
	[AlarmLevel] [int] NULL,
	[AlarmFlag] [int] NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmRemark] [varchar](100) NULL,
 CONSTRAINT [PK_A_HAlarm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[SerialNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[TT_BatTime]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TT_BatTime]') AND type in (N'U'))
DROP TABLE [dbo].[TT_BatTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TT_BatTime](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[StartValue] [float] NOT NULL,
	[EndValue] [float] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TT_BatTime] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC,
	[StationId] ASC,
	[RoomId] ASC,
	[DeviceId] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[TT_Elec]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TT_Elec]') AND type in (N'U'))
DROP TABLE [dbo].[TT_Elec]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TT_Elec](
	[Id] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[FormulaType] [int] NOT NULL,
	[Period] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
 CONSTRAINT [PK_E_Elec] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Type] ASC,
	[FormulaType] ASC,
	[Period] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[TT_LoadRate]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TT_LoadRate]') AND type in (N'U'))
DROP TABLE [dbo].[TT_LoadRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TT_LoadRate](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TT_LoadRate] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC,
	[StationId] ASC,
	[RoomId] ASC,
	[DeviceId] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
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
--创建表[dbo].[V_HMeasure]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_HMeasure]') AND type in (N'U'))
DROP TABLE [dbo].[V_HMeasure]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_HMeasure](
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NULL,
	[DeviceId] [varchar](100) NULL,
	[PointId] [varchar](100) NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](10) NULL,
	[SignalDesc] [varchar](120) NULL,
	[Type] [int] NULL,
	[Value] [real] NULL,
	[OrderNumber] [varchar](20) NULL,
	[UpdateTime] [datetime] NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_HPoint]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_HPoint]') AND type in (N'U'))
DROP TABLE [dbo].[V_HPoint]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_HPoint](
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NULL,
	[DeviceId] [varchar](100) NULL,
	[PointId] [varchar](100) NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](10) NULL,
	[PointType] [int] NULL,
	[RecordType] [int] NULL,
	[Value] [real] NULL,
	[State] [int] NULL,
	[UpdateTime] [datetime] NULL
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
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NULL,
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