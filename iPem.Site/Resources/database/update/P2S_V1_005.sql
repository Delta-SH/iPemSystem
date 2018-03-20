--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增表[dbo].[X_ASerialNo]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[X_ASerialNo](
	[Name] [varchar](50) NOT NULL,
	[Code] [bigint] IDENTITY(0,1) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_X_ASerialNo] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--修改表[dbo].[U_Profile]

--删除外键[dbo].[FK_U_Profile_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] DROP CONSTRAINT [FK_U_Profile_U_Users]
GO

--删除表[dbo].[U_Profile]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Profile]') AND type in (N'U'))
DROP TABLE [dbo].[U_Profile]
GO

--创建表[dbo].[U_Profile]
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

--添加外键[dbo].[FK_U_Profile_U_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile]  WITH CHECK ADD  CONSTRAINT [FK_U_Profile_U_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[U_Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

--检查外键[dbo].[FK_U_Profile_U_Users]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_Profile_U_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_Profile]'))
ALTER TABLE [dbo].[U_Profile] CHECK CONSTRAINT [FK_U_Profile_U_Users]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除表[dbo].[U_FollowPoints]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_FollowPoints]') AND type in (N'U'))
DROP TABLE [dbo].[U_FollowPoints]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--修改表[dbo].[M_Reservations]

--删除外键[dbo].[FK_M_Appointments_M_Projects]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] DROP CONSTRAINT [FK_M_Appointments_M_Projects]
GO

--删除表[dbo].[M_Reservations]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Reservations]') AND type in (N'U'))
DROP TABLE [dbo].[M_Reservations]
GO

--创建表[dbo].[M_Reservations]
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

--添加外键[dbo].[FK_M_Appointments_M_Projects]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations]  WITH CHECK ADD  CONSTRAINT [FK_M_Appointments_M_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Projects] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

--检查外键[dbo].[FK_M_Appointments_M_Projects]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Appointments_M_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Reservations]'))
ALTER TABLE [dbo].[M_Reservations] CHECK CONSTRAINT [FK_M_Appointments_M_Projects]
GO
