/*
* P2R_V1 Database Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
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
	[Model] [varchar](20) NOT NULL,
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
	[Name] [varchar](100) NOT NULL,
	[Protocol] [int] NOT NULL,
	[Address] [int] NOT NULL,
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
	[DeviceID] [varchar](100) NULL,
	[GroupID] [varchar](100) NULL,
	[LimitType] [int] NULL,
	[LimitIndex] [int] NULL,
	[BeginTime] [datetime] NULL,
	[LimitTime] [datetime] NULL,
	[Pwd] [varchar](50) NULL,
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