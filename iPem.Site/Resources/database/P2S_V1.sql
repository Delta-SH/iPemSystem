/*
* P2S_V1 Database Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
*/

USE [master]
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'P2S_V1')
CREATE DATABASE [P2S_V1]
GO

USE [P2S_V1]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_G_Pages_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] DROP CONSTRAINT [FK_G_Pages_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[H_NoticesInUsers]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] DROP CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_NodesInReservation]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] DROP CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_Reservations]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] DROP CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_AreasInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] DROP CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_FollowPoints]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_FollowPoints_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]'))
ALTER TABLE [dbo].[U_FollowPoints] DROP CONSTRAINT [FK_U_FollowPoints_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_MenusInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_PermissionsInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] DROP CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_Profile]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] DROP CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_UsersInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] DROP CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] DROP CONSTRAINT [FK_U_UsersInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[G_Images]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[G_Images]') AND type in (N'U'))
DROP TABLE [dbo].[G_Images]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[G_Images](
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[Content] [image] NOT NULL,
	[Thumbnail] [image] NOT NULL,
	[UpdateMark] [varchar](100) NULL,
	[Remark] [varchar](512) NULL,
 CONSTRAINT [PK_G_Images] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[G_Pages]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[G_Pages]') AND type in (N'U'))
DROP TABLE [dbo].[G_Pages]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[G_Pages](
	[RoleId] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsHome] [bit] NOT NULL,
	[Content] [text] NULL,
	[ObjId] [varchar](100) NOT NULL,
	[ObjType] [int] NOT NULL,
	[Remark] [varchar](512) NULL,
 CONSTRAINT [PK_G_Pages] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[G_Templates]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[G_Templates]') AND type in (N'U'))
DROP TABLE [dbo].[G_Templates]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[G_Templates](
	[Name] [varchar](200) NOT NULL,
	[Content] [text] NULL,
	[Remark] [varchar](512) NULL,
 CONSTRAINT [PK_G_Templates] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[H_Notices]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Notices]') AND type in (N'U'))
DROP TABLE [dbo].[H_Notices]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_Notices](
	[Id] [varchar](100) NOT NULL,
	[Title] [varchar](512) NOT NULL,
	[Content] [varchar](max) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_H_Notices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_NoticesInUsers]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]') AND type in (N'U'))
DROP TABLE [dbo].[H_NoticesInUsers]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_NoticesInUsers](
	[NoticeId] [varchar](100) NOT NULL,
	[UserId] [varchar](100) NOT NULL,
	[Readed] [bit] NULL,
	[ReadTime] [datetime] NULL,
 CONSTRAINT [PK_H_NoticesInUsers] PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_WebEvents]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_WebEvents]') AND type in (N'U'))
DROP TABLE [dbo].[H_WebEvents]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_WebEvents](
	[Id] [varchar](100) NOT NULL,
	[Level] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[ShortMessage] [nvarchar](1024) NOT NULL,
	[FullMessage] [ntext] NULL,
	[IpAddress] [varchar](200) NULL,
	[PageUrl] [varchar](512) NULL,
	[ReferrerUrl] [varchar](512) NULL,
	[UserId] [varchar](100) NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_H_WebEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Dictionary]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Dictionary]') AND type in (N'U'))
DROP TABLE [dbo].[M_Dictionary]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_Dictionary](
	[Id] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ValuesJson] [ntext] NULL,
	[ValuesBinary] [image] NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_M_Dictionary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Formulas]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Formulas]') AND type in (N'U'))
DROP TABLE [dbo].[M_Formulas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_Formulas](
	[Id] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[FormulaType] [int] NOT NULL,
	[ComputeType] [int] NOT NULL,
	[Formula] [varchar](max) NULL,
	[Comment] [varchar](1024) NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_M_Formulas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Type] ASC,
	[FormulaType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_NodesInReservation]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]') AND type in (N'U'))
DROP TABLE [dbo].[M_NodesInReservation]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_NodesInReservation](
	[ReservationId] [varchar](100) NOT NULL,
	[NodeId] [varchar](100) NOT NULL,
	[NodeType] [int] NOT NULL,
 CONSTRAINT [PK_M_NodesInReservation] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC,
	[NodeId] ASC,
	[NodeType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Projects]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Projects]') AND type in (N'U'))
DROP TABLE [dbo].[M_Projects]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_Projects](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Responsible] [varchar](50) NULL,
	[ContactPhone] [varchar](20) NULL,
	[Company] [varchar](100) NULL,
	[Creator] [varchar](100) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Comment] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_M_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Reservations]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Reservations]') AND type in (N'U'))
