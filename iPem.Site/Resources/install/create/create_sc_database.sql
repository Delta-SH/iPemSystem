/*
* P2S_V1 Database Script Library v1.2.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/12/08
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_G_Pages_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] DROP CONSTRAINT [FK_G_Pages_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_H_NoticesInUsers_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] DROP CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_M_NodesInReservation_M_Reservations]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] DROP CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_M_Appointments_M_Projects]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] DROP CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_AreasInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] DROP CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_FollowPoints_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_FollowPoints_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]'))
ALTER TABLE [dbo].[U_FollowPoints] DROP CONSTRAINT [FK_U_FollowPoints_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_MenusInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO
--删除外键[dbo].[FK_U_MenusInRoles_U_Menus]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] DROP CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_OperateInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] DROP CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_Profile_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] DROP CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_RoomsInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] DROP CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_StationsInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] DROP CONSTRAINT [FK_U_StationsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[FK_U_UsersInRoles_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] DROP CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO
--删除外键[dbo].[FK_U_UsersInRoles_U_Roles]
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
	[ExpStartTime] [datetime] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NOT NULL,
	[ProjectId] [varchar](100) NOT NULL,
	[Creator] [varchar](100) NOT NULL,
	[UserId] [varchar](100) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Comment] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
	[Status] [int] NOT NULL,
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
--删除表[dbo].[U_FollowPoints]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]') AND type in (N'U'))
DROP TABLE [dbo].[U_FollowPoints]
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
	[Type] [int] NOT NULL,
	[ValuesJson] [ntext] NULL,
	[ValuesBinary] [image] NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_U_Profile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Type] ASC
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
--创建表[dbo].[U_RoomsInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_RoomsInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_RoomsInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[RoomId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_RoomsInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_StationsInRoles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_StationsInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_StationsInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[StationId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_StationsInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[StationId] ASC
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
--新增表[dbo].[X_ASerialNo]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[X_ASerialNo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[X_ASerialNo](
	[Name] [varchar](50) NOT NULL,
	[Code] [bigint] IDENTITY(0,1) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_X_ASerialNo] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_G_Pages_U_Roles]
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
--添加外键[dbo].[FK_H_NoticesInUsers_U_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers]  WITH CHECK ADD  CONSTRAINT [FK_H_NoticesInUsers_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_H_NoticesInUsers_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[H_NoticesInUsers]'))
ALTER TABLE [dbo].[H_NoticesInUsers] CHECK CONSTRAINT [FK_H_NoticesInUsers_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_M_NodesInReservation_M_Reservations]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation]  WITH CHECK ADD  CONSTRAINT [FK_M_NodesInReservation_M_Reservations] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[M_Reservations] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_NodesInReservation_M_Reservations]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_NodesInReservation]'))
ALTER TABLE [dbo].[M_NodesInReservation] CHECK CONSTRAINT [FK_M_NodesInReservation_M_Reservations]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_M_Appointments_M_Projects]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations]  WITH CHECK ADD  CONSTRAINT [FK_M_Appointments_M_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Projects] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] CHECK CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_AreasInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_AreasInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_AreasInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]'))
ALTER TABLE [dbo].[U_AreasInRoles] CHECK CONSTRAINT [FK_U_AreasInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_MenusInRoles_U_Menus]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[U_Menus] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Menus]
GO
--添加外键[dbo].[FK_U_MenusInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRoles]'))
ALTER TABLE [dbo].[U_MenusInRoles] CHECK CONSTRAINT [FK_U_MenusInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_OperateInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_OperateInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_OperateInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_PermissionsInRoles]'))
ALTER TABLE [dbo].[U_PermissionsInRoles] CHECK CONSTRAINT [FK_U_OperateInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_Profile_U_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile]  WITH CHECK ADD  CONSTRAINT [FK_U_Profile_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] CHECK CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_RoomsInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_RoomsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] CHECK CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_StationsInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_StationsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] CHECK CONSTRAINT [FK_U_StationsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[FK_U_UsersInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Roles]
GO
--添加外键[dbo].[FK_U_UsersInRoles_U_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_UsersInRoles_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_UsersInRoles_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_UsersInRoles]'))
ALTER TABLE [dbo].[U_UsersInRoles] CHECK CONSTRAINT [FK_U_UsersInRoles_U_Users]
GO