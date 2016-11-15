/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/09/02
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[H_NoticesInUsers]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] DROP CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_H_Notices]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] DROP CONSTRAINT [FK_H_NoticesInUsers_H_Notices]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_Appointments]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Appointments]'))
ALTER TABLE [dbo].[M_Appointments] DROP CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_NodesInAppointment]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInAppointment_M_Appointments]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInAppointment]'))
ALTER TABLE [dbo].[M_NodesInAppointment] DROP CONSTRAINT [FK_M_NodesInAppointment_M_Appointments]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_AreasInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] DROP CONSTRAINT [FK_U_AreasInRoles_U_Roles]
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
--删除外键[dbo].[U_OperateInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_OperateInRoles]'))
ALTER TABLE [dbo].[U_OperateInRoles] DROP CONSTRAINT [FK_U_OperateInRoles_U_Roles]
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
--创建表[dbo].[AM_Devices]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AM_Devices]') AND type in (N'U'))
DROP TABLE [dbo].[AM_Devices]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AM_Devices](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [varchar](200) NOT NULL,
	[ParentId] [varchar](100) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_AM_Devices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[AM_Stations]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AM_Stations]') AND type in (N'U'))
DROP TABLE [dbo].[AM_Stations]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AM_Stations](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [varchar](200) NOT NULL,
	[Parent] [varchar](200) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_AM_Stations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_ExtendAlms]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_ExtendAlms]') AND type in (N'U'))
DROP TABLE [dbo].[H_ExtendAlms]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_ExtendAlms](
	[Id] [varchar](100) NOT NULL,
	[FsuId] [varchar](100) NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NULL,
	[ProjectId] [varchar](100) NULL,
	[Confirmed] [int] NULL,
	[Confirmer] [varchar](100) NULL,
	[ConfirmedTime] [datetime] NULL,
 CONSTRAINT [PK_H_ExtendAlms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[FsuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_Notices]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Notices]') AND type in (N'U'))
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]') AND type in (N'U'))
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_WebEvents]') AND type in (N'U'))
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
--创建表[dbo].[M_Appointments]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Appointments]') AND type in (N'U'))
DROP TABLE [dbo].[M_Appointments]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_Appointments](
	[Id] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[ProjectId] [varchar](100) NOT NULL,
	[Creator] [varchar](100) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Comment] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_M_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Dictionary]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Dictionary]') AND type in (N'U'))
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
--创建表[dbo].[M_NodesInAppointment]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_NodesInAppointment]') AND type in (N'U'))
DROP TABLE [dbo].[M_NodesInAppointment]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[M_NodesInAppointment](
	[AppointmentId] [varchar](100) NOT NULL,
	[NodeId] [varchar](100) NOT NULL,
	[NodeType] [int] NOT NULL,
 CONSTRAINT [PK_M_NodesInAppointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC,
	[NodeId] ASC,
	[NodeType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Projects]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Projects]') AND type in (N'U'))
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
	[Name] [varchar](100) NOT NULL,
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
--创建表[dbo].[U_AreasInRoles]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]') AND type in (N'U'))
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
--创建表[dbo].[U_Menus]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Menus]') AND type in (N'U'))
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
	[Name] [varchar](100) NOT NULL,
	[Icon] [varchar](512) NULL,
	[Url] [varchar](512) NULL,
	[Comment] [varchar](MAX) NULL,
	[Index] [int] NULL,
	[LastId] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_U_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_MenusInRoles]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]') AND type in (N'U'))
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
--创建表[dbo].[U_OperateInRoles]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_OperateInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_OperateInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_OperateInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[OperateId] [int] NOT NULL,
 CONSTRAINT [PK_U_OperateInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[OperateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Profile]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Profile]') AND type in (N'U'))
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Roles]') AND type in (N'U'))
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
	[Name] [varchar](100) NOT NULL,
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Users]') AND type in (N'U'))
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]') AND type in (N'U'))
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
--添加外键[dbo].[H_NoticesInUsers]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_H_Notices]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers]  WITH CHECK ADD  CONSTRAINT [FK_H_NoticesInUsers_H_Notices] FOREIGN KEY([NoticeId])
REFERENCES [dbo].[H_Notices] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_H_Notices]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] CHECK CONSTRAINT [FK_H_NoticesInUsers_H_Notices]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers]  WITH CHECK ADD  CONSTRAINT [FK_H_NoticesInUsers_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] CHECK CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_Appointments]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Appointments]'))
ALTER TABLE [dbo].[M_Appointments]  WITH CHECK ADD  CONSTRAINT [FK_M_Appointments_M_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Projects] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Appointments]'))
ALTER TABLE [dbo].[M_Appointments] CHECK CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_NodesInAppointment]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInAppointment_M_Appointments]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInAppointment]'))
ALTER TABLE [dbo].[M_NodesInAppointment]  WITH CHECK ADD  CONSTRAINT [FK_M_NodesInAppointment_M_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[M_Appointments] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInAppointment_M_Appointments]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInAppointment]'))
ALTER TABLE [dbo].[M_NodesInAppointment] CHECK CONSTRAINT [FK_M_NodesInAppointment_M_Appointments]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_AreasInRoles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_AreasInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] CHECK CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_MenusInRoles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[U_Menus] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_OperateInRoles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_OperateInRoles]'))
ALTER TABLE [dbo].[U_OperateInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_OperateInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_OperateInRoles]'))
ALTER TABLE [dbo].[U_OperateInRoles] CHECK CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_Profile]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile]  WITH CHECK ADD  CONSTRAINT [FK_U_Profile_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] CHECK CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_UsersInRoles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Roles]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO