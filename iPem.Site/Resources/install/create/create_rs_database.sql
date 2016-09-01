/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/08/18
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Area]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Area]') AND type in (N'U'))
DROP TABLE [dbo].[A_Area]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_Area](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[NodeLevel] [int] NULL,
	[ParentId] [varchar](100) NOT NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_A_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Brand]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Brand]') AND type in (N'U'))
DROP TABLE [dbo].[C_Brand]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Brand](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ProductorId] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Department]
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Department]') AND type in (N'U'))
DROP TABLE [dbo].[C_Department]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Department](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Code] [varchar](50) NULL,
	[ParentId] [varchar](100) NOT NULL,
	[TypeDesc] [varchar](512) NULL,
	[Phone] [varchar](40) NULL,
	[PostCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_C_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO