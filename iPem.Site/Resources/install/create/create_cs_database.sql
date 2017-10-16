/*
* P2H_V1 Database Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_AAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_AAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_AAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_AAlarm](
	[Id] [varchar](200) NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[NMAlarmId] [varchar](100) NOT NULL,
	[AlarmTime] [datetime] NOT NULL,
	[AlarmLevel] [int] NOT NULL,
	[AlarmValue] [float] NOT NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmRemark] [varchar](100) NULL,
	[Confirmed] [int] NULL,
	[Confirmer] [varchar](100) NULL,
	[ConfirmedTime] [datetime] NULL,
	[ReservationId] [varchar](100) NULL,
	[PrimaryId] [varchar](200) NULL,
	[RelatedId] [varchar](200) NULL,
	[FilterId] [varchar](200) NULL,
	[ReversalId] [varchar](200) NULL,
	[ReversalCount] [int] NOT NULL,
	[Masked] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_A_AAlarm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_FAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_FAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_FAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_FAlarm](
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SignalId] [varchar](100) NOT NULL,
	[SignalNumber] [varchar](10) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[NMAlarmId] [varchar](100) NULL,
	[AlarmTime] [datetime] NULL,
	[AlarmLevel] [int] NULL,
	[AlarmFlag] [int] NOT NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmValue] [float] NULL,
	[AlarmRemark] [varchar](100) NULL,
 CONSTRAINT [PK_A_FAlarm] PRIMARY KEY CLUSTERED 
(
	[FsuId] ASC,
	[SerialNo] ASC,
	[AlarmFlag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_HAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_HAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_HAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_HAlarm](
	[Id] [varchar](200) NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[NMAlarmId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[AlarmLevel] [int] NOT NULL,
	[StartValue] [float] NOT NULL,
	[EndValue] [float] NOT NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmRemark] [varchar](100) NULL,
	[Confirmed] [int] NULL,
	[Confirmer] [varchar](100) NULL,
	[ConfirmedTime] [datetime] NULL,
	[ReservationId] [varchar](100) NULL,
	[PrimaryId] [varchar](200) NULL,
	[RelatedId] [varchar](200) NULL,
	[FilterId] [varchar](200) NULL,
	[ReversalId] [varchar](200) NULL,
	[ReversalCount] [int] NOT NULL,
	[Masked] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_A_HAlarm] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_IAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_IAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_IAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_IAlarm](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[NMAlarmId] [varchar](100) NOT NULL,
	[AlarmTime] [datetime] NOT NULL,
	[AlarmLevel] [int] NOT NULL,
	[AlarmFlag] [int] NOT NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmValue] [float] NULL,
	[AlarmRemark] [varchar](100) NULL,
	[Confirmed] [int] NULL,
	[Confirmer] [varchar](100) NULL,
	[ConfirmedTime] [datetime] NULL,
	[ReservationId] [varchar](100) NULL,
	[ReservationName] [varchar](200) NULL,
	[ReservationStart] [datetime] NULL,
	[ReservationEnd] [datetime] NULL,
	[PrimaryId] [varchar](200) NULL,
	[RelatedId] [varchar](200) NULL,
	[FilterId] [varchar](200) NULL,
	[ReversalId] [varchar](200) NULL,
	[Masked] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_A_IAlarm] PRIMARY KEY CLUSTERED 
(
	[FsuId] ASC,
	[SerialNo] ASC,
	[AlarmFlag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_MaxID]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaxID]') AND type in (N'U'))
DROP TABLE [dbo].[A_MaxID]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_MaxID](
	[GroupID] [varchar](100) NOT NULL,
	[MaxAlarmSerilNO] [bigint] NULL,
 CONSTRAINT [PK_A_MaxID] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_RAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_RAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_RAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_RAlarm](
	[GroupID] [varchar](100) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](100) NULL,
	[NMAlarmId] [varchar](100) NULL,
	[AlarmTime] [datetime] NULL,
	[AlarmLevel] [int] NULL,
	[AlarmFlag] [int] NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmValue] [float] NULL,
	[EndTime] [datetime] NULL,
	[EndValue] [float] NULL,
	[AlarmRemark] [varchar](100) NULL,
 CONSTRAINT [PK_A_RAlarm] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[SerialNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_TAlarm]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_TAlarm]') AND type in (N'U'))
DROP TABLE [dbo].[A_TAlarm]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_TAlarm](
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SignalId] [varchar](100) NOT NULL,
	[SignalNumber] [varchar](10) NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[NMAlarmId] [varchar](100) NULL,
	[AlarmTime] [datetime] NULL,
	[AlarmLevel] [int] NULL,
	[AlarmFlag] [int] NOT NULL,
	[AlarmDesc] [varchar](120) NULL,
	[AlarmValue] [float] NULL,
	[AlarmRemark] [varchar](100) NULL,
 CONSTRAINT [PK_A_TAlarm] PRIMARY KEY CLUSTERED 
(
	[FsuId] ASC,
	[SerialNo] ASC,
	[AlarmFlag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_CardRecord]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_CardRecord]') AND type in (N'U'))
DROP TABLE [dbo].[H_CardRecord]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_CardRecord](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[CardId] [varchar](100) NOT NULL,
	[PunchTime] [datetime] NOT NULL,
	[Status] [varchar](256) NULL,
	[Remark] [varchar](256) NULL,
	[Direction] [int] NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_DBScript]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_DBScript]') AND type in (N'U'))
DROP TABLE [dbo].[H_DBScript]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_DBScript](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NULL,
	[CreateUser] [varchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[ExecuteUser] [varchar](200) NULL,
	[ExecuteTime] [datetime] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_H_DBScript] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_FsuEvent]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_FsuEvent]') AND type in (N'U'))
DROP TABLE [dbo].[H_FsuEvent]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_FsuEvent](
	[FsuId] [varchar](100) NULL,
	[EventType] [int] NULL,
	[EventDesc] [varchar](500) NULL,
	[EventTime] [datetime] NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_IArea]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IArea]') AND type in (N'U'))
DROP TABLE [dbo].[H_IArea]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_IArea](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeId] [varchar](100) NULL,
	[TypeName] [varchar](200) NULL,
	[ParentId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_H_IArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_IDevice]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IDevice]') AND type in (N'U'))
DROP TABLE [dbo].[H_IDevice]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_IDevice](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeId] [varchar](100) NULL,
	[TypeName] [varchar](200) NULL,
	[StationId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_H_IDevice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_IStation]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_IStation]') AND type in (N'U'))
DROP TABLE [dbo].[H_IStation]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_IStation](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeId] [varchar](100) NULL,
	[TypeName] [varchar](200) NULL,
	[AreaId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_H_IStation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_AMeasure]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_AMeasure]') AND type in (N'U'))
DROP TABLE [dbo].[V_AMeasure]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_AMeasure](
	[GroupId] [varchar](100) NULL,
	[AreaId] [varchar](100) NULL,
	[StationId] [varchar](100) NULL,
	[RoomId] [varchar](100) NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[SignalId] [varchar](100) NULL,
	[SignalNumber] [varchar](10) NULL,
	[SignalDesc] [varchar](120) NULL,
	[Status] [int] NOT NULL,
	[Value] [float] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_V_AMeasure] PRIMARY KEY NONCLUSTERED 
(
	[FsuId] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Bat]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Bat]') AND type in (N'U'))
DROP TABLE [dbo].[V_Bat]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Bat](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[PackId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL,
	[ValueTime] [datetime] NOT NULL
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
	[PointId] [varchar](100) NOT NULL,
	[PackId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[StartValue] [float] NOT NULL,
	[EndValue] [float] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Cuted]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cuted]') AND type in (N'U'))
DROP TABLE [dbo].[V_Cuted]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Cuted](
	[Id] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_V_Cuted] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Cutting]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cutting]') AND type in (N'U'))
DROP TABLE [dbo].[V_Cutting]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Cutting](
	[Id] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
 CONSTRAINT [PK_V_Cutting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	[ComputeType] [int] NOT NULL,
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
--创建表[dbo].[V_HMeasure]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_HMeasure]') AND type in (N'U'))
DROP TABLE [dbo].[V_HMeasure]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	[Value] [float] NULL,
	[OrderNumber] [varchar](20) NULL,
	[UpdateTime] [datetime] NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Load]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Load]') AND type in (N'U'))
DROP TABLE [dbo].[V_Load]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Load](
	[AreaId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Value] [float] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_ParamDiff]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_ParamDiff]') AND type in (N'U'))
DROP TABLE [dbo].[V_ParamDiff]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_ParamDiff](
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[Threshold] [varchar](20) NULL,
	[AlarmLevel] [varchar](20) NULL,
	[NMAlarmID] [varchar](50) NULL,
	[AbsoluteVal] [varchar](20) NULL,
	[RelativeVal] [varchar](20) NULL,
	[StorageInterval] [varchar](20) NULL,
	[StorageRefTime] [varchar](50) NULL,
	[Masked] [bit] NULL,
 CONSTRAINT [PK_V_ParamDiff] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Static]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Static]') AND type in (N'U'))
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
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
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


--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建存储过程[dbo].[CreateTable]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateTable]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[CreateTable]
	@Name varchar(40),
	@TableDefine varchar(1000)
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT NAME FROM SYSOBJECTS WHERE XTYPE = 'U' AND NAME = @NAME)
		EXEC('CREATE TABLE [dbo].['+@Name+'](' + @TableDefine + ')');
END
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[A_AAlarm]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_AAlarm]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[A_AAlarm]') AND name = N'NonClusteredIndex')
CREATE NONCLUSTERED INDEX [NonClusteredIndex] ON [dbo].[A_AAlarm]
(
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[A_HAlarm]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_HAlarm]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[A_HAlarm]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[A_HAlarm]
(
	[StartTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[H_CardRecord]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_CardRecord]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[H_CardRecord]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[H_CardRecord]
(
	[PunchTime] ASC,
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[H_FsuEvent]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_FsuEvent]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[H_FsuEvent]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[H_FsuEvent]
(
	[EventTime] ASC,
	[FsuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_AMeasure]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_AMeasure]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_AMeasure]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_AMeasure]
(
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Bat]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Bat]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Bat]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_Bat]
(
	[ValueTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
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
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Cuted]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cuted]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Cuted]') AND name = N'ClusteredIndex')
CREATE NONCLUSTERED INDEX [NonClusteredIndex] ON [dbo].[V_Cuted]
(
	[StartTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Cutting]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Cutting]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Cutting]') AND name = N'ClusteredIndex')
CREATE NONCLUSTERED INDEX [NonClusteredIndex] ON [dbo].[V_Cutting]
(
	[StartTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
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
--创建索引[dbo].[V_HMeasure]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_HMeasure]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_HMeasure]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_HMeasure]
(
	[UpdateTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Load]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Load]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Load]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_Load]
(
	[StartTime] ASC,
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建索引[dbo].[V_Static]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Static]') AND type in (N'U')) 
AND NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[V_Static]') AND name = N'ClusteredIndex')
CREATE CLUSTERED INDEX [ClusteredIndex] ON [dbo].[V_Static]
(
	[StartTime] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO