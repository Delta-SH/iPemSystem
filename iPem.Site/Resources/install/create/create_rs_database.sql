/*
* P2R_V1 Database Script Library v1.0.0
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/07/10
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[C_SubDeviceType]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubDeviceType_C_DeviceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]'))
ALTER TABLE [dbo].[C_SubDeviceType] DROP CONSTRAINT [FK_C_SubDeviceType_C_DeviceType]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[C_SubLogicType]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubLogicType_C_LogicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]'))
ALTER TABLE [dbo].[C_SubLogicType] DROP CONSTRAINT [FK_C_SubLogicType_C_LogicType]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_ACDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ACDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ACDistBox]'))
ALTER TABLE [dbo].[D_ACDistBox] DROP CONSTRAINT [FK_D_ACDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_AirCondHost]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondHost_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondHost]'))
ALTER TABLE [dbo].[D_AirCondHost] DROP CONSTRAINT [FK_D_AirCondHost_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_AirCondWindCabi]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCabi]'))
ALTER TABLE [dbo].[D_AirCondWindCabi] DROP CONSTRAINT [FK_D_AirCondWindCabi_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_AirCondWindCool]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCool_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCool]'))
ALTER TABLE [dbo].[D_AirCondWindCool] DROP CONSTRAINT [FK_D_AirCondWindCool_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_BattGroup]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattGroup]'))
ALTER TABLE [dbo].[D_BattGroup] DROP CONSTRAINT [FK_D_BattGroup_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_BattTempBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattTempBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattTempBox]'))
ALTER TABLE [dbo].[D_BattTempBox] DROP CONSTRAINT [FK_D_BattTempBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_ChangeHeat]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ChangeHeat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ChangeHeat]'))
ALTER TABLE [dbo].[D_ChangeHeat] DROP CONSTRAINT [FK_D_ChangeHeat_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_CombSwitElecSour]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_CombSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_CombSwitElecSour]'))
ALTER TABLE [dbo].[D_CombSwitElecSour] DROP CONSTRAINT [FK_D_CombSwitElecSour_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_ControlEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ControlEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ControlEqui]'))
ALTER TABLE [dbo].[D_ControlEqui] DROP CONSTRAINT [FK_D_ControlEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_DCDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DCDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DCDistBox]'))
ALTER TABLE [dbo].[D_DCDistBox] DROP CONSTRAINT [FK_D_DCDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_Device]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Device_S_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Device]'))
ALTER TABLE [dbo].[D_Device] DROP CONSTRAINT [FK_D_Device_S_Room]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_DivSwitElecSour]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DivSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DivSwitElecSour]'))
ALTER TABLE [dbo].[D_DivSwitElecSour] DROP CONSTRAINT [FK_D_DivSwitElecSour_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_ElecSourCabi]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ElecSourCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ElecSourCabi]'))
ALTER TABLE [dbo].[D_ElecSourCabi] DROP CONSTRAINT [FK_D_ElecSourCabi_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_GeneratorGroup]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_GeneratorGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_GeneratorGroup]'))
ALTER TABLE [dbo].[D_GeneratorGroup] DROP CONSTRAINT [FK_D_GeneratorGroup_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_HighVoltDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_HighVoltDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_HighVoltDistBox]'))
ALTER TABLE [dbo].[D_HighVoltDistBox] DROP CONSTRAINT [FK_D_HighVoltDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_Inverter]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Inverter_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Inverter]'))
ALTER TABLE [dbo].[D_Inverter] DROP CONSTRAINT [FK_D_Inverter_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_LowDistCabinet]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_LowDistCabinet_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_LowDistCabinet]'))
ALTER TABLE [dbo].[D_LowDistCabinet] DROP CONSTRAINT [FK_D_LowDistCabinet_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_Manostat]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Manostat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Manostat]'))
ALTER TABLE [dbo].[D_Manostat] DROP CONSTRAINT [FK_D_Manostat_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_MobiGenerator]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_MobiGenerator_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_MobiGenerator]'))
ALTER TABLE [dbo].[D_MobiGenerator] DROP CONSTRAINT [FK_D_MobiGenerator_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_OrdiAirCond]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_OrdiAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_OrdiAirCond]'))
ALTER TABLE [dbo].[D_OrdiAirCond] DROP CONSTRAINT [FK_D_OrdiAirCond_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_RedefinePoint]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] DROP CONSTRAINT [FK_D_RedefinePoint_P_Point]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] DROP CONSTRAINT [FK_D_RedefinePoint_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_SolarController]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarController_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarController]'))
ALTER TABLE [dbo].[D_SolarController] DROP CONSTRAINT [FK_D_SolarController_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_SolarEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarEqui]'))
ALTER TABLE [dbo].[D_SolarEqui] DROP CONSTRAINT [FK_D_SolarEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_SpecAirCond]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SpecAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SpecAirCond]'))
ALTER TABLE [dbo].[D_SpecAirCond] DROP CONSTRAINT [FK_D_SpecAirCond_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_SwitchFuse]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SwitchFuse_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SwitchFuse]'))
ALTER TABLE [dbo].[D_SwitchFuse] DROP CONSTRAINT [FK_D_SwitchFuse_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_Transformer]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Transformer_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Transformer]'))
ALTER TABLE [dbo].[D_Transformer] DROP CONSTRAINT [FK_D_Transformer_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_UPS]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_UPS_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_UPS]'))
ALTER TABLE [dbo].[D_UPS] DROP CONSTRAINT [FK_D_UPS_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_Ventilation]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Ventilation_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Ventilation]'))
ALTER TABLE [dbo].[D_Ventilation] DROP CONSTRAINT [FK_D_Ventilation_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_WindEnerEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindEnerEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindEnerEqui]'))
ALTER TABLE [dbo].[D_WindEnerEqui] DROP CONSTRAINT [FK_D_WindEnerEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_WindLightCompCon]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindLightCompCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindLightCompCon]'))
ALTER TABLE [dbo].[D_WindLightCompCon] DROP CONSTRAINT [FK_D_WindLightCompCon_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[D_WindPowerCon]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindPowerCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindPowerCon]'))
ALTER TABLE [dbo].[D_WindPowerCon] DROP CONSTRAINT [FK_D_WindPowerCon_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[P_PointsInProtocol]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Protocol]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] DROP CONSTRAINT [FK_P_PointsInProtocol_P_Protocol]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] DROP CONSTRAINT [FK_P_PointsInProtocol_P_Point]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[P_SubPoint]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_SubPoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_SubPoint]'))
ALTER TABLE [dbo].[P_SubPoint] DROP CONSTRAINT [FK_P_SubPoint_P_Point]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[S_Room]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Room_S_Station]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Room]'))
ALTER TABLE [dbo].[S_Room] DROP CONSTRAINT [FK_S_Room_S_Station]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[S_Station]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Station_A_Area]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Station]'))
ALTER TABLE [dbo].[S_Station] DROP CONSTRAINT [FK_S_Station_A_Area]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[U_MenusInRole]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRole_U_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRole]'))
ALTER TABLE [dbo].[U_MenusInRole] DROP CONSTRAINT [FK_U_MenusInRole_U_Role]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[A_Area]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[A_Area]') AND type in (N'U'))
DROP TABLE [dbo].[A_Area]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[A_Area](
	[ID] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ParentID] [varchar](100) NOT NULL,
	[NodeLevel] [int] NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_A_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_AreaType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_AreaType]') AND type in (N'U'))
DROP TABLE [dbo].[C_AreaType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_AreaType](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_C_AreaType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Brand]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Brand]') AND type in (N'U'))
DROP TABLE [dbo].[C_Brand]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Brand](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ProductorID] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Brand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Department]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Department]') AND type in (N'U'))
DROP TABLE [dbo].[C_Department]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Department](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Code] [varchar](50) NULL,
	[ParentID] [varchar](100) NOT NULL,
	[TypeDesc] [varchar](512) NULL,
	[Phone] [varchar](40) NULL,
	[PostCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_DeviceType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_DeviceType]') AND type in (N'U'))
DROP TABLE [dbo].[C_DeviceType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_DeviceType](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_DeviceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Duty]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Duty]') AND type in (N'U'))
DROP TABLE [dbo].[C_Duty]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Duty](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Level] [varchar](40) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Duty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_EnumMethods]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_EnumMethods]') AND type in (N'U'))
DROP TABLE [dbo].[C_EnumMethods]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_EnumMethods](
	[ID] [int] NOT NULL,
	[Index] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_EnumMethods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Group]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Group]') AND type in (N'U'))
DROP TABLE [dbo].[C_Group]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Group](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[IP] [varchar](20) NULL,
	[Port] [int] NULL,
	[ChangeTime] [datetime] NOT NULL,
	[LastTime] [datetime] NOT NULL,
	[Desc] [varchar](512) NOT NULL,
 CONSTRAINT [PK_C_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_LogicType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_LogicType]') AND type in (N'U'))
DROP TABLE [dbo].[C_LogicType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_LogicType](
	[ID] [varchar](100) NOT NULL,
	[DeviceTypeID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_C_LogicType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Productor]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Productor]') AND type in (N'U'))
DROP TABLE [dbo].[C_Productor]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Productor](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[EngName] [varchar](200) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](200) NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Productor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_RoomType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_RoomType]') AND type in (N'U'))
DROP TABLE [dbo].[C_RoomType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_RoomType](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_RoomType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SCVendor]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SCVendor]') AND type in (N'U'))
DROP TABLE [dbo].[C_SCVendor]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[C_SCVendor](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_C_SCVendor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_StationType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_StationType]') AND type in (N'U'))
DROP TABLE [dbo].[C_StationType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_StationType](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_StationType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubCompany]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubCompany]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubCompany]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubCompany](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[linkMan] [varchar](40) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](200) NULL,
	[Level] [int] NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_SubCompany] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubDeviceType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubDeviceType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubDeviceType](
	[ID] [varchar](100) NOT NULL,
	[DeviceTypeID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_SubDeviceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_SubLogicType]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]') AND type in (N'U'))
DROP TABLE [dbo].[C_SubLogicType]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_SubLogicType](
	[ID] [varchar](100) NOT NULL,
	[LogicTypeID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_C_SubLogicType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Supplier]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Supplier]') AND type in (N'U'))
DROP TABLE [dbo].[C_Supplier]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Supplier](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[linkMan] [varchar](40) NULL,
	[Phone] [varchar](40) NULL,
	[Fax] [varchar](40) NULL,
	[Address] [varchar](160) NULL,
	[Level] [int] NULL,
	[PostalCode] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[C_Unit]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[C_Unit]') AND type in (N'U'))
DROP TABLE [dbo].[C_Unit]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[C_Unit](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_C_Unit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_ACDistBox]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_ACDistBox]') AND type in (N'U'))
DROP TABLE [dbo].[D_ACDistBox]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_ACDistBox](
	[DeviceID] [varchar](100) NOT NULL,
	[TotalCap] [varchar](20) NOT NULL,
	[OutputPortNumber] [int] NOT NULL,
 CONSTRAINT [PK_D_ACDistBox] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_AirCondHost]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_AirCondHost]') AND type in (N'U'))
DROP TABLE [dbo].[D_AirCondHost]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_AirCondHost](
	[DeviceID] [varchar](100) NOT NULL,
	[MakeCoolCap] [varchar](20) NOT NULL,
	[WorkID] [int] NOT NULL,
	[InputRatedPower] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_AirCondHost] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_AirCondWindCabi]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCabi]') AND type in (N'U'))
DROP TABLE [dbo].[D_AirCondWindCabi]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_AirCondWindCabi](
	[DeviceID] [varchar](100) NOT NULL,
	[InputRatedPower] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_AirCondWindCabi] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_AirCondWindCool]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCool]') AND type in (N'U'))
DROP TABLE [dbo].[D_AirCondWindCool]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_AirCondWindCool](
	[DeviceID] [varchar](100) NOT NULL,
	[InputRatedPower] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_AirCondWindCool] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_BattGroup]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_BattGroup]') AND type in (N'U'))
DROP TABLE [dbo].[D_BattGroup]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_BattGroup](
	[DeviceID] [varchar](100) NOT NULL,
	[SingGroupCap] [varchar](20) NOT NULL,
	[SingVoltGrade] [int] NOT NULL,
	[SingGroupBattNumber] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_BattGroup] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_BattTempBox]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_BattTempBox]') AND type in (N'U'))
DROP TABLE [dbo].[D_BattTempBox]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_BattTempBox](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedMakeCoolCap] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_BattTempBox] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_ChangeHeat]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_ChangeHeat]') AND type in (N'U'))
DROP TABLE [dbo].[D_ChangeHeat]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_ChangeHeat](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_ChangeHeat] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_CombSwitElecSour]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_CombSwitElecSour]') AND type in (N'U'))
DROP TABLE [dbo].[D_CombSwitElecSour]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_CombSwitElecSour](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedOutputVolt] [float] NOT NULL,
	[MoniModuleModel] [varchar](20) NOT NULL,
	[ExisRModuleCount] [varchar](20) NOT NULL,
	[RModuleModel] [varchar](20) NOT NULL,
	[RModuleRatedWorkVolt] [int] NOT NULL,
	[SingRModuleRatedOPCap] [varchar](20) NOT NULL,
	[SingGBattGFuseCap] [varchar](20) NOT NULL,
	[BattGFuseGNumber] [int] NOT NULL,
	[OrCanSecoDownPower] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_CombSwitElecSour] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_ControlEqui]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_ControlEqui]') AND type in (N'U'))
DROP TABLE [dbo].[D_ControlEqui]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_ControlEqui](
	[DeviceID] [varchar](100) NOT NULL,
	[DeviceIP] [varchar](20) NULL,
 CONSTRAINT [PK_D_ControlEqui] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_DCDistBox]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_DCDistBox]') AND type in (N'U'))
DROP TABLE [dbo].[D_DCDistBox]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_DCDistBox](
	[DeviceID] [varchar](100) NOT NULL,
	[TotalCap] [varchar](20) NOT NULL,
	[OutputPortNumber] [int] NULL,
 CONSTRAINT [PK_D_DCDistBox] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Device]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Device]') AND type in (N'U'))
DROP TABLE [dbo].[D_Device]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Device](
	[ID] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[RoomID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[GroupID] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
	[SubDeviceTypeID] [varchar](100) NOT NULL,
	[SubLogicTypeID] [varchar](100) NOT NULL,
	[FsuID] [varchar](100) NOT NULL,
	[ProtocolID] [varchar](100) NULL,
	[SysName] [varchar](200) NOT NULL,
	[SysCode] [varchar](100) NULL,
	[Model] [varchar](20) NOT NULL,
	[ProdID] [varchar](100) NOT NULL,
	[BrandID] [varchar](100) NOT NULL,
	[SuppID] [varchar](100) NOT NULL,
	[SubCompID] [varchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[ScrapTime] [datetime] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Contact] [varchar](40) NOT NULL,
 CONSTRAINT [PK_D_Device] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_DivSwitElecSour]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_DivSwitElecSour]') AND type in (N'U'))
DROP TABLE [dbo].[D_DivSwitElecSour]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_DivSwitElecSour](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedOutputVolt] [float] NOT NULL,
	[MoniModuleModel] [varchar](20) NOT NULL,
	[ExisRModuleCount] [varchar](20) NOT NULL,
	[RModuleModel] [varchar](20) NOT NULL,
	[RModuleRatedWorkVolt] [int] NOT NULL,
	[SingRModuleRatedOPCap] [varchar](20) NOT NULL,
	[SingGBattGFuseCap] [varchar](20) NOT NULL,
	[BattGFuseGNumber] [int] NOT NULL,
	[OPDistBoardModel] [varchar](20) NOT NULL,
	[OPDistBoardNumber] [int] NOT NULL,
 CONSTRAINT [PK_D_DivSwitElecSour] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_ElecSourCabi]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_ElecSourCabi]') AND type in (N'U'))
DROP TABLE [dbo].[D_ElecSourCabi]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_ElecSourCabi](
	[DeviceID] [varchar](100) NOT NULL,
	[ChangeSwitchCap] [varchar](20) NOT NULL,
	[SwitchingObjectID] [int] NOT NULL,
 CONSTRAINT [PK_D_ElecSourCabi] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_FSU]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_FSU]') AND type in (N'U'))
DROP TABLE [dbo].[D_FSU]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_FSU](
	[DeviceID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Code] [varchar](100) NULL,
	[GroupID] [varchar](100) NULL,
	[VendorID] [varchar](100) NULL,
	[Status] [bit] NOT NULL,
	[IP] [varchar](20) NULL,
	[Port] [int] NULL,
	[UID] [varchar](20) NULL,
	[PWD] [varchar](20) NULL,
	[FtpUID] [varchar](20) NULL,
	[FtpPWD] [varchar](20) NULL,
	[FtpFilePath] [varchar](20) NULL,
	[FtpAuthority] [int] NULL,
	[ChangeTime] [datetime] NOT NULL,
	[LastTime] [datetime] NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_D_FSU] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_GeneratorGroup]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_GeneratorGroup]') AND type in (N'U'))
DROP TABLE [dbo].[D_GeneratorGroup]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_GeneratorGroup](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedWorkVolt] [varchar](20) NOT NULL,
	[RunTypeID] [int] NOT NULL,
	[CoolingTypeID] [int] NOT NULL,
	[RunBattModel] [varchar](40) NOT NULL,
	[RunBattGroupNum] [int] NOT NULL,
	[RunBattCap] [int] NOT NULL,
	[OilBoxVolume] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_GeneratorGroup] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_HighVoltDistBox]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_HighVoltDistBox]') AND type in (N'U'))
DROP TABLE [dbo].[D_HighVoltDistBox]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_HighVoltDistBox](
	[DeviceID] [varchar](100) NOT NULL,
	[SysRatedCap] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_HighVoltDistBox] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Inverter]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Inverter]') AND type in (N'U'))
DROP TABLE [dbo].[D_Inverter]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Inverter](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[InputRatedVolt] [varchar](20) NOT NULL,
	[OutputRatedVolt] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_Inverter] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_LowDistCabinet]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_LowDistCabinet]') AND type in (N'U'))
DROP TABLE [dbo].[D_LowDistCabinet]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_LowDistCabinet](
	[DeviceID] [varchar](100) NOT NULL,
	[SysRatedCapacity] [varchar](30) NOT NULL,
	[SingelCapacity] [varchar](30) NOT NULL,
	[CapGroupNumber] [int] NOT NULL,
	[CapNumber] [int] NOT NULL,
	[OutputPortNumber] [int] NULL,
 CONSTRAINT [PK_D_LowDistCabinet] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Manostat]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Manostat]') AND type in (N'U'))
DROP TABLE [dbo].[D_Manostat]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Manostat](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_Manostat] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_MobiGenerator]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_MobiGenerator]') AND type in (N'U'))
DROP TABLE [dbo].[D_MobiGenerator]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_MobiGenerator](
	[DeviceID] [varchar](100) NOT NULL,
	[SourceID] [int] NOT NULL,
	[OilEngiTypeID] [int] NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedWorkVolt] [varchar](20) NOT NULL,
	[PhasesTypeID] [int] NOT NULL,
 CONSTRAINT [PK_D_MobiGenerator] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_OrdiAirCond]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_OrdiAirCond]') AND type in (N'U'))
DROP TABLE [dbo].[D_OrdiAirCond]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_OrdiAirCond](
	[DeviceID] [varchar](100) NOT NULL,
	[MakeCoolCap] [varchar](20) NOT NULL,
	[MakeHotCap] [varchar](20) NOT NULL,
	[InputRatedPower] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_OrdiAirCond] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_RedefinePoint]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]') AND type in (N'U'))
DROP TABLE [dbo].[D_RedefinePoint]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_RedefinePoint](
	[DeviceID] [varchar](100) NOT NULL,
	[PointID] [varchar](100) NOT NULL,
	[AlarmLevel] [int] NOT NULL,
	[NMAlarmID] [varchar](100) NULL,
	[AlarmLimit] [float] NULL,
	[AlarmReturnDiff] [float] NULL,
	[AlarmDelay] [int] NULL,
	[AlarmRecoveryDelay] [int] NULL,
	[TriggerTypeID] [int] NULL,
	[SavedPeriod] [int] NULL,
	[AbsoluteThreshold] [float] NULL,
	[PerThreshold] [float] NULL,
	[StaticPeriod] [int] NULL,
	[InferiorAlarmStr] [varchar](256) NULL,
	[ConnAlarmStr] [varchar](256) NULL,
	[AlarmFilteringStr] [varchar](256) NULL,
	[AlarmReversalStr] [varchar](256) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Extend] [varchar](max) NULL,
 CONSTRAINT [PK_D_RedefinePoint] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC,
	[PointID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_SolarController]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_SolarController]') AND type in (N'U'))
DROP TABLE [dbo].[D_SolarController]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_SolarController](
	[DeviceID] [varchar](100) NOT NULL,
	[DownHangSunBoardNumber] [int] NOT NULL,
	[OutputRatedCap] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_SolarController] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_SolarEqui]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_SolarEqui]') AND type in (N'U'))
DROP TABLE [dbo].[D_SolarEqui]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_SolarEqui](
	[DeviceID] [varchar](100) NOT NULL,
	[SolarBoardRatedPower] [varchar](20) NOT NULL,
	[OutputVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_SolarEqui] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_SpecAirCond]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_SpecAirCond]') AND type in (N'U'))
DROP TABLE [dbo].[D_SpecAirCond]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_SpecAirCond](
	[DeviceID] [varchar](100) NOT NULL,
	[MakeCoolCap] [varchar](20) NOT NULL,
	[MakeHotCap] [varchar](20) NOT NULL,
	[InputRatedPower] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_SpecAirCond] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_SwitchFuse]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_SwitchFuse]') AND type in (N'U'))
DROP TABLE [dbo].[D_SwitchFuse]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_SwitchFuse](
	[DeviceID] [varchar](100) NOT NULL,
	[TermSeriesID] [int] NOT NULL,
	[ElecModeID] [int] NOT NULL,
	[RatedCap] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
	[OPCableCode] [varchar](20) NULL,
	[OPCableName] [varchar](40) NOT NULL,
	[OPCableModel] [varchar](40) NOT NULL,
	[OPCableCap] [varchar](20) NOT NULL,
	[DownDevName] [varchar](40) NOT NULL,
	[DownDevSFName] [varchar](40) NOT NULL,
 CONSTRAINT [PK_D_SwitchFuse] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Transformer]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Transformer]') AND type in (N'U'))
DROP TABLE [dbo].[D_Transformer]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Transformer](
	[DeviceID] [varchar](100) NOT NULL,
	[TypeID] [int] NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedInputVolt] [varchar](20) NOT NULL,
	[RatedOutputVolt] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_Transformer] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_UPS]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_UPS]') AND type in (N'U'))
DROP TABLE [dbo].[D_UPS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_UPS](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedOutputVolt] [int] NOT NULL,
	[WorkID] [int] NOT NULL,
 CONSTRAINT [PK_D_UPS] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_Ventilation]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Ventilation]') AND type in (N'U'))
DROP TABLE [dbo].[D_Ventilation]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Ventilation](
	[DeviceID] [varchar](100) NOT NULL,
	[RatedPower] [varchar](20) NOT NULL,
	[RatedVolt] [int] NOT NULL,
 CONSTRAINT [PK_D_Ventilation] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_WindEnerEqui]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_WindEnerEqui]') AND type in (N'U'))
DROP TABLE [dbo].[D_WindEnerEqui]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_WindEnerEqui](
	[DeviceID] [varchar](100) NOT NULL,
	[FanRatedPower] [varchar](20) NOT NULL,
	[OutputPowerVolt] [int] NOT NULL,
	[FanTypeID] [int] NOT NULL,
 CONSTRAINT [PK_D_WindEnerEqui] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_WindLightCompCon]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_WindLightCompCon]') AND type in (N'U'))
DROP TABLE [dbo].[D_WindLightCompCon]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_WindLightCompCon](
	[DeviceID] [varchar](100) NOT NULL,
	[DownHangSunBoardNumber] [int] NOT NULL,
	[DownHangFanNumber] [int] NOT NULL,
	[OutputRatedCap] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_WindLightCompCon] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[D_WindPowerCon]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_WindPowerCon]') AND type in (N'U'))
DROP TABLE [dbo].[D_WindPowerCon]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_WindPowerCon](
	[DeviceID] [varchar](100) NOT NULL,
	[DownFanNumber] [varchar](20) NOT NULL,
	[OutputRatedCap] [varchar](20) NOT NULL,
 CONSTRAINT [PK_D_WindPowerCon] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
SET ANSI_PADDING ON
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
--创建表[dbo].[H_Masking]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Masking]') AND type in (N'U'))
DROP TABLE [dbo].[H_Masking]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_Masking](
	[ID] [varchar](256) NOT NULL,
	[Type] [int] NOT NULL,
	[UserID] [varchar](100) NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_H_Masking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_Note]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Note]') AND type in (N'U'))
DROP TABLE [dbo].[H_Note]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_Note](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SysType] [int] NOT NULL,
	[GroupID] [varchar](100) NOT NULL,
	[Name] [text] NOT NULL,
	[DtType] [int] NOT NULL,
	[OpType] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Desc] [text] NULL,
 CONSTRAINT [PK_H_Note] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_OpEvent]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_OpEvent]') AND type in (N'U'))
DROP TABLE [dbo].[H_OpEvent]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[H_OpEvent](
	[ID] [varchar](100) NOT NULL,
	[UserID] [varchar](100) NULL,
	[NodeID] [varchar](100) NULL,
	[NodeTable] [varchar](40) NULL,
	[OpType] [int] NULL,
	[OpTime] [datetime] NULL,
	[Desc] [text] NULL,
 CONSTRAINT [PK_H_OpEvent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[H_Sync]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Sync]') AND type in (N'U'))
DROP TABLE [dbo].[H_Sync]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[H_Sync](
	[ID] [varchar](100) NOT NULL,
	[Name] [text] NOT NULL,
	[Table] [varchar](100) NOT NULL,
	[Field] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_H_Sync] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_Point]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Point]') AND type in (N'U'))
DROP TABLE [dbo].[P_Point]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_Point](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Code] [varchar](100) NULL,
	[Number] [varchar](20) NOT NULL,
	[AlarmID] [varchar](100) NULL,
	[NMAlarmID] [varchar](100) NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[DeviceTypeID] [varchar](100) NOT NULL,
	[LogicTypeID] [varchar](100) NOT NULL,
	[UnitState] [varchar](160) NULL,
	[AlarmTimeDesc] [varchar](40) NULL,
	[NormalTimeDesc] [varchar](40) NULL,
	[DeviceEffect] [varchar](100) NULL,
	[BusiEffect] [varchar](100) NULL,
	[Comment] [varchar](512) NULL,
	[Interpret] [varchar](512) NULL,
	[Extend1] [text] NULL,
	[Extend2] [text] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_P_Point] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_PointsInProtocol]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]') AND type in (N'U'))
DROP TABLE [dbo].[P_PointsInProtocol]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_PointsInProtocol](
	[ProtocolID] [varchar](100) NOT NULL,
	[PointID] [varchar](100) NOT NULL,
	[Desc] [varchar](200) NULL,
 CONSTRAINT [PK_P_PointsInProtocol] PRIMARY KEY CLUSTERED 
(
	[ProtocolID] ASC,
	[PointID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_Protocol]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Protocol]') AND type in (N'U'))
DROP TABLE [dbo].[P_Protocol]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_Protocol](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[SubDeviceTypeID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_P_Protocol] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[P_SubPoint]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_SubPoint]') AND type in (N'U'))
DROP TABLE [dbo].[P_SubPoint]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[P_SubPoint](
	[PointID] [varchar](100) NOT NULL,
	[StaTypeID] [varchar](100) NOT NULL,
	[AlarmLevel] [int] NOT NULL,
	[AlarmLimit] [float] NULL,
	[AlarmReturnDiff] [float] NULL,
	[AlarmDelay] [int] NULL,
	[AlarmRecoveryDelay] [int] NULL,
	[TriggerTypeID] [int] NULL,
	[SavedPeriod] [int] NULL,
	[AbsoluteThreshold] [float] NULL,
	[PerThreshold] [float] NULL,
	[StaticPeriod] [int] NULL,
 CONSTRAINT [PK_P_SubPoint] PRIMARY KEY CLUSTERED 
(
	[PointID] ASC,
	[StaTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[S_Room]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[S_Room]') AND type in (N'U'))
DROP TABLE [dbo].[S_Room]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[S_Room](
	[ID] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[StationID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[GroupID] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
	[Floor] [int] NULL,
	[RoomTypeID] [varchar](100) NOT NULL,
	[PropertyID] [int] NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Length] [varchar](20) NULL,
	[Width] [varchar](20) NULL,
	[Heigth] [varchar](20) NULL,
	[FloorLoad] [varchar](20) NULL,
	[LineHeigth] [varchar](20) NULL,
	[Square] [varchar](20) NULL,
	[EffeSquare] [varchar](20) NULL,
	[FireFighEuip] [varchar](200) NULL,
	[Owner] [varchar](40) NULL,
	[QueryPhone] [varchar](20) NULL,
	[PowerSubMain] [varchar](20) NULL,
	[TranSubMain] [varchar](20) NULL,
	[EnviSubMain] [varchar](20) NULL,
	[FireSubMain] [varchar](20) NULL,
	[AirSubMain] [varchar](20) NULL,
	[Contact] [varchar](40) NOT NULL,
 CONSTRAINT [PK_S_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[S_Station]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[S_Station]') AND type in (N'U'))
DROP TABLE [dbo].[S_Station]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[S_Station](
	[ID] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[AreaID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[GroupID] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
	[StaTypeID] [varchar](100) NOT NULL,
	[Longitude] [varchar](20) NULL,
	[Latitude] [varchar](20) NULL,
	[Altitude] [varchar](20) NULL,
	[CityElecLoad] [varchar](40) NOT NULL,
	[Contact] [varchar](20) NULL,
	[CityElecCap] [varchar](20) NOT NULL,
	[CityElecLoadTypeID] [int] NOT NULL,
	[CityElectNumber] [int] NOT NULL,
	[LineRadiusSize] [varchar](20) NULL,
	[LineLength] [varchar](20) NULL,
	[SuppPowerTypeID] [int] NULL,
	[TranInfo] [varchar](250) NULL,
	[TranContNo] [varchar](40) NULL,
	[TranPhone] [varchar](20) NULL,
 CONSTRAINT [PK_S_Station] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[Sys_Menu]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sys_Menu]') AND type in (N'U'))
DROP TABLE [dbo].[Sys_Menu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sys_Menu](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[ParentID] [varchar](100) NULL,
	[Type] [int] NOT NULL,
	[Name] [varchar](200) NULL,
	[NameImg] [varchar](100) NULL,
	[Url] [varchar](200) NULL,
	[Index] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_Sys_Menu] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Employee]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Employee]') AND type in (N'U'))
DROP TABLE [dbo].[U_Employee]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Employee](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NULL,
	[EngName] [varchar](100) NULL,
	[UsedName] [varchar](200) NULL,
	[EmpNo] [varchar](20) NULL,
	[DeptID] [varchar](100) NOT NULL,
	[DutyID] [varchar](100) NULL,
	[ICardID] [varchar](20) NULL,
	[Sex] [int] NULL,
	[Birthday] [datetime] NULL,
	[Degree] [int] NULL,
	[Marriage] [int] NULL,
	[Nation] [varchar](40) NULL,
	[Provinces] [varchar](40) NULL,
	[Native] [varchar](80) NULL,
	[Address] [varchar](200) NULL,
	[PostalCode] [varchar](6) NULL,
	[AddrPhone] [varchar](20) NULL,
	[WorkPhone] [varchar](20) NULL,
	[MobilePhone] [varchar](20) NULL,
	[Email] [varchar](20) NULL,
	[Photo] [image] NULL,
	[Leaving] [bit] NULL,
	[EntryTime] [datetime] NULL,
	[RetireTime] [datetime] NULL,
	[IsFormal] [bit] NULL,
	[Remarks] [varchar](512) NULL,
 CONSTRAINT [PK_U_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_MenusInRole]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_MenusInRole]') AND type in (N'U'))
DROP TABLE [dbo].[U_MenusInRole]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_MenusInRole](
	[RoleID] [varchar](100) NOT NULL,
	[MenuID] [varchar](100) NOT NULL,
 CONSTRAINT [PK_U_MenusInRole] PRIMARY KEY NONCLUSTERED 
(
	[RoleID] ASC,
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_Role]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Role]') AND type in (N'U'))
DROP TABLE [dbo].[U_Role]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Role](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Authority] [varchar](300) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_U_Role] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[U_User]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_User]') AND type in (N'U'))
DROP TABLE [dbo].[U_User]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_User](
	[ID] [varchar](100) NOT NULL,
	[EmpID] [varchar](100) NULL,
	[Enabled] [bit] NOT NULL,
	[UID] [varchar](100) NOT NULL,
	[PWD] [varchar](128) NOT NULL,
	[PwdFormat] [int] NOT NULL,
	[PwdSalt] [varchar](128) NOT NULL,
	[RoleID] [varchar](100) NOT NULL,
	[OnlineTime] [datetime] NULL,
	[LimitTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[LastPwdChangedTime] [datetime] NULL,
	[FailedPwdAttemptCount] [int] NOT NULL,
	[FailedPwdTime] [datetime] NULL,
	[IsLockedOut] [bit] NOT NULL,
	[LastLockoutTime] [datetime] NULL,
	[Remark] [varchar](512) NULL,
 CONSTRAINT [PK_U_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[C_SubDeviceType]
ALTER TABLE [dbo].[C_SubDeviceType]  WITH CHECK ADD  CONSTRAINT [FK_C_SubDeviceType_C_DeviceType] FOREIGN KEY([DeviceTypeID])
REFERENCES [dbo].[C_DeviceType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubDeviceType_C_DeviceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]'))
ALTER TABLE [dbo].[C_SubDeviceType] CHECK CONSTRAINT [FK_C_SubDeviceType_C_DeviceType]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[C_SubLogicType]
ALTER TABLE [dbo].[C_SubLogicType]  WITH NOCHECK ADD  CONSTRAINT [FK_C_SubLogicType_C_LogicType] FOREIGN KEY([LogicTypeID])
REFERENCES [dbo].[C_LogicType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubLogicType_C_LogicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]'))
ALTER TABLE [dbo].[C_SubLogicType] CHECK CONSTRAINT [FK_C_SubLogicType_C_LogicType]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_ACDistBox]
ALTER TABLE [dbo].[D_ACDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_ACDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ACDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ACDistBox]'))
ALTER TABLE [dbo].[D_ACDistBox] CHECK CONSTRAINT [FK_D_ACDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_AirCondHost]
ALTER TABLE [dbo].[D_AirCondHost]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondHost_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondHost_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondHost]'))
ALTER TABLE [dbo].[D_AirCondHost] CHECK CONSTRAINT [FK_D_AirCondHost_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_AirCondWindCabi]
ALTER TABLE [dbo].[D_AirCondWindCabi]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondWindCabi_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCabi]'))
ALTER TABLE [dbo].[D_AirCondWindCabi] CHECK CONSTRAINT [FK_D_AirCondWindCabi_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_AirCondWindCool]
ALTER TABLE [dbo].[D_AirCondWindCool]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondWindCool_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCool_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCool]'))
ALTER TABLE [dbo].[D_AirCondWindCool] CHECK CONSTRAINT [FK_D_AirCondWindCool_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_BattGroup]
ALTER TABLE [dbo].[D_BattGroup]  WITH CHECK ADD  CONSTRAINT [FK_D_BattGroup_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattGroup]'))
ALTER TABLE [dbo].[D_BattGroup] CHECK CONSTRAINT [FK_D_BattGroup_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_BattTempBox]
ALTER TABLE [dbo].[D_BattTempBox]  WITH CHECK ADD  CONSTRAINT [FK_D_BattTempBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattTempBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattTempBox]'))
ALTER TABLE [dbo].[D_BattTempBox] CHECK CONSTRAINT [FK_D_BattTempBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_ChangeHeat]
ALTER TABLE [dbo].[D_ChangeHeat]  WITH CHECK ADD  CONSTRAINT [FK_D_ChangeHeat_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ChangeHeat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ChangeHeat]'))
ALTER TABLE [dbo].[D_ChangeHeat] CHECK CONSTRAINT [FK_D_ChangeHeat_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_CombSwitElecSour]
ALTER TABLE [dbo].[D_CombSwitElecSour]  WITH CHECK ADD  CONSTRAINT [FK_D_CombSwitElecSour_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_CombSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_CombSwitElecSour]'))
ALTER TABLE [dbo].[D_CombSwitElecSour] CHECK CONSTRAINT [FK_D_CombSwitElecSour_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_ControlEqui]
ALTER TABLE [dbo].[D_ControlEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_ControlEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ControlEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ControlEqui]'))
ALTER TABLE [dbo].[D_ControlEqui] CHECK CONSTRAINT [FK_D_ControlEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_DCDistBox]
ALTER TABLE [dbo].[D_DCDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_DCDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DCDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DCDistBox]'))
ALTER TABLE [dbo].[D_DCDistBox] CHECK CONSTRAINT [FK_D_DCDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_Device]
ALTER TABLE [dbo].[D_Device]  WITH CHECK ADD  CONSTRAINT [FK_D_Device_S_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[S_Room] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Device_S_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Device]'))
ALTER TABLE [dbo].[D_Device] CHECK CONSTRAINT [FK_D_Device_S_Room]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_DivSwitElecSour]
ALTER TABLE [dbo].[D_DivSwitElecSour]  WITH CHECK ADD  CONSTRAINT [FK_D_DivSwitElecSour_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DivSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DivSwitElecSour]'))
ALTER TABLE [dbo].[D_DivSwitElecSour] CHECK CONSTRAINT [FK_D_DivSwitElecSour_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_ElecSourCabi]
ALTER TABLE [dbo].[D_ElecSourCabi]  WITH CHECK ADD  CONSTRAINT [FK_D_ElecSourCabi_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ElecSourCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ElecSourCabi]'))
ALTER TABLE [dbo].[D_ElecSourCabi] CHECK CONSTRAINT [FK_D_ElecSourCabi_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_GeneratorGroup]
ALTER TABLE [dbo].[D_GeneratorGroup]  WITH CHECK ADD  CONSTRAINT [FK_D_GeneratorGroup_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_GeneratorGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_GeneratorGroup]'))
ALTER TABLE [dbo].[D_GeneratorGroup] CHECK CONSTRAINT [FK_D_GeneratorGroup_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_HighVoltDistBox]
ALTER TABLE [dbo].[D_HighVoltDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_HighVoltDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_HighVoltDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_HighVoltDistBox]'))
ALTER TABLE [dbo].[D_HighVoltDistBox] CHECK CONSTRAINT [FK_D_HighVoltDistBox_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_Inverter]
ALTER TABLE [dbo].[D_Inverter]  WITH CHECK ADD  CONSTRAINT [FK_D_Inverter_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Inverter_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Inverter]'))
ALTER TABLE [dbo].[D_Inverter] CHECK CONSTRAINT [FK_D_Inverter_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_LowDistCabinet]
ALTER TABLE [dbo].[D_LowDistCabinet]  WITH CHECK ADD  CONSTRAINT [FK_D_LowDistCabinet_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_LowDistCabinet_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_LowDistCabinet]'))
ALTER TABLE [dbo].[D_LowDistCabinet] CHECK CONSTRAINT [FK_D_LowDistCabinet_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_Manostat]
ALTER TABLE [dbo].[D_Manostat]  WITH CHECK ADD  CONSTRAINT [FK_D_Manostat_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Manostat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Manostat]'))
ALTER TABLE [dbo].[D_Manostat] CHECK CONSTRAINT [FK_D_Manostat_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_MobiGenerator]
ALTER TABLE [dbo].[D_MobiGenerator]  WITH CHECK ADD  CONSTRAINT [FK_D_MobiGenerator_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_MobiGenerator_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_MobiGenerator]'))
ALTER TABLE [dbo].[D_MobiGenerator] CHECK CONSTRAINT [FK_D_MobiGenerator_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_OrdiAirCond]
ALTER TABLE [dbo].[D_OrdiAirCond]  WITH CHECK ADD  CONSTRAINT [FK_D_OrdiAirCond_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_OrdiAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_OrdiAirCond]'))
ALTER TABLE [dbo].[D_OrdiAirCond] CHECK CONSTRAINT [FK_D_OrdiAirCond_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_RedefinePoint]
ALTER TABLE [dbo].[D_RedefinePoint]  WITH CHECK ADD  CONSTRAINT [FK_D_RedefinePoint_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] CHECK CONSTRAINT [FK_D_RedefinePoint_D_Device]
GO

ALTER TABLE [dbo].[D_RedefinePoint]  WITH CHECK ADD  CONSTRAINT [FK_D_RedefinePoint_P_Point] FOREIGN KEY([PointID])
REFERENCES [dbo].[P_Point] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] CHECK CONSTRAINT [FK_D_RedefinePoint_P_Point]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_SolarController]
ALTER TABLE [dbo].[D_SolarController]  WITH CHECK ADD  CONSTRAINT [FK_D_SolarController_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarController_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarController]'))
ALTER TABLE [dbo].[D_SolarController] CHECK CONSTRAINT [FK_D_SolarController_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_SolarEqui]
ALTER TABLE [dbo].[D_SolarEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_SolarEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarEqui]'))
ALTER TABLE [dbo].[D_SolarEqui] CHECK CONSTRAINT [FK_D_SolarEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_SpecAirCond]
ALTER TABLE [dbo].[D_SpecAirCond]  WITH CHECK ADD  CONSTRAINT [FK_D_SpecAirCond_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SpecAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SpecAirCond]'))
ALTER TABLE [dbo].[D_SpecAirCond] CHECK CONSTRAINT [FK_D_SpecAirCond_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_SwitchFuse]
ALTER TABLE [dbo].[D_SwitchFuse]  WITH CHECK ADD  CONSTRAINT [FK_D_SwitchFuse_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SwitchFuse_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SwitchFuse]'))
ALTER TABLE [dbo].[D_SwitchFuse] CHECK CONSTRAINT [FK_D_SwitchFuse_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_Transformer]
ALTER TABLE [dbo].[D_Transformer]  WITH CHECK ADD  CONSTRAINT [FK_D_Transformer_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Transformer_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Transformer]'))
ALTER TABLE [dbo].[D_Transformer] CHECK CONSTRAINT [FK_D_Transformer_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_UPS]
ALTER TABLE [dbo].[D_UPS]  WITH CHECK ADD  CONSTRAINT [FK_D_UPS_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_UPS_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_UPS]'))
ALTER TABLE [dbo].[D_UPS] CHECK CONSTRAINT [FK_D_UPS_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_Ventilation]
ALTER TABLE [dbo].[D_Ventilation]  WITH CHECK ADD  CONSTRAINT [FK_D_Ventilation_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Ventilation_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Ventilation]'))
ALTER TABLE [dbo].[D_Ventilation] CHECK CONSTRAINT [FK_D_Ventilation_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_WindEnerEqui]
ALTER TABLE [dbo].[D_WindEnerEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_WindEnerEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindEnerEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindEnerEqui]'))
ALTER TABLE [dbo].[D_WindEnerEqui] CHECK CONSTRAINT [FK_D_WindEnerEqui_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_WindLightCompCon]
ALTER TABLE [dbo].[D_WindLightCompCon]  WITH CHECK ADD  CONSTRAINT [FK_D_WindLightCompCon_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindLightCompCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindLightCompCon]'))
ALTER TABLE [dbo].[D_WindLightCompCon] CHECK CONSTRAINT [FK_D_WindLightCompCon_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[D_WindPowerCon]
ALTER TABLE [dbo].[D_WindPowerCon]  WITH CHECK ADD  CONSTRAINT [FK_D_WindPowerCon_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindPowerCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindPowerCon]'))
ALTER TABLE [dbo].[D_WindPowerCon] CHECK CONSTRAINT [FK_D_WindPowerCon_D_Device]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[P_PointsInProtocol]
ALTER TABLE [dbo].[P_PointsInProtocol]  WITH CHECK ADD  CONSTRAINT [FK_P_PointsInProtocol_P_Point] FOREIGN KEY([PointID])
REFERENCES [dbo].[P_Point] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] CHECK CONSTRAINT [FK_P_PointsInProtocol_P_Point]
GO

ALTER TABLE [dbo].[P_PointsInProtocol]  WITH CHECK ADD  CONSTRAINT [FK_P_PointsInProtocol_P_Protocol] FOREIGN KEY([ProtocolID])
REFERENCES [dbo].[P_Protocol] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Protocol]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] CHECK CONSTRAINT [FK_P_PointsInProtocol_P_Protocol]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[P_SubPoint]
ALTER TABLE [dbo].[P_SubPoint]  WITH CHECK ADD  CONSTRAINT [FK_P_SubPoint_P_Point] FOREIGN KEY([PointID])
REFERENCES [dbo].[P_Point] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_SubPoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_SubPoint]'))
ALTER TABLE [dbo].[P_SubPoint] CHECK CONSTRAINT [FK_P_SubPoint_P_Point]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[S_Room]
ALTER TABLE [dbo].[S_Room]  WITH CHECK ADD  CONSTRAINT [FK_S_Room_S_Station] FOREIGN KEY([StationID])
REFERENCES [dbo].[S_Station] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Room_S_Station]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Room]'))
ALTER TABLE [dbo].[S_Room] CHECK CONSTRAINT [FK_S_Room_S_Station]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[S_Station]
ALTER TABLE [dbo].[S_Station]  WITH CHECK ADD  CONSTRAINT [FK_S_Station_A_Area] FOREIGN KEY([AreaID])
REFERENCES [dbo].[A_Area] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Station_A_Area]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Station]'))
ALTER TABLE [dbo].[S_Station] CHECK CONSTRAINT [FK_S_Station_A_Area]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[U_MenusInRole]
ALTER TABLE [dbo].[U_MenusInRole]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRole_U_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[U_Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRole_U_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRole]'))
ALTER TABLE [dbo].[U_MenusInRole] CHECK CONSTRAINT [FK_U_MenusInRole_U_Role]
GO