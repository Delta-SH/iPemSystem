/*
* P2R_V1 Data Script Library v1.2.0
* Copyright 2017, Delta
* Author: Chen.Jianwen
* Date: 2017/11/01
*/

USE [P2R_V1]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[P_Point]
DELETE FROM [dbo].[P_Point];
GO 

BULK INSERT [dbo].[P_Point] FROM 'F:\data\points.csv' WITH (FIELDTERMINATOR = '	',ROWTERMINATOR = '\n');
GO

UPDATE [dbo].[P_Point] SET [AlarmTimeDesc] = '',[NormalTimeDesc] = '',[DeviceEffect] = '',[BusiEffect] = '',[Comment] = '',[Extend1] = '',[Extend2] = '',[Desc] = '';
UPDATE [dbo].[P_Point] SET [AlarmID] = '' WHERE [AlarmID] IS NULL;
UPDATE [dbo].[P_Point] SET [NMAlarmID] = '' WHERE [NMAlarmID] IS NULL;
UPDATE [dbo].[P_Point] SET [UnitState] = '' WHERE [UnitState] IS NULL;
UPDATE [dbo].[P_Point] SET [Interpret] = '' WHERE [Interpret] IS NULL;
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[P_SubPoint]
BULK INSERT [dbo].[P_SubPoint] FROM 'F:\data\subpoints.csv' WITH (FIELDTERMINATOR = '	',ROWTERMINATOR = '\n');
GO

INSERT INTO [dbo].[P_SubPoint]([PointID],[StaTypeID],[AlarmLevel],[AlarmLimit],[AlarmReturnDiff],[AlarmDelay],[AlarmRecoveryDelay],[TriggerTypeID],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[StaticPeriod])
SELECT [PointID],'2' AS [StaTypeID],[AlarmLevel],[AlarmLimit],[AlarmReturnDiff],[AlarmDelay],[AlarmRecoveryDelay],[TriggerTypeID],[SavedPeriod],[AbsoluteThreshold],[PerThreshold],[StaticPeriod] FROM [dbo].[P_SubPoint] WHERE [StaTypeID] = '1';
GO

UPDATE [dbo].[P_SubPoint] SET [StorageRefTime] = '' WHERE [StorageRefTime] IS NULL;
GO