/*
* P2R_V1 Database Script Library v1.2.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/12/08
*/

USE [master]
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'P2R_V1')
CREATE DATABASE [P2R_V1]
GO

USE [P2R_V1]
GO

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
--删除外键[dbo].[D_Signal]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] DROP CONSTRAINT [FK_D_Signal_P_Point]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] DROP CONSTRAINT [FK_D_Signal_D_Device]
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
--删除外键[dbo].[G_Driver]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GDriver_GBus]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Driver]'))
ALTER TABLE [dbo].[G_Driver] DROP CONSTRAINT [FK_GDriver_GBus]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_Authorization]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] DROP CONSTRAINT [FK_M_Authorization_M_Card]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] DROP CONSTRAINT [FK_M_Authorization_G_Driver]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_CardsInEmployee]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_CardsInEmployee_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_CardsInEmployee]'))
ALTER TABLE [dbo].[M_CardsInEmployee] DROP CONSTRAINT [FK_M_CardsInEmployee_M_Card]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--删除外键[dbo].[M_DriversInTime]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_DriversInTime_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DriversInTime]'))
ALTER TABLE [dbo].[M_DriversInTime] DROP CONSTRAINT [FK_M_DriversInTime_G_Driver]
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
--删除外键[dbo].[V_Channel]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_V_Channel_V_Camera]') AND parent_object_id = OBJECT_ID(N'[dbo].[V_Channel]'))
ALTER TABLE [dbo].[V_Channel] DROP CONSTRAINT [FK_V_Channel_V_Camera]
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
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[A_Area](
	[ID] [varchar](100) NOT NULL,
	[Code] [varchar](100) NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ParentID] [varchar](100) NOT NULL,
	[NodeLevel] [int] NOT NULL,
	[Desc] [varchar](512) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[A_Area] ADD [VendorID] [varchar](100) NULL
 CONSTRAINT [PK_A_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
	[Model] [varchar](100) NOT NULL,
	[ProdID] [varchar](100) NOT NULL,
	[BrandID] [varchar](100) NOT NULL,
	[SuppID] [varchar](100) NOT NULL,
	[SubCompID] [varchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[ScrapTime] [datetime] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Contact] [varchar](40) NOT NULL,
	[VendorID] [varchar](100) NULL,
	[Index] [int] NOT NULL CONSTRAINT [DF_D_Device_Index] DEFAULT ((0)),
	[Version] [varchar](100) NULL,
	[DriverID] [varchar](100) NOT NULL CONSTRAINT [DF_D_Device_Driver] DEFAULT ('')
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
	[RoomID] [varchar](100) NOT NULL CONSTRAINT [DF_D_FSU_Room] DEFAULT ((0)),
	[IsFtpSync] [bit] NULL,
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
	[Extend] [varchar](max) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[NMAlarmID] [varchar](100) NULL,
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
--创建表[dbo].[D_Signal]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[D_Signal]') AND type in (N'U'))
DROP TABLE [dbo].[D_Signal]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[D_Signal](
	[DeviceID] [varchar](100) NOT NULL,
	[PointID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[GroupID] [varchar](100) NULL,
	[DriverID] [varchar](100) NULL,
	[DotID] [int] NULL,
	[AlarmThreshold] [int] NULL,
	[PolyNot] [bit] NULL,
	[Mark] [varchar](200) NULL,
	[EmulateValue] [int] NULL,
	[ProtocolID] [varchar](100) NULL,
	[IsBindProtocol] [bit] NULL,
	[AlmSource] [varchar](200) NULL,
	[AlmThreshold1] [float] NULL,
	[AlmTrigger1] [int] NULL,
	[AlmThreshold2] [float] NULL,
	[AlmTrigger2] [int] NULL,
	[Multiple] [float] NULL,
	[Offset] [float] NULL,
	[Percision] [float] NULL,
	[AlmCurve] [float] NULL,
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
	[StorageRefTime] [varchar](40) NULL,
	[InferiorAlarmStr] [varchar](256) NULL,
	[ConnAlarmStr] [varchar](256) NULL,
	[AlarmFilteringStr] [varchar](256) NULL,
	[AlarmReversalStr] [varchar](256) NULL,
	[MaxVal] [float] NULL,
	[MinVal] [float] NULL,
	[Extend] [varchar](max) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_D_Signal] PRIMARY KEY CLUSTERED 
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[G_Bus]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[G_Bus]') AND type in (N'U'))
DROP TABLE [dbo].[G_Bus]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[G_Bus](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[GroupID] [varchar](100) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[Mode] [int] NOT NULL,
	[LinkType] [int] NOT NULL,
	[RemoteIP] [varchar](15) NOT NULL,
	[RemotePort] [int] NOT NULL,
	[LocalIP] [varchar](15) NOT NULL,
	[LocalPort] [int] NOT NULL,
	[SerialParam] [varchar](max) NOT NULL,
	[SwtDriverInterval] [int] NOT NULL,
	[ReConnectInterval] [int] NOT NULL,
	[Heartbeat] [int] NOT NULL,
	[PrKey] [varchar](100) NOT NULL,
	[AuxSet] [varchar](max) NOT NULL,
 CONSTRAINT [PK_GBus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[G_Driver]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[G_Driver]') AND type in (N'U'))
DROP TABLE [dbo].[G_Driver]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[G_Driver](
	[ID] [varchar](100) NOT NULL,
	[BusID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[GroupID] [varchar](100) NOT NULL,
	[FsuID] [varchar](100) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Protocol] [int] NOT NULL,
	[Address] [int] NOT NULL,
	[ComPort] [int] NOT NULL,
	[RtuParam] [varchar](max) NOT NULL,
	[SendInterval] [int] NOT NULL,
	[ReceiveInterval] [int] NOT NULL,
	[AlarmThreshold] [int] NOT NULL,
	[AuxSet] [varchar](max) NOT NULL,
	[ExpSet] [varchar](max) NOT NULL,
	[TID] [int] NOT NULL,
	[Bind] [bit] NOT NULL,
 CONSTRAINT [PK_GDriver] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
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
--创建表[dbo].[H_Masking]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[H_Masking]') AND type in (N'U'))
DROP TABLE [dbo].[H_Masking]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[M_Authorization]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Authorization]') AND type in (N'U'))
DROP TABLE [dbo].[M_Authorization]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Authorization](
	[DriverID] [varchar](100) NOT NULL,
	[CardID] [varchar](100) NOT NULL,
	[HexCode] [varchar](100) NULL,
	[DeviceID] [varchar](100) NULL,
	[GroupID] [varchar](100) NULL,
	[LimitType] [int] NULL,
	[LimitIndex] [int] NULL,
	[BeginTime] [datetime] NULL,
	[LimitTime] [datetime] NULL,
	[Uid] [varchar](100) NULL,
	[Pwd] [varchar](100) NULL,
 CONSTRAINT [PK_M_Authorization] PRIMARY KEY CLUSTERED 
(
	[CardID] ASC,
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Card]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Card]') AND type in (N'U'))
DROP TABLE [dbo].[M_Card]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Card](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NULL,
	[Name] [varchar](200) NOT NULL,
	[Code] [varchar](100) NULL,
	[HexCode] [varchar](100) NULL,
	[UID] [varchar](100) NULL,
	[PWD] [varchar](100) NULL,
	[Type] [int] NULL,
	[Status] [int] NULL,
	[StatusTime] [datetime] NOT NULL,
	[StatusReason] [varchar](512) NULL,
	[BeginTime] [datetime] NOT NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_Card] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_CardsInEmployee]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_CardsInEmployee]') AND type in (N'U'))
DROP TABLE [dbo].[M_CardsInEmployee]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_CardsInEmployee](
	[CardID] [varchar](100) NOT NULL,
	[EmployeeID] [varchar](100) NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_M_CardsInEmployee] PRIMARY KEY NONCLUSTERED 
(
	[CardID] ASC,
	[EmployeeID] ASC,
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_DriversInTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DriversInTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_DriversInTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_DriversInTime](
	[DriverID] [varchar](100) NOT NULL,
	[DeviceID] [varchar](100) NOT NULL,
	[TimeID] [varchar](100) NOT NULL,
	[TimeType] [int] NOT NULL,
 CONSTRAINT [PK_M_DriversInTime] PRIMARY KEY NONCLUSTERED 
(
	[DriverID] ASC,
	[TimeID] ASC,
	[TimeType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_HolidayTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_HolidayTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_HolidayTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_HolidayTime](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TimeStr] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_HolidayTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_InfraredTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_InfraredTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_InfraredTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_InfraredTime](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[StartTimeStr1] [varchar](20) NULL,
	[EndTimeStr1] [varchar](20) NULL,
	[StartTimeStr2] [varchar](20) NULL,
	[EndTimeStr2] [varchar](20) NULL,
	[StartTimeStr3] [varchar](20) NULL,
	[EndTimeStr3] [varchar](20) NULL,
	[StartTimeStr4] [varchar](20) NULL,
	[EndTimeStr4] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_InfraredTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_Sync]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Sync]') AND type in (N'U'))
DROP TABLE [dbo].[M_Sync]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_Sync](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NULL,
	[Type] [int] NULL,
	[GroupID] [varchar](100) NULL,
	[DriverID] [varchar](100) NULL,
	[OpType] [int] NULL,
	[Time] [datetime] NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_M_Sync] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_WeekEndTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_WeekEndTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_WeekEndTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_WeekEndTime](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TimeStr] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_WeekEndTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_WeekTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_WeekTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_WeekTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_WeekTime](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[GroupIndex] [int] NOT NULL,
	[Index] [int] NOT NULL,
	[StartTimeStr1] [varchar](20) NULL,
	[EndTimeStr1] [varchar](20) NULL,
	[StartTimeStr2] [varchar](20) NULL,
	[EndTimeStr2] [varchar](20) NULL,
	[StartTimeStr3] [varchar](20) NULL,
	[EndTimeStr3] [varchar](20) NULL,
	[StartTimeStr4] [varchar](20) NULL,
	[EndTimeStr4] [varchar](20) NULL,
	[StartTimeStr5] [varchar](20) NULL,
	[EndTimeStr5] [varchar](20) NULL,
	[StartTimeStr6] [varchar](20) NULL,
	[EndTimeStr6] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_WeekTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[M_WorkTime]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_WorkTime]') AND type in (N'U'))
DROP TABLE [dbo].[M_WorkTime]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[M_WorkTime](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[Index] [int] NOT NULL,
	[StartTimeStr1] [varchar](20) NULL,
	[EndTimeStr1] [varchar](20) NULL,
	[StartTimeStr2] [varchar](20) NULL,
	[EndTimeStr2] [varchar](20) NULL,
	[StartTimeStr3] [varchar](20) NULL,
	[EndTimeStr3] [varchar](20) NULL,
	[StartTimeStr4] [varchar](20) NULL,
	[EndTimeStr4] [varchar](20) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_M_WorkTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
	[DotID] [int] NULL,
	[AlarmThreshold] [int] NULL,
	[PolyNot] [bit] NULL,
	[Mark] [varchar](200) NULL,
	[EmulateValue] [int] NULL,
	[Multiple] [float] NULL,
	[Offset] [float] NULL,
	[Percision] [float] NULL,
	[AlmCurve] [float] NULL,
	[MaxVal] [float] NULL,
	[MinVal] [float] NULL,
	[AlmSource] [varchar](200) NULL,
	[AlmThreshold1] [float] NULL,
	[AlmTrigger1] [int] NULL,
	[AlmThreshold2] [float] NULL,
	[AlmTrigger2] [int] NULL,
 CONSTRAINT [FK_P_PointsInProtocol] PRIMARY KEY CLUSTERED 
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
SET ANSI_PADDING OFF
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
	[StorageRefTime] [varchar](40) NULL,
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
SET ANSI_PADDING OFF
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
	[Contact] [varchar](40) NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[S_Room] ADD [VendorID] [varchar](100) NULL
 CONSTRAINT [PK_S_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
	[TranPhone] [varchar](20) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[S_Station] ADD [VendorID] [varchar](100) NULL
 CONSTRAINT [PK_S_Station] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[U_Client]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_Client]') AND type in (N'U'))
DROP TABLE [dbo].[U_Client]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[U_Client](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NULL,
	[Name] [varchar](200) NULL,
	[UID] [varchar](20) NULL,
	[PWD] [varchar](20) NULL,
	[Level] [int] NULL,
	[Ver] [varchar](100) NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_U_Client] PRIMARY KEY CLUSTERED 
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[U_OutEmployee]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_OutEmployee]') AND type in (N'U'))
DROP TABLE [dbo].[U_OutEmployee]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[U_OutEmployee](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[EmpID] [varchar](100) NOT NULL,
	[Photo] [image] NULL,
	[Sex] [int] NOT NULL,
	[ICardID] [varchar](50) NULL,
	[ICardAddress] [varchar](200) NULL,
	[ICardIssue] [varchar](200) NULL,
	[Address] [varchar](200) NULL,
	[CompanyName] [varchar](200) NULL,
	[ProjectName] [varchar](200) NULL,
	[WorkPhone] [varchar](20) NULL,
	[MobilePhone] [varchar](20) NULL,
	[Email] [varchar](20) NULL,
	[Remarks] [varchar](512) NULL,
 CONSTRAINT [PK_U_OutEmployee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
SET ANSI_PADDING OFF
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
SET ANSI_PADDING OFF
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
--创建表[dbo].[V_Camera]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Camera]') AND type in (N'U'))
DROP TABLE [dbo].[V_Camera]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Camera](
	[ID] [varchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[DeviceID] [varchar](100) NULL,
	[Name] [varchar](200) NULL,
	[IP] [varchar](20) NULL,
	[Port] [int] NULL,
	[UID] [varchar](40) NULL,
	[PWD] [varchar](20) NULL,
	[Bright] [int] NULL,
	[Contrast] [int] NULL,
	[Saturation] [int] NULL,
	[Hue] [int] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_V_Camera] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Channel]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Channel]') AND type in (N'U'))
DROP TABLE [dbo].[V_Channel]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[V_Channel](
	[ID] [varchar](100) NOT NULL,
	[CameraID] [varchar](100) NULL,
	[Name] [varchar](200) NULL,
	[Mask] [int] NULL,
	[Index] [int] NULL,
	[Zero] [bit] NULL,
	[Desc] [varchar](512) NULL,
 CONSTRAINT [PK_V_Channel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建表[dbo].[V_Preset]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[V_Preset]') AND type in (N'U'))
DROP TABLE [dbo].[V_Preset]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[V_Preset](
	[ID] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NULL,
	[Index] [int] NULL,
 CONSTRAINT [PK_V_Preset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建视图[dbo].[V_Point]
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_Point]'))
DROP VIEW [dbo].[V_Point]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_Point]
AS
SELECT P.*, SP.* FROM [dbo].[P_Point] P INNER JOIN [dbo].[P_SubPoint] SP ON P.[ID] = SP.[PointID];
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--创建存储过程[dbo].[PI_H_OpEvent]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PI_H_OpEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PI_H_OpEvent]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		Donghua.li
-- Create date: 2017.08.20
-- Description:	OpEvent Tables
-- =============================================
CREATE PROCEDURE [dbo].[PI_H_OpEvent] 
    @ID varchar(100),
    @UserID varchar(100),
	@NodeID varchar(100),
	@NodeTable varchar(40),
	@OpType int,
	@OpTime datetime,
	@Desc varchar(512) = NULL
AS
BEGIN
	DECLARE @NAME varchar(40);
	DECLARE @SQL nvarchar(1000);
	DECLARE @ParmDefinition nvarchar(1000);
    SET NOCOUNT ON;
    SELECT @NAME=MONTH(@OpTime);
	IF LEN(@NAME)=1 
	    SELECT @NAME='H_OpEvent'+CAST(YEAR(@OpTime) AS CHAR(4))+'0'+@NAME;
	ELSE 
        SELECT @NAME='H_OpEvent'+CAST(YEAR(@OpTime) AS CHAR(4))+@NAME;
	IF NOT EXISTS(SELECT NAME FROM SYSOBJECTS WHERE XTYPE = 'U' AND NAME = @NAME)
	BEGIN
	SELECT @SQL='CREATE TABLE [dbo].['+@NAME+'](
	  	         [ID] [varchar](100) NOT NULL,
			     [UserID] [varchar](100) NULL,
				 [NodeID] [varchar](100) NULL,
				 [NodeTable] [varchar](40) NULL,
				 [OpType] [int] NULL,
			     [OpTime] [datetime] NULL,
			     [Desc] [varchar](512) NULL,
			 CONSTRAINT [PK_'+@NAME+'] PRIMARY KEY CLUSTERED 
			(
			[ID] ASC
			)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
			) ON [PRIMARY]'
		EXEC(@SQL)
	END
    SELECT @SQL=N'INSERT INTO '+@NAME+N' VALUES(
    @ID1,
    @UserID1,
	@NodeID1,
	@NodeTable1,
	@OpType1,
	@OpTime1,
	@Desc1)';
    
    SELECT @ParmDefinition=N'
    @ID1 varchar(100),
    @UserID1 varchar(100),
	@NodeID1 varchar(100),
	@NodeTable1 varchar(40),
	@OpType1 int,
	@OpTime1 datetime,
	@Desc1 varchar(160)';

	EXECUTE sp_executesql @SQL,@ParmDefinition,
	@ID1 = @ID,
    @UserID1 = @UserID ,
	@NodeID1 = @NodeID ,
	@NodeTable1 = @NodeTable ,
	@OpType1 = @OpType,
	@OpTime1 = OpTime,
	@Desc1 = @Desc;
 
	SET NOCOUNT ON;
	INSERT INTO [dbo].[H_OpEvent] (ID,UserID,NodeID,NodeTable,OpType,OpTime,[Desc]) VALUES ( @ID,@UserID,@NodeID,@NodeTable ,@OpType,@OpTime,@desc);
END
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[C_SubDeviceType]
ALTER TABLE [dbo].[C_SubDeviceType]  WITH NOCHECK ADD  CONSTRAINT [FK_C_SubDeviceType_C_DeviceType] FOREIGN KEY([DeviceTypeID])
REFERENCES [dbo].[C_DeviceType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubDeviceType_C_DeviceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]'))
ALTER TABLE [dbo].[C_SubDeviceType] CHECK CONSTRAINT [FK_C_SubDeviceType_C_DeviceType]

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
--添加外键[dbo].[D_Signal]
ALTER TABLE [dbo].[D_Signal]  WITH CHECK ADD  CONSTRAINT [FK_D_Signal_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] CHECK CONSTRAINT [FK_D_Signal_D_Device]
GO

ALTER TABLE [dbo].[D_Signal]  WITH CHECK ADD  CONSTRAINT [FK_D_Signal_P_Point] FOREIGN KEY([PointID])
REFERENCES [dbo].[P_Point] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] CHECK CONSTRAINT [FK_D_Signal_P_Point]
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
--添加外键[dbo].[G_Driver]
ALTER TABLE [dbo].[G_Driver]  WITH CHECK ADD  CONSTRAINT [FK_GDriver_GBus] FOREIGN KEY([BusID])
REFERENCES [dbo].[G_Bus] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GDriver_GBus]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Driver]'))
ALTER TABLE [dbo].[G_Driver] CHECK CONSTRAINT [FK_GDriver_GBus]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_Authorization]
ALTER TABLE [dbo].[M_Authorization]  WITH CHECK ADD  CONSTRAINT [FK_M_Authorization_G_Driver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[G_Driver] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] CHECK CONSTRAINT [FK_M_Authorization_G_Driver]
GO