DROP TABLE [dbo].[M_Reservations]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_Reservations](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[ProjectId] [varchar](100) NOT NULL,
	[Creator] [varchar](100) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Comment] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_M_Reservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_AreasInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_AreasInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_AreasInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[AreaId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_AreasInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_FollowPoints]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]') AND type in (N'U'))
DROP TABLE [dbo].[U_FollowPoints]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_FollowPoints](
	[UserId] [varchar](100) NOT NULL,
	[DeviceId] [varchar](100) NOT NULL,
	[PointId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_FollowPoints] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DeviceId] ASC,
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Menus]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Menus]') AND type in (N'U'))
DROP TABLE [dbo].[U_Menus]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Menus](
	[Id] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Icon] [varchar](512) NULL,
	[Url] [varchar](512) NULL,
	[Comment] [varchar](max) NULL,
	[Index] [int] NULL,
	[LastId] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_U_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_MenusInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_MenusInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_MenusInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[MenuId] [int] NOT NULL,
 CONSTRAINT [PK_U_MenusInRoles] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_PermissionsInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_PermissionsInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_PermissionsInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[Permission] [int] NOT NULL,
 CONSTRAINT [PK_U_PermissionsInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[Permission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Profile]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Profile]') AND type in (N'U'))
DROP TABLE [dbo].[U_Profile]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Profile](
	[UserId] [varchar](100) NOT NULL,
	[ValuesJson] [ntext] NULL,
	[ValuesBinary] [image] NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_U_Profile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Roles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Roles]') AND type in (N'U'))
DROP TABLE [dbo].[U_Roles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Roles](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Comment] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_U_Roles] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Users]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Users]') AND type in (N'U'))
DROP TABLE [dbo].[U_Users]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Users](
	[Id] [varchar](100) NOT NULL,
	[Uid] [varchar](100) NOT NULL,
	[Password] [varchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [varchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LimitedDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordDate] [datetime] NULL,
	[IsLockedOut] [bit] NOT NULL,
	[LastLockoutDate] [datetime] NULL,
	[Comment] [varchar](512) NULL,
	[EmployeeId] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_U_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_UsersInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_UsersInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_UsersInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[UserId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[G_Pages]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages]  WITH CHECK ADD  CONSTRAINT [FK_G_Pages_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] CHECK CONSTRAINT [FK_G_Pages_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[H_NoticesInUsers]
ALTER TABLE [dbo].[H_NoticesInUsers]  WITH CHECK ADD  CONSTRAINT [FK_H_NoticesInUsers_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] CHECK CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_NodesInReservation]
ALTER TABLE [dbo].[M_NodesInReservation]  WITH CHECK ADD  CONSTRAINT [FK_M_NodesInReservation_M_Reservations] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[M_Reservations] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] CHECK CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_Reservations]
ALTER TABLE [dbo].[M_Reservations]  WITH CHECK ADD  CONSTRAINT [FK_M_Appointments_M_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Projects] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] CHECK CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_AreasInRoles]
ALTER TABLE [dbo].[U_AreasInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_AreasInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] CHECK CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_FollowPoints]
ALTER TABLE [dbo].[U_FollowPoints]  WITH CHECK ADD  CONSTRAINT [FK_U_FollowPoints_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_FollowPoints_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]'))
ALTER TABLE [dbo].[U_FollowPoints] CHECK CONSTRAINT [FK_U_FollowPoints_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_MenusInRoles]
ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[U_Menus] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO

ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_PermissionsInRoles]
ALTER TABLE [dbo].[U_PermissionsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_OperateInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] CHECK CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_Profile]
ALTER TABLE [dbo].[U_Profile]  WITH CHECK ADD  CONSTRAINT [FK_U_Profile_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] CHECK CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_UsersInRoles]
ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Roles]
GO

ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO

/*
* P2S_V1 Data Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
*/

USE [P2S_V1]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[M_Dictionary]
DELETE FROM [dbo].[M_Dictionary];
GO

INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(1,N'数据管理',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(2,N'语音播报',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(3,N'能耗分类',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(4,N'报表参数',NULL,NULL,GETDATE());
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus];
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1, N'系统管理', '/content/themes/icons/menu-xtgl.png', NULL, N'系统管理', 1, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2, N'配置管理', '/content/themes/icons/menu-pzgl.png', NULL, N'配置管理', 2, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3, N'虚拟界面', '/content/themes/icons/menu-xnjm.png', NULL, N'虚拟界面', 3, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4, N'系统报表', '/content/themes/icons/menu-report.png', NULL, N'系统报表', 4, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5, N'系统指标', '/content/themes/icons/menu-kpi.png', NULL, N'系统指标', 5, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1001, N'角色管理', '/content/themes/icons/menu-jsgl.png', '/Account/Roles', N'角色是指系统相关权限的集合组，使用角色可以简化权限管理，实现用户权限的统一分配管理。角色管理主要实现了角色信息的新增、编辑、删除及角色权限分配等功能。', 1, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1002, N'用户管理', '/content/themes/icons/menu-yhgl.png', '/Account/Users', N'用户是指登录系统所需的账号密码信息，用户必须隶属于某个角色并继承该角色所拥有的系统权限。用户管理主要实现了用户信息的新增、编辑、删除及密码重置等功能。', 2, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1003, N'日志管理', '/content/themes/icons/menu-rzgl.png', '/Account/Events', N'日志管理是对系统中记录的系统异常、操作事件等日志信息进行管理维护。用户可以查询、导出系统日志记录，定期删除过期的日志记录等操作。', 3, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1004, N'消息管理', '/content/themes/icons/menu-xxgl.png', '/Account/Notice', N'消息管理是对站内的公开消息、公告、通知、提示等信息进行发布管理。用户可以查询、发布、更新、删除站内的广播消息。', 4, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005, N'参数管理', '/content/themes/icons/menu-csgl.png', '/Account/Dictionary', N'参数管理是对系统运行所需的各类参数进行管理维护，用户可根据系统实际的业务逻辑需求配置适合的参数。<br/>能耗公式说明:<br/>1. 公式中以“@设备名称>>信号名称”的形式映射一个具体设备的信号点。<br/>2. 设备和信号名称中禁止出现“@”、“>>”、“+”、“-”、“*”、“/”、“(”、“)”等符号。<br/>公式示例: ((@设备1>>信号1 + @设备2>>信号2 - @设备3>>信号3) * @设备4>>信号4) / @设备5>>信号5', 5, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2001, N'FSU信息管理', '/content/themes/icons/menu-fsucx.png', '/Fsu/Index', N'FSU信息查询是指对系统内符合查询条件的FSU进行查询展现，提供FSU的编码、IP、端口、是否离线、离线时间等相关信息。', 1, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2002, N'FSU配置管理', '/content/themes/icons/menu-fsupz.png', '/Fsu/Configuration', N'FSU在线管理是指对系统内符合查询条件的FSU进行在线配置管理，提供修改单个或批量修改多个FSU相关配置的能力。', 2, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2003, N'FSU日志管理', '/content/themes/icons/menu-fsuftp.png', '/Fsu/Event', N'FSU日志是指SC对FSU中的FTP文件下载、解析、操作的日志记录；FSU日志管理提供对FTP日志记录的筛选、查询、导出等功能。', 3, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2004, N'动环参数巡检', '/content/themes/icons/menu-csgl.png', '/Fsu/ParamDiff', N'动环参数巡检是指系统可自动对动环参数进行巡检，包括历史数据存储周期、告警信号的门限值、信号是否被屏蔽等，生成巡检结果，提示不合理的参数设置，以提醒维护人员核对和修正。<br/>巡检结果格式：当前值&标准值', 4, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2005, N'设备管理', '/content/themes/icons/leaf.png', '/Account/2005', N'设备管理', 5, 2, 0);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2006, N'工程信息管理', '/content/themes/icons/menu-gcgl.png', '/Project/Index', N'工程信息管理是对系统所涉及的工程项目进行维护管理，以完善后续的报表统计功能。注：因工程预约需要引用工程信息，为了保证工程预约信息的完整性，暂不提供删除工程功能。', 6, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2007, N'工程预约管理', '/content/themes/icons/menu-gcyy.png', '/Project/Reservation', N'工程预约管理是对施工过程中所影响到的区域、站点、机房、设备等进行提前预约，系统会根据预约信息将其产生的告警标识为工程告警，为后续报表统计提供数据支持。注：因告警信息需要引用工程预约信息，为了保证告警信息的完整性，暂不提供删除预约功能。', 7, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3001, N'图形组态', '/content/themes/icons/menu-txzt.png', '/Configuration/Index', N'图形组态提供了更加灵活多样化、更加直观的图形方式展示系统中设备的物理拓扑图以及设备信号的实时测值。', 1, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3002, N'电子地图', '/content/themes/icons/menu-dzdt.png', '/Configuration/Map', N'电子地图提供了系统中各站点的地理位置、路线指导、站点概况、实时告警等信息的概览功能。<br/>注：在使用电子地图之前，请为站点设置经纬度信息。', 2, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4001, N'基础报表', '/content/themes/icons/menu-report-jc.png', NULL, N'基础报表', 1, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4002, N'历史报表', '/content/themes/icons/menu-report-ls.png', NULL, N'历史报表', 2, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4003, N'图形报表', '/content/themes/icons/menu-report-qx.png', NULL, N'图形报表', 3, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4004, N'订制报表', '/content/themes/icons/menu-report-dz.png', NULL, N'订制报表', 4, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5001, N'健康度指标(核心站点)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'健康度指标(核心站点)', 1, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5002, N'健康度指标(其他站点)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'健康度指标(其他站点)', 2, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5003, N'能耗指标', '/content/themes/icons/menu-kpi-nh.png', NULL, N'能耗指标', 3, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5004, N'订制指标', '/content/themes/icons/menu-kpi-dz.png', NULL, N'订制指标', 4, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400101, N'区域统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'区域统计是指对系统所有区域按照区域类型进行分类统计、对比分析。', 1, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400102, N'站点统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'站点统计是指对系统所有站点按照站点类型进行分类统计、对比分析。', 2, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400103, N'机房统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'机房统计是指对系统所有机房按照机房类型进行分类统计、对比分析。', 3, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400104, N'设备统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'设备统计是指对系统所有设备按照设备类型进行分类统计、对比分析。', 4, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400105, N'员工统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'员工统计是指对系统内所有正式员工的基本信息及门禁相关信息（持卡情况、授权设备等）进行统计、分析。', 5, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400106, N'外协统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'员工统计是指对系统内所有外协人员的基本信息及门禁相关信息（持卡情况、授权设备等）进行统计、分析。', 6, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400201, N'历史测值查詢', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'历史测值查詢提供了系统所有设备信号点产生的历史数据的查询统计、数据导出等功能。', 1, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400202, N'历史告警查詢', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'历史告警查詢提供了系统所有设备信号点产生的历史告警的查询统计、对比分析、数据导出等功能。', 2, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400203, N'告警分类统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'告警分类统计提供了系统所有设备信号点产生的历史告警的分类统计、对比分析、数据导出等功能。', 3, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400204, N'设备告警统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'设备告警统计提供了系统所有设备信号点产生的历史告警的分类统计、对比分析、数据导出等功能。', 4, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400205, N'工程项目统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'工程项目统计提供了系统中所有工程项目的查询统计、数据导出等功能。', 5, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400206, N'工程预约统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'工程预约统计提供了系统中所有工程预约的查询统计、数据导出等功能。', 6, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400207, N'市电停电统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'市电停电统计提供了系统中所有市电停电站点的查询统计、数据导出等功能。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>市电停电"里的参数信息。', 7, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400208, N'油机发电统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'油机发电统计提供了系统中所有油机发电站点的查询统计、数据导出等功能。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>油机发电"里的参数信息。', 8, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400209, N'刷卡记录统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'刷卡记录统计提供了系统中所有正式员工、外协人员的刷卡记录的查询统计、数据导出等功能。', 9, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400210, N'刷卡次数统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'刷卡记录统计提供了系统中所有正式员工、外协人员的刷卡次数的查询统计、数据导出等功能。', 10, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400211, N'放电次数统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'放电次数统计提供了系统中所有蓄电池的放电次数、放电过程、放电曲线的查询统计、数据导出等功能。', 11, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400301, N'信号测值曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'信号测值曲线是指以曲线图形的方式展示系统中设备信号产生的历史数据。', 1, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400302, N'信号统计曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'信号统计曲线是指以曲线图形的方式展示系统中设备信号产生的统计数据。', 2, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400303, N'电池放电曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'电池放电曲线是指以曲线图形的方式展示系统中电池放电产生的历史数据。', 3, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400401, N'超频告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超频告警是指对在一定时间范围内，告警次数超过规定次数的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 1, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400402, N'超短告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超短告警是指对在一定时间范围内，告警历时小于规定时长的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 2, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400403, N'超长告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超长告警是指对在一定时间范围内，告警历时大于规定时长的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 3, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500101, N'直流系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'直流系统可用度 = {1 - 开关电源蓄电池组总电压低告警时长 / (开关电源蓄电池组数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>直流系统可用度(核心站点)"里的参数信息。', 1, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500102, N'交流不间断系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'交流不间断系统可用度 = {1 - (UPS蓄电池组总电压低告警时长 + UPS旁路运行时长) / (UPS蓄电池组数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>交流不间断系统可用度(核心站点)"里的参数信息。', 2, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500103, N'温控系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'温控系统可用度 = {1 - 高温告警时长 / (温度测点总数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>温控系统可用度(核心站点)"里的参数信息。', 3, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500104, N'监控可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控可用度 = {1 - 采集设备中断告警时长 / (采集设备数量 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>监控可用度(核心站点)"里的参数信息。', 4, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500105, N'市电可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'市电可用度 = {1 - 市电停电告警时长 / (市电路数 × 统计时长)} × 100%', 5, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500201, N'监控覆盖率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控覆盖率 = (本月监控站点总数 / 上月监控站点总数) × 100%', 1, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500202, N'关键监控测点接入率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'关键监控测点接入率 = (本月监控设备数量 / 上月监控设备数量) × 100%<br/>注：<br/>1.关键监控测点指"开关电源"、"蓄电池总电压"。<br/>2.在使用之前，请确保已设置了"系统管理>系统参数>报表参数>关键监控测点接入率(其他站点)"里的参数信息。', 2, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500203, N'站点标识率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'站点标识率 = (本月标示站点总数 / 上月站点总数) × 100%', 3, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500204, N'开关电源带载合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'开关电源带载合格率 = 开关电源带载率合格套数 / 开关电源总套数 × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>开关电源带载合格率(其他站点)"里的参数信息。', 4, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500205, N'蓄电池后备时长合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'蓄电池后备时长合格率 = 已完成放电基站蓄电池后备时长合格基站数量 / 已完成放电基站数量 × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>蓄电池后备时长合格率(其他站点)"里的参数信息。', 5, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500206, N'温控容量合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'温控容量合格率 = (1 - 高温告警站点总数 / 包含温度测点的站点总数) × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>温控容量合格率(其他站点)"里的参数信息。', 6, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500207, N'直流系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'直流系统可用度 = {1 - 开关电源一次下电告警总时长 / (开关电源套数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>直流系统可用度(其他站点)"里的参数信息。', 7, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500208, N'监控故障处理及时率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控故障处理及时率 = {1 - 站点通信中断告警总时长 / (系统站点总数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>监控故障处理及时率(其他站点)"里的参数信息。', 8, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500209, N'蓄电池核对性放电及时率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'蓄电池核对性放电及时率 = 蓄电池1小时以上放电站点总数 / 系统站点总数 × 100%', 9, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500301, N'能耗分类统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'能耗分类统计是指对同一统计对象按照能耗分类进行查询统计、对比分析。', 1, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500302, N'能耗趋势分析', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'能耗趋势分析是指对同一统计对象按照不同的统计周期进行查询统计、对比分析。', 2, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500303, N'能耗同址同比', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'能耗同址同比是指对同一统计对象本期的总能耗和上年同期的总能耗进行查询统计、对比分析。', 3, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500304, N'能耗同址环比', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'能耗同址环比是指对同一统计对象本期的总能耗和上期的总能耗进行查询统计、对比分析。', 4, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500305, N'站点能耗对比', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'站点能耗对比是指对选中的两个站点的总能耗进行查询统计、对比分析。<br/>注：仅支持两个站点进行对比分析。', 5, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500306, N'站点PUE统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'PUE是评价站点能源效率的指标，PUE = 总能耗 / 设备能耗，PUE是一个比值，基准是2，越接近1表明能源效率越高。', 6, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500401, N'系统设备完好率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'系统设备完好率 = {1－设备告警总时长 / (设备数量 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>系统设备完好率"里的参数信息。', 1, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500402, N'故障处理及时率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'故障处理及时率 = {1－超出规定处理时长的设备故障次数 / 设备故障总次数} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>故障处理及时率"里的参数信息。', 2, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500403, N'告警确认及时率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'告警确认及时率 = {1－超出规定确认时长的告警条数 / 告警总条数} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>告警确认及时率"里的参数信息。', 3, 5004, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Roles]
DELETE FROM [dbo].[U_Roles];
GO

INSERT INTO [dbo].[U_Roles]([Id],[Name],[Comment],[Enabled]) VALUES('a0000000-6000-2000-1000-f00000000000','Administrator','超级管理员',1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Users]
DELETE FROM [dbo].[U_Users];
GO

INSERT INTO [dbo].[U_Users]([Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled]) VALUES('62ab161f-dcbb-633b-b6a0-a9ebf6099862', 'system', 'ynMbt/ns3PKIJvDa/a6UiwDwxrE=', 1, '4RltREDrDBzwvkPj0j5hLg==', GETDATE(), '2099-12-31', GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE(), '默认用户', '00001', 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_UsersInRoles]
DELETE FROM [dbo].[U_UsersInRoles];
GO

INSERT INTO [dbo].[U_UsersInRoles]([RoleId],[UserId]) VALUES('a0000000-6000-2000-1000-f00000000000', '62ab161f-dcbb-633b-b6a0-a9ebf6099862');
GO