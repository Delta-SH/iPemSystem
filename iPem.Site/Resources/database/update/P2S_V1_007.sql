--��������������������������������������������������������������������������������������������������������������������������������������
--[dbo].[U_Roles]�����ӡ�type���ֶ�
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[U_Roles]') AND name = N'Type')
BEGIN
	ALTER TABLE [dbo].[U_Roles] ADD [Type] int  NOT NULL DEFAULT 0;
END
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--[dbo].[U_Roles]�����ӡ�Config���ֶ�
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[U_Roles]') AND name = N'Config')
BEGIN
	ALTER TABLE [dbo].[U_Roles] ADD [Config] int  NOT NULL DEFAULT 0;
END
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--[dbo].[U_Roles]�����ӡ�ValuesJson���ֶ�
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[U_Roles]') AND name = N'ValuesJson')
BEGIN
	ALTER TABLE [dbo].[U_Roles] ADD [ValuesJson] ntext NULL;
END
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--[dbo].[U_AreasInRoles]�����ֶ�����AreaId��Ϊ��NodeId��
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[U_AreasInRoles]') AND name = N'AreaId')
BEGIN
EXEC sp_rename 'U_AreasInRoles.[AreaId]', 'NodeId', 'COLUMN'
END
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_StationsInRoles]

--ɾ�����[dbo].[FK_U_StationsInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] DROP CONSTRAINT [FK_U_StationsInRoles_U_Roles]
GO

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
	[NodeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_StationsInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--������[dbo].[FK_U_StationsInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_StationsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_StationsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_StationsInRoles]'))
ALTER TABLE [dbo].[U_StationsInRoles] CHECK CONSTRAINT [FK_U_StationsInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_RoomsInRoles]

--ɾ�����[dbo].[FK_U_RoomsInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] DROP CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

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
	[NodeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_RoomsInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--������[dbo].[FK_U_RoomsInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_RoomsInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_RoomsInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_RoomsInRoles]'))
ALTER TABLE [dbo].[U_RoomsInRoles] CHECK CONSTRAINT [FK_U_RoomsInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_DevicesInRoles]

--ɾ�����[dbo].[FK_U_DevicesInRoles_U_Roles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_DevicesInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_DevicesInRoles]'))
ALTER TABLE [dbo].[U_DevicesInRoles] DROP CONSTRAINT [FK_U_DevicesInRoles_U_Roles]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_DevicesInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[U_DevicesInRoles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_DevicesInRoles](
	[RoleId] [varchar](100) NOT NULL,
	[NodeId] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_DevicesInRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--������[dbo].[FK_U_DevicesInRoles_U_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_DevicesInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_DevicesInRoles]'))
ALTER TABLE [dbo].[U_DevicesInRoles]  WITH CHECK ADD  CONSTRAINT [FK_U_DevicesInRoles_U_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[U_Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_DevicesInRoles_U_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_DevicesInRoles]'))
ALTER TABLE [dbo].[U_DevicesInRoles] CHECK CONSTRAINT [FK_U_DevicesInRoles_U_Roles]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�����ű�������־
DECLARE @Id VARCHAR(100) = 'P2S_V1_007';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'��ɫȨ��ϸ�����豸,���Ӷ���/�����澯����','Scorpio',GETDATE(),NULL,GETDATE(),'��ɫȨ��ϸ�����豸,���Ӷ���/�����澯����');
GO