ALTER TABLE [dbo].[M_Authorization]  WITH CHECK ADD  CONSTRAINT [FK_M_Authorization_M_Card] FOREIGN KEY([CardID])
REFERENCES [dbo].[M_Card] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] CHECK CONSTRAINT [FK_M_Authorization_M_Card]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_CardsInEmployee]
ALTER TABLE [dbo].[M_CardsInEmployee]  WITH CHECK ADD  CONSTRAINT [FK_M_CardsInEmployee_M_Card] FOREIGN KEY([CardID])
REFERENCES [dbo].[M_Card] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_CardsInEmployee_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_CardsInEmployee]'))
ALTER TABLE [dbo].[M_CardsInEmployee] CHECK CONSTRAINT [FK_M_CardsInEmployee_M_Card]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[M_DriversInTime]
ALTER TABLE [dbo].[M_DriversInTime]  WITH CHECK ADD  CONSTRAINT [FK_M_DriversInTime_G_Driver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[G_Driver] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_DriversInTime_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DriversInTime]'))
ALTER TABLE [dbo].[M_DriversInTime] CHECK CONSTRAINT [FK_M_DriversInTime_G_Driver]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].
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
ALTER TABLE [dbo].[P_SubPoint]  WITH NOCHECK ADD  CONSTRAINT [FK_P_SubPoint_P_Point] FOREIGN KEY([PointID])
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

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加外键[dbo].[V_Channel]
ALTER TABLE [dbo].[V_Channel]  WITH CHECK ADD  CONSTRAINT [FK_V_Channel_V_Camera] FOREIGN KEY([CameraID])
REFERENCES [dbo].[V_Camera] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_V_Channel_V_Camera]') AND parent_object_id = OBJECT_ID(N'[dbo].[V_Channel]'))
ALTER TABLE [dbo].[V_Channel] CHECK CONSTRAINT [FK_V_Channel_V_Camera]
GO

/*
* P2R_V1 Data Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
*/

USE [P2R_V1]
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_AreaType]
DELETE FROM [dbo].[C_AreaType]
GO

INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北京市','110000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','110100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东城区','110101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西城区','110102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朝阳区','110105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰台区','110106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石景山区','110107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海淀区','110108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('门头沟区','110109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('房山区','110111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通州区','110112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺义区','110113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌平区','110114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大兴区','110115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀柔区','110116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平谷区','110117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('县','110200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('密云县','110228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延庆县','110229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天津市','120000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','120100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和平区','120101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河东区','120102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河西区','120103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南开区','120104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河北区','120105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红桥区','120106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东丽区','120110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西青区','120111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('津南区','120112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北辰区','120113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武清区','120114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝坻区','120115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨海新区','120116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('县','120200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁河县','120221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('静海县','120223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓟县','120225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河北省','130000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石家庄市','130100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长安区','130102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥东区','130103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥西区','130104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新华区','130105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('井陉矿区','130107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('裕华区','130108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('井陉县','130121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正定县','130123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('栾城县','130124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('行唐县','130125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵寿县','130126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高邑县','130127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('深泽县','130128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赞皇县','130129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('无极县','130130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平山县','130131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元氏县','130132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赵县','130133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辛集市','130181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('藁城市','130182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋州市','130183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新乐市','130184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹿泉市','130185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('唐山市','130200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('路南区','130202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('路北区','130203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古冶区','130204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开平区','130205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰南区','130207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰润区','130208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曹妃甸区','130209');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滦县','130223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滦南县','130224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐亭县','130225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迁西县','130227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉田县','130229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遵化市','130281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迁安市','130283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秦皇岛市','130300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海港区','130302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山海关区','130303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北戴河区','130304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青龙满族自治县','130321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌黎县','130322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚宁县','130323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卢龙县','130324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邯郸市','130400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邯山区','130402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丛台区','130403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('复兴区','130404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峰峰矿区','130406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邯郸县','130421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临漳县','130423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('成安县','130424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大名县','130425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涉县','130426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('磁县','130427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肥乡县','130428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永年县','130429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邱县','130430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鸡泽县','130431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广平县','130432');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('馆陶县','130433');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('魏县','130434');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲周县','130435');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武安市','130481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邢台市','130500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥东区','130502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥西区','130503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邢台县','130521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临城县','130522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('内丘县','130523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柏乡县','130524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆尧县','130525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('任县','130526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南和县','130527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁晋县','130528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巨鹿县','130529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新河县','130530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广宗县','130531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平乡县','130532');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('威县','130533');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清河县','130534');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临西县','130535');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南宫市','130581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙河市','130582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保定市','130600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新市区','130602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北市区','130603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南市区','130604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('满城县','130621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清苑县','130622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涞水县','130623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜平县','130624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徐水县','130625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定兴县','130626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('唐县','130627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高阳县','130628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('容城县','130629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涞源县','130630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望都县','130631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安新县','130632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('易县','130633');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲阳县','130634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蠡县','130635');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺平县','130636');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博野县','130637');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雄县','130638');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涿州市','130681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定州市','130682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安国市','130683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高碑店市','130684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张家口市','130700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥东区','130702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桥西区','130703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣化区','130705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('下花园区','130706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣化县','130721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张北县','130722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康保县','130723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沽源县','130724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尚义县','130725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蔚县','130726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳原县','130727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀安县','130728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万全县','130729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀来县','130730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涿鹿县','130731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赤城县','130732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇礼县','130733');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('承德市','130800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双桥区','130802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双滦区','130803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹰手营子矿区','130804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('承德县','130821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴隆县','130822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平泉县','130823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滦平县','130824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆化县','130825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰宁满族自治县','130826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宽城满族自治县','130827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('围场满族蒙古族自治县','130828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沧州市','130900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','130901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新华区','130902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('运河区','130903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沧县','130921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青县','130922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东光县','130923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海兴县','130924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐山县','130925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肃宁县','130926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南皮县','130927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴桥县','130928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('献县','130929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孟村回族自治县','130930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泊头市','130981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('任丘市','130982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄骅市','130983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河间市','130984');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('廊坊市','131000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','131001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安次区','131002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广阳区','131003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('固安县','131022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永清县','131023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('香河县','131024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大城县','131025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文安县','131026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大厂回族自治县','131028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霸州市','131081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三河市','131082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡水市','131100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','131101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桃城区','131102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('枣强县','131121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武邑县','131122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武强县','131123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('饶阳县','131124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安平县','131125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('故城县','131126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景县','131127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜城县','131128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('冀州市','131181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('深州市','131182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山西省','140000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太原市','140100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('小店区','140105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迎泽区','140106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杏花岭区','140107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尖草坪区','140108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万柏林区','140109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋源区','140110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清徐县','140121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳曲县','140122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('娄烦县','140123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古交市','140181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大同市','140200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城区','140202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('矿区','140203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南郊区','140211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新荣区','140212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳高县','140221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天镇县','140222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广灵县','140223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵丘县','140224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浑源县','140225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('左云县','140226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大同县','140227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳泉市','140300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城区','140302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('矿区','140303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郊区','140311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平定县','140321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盂县','140322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长治市','140400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城区','140402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郊区','140411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长治县','140421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄垣县','140423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屯留县','140424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平顺县','140425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黎城县','140426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('壶关县','140427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长子县','140428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武乡县','140429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沁县','140430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沁源县','140431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潞城市','140481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋城市','140500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋城市市辖区','140501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城区','140502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沁水县','140521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳城县','140522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陵川县','140524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泽州县','140525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高平市','140581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朔州市','140600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朔城区','140602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平鲁区','140603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山阴县','140621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('应县','140622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('右玉县','140623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀仁县','140624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋中市','140700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆次区','140702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆社县','140721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('左权县','140722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和顺县','140723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昔阳县','140724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寿阳县','140725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太谷县','140726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祁县','140727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平遥县','140728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵石县','140729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('介休市','140781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('运城市','140800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐湖区','140802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临猗县','140821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万荣县','140822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('闻喜县','140823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('稷山县','140824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新绛县','140825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绛县','140826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('垣曲县','140827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夏县','140828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平陆县','140829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芮城县','140830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永济市','140881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河津市','140882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('忻州市','140900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','140901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('忻府区','140902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定襄县','140921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五台县','140922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('代县','140923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('繁峙县','140924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁武县','140925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('静乐县','140926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('神池县','140927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五寨县','140928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岢岚县','140929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河曲县','140930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保德县','140931');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('偏关县','140932');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('原平市','140981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临汾市','141000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','141001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尧都区','141002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲沃县','141021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翼城县','141022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄汾县','141023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪洞县','141024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古县','141025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安泽县','141026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浮山县','141027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉县','141028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乡宁县','141029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大宁县','141030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隰县','141031');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永和县','141032');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒲县','141033');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汾西县','141034');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('侯马市','141081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霍州市','141082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吕梁市','141100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','141101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('离石区','141102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文水县','141121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('交城县','141122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴县','141123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临县','141124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳林县','141125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石楼县','141126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岚县','141127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('方山县','141128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中阳县','141129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('交口县','141130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孝义市','141181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汾阳市','141182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('内蒙古自治区','150000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呼和浩特市','150100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新城区','150102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('回民区','150103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉泉区','150104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赛罕区','150105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('土默特左旗','150121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('托克托县','150122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和林格尔县','150123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清水河县','150124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武川县','150125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('包头市','150200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东河区','150202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昆都仑区','150203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青山区','150204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石拐区','150205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白云鄂博矿区','150206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九原区','150207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('土默特右旗','150221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('固阳县','150222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达尔罕茂明安联合旗','150223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌海市','150300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海勃湾区','150302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海南区','150303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌达区','150304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赤峰市','150400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红山区','150402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元宝山区','150403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松山区','150404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿鲁科尔沁旗','150421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴林左旗','150422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴林右旗','150423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林西县','150424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克什克腾旗','150425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翁牛特旗','150426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('喀喇沁旗','150428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁城县','150429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('敖汉旗','150430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通辽市','150500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('科尔沁区','150502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('科尔沁左翼中旗','150521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('科尔沁左翼后旗','150522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开鲁县','150523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('库伦旗','150524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奈曼旗','150525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扎鲁特旗','150526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霍林郭勒市','150581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂尔多斯市','150600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东胜区','150602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达拉特旗','150621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('准格尔旗','150622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂托克前旗','150623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂托克旗','150624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杭锦旗','150625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌审旗','150626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊金霍洛旗','150627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呼伦贝尔市','150700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海拉尔区','150702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿荣旗','150721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莫力达瓦达斡尔族自治旗','150722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂伦春自治旗','150723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂温克族自治旗','150724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陈巴尔虎旗','150725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新巴尔虎左旗','150726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新巴尔虎右旗','150727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('满洲里市','150781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牙克石市','150782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扎兰屯市','150783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('额尔古纳市','150784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('根河市','150785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴彦淖尔市','150800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临河区','150802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五原县','150821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('磴口县','150822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌拉特前旗','150823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌拉特中旗','150824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌拉特后旗','150825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杭锦后旗','150826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌兰察布市','150900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','150901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('集宁区','150902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卓资县','150921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('化德县','150922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商都县','150923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴和县','150924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凉城县','150925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察哈尔右翼前旗','150926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察哈尔右翼中旗','150927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察哈尔右翼后旗','150928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四子王旗','150929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰镇市','150981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴安盟','152200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌兰浩特市','152201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿尔山市','152202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('科尔沁右翼前旗','152221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('科尔沁右翼中旗','152222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扎赉特旗','152223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('突泉县','152224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锡林郭勒盟','152500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('二连浩特市','152501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锡林浩特市','152502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿巴嘎旗','152522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苏尼特左旗','152523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苏尼特右旗','152524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东乌珠穆沁旗','152525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西乌珠穆沁旗','152526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太仆寺旗','152527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镶黄旗','152528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正镶白旗','152529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正蓝旗','152530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('多伦县','152531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿拉善盟','152900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿拉善左旗','152921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿拉善右旗','152922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('额济纳旗','152923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辽宁省','210000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沈阳市','210100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和平区','210102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沈河区','210103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大东区','210104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('皇姑区','210105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁西区','210106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苏家屯区','210111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东陵区','210112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沈北新区','210113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('于洪区','210114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辽中县','210122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康平县','210123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('法库县','210124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新民市','210181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大连市','210200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中山区','210202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西岗区','210203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙河口区','210204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘井子区','210211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旅顺口区','210212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金州区','210213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长海县','210224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瓦房店市','210281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普兰店市','210282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庄河市','210283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鞍山市','210300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁东区','210302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁西区','210303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('立山区','210304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('千山区','210311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台安县','210321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岫岩满族自治县','210323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海城市','210381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚顺市','210400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新抚区','210402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东洲区','210403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望花区','210404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺城区','210411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚顺县','210421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新宾满族自治县','210422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清原满族自治县','210423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('本溪市','210500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平山区','210502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('溪湖区','210503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('明山区','210504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南芬区','210505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('本溪满族自治县','210521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桓仁满族自治县','210522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹东市','210600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元宝区','210602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('振兴区','210603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('振安区','210604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宽甸满族自治县','210624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东港市','210681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤城市','210682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锦州市','210700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古塔区','210702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凌河区','210703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太和区','210711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黑山县','210726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('义县','210727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凌海市','210781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北镇市','210782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('营口市','210800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('站前区','210802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西市区','210803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鲅鱼圈区','210804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('老边区','210811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盖州市','210881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大石桥市','210882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜新市','210900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','210901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海州区','210902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新邱区','210903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太平区','210904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清河门区','210905');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('细河区','210911');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜新蒙古族自治县','210921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彰武县','210922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辽阳市','211000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','211001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白塔区','211002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文圣区','211003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宏伟区','211004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('弓长岭区','211005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太子河区','211011');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辽阳县','211021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灯塔市','211081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盘锦市','211100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','211101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双台子区','211102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴隆台区','211103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大洼县','211121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盘山县','211122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁岭市','211200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','211201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('银州区','211202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清河区','211204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁岭县','211221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西丰县','211223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌图县','211224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('调兵山市','211281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开原市','211282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朝阳市','211300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','211301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双塔区','211302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙城区','211303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朝阳县','211321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建平县','211322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('喀喇沁左翼蒙古族自治县','211324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北票市','211381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凌源市','211382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('葫芦岛市','211400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','211401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连山区','211402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙港区','211403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南票区','211404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥中县','211421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建昌县','211422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴城市','211481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉林省','220000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长春市','220100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南关区','220102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宽城区','220103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朝阳区','220104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('二道区','220105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绿园区','220106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双阳区','220112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('农安县','220122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九台市','220181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆树市','220182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德惠市','220183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉林市','220200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌邑区','220202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙潭区','220203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('船营区','220204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰满区','220211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永吉县','220221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蛟河市','220281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桦甸市','220282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舒兰市','220283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('磐石市','220284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四平市','220300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁西区','220302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁东区','220303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梨树县','220322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊通满族自治县','220323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('公主岭市','220381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双辽市','220382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辽源市','220400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙山区','220402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西安区','220403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东丰县','220421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东辽县','220422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通化市','220500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东昌区','220502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('二道江区','220503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通化县','220521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辉南县','220523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳河县','220524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅河口市','220581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('集安市','220582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白山市','220600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浑江区','220602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江源区','220605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚松县','220621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖宇县','220622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长白朝鲜族自治县','220623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临江市','220681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松原市','220700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁江区','220702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('前郭尔罗斯蒙古族自治县','220721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长岭县','220722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乾安县','220723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扶余县','220724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白城市','220800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','220801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洮北区','220802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇赉县','220821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通榆县','220822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洮南市','220881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大安市','220882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延边朝鲜族自治州','222400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延吉市','222401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('图们市','222402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('敦化市','222403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('珲春市','222404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙井市','222405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和龙市','222406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汪清县','222424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安图县','222426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黑龙江省','230000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('哈尔滨市','230100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('道里区','230102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南岗区','230103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('道外区','230104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平房区','230108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松北区','230109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('香坊区','230110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呼兰区','230111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿城区','230112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('依兰县','230123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('方正县','230124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宾县','230125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴彦县','230126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('木兰县','230127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通河县','230128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延寿县','230129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双城市','230182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尚志市','230183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五常市','230184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('齐齐哈尔市','230200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙沙区','230202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建华区','230203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁锋区','230204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昂昂溪区','230205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富拉尔基区','230206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('碾子山区','230207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅里斯达斡尔族区','230208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙江县','230221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('依安县','230223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰来县','230224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘南县','230225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富裕县','230227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克山县','230229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克东县','230230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('拜泉县','230231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('讷河市','230281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鸡西市','230300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鸡冠区','230302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('恒山区','230303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滴道区','230304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梨树区','230305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城子河区','230306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻山区','230307');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鸡东县','230321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('虎林市','230381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('密山市','230382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤岗市','230400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('向阳区','230402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('工农区','230403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南山区','230404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴安区','230405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东山区','230406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴山区','230407');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萝北县','230421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥滨县','230422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双鸭山市','230500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尖山区','230502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岭东区','230503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四方台区','230505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝山区','230506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('集贤县','230521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('友谊县','230522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝清县','230523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('饶河县','230524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大庆市','230600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萨尔图区','230602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙凤区','230603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('让胡路区','230604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红岗区','230605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大同区','230606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肇州县','230621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肇源县','230622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林甸县','230623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杜尔伯特蒙古族自治县','230624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊春市','230700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊春区','230702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南岔区','230703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('友好区','230704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西林区','230705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翠峦区','230706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新青区','230707');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('美溪区','230708');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金山屯区','230709');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五营区','230710');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌马河区','230711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汤旺河区','230712');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('带岭区','230713');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌伊岭区','230714');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红星区','230715');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上甘岭区','230716');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉荫县','230722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁力市','230781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('佳木斯市','230800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('向阳区','230803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('前进区','230804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东风区','230805');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郊区','230811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桦南县','230822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桦川县','230826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汤原县','230828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚远县','230833');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('同江市','230881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富锦市','230882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('七台河市','230900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','230901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新兴区','230902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桃山区','230903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茄子河区','230904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('勃利县','230921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牡丹江市','231000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','231001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东安区','231002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳明区','231003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('爱民区','231004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西安区','231005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东宁县','231024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林口县','231025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥芬河市','231081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海林市','231083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁安市','231084');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('穆棱市','231085');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黑河市','231100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','231101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('爱辉区','231102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嫩江县','231121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('逊克县','231123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孙吴县','231124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北安市','231181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五大连池市','231182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥化市','231200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','231201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北林区','231202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望奎县','231221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰西县','231222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青冈县','231223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庆安县','231224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('明水县','231225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥棱县','231226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安达市','231281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肇东市','231282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海伦市','231283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大兴安岭地区','232700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呼玛县','232721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('塔河县','232722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漠河县','232723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上海市','310000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','310100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄浦区','310101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徐汇区','310104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长宁区','310105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('静安区','310106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普陀区','310107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('闸北区','310108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('虹口区','310109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杨浦区','310110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('闵行区','310112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝山区','310113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉定区','310114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浦东新区','310115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金山区','310116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松江区','310117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青浦区','310118');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奉贤区','310120');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('县','310200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇明县','310230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江苏省','320000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南京市','320100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玄武区','320102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白下区','320103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秦淮区','320104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建邺区','320105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼓楼区','320106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('下关区','320107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浦口区','320111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('栖霞区','320113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雨花台区','320114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江宁区','320115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('六合区','320116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('溧水县','320124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高淳县','320125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('无锡市','320200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇安区','320202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南长区','320203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北塘区','320204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锡山区','320205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠山区','320206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨湖区','320211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江阴市','320281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜兴市','320282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徐州市','320300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼓楼区','320302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云龙区','320303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贾汪区','320305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泉山区','320311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜山区','320312');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰县','320321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沛县','320322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('睢宁县','320324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新沂市','320381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邳州市','320382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('常州市','320400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天宁区','320402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钟楼区','320404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('戚墅堰区','320405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新北区','320411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武进区','320412');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('溧阳市','320481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金坛市','320482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苏州市','320500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('虎丘区','320505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴中区','320506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('相城区','320507');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('姑苏区','320508');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴江区','320509');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('常熟市','320581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张家港市','320582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昆山市','320583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太仓市','320585');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南通市','320600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇川区','320602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('港闸区','320611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通州区','320612');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海安县','320621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('如东县','320623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('启东市','320681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('如皋市','320682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海门市','320684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连云港市','320700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连云区','320703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新浦区','320705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海州区','320706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赣榆县','320721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东海县','320722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灌云县','320723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灌南县','320724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮安市','320800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清河区','320802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮安区','320803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮阴区','320804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清浦区','320811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涟水县','320826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪泽县','320829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盱眙县','320830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金湖县','320831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐城市','320900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','320901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('亭湖区','320902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐都区','320903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('响水县','320921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨海县','320922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜宁县','320923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('射阳县','320924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建湖县','320925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东台市','320981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大丰市','320982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扬州市','321000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','321001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广陵区','321002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邗江区','321003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江都区','321012');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝应县','321023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仪征市','321081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高邮市','321084');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇江市','321100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','321101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('京口区','321102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('润州区','321111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹徒区','321112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹阳市','321181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扬中市','321182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('句容市','321183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰州市','321200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','321201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海陵区','321202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高港区','321203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴化市','321281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖江市','321282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰兴市','321283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('姜堰市','321284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宿迁市','321300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','321301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宿城区','321302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宿豫区','321311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沭阳县','321322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泗阳县','321323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泗洪县','321324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浙江省','330000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杭州市','330100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上城区','330102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('下城区','330103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江干区','330104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('拱墅区','330105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西湖区','330106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨江区','330108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萧山区','330109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('余杭区','330110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桐庐县','330122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淳安县','330127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建德市','330182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富阳市','330183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临安市','330185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁波市','330200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海曙区','330203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江东区','330204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江北区','330205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北仑区','330206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇海区','330211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄞州区','330212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('象山县','330225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁海县','330226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('余姚市','330281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('慈溪市','330282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奉化市','330283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温州市','330300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹿城区','330302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙湾区','330303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瓯海区','330304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洞头县','330322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永嘉县','330324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平阳县','330326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苍南县','330327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文成县','330328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰顺县','330329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瑞安市','330381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐清市','330382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉兴市','330400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南湖区','330402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秀洲区','330411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉善县','330421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海盐县','330424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海宁市','330481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平湖市','330482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桐乡市','330483');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖州市','330500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴兴区','330502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南浔区','330503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德清县','330521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长兴县','330522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安吉县','330523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绍兴市','330600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('越城区','330602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绍兴县','330621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新昌县','330624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('诸暨市','330681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上虞市','330682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嵊州市','330683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金华市','330700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('婺城区','330702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金东区','330703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武义县','330723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浦江县','330726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('磐安县','330727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰溪市','330781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('义乌市','330782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东阳市','330783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永康市','330784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衢州市','330800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柯城区','330802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衢江区','330803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('常山县','330822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开化县','330824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙游县','330825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江山市','330881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舟山市','330900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','330901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定海区','330902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普陀区','330903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岱山县','330921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嵊泗县','330922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台州市','331000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','331001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('椒江区','331002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄岩区','331003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('路桥区','331004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉环县','331021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三门县','331022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天台县','331023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仙居县','331024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温岭市','331081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临海市','331082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丽水市','331100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','331101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莲都区','331102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青田县','331121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('缙云县','331122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遂昌县','331123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松阳县','331124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云和县','331125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庆元县','331126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景宁畲族自治县','331127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙泉市','331181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安徽省','340000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合肥市','340100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瑶海区','340102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庐阳区','340103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蜀山区','340104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('包河区','340111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长丰县','340121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肥东县','340122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肥西县','340123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庐江县','340124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巢湖市','340181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芜湖市','340200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镜湖区','340202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('弋江区','340203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鸠江区','340207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三山区','340208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芜湖县','340221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('繁昌县','340222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南陵县','340223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('无为县','340225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蚌埠市','340300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙子湖区','340302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蚌山区','340303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禹会区','340304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮上区','340311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀远县','340321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五河县','340322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('固镇县','340323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮南市','340400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大通区','340402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('田家庵区','340403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('谢家集区','340404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('八公山区','340405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潘集区','340406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤台县','340421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马鞍山市','340500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('花山区','340503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雨山区','340504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博望区','340506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('当涂县','340521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('含山县','340522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和县','340523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮北市','340600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杜集区','340602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('相山区','340603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('烈山区','340604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('濉溪县','340621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜陵市','340700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜官山区','340702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('狮子山区','340703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郊区','340711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜陵县','340721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安庆市','340800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','340801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迎江区','340802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大观区','340803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜秀区','340811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀宁县','340822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('枞阳县','340823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潜山县','340824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太湖县','340825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宿松县','340826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望江县','340827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳西县','340828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桐城市','340881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄山市','341000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屯溪区','341002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄山区','341003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徽州区','341004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('歙县','341021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('休宁县','341022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黟县','341023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祁门县','341024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滁州市','341100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('琅琊区','341102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南谯区','341103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('来安县','341122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('全椒县','341124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定远县','341125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤阳县','341126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天长市','341181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('明光市','341182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜阳市','341200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('颍州区','341202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('颍东区','341203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('颍泉区','341204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临泉县','341221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太和县','341222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜南县','341225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('颍上县','341226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('界首市','341282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宿州市','341300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('埇桥区','341302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('砀山县','341321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萧县','341322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵璧县','341323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泗县','341324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('六安市','341500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金安区','341502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('裕安区','341503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寿县','341521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霍邱县','341522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舒城县','341523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金寨县','341524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霍山县','341525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('亳州市','341600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('谯城区','341602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涡阳县','341621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒙城县','341622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('利辛县','341623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('池州市','341700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵池区','341702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东至县','341721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石台县','341722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青阳县','341723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣城市','341800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','341801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣州区','341802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郎溪县','341821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广德县','341822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泾县','341823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绩溪县','341824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旌德县','341825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁国市','341881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福建省','350000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福州市','350100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼓楼区','350102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台江区','350103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仓山区','350104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马尾区','350105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋安区','350111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('闽侯县','350121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连江县','350122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗源县','350123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('闽清县','350124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永泰县','350125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平潭县','350128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福清市','350181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长乐市','350182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('厦门市','350200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('思明区','350203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海沧区','350205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖里区','350206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('集美区','350211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('同安区','350212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翔安区','350213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莆田市','350300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城厢区','350302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涵江区','350303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荔城区','350304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秀屿区','350305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仙游县','350322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三明市','350400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅列区','350402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三元区','350403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('明溪县','350421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清流县','350423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁化县','350424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大田县','350425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尤溪县','350426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙县','350427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('将乐县','350428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰宁县','350429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建宁县','350430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永安市','350481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泉州市','350500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鲤城区','350502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰泽区','350503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛江区','350504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泉港区','350505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠安县','350521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安溪县','350524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永春县','350525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德化县','350526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金门县','350527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石狮市','350581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋江市','350582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南安市','350583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漳州市','350600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芗城区','350602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙文区','350603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云霄县','350622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漳浦县','350623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('诏安县','350624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长泰县','350625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东山县','350626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南靖县','350627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平和县','350628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华安县','350629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙海市','350681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南平市','350700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延平区','350702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺昌县','350721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浦城县','350722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('光泽县','350723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松溪县','350724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('政和县','350725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邵武市','350781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武夷山市','350782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建瓯市','350783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建阳市','350784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙岩市','350800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新罗区','350802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长汀县','350821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永定县','350822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上杭县','350823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武平县','350824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连城县','350825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漳平市','350881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁德市','350900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','350901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蕉城区','350902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霞浦县','350921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古田县','350922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屏南县','350923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寿宁县','350924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('周宁县','350925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柘荣县','350926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福安市','350981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福鼎市','350982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江西省','360000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南昌市','360100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东湖区','360102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西湖区','360103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青云谱区','360104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湾里区','360105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青山湖区','360111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南昌县','360121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新建县','360122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安义县','360123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('进贤县','360124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景德镇市','360200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌江区','360202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('珠山区','360203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浮梁县','360222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐平市','360281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萍乡市','360300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安源区','360302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘东区','360313');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莲花县','360321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上栗县','360322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芦溪县','360323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九江市','360400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庐山区','360402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浔阳区','360403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九江县','360421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武宁县','360423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('修水县','360424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永修县','360425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德安县','360426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('星子县','360427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('都昌县','360428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖口县','360429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彭泽县','360430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瑞昌市','360481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('共青城市','360482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新余市','360500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渝水区','360502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('分宜县','360521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹰潭市','360600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('月湖区','360602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('余江县','360622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵溪市','360681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赣州市','360700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('章贡区','360702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赣县','360721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('信丰县','360722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大余县','360723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上犹县','360724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇义县','360725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安远县','360726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙南县','360727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定南县','360728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('全南县','360729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁都县','360730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('于都县','360731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴国县','360732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会昌县','360733');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寻乌县','360734');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石城县','360735');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瑞金市','360781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南康市','360782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉安市','360800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉州区','360802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青原区','360803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉安县','360821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉水县','360822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峡江县','360823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新干县','360824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永丰县','360825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰和县','360826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遂川县','360827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万安县','360828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安福县','360829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永新县','360830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('井冈山市','360881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜春市','360900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','360901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('袁州区','360902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奉新县','360921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万载县','360922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上高县','360923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜丰县','360924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖安县','360925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜鼓县','360926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰城市','360981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('樟树市','360982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高安市','360983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('抚州市','361000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','361001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临川区','361002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南城县','361021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黎川县','361022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南丰县','361023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇仁县','361024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐安县','361025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜黄县','361026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金溪县','361027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资溪县','361028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东乡县','361029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广昌县','361030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上饶市','361100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','361101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('信州区','361102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上饶县','361121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广丰县','361122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉山县','361123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铅山县','361124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('横峰县','361125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('弋阳县','361126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('余干县','361127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄱阳县','361128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万年县','361129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('婺源县','361130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德兴市','361181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山东省','370000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('济南市','370100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('历下区','370102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市中区','370103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('槐荫区','370104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天桥区','370105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('历城区','370112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长清区','370113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平阴县','370124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('济阳县','370125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商河县','370126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('章丘市','370181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青岛市','370200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市南区','370202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市北区','370203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四方区','370205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄岛区','370211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崂山区','370212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('李沧区','370213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城阳区','370214');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('胶州市','370281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('即墨市','370282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平度市','370283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('胶南市','370284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱西市','370285');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淄博市','370300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淄川区','370302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张店区','370303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博山区','370304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临淄区','370305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('周村区','370306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桓台县','370321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高青县','370322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沂源县','370323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('枣庄市','370400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市中区','370402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('薛城区','370403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峄城区','370404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台儿庄区','370405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山亭区','370406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滕州市','370481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东营市','370500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东营区','370502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河口区','370503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('垦利县','370521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('利津县','370522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广饶县','370523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('烟台市','370600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芝罘区','370602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福山区','370611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牟平区','370612');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱山区','370613');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长岛县','370634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙口市','370681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱阳市','370682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱州市','370683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓬莱市','370684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('招远市','370685');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('栖霞市','370686');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海阳市','370687');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潍坊市','370700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潍城区','370702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寒亭区','370703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('坊子区','370704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奎文区','370705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临朐县','370724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌乐县','370725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青州市','370781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('诸城市','370782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寿光市','370783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安丘市','370784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高密市','370785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌邑市','370786');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('济宁市','370800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市中区','370802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('任城区','370811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('微山县','370826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鱼台县','370827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金乡县','370828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉祥县','370829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汶上县','370830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泗水县','370831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梁山县','370832');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲阜市','370881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兖州市','370882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邹城市','370883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰安市','370900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','370901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泰山区','370902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岱岳区','370911');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁阳县','370921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东平县','370923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新泰市','370982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肥城市','370983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('威海市','371000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('环翠区','371002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文登市','371081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荣成市','371082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乳山市','371083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('日照市','371100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东港区','371102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岚山区','371103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五莲县','371121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莒县','371122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱芜市','371200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莱城区','371202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钢城区','371203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临沂市','371300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰山区','371302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗庄区','371311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河东区','371312');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沂南县','371321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郯城县','371322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沂水县','371323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苍山县','371324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('费县','371325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平邑县','371326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莒南县','371327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒙阴县','371328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临沭县','371329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德州市','371400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德城区','371402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陵县','371421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁津县','371422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庆云县','371423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临邑县','371424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('齐河县','371425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平原县','371426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夏津县','371427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武城县','371428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐陵市','371481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禹城市','371482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('聊城市','371500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东昌府区','371502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳谷县','371521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莘县','371522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茌平县','371523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东阿县','371524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('冠县','371525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高唐县','371526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临清市','371581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨州市','371600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滨城区','371602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠民县','371621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳信县','371622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('无棣县','371623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沾化县','371624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博兴县','371625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邹平县','371626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('菏泽市','371700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','371701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牡丹区','371702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曹县','371721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('单县','371722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('成武县','371723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巨野县','371724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郓城县','371725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄄城县','371726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定陶县','371727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东明县','371728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河南省','410000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郑州市','410100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中原区','410102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('二七区','410103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('管城回族区','410104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金水区','410105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上街区','410106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠济区','410108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中牟县','410122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巩义市','410181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荥阳市','410182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新密市','410183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新郑市','410184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('登封市','410185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开封市','410200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙亭区','410202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺河回族区','410203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼓楼区','410204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禹王台区','410205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金明区','410211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杞县','410221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通许县','410222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尉氏县','410223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开封县','410224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰考县','410225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛阳市','410300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('老城区','410302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西工区','410303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瀍河回族区','410304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涧西区','410305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉利区','410306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛龙区','410311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孟津县','410322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新安县','410323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('栾川县','410324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嵩县','410325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汝阳县','410326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜阳县','410327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛宁县','410328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊川县','410329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('偃师市','410381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平顶山市','410400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新华区','410402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卫东区','410403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石龙区','410404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湛河区','410411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝丰县','410421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('叶县','410422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鲁山县','410423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郏县','410425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舞钢市','410481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汝州市','410482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安阳市','410500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文峰区','410502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北关区','410503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('殷都区','410505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙安区','410506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安阳县','410522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汤阴县','410523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('滑县','410526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('内黄县','410527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林州市','410581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤壁市','410600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤山区','410602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山城区','410603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淇滨区','410611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浚县','410621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淇县','410622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新乡市','410700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红旗区','410702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卫滨区','410703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤泉区','410704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牧野区','410711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新乡县','410721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('获嘉县','410724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('原阳县','410725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延津县','410726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('封丘县','410727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长垣县','410728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卫辉市','410781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辉县市','410782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('焦作市','410800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('解放区','410802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中站区','410803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马村区','410804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山阳区','410811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('修武县','410821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博爱县','410822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武陟县','410823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温县','410825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沁阳市','410882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孟州市','410883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('濮阳市','410900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','410901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华龙区','410902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清丰县','410922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南乐县','410923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('范县','410926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台前县','410927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('濮阳县','410928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('许昌市','411000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('魏都区','411002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('许昌县','411023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄢陵县','411024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄城县','411025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禹州市','411081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长葛市','411082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漯河市','411100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('源汇区','411102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郾城区','411103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('召陵区','411104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舞阳县','411121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临颍县','411122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三门峡市','411200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖滨区','411202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渑池县','411221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陕县','411222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卢氏县','411224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('义马市','411281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵宝市','411282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南阳市','411300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宛城区','411302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卧龙区','411303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南召县','411321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('方城县','411322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西峡县','411323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇平县','411324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('内乡县','411325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淅川县','411326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('社旗县','411327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('唐河县','411328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新野县','411329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桐柏县','411330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邓州市','411381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商丘市','411400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梁园区','411402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('睢阳区','411403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('民权县','411421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('睢县','411422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁陵县','411423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柘城县','411424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('虞城县','411425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夏邑县','411426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永城市','411481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('信阳市','411500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浉河区','411502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平桥区','411503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗山县','411521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('光山县','411522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新县','411523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商城县','411524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('固始县','411525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潢川县','411526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮滨县','411527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('息县','411528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('周口市','411600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('川汇区','411602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扶沟县','411621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西华县','411622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商水县','411623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沈丘县','411624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郸城县','411625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淮阳县','411626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太康县','411627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹿邑县','411628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('项城市','411681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('驻马店市','411700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','411701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('驿城区','411702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西平县','411721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上蔡县','411722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平舆县','411723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正阳县','411724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('确山县','411725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泌阳县','411726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汝南县','411727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遂平县','411728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新蔡县','411729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('省直辖县级行政区划','419000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('济源市','419001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖北省','420000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武汉市','420100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江岸区','420102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江汉区','420103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('硚口区','420104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉阳区','420105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武昌区','420106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青山区','420107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪山区','420111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东西湖区','420112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉南区','420113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蔡甸区','420114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江夏区','420115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄陂区','420116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新洲区','420117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄石市','420200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄石港区','420202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西塞山区','420203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('下陆区','420204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁山区','420205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳新县','420222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大冶市','420281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('十堰市','420300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茅箭区','420302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张湾区','420303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郧县','420321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郧西县','420322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('竹山县','420323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('竹溪县','420324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('房县','420325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹江口市','420381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜昌市','420500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西陵区','420502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伍家岗区','420503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('点军区','420504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('猇亭区','420505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夷陵区','420506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('远安县','420525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴山县','420526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秭归县','420527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长阳土家族自治县','420528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五峰土家族自治县','420529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜都市','420581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('当阳市','420582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('枝江市','420583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄阳市','420600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄城区','420602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('樊城区','420606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('襄州区','420607');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南漳县','420624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('谷城县','420625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保康县','420626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('老河口市','420682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('枣阳市','420683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜城市','420684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂州市','420700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梁子湖区','420702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华容区','420703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄂城区','420704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荆门市','420800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东宝区','420802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('掇刀区','420804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('京山县','420821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙洋县','420822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钟祥市','420881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孝感市','420900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','420901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孝南区','420902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孝昌县','420921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大悟县','420922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云梦县','420923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('应城市','420981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安陆市','420982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉川市','420984');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荆州市','421000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','421001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙市区','421002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荆州区','421003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('公安县','421022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('监利县','421023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江陵县','421024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石首市','421081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪湖市','421083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松滋市','421087');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄冈市','421100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','421101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄州区','421102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('团风县','421121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红安县','421122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗田县','421123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('英山县','421124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浠水县','421125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蕲春县','421126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄梅县','421127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻城市','421181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武穴市','421182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('咸宁市','421200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','421201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('咸安区','421202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉鱼县','421221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通城县','421222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇阳县','421223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通山县','421224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赤壁市','421281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('随州市','421300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','421301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曾都区','421303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('随县','421321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广水市','421381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('恩施土家族苗族自治州','422800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('恩施市','422801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('利川市','422802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建始县','422822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴东县','422823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣恩县','422825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('咸丰县','422826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('来凤县','422827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤峰县','422828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('省直辖县级行政区划','429000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仙桃市','429004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潜江市','429005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天门市','429006');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('神农架林区','429021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湖南省','430000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长沙市','430100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芙蓉区','430102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天心区','430103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳麓区','430104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开福区','430105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雨花区','430111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望城区','430112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长沙县','430121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁乡县','430124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浏阳市','430181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('株洲市','430200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荷塘区','430202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芦淞区','430203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石峰区','430204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天元区','430211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('株洲县','430221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('攸县','430223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茶陵县','430224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('炎陵县','430225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('醴陵市','430281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘潭市','430300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雨湖区','430302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳塘区','430304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘潭县','430321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘乡市','430381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('韶山市','430382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡阳市','430400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('珠晖区','430405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雁峰区','430406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石鼓区','430407');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒸湘区','430408');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南岳区','430412');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡阳县','430421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡南县','430422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡山县','430423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('衡东县','430424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祁东县','430426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('耒阳市','430481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('常宁市','430482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邵阳市','430500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双清区','430502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大祥区','430503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北塔区','430511');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邵东县','430521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新邵县','430522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邵阳县','430523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆回县','430524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洞口县','430525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥宁县','430527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新宁县','430528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城步苗族自治县','430529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武冈市','430581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳阳市','430600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳阳楼区','430602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云溪区','430603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('君山区','430611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳阳县','430621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华容县','430623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘阴县','430624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平江县','430626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汨罗市','430681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临湘市','430682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('常德市','430700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武陵区','430702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼎城区','430703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安乡县','430721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉寿县','430722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澧县','430723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临澧县','430724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桃源县','430725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石门县','430726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('津市市','430781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张家界市','430800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永定区','430802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武陵源区','430811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('慈利县','430821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桑植县','430822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('益阳市','430900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','430901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资阳区','430902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赫山区','430903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南县','430921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桃江县','430922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安化县','430923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沅江市','430981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郴州市','431000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','431001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北湖区','431002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苏仙区','431003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桂阳县','431021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜章县','431022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永兴县','431023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉禾县','431024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临武县','431025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汝城县','431026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桂东县','431027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安仁县','431028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资兴市','431081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永州市','431100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','431101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('零陵区','431102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('冷水滩区','431103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祁阳县','431121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东安县','431122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双牌县','431123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('道县','431124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江永县','431125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁远县','431126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓝山县','431127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新田县','431128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江华瑶族自治县','431129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀化市','431200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','431201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤城区','431202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中方县','431221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沅陵县','431222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('辰溪县','431223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('溆浦县','431224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会同县','431225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻阳苗族自治县','431226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新晃侗族自治县','431227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芷江侗族自治县','431228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖州苗族侗族自治县','431229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通道侗族自治县','431230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪江市','431281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('娄底市','431300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','431301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('娄星区','431302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双峰县','431321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新化县','431322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('冷水江市','431381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涟源市','431382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘西土家族苗族自治州','433100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉首市','433101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸溪县','433122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤凰县','433123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('花垣县','433124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保靖县','433125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古丈县','433126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永顺县','433127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙山县','433130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广东省','440000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广州市','440100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荔湾区','440103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('越秀区','440104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海珠区','440105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天河区','440106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白云区','440111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄埔区','440112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('番禺区','440113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('花都区','440114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南沙区','440115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萝岗区','440116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('增城市','440183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('从化市','440184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('韶关市','440200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武江区','440203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浈江区','440204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲江区','440205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('始兴县','440222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仁化县','440224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翁源县','440229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乳源瑶族自治县','440232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新丰县','440233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐昌市','440281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南雄市','440282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('深圳市','440300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗湖区','440303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福田区','440304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南山区','440305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝安区','440306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙岗区','440307');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐田区','440308');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('珠海市','440400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('香洲区','440402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('斗门区','440403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金湾区','440404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汕头市','440500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙湖区','440507');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金平区','440511');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('濠江区','440512');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潮阳区','440513');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潮南区','440514');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澄海区','440515');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南澳县','440523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('佛山市','440600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禅城区','440604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南海区','440605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺德区','440606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三水区','440607');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高明区','440608');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江门市','440700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓬江区','440703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江海区','440704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新会区','440705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台山市','440781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开平市','440783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤山市','440784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('恩平市','440785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湛江市','440800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赤坎区','440802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霞山区','440803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('坡头区','440804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻章区','440811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遂溪县','440823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徐闻县','440825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('廉江市','440881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雷州市','440882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴川市','440883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茂名市','440900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','440901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茂南区','440902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茂港区','440903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('电白县','440923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高州市','440981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('化州市','440982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('信宜市','440983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肇庆市','441200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('端州区','441202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鼎湖区','441203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广宁县','441223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怀集县','441224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('封开县','441225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德庆县','441226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高要市','441283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四会市','441284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠州市','441300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠城区','441302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠阳区','441303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博罗县','441322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠东县','441323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙门县','441324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅州市','441400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅江区','441402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梅县','441421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大埔县','441422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰顺县','441423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五华县','441424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平远县','441426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蕉岭县','441427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴宁市','441481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汕尾市','441500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城区','441502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海丰县','441521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陆河县','441523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陆丰市','441581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河源市','441600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('源城区','441602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('紫金县','441621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙川县','441622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连平县','441623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和平县','441624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东源县','441625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳江市','441700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江城区','441702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳西县','441721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳东县','441723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳春市','441781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清远市','441800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','441801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清城区','441802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('佛冈县','441821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳山县','441823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连山壮族瑶族自治县','441825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连南瑶族自治县','441826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清新县','441827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('英德市','441881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('连州市','441882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东莞市','441900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中山市','442000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潮州市','445100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','445101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湘桥区','445102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潮安县','445121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('饶平县','445122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('揭阳市','445200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','445201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榕城区','445202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('揭东县','445221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('揭西县','445222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠来县','445224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普宁市','445281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云浮市','445300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','445301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云城区','445302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新兴县','445321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郁南县','445322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云安县','445323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗定市','445381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广西壮族自治区','450000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南宁市','450100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴宁区','450102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青秀区','450103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江南区','450105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西乡塘区','450107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('良庆区','450108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邕宁区','450109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武鸣县','450122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆安县','450123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马山县','450124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上林县','450125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宾阳县','450126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('横县','450127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳州市','450200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城中区','450202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鱼峰区','450203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳南区','450204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳北区','450205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳江县','450221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柳城县','450222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹿寨县','450223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('融安县','450224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('融水苗族自治县','450225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三江侗族自治县','450226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桂林市','450300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秀峰区','450302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('叠彩区','450303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('象山区','450304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('七星区','450305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雁山区','450311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阳朔县','450321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临桂县','450322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵川县','450323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('全州县','450324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴安县','450325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永福县','450326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灌阳县','450327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙胜各族自治县','450328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资源县','450329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平乐县','450330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荔浦县','450331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('恭城瑶族自治县','450332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梧州市','450400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万秀区','450403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蝶山区','450404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长洲区','450405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苍梧县','450421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('藤县','450422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒙山县','450423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岑溪市','450481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北海市','450500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海城区','450502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('银海区','450503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铁山港区','450512');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合浦县','450521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('防城港市','450600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('港口区','450602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('防城区','450603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('上思县','450621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东兴市','450681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钦州市','450700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钦南区','450702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钦北区','450703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵山县','450721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浦北县','450722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵港市','450800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('港北区','450802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('港南区','450803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('覃塘区','450804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平南县','450821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桂平市','450881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉林市','450900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','450901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉州区','450902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('容县','450921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陆川县','450922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博白县','450923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴业县','450924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北流市','450981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('百色市','451000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','451001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('右江区','451002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('田阳县','451021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('田东县','451022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平果县','451023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德保县','451024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖西县','451025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('那坡县','451026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凌云县','451027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐业县','451028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('田林县','451029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西林县','451030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆林各族自治县','451031');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贺州市','451100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','451101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('八步区','451102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昭平县','451121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钟山县','451122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富川瑶族自治县','451123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河池市','451200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','451201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金城江区','451202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南丹县','451221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天峨县','451222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤山县','451223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东兰县','451224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗城仫佬族自治县','451225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('环江毛南族自治县','451226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴马瑶族自治县','451227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('都安瑶族自治县','451228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大化瑶族自治县','451229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜州市','451281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('来宾市','451300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','451301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴宾区','451302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('忻城县','451321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('象州县','451322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武宣县','451323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金秀瑶族自治县','451324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合山市','451381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇左市','451400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','451401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江洲区','451402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扶绥县','451421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁明县','451422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙州县','451423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大新县','451424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天等县','451425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凭祥市','451481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海南省','460000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海口市','460100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','460101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秀英区','460105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙华区','460106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('琼山区','460107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('美兰区','460108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三亚市','460200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','460201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三沙市','460300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西沙群岛','460321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南沙群岛','460322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中沙群岛的岛礁及其海域','460323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('省直辖县级行政区划','469000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五指山市','469001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('琼海市','469002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('儋州市','469003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文昌市','469005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万宁市','469006');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东方市','469007');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定安县','469021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屯昌县','469022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澄迈县','469023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临高县','469024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白沙黎族自治县','469025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌江黎族自治县','469026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐东黎族自治县','469027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陵水黎族自治县','469028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保亭黎族苗族自治县','469029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('琼中黎族苗族自治县','469030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('重庆市','500000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','500100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万州区','500101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涪陵区','500102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渝中区','500103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大渡口区','500104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江北区','500105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙坪坝区','500106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九龙坡区','500107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南岸区','500108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北碚区','500109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('綦江区','500110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大足区','500111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渝北区','500112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴南区','500113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黔江区','500114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长寿区','500115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江津区','500116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合川区','500117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永川区','500118');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南川区','500119');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('县','500200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潼南县','500223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜梁县','500224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荣昌县','500226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('璧山县','500227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梁平县','500228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城口县','500229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丰都县','500230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('垫江县','500231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武隆县','500232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('忠县','500233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开县','500234');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云阳县','500235');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奉节县','500236');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巫山县','500237');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巫溪县','500238');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石柱土家族自治县','500240');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秀山土家族苗族自治县','500241');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('酉阳土家族苗族自治县','500242');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彭水苗族土家族自治县','500243');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('四川省','510000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('成都市','510100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锦江区','510104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青羊区','510105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金牛区','510106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武侯区','510107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('成华区','510108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙泉驿区','510112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青白江区','510113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新都区','510114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温江区','510115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金堂县','510121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双流县','510122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('郫县','510124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大邑县','510129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒲江县','510131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新津县','510132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('都江堰市','510181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彭州市','510182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邛崃市','510183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇州市','510184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('自贡市','510300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('自流井区','510302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贡井区','510303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大安区','510304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沿滩区','510311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荣县','510321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富顺县','510322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('攀枝花市','510400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东区','510402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西区','510403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仁和区','510411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('米易县','510421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐边县','510422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸州市','510500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江阳区','510502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('纳溪区','510503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙马潭区','510504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸县','510521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合江县','510522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('叙永县','510524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古蔺县','510525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德阳市','510600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旌阳区','510603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中江县','510623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗江县','510626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广汉市','510681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('什邡市','510682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绵竹市','510683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绵阳市','510700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('涪城区','510703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('游仙区','510704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三台县','510722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐亭县','510723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安县','510724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梓潼县','510725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('北川羌族自治县','510726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平武县','510727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江油市','510781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广元市','510800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('利州区','510802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元坝区','510811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朝天区','510812');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旺苍县','510821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青川县','510822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('剑阁县','510823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('苍溪县','510824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遂宁市','510900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','510901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('船山区','510903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安居区','510904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓬溪县','510921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('射洪县','510922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大英县','510923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('内江市','511000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市中区','511002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东兴区','511011');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('威远县','511024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资中县','511025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆昌县','511028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐山市','511100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市中区','511102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙湾区','511111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五通桥区','511112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金口河区','511113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('犍为县','511123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('井研县','511124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夹江县','511126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沐川县','511129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峨边彝族自治县','511132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马边彝族自治县','511133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峨眉山市','511181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南充市','511300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('顺庆区','511302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高坪区','511303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉陵区','511304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南部县','511321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('营山县','511322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓬安县','511323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仪陇县','511324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西充县','511325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阆中市','511381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('眉山市','511400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东坡区','511402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仁寿县','511421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彭山县','511422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洪雅县','511423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹棱县','511424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青神县','511425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜宾市','511500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('翠屏区','511502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南溪区','511503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜宾县','511521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江安县','511523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长宁县','511524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高县','511525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('珙县','511526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('筠连县','511527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴文县','511528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屏山县','511529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广安市','511600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广安区','511602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳池县','511621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武胜县','511622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('邻水县','511623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华蓥市','511681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达州市','511700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通川区','511702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达县','511721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣汉县','511722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开江县','511723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大竹县','511724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渠县','511725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万源市','511781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雅安市','511800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雨城区','511802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('名山区','511803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荥经县','511822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉源县','511823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石棉县','511824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天全县','511825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芦山县','511826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝兴县','511827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴中市','511900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','511901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴州区','511902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通江县','511921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南江县','511922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平昌县','511923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('资阳市','512000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','512001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雁江区','512002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安岳县','512021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐至县','512022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('简阳市','512081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿坝藏族羌族自治州','513200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汶川县','513221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('理县','513222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('茂县','513223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松潘县','513224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九寨沟县','513225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金川县','513226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('小金县','513227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黑水县','513228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马尔康县','513229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('壤塘县','513230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿坝县','513231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('若尔盖县','513232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红原县','513233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘孜藏族自治州','513300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康定县','513321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸定县','513322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹巴县','513323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('九龙县','513324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雅江县','513325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('道孚县','513326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('炉霍县','513327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘孜县','513328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新龙县','513329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德格县','513330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白玉县','513331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石渠县','513332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('色达县','513333');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('理塘县','513334');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴塘县','513335');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乡城县','513336');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('稻城县','513337');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('得荣县','513338');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凉山彝族自治州','513400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西昌市','513401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('木里藏族自治县','513422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐源县','513423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德昌县','513424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会理县','513425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会东县','513426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁南县','513427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普格县','513428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('布拖县','513429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金阳县','513430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昭觉县','513431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('喜德县','513432');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('冕宁县','513433');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('越西县','513434');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘洛县','513435');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('美姑县','513436');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雷波县','513437');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵州省','520000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵阳市','520100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','520101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南明区','520102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云岩区','520103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('花溪区','520111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌当区','520112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白云区','520113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('小河区','520114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开阳县','520121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('息烽县','520122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('修文县','520123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清镇市','520181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('六盘水市','520200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('钟山区','520201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('六枝特区','520203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('水城县','520221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盘县','520222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遵义市','520300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','520301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红花岗区','520302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汇川区','520303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('遵义县','520321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桐梓县','520322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥阳县','520323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正安县','520324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('道真仡佬族苗族自治县','520325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('务川仡佬族苗族自治县','520326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤冈县','520327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湄潭县','520328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('余庆县','520329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('习水县','520330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赤水市','520381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仁怀市','520382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安顺市','520400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','520401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西秀区','520402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平坝县','520421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普定县','520422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇宁布依族苗族自治县','520423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('关岭布依族苗族自治县','520424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('紫云苗族布依族自治县','520425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('毕节市','520500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('七星关区','520502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大方县','520521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黔西县','520522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金沙县','520523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('织金县','520524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('纳雍县','520525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('威宁彝族回族苗族自治县','520526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('赫章县','520527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜仁市','520600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('碧江区','520602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('万山区','520603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江口县','520621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉屏侗族自治县','520622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石阡县','520623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('思南县','520624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('印江土家族苗族自治县','520625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德江县','520626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沿河土家族自治县','520627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('松桃苗族自治县','520628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黔西南布依族苗族自治州','522300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴义市','522301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴仁县','522322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普安县','522323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晴隆县','522324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贞丰县','522325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('望谟县','522326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('册亨县','522327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安龙县','522328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黔东南苗族侗族自治州','522600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凯里市','522601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄平县','522622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('施秉县','522623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三穗县','522624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇远县','522625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岑巩县','522626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天柱县','522627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('锦屏县','522628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('剑河县','522629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台江县','522630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黎平县','522631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榕江县','522632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('从江县','522633');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雷山县','522634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻江县','522635');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹寨县','522636');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黔南布依族苗族自治州','522700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('都匀市','522701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福泉市','522702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('荔波县','522722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵定县','522723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瓮安县','522725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('独山县','522726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平塘县','522727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗甸县','522728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长顺县','522729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙里县','522730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠水县','522731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三都水族自治县','522732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云南省','530000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昆明市','530100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五华区','530102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盘龙区','530103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('官渡区','530111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西山区','530112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东川区','530113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呈贡区','530114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('晋宁县','530122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富民县','530124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜良县','530125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石林彝族自治县','530126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嵩明县','530127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禄劝彝族苗族自治县','530128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('寻甸回族彝族自治县','530129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安宁市','530181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲靖市','530300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麒麟区','530302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马龙县','530321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陆良县','530322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('师宗县','530323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('罗平县','530324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富源县','530325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会泽县','530326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沾益县','530328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宣威市','530381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉溪市','530400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红塔区','530402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江川县','530421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澄江县','530422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通海县','530423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华宁县','530424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('易门县','530425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('峨山彝族自治县','530426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新平彝族傣族自治县','530427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元江哈尼族彝族傣族自治县','530428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('保山市','530500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆阳区','530502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('施甸县','530521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('腾冲县','530522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('龙陵县','530523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌宁县','530524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昭通市','530600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昭阳区','530602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鲁甸县','530621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巧家县','530622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐津县','530623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大关县','530624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永善县','530625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥江县','530626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇雄县','530627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彝良县','530628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('威信县','530629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('水富县','530630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丽江市','530700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古城区','530702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉龙纳西族自治县','530721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永胜县','530722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华坪县','530723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁蒗彝族自治县','530724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普洱市','530800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('思茅区','530802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁洱哈尼族彝族自治县','530821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('墨江哈尼族自治县','530822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景东彝族自治县','530823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景谷傣族彝族自治县','530824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇沅彝族哈尼族拉祜族自治县','530825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江城哈尼族彝族自治县','530826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('孟连傣族拉祜族佤族自治县','530827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澜沧拉祜族自治县','530828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西盟佤族自治县','530829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临沧市','530900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','530901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临翔区','530902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤庆县','530921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云县','530922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永德县','530923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇康县','530924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双江拉祜族佤族布朗族傣族自治县','530925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('耿马傣族佤族自治县','530926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沧源佤族自治县','530927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('楚雄彝族自治州','532300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('楚雄市','532301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('双柏县','532322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('牟定县','532323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南华县','532324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('姚安县','532325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大姚县','532326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永仁县','532327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元谋县','532328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武定县','532329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('禄丰县','532331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红河哈尼族彝族自治州','532500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('个旧市','532501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('开远市','532502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒙自市','532503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('屏边苗族自治县','532523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('建水县','532524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石屏县','532525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('弥勒县','532526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸西县','532527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('元阳县','532528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红河县','532529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金平苗族瑶族傣族自治县','532530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绿春县','532531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河口瑶族自治县','532532');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文山壮族苗族自治州','532600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文山市','532601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('砚山县','532622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西畴县','532623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麻栗坡县','532624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('马关县','532625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丘北县','532626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广南县','532627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富宁县','532628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西双版纳傣族自治州','532800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景洪市','532801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('勐海县','532822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('勐腊县','532823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大理白族自治州','532900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大理市','532901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漾濞彝族自治县','532922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祥云县','532923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宾川县','532924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('弥渡县','532925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南涧彝族自治县','532926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巍山彝族回族自治县','532927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永平县','532928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('云龙县','532929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洱源县','532930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('剑川县','532931');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鹤庆县','532932');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德宏傣族景颇族自治州','533100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瑞丽市','533102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芒市','533103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('梁河县','533122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盈江县','533123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陇川县','533124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('怒江傈僳族自治州','533300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泸水县','533321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福贡县','533323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贡山独龙族怒族自治县','533324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰坪白族普米族自治县','533325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迪庆藏族自治州','533400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('香格里拉县','533421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德钦县','533422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('维西傈僳族自治县','533423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西藏自治区','540000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('拉萨市','540100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','540101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城关区','540102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林周县','540121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('当雄县','540122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尼木县','540123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲水县','540124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('堆龙德庆县','540125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达孜县','540126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('墨竹工卡县','540127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌都地区','542100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌都县','542121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江达县','542122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贡觉县','542123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('类乌齐县','542124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丁青县','542125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察雅县','542126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('八宿县','542127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('左贡县','542128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('芒康县','542129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛隆县','542132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('边坝县','542133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山南地区','542200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乃东县','542221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扎囊县','542222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贡嘎县','542223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('桑日县','542224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('琼结县','542225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲松县','542226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('措美县','542227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛扎县','542228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('加查县','542229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆子县','542231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('错那县','542232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('浪卡子县','542233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('日喀则地区','542300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('日喀则市','542301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南木林县','542322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('江孜县','542323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定日县','542324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萨迦县','542325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('拉孜县','542326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昂仁县','542327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('谢通门县','542328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白朗县','542329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仁布县','542330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康马县','542331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定结县','542332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('仲巴县','542333');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('亚东县','542334');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉隆县','542335');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('聂拉木县','542336');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('萨嘎县','542337');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岗巴县','542338');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('那曲地区','542400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('那曲县','542421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉黎县','542422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('比如县','542423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('聂荣县','542424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安多县','542425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('申扎县','542426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('索县','542427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('班戈县','542428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴青县','542429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尼玛县','542430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿里地区','542500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('普兰县','542521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('札达县','542522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('噶尔县','542523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('日土县','542524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('革吉县','542525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('改则县','542526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('措勤县','542527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林芝地区','542600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('林芝县','542621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('工布江达县','542622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('米林县','542623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('墨脱县','542624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('波密县','542625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察隅县','542626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('朗县','542627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陕西省','610000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西安市','610100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新城区','610102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('碑林区','610103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莲湖区','610104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灞桥区','610111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('未央区','610112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('雁塔区','610113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阎良区','610114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临潼区','610115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长安区','610116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蓝田县','610122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('周至县','610124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('户县','610125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高陵县','610126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('铜川市','610200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('王益区','610202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('印台区','610203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('耀州区','610204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜君县','610222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝鸡市','610300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渭滨区','610302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金台区','610303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陈仓区','610304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤翔县','610322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岐山县','610323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('扶风县','610324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('眉县','610326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陇县','610327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('千阳县','610328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麟游县','610329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凤县','610330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('太白县','610331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('咸阳市','610400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秦都区','610402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杨陵区','610403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渭城区','610404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('三原县','610422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泾阳县','610423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乾县','610424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('礼泉县','610425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永寿县','610426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彬县','610427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('长武县','610428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旬邑县','610429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('淳化县','610430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武功县','610431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴平市','610481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渭南市','610500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临渭区','610502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华县','610521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('潼关县','610522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大荔县','610523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合阳县','610524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澄城县','610525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('蒲城县','610526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白水县','610527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富平县','610528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('韩城市','610581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华阴市','610582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延安市','610600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宝塔区','610602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延长县','610621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('延川县','610622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('子长县','610623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安塞县','610624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('志丹县','610625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴起县','610626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘泉县','610627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富县','610628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛川县','610629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宜川县','610630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄龙县','610631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄陵县','610632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉中市','610700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉台区','610702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('南郑县','610721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城固县','610722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洋县','610723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西乡县','610724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('勉县','610725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁强县','610726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('略阳县','610727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇巴县','610728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('留坝县','610729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('佛坪县','610730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆林市','610800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆阳区','610802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('神木县','610821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('府谷县','610822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('横山县','610823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖边县','610824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定边县','610825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('绥德县','610826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('米脂县','610827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('佳县','610828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴堡县','610829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清涧县','610830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('子洲县','610831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安康市','610900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','610901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉滨区','610902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('汉阴县','610921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石泉县','610922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁陕县','610923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('紫阳县','610924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岚皋县','610925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平利县','610926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇坪县','610927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('旬阳县','610928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白河县','610929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商洛市','611000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','611001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商州区','611002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛南县','611021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('丹凤县','611022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('商南县','611023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山阳县','611024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇安县','611025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柞水县','611026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘肃省','620000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兰州市','620100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城关区','620102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('七里河区','620103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西固区','620104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安宁区','620105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红古区','620111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永登县','620121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('皋兰县','620122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('榆中县','620123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('嘉峪关市','620200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金昌市','620300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金川区','620302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永昌县','620321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白银市','620400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白银区','620402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平川区','620403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('靖远县','620421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('会宁县','620422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('景泰县','620423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天水市','620500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秦州区','620502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麦积区','620503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('清水县','620521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('秦安县','620522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘谷县','620523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武山县','620524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张家川回族自治县','620525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武威市','620600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('凉州区','620602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('民勤县','620621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('古浪县','620622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天祝藏族自治县','620623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('张掖市','620700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘州区','620702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肃南裕固族自治县','620721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('民乐县','620722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临泽县','620723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('高台县','620724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('山丹县','620725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平凉市','620800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崆峒区','620802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泾川县','620821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵台县','620822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崇信县','620823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华亭县','620824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庄浪县','620825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('静宁县','620826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('酒泉市','620900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','620901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肃州区','620902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金塔县','620921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('瓜州县','620922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('肃北蒙古族自治县','620923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿克塞哈萨克族自治县','620924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉门市','620981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('敦煌市','620982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庆阳市','621000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','621001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西峰区','621002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('庆城县','621021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('环县','621022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('华池县','621023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合水县','621024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('正宁县','621025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁县','621026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('镇原县','621027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('定西市','621100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','621101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('安定区','621102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('通渭县','621121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陇西县','621122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('渭源县','621123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临洮县','621124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('漳县','621125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岷县','621126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('陇南市','621200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','621201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('武都区','621202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('成县','621221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('文县','621222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宕昌县','621223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康县','621224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西和县','621225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('礼县','621226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('徽县','621227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('两当县','621228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临夏回族自治州','622900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临夏市','622901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临夏县','622921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('康乐县','622922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永靖县','622923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('广河县','622924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和政县','622925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('东乡族自治县','622926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('积石山保安族东乡族撒拉族自治县','622927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘南藏族自治州','623000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('合作市','623001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('临潭县','623021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('卓尼县','623022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('舟曲县','623023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('迭部县','623024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玛曲县','623025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('碌曲县','623026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('夏河县','623027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青海省','630000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西宁市','630100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','630101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城东区','630102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城中区','630103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城西区','630104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('城北区','630105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大通回族土族自治县','630121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湟中县','630122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('湟源县','630123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海东地区','632100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平安县','632121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('民和回族土族自治县','632122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乐都县','632123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('互助土族自治县','632126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('化隆回族自治县','632127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('循化撒拉族自治县','632128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海北藏族自治州','632200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('门源回族自治县','632221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('祁连县','632222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海晏县','632223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('刚察县','632224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('黄南藏族自治州','632300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('同仁县','632321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尖扎县','632322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泽库县','632323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('河南蒙古族自治县','632324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海南藏族自治州','632500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('共和县','632521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('同德县','632522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵德县','632523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴海县','632524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贵南县','632525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('果洛藏族自治州','632600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玛沁县','632621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('班玛县','632622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('甘德县','632623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达日县','632624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('久治县','632625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玛多县','632626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉树藏族自治州','632700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玉树县','632721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('杂多县','632722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('称多县','632723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('治多县','632724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('囊谦县','632725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('曲麻莱县','632726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海西蒙古族藏族自治州','632800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('格尔木市','632801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('德令哈市','632802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌兰县','632821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('都兰县','632822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天峻县','632823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('宁夏回族自治区','640000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('银川市','640100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','640101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('兴庆区','640104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西夏区','640105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('金凤区','640106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('永宁县','640121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('贺兰县','640122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('灵武市','640181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石嘴山市','640200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','640201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('大武口区','640202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('惠农区','640205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('平罗县','640221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吴忠市','640300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','640301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('利通区','640302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('红寺堡区','640303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('盐池县','640323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('同心县','640324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青铜峡市','640381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('固原市','640400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','640401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('原州区','640402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('西吉县','640422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('隆德县','640423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泾源县','640424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('彭阳县','640425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中卫市','640500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','640501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙坡头区','640502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('中宁县','640521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('海原县','640522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新疆维吾尔自治区','650000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌鲁木齐市','650100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','650101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('天山区','650102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙依巴克区','650103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新市区','650104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('水磨沟区','650105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('头屯河区','650106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('达坂城区','650107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('米东区','650109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌鲁木齐县','650121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克拉玛依市','650200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('市辖区','650201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('独山子区','650202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克拉玛依区','650203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('白碱滩区','650204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌尔禾区','650205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吐鲁番地区','652100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吐鲁番市','652101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('鄯善县','652122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('托克逊县','652123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('哈密地区','652200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('哈密市','652201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴里坤哈萨克自治县','652222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊吾县','652223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌吉回族自治州','652300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昌吉市','652301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阜康市','652302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('呼图壁县','652323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('玛纳斯县','652324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奇台县','652325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉木萨尔县','652327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('木垒哈萨克自治县','652328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博尔塔拉蒙古自治州','652700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博乐市','652701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('精河县','652722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温泉县','652723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴音郭楞蒙古自治州','652800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('库尔勒市','652801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('轮台县','652822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尉犁县','652823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('若羌县','652824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('且末县','652825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('焉耆回族自治县','652826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和静县','652827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和硕县','652828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('博湖县','652829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿克苏地区','652900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿克苏市','652901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('温宿县','652922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('库车县','652923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙雅县','652924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新和县','652925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('拜城县','652926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌什县','652927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿瓦提县','652928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('柯坪县','652929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('克孜勒苏柯尔克孜自治州','653000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿图什市','653001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿克陶县','653022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿合奇县','653023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌恰县','653024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('喀什地区','653100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('喀什市','653101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('疏附县','653121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('疏勒县','653122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('英吉沙县','653123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('泽普县','653124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('莎车县','653125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('叶城县','653126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('麦盖提县','653127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('岳普湖县','653128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伽师县','653129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巴楚县','653130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('塔什库尔干塔吉克自治县','653131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和田地区','653200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和田市','653201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和田县','653221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('墨玉县','653222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('皮山县','653223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('洛浦县','653224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('策勒县','653225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('于田县','653226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('民丰县','653227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊犁哈萨克自治州','654000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊宁市','654002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('奎屯市','654003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('伊宁县','654021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('察布查尔锡伯自治县','654022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('霍城县','654023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('巩留县','654024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('新源县','654025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('昭苏县','654026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('特克斯县','654027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('尼勒克县','654028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('塔城地区','654200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('塔城市','654201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('乌苏市','654202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('额敏县','654221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('沙湾县','654223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('托里县','654224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('裕民县','654225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('和布克赛尔蒙古自治县','654226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿勒泰地区','654300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿勒泰市','654301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('布尔津县','654321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('富蕴县','654322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('福海县','654323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('哈巴河县','654324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('青河县','654325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('吉木乃县','654326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('自治区直辖县级行政区划','659000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('石河子市','659001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('阿拉尔市','659002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('图木舒克市','659003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('五家渠市','659004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('台湾省','710000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('香港特别行政区','810000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('澳门特别行政区','820000');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Department]
DELETE FROM [dbo].[C_Department];
GO

INSERT INTO [dbo].[C_Department]([ID],[Code],[Name],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc],[Enabled]) VALUES('001','X001',N'默认部门',NULL,NULL,NULL,0,NULL,1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_DeviceType]
DELETE FROM [dbo].[C_DeviceType];
GO

INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('0', N'未定义');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('01', N'高压配电');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('02', N'低压交流配电');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('03', N'变压器');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('04', N'低压直流配电');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('05', N'发电机组');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('06', N'开关电源');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('07', N'铅酸电池');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('08', N'UPS设备');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('09', N'UPS配电');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('11', N'机房专用空调');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('12', N'中央空调末端');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('13', N'中央空调主机');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('14', N'变换设备');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('15', N'普通空调');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('16', N'极早期烟感');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('17', N'机房环境');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('18', N'电池恒温柜');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('38', N'FSU');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('68', N'锂电池');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('76', N'动环监控');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('77', N'智能通风换热');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('78', N'风光设备');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('87', N'高压直流');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('88', N'高压直流电源配电');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('92', N'智能电表');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('93', N'智能门禁');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Duty]
DELETE FROM [dbo].[C_Duty];
GO

INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('001', N'董事长', 1, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('002', N'总经理', 2, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('003', N'部门经理', 3, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('004', N'员工', 4, NULL, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_LogicType]
DELETE FROM [dbo].[C_LogicType];
GO

INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0', '0', N'未定义');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0101', '01', N'高压配电告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0102', '01', N'高压操作电源告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0201', '02', N'低压配电告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0202', '02', N'电容补偿设备告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0203', '02', N'谐波抑制设备告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0301', '03', N'变压器告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0401', '04', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0501', '05', N'发电机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0502', '05', N'燃料电池告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0601', '06', N'电池告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0602', '06', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0603', '06', N'变换设备告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0604', '06', N'输入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0605', '06', N'整流模块告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0701', '07', N'电池告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0801', '08', N'输入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0802', '08', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0803', '08', N'旁路告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0804', '08', N'整流器告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0805', '08', N'逆变器告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0806', '08', N'辅助及控制部件告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0901', '09', N'输出中断告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1101', '11', N'风路系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1102', '11', N'压缩机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1103', '11', N'系统故障告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1201', '12', N'风路系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1202', '12', N'水路系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1203', '12', N'系统故障告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1301', '13', N'系统故障告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1302', '13', N'监控故障告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1303', '13', N'压缩机故障');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1401', '14', N'输入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1402', '14', N'逆变器告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1403', '14', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1501', '15', N'风机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1502', '15', N'压缩机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1503', '15', N'输入电源告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1601', '16', N'系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1602', '16', N'烟雾告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1701', '17', N'水浸告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1702', '17', N'烟雾告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1703', '17', N'红外告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1704', '17', N'温度告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1705', '17', N'湿度告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1706', '17', N'门碰告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1707', '17', N'玻破告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1708', '17', N'震动告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1709', '17', N'被盗告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1710', '17', N'摄像机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1801', '18', N'电池恒温箱告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('6801', '68', N'温度告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('6802', '68', N'电压告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7601', '76', N'采集设备通信中断告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7602', '76', N'系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7603', '76', N'被监控设备通信中断告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7701', '77', N'电源告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7702', '77', N'辅助告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7703', '77', N'风机告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7801', '78', N'系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8701', '87', N'输入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8702', '87', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8703', '87', N'变换设备告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8704', '87', N'整流设备告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8705', '87', N'电池告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8801', '88', N'输出告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9201', '92', N'市电异常告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9202', '92', N'系统告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9203', '92', N'输入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9301', '93', N'非法出入告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9302', '93', N'门开告警');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9303', '93', N'系统告警');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Productor]
DELETE FROM [dbo].[C_Productor];
GO

INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('00', 1, N'默认生产厂家');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('01', 1, N'爱立信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('03', 1, N'摩托罗拉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('04', 1, N'上海贝尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('05', 1, N'西门子');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('06', 1, N'北电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('07', 1, N'华为');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('08', 1, N'中兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('09', 1, N'东信北邮');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('10', 1, N'诺西');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1001', 1, N'21世纪联合');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1002', 1, N'TCL');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1003', 1, N'阿尔西');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1004', 1, N'ATLAS');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1005', 1, N'华达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1006', 1, N'爱克赛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1007', 1, N'蚌开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1008', 1, N'亳州达源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1009', 1, N'安徽华炬');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1010', 1, N'安徽鑫龙');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1011', 1, N'安康水电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1012', 1, N'平原电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1013', 1, N'奥冠');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1014', 1, N'奥林匹亚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1015', 1, N'澳柯玛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1016', 1, N'BP SOLAR');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1017', 1, N'霸州利克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1018', 1, N'柏克新能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1019', 1, N'保定宏达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1020', 1, N'保定嘉信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1021', 1, N'保定先达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1022', 1, N'北京爱劳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1023', 1, N'北京奥福瑞');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1024', 1, N'奥普里特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1025', 1, N'北京佰瑞成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1026', 1, N'北开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1027', 1, N'北京碧空');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1028', 1, N'博联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1029', 1, N'博文');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1030', 1, N'创和世纪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1031', 1, N'北京低开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1032', 1, N'动力源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1033', 1, N'丰轩瑞达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1034', 1, N'福斯特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1035', 1, N'国能电池');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1036', 1, N'国能风力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1037', 1, N'昊诚动力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1038', 1, N'北京浩凯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1039', 1, N'恒圣环');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1040', 1, N'宏光星宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1041', 1, N'华东森源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1042', 1, N'华伟基业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1043', 1, N'基业达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1044', 1, N'北京今科达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1045', 1, N'京港汇丰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1046', 1, N'凯华网联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1047', 1, N'康诚鸿业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1048', 1, N'康富英格尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1049', 1, N'科讯时代');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1050', 1, N'北京雷安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1051', 1, N'立信伟业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1052', 1, N'联动天翼');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1053', 1, N'联合华平');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1054', 1, N'鲁京国志');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1055', 1, N'美基机电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1056', 1, N'美基信达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1057', 1, N'纳源丰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1058', 1, N'北内');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1059', 1, N'氢璞创能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1060', 1, N'融和创科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1061', 1, N'锐克天成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1062', 1, N'北京瑞克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1063', 1, N'北京睿智源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1064', 1, N'北京赛康');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1065', 1, N'山川浩通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1066', 1, N'世纪华通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1067', 1, N'世纪康华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1068', 1, N'世纪瑞尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1069', 1, N'科广联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1070', 1, N'北京首信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1071', 1, N'水木能环');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1072', 1, N'硕瑞伟业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1073', 1, N'斯泰科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1074', 1, N'泰立威武');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1075', 1, N'天恒华意');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1076', 1, N'同友创业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1077', 1, N'伟思利达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1078', 1, N'北京新立');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1079', 1, N'雅驿欣');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1080', 1, N'怡蔚佳邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1081', 1, N'北京宜通源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1082', 1, N'移联信达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1083', 1, N'易达新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1084', 1, N'北京意科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1085', 1, N'北京邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1086', 1, N'友邦众拓');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1087', 1, N'裕源大通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1088', 1, N'远东博力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1089', 1, N'北京兆维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1090', 1, N'智力通源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1091', 1, N'北京中创纪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1092', 1, N'中创瑞普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1093', 1, N'中佳银讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1094', 1, N'北京中盛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1095', 1, N'中威信通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1096', 1, N'北京中移');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1097', 1, N'中奕合创');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1098', 1, N'本溪电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1099', 1, N'兵团一开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1100', 1, N'BORRI');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1101', 1, N'博尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1102', 1, N'沧开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1103', 1, N'常开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1104', 1, N'常州华迪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1105', 1, N'科勒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1106', 1, N'常州顺风');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1107', 1, N'常州太平');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1108', 1, N'常州益信通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1109', 1, N'常州远东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1110', 1, N'中兴华达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1111', 1, N'标定');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1112', 1, N'成都潮浩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1113', 1, N'成都四变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1114', 1, N'成都弘邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1115', 1, N'宏润成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1116', 1, N'成都华伟');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1117', 1, N'成都力达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1118', 1, N'龙泉机电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1119', 1, N'成都秦川');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1120', 1, N'成都四鹏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1121', 1, N'兴业雷安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1122', 1, N'成都阳光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1123', 1, N'成都英利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1124', 1, N'成都邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1125', 1, N'川开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1126', 1, N'川崎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1127', 1, N'春兰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1128', 1, N'大金');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1129', 1, N'宝士达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1130', 1, N'北方工控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1131', 1, N'北方森源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1132', 1, N'大连恩泽');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1133', 1, N'大连飞腾');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1134', 1, N'金州兴业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1135', 1, N'旅顺建筑');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1136', 1, N'太工天成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1137', 1, N'道依茨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1138', 1, N'德国宾士');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1139', 1, N'荷贝克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1140', 1, N'OBO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1141', 1, N'德国阳光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1142', 1, N'DEHN');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1143', 1, N'DEKA');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1144', 1, N'东方通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1145', 1, N'东风');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1146', 1, N'东莞超业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1147', 1, N'东莞基业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1148', 1, N'东莞康达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1149', 1, N'铭普光磁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1150', 1, N'东莞三鼎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1151', 1, N'康德威');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1152', 1, N'东莞梅兰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1153', 1, N'东莞星源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1154', 1, N'东莞裕邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1155', 1, N'东莞煜达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1156', 1, N'西电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1157', 1, N'飞达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1158', 1, N'飞毛腿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1159', 1, N'非凡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1160', 1, N'PHOENIX');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1161', 1, N'风帆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1162', 1, N'佛山禅星');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1163', 1, N'南海凯讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1164', 1, N'南海银科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1165', 1, N'佛山禅信通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1166', 1, N'佛山佛盛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1167', 1, N'佛山汇茂');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1168', 1, N'佛山力迅');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1169', 1, N'佛山智友');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1170', 1, N'福建华泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1171', 1, N'闽东本田');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1172', 1, N'福建闽泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1173', 1, N'明辉机电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1174', 1, N'福安日丰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1175', 1, N'福建泉变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1176', 1, N'福建越众');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1177', 1, N'福州福光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1178', 1, N'福州凌力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1179', 1, N'福州南方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1180', 1, N'天宇电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1181', 1, N'福州伟星');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1182', 1, N'福州亚玛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1183', 1, N'抚顺电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1184', 1, N'阜新鼎兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1185', 1, N'共信电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1186', 1, N'广东番开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1187', 1, N'番禺明珠');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1188', 1, N'高频顿保');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1189', 1, N'广东广大');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1190', 1, N'广东广特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1191', 1, N'广东海鸿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1192', 1, N'广东海坤');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1193', 1, N'广东海悟');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1194', 1, N'广东华宝');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1195', 1, N'广东华德力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1196', 1, N'惠州龙源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1197', 1, N'广东佳力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1198', 1, N'广东金颖');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1199', 1, N'广东金泽');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('12', 1, N'思科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1200', 1, N'广东康菱');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1201', 1, N'南海汇衡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1202', 1, N'南粤');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1203', 1, N'广东申菱');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1204', 1, N'汕头红卫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1205', 1, N'南顺电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1206', 1, N'广东胜捷');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1207', 1, N'龙山陈涌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1208', 1, N'顺德特变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1209', 1, N'广东顺开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1210', 1, N'顺特电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1211', 1, N'斯奥动力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1212', 1, N'天民通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1213', 1, N'西屋康达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1214', 1, N'雅达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1215', 1, N'广东雅华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1216', 1, N'广东雅景');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1217', 1, N'易事特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1218', 1, N'广东盈嘉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1219', 1, N'广东长电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1220', 1, N'广东正超');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1221', 1, N'志成冠军');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1222', 1, N'志高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1223', 1, N'中山三怡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1224', 1, N'中商国通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1225', 1, N'广东卓亚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1226', 1, N'柳电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1227', 1, N'广西玉柴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1228', 1, N'玉林开关厂');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1229', 1, N'白云电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1230', 1, N'创邮通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1231', 1, N'广州达至');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1232', 1, N'广州大中');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1233', 1, N'番禺超能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1234', 1, N'番禺骏发');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1235', 1, N'广州丰江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1236', 1, N'伽玛通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1237', 1, N'广州广高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1238', 1, N'广顺环保');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1239', 1, N'广州华成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1240', 1, N'广州华炜');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1241', 1, N'广州建新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1242', 1, N'广州捷联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1243', 1, N'广州金锵');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1244', 1, N'广州科铭');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1245', 1, N'科易光电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1246', 1, N'广州雷伏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1247', 1, N'广州猛犸');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1248', 1, N'广州南方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1249', 1, N'广州瑞华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1250', 1, N'飒特红外');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1251', 1, N'广州恒达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1252', 1, N'宏绩信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1253', 1, N'广州祺能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1254', 1, N'广州维安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1255', 1, N'广州伟昊');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1256', 1, N'新科利保');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1257', 1, N'广州怡昌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1258', 1, N'广州正通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1259', 1, N'广州中人');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1260', 1, N'广州泰光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1261', 1, N'广州天东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1262', 1, N'天邮通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1263', 1, N'广州天邮');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1264', 1, N'广州威能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1265', 1, N'广州为邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1266', 1, N'午晨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1267', 1, N'广州信和诚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1268', 1, N'广州旭杰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1269', 1, N'广州亿盛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1270', 1, N'广州邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1271', 1, N'广州致远');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1272', 1, N'珠江电信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1273', 1, N'桂林南方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1274', 1, N'国彪电源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1275', 1, N'国际久保');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1276', 1, N'东安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1277', 1, N'光宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1278', 1, N'华光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1279', 1, N'九洲电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1280', 1, N'凯尔达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1281', 1, N'立圆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1282', 1, N'海尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1283', 1, N'海康威视');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1284', 1, N'海洛斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1285', 1, N'海南海新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1286', 1, N'金盘');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1287', 1, N'海南威特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1288', 1, N'海信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1289', 1, N'海志');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1290', 1, N'火箭');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1291', 1, N'翰林科技');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1292', 1, N'杭州奥克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1293', 1, N'杭州更新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1294', 1, N'鸿雁电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1295', 1, N'杭州华鑫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1296', 1, N'杭州蓝光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1297', 1, N'杭州蓝天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1298', 1, N'杭州三以');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1299', 1, N'杭州圣力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1300', 1, N'施威特克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1301', 1, N'杭州天鸿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1302', 1, N'杭州威尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1303', 1, N'杭州西力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1304', 1, N'杭州欣美');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1305', 1, N'杭州新世纪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1306', 1, N'杭州鑫普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1307', 1, N'杭州信控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1308', 1, N'杭州幸福');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1309', 1, N'杭州亚泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1310', 1, N'杭州之江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1311', 1, N'杭州中新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1312', 1, N'杭州卓联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1313', 1, N'美菱');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1314', 1, N'合肥西子行');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1315', 1, N'河北博宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1316', 1, N'河北科华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1317', 1, N'先控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1318', 1, N'河北星河');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1319', 1, N'河北亚澳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1320', 1, N'河南华美');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1321', 1, N'河南华源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1322', 1, N'河南汇祥');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1323', 1, N'河南锂动');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1324', 1, N'河南联吉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1325', 1, N'南阳鑫特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1326', 1, N'中原通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1327', 1, N'西屋电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1328', 1, N'新太行');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1329', 1, N'恒天开马');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1330', 1, N'衡阳瑞达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1331', 1, N'鸿宝');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1332', 1, N'飞力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1333', 1, N'镕诚机械');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1334', 1, N'湖北骆驼');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1335', 1, N'普天电池');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1336', 1, N'湖北普天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1337', 1, N'湖北银箭');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1338', 1, N'湖南丰日');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1339', 1, N'湖南广能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1340', 1, N'湖南龙豪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1341', 1, N'湖南亿信安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1342', 1, N'长沙众强');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1343', 1, N'湖州成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1344', 1, N'华邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1345', 1, N'华立');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1346', 1, N'华凌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1347', 1, N'华仪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1348', 1, N'华自科技');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1349', 1, N'环宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1350', 1, N'吉林达森');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1351', 1, N'吉林汇成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1352', 1, N'吉林龙鼎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1353', 1, N'吉林耐德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1354', 1, N'济柴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1355', 1, N'济南博宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1356', 1, N'济南天悦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1357', 1, N'济南志亨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1358', 1, N'嘉兴电控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1359', 1, N'江门朗达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1360', 1, N'新会电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1361', 1, N'江苏澳华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1362', 1, N'江苏宝胜');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1363', 1, N'江苏宝狮');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1364', 1, N'江苏常鑫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1365', 1, N'江苏超宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1366', 1, N'江苏道康');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1367', 1, N'江苏迪恩杰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1368', 1, N'江苏佛朗克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1369', 1, N'江苏富思特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1370', 1, N'江苏海航');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1371', 1, N'江苏海四达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1372', 1, N'江苏华富');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1373', 1, N'华鹏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1374', 1, N'江苏华鹏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1375', 1, N'江苏华夏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1376', 1, N'江苏京隆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1377', 1, N'江苏理士');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1378', 1, N'江苏力天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1379', 1, N'江苏林洋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1380', 1, N'江苏美联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1381', 1, N'江苏欧力特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1382', 1, N'侨宏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1383', 1, N'江苏荣联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1384', 1, N'江苏士林');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1385', 1, N'双登');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1386', 1, N'江苏苏中');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1387', 1, N'德锋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1388', 1, N'天源华威');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1389', 1, N'江苏威腾');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1390', 1, N'吴通通讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1391', 1, N'江苏西门控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1392', 1, N'香江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1393', 1, N'江苏向荣');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1394', 1, N'江苏星信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1395', 1, N'兴阳能源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1396', 1, N'江苏亿能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1397', 1, N'江苏中博');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1398', 1, N'江苏中环');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1399', 1, N'江苏中天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1400', 1, N'锦州施维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1401', 1, N'锦州古塔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1402', 1, N'靖江远东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1403', 1, N'靖江中宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1404', 1, N'九川');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1405', 1, N'九江华尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1406', 1, N'俊朗');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1407', 1, N'康明斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1408', 1, N'康普顿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1409', 1, N'科奥信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1410', 1, N'科龙');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1411', 1, N'港汕南方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1412', 1, N'昆明精达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1413', 1, N'昆开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1414', 1, N'兰州海红');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1415', 1, N'劳斯莱斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1416', 1, N'乐清瑞宣');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1417', 1, N'乐星电缆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1418', 1, N'雷乐士');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1419', 1, N'凌日');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1420', 1, N'柳开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1421', 1, N'漯河宏达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1422', 1, N'麦迪思');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1423', 1, N'麦克维尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1424', 1, N'梅兰日兰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1425', 1, N'美的');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1426', 1, N'GNB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1427', 1, N'ALLTEC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1428', 1, N'APC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1429', 1, N'GE');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1430', 1, N'CSB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1431', 1, N'ASCO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1432', 1, N'美美');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1433', 1, N'南昌通用');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1434', 1, N'南京奥联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1435', 1, N'南京大全');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1436', 1, N'南京冠亚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1437', 1, N'南京海虹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1438', 1, N'南京华脉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1439', 1, N'佳力图');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1440', 1, N'南京雷安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1441', 1, N'曼奈克斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1442', 1, N'南京欧陆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1443', 1, N'南京普天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1444', 1, N'南京天加');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1445', 1, N'南京天溯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1446', 1, N'新奇能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1447', 1, N'南宁奥泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1448', 1, N'南宁山普力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1449', 1, N'南洋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1450', 1, N'能威');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1451', 1, N'奥克斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1452', 1, N'宁波创隆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1453', 1, N'宁波立新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1454', 1, N'宁波隆兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1455', 1, N'宁波耐吉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1456', 1, N'宁波欧日力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1457', 1, N'宁波三正');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1458', 1, N'宁波高云');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1459', 1, N'宁波天安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1460', 1, N'宁德时代');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1461', 1, N'平顶山天籁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1462', 1, N'平顶山豫电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1463', 1, N'珀金斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1464', 1, N'钱江亿江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1465', 1, N'青岛海霸');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1466', 1, N'青海海西');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1467', 1, N'清华阳光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1468', 1, N'泉州赛特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1469', 1, N'丰泽巨虹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1470', 1, N'泉州顺控');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1471', 1, N'泉州万泉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1472', 1, N'罗宾');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1473', 1, N'日新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1474', 1, N'瑞昌哥尔德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1475', 1, N'VOLVO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1476', 1, N'塞瓦特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1477', 1, N'三变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1478', 1, N'三菱');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1479', 1, N'三菱重工');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1480', 1, N'三门峡富达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1481', 1, N'三洋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1482', 1, N'森林海');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1483', 1, N'衡远致业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1484', 1, N'科华恒盛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1485', 1, N'爱维达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1486', 1, N'厦门特力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1487', 1, N'山东华日');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1488', 1, N'山东辉煌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1489', 1, N'山东金曼克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1490', 1, N'山东科普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1491', 1, N'山东力诺');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1492', 1, N'山东润峰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1493', 1, N'山东圣阳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1494', 1, N'山东时风');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1495', 1, N'山东兆宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1496', 1, N'甄城宏博');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1497', 1, N'深圳山特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1498', 1, N'山西樊氏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1499', 1, N'吉天利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1500', 1, N'山西诺高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1501', 1, N'山开成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1502', 1, N'山开兆通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1503', 1, N'太原机械');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1504', 1, N'通同电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1505', 1, N'陕西德赛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1506', 1, N'陕西海跃');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1507', 1, N'陕西金山');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1508', 1, N'陕西普声');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1509', 1, N'汕头华通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1510', 1, N'汕头金凤');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1511', 1, N'汕头电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1512', 1, N'汕头南方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1513', 1, N'汕头益通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1514', 1, N'上海艾博');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1515', 1, N'上海澳通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1516', 1, N'上海宝临');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1517', 1, N'上海贝电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1518', 1, N'上海超韩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1519', 1, N'上海当宁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1520', 1, N'东洲罗顿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1521', 1, N'上海法玫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1522', 1, N'上海法诺格');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1523', 1, N'上海飞洲');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1524', 1, N'复华保护神');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1525', 1, N'上海广电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1526', 1, N'上海海滨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1527', 1, N'上海航天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1528', 1, N'上海合普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1529', 1, N'上海恒锦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1530', 1, N'上海沪工');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1531', 1, N'上海华东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1532', 1, N'上海华冠');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1533', 1, N'华夏恒业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1534', 1, N'上海华银');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1535', 1, N'上海辉龙');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1536', 1, N'嘉定朝阳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1537', 1, N'上海精益');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1538', 1, N'上海康诚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1539', 1, N'上海科泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1540', 1, N'上海良信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1541', 1, N'上海攀业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1542', 1, N'上海侨光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1543', 1, N'上海青辰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1544', 1, N'青浦新联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1545', 1, N'人民电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1546', 1, N'上海四通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1547', 1, N'上陶电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1548', 1, N'上海天灵');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1549', 1, N'上海天讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1550', 1, N'上海通普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1551', 1, N'上海通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1552', 1, N'上海拓中');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1553', 1, N'上海西岱尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1554', 1, N'上海西恩迪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1555', 1, N'上海鑫通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1556', 1, N'易捷瑞弗');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1557', 1, N'上海永鸣');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1558', 1, N'上海煜菱');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1559', 1, N'上海运图');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1560', 1, N'上海织科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1561', 1, N'上海致远');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1562', 1, N'上海中发');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1563', 1, N'中兴派能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1564', 1, N'绍兴成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1565', 1, N'绍兴咸亨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1566', 1, N'奥特迅');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1567', 1, N'深圳榜样');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1568', 1, N'碧辟佳阳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1569', 1, N'大族能联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1570', 1, N'东方泰和');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1571', 1, N'深圳盾牌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1572', 1, N'深圳富电康');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1573', 1, N'VAPEL');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1574', 1, N'深圳华晨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1575', 1, N'华海力达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1576', 1, N'锦天乐');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1577', 1, N'康普盾');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1578', 1, N'科士达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1579', 1, N'理士奥');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1580', 1, N'深圳日海');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1581', 1, N'深圳实九');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1582', 1, N'深圳世纪人');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1583', 1, N'艾苏威尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1584', 1, N'比亚迪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1585', 1, N'德泰法亚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1586', 1, N'深圳福尔成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1587', 1, N'深圳光辉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1588', 1, N'深圳国耀');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1589', 1, N'深圳海能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1590', 1, N'深圳恒通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1591', 1, N'深圳华宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1592', 1, N'今星光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1593', 1, N'金威源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1594', 1, N'深圳康益');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1595', 1, N'深圳科信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1596', 1, N'力达唯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1597', 1, N'能联壳牌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1598', 1, N'日恒利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1599', 1, N'深圳瑞达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1600', 1, N'瑞业通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1601', 1, N'赛瓦特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1602', 1, N'深北电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1603', 1, N'四方易联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1604', 1, N'深圳特发');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1605', 1, N'伟林锦龙');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1606', 1, N'深圳物润');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1607', 1, N'雄韬+B884');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1608', 1, N'深圳怡昌');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1609', 1, N'意华宝');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1610', 1, N'银翔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1611', 1, N'英维克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1612', 1, N'深圳索瑞德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1613', 1, N'深圳万里');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1614', 1, N'深圳沃特玛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1615', 1, N'深圳艺朴露');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1616', 1, N'深圳远征');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1617', 1, N'中兴力维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1618', 1, N'中远通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1619', 1, N'神州巨电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1620', 1, N'沈阳电业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1621', 1, N'沈阳东北');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1622', 1, N'沈阳东方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1623', 1, N'沈阳华利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1624', 1, N'沈阳科海');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1625', 1, N'沈阳天和');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1626', 1, N'沈阳天维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1627', 1, N'沈阳通运');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1628', 1, N'沈阳兴顺达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1629', 1, N'沈阳永成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1630', 1, N'沈阳远大');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1631', 1, N'沈阳中兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1632', 1, N'圣特立');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1633', 1, N'盛道');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1634', 1, N'施耐德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1635', 1, N'科林电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1636', 1, N'DANCO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1637', 1, N'STULZ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1638', 1, N'四川达卡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1639', 1, N'四川东方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1640', 1, N'四川铭士');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1641', 1, N'四川射洪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1642', 1, N'四川晶源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1643', 1, N'辛普森');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1644', 1, N'四川斯瑞奇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1645', 1, N'先进嘉诚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1646', 1, N'长虹电源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1647', 1, N'四川中光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1648', 1, N'松下');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1649', 1, N'苏驼');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1650', 1, N'苏州华波');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1651', 1, N'苏州杰成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1652', 1, N'新海宜');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1653', 1, N'新宏博');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1654', 1, N'苏州耀华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1655', 1, N'SOCOMEC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1656', 1, N'太原电力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1657', 1, N'太原伟业');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1658', 1, N'泰豪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1659', 1, N'THOMSON');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1660', 1, N'汤浅');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1661', 1, N'天开三厂');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1662', 1, N'天津东信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1663', 1, N'天津海洋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1664', 1, N'天津杰士');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1665', 1, N'天津津友');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1666', 1, N'天津康迪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1667', 1, N'天津力神');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1668', 1, N'天津联迅');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1669', 1, N'天津三源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1670', 1, N'河东电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1671', 1, N'天津朗照');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1672', 1, N'天津明通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1673', 1, N'天开成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1674', 1, N'天津协盛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1675', 1, N'天津特变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1676', 1, N'天津天利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1677', 1, N'天津中聚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1678', 1, N'天乐');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1679', 1, N'天水二一三');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1680', 1, N'通力盛达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1681', 1, N'威尔信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1682', 1, N'威海文隆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1683', 1, N'潍柴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1684', 1, N'潍坊泰吉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1685', 1, N'伟能机电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1686', 1, N'温州昌泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1687', 1, N'温州创立');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1688', 1, N'温州华高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1689', 1, N'温州开元');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1690', 1, N'温州麦克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1691', 1, N'温州上丰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1692', 1, N'无锡百发');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1693', 1, N'华源凯马');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1694', 1, N'开普');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1695', 1, N'无锡成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1696', 1, N'梁溪成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1697', 1, N'无锡星林');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1698', 1, N'芜湖生大');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1699', 1, N'高德红外');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1700', 1, N'武汉禾通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1701', 1, N'武汉虹信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1702', 1, N'武汉雷光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1703', 1, N'武汉力兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1704', 1, N'普天瑞海');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1705', 1, N'武汉普天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1706', 1, N'武汉维京');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1707', 1, N'现代高科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1708', 1, N'武汉鑫达通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1709', 1, N'武汉亿铢');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1710', 1, N'武汉银泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1711', 1, N'武汉长光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1712', 1, N'武汉长空');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1713', 1, N'武汉长兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1714', 1, N'西安安泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1715', 1, N'西安启源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1716', 1, N'西安前进');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1717', 1, N'西熔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1718', 1, N'西安瑞东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1719', 1, N'西开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1720', 1, N'西安中瑞');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1721', 1, N'西宁光明');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1722', 1, N'欣旺达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1723', 1, N'新疆奥奇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1724', 1, N'哈密巨轮');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1725', 1, N'奎开电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1726', 1, N'新疆双新');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1727', 1, N'新疆特变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1728', 1, N'新疆新能源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1729', 1, N'新疆新普能');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1730', 1, N'新泰达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1731', 1, N'新乡宝光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1732', 1, N'新乡中大');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1733', 1, N'新乡逐鹿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1734', 1, N'新郑中兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1735', 1, N'兴安邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1736', 1, N'许继');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1737', 1, N'雅马哈');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1738', 1, N'东方威思顿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1739', 1, N'顿汉布什');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1740', 1, N'烟台荏原');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1741', 1, N'烟台有利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1742', 1, N'阿波罗');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1743', 1, N'扬州劲贝');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1744', 1, N'扬州华东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1745', 1, N'扬子');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1746', 1, N'伊顿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1747', 1, N'伊莱克斯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1748', 1, N'伊蒙妮莎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1749', 1, N'依米康');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1750', 1, N'易达威锐');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1751', 1, N'逸仙微电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1752', 1, N'优力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1753', 1, N'奔马');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1754', 1, N'大皇冠');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1755', 1, N'霍克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1756', 1, N'英特吉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1757', 1, N'临潼维宁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1758', 1, N'余姚邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1759', 1, N'余姚天鸿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1760', 1, N'宇华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1761', 1, N'云南奥赛德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1762', 1, N'世通祥润');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1763', 1, N'云南天达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1764', 1, N'运城天兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1765', 1, N'张掖开关厂');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1766', 1, N'漳州科华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1767', 1, N'长城电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1768', 1, N'长春金色');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1769', 1, N'长沙道恒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1770', 1, N'业通达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1771', 1, N'长征电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1772', 1, N'肇庆华南');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1773', 1, N'肇庆澳华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1774', 1, N'肇庆奇顶');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1775', 1, N'肇阳高开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1776', 1, N'浙江达讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1777', 1, N'浙江大立');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1778', 1, N'浙电成套');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1779', 1, N'浙电器材');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1780', 1, N'浙江鼎联科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1781', 1, N'浙江东方');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1782', 1, N'浙江盾安');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1783', 1, N'浙江飞跃');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1784', 1, N'杭电控制');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1785', 1, N'杭开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1786', 1, N'浙江佳贝思');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1787', 1, N'金华电开');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1788', 1, N'浙江金之路');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1789', 1, N'南都');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1790', 1, N'钱江股份');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1791', 1, N'浙江赛福');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1792', 1, N'浙江三辰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1793', 1, N'浙江天正');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1794', 1, N'卧龙灯塔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1795', 1, N'兴海能源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1796', 1, N'浙江正泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1797', 1, N'浙江中高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1798', 1, N'镇江鼎圣');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1799', 1, N'默勒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('18', 1, N'富士通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1800', 1, N'镇江亚东');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1801', 1, N'郑州创成');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1802', 1, N'佛光');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1803', 1, N'郑州众智');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1804', 1, N'中达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1805', 1, N'德力西');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1806', 1, N'中国电子');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1807', 1, N'中科器材');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1808', 1, N'中国沈通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1809', 1, N'中国特变');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1810', 1, N'西安翰德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1811', 1, N'中电光伏');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1812', 1, N'中俊电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1813', 1, N'中科恒源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1814', 1, N'中瑞电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1815', 1, N'大洋');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1816', 1, N'中山冠虹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1817', 1, N'中山明阳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1818', 1, N'中山恩留宁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1819', 1, N'中兴能源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1820', 1, N'中邮科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1821', 1, N'中州电气');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1822', 1, N'重庆博联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1823', 1, N'重庆博森');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1824', 1, N'重庆成科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1825', 1, N'重庆大江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1826', 1, N'重庆大顺');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1827', 1, N'重庆迪科');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1828', 1, N'重庆东电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1829', 1, N'重庆东诺');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1830', 1, N'重庆赋仁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1831', 1, N'重庆果林');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1832', 1, N'重庆恒联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1833', 1, N'重庆金华');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1834', 1, N'重庆凯尔特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1835', 1, N'重庆凯米尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1836', 1, N'重庆康茂');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1837', 1, N'重庆力帆');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1838', 1, N'重庆隆鑫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1839', 1, N'重庆民生');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1840', 1, N'普天博威');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1841', 1, N'重庆三鼎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1842', 1, N'重庆世达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1843', 1, N'重庆盾德');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1844', 1, N'重庆斯拜逊');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1845', 1, N'重庆万里');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1846', 1, N'重庆望江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1847', 1, N'重庆祥泰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1848', 1, N'重庆新世纪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1849', 1, N'重庆耀虎');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1850', 1, N'重庆长江');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1851', 1, N'重庆长寿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1852', 1, N'重庆致维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1853', 1, N'重庆智仁');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1854', 1, N'重庆中创纪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1855', 1, N'重庆众恒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1856', 1, N'重变电器');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1857', 1, N'重庆宗申');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1858', 1, N'格力');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1859', 1, N'珠海光乐');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1860', 1, N'珠海金电');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1861', 1, N'南方华力通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1862', 1, N'珠海铨高');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1863', 1, N'珠海瑞特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1864', 1, N'珠海赛迪生');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1865', 1, N'珠海丰兰');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1866', 1, N'珠海利恒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1867', 1, N'珠海泰坦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1868', 1, N'珠海盈源');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1869', 1, N'中普防雷');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1870', 1, N'驻马店胜利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1871', 1, N'驻马店华宇');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1872', 1, N'淄博火炬');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('19', 1, N'安耐特');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('21', 1, N'艾默生');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('22', 1, N'高新兴');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('23', 1, N'动环其它');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('24', 1, N'烽火');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('26', 1, N'大唐');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('27', 1, N'新邮通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('28', 1, N'普天');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('29', 1, N'ABB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('30', 1, N'MTU');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('31', 1, N'傲天动联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('32', 1, N'邦讯');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('36', 1, N'东方信联');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('37', 1, N'国人');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('38', 1, N'杭州志诚');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('39', 1, N'弘浩明传');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('41', 1, N'华三');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('42', 1, N'杰赛');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('43', 1, N'京信通信');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('44', 1, N'卡特比勒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('45', 1, N'开利');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('46', 1, N'康维');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('47', 1, N'联信永益');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('48', 1, N'明华澳汉');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('49', 1, N'三元达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('50', 1, N'陕西新通');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('52', 1, N'四川宏邦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('53', 1, N'特灵');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('55', 1, N'威士达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('57', 1, N'星网锐捷');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('58', 1, N'约克');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('59', 1, N'智达康');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('60', 1, N'中恒');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('61', 1, N'中太');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('62', 1, N'天波');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('63', 1, N'佳和');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('64', 1, N'瑞斯康达');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('65', 1, N'格林威尔');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('66', 1, N'甲骨文');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('80', 1, N'亿阳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('81', 1, N'浪潮');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('82', 1, N'神州泰岳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('99', 1, N'南京网元');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Brand]
DELETE FROM [dbo].[C_Brand];
GO

INSERT INTO [dbo].[C_Brand]([ID],[Enabled],[Name],[ProductorID]) VALUES('00', 1, N'默认品牌', '00');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_RoomType]
DELETE FROM [dbo].[C_RoomType];
GO

INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('00', N'未定义');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('01', N'汇聚机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('02', N'基站机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('11', N'发电机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('12', N'电力机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('13', N'电池机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('14', N'空调机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('51', N'传输机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('52', N'交换机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('53', N'数据机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('54', N'IDC机房');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('55', N'综合机房');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_SCVendor]
DELETE FROM [dbo].[C_SCVendor]
GO

INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('01', N'中兴');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('02', N'艾默生');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('03', N'中达');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('04', N'盈佳');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('05', N'威克姆');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('06', N'四维安通');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('07', N'大唐');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('08', N'瑞笛恩');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('09', N'高新兴');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('10', N'康大奈特');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('11', N'合广');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('12', N'业通达');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('13', N'上海长电');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('14', N'佳讯');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('15', N'大诚');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('16', N'电源中心');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('17', N'移联信达');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('19', N'邦讯');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('20', N'亚澳');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('21', N'天河');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('22', N'南方');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('23', N'栅格');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('24', N'世纪瑞尔');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('25', N'大华');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('26', N'亿阳');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('27', N'江苏云博');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('28', N'艾赛');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('29', N'华为');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('70', N'华创');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('71', N'卫东');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('72', N'创立');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('73', N'恒成');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('74', N'天羚');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('75', N'维创');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('76', N'鑫天兴');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('77', N'维安');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('78', N'美迪特');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('79', N'大光明');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('99', N'中国铁塔');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_StationType]
DELETE FROM [dbo].[C_StationType];
GO

INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('1', N'数据中心');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('2', N'通信机楼');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('3', N'传输节点');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('4', N'通信基站');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_SubCompany]
DELETE FROM [dbo].[C_SubCompany];
GO

INSERT INTO [dbo].[C_SubCompany]([ID],[Enabled],[Name]) VALUES('00', 1, N'默认代维公司');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_SubDeviceType]
DELETE FROM [dbo].[C_SubDeviceType];
GO

INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0' ,'0', N'未定义');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0101' ,'01', N'高压进线柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0102' ,'01', N'高压计量柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0103' ,'01', N'高压避雷器柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0104' ,'01', N'高压隔离柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0105' ,'01', N'高压升高柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0106' ,'01', N'高压压变柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0107' ,'01', N'高压母联柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0108' ,'01', N'高压出线柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0109' ,'01', N'高压电容补偿柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0110' ,'01', N'高压油机配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0111' ,'01', N'高压切换柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0112' ,'01', N'高压操作电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0201' ,'02', N'低压进线柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0202' ,'02', N'低压进线计量柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0203' ,'02', N'低压计量柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0204' ,'02', N'低压出线柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0205' ,'02', N'低压联络柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0206' ,'02', N'低压ATS切换柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0207' ,'02', N'低压电容补偿柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0208' ,'02', N'低压油机配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0209' ,'02', N'应急油机接入箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0210' ,'02', N'应急油机接入柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0211' ,'02', N'低压楼层配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0212' ,'02', N'低压交流配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0213' ,'02', N'开关电源输入柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0214' ,'02', N'UPS输入柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0215' ,'02', N'高压直流输入柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0216' ,'02', N'市电交流配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0217' ,'02', N'独立防雷箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0218' ,'02', N'有源滤波设备');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0219' ,'02', N'无源滤波设备');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0301' ,'03', N'干式变压器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0302' ,'03', N'油浸变压器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0303' ,'03', N'非晶合金变压器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0304' ,'03', N'调压器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0401' ,'04', N'24V直流配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0402' ,'04', N'-48V直流配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0403' ,'04', N'-48V直流列头柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0404' ,'04', N'-48V直流配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0501' ,'05', N'柴油发电机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0502' ,'05', N'燃气轮发电机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0503' ,'05', N'汽油发电机');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0504' ,'05', N'燃料电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0601' ,'06', N'分立开关电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0602' ,'06', N'组合开关电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0603' ,'06', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0604' ,'06', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0701' ,'07', N'UPS铅酸电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0702' ,'07', N'开关电源铅酸电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0703' ,'07', N'高压直流铅酸电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0704' ,'07', N'操作电源铅酸电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0801' ,'08', N'工频UPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0802' ,'08', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0803' ,'08', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0901' ,'09', N'UPS输出配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0902' ,'09', N'UPS输出列头柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0903' ,'09', N'UPS输出配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1101' ,'11', N'风冷专用空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1102' ,'11', N'水冷专用空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1103' ,'11', N'双冷源专用空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1201' ,'12', N'冷冻水专用空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1202' ,'12', N'热管背板');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1203' ,'12', N'水冷前门');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1204' ,'12', N'水冷后门');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1205' ,'12', N'列间空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1206' ,'12', N'嵌入式空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1301' ,'13', N'多联机中央空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1302' ,'13', N'风冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1303' ,'13', N'水冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1304' ,'13', N'高压离心冷水机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1305' ,'13', N'低压离心冷水机组');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1306' ,'13', N'逆流冷却塔');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1307' ,'13', N'横流冷却塔');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1401' ,'14', N'逆变器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1402' ,'14', N'DC/DC设备');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1403' ,'14', N'直流远供近端');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1404' ,'14', N'直流远供远端');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1501' ,'15', N'柜式空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1502' ,'15', N'壁挂式空调');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1601' ,'16', N'极早期烟感');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1701' ,'17', N'温度');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1702' ,'17', N'湿度');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1703' ,'17', N'水浸');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1704' ,'17', N'烟感');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1705' ,'17', N'红外');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1706' ,'17', N'玻破');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1707' ,'17', N'门磁');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1708' ,'17', N'摄像机');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1801' ,'18', N'电池恒温箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6801' ,'68', N'UPS锂电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6802' ,'68', N'开关电源锂电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6803' ,'68', N'高压直流锂电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6804' ,'68', N'操作电源锂电池');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7601' ,'76', N'CSC');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7602' ,'76', N'LSC');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7603' ,'76', N'FSU');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7604' ,'76', N'蓄电池采集设备');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7701' ,'77', N'智能通风系统');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7702' ,'77', N'智能换热系统');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7801' ,'78', N'风力控制器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7802' ,'78', N'太阳能控制器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7803' ,'78', N'风光互补控制器');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8701' ,'87', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8702' ,'87', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8703' ,'87', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8704' ,'87', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8801' ,'88', N'240V直流配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8802' ,'88', N'240V直流列头柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8803' ,'88', N'240V直流配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8804' ,'88', N'336V直流配电柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8805' ,'88', N'336V直流列头柜');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8806' ,'88', N'336V直流配电箱');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9201' ,'92', N'交流智能电表');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9202' ,'92', N'直流智能电表');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9301' ,'93', N'智能门禁');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_SubLogicType]
DELETE FROM [dbo].[C_SubLogicType];
GO

INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('0' ,'0', N'未定义');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010101' ,'0101', N'高压进线柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010102' ,'0101', N'高压计量柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010103' ,'0101', N'高压避雷器柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010104' ,'0101', N'高压隔离柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010105' ,'0101', N'高压升高柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010106' ,'0101', N'高压压变柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010107' ,'0101', N'高压母联柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010108' ,'0101', N'高压出线柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010109' ,'0101', N'高压电容补偿柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010110' ,'0101', N'高压油机配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010111' ,'0101', N'高压切换柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010201' ,'0102', N'高压操作电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020101' ,'0201', N'低压进线柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020102' ,'0201', N'低压进线计量柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020103' ,'0201', N'低压计量柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020104' ,'0201', N'低压出线柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020105' ,'0201', N'低压联络柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020106' ,'0201', N'低压ATS切换柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020107' ,'0201', N'低压油机配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020108' ,'0201', N'应急油机接入箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020109' ,'0201', N'应急油机接入柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020110' ,'0201', N'低压楼层配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020111' ,'0201', N'低压交流配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020112' ,'0201', N'开关电源输入柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020113' ,'0201', N'UPS输入柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020114' ,'0201', N'高压直流输入柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020115' ,'0201', N'市电交流配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020116' ,'0201', N'独立防雷箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020201' ,'0202', N'低压电容补偿柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020301' ,'0203', N'有源滤波设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020302' ,'0203', N'无源滤波设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030101' ,'0301', N'干式变压器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030102' ,'0301', N'油浸变压器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030103' ,'0301', N'非晶合金变压器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030104' ,'0301', N'调压器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040101' ,'0401', N'24V直流配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040102' ,'0401', N'-48V直流配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040103' ,'0401', N'-48V直流列头柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040104' ,'0401', N'-48V直流配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050101' ,'0501', N'柴油发电机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050102' ,'0501', N'燃气轮发电机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050103' ,'0501', N'汽油发电机');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050201' ,'0502', N'燃料电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060101' ,'0601', N'分立开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060102' ,'0601', N'组合开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060103' ,'0601', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060104' ,'0601', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060201' ,'0602', N'分立开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060202' ,'0602', N'组合开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060203' ,'0602', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060204' ,'0602', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060301' ,'0603', N'分立开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060302' ,'0603', N'组合开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060303' ,'0603', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060304' ,'0603', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060401' ,'0604', N'分立开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060402' ,'0604', N'组合开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060403' ,'0604', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060404' ,'0604', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060501' ,'0605', N'分立开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060502' ,'0605', N'组合开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060503' ,'0605', N'壁挂开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060504' ,'0605', N'嵌入开关电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070101' ,'0701', N'UPS铅酸电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070102' ,'0701', N'开关电源铅酸电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070103' ,'0701', N'高压直流铅酸电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070104' ,'0701', N'操作电源铅酸电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080101' ,'0801', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080102' ,'0801', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080103' ,'0801', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080201' ,'0802', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080202' ,'0802', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080203' ,'0802', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080301' ,'0803', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080302' ,'0803', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080303' ,'0803', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080401' ,'0804', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080402' ,'0804', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080403' ,'0804', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080501' ,'0805', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080502' ,'0805', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080503' ,'0805', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080601' ,'0806', N'工频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080602' ,'0806', N'一体化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080603' ,'0806', N'模块化高频UPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090101' ,'0901', N'UPS输出配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090102' ,'0901', N'UPS输出列头柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090103' ,'0901', N'UPS输出配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110101' ,'1101', N'风冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110102' ,'1101', N'水冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110103' ,'1101', N'双冷源专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110201' ,'1102', N'风冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110202' ,'1102', N'水冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110203' ,'1102', N'双冷源专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110301' ,'1103', N'风冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110302' ,'1103', N'水冷专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110303' ,'1103', N'双冷源专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120101' ,'1201', N'冷冻水专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120102' ,'1201', N'热管背板');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120103' ,'1201', N'水冷前门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120104' ,'1201', N'水冷后门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120105' ,'1201', N'列间空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120106' ,'1201', N'嵌入式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120201' ,'1202', N'冷冻水专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120202' ,'1202', N'热管背板');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120203' ,'1202', N'水冷前门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120204' ,'1202', N'水冷后门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120205' ,'1202', N'列间空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120206' ,'1202', N'嵌入式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120301' ,'1203', N'冷冻水专用空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120302' ,'1203', N'热管背板');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120303' ,'1203', N'水冷前门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120304' ,'1203', N'水冷后门');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120305' ,'1203', N'列间空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120306' ,'1203', N'嵌入式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130101' ,'1301', N'多联机中央空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130102' ,'1301', N'风冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130103' ,'1301', N'水冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130104' ,'1301', N'高压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130105' ,'1301', N'低压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130106' ,'1301', N'逆流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130107' ,'1301', N'横流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130201' ,'1302', N'多联机中央空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130202' ,'1302', N'风冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130203' ,'1302', N'水冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130204' ,'1302', N'高压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130205' ,'1302', N'低压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130206' ,'1302', N'逆流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130207' ,'1302', N'横流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130301' ,'1303', N'多联机中央空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130302' ,'1303', N'风冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130303' ,'1303', N'水冷螺杆冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130304' ,'1303', N'高压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130305' ,'1303', N'低压离心冷水机组');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130306' ,'1303', N'逆流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130307' ,'1303', N'横流冷却塔');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140101' ,'1401', N'逆变器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140102' ,'1401', N'DC/DC设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140103' ,'1401', N'直流远供近端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140104' ,'1401', N'直流远供远端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140201' ,'1402', N'逆变器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140202' ,'1402', N'DC/DC设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140203' ,'1402', N'直流远供近端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140204' ,'1402', N'直流远供远端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140301' ,'1403', N'逆变器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140302' ,'1403', N'DC/DC设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140303' ,'1403', N'直流远供近端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140304' ,'1403', N'直流远供远端');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150101' ,'1501', N'柜式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150102' ,'1501', N'壁挂空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150201' ,'1502', N'柜式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150202' ,'1502', N'壁挂空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150301' ,'1503', N'柜式空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150302' ,'1503', N'壁挂空调');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('160101' ,'1601', N'极早期烟感');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('160201' ,'1602', N'极早期烟感');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170101' ,'1701', N'水浸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170201' ,'1702', N'烟雾');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170301' ,'1703', N'红外');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170401' ,'1704', N'温度');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170501' ,'1705', N'湿度');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170601' ,'1706', N'门碰');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170701' ,'1707', N'玻破');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170801' ,'1708', N'震动');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170901' ,'1709', N'被盗');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('171001' ,'1710', N'摄像机');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('180101' ,'1801', N'电池恒温箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680101' ,'6801', N'UPS锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680102' ,'6801', N'开关电源锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680103' ,'6801', N'高压直流锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680104' ,'6801', N'操作电源锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680201' ,'6802', N'UPS锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680202' ,'6802', N'开关电源锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680203' ,'6802', N'高压直流锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680204' ,'6802', N'操作电源锂电池');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760101' ,'7601', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760102' ,'7601', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760103' ,'7601', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760104' ,'7601', N'蓄电池采集设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760201' ,'7602', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760202' ,'7602', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760203' ,'7602', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760204' ,'7602', N'蓄电池采集设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760301' ,'7603', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760302' ,'7603', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760303' ,'7603', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760304' ,'7603', N'蓄电池采集设备');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770101' ,'7701', N'智能通风系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770102' ,'7701', N'智能换热系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770201' ,'7702', N'智能通风系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770202' ,'7702', N'智能换热系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770301' ,'7703', N'智能通风系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770302' ,'7703', N'智能换热系统');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780101' ,'7801', N'风力控制器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780102' ,'7801', N'太阳能控制器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780103' ,'7801', N'风光互补控制器');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870101' ,'8701', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870102' ,'8701', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870103' ,'8701', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870104' ,'8701', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870201' ,'8702', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870202' ,'8702', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870203' ,'8702', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870204' ,'8702', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870301' ,'8703', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870302' ,'8703', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870303' ,'8703', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870304' ,'8703', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870401' ,'8704', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870402' ,'8704', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870403' ,'8704', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870404' ,'8704', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870501' ,'8705', N'分立高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870502' ,'8705', N'组合高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870503' ,'8705', N'壁挂高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870504' ,'8705', N'嵌入高压直流电源');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880101' ,'8801', N'240V直流配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880102' ,'8801', N'240V直流列头柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880103' ,'8801', N'240V直流配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880104' ,'8801', N'336V直流配电柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880105' ,'8801', N'336V直流列头柜');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880106' ,'8801', N'336V直流配电箱');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920101' ,'9201', N'交流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920102' ,'9201', N'直流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920201' ,'9202', N'交流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920202' ,'9202', N'直流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920301' ,'9203', N'交流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920302' ,'9203', N'直流智能电表');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930101' ,'9301', N'智能门禁');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930201' ,'9302', N'智能门禁');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930301' ,'9303', N'智能门禁');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Supplier]
DELETE FROM [dbo].[C_Supplier];
GO

INSERT INTO [dbo].[C_Supplier]([ID],[Enabled],[Name]) VALUES('00', 1, N'默认供应商');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_EnumMethods]
DELETE FROM [dbo].[C_EnumMethods]
GO

INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1001, 1, 1, N'省', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1002, 2, 1, N'市', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1003, 3, 1, N'县(区)', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2001, 1, 2, N'地埋', N'市电引入方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2002, 2, 2, N'架空', N'市电引入方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2003, 1, 2, N'转供', N'供电性质');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2004, 2, 2, N'直供', N'供电性质');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3001, 1, 3, N'自建', N'产权');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3002, 2, 3, N'购买', N'产权');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3003, 3, 3, N'租用', N'产权');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4001, 1, 4, N'禁用', N'权限');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4002, 2, 4, N'只读', N'权限');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4003, 3, 4, N'读写', N'权限');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5001, 1, 5, N'N+1(N>1)', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5002, 2, 5, N'1+1主从', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5003, 3, 5, N'单机1+1', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5004, 4, 5, N'均分', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5005, 5, 5, N'双母线', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(6001, 1, 6, N'干式', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(6002, 2, 6, N'油浸式', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7001, 1, 7, N'自动启动', N'启动方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7002, 2, 7, N'手动启动', N'启动方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7003, 1, 7, N'风冷', N'冷却方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7004, 2, 7, N'水冷', N'冷却方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(8001, 1, 8, N'水平轴', N'风机类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(8002, 2, 8, N'垂直轴', N'风机类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9001, 1, 9, N'三相', N'端子极数');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9002, 2, 9, N'单相', N'端子极数');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9003, 1, 9, N'直流', N'交直流方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9004, 2, 9, N'交流', N'交直流方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10001, 1, 10, N'自购', N'来源');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10002, 2, 10, N'租用', N'来源');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10003, 1, 10, N'柴油机', N'油机种类');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10004, 2, 10, N'汽油机', N'油机种类');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10005, 1, 10, N'单相', N'相数');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10006, 2, 10, N'三相', N'相数');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11001, 1, 11, N'活塞式', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11002, 2, 11, N'涡旋式', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11003, 3, 11, N'单螺杆式', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11004, 4, 11, N'双螺杆式', N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(12001, 1, 12, N'油机与市电', N'切换对象');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(12002, 2, 12, N'油机与油机', N'切换对象');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13001, 0, 13, N'在用-良好', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13002, 1, 13, N'在用-故障', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13003, 2, 13, N'闲置-良好', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13004, 3, 13, N'闲置-故障', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13005, 4, 13, N'返修', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13006, 5, 13, N'报废', N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13007, 0, 13, N'默认', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13008, 51, 13, N'高压配电系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13009, 52, 13, N'低压配电系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13010, 53, 13, N'UPS系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13011, 54, 13, N'开关电源系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13012, 55, 13, N'高压直流系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13013, 56, 13, N'发电系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13014, 57, 13, N'中央空调系统', N'系统名称');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14001, 1, 14, N'>', N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14002, 2, 14, N'<', N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14003, 3, 14, N'=', N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14004, 4, 14, N'!=', N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15001, 0, 15, N'高中', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15002, 1, 15, N'大专', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15003, 2, 15, N'本科', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15004, 3, 15, N'研究生', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15005, 4, 15, N'博士或博士后', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15006, 5, 15, N'其他', N'学历');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15007, 0, 15, N'未婚', N'婚姻状况');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15008, 1, 15, N'已婚', N'婚姻状况');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15009, 2, 15, N'其他', N'婚姻状况');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16001, 0, 16, N'移动B接口', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16002, 1, 16, N'电信B接口', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16003, 2, 16, N'联通B接口', N'类型');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16004, 3, 16, N'自有接口', N'类型');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[Sys_Menu]
DELETE FROM [dbo].[Sys_Menu]
GO

INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('1', 1, '0', 1, N'中心资产', 'Assets.png', 'Asset_Panel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('2', 1, '0', 2, N'设备工具', 'Tools.png', '', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('3', 1, '0', 3, N'标准字典', 'Books.png', '', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('4', 1, '0', 4, N'系统管理', 'Sys.png', '', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('5', 1, '0', 5, N'门禁管理', 'Door.png', NULL, 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('6', 1, '0', 6, N'视频管理', 'Video.png', 'Video_View', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('100', 1, '1', 1, N'区域', 'Area.png', 'Area_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('101', 1, '1', 1, N'站点', 'Station.png', 'Station_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('102', 1, '1', 1, N'机房', 'Room.png', 'Room_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('103', 1, '1', 1, N'设备', 'Device.png', 'Device_Panel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('104', 1, '1', 1, N'模板', 'Protocol.png', 'Protocol_View', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('105', 1, '3', 3, N'标准信号', 'Point.png', 'Point_View', 8, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('106', 1, '1', 1, N'FSU', 'Fsu.png', 'FSU_Panel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('107', 1, '1', 1, N'通讯', 'Bus.png', 'Bus_View', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('108', 1, '107', 1, N'驱动', 'Driver.png', 'Driver_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('200', 1, '2', 2, N'生产厂家', 'DevTool.png', 'Productor_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('201', 1, '2', 2, N'品牌', 'DevTool.png', 'Brand_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('202', 1, '2', 2, N'供应商', 'DevTool.png', 'Supplier_GrdPanel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('203', 1, '2', 2, N'代维公司', 'DevTool.png', 'SubCompany_GrdPanel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('300', 1, '3', 3, N'站点类型', 'Book.png', 'StaType_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('301', 1, '3', 3, N'机房类型', 'Book.png', 'RoomType_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('302', 1, '3', 3, N'设备类型', 'Book.png', 'DevType_GrdPanel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('303', 1, '3', 3, N'设备子类', 'Book.png', 'SubDevType_GrdPanel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('304', 1, '3', 3, N'逻辑分类', 'Book.png', 'LogicType_GrdPanel', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('305', 1, '3', 3, N'逻辑子类', 'Book.png', 'SubLogicType_GrdPanel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('306', 1, '3', 3, N'单位状态', 'Book.png', 'Unit_GrdPanel', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('307', 1, '3', 3, N'枚举类型', 'Book.png', 'EnumMethod_GrdPanel', 9, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('308', 1, '3', 3, N'采集组群', 'Book.png', 'GroupForm', 10, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('401', 1, '4', 4, N'人员', 'Users.png', '', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('402', 1, '4', 4, N'角色', 'Role.png', 'Role_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('403', 1, '4', 4, N'账号', 'UID.png', 'User_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('404', 1, '4', 4, N'维护', 'Maintain.png', '', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('500', 1, '5', 5, N'时段', 'Time.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('501', 1, '5', 5, N'门卡', 'Card.png', 'Card_View', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('502', 1, '5', 5, N'控制器', 'accessControl.png', NULL, 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('600', 1, '6', 6, N'摄影机', 'Camera.png', 'Camera_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10301', 1, '103', 1, N'UPS', 'DevType.png', 'UPS_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10302', 1, '103', 1, N'变压器', 'DevType.png', 'Transformer_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10303', 1, '103', 1, N'电池恒温箱', 'DevType.png', 'BattTempBox_Panel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10304', 1, '103', 1, N'发电机组', 'DevType.png', 'GeneratorGroup_Panel', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10305', 1, '103', 1, N'分列开关电源系统', 'DevType.png', 'DivSwitElecSour_Panel', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10306', 1, '103', 1, N'组合开关电源系统', 'DevType.png', 'CombSwitElecSour_Panel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10307', 1, '103', 1, N'风电控制器', 'DevType.png', 'WindPowerCon_Panel', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10308', 1, '103', 1, N'风光互补控制器', 'DevType.png', 'WindLightCompCon_Panel', 8, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10309', 1, '103', 1, N'风能设备', 'DevType.png', 'WindEnerEqui_Panel', 9, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10310', 1, '103', 1, N'高压配电柜', 'DevType.png', 'HighVoltDistBox_Panel', 10, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10311', 1, '103', 1, N'换热系统', 'DevType.png', 'ChangeHeat_Panel', 11, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10312', 1, '103', 1, N'交流配电箱', 'DevType.png', 'ACDistBox_Panel', 12, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10313', 1, '103', 1, N'开关熔丝', 'DevType.png', 'SwitchFuse_Panel', 24, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10314', 1, '103', 1, N'逆变器', 'DevType.png', 'Inverter_Panel', 14, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10315', 1, '103', 1, N'普通空调', 'DevType.png', 'OrdiAirCond_Panel', 15, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10316', 1, '103', 1, N'低压配电柜', 'DevType.png', 'LowDistCabinet_Panel', 16, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10317', 1, '103', 1, N'太阳能控制器', 'DevType.png', 'SolarController_Panel', 17, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10318', 1, '103', 1, N'太阳能设备', 'DevType.png', 'SolarEqui_Panel', 18, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10319', 1, '103', 1, N'通风系统', 'DevType.png', 'Ventilation_Panel', 19, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10320', 0, '103', 1, N'监控设备', 'DevType.png', '', 20, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10321', 1, '103', 1, N'稳压器', 'DevType.png', 'Manostat_Panel', 21, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10322', 1, '103', 1, N'蓄电池组', 'DevType.png', 'BattGroup_Panel', 22, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10323', 1, '103', 1, N'移动发电机', 'DevType.png', 'MobiGenerator_Panel', 23, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10324', 1, '103', 1, N'直流配电箱', 'DevType.png', 'DCDistBox_Panel', 13, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10325', 1, '103', 1, N'中央空调风柜系统', 'DevType.png', 'AirCondWindCabi_Panel', 25, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10326', 1, '103', 1, N'中央空调风冷却系统', 'DevType.png', 'AirCondWindCool_Panel', 26, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10327', 1, '103', 1, N'中央空调主机系统', 'DevType.png', 'AirCondHost_Panel', 27, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10328', 1, '103', 1, N'专用空调', 'DevType.png', 'SpecAirCond_Panel', 28, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10329', 1, '103', 1, N'自动电源切换柜', 'DevType.png', 'ElecSourCabi_Panel', 29, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10500', 1, '105', 1, N'参数', 'SubPoint.png', 'SubPoint_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40101', 1, '401', 2, N'职位', 'Duty.png', 'Duty_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40102', 1, '401', 2, N'部门', 'Department.png', 'Department_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40103', 1, '401', 2, N'员工', 'User.png', 'Employee_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40400', 1, '404', 4, N'升级', 'UpGrade.png', 'UpGrade_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40401', 0, '404', 4, N'冗余', 'Redundancy.png', '', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40402', 1, '404', 4, N'日志', 'UserLog.png', 'UserLog_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50001', 1, '500', 5, N'工作', 'workTime.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50002', 1, '500', 5, N'星期', 'workTime.png', NULL, 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50003', 1, '500', 5, N'红外', 'workTime.png', NULL, 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50004', 1, '500', 5, N'节假日', 'workTime.png', NULL, 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50005', 1, '500', 5, N'周末', 'workTime.png', NULL, 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50100', 1, '501', 5, N'外协', 'OutEmployee.png', 'OutEmployee_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50101', 1, '501', 5, N'发卡', 'bindCard.png', 'CardsInEmployee_View', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50102', 1, '501', 5, N'授权', 'cardGrand.png', 'AuthorizationCard_View', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50201', 1, '502', 5, N'授时', 'TimeGrand.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50202', 1, '502', 5, N'授权', 'cardGrand.png', 'DoorAuthorization_View', 1, GETDATE(), NULL);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Employee]
DELETE FROM [dbo].[U_Employee];
GO

INSERT INTO [dbo].[U_Employee]([ID],[Name],[EngName],[UsedName],[EmpNo],[DeptId],[DutyId],[ICardId],[Sex],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving],[EntryTime],[RetireTime],[IsFormal],[Remarks],[Enabled]) 
VALUES('00001', '默认员工', 'Default Employee', NULL, 'W00001', '001', '001', '310000198501010001', 0, '1985-01-01', 4, 1, N'中国', N'上海', N'江苏', N'上海市浦东新区', '200000', '68120000', '58660000', '13800138000', '13800138000@vip.com', NULL, 0, '2011-01-01', '2099-12-31' , 1, NULL, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_User]
DELETE FROM [dbo].[U_User];
GO

INSERT INTO [dbo].[U_User]([ID],[EmpID],[Enabled],[UID],[PWD],[PwdFormat],[PwdSalt],[RoleID],[OnlineTime],[LimitTime],[CreateTime],[LastLoginTime],[LastPwdChangedTime],[FailedPwdAttemptCount],[FailedPwdTime],[IsLockedOut],[LastLockoutTime],[Remark]) 
VALUES ('1', '00', 1, 'PECS', 'PECS@1234', 0, '', 0, GETDATE(), '2099-12-31 23:59:59', GETDATE(), GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE(), '');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Role]
DELETE FROM [dbo].[U_Role];
GO

INSERT INTO [dbo].[U_Role]([ID],[Enabled],[Name],[Authority],[Desc]) VALUES(0,1,'超级管理员','All','系统默认角色');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_MenusInRole]
DELETE FROM [dbo].[U_MenusInRole]
GO

INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '1');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '2');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '3');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '4');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '5');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '6');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '100');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '101');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '102');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '103');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '104');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '105');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '106');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '107');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '108');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '200');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '201');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '202');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '203');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '300');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '301');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '302');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '303');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '304');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '305');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '306');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '307');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '308');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '401');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '402');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '403');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '404');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '500');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '501');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '502');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '600');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10301');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10302');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10303');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10304');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10305');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10306');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10307');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10308');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10309');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10310');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10311');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10312');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10313');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10314');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10315');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10316');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10317');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10318');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10319');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10321');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10322');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10323');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10324');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10325');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10326');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10327');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10328');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10329');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '10500');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '40101');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '40102');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '40103');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '40400');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '40402');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50001');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50002');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50003');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50004');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50005');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50100');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50101');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50102');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50201');
INSERT INTO [dbo].[U_MenusInRole]([RoleID],[MenuID]) VALUES(0, '50202');
GO