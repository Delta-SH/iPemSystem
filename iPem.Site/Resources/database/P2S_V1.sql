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

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[FK_G_Pages_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] DROP CONSTRAINT [FK_G_Pages_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[H_NoticesInUsers]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] DROP CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[M_NodesInReservation]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] DROP CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[M_Reservations]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] DROP CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_AreasInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] DROP CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_FollowPoints]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_FollowPoints_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]'))
ALTER TABLE [dbo].[U_FollowPoints] DROP CONSTRAINT [FK_U_FollowPoints_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_MenusInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_PermissionsInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] DROP CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_Profile]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] DROP CONSTRAINT [FK_U_Profile_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_UsersInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] DROP CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] DROP CONSTRAINT [FK_U_UsersInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Images]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Pages]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Templates]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_DBScript]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_Notices]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_NoticesInUsers]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_WebEvents]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Dictionary]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Formulas]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_NodesInReservation]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Projects]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Reservations]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_AreasInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_FollowPoints]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Menus]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_MenusInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_PermissionsInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Profile]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Roles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Users]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_UsersInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Pages]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages]  WITH CHECK ADD  CONSTRAINT [FK_G_Pages_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] CHECK CONSTRAINT [FK_G_Pages_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_NoticesInUsers]
ALTER TABLE [dbo].[H_NoticesInUsers]  WITH CHECK ADD  CONSTRAINT [FK_H_NoticesInUsers_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] CHECK CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_NodesInReservation]
ALTER TABLE [dbo].[M_NodesInReservation]  WITH CHECK ADD  CONSTRAINT [FK_M_NodesInReservation_M_Reservations] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[M_Reservations] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] CHECK CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Reservations]
ALTER TABLE [dbo].[M_Reservations]  WITH CHECK ADD  CONSTRAINT [FK_M_Appointments_M_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Projects] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] CHECK CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_AreasInRoles]
ALTER TABLE [dbo].[U_AreasInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_AreasInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] CHECK CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_FollowPoints]
ALTER TABLE [dbo].[U_FollowPoints]  WITH CHECK ADD  CONSTRAINT [FK_U_FollowPoints_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_FollowPoints_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]'))
ALTER TABLE [dbo].[U_FollowPoints] CHECK CONSTRAINT [FK_U_FollowPoints_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_MenusInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_PermissionsInRoles]
ALTER TABLE [dbo].[U_PermissionsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_OperateInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] CHECK CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Profile]
ALTER TABLE [dbo].[U_Profile]  WITH CHECK ADD  CONSTRAINT [FK_U_Profile_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] CHECK CONSTRAINT [FK_U_Profile_U_Users]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_UsersInRoles]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[M_Dictionary]
DELETE FROM [dbo].[M_Dictionary];
GO

INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(1,N'���ݹ���',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(2,N'��������',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(3,N'�ܺķ���',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(4,N'�������',NULL,NULL,GETDATE());
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus];
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1, N'ϵͳ����', '/content/themes/icons/menu-xtgl.png', NULL, N'ϵͳ����', 1, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2, N'���ù���', '/content/themes/icons/menu-pzgl.png', NULL, N'���ù���', 2, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3, N'�������', '/content/themes/icons/menu-xnjm.png', NULL, N'�������', 3, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4, N'ϵͳ����', '/content/themes/icons/menu-report.png', NULL, N'ϵͳ����', 4, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5, N'ϵͳָ��', '/content/themes/icons/menu-kpi.png', NULL, N'ϵͳָ��', 5, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1001, N'��ɫ����', '/content/themes/icons/menu-jsgl.png', '/Account/Roles', N'��ɫ��ָϵͳ���Ȩ�޵ļ����飬ʹ�ý�ɫ���Լ�Ȩ�޹���ʵ���û�Ȩ�޵�ͳһ���������ɫ������Ҫʵ���˽�ɫ��Ϣ���������༭��ɾ������ɫȨ�޷���ȹ��ܡ�', 1, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1002, N'�û�����', '/content/themes/icons/menu-yhgl.png', '/Account/Users', N'�û���ָ��¼ϵͳ������˺�������Ϣ���û�����������ĳ����ɫ���̳иý�ɫ��ӵ�е�ϵͳȨ�ޡ��û�������Ҫʵ�����û���Ϣ���������༭��ɾ�����������õȹ��ܡ�', 2, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1003, N'��־����', '/content/themes/icons/menu-rzgl.png', '/Account/Events', N'��־�����Ƕ�ϵͳ�м�¼��ϵͳ�쳣�������¼�����־��Ϣ���й���ά�����û����Բ�ѯ������ϵͳ��־��¼������ɾ�����ڵ���־��¼�Ȳ�����', 3, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1004, N'��Ϣ����', '/content/themes/icons/menu-xxgl.png', '/Account/Notice', N'��Ϣ�����Ƕ�վ�ڵĹ�����Ϣ�����桢֪ͨ����ʾ����Ϣ���з��������û����Բ�ѯ�����������¡�ɾ��վ�ڵĹ㲥��Ϣ��', 4, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005, N'��������', '/content/themes/icons/menu-csgl.png', '/Account/Dictionary', N'���������Ƕ�ϵͳ��������ĸ���������й���ά�����û��ɸ���ϵͳʵ�ʵ�ҵ���߼����������ʺϵĲ�����<br/>�ܺĹ�ʽ˵��:<br/>1. ��ʽ���ԡ�@�豸����>>�ź����ơ�����ʽӳ��һ�������豸���źŵ㡣<br/>2. �豸���ź������н�ֹ���֡�@������>>������+������-������*������/������(������)���ȷ��š�<br/>��ʽʾ��: ((@�豸1>>�ź�1 + @�豸2>>�ź�2 - @�豸3>>�ź�3) * @�豸4>>�ź�4) / @�豸5>>�ź�5', 5, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2001, N'FSU��Ϣ����', '/content/themes/icons/menu-fsucx.png', '/Fsu/Index', N'FSU��Ϣ��ѯ��ָ��ϵͳ�ڷ��ϲ�ѯ������FSU���в�ѯչ�֣��ṩFSU�ı��롢IP���˿ڡ��Ƿ����ߡ�����ʱ��������Ϣ��', 1, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2002, N'FSU���ù���', '/content/themes/icons/menu-fsupz.png', '/Fsu/Configuration', N'FSU���߹�����ָ��ϵͳ�ڷ��ϲ�ѯ������FSU�����������ù����ṩ�޸ĵ����������޸Ķ��FSU������õ�������', 2, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2003, N'FSU��־����', '/content/themes/icons/menu-fsuftp.png', '/Fsu/Event', N'FSU��־��ָSC��FSU�е�FTP�ļ����ء���������������־��¼��FSU��־�����ṩ��FTP��־��¼��ɸѡ����ѯ�������ȹ��ܡ�', 3, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2004, N'��������Ѳ��', '/content/themes/icons/menu-csgl.png', '/Fsu/ParamDiff', N'��������Ѳ����ָϵͳ���Զ��Զ�����������Ѳ�죬������ʷ���ݴ洢���ڡ��澯�źŵ�����ֵ���ź��Ƿ����εȣ�����Ѳ��������ʾ������Ĳ������ã�������ά����Ա�˶Ժ�������<br/>Ѳ������ʽ����ǰֵ&��׼ֵ', 4, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2005, N'�豸����', '/content/themes/icons/leaf.png', '/Account/2005', N'�豸����', 5, 2, 0);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2006, N'������Ϣ����', '/content/themes/icons/menu-gcgl.png', '/Project/Index', N'������Ϣ�����Ƕ�ϵͳ���漰�Ĺ�����Ŀ����ά�����������ƺ����ı���ͳ�ƹ��ܡ�ע���򹤳�ԤԼ��Ҫ���ù�����Ϣ��Ϊ�˱�֤����ԤԼ��Ϣ�������ԣ��ݲ��ṩɾ�����̹��ܡ�', 6, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2007, N'����ԤԼ����', '/content/themes/icons/menu-gcyy.png', '/Project/Reservation', N'����ԤԼ�����Ƕ�ʩ����������Ӱ�쵽������վ�㡢�������豸�Ƚ�����ǰԤԼ��ϵͳ�����ԤԼ��Ϣ��������ĸ澯��ʶΪ���̸澯��Ϊ��������ͳ���ṩ����֧�֡�ע����澯��Ϣ��Ҫ���ù���ԤԼ��Ϣ��Ϊ�˱�֤�澯��Ϣ�������ԣ��ݲ��ṩɾ��ԤԼ���ܡ�', 7, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3001, N'ͼ����̬', '/content/themes/icons/menu-txzt.png', '/Configuration/Index', N'ͼ����̬�ṩ�˸�����������������ֱ�۵�ͼ�η�ʽչʾϵͳ���豸����������ͼ�Լ��豸�źŵ�ʵʱ��ֵ��', 1, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3002, N'���ӵ�ͼ', '/content/themes/icons/menu-dzdt.png', '/Configuration/Map', N'���ӵ�ͼ�ṩ��ϵͳ�и�վ��ĵ���λ�á�·��ָ����վ��ſ���ʵʱ�澯����Ϣ�ĸ������ܡ�<br/>ע����ʹ�õ��ӵ�ͼ֮ǰ����Ϊվ�����þ�γ����Ϣ��', 2, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4001, N'��������', '/content/themes/icons/menu-report-jc.png', NULL, N'��������', 1, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4002, N'��ʷ����', '/content/themes/icons/menu-report-ls.png', NULL, N'��ʷ����', 2, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4003, N'ͼ�α���', '/content/themes/icons/menu-report-qx.png', NULL, N'ͼ�α���', 3, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4004, N'���Ʊ���', '/content/themes/icons/menu-report-dz.png', NULL, N'���Ʊ���', 4, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5001, N'������ָ��(����վ��)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'������ָ��(����վ��)', 1, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5002, N'������ָ��(����վ��)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'������ָ��(����վ��)', 2, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5003, N'�ܺ�ָ��', '/content/themes/icons/menu-kpi-nh.png', NULL, N'�ܺ�ָ��', 3, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5004, N'����ָ��', '/content/themes/icons/menu-kpi-dz.png', NULL, N'����ָ��', 4, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400101, N'����ͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'����ͳ����ָ��ϵͳ�����������������ͽ��з���ͳ�ơ��Աȷ�����', 1, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400102, N'վ��ͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'վ��ͳ����ָ��ϵͳ����վ�㰴��վ�����ͽ��з���ͳ�ơ��Աȷ�����', 2, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400103, N'����ͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'����ͳ����ָ��ϵͳ���л������ջ������ͽ��з���ͳ�ơ��Աȷ�����', 3, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400104, N'�豸ͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'�豸ͳ����ָ��ϵͳ�����豸�����豸���ͽ��з���ͳ�ơ��Աȷ�����', 4, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400105, N'Ա��ͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'Ա��ͳ����ָ��ϵͳ��������ʽԱ���Ļ�����Ϣ���Ž������Ϣ���ֿ��������Ȩ�豸�ȣ�����ͳ�ơ�������', 5, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400106, N'��Эͳ��', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'Ա��ͳ����ָ��ϵͳ��������Э��Ա�Ļ�����Ϣ���Ž������Ϣ���ֿ��������Ȩ�豸�ȣ�����ͳ�ơ�������', 6, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400201, N'��ʷ��ֵ��ԃ', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'��ʷ��ֵ��ԃ�ṩ��ϵͳ�����豸�źŵ��������ʷ���ݵĲ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 1, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400202, N'��ʷ�澯��ԃ', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'��ʷ�澯��ԃ�ṩ��ϵͳ�����豸�źŵ��������ʷ�澯�Ĳ�ѯͳ�ơ��Աȷ��������ݵ����ȹ��ܡ�', 2, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400203, N'�澯����ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�澯����ͳ���ṩ��ϵͳ�����豸�źŵ��������ʷ�澯�ķ���ͳ�ơ��Աȷ��������ݵ����ȹ��ܡ�', 3, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400204, N'�豸�澯ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�豸�澯ͳ���ṩ��ϵͳ�����豸�źŵ��������ʷ�澯�ķ���ͳ�ơ��Աȷ��������ݵ����ȹ��ܡ�', 4, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400205, N'������Ŀͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'������Ŀͳ���ṩ��ϵͳ�����й�����Ŀ�Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 5, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400206, N'����ԤԼͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'����ԤԼͳ���ṩ��ϵͳ�����й���ԤԼ�Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 6, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400207, N'�е�ͣ��ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�е�ͣ��ͳ���ṩ��ϵͳ�������е�ͣ��վ��Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�е�ͣ��"��Ĳ�����Ϣ��', 7, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400208, N'�ͻ�����ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�ͻ�����ͳ���ṩ��ϵͳ�������ͻ�����վ��Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�ͻ�����"��Ĳ�����Ϣ��', 8, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400209, N'ˢ����¼ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'ˢ����¼ͳ���ṩ��ϵͳ��������ʽԱ������Э��Ա��ˢ����¼�Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 9, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400210, N'ˢ������ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'ˢ����¼ͳ���ṩ��ϵͳ��������ʽԱ������Э��Ա��ˢ�������Ĳ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 10, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400211, N'�ŵ����ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�ŵ����ͳ���ṩ��ϵͳ���������صķŵ�������ŵ���̡��ŵ����ߵĲ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 11, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400301, N'�źŲ�ֵ����', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'�źŲ�ֵ������ָ������ͼ�εķ�ʽչʾϵͳ���豸�źŲ�������ʷ���ݡ�', 1, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400302, N'�ź�ͳ������', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'�ź�ͳ��������ָ������ͼ�εķ�ʽչʾϵͳ���豸�źŲ�����ͳ�����ݡ�', 2, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400303, N'��طŵ�����', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'��طŵ�������ָ������ͼ�εķ�ʽչʾϵͳ�е�طŵ��������ʷ���ݡ�', 3, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400401, N'��Ƶ�澯', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'��Ƶ�澯��ָ����һ��ʱ�䷶Χ�ڣ��澯���������涨�����ĸ澯���в�ѯͳ�ơ��Աȷ�����<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��', 1, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400402, N'���̸澯', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'���̸澯��ָ����һ��ʱ�䷶Χ�ڣ��澯��ʱС�ڹ涨ʱ���ĸ澯���в�ѯͳ�ơ��Աȷ�����<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��', 2, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400403, N'�����澯', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'�����澯��ָ����һ��ʱ�䷶Χ�ڣ��澯��ʱ���ڹ涨ʱ���ĸ澯���в�ѯͳ�ơ��Աȷ�����<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��', 3, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500101, N'ֱ��ϵͳ���ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'ֱ��ϵͳ���ö� = {1 - ���ص�Դ�������ܵ�ѹ�͸澯ʱ�� / (���ص�Դ�������� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>ֱ��ϵͳ���ö�(����վ��)"��Ĳ�����Ϣ��', 1, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500102, N'���������ϵͳ���ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'���������ϵͳ���ö� = {1 - (UPS�������ܵ�ѹ�͸澯ʱ�� + UPS��·����ʱ��) / (UPS�������� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>���������ϵͳ���ö�(����վ��)"��Ĳ�����Ϣ��', 2, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500103, N'�¿�ϵͳ���ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'�¿�ϵͳ���ö� = {1 - ���¸澯ʱ�� / (�¶Ȳ������ �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�¿�ϵͳ���ö�(����վ��)"��Ĳ�����Ϣ��', 3, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500104, N'��ؿ��ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'��ؿ��ö� = {1 - �ɼ��豸�жϸ澯ʱ�� / (�ɼ��豸���� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>��ؿ��ö�(����վ��)"��Ĳ�����Ϣ��', 4, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500105, N'�е���ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'�е���ö� = {1 - �е�ͣ��澯ʱ�� / (�е�·�� �� ͳ��ʱ��)} �� 100%', 5, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500201, N'��ظ�����', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'��ظ����� = (���¼��վ������ / ���¼��վ������) �� 100%', 1, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500202, N'�ؼ���ز�������', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'�ؼ���ز������� = (���¼���豸���� / ���¼���豸����) �� 100%<br/>ע��<br/>1.�ؼ���ز��ָ"���ص�Դ"��"�����ܵ�ѹ"��<br/>2.��ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�ؼ���ز�������(����վ��)"��Ĳ�����Ϣ��', 2, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500203, N'վ���ʶ��', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'վ���ʶ�� = (���±�ʾվ������ / ����վ������) �� 100%', 3, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500204, N'���ص�Դ���غϸ���', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'���ص�Դ���غϸ��� = ���ص�Դ�����ʺϸ����� / ���ص�Դ������ �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>���ص�Դ���غϸ���(����վ��)"��Ĳ�����Ϣ��', 4, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500205, N'���غ�ʱ���ϸ���', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'���غ�ʱ���ϸ��� = ����ɷŵ��վ���غ�ʱ���ϸ��վ���� / ����ɷŵ��վ���� �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>���غ�ʱ���ϸ���(����վ��)"��Ĳ�����Ϣ��', 5, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500206, N'�¿������ϸ���', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'�¿������ϸ��� = (1 - ���¸澯վ������ / �����¶Ȳ���վ������) �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�¿������ϸ���(����վ��)"��Ĳ�����Ϣ��', 6, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500207, N'ֱ��ϵͳ���ö�', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'ֱ��ϵͳ���ö� = {1 - ���ص�Դһ���µ�澯��ʱ�� / (���ص�Դ���� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>ֱ��ϵͳ���ö�(����վ��)"��Ĳ�����Ϣ��', 7, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500208, N'��ع��ϴ���ʱ��', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'��ع��ϴ���ʱ�� = {1 - վ��ͨ���жϸ澯��ʱ�� / (ϵͳվ������ �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>��ع��ϴ���ʱ��(����վ��)"��Ĳ�����Ϣ��', 8, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500209, N'���غ˶��Էŵ缰ʱ��', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'���غ˶��Էŵ缰ʱ�� = ����1Сʱ���Ϸŵ�վ������ / ϵͳվ������ �� 100%', 9, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500301, N'�ܺķ���ͳ��', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'�ܺķ���ͳ����ָ��ͬһͳ�ƶ������ܺķ�����в�ѯͳ�ơ��Աȷ�����', 1, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500302, N'�ܺ����Ʒ���', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'�ܺ����Ʒ�����ָ��ͬһͳ�ƶ����ղ�ͬ��ͳ�����ڽ��в�ѯͳ�ơ��Աȷ�����', 2, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500303, N'�ܺ�ַͬͬ��', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'�ܺ�ַͬͬ����ָ��ͬһͳ�ƶ����ڵ����ܺĺ�����ͬ�ڵ����ܺĽ��в�ѯͳ�ơ��Աȷ�����', 3, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500304, N'�ܺ�ַͬ����', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'�ܺ�ַͬ������ָ��ͬһͳ�ƶ����ڵ����ܺĺ����ڵ����ܺĽ��в�ѯͳ�ơ��Աȷ�����', 4, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500305, N'վ���ܺĶԱ�', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'վ���ܺĶԱ���ָ��ѡ�е�����վ������ܺĽ��в�ѯͳ�ơ��Աȷ�����<br/>ע����֧������վ����жԱȷ�����', 5, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500306, N'վ��PUEͳ��', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'PUE������վ����ԴЧ�ʵ�ָ�꣬PUE = ���ܺ� / �豸�ܺģ�PUE��һ����ֵ����׼��2��Խ�ӽ�1������ԴЧ��Խ�ߡ�', 6, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500401, N'ϵͳ�豸�����', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'ϵͳ�豸����� = {1���豸�澯��ʱ�� / (�豸���� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>ϵͳ�豸�����"��Ĳ�����Ϣ��', 1, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500402, N'���ϴ���ʱ��', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'���ϴ���ʱ�� = {1�������涨����ʱ�����豸���ϴ��� / �豸�����ܴ���} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>���ϴ���ʱ��"��Ĳ�����Ϣ��', 2, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500403, N'�澯ȷ�ϼ�ʱ��', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'�澯ȷ�ϼ�ʱ�� = {1�������涨ȷ��ʱ���ĸ澯���� / �澯������} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�澯ȷ�ϼ�ʱ��"��Ĳ�����Ϣ��', 3, 5004, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_Roles]
DELETE FROM [dbo].[U_Roles];
GO

INSERT INTO [dbo].[U_Roles]([Id],[Name],[Comment],[Enabled]) VALUES('a0000000-6000-2000-1000-f00000000000','Administrator','��������Ա',1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_Users]
DELETE FROM [dbo].[U_Users];
GO

INSERT INTO [dbo].[U_Users]([Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled]) VALUES('62ab161f-dcbb-633b-b6a0-a9ebf6099862', 'system', 'ynMbt/ns3PKIJvDa/a6UiwDwxrE=', 1, '4RltREDrDBzwvkPj0j5hLg==', GETDATE(), '2099-12-31', GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE(), 'Ĭ���û�', '00001', 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_UsersInRoles]
DELETE FROM [dbo].[U_UsersInRoles];
GO

INSERT INTO [dbo].[U_UsersInRoles]([RoleId],[UserId]) VALUES('a0000000-6000-2000-1000-f00000000000', '62ab161f-dcbb-633b-b6a0-a9ebf6099862');
GO