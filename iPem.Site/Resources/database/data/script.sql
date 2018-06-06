/*
* P2R_V1 Data Script Library v1.0.0
* Copyright 2018, Delta
* Author: Chen.Jianwen
* Date: 2018/01/01
*/

USE [P2R_V1]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[P_Point]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] DROP CONSTRAINT [FK_P_PointsInProtocol_P_Point]
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] DROP CONSTRAINT [FK_D_Signal_P_Point]
GO

DELETE FROM [dbo].[P_SubPoint];
GO

DELETE FROM [dbo].[P_Point]
GO

BULK INSERT [dbo].[P_Point] FROM 'D:\data\points.csv' WITH (FIELDTERMINATOR = '	',ROWTERMINATOR = '\n');
GO

UPDATE [dbo].[P_Point] SET [AlarmTimeDesc] = '',[NormalTimeDesc] = '',[DeviceEffect] = '',[BusiEffect] = '',[Comment] = '',[Extend1] = '',[Extend2] = '',[Desc] = '';
UPDATE [dbo].[P_Point] SET [AlarmID] = '' WHERE [AlarmID] IS NULL OR [AlarmID] = 'NULL';
UPDATE [dbo].[P_Point] SET [NMAlarmID] = '' WHERE [NMAlarmID] IS NULL OR [NMAlarmID] = 'NULL';
UPDATE [dbo].[P_Point] SET [UnitState] = '' WHERE [UnitState] IS NULL;
UPDATE [dbo].[P_Point] SET [Interpret] = '' WHERE [Interpret] IS NULL;
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[P_SubPoint]
BULK INSERT [dbo].[P_SubPoint] FROM 'D:\data\subpoints.csv' WITH (FIELDTERMINATOR = '	',ROWTERMINATOR = '\n');
GO

INSERT INTO [dbo].[P_SubPoint]([PointID],[StaTypeID],[AlarmLevel],[AlarmLimit],[AlarmReturnDiff],[AlarmDelay],[AlarmRecoveryDelay],[TriggerTypeID],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[StaticPeriod])
SELECT [PointID],'2' AS [StaTypeID],[AlarmLevel],[AlarmLimit],[AlarmReturnDiff],[AlarmDelay],[AlarmRecoveryDelay],[TriggerTypeID],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[StaticPeriod] FROM [dbo].[P_SubPoint] WHERE [StaTypeID] = '1';
GO

UPDATE [dbo].[P_SubPoint] SET [StorageRefTime] = '' WHERE [StorageRefTime] IS NULL;
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--检查有问题的中心及模板中的信号
SELECT * FROM [dbo].[D_Signal] WHERE [PointID] NOT IN(SELECT [ID] FROM [dbo].[P_Point]);
GO
SELECT * FROM [dbo].[P_PointsInProtocol] WHERE [PointID] NOT IN (SELECT [ID] FROM [dbo].[P_Point]);
GO