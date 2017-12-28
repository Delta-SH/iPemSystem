--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[G_Pages]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_G_Pages_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Pages]'))
ALTER TABLE [dbo].[G_Pages] DROP CONSTRAINT [FK_G_Pages_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_RoomsInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] DROP CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_StationsInRoles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] DROP CONSTRAINT [FK_U_StationsInRoles_U_Roles]
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
--添加外键[dbo].[U_RoomsInRoles]
ALTER TABLE [dbo].[U_RoomsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_RoomsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] CHECK CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_StationsInRoles]
ALTER TABLE [dbo].[U_StationsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_StationsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] CHECK CONSTRAINT [FK_U_StationsInRoles_U_Roles]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增脚本升级日志
DECLARE @Id VARCHAR(100) = 'P2S_V1_002';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'新增组态功能、角色权限功能','Steven',GETDATE(),NULL,GETDATE(),'新增组态功能相关数据表、站点角色权限表、机房角色权限表');
GO