/*
* P2R_V1 Database Script Library v1.1.2
* Copyright 2017, Delta
* Author: Guo.Jing
* Date: 2017/10/12
*/

USE [master]
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'P2R_V1')
CREATE DATABASE [P2R_V1]
GO

USE [P2R_V1]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[C_SubDeviceType]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubDeviceType_C_DeviceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]'))
ALTER TABLE [dbo].[C_SubDeviceType] DROP CONSTRAINT [FK_C_SubDeviceType_C_DeviceType]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[C_SubLogicType]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubLogicType_C_LogicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]'))
ALTER TABLE [dbo].[C_SubLogicType] DROP CONSTRAINT [FK_C_SubLogicType_C_LogicType]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_ACDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ACDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ACDistBox]'))
ALTER TABLE [dbo].[D_ACDistBox] DROP CONSTRAINT [FK_D_ACDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_AirCondHost]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondHost_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondHost]'))
ALTER TABLE [dbo].[D_AirCondHost] DROP CONSTRAINT [FK_D_AirCondHost_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_AirCondWindCabi]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCabi]'))
ALTER TABLE [dbo].[D_AirCondWindCabi] DROP CONSTRAINT [FK_D_AirCondWindCabi_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_AirCondWindCool]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCool_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCool]'))
ALTER TABLE [dbo].[D_AirCondWindCool] DROP CONSTRAINT [FK_D_AirCondWindCool_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_BattGroup]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattGroup]'))
ALTER TABLE [dbo].[D_BattGroup] DROP CONSTRAINT [FK_D_BattGroup_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_BattTempBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattTempBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattTempBox]'))
ALTER TABLE [dbo].[D_BattTempBox] DROP CONSTRAINT [FK_D_BattTempBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_ChangeHeat]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ChangeHeat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ChangeHeat]'))
ALTER TABLE [dbo].[D_ChangeHeat] DROP CONSTRAINT [FK_D_ChangeHeat_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_CombSwitElecSour]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_CombSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_CombSwitElecSour]'))
ALTER TABLE [dbo].[D_CombSwitElecSour] DROP CONSTRAINT [FK_D_CombSwitElecSour_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_ControlEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ControlEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ControlEqui]'))
ALTER TABLE [dbo].[D_ControlEqui] DROP CONSTRAINT [FK_D_ControlEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_DCDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DCDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DCDistBox]'))
ALTER TABLE [dbo].[D_DCDistBox] DROP CONSTRAINT [FK_D_DCDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Device]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Device_S_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Device]'))
ALTER TABLE [dbo].[D_Device] DROP CONSTRAINT [FK_D_Device_S_Room]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_DivSwitElecSour]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DivSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DivSwitElecSour]'))
ALTER TABLE [dbo].[D_DivSwitElecSour] DROP CONSTRAINT [FK_D_DivSwitElecSour_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_ElecSourCabi]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ElecSourCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ElecSourCabi]'))
ALTER TABLE [dbo].[D_ElecSourCabi] DROP CONSTRAINT [FK_D_ElecSourCabi_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_GeneratorGroup]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_GeneratorGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_GeneratorGroup]'))
ALTER TABLE [dbo].[D_GeneratorGroup] DROP CONSTRAINT [FK_D_GeneratorGroup_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_HighVoltDistBox]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_HighVoltDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_HighVoltDistBox]'))
ALTER TABLE [dbo].[D_HighVoltDistBox] DROP CONSTRAINT [FK_D_HighVoltDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Inverter]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Inverter_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Inverter]'))
ALTER TABLE [dbo].[D_Inverter] DROP CONSTRAINT [FK_D_Inverter_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_LowDistCabinet]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_LowDistCabinet_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_LowDistCabinet]'))
ALTER TABLE [dbo].[D_LowDistCabinet] DROP CONSTRAINT [FK_D_LowDistCabinet_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Manostat]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Manostat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Manostat]'))
ALTER TABLE [dbo].[D_Manostat] DROP CONSTRAINT [FK_D_Manostat_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_MobiGenerator]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_MobiGenerator_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_MobiGenerator]'))
ALTER TABLE [dbo].[D_MobiGenerator] DROP CONSTRAINT [FK_D_MobiGenerator_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_OrdiAirCond]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_OrdiAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_OrdiAirCond]'))
ALTER TABLE [dbo].[D_OrdiAirCond] DROP CONSTRAINT [FK_D_OrdiAirCond_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_RedefinePoint]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] DROP CONSTRAINT [FK_D_RedefinePoint_P_Point]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_RedefinePoint_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_RedefinePoint]'))
ALTER TABLE [dbo].[D_RedefinePoint] DROP CONSTRAINT [FK_D_RedefinePoint_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Signal]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] DROP CONSTRAINT [FK_D_Signal_P_Point]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Signal_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Signal]'))
ALTER TABLE [dbo].[D_Signal] DROP CONSTRAINT [FK_D_Signal_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_SolarController]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarController_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarController]'))
ALTER TABLE [dbo].[D_SolarController] DROP CONSTRAINT [FK_D_SolarController_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_SolarEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarEqui]'))
ALTER TABLE [dbo].[D_SolarEqui] DROP CONSTRAINT [FK_D_SolarEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_SpecAirCond]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SpecAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SpecAirCond]'))
ALTER TABLE [dbo].[D_SpecAirCond] DROP CONSTRAINT [FK_D_SpecAirCond_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_SwitchFuse]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SwitchFuse_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SwitchFuse]'))
ALTER TABLE [dbo].[D_SwitchFuse] DROP CONSTRAINT [FK_D_SwitchFuse_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Transformer]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Transformer_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Transformer]'))
ALTER TABLE [dbo].[D_Transformer] DROP CONSTRAINT [FK_D_Transformer_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_UPS]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_UPS_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_UPS]'))
ALTER TABLE [dbo].[D_UPS] DROP CONSTRAINT [FK_D_UPS_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_Ventilation]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Ventilation_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Ventilation]'))
ALTER TABLE [dbo].[D_Ventilation] DROP CONSTRAINT [FK_D_Ventilation_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_WindEnerEqui]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindEnerEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindEnerEqui]'))
ALTER TABLE [dbo].[D_WindEnerEqui] DROP CONSTRAINT [FK_D_WindEnerEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_WindLightCompCon]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindLightCompCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindLightCompCon]'))
ALTER TABLE [dbo].[D_WindLightCompCon] DROP CONSTRAINT [FK_D_WindLightCompCon_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[D_WindPowerCon]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindPowerCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindPowerCon]'))
ALTER TABLE [dbo].[D_WindPowerCon] DROP CONSTRAINT [FK_D_WindPowerCon_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[G_Driver]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GDriver_GBus]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Driver]'))
ALTER TABLE [dbo].[G_Driver] DROP CONSTRAINT [FK_GDriver_GBus]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[M_Authorization]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] DROP CONSTRAINT [FK_M_Authorization_M_Card]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_Authorization_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_Authorization]'))
ALTER TABLE [dbo].[M_Authorization] DROP CONSTRAINT [FK_M_Authorization_G_Driver]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[M_CardsInEmployee]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_CardsInEmployee_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_CardsInEmployee]'))
ALTER TABLE [dbo].[M_CardsInEmployee] DROP CONSTRAINT [FK_M_CardsInEmployee_M_Card]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[M_DriversInTime]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_DriversInTime_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DriversInTime]'))
ALTER TABLE [dbo].[M_DriversInTime] DROP CONSTRAINT [FK_M_DriversInTime_G_Driver]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[P_PointsInProtocol]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Protocol]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] DROP CONSTRAINT [FK_P_PointsInProtocol_P_Protocol]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_PointsInProtocol_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_PointsInProtocol]'))
ALTER TABLE [dbo].[P_PointsInProtocol] DROP CONSTRAINT [FK_P_PointsInProtocol_P_Point]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[P_SubPoint]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_SubPoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_SubPoint]'))
ALTER TABLE [dbo].[P_SubPoint] DROP CONSTRAINT [FK_P_SubPoint_P_Point]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[S_Room]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Room_S_Station]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Room]'))
ALTER TABLE [dbo].[S_Room] DROP CONSTRAINT [FK_S_Room_S_Station]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[S_Station]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Station_A_Area]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Station]'))
ALTER TABLE [dbo].[S_Station] DROP CONSTRAINT [FK_S_Station_A_Area]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[U_MenusInRole]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRole_U_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRole]'))
ALTER TABLE [dbo].[U_MenusInRole] DROP CONSTRAINT [FK_U_MenusInRole_U_Role]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--ɾ�����[dbo].[V_Channel]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_V_Channel_V_Camera]') AND parent_object_id = OBJECT_ID(N'[dbo].[V_Channel]'))
ALTER TABLE [dbo].[V_Channel] DROP CONSTRAINT [FK_V_Channel_V_Camera]
GO


--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[A_Area]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_AreaType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Brand]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Department]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_DeviceType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Duty]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_EnumMethods]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Group]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_LogicType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Productor]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_RoomType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_SCVendor]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_StationType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_SubCompany]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_SubDeviceType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_SubLogicType]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Supplier]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[C_Unit]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_ACDistBox]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_AirCondHost]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_AirCondWindCabi]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_AirCondWindCool]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_BattGroup]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_BattTempBox]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_ChangeHeat]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_CombSwitElecSour]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_ControlEqui]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_DCDistBox]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Device]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_DivSwitElecSour]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_ElecSourCabi]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_FSU]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_GeneratorGroup]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_HighVoltDistBox]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Inverter]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_LowDistCabinet]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Manostat]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_MobiGenerator]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_OrdiAirCond]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_RedefinePoint]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Signal]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_SolarController]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_SolarEqui]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_SpecAirCond]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_SwitchFuse]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Transformer]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_UPS]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_Ventilation]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_WindEnerEqui]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_WindLightCompCon]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[D_WindPowerCon]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Bus]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[G_Driver]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_DBScript]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_Masking]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_Note]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_OpEvent]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[H_Sync]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Authorization]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Card]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_CardsInEmployee]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_DriversInTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_HolidayTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_InfraredTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_Sync]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_WeekEndTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_WeekTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[M_WorkTime]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[P_Point]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[P_PointsInProtocol]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[P_Protocol]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[P_SubPoint]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[S_Room]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[S_Station]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[Sys_Menu]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Client]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Employee]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_MenusInRole]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_OutEmployee]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_Role]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[U_User]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[V_Camera]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[V_Channel]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������[dbo].[V_Preset]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--������ͼ[dbo].[V_Point]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�����洢����[dbo].[PI_H_OpEvent]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[C_SubDeviceType]
ALTER TABLE [dbo].[C_SubDeviceType]  WITH NOCHECK ADD  CONSTRAINT [FK_C_SubDeviceType_C_DeviceType] FOREIGN KEY([DeviceTypeID])
REFERENCES [dbo].[C_DeviceType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubDeviceType_C_DeviceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubDeviceType]'))
ALTER TABLE [dbo].[C_SubDeviceType] CHECK CONSTRAINT [FK_C_SubDeviceType_C_DeviceType]

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[C_SubLogicType]
ALTER TABLE [dbo].[C_SubLogicType]  WITH NOCHECK ADD  CONSTRAINT [FK_C_SubLogicType_C_LogicType] FOREIGN KEY([LogicTypeID])
REFERENCES [dbo].[C_LogicType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_C_SubLogicType_C_LogicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[C_SubLogicType]'))
ALTER TABLE [dbo].[C_SubLogicType] CHECK CONSTRAINT [FK_C_SubLogicType_C_LogicType]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_ACDistBox]
ALTER TABLE [dbo].[D_ACDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_ACDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ACDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ACDistBox]'))
ALTER TABLE [dbo].[D_ACDistBox] CHECK CONSTRAINT [FK_D_ACDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_AirCondHost]
ALTER TABLE [dbo].[D_AirCondHost]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondHost_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondHost_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondHost]'))
ALTER TABLE [dbo].[D_AirCondHost] CHECK CONSTRAINT [FK_D_AirCondHost_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_AirCondWindCabi]
ALTER TABLE [dbo].[D_AirCondWindCabi]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondWindCabi_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCabi]'))
ALTER TABLE [dbo].[D_AirCondWindCabi] CHECK CONSTRAINT [FK_D_AirCondWindCabi_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_AirCondWindCool]
ALTER TABLE [dbo].[D_AirCondWindCool]  WITH CHECK ADD  CONSTRAINT [FK_D_AirCondWindCool_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_AirCondWindCool_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_AirCondWindCool]'))
ALTER TABLE [dbo].[D_AirCondWindCool] CHECK CONSTRAINT [FK_D_AirCondWindCool_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_BattGroup]
ALTER TABLE [dbo].[D_BattGroup]  WITH CHECK ADD  CONSTRAINT [FK_D_BattGroup_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattGroup]'))
ALTER TABLE [dbo].[D_BattGroup] CHECK CONSTRAINT [FK_D_BattGroup_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_BattTempBox]
ALTER TABLE [dbo].[D_BattTempBox]  WITH CHECK ADD  CONSTRAINT [FK_D_BattTempBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_BattTempBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_BattTempBox]'))
ALTER TABLE [dbo].[D_BattTempBox] CHECK CONSTRAINT [FK_D_BattTempBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_ChangeHeat]
ALTER TABLE [dbo].[D_ChangeHeat]  WITH CHECK ADD  CONSTRAINT [FK_D_ChangeHeat_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ChangeHeat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ChangeHeat]'))
ALTER TABLE [dbo].[D_ChangeHeat] CHECK CONSTRAINT [FK_D_ChangeHeat_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_CombSwitElecSour]
ALTER TABLE [dbo].[D_CombSwitElecSour]  WITH CHECK ADD  CONSTRAINT [FK_D_CombSwitElecSour_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_CombSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_CombSwitElecSour]'))
ALTER TABLE [dbo].[D_CombSwitElecSour] CHECK CONSTRAINT [FK_D_CombSwitElecSour_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_ControlEqui]
ALTER TABLE [dbo].[D_ControlEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_ControlEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ControlEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ControlEqui]'))
ALTER TABLE [dbo].[D_ControlEqui] CHECK CONSTRAINT [FK_D_ControlEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_DCDistBox]
ALTER TABLE [dbo].[D_DCDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_DCDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DCDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DCDistBox]'))
ALTER TABLE [dbo].[D_DCDistBox] CHECK CONSTRAINT [FK_D_DCDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Device]
ALTER TABLE [dbo].[D_Device]  WITH CHECK ADD  CONSTRAINT [FK_D_Device_S_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[S_Room] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Device_S_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Device]'))
ALTER TABLE [dbo].[D_Device] CHECK CONSTRAINT [FK_D_Device_S_Room]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_DivSwitElecSour]
ALTER TABLE [dbo].[D_DivSwitElecSour]  WITH CHECK ADD  CONSTRAINT [FK_D_DivSwitElecSour_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_DivSwitElecSour_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_DivSwitElecSour]'))
ALTER TABLE [dbo].[D_DivSwitElecSour] CHECK CONSTRAINT [FK_D_DivSwitElecSour_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_ElecSourCabi]
ALTER TABLE [dbo].[D_ElecSourCabi]  WITH CHECK ADD  CONSTRAINT [FK_D_ElecSourCabi_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_ElecSourCabi_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_ElecSourCabi]'))
ALTER TABLE [dbo].[D_ElecSourCabi] CHECK CONSTRAINT [FK_D_ElecSourCabi_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_GeneratorGroup]
ALTER TABLE [dbo].[D_GeneratorGroup]  WITH CHECK ADD  CONSTRAINT [FK_D_GeneratorGroup_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_GeneratorGroup_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_GeneratorGroup]'))
ALTER TABLE [dbo].[D_GeneratorGroup] CHECK CONSTRAINT [FK_D_GeneratorGroup_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_HighVoltDistBox]
ALTER TABLE [dbo].[D_HighVoltDistBox]  WITH CHECK ADD  CONSTRAINT [FK_D_HighVoltDistBox_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_HighVoltDistBox_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_HighVoltDistBox]'))
ALTER TABLE [dbo].[D_HighVoltDistBox] CHECK CONSTRAINT [FK_D_HighVoltDistBox_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Inverter]
ALTER TABLE [dbo].[D_Inverter]  WITH CHECK ADD  CONSTRAINT [FK_D_Inverter_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Inverter_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Inverter]'))
ALTER TABLE [dbo].[D_Inverter] CHECK CONSTRAINT [FK_D_Inverter_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_LowDistCabinet]
ALTER TABLE [dbo].[D_LowDistCabinet]  WITH CHECK ADD  CONSTRAINT [FK_D_LowDistCabinet_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_LowDistCabinet_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_LowDistCabinet]'))
ALTER TABLE [dbo].[D_LowDistCabinet] CHECK CONSTRAINT [FK_D_LowDistCabinet_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Manostat]
ALTER TABLE [dbo].[D_Manostat]  WITH CHECK ADD  CONSTRAINT [FK_D_Manostat_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Manostat_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Manostat]'))
ALTER TABLE [dbo].[D_Manostat] CHECK CONSTRAINT [FK_D_Manostat_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_MobiGenerator]
ALTER TABLE [dbo].[D_MobiGenerator]  WITH CHECK ADD  CONSTRAINT [FK_D_MobiGenerator_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_MobiGenerator_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_MobiGenerator]'))
ALTER TABLE [dbo].[D_MobiGenerator] CHECK CONSTRAINT [FK_D_MobiGenerator_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_OrdiAirCond]
ALTER TABLE [dbo].[D_OrdiAirCond]  WITH CHECK ADD  CONSTRAINT [FK_D_OrdiAirCond_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_OrdiAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_OrdiAirCond]'))
ALTER TABLE [dbo].[D_OrdiAirCond] CHECK CONSTRAINT [FK_D_OrdiAirCond_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_RedefinePoint]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Signal]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_SolarController]
ALTER TABLE [dbo].[D_SolarController]  WITH CHECK ADD  CONSTRAINT [FK_D_SolarController_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarController_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarController]'))
ALTER TABLE [dbo].[D_SolarController] CHECK CONSTRAINT [FK_D_SolarController_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_SolarEqui]
ALTER TABLE [dbo].[D_SolarEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_SolarEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SolarEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SolarEqui]'))
ALTER TABLE [dbo].[D_SolarEqui] CHECK CONSTRAINT [FK_D_SolarEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_SpecAirCond]
ALTER TABLE [dbo].[D_SpecAirCond]  WITH CHECK ADD  CONSTRAINT [FK_D_SpecAirCond_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SpecAirCond_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SpecAirCond]'))
ALTER TABLE [dbo].[D_SpecAirCond] CHECK CONSTRAINT [FK_D_SpecAirCond_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_SwitchFuse]
ALTER TABLE [dbo].[D_SwitchFuse]  WITH CHECK ADD  CONSTRAINT [FK_D_SwitchFuse_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_SwitchFuse_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_SwitchFuse]'))
ALTER TABLE [dbo].[D_SwitchFuse] CHECK CONSTRAINT [FK_D_SwitchFuse_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Transformer]
ALTER TABLE [dbo].[D_Transformer]  WITH CHECK ADD  CONSTRAINT [FK_D_Transformer_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Transformer_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Transformer]'))
ALTER TABLE [dbo].[D_Transformer] CHECK CONSTRAINT [FK_D_Transformer_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_UPS]
ALTER TABLE [dbo].[D_UPS]  WITH CHECK ADD  CONSTRAINT [FK_D_UPS_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_UPS_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_UPS]'))
ALTER TABLE [dbo].[D_UPS] CHECK CONSTRAINT [FK_D_UPS_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_Ventilation]
ALTER TABLE [dbo].[D_Ventilation]  WITH CHECK ADD  CONSTRAINT [FK_D_Ventilation_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_Ventilation_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_Ventilation]'))
ALTER TABLE [dbo].[D_Ventilation] CHECK CONSTRAINT [FK_D_Ventilation_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_WindEnerEqui]
ALTER TABLE [dbo].[D_WindEnerEqui]  WITH CHECK ADD  CONSTRAINT [FK_D_WindEnerEqui_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindEnerEqui_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindEnerEqui]'))
ALTER TABLE [dbo].[D_WindEnerEqui] CHECK CONSTRAINT [FK_D_WindEnerEqui_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_WindLightCompCon]
ALTER TABLE [dbo].[D_WindLightCompCon]  WITH CHECK ADD  CONSTRAINT [FK_D_WindLightCompCon_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindLightCompCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindLightCompCon]'))
ALTER TABLE [dbo].[D_WindLightCompCon] CHECK CONSTRAINT [FK_D_WindLightCompCon_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[D_WindPowerCon]
ALTER TABLE [dbo].[D_WindPowerCon]  WITH CHECK ADD  CONSTRAINT [FK_D_WindPowerCon_D_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[D_Device] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_D_WindPowerCon_D_Device]') AND parent_object_id = OBJECT_ID(N'[dbo].[D_WindPowerCon]'))
ALTER TABLE [dbo].[D_WindPowerCon] CHECK CONSTRAINT [FK_D_WindPowerCon_D_Device]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[G_Driver]
ALTER TABLE [dbo].[G_Driver]  WITH CHECK ADD  CONSTRAINT [FK_GDriver_GBus] FOREIGN KEY([BusID])
REFERENCES [dbo].[G_Bus] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GDriver_GBus]') AND parent_object_id = OBJECT_ID(N'[dbo].[G_Driver]'))
ALTER TABLE [dbo].[G_Driver] CHECK CONSTRAINT [FK_GDriver_GBus]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[M_Authorization]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[M_CardsInEmployee]
ALTER TABLE [dbo].[M_CardsInEmployee]  WITH CHECK ADD  CONSTRAINT [FK_M_CardsInEmployee_M_Card] FOREIGN KEY([CardID])
REFERENCES [dbo].[M_Card] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_CardsInEmployee_M_Card]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_CardsInEmployee]'))
ALTER TABLE [dbo].[M_CardsInEmployee] CHECK CONSTRAINT [FK_M_CardsInEmployee_M_Card]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[M_DriversInTime]
ALTER TABLE [dbo].[M_DriversInTime]  WITH CHECK ADD  CONSTRAINT [FK_M_DriversInTime_G_Driver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[G_Driver] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_M_DriversInTime_G_Driver]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DriversInTime]'))
ALTER TABLE [dbo].[M_DriversInTime] CHECK CONSTRAINT [FK_M_DriversInTime_G_Driver]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].
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

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[P_SubPoint]
ALTER TABLE [dbo].[P_SubPoint]  WITH NOCHECK ADD  CONSTRAINT [FK_P_SubPoint_P_Point] FOREIGN KEY([PointID])
REFERENCES [dbo].[P_Point] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_P_SubPoint_P_Point]') AND parent_object_id = OBJECT_ID(N'[dbo].[P_SubPoint]'))
ALTER TABLE [dbo].[P_SubPoint] CHECK CONSTRAINT [FK_P_SubPoint_P_Point]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[S_Room]
ALTER TABLE [dbo].[S_Room]  WITH CHECK ADD  CONSTRAINT [FK_S_Room_S_Station] FOREIGN KEY([StationID])
REFERENCES [dbo].[S_Station] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Room_S_Station]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Room]'))
ALTER TABLE [dbo].[S_Room] CHECK CONSTRAINT [FK_S_Room_S_Station]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[S_Station]
ALTER TABLE [dbo].[S_Station]  WITH CHECK ADD  CONSTRAINT [FK_S_Station_A_Area] FOREIGN KEY([AreaID])
REFERENCES [dbo].[A_Area] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_S_Station_A_Area]') AND parent_object_id = OBJECT_ID(N'[dbo].[S_Station]'))
ALTER TABLE [dbo].[S_Station] CHECK CONSTRAINT [FK_S_Station_A_Area]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[U_MenusInRole]
ALTER TABLE [dbo].[U_MenusInRole]  WITH CHECK ADD  CONSTRAINT [FK_U_MenusInRole_U_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[U_Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_U_MenusInRole_U_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[U_MenusInRole]'))
ALTER TABLE [dbo].[U_MenusInRole] CHECK CONSTRAINT [FK_U_MenusInRole_U_Role]
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�������[dbo].[V_Channel]
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

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_AreaType]
DELETE FROM [dbo].[C_AreaType]
GO

INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','110100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','110106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ��ɽ��','110107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ����','110109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','110111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','110112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳����','110113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','110114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','110117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��','110200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','110229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','120000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','120100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','120101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӷ���','120102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͽ���','120104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӱ���','120105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','120116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��','120200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','120223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','120225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӱ�ʡ','130000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ��ׯ��','130100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ŷ���','130103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','130105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','130107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԣ����','130108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','130124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޻���','130129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޼���','130130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽɽ��','130131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ����','130132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('޻����','130182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¹Ȫ��','130185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','130200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('·����','130202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('·����','130203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ұ��','130204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','130205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','130209');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��','130225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǩ����','130227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǩ����','130283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ػʵ���','130300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ������','130303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','130304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','130321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¬����','130324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','130402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','130403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɰ���','130424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','130432');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130433');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('κ��','130434');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130435');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�䰲��','130481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','130500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ŷ���','130502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','130521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٳ���','130522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡Ң��','130525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻ���','130527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¹��','130529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�º���','130530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','130532');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130533');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','130534');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130535');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϲ���','130581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','130582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Է��','130622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ˮ��','130623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','130624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','130625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݳ���','130629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Դ��','130630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130633');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','130635');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳ƽ��','130636');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ұ��','130637');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130638');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�߱�����','130684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�żҿ���','130700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ŷ���','130702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»�԰��','130706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ű���','130722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','130724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ε��','130726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','130727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ȫ��','130729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¹��','130731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','130732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130733');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�е���','130800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','130802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','130803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӥ��Ӫ�ӿ���','130804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�е���','130821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡��','130822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽȪ��','130823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','130824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','130825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','130826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','130827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Χ�������ɹ���������','130828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','130901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','130902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˺���','130903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','130925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ƥ��','130927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','130929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϴ����������','130930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ��','130981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','130983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӽ���','130984');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ȷ���','131000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','131001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̰���','131022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','131024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','131025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�İ���','131026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�󳧻���������','131028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','131100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','131101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ҳ���','131102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǿ��','131121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǿ��','131123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','131125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʳ���','131126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','131127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','131182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ��ʡ','140000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫ԭ��','140100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('С����','140105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӭ����','140106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӻ�����','140107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ƺ��','140108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','140109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','140110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¦����','140123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ž���','140181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͬ��','140200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','140211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','140225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͬ��','140227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','140300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','140321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԫ��','140423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ˳��','140425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','140426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','140431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('º����','140481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������Ͻ��','140501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','140521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�괨��','140524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','140581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˷����','140600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˷����','140602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ³��','140603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ����','140621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӧ��','140622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܴ���','140702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȩ��','140722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','140723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','140726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽң��','140728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ��','140729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˳���','140800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�κ���','140802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','140821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ϲ��','140823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ��','140824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','140825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','140826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԫ����','140827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ½��','140829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǳ���','140830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӽ���','140882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','140901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ø���','140902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','140922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','140927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��կ��','140928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','140929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','140931');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƫ����','140932');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԭƽ��','140981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٷ���','141000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','141001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ң����','141002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','141023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�鶴��','141024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','141027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141031');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141032');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141033');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141034');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','141101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ��','141102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','141121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','141124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ¥��','141126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','141127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','141128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Т����','141181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','141182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ɹ�������','150000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ͺ�����','150100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�³���','150102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','150104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ĭ������','150121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�п�����','150122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ָ����','150123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ����','150124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�䴨��','150125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ��','150200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','150204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','150205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ƶ�������','150206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','150207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ĭ������','150221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����ï����������','150223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ں���','150300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڴ���','150304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','150400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','150402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ��ɽ��','150403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','150404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³�ƶ�����','150421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʲ������','150425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ţ����','150426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','150500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ�����','150502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ�����������','150521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ�����������','150522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³��','150523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³����','150526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ֹ�����','150581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������˹��','150600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʤ��','150602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('׼�����','150622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���п�ǰ��','150623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���п���','150624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������','150627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ױ�����','150700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ī�����ߴ��Ӷ���������','150722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���״�������','150723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���¿���������','150724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�°Ͷ�����','150725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�°Ͷ�������','150726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�°Ͷ�������','150727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʯ��','150782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������','150784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����׶���','150800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٺ���','150802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','150821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','150822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������ǰ��','150823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','150824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����غ���','150825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����첼��','150900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','150901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('׿����','150921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̶���','150923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˺���','150924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������ǰ��','150926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','150927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','150928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','150929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','150981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˰���','152200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','152202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ�������ǰ��','152221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ�����������','152222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','152223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͻȪ��','152224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ֹ�����','152500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ֺ�����','152502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���͸���','152522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������','152525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������','152526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫������','152527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','152528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','152529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','152530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','152531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','152900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','152922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','152923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','210000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','210102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','210103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','210104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʹ���','210105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ռ�����','210111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ں���','210114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','210123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','210202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ�ӿ���','210204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʾ�����','210211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳����','210212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�߷�����','210281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','210282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ׯ����','210283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','210300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','210304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǧɽ��','210311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨����','210321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','210323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','210400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¸���','210402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳����','210411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','210421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�±�����������','210422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ����������','210423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','210500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽɽ��','210502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ϫ����','210503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','210504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϸ���','210505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ����������','210521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','210522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ����','210602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','210604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','210624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','210682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','210703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','210711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','210726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','210727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�躣��','210781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӫ����','210800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('վǰ��','210802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����Ȧ��','210804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϱ���','210811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ����','210882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','210901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫ƽ��','210904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','210905');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ϸ����','210911');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����ɹ���������','210921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','210922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','211001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʥ��','211003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ΰ��','211004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','211005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫�Ӻ���','211011');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̽���','211100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','211101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫̨����','211102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡̨��','211103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','211122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','211201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','211204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͼ��','211224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','211281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','211282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','211301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','211302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','211322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����������ɹ���������','211324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ʊ��','211381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','211382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��«����','211400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','211401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','211402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ʊ��','211404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','211422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˳���','211481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','220000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϲ���','220102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��԰��','220106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','220112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ũ����','220122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','220181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','220183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̶��','220203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӫ��','220204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ժ���','220281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','220282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ��','220284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','220300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ����������','220323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','220381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','220382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','220400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','220402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','220500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','220503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','220521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷�ӿ���','220581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','220600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�뽭��','220602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','220605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���׳�����������','220623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٽ���','220681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','220700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǰ������˹�ɹ���������','220721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǭ����','220723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׳���','220800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','220801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('䬱���','220802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','220821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','220822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','220881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','220882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӱ߳�����������','222400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӽ���','222401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͼ����','222402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ػ���','222403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','222404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','222405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','222406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','222424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͼ��','222426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������ʡ','230000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','230100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϸ���','230103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','230108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɱ���','230109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㷻��','230110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','230125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ľ����','230127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','230128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','230182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��־��','230183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�峣��','230184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������','230200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','230202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����Ϫ��','230205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','230206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','230207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷��˹���Ӷ�����','230208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','230224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԣ��','230227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˶���','230230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','230231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ګ����','230281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ε���','230304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ӻ���','230306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230307');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׸���','230400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ũ��','230403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˰���','230405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230407');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܱ���','230421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','230422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫Ѽɽ��','230500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�붫��','230503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ķ�̨��','230505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĺ���','230524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ͼ��','230602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ú�·��','230604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','230605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͬ��','230606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','230622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֵ���','230623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ŷ������ɹ���������','230624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϲ���','230703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ѻ���','230704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230707');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','230708');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����','230709');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӫ��','230710');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','230711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','230712');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230713');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','230714');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230715');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϸ�����','230716');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ˹��','230800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǰ����','230804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230805');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','230811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�봨��','230826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','230828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','230833');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͬ����','230881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨����','230900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','230901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','230903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ӻ���','230904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','230921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ĵ������','231000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','231001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֿ���','231025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Һ���','231081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231084');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231085');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ں���','231100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','231101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�۽���','231121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ѷ����','231123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������','231182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�绯��','231200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','231201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','231223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�찲��','231224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','231225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ض���','231282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','231283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���˰������','232700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','232721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','232722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Į����','232723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻ���','310000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','310100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','310104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('բ����','310108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','310109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','310113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ζ���','310114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֶ�����','310115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','310116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ���','310117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310118');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310120');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��','310200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','310230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','320000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͼ���','320100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ػ���','320104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¥��','320106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¹���','320107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֿ���','320111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ϼ��','320113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�껨̨��','320114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','320124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ߴ���','320125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�簲��','320202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','320203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','320205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','320206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¥��','320302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ȫɽ��','320311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭɽ��','320312');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','320321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','320322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¥��','320404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','320405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�±���','320411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320412');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̳��','320482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320507');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320508');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�⽭��','320509');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�żҸ���','320582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','320583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','320585');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ��','320600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�紨��','320602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��բ��','320611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','320612');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�綫��','320623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ƹ���','320700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320706');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','320826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�γ���','320900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','320901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͤ����','320902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ζ���','320903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','320921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','320925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','320981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','320982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','321001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321012');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӧ��','321023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321084');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','321100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','321101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͽ��','321112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','321200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','321201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�߸���','321203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˻���','321281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','321283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ǩ��','321300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','321301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޳���','321302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԥ��','321311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','321324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㽭ʡ','330000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','330102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�³���','330103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','330109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ຼ��','330110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͩ®��','330122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٰ���','330185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','330211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۴����','330212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','330225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ҧ��','330281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','330282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','330283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¹����','330302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('걺���','330304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ��','330322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','330326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĳ���','330328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩˳��','330329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','330381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻ���','330402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','330482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͩ����','330483');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','330503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Խ����','330602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�²���','330624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','330700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĳ���','330702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','330703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֽ���','330726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͱ���','330727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','330781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�³���','330802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�齭��','330803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','330822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','330881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','330900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','330901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ��','330921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','330922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨����','331000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','331001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('·����','331004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','331021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','331023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɾ���','331024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٺ���','331082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','331100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','331101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','331123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','331124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƺ���','331125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ԫ��','331126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','331127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','331181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','340000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϸ���','340100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('®����','340103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʶ���','340122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('®����','340124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ߺ���','340200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('߮����','340203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('𯽭��','340207');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340208');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ߺ���','340221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϊ��','340225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ӻ���','340302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','340321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','340322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ��','340402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','340403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('л�Ҽ���','340404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˹�ɽ��','340405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˼���','340406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','340421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','340500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϳ��','340521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','340523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ż���','340602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','340604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϫ��','340621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','340700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ��ɽ��','340702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʨ��ɽ��','340703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','340711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','340721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','340801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӭ����','340802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','340803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǳɽ��','340824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','340825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','340828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͩ����','340881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','341000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','341002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','341003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','341021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ȫ����','341124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','341125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�쳤��','341181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('򣶫��','341203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ȫ��','341204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','341221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','341222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','341321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԣ����','341503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��կ��','341524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','341525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�۳���','341602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɳ���','341622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ̨��','341722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','341801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','341821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','341822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','341823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','341824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('캵���','341825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','341881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','350000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¥��','350102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨����','350103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','350104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��β��','350105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','350123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̩��','350125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ̶��','350128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˼����','350203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350206');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͬ����','350212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�谲��','350213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','350304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷����','350402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ԫ��','350403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','350421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','350426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ��','350427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','350429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ȫ����','350500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','350502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�彭��','350504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ȫ����','350505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݰ���','350521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','350524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','350526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯʨ��','350581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϰ���','350583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ܼ����','350602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('گ����','350624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̩��','350625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','350626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͼ���','350627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','350628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','350700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','350702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳����','350721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֳ���','350722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','350724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','350782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','350783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��͡��','350821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻ���','350823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','350824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','350881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','350901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ϼ����','350921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','350982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','360000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϲ���','360100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','360104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����','360111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϲ���','360121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�½���','360122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','360200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','360203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','360281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ƽ����','360300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','360302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�涫��','360313');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('«Ϫ��','360323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ž���','360400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('®ɽ��','360402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','360403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ž���','360421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','360424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�°���','360426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','360481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','360482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','360502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӥ̶��','360600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�º���','360602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�཭��','360622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','360681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¹���','360702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','360721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ŷ���','360722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','360726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ȫ����','360729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڶ���','360731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˹���','360732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','360733');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ѱ����','360734');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','360735');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','360781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͽ���','360782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','360803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','360822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ͽ����','360823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¸���','360824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','360826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�촨��','360827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','360828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','360881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˴���','360900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','360901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԭ����','360902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϸ���','360923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˷���','360924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','360926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','360981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','360982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�߰���','360983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','361001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٴ���','361002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','361021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�质��','361022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϸ���','361023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ְ���','361025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˻���','361026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','361027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','361028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','361030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','361101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','361122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','361123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǧɽ��','361124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','361125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('߮����','361126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','361127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۶����','361128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','361130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','361181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ��ʡ','370000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','370124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̺���','370126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ൺ��','370200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�б���','370203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ķ���','370205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƶ���','370211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','370212');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','370213');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370214');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ī��','370282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','370283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370285');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͳ���','370300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʹ���','370302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ŵ���','370303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','370304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܴ���','370306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','370321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','370323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ׯ��','370400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ѧ����','370403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ỳ���','370404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨��ׯ��','370405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽͤ��','370406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӫ��','370500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӫ��','370502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӿ���','370503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','370600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('֥���','370602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','370611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ĳƽ��','370612');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','370613');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','370685');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ϼ��','370686');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370687');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ϋ����','370700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ϋ����','370702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��','370703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','370782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٹ���','370783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370786');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�γ���','370811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('΢ɽ��','370826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','370827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','370831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','370832');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޳���','370883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩����','370900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','370901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̩ɽ��','370902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','370911');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','370921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','370923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̩��','370982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʳ���','370983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĵ���','371081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٳ���','371082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','371083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ��','371103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֳ���','371203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','371302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ׯ��','371311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӷ���','371312');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۰����','371322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','371323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','371324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','371326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�³���','371402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','371425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽԭ��','371426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ľ���','371427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','371428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĳ���','371500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','371502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ݷ��','371522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','371523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','371623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('մ����','371624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','371626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','371701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ĵ����','371702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','371722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ұ��','371724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۩����','371725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۲����','371726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','371728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','410000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('֣����','410100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','410102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܳǻ�����','410104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','410105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','410106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݼ���','410108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ĳ��','410122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֣��','410184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƿ���','410185');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��','410202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳�ӻ�����','410203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¥��','410204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����̨��','410205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','410221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','410222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ξ����','410223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','410302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�e�ӻ�����','410304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','410322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�°���','410323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ﴨ��','410324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʦ��','410381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ��ɽ��','410400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','410402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','410404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('տ����','410411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ҷ��','410422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('³ɽ��','410423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ۣ��','410425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ķ���','410502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڻ���','410527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ױ���','410600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','410602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ����','410603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('俱���','410611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','410622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','410704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ұ��','410711');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԭ����','410725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӽ���','410726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԫ��','410728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410782');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��վ��','410803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ����','410811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','410901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','410923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','410926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨ǰ��','410927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','410928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('κ����','411002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۳����','411024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411082');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Դ����','411102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۱����','411103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����Ͽ��','411200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ų���','411221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','411222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¬����','411224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�鱦��','411282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͽ��','411323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','411324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƺ���','411328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ұ��','411329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͩ����','411330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��԰��','411402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȩ��','411421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','411422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','411424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݳ���','411425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','411503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','411521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','411522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','411523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̳���','411524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʼ��','411525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�괨��','411526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ϣ��','411528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܿ���','411600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','411623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','411627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¹����','411628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('פ������','411700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','411701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','411702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','411721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϲ���','411722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','411723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ȷɽ��','411725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','411727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','411728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�²���','411729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʡֱϽ�ؼ���������','419000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','419001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','420000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�人��','420100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�~����','420104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','420106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','420112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̵���','420114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ��','420200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯ����','420202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ɽ��','420203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��½��','420204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ұ��','420281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʮ����','420300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('é����','420302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','420321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','420324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','420325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','420381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˲���','420500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ҹ���','420503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','420504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Vͤ��','420505');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420506');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Զ����','420525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������������','420528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������������','420529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˶���','420581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('֦����','420583');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','420602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420607');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ȳ���','420625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻӿ���','420682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˳���','420684');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ӻ���','420702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޵���','420804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','420821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','420822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Т����','420900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','420901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Т����','420902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Т����','420921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӧ����','420981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��½��','420982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','420984');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','421001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','421002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','421081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','421083');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421087');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƹ���','421100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','421101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ŷ���','421121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�찲��','421122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӣɽ��','421124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ˮ��','421125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ޭ����','421126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��÷��','421127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','421181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ѩ��','421182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','421201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̰���','421202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','421222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨɽ��','421224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','421281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','421301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','421303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','421321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','421381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʩ����������������','422800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʩ��','422801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','422802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʼ��','422822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͷ���','422823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','422825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̷���','422826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','422827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׷���','422828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʡֱϽ�ؼ���������','429000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','429004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǳ����','429005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','429006');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ũ������','429021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','430000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','430100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ܽ����','430102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��´��','430104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�껨��','430111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','430121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','430181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('«����','430203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','430204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ԫ��','430211');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','430223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̶��','430300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','430302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̶��','430321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','430382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','430406');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','430407');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430408');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430412');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','430423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ⶫ��','430424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','430426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430482');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','430502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430511');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�۶���','430521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','430524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǲ�����������','430529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','430581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����¥��','430602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','430603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','430611');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','430626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','430723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','430724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','430725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','430726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�żҽ���','430800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����Դ��','430811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɣֲ��','430822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','430901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','430903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','430921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ҽ���','430922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','430923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�佭��','430981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','431001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�κ���','431024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','431026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','431027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','431101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ̲��','431103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','431123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','431124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','431126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','431127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','431129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','431201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׳���','431202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�з���','431221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','431223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','431224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͬ��','431225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','431226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»ζ���������','431227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƽ�����������','431228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������嶱��������','431229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ������������','431230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�齭��','431281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¦����','431300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','431301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¦����','431302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','431321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','431322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ����','431381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','431382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','433100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','433101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','433122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','433123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԫ��','433124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','433125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','433126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','433127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','433130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㶫ʡ','440000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Խ����','440104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','440106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��خ��','440113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','440115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ܸ���','440116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӻ���','440184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ع���','440200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�佭��','440203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('䥽���','440204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʼ����','440222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʻ���','440224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','440229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ����������','440232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�·���','440233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֲ���','440281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440282');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޺���','440303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','440305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440306');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440307');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440308');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�麣��','440400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ��','440500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440507');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','440511');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('婽���','440512');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440513');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440514');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�κ���','440515');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϰ���','440523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','440600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440604');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϻ���','440605');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳����','440606');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','440607');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440608');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','440703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�»���','440705');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨ɽ��','440781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','440783');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','440784');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','440785');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('տ����','440800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�࿲��','440802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ϼɽ��','440803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͷ��','440804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','440823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�⴨��','440883');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ï����','440900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','440901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ï����','440902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ï����','440903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','440923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','440983');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�⿪��','441225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ҫ��','441283');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ļ���','441284');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݳ���','441302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݶ���','441323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷����','441400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷����','441402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('÷��','441421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','441423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�廪��','441424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽԶ��','441426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��β��','441500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','441502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('½����','441523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('½����','441581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','441600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Դ����','441602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','441621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','441623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','441624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','441625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','441800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','441801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','441802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','441821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','441823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ׳������������','441825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','441826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӣ����','441881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','441882');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ݸ��','441900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','442000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','445101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','445122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','445201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ų���','445202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ҷ���','445221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƹ���','445300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','445301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƴ���','445302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','445322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ư���','445323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޶���','445381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����׳��������','450000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','450107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','450123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','450127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','450203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¹կ��','450223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڰ���','450224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ����������','450225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','450226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','450302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450305');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˷��','450321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٹ���','450322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�鴨��','450323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ȫ����','450324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˰���','450325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʤ����������','450328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','450329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','450330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','450332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450405');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','450422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ϫ��','450481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����','450512');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ǹ���','450600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ۿ���','450602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˼��','450621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ձ���','450703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','450721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֱ���','450722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','450800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�۱���','450802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450804');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','450821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','450881');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','450901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','450921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('½����','450922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ҵ��','450924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','450981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɫ��','451000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','451001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ҽ���','451002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ﶫ��','451022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','451023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�±���','451024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ҵ��','451028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡�ָ���������','451031');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','451101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˲���','451102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','451121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','451122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','451123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӳ���','451200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','451201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǽ���','451202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϵ���','451221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','451222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','451223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޳�������������','451225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ë����������','451226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','451227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','451228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������','451229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451281');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','451301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˱���','451302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ó���','451321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','451324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','451381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','451401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','451424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','451425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƾ����','451481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','460000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','460100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','460101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӣ��','460105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','460106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','460107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','460108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','460200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','460201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','460300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳȺ��','460321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳȺ��','460322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳȺ���ĵ������亣��','460323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʡֱϽ�ؼ���������','469000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ָɽ��','469001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ĳ���','469005');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469006');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469007');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͳ���','469022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','469023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٸ���','469024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ����������','469025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','469026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֶ�����������','469027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ����������','469028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��������������','469029');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������������','469030');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','500100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɿ���','500104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳƺ����','500106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','500107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϰ���','500108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�뽭��','500110');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�山��','500112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǭ����','500114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϴ���','500117');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500118');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϴ���','500119');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��','500200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','500224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٲ���','500226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ��','500227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','500228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǿ���','500229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ᶼ��','500230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�潭��','500231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡��','500232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','500233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','500234');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','500235');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','500236');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','500237');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','500238');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ��������������','500240');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����������������','500241');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','500242');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ����������������','500243');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ĵ�ʡ','510000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɶ���','510100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ţ��','510106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','510107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɻ���','510108');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ����','510112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��׽���','510113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¶���','510114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�½���','510115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','510122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ۯ��','510124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ѽ���','510131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�½���','510132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','510181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510182');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510183');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510184');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Թ���','510300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','510302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̲��','510311');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','510322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֦����','510400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʺ���','510411');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�α���','510422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','510503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����̶��','510504');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','510522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','510603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�н���','510623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޽���','510626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㺺��','510681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʲ����','510682');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510683');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510703');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510704');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','510722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��','510723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','510724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����Ǽ��������','510726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','510727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ԫ��','510800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ����','510811');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510812');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ന��','510822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','510824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','510901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','510903');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','510904');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','510921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','510922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӣ��','510923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڽ���','511000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511011');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','511024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','511028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','511100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','511111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ����','511112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ں���','511113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϊ��','511123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�н���','511126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�崨��','511129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','511132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','511133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��üɽ��','511181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϳ���','511300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˳����','511302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ��','511303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϲ���','511321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӫɽ��','511322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','511323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¤��','511324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('üɽ��','511400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','511422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˱���','511500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','511503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˱���','511521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','511525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','511526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','511529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㰲��','511600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㰲��','511602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʤ��','511622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','511623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511681');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','511702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','511721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','511725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','511781');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ű���','511800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','511802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','511803');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','511823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','511824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ȫ��','511825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('«ɽ��','511826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','511901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','511902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','511921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','511922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','511923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','512000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','512001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㽭��','512002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','512021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','512022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','512081');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ӳ���Ǽ��������','513200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�봨��','513221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','513222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ï��','513223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��կ����','513225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','513226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('С����','513227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','513228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','513229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513230');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','513232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','513233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���β���������','513300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','513322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ž���','513325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¯����','513327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�¸���','513330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','513332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɫ����','513333');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513334');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513335');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','513336');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513337');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513338');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����������','513400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ľ�����������','513422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','513423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�²���','513424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ᶫ��','513426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ո���','513428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ѿ���','513431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ϲ����','513432');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513433');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Խ����','513434');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513435');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','513436');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ײ���','513437');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','520000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','520101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','520111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڵ���','520112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('С����','520114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ϣ����','520122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ˮ��','520200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','520201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֦����','520203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ˮ����','520221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','520222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','520301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�컨����','520302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�㴨��','520303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͩ����','520322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','520325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������������','520326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','520327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̶��','520328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ϰˮ��','520330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','520381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʻ���','520382');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','520400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','520401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','520421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ն���','520422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','520423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���벼��������������','520424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������岼����������','520425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ���','520500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ǹ���','520502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','520521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǭ����','520522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɳ��','520523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('֯����','520524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ӻ��','520525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������������������','520526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','520600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̽���','520602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','520603');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','520621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','520622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','520623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˼����','520624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӡ������������������','520625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�½���','520626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�غ�������������','520627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','520628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǭ���ϲ���������������','522300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�հ���','522323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡��','522324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','522325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','522327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǭ�������嶱��������','522600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','522622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʩ����','522623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','522625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('᯹���','522626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨����','522630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','522631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ž���','522632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӽ���','522633');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','522634');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�齭��','522635');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��կ��','522636');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǭ�ϲ���������������','522700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','522702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','522722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','522723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͱ���','522725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','522726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','522727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�޵���','522728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˳��','522729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','522730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','522731');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ˮ��������','522732');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','530000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�廪��','530102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٶ���','530111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','530112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʹ���','530114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ������������','530126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('»Ȱ��������������','530128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ѱ���������������','530129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('½����','530322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʦ����','530323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','530324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','530325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('մ����','530328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ϫ��','530400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ν���','530422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨ����','530423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����������','530426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ�������������','530427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ���������������������','530428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','530500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','530502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʩ����','530521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڳ���','530522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ��','530600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('³����','530621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɼ���','530622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ν���','530623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','530624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�罭��','530626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ˮ����','530630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ų���','530702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������������','530721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʤ��','530722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ��','530723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','530724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ն���','530800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˼é��','530802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','530821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ī��������������','530822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','530823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ȴ�������������','530824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������������������','530825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ǹ���������������','530826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������������������','530827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������������','530828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','530829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٲ���','530900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','530901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','530922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','530923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','530924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫�����������岼�������������','530925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������������','530926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ����������','530927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','532300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('˫����','532322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ĳ����','532323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ϻ���','532324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ҧ����','532325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ҧ��','532326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫı��','532328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�䶨��','532329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('»����','532331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ӹ���������������','532500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','532502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','532523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','532524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ����','532525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ԫ����','532528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','532529');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ�����������������','532530');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�̴���','532531');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӿ�����������','532532');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ׳������������','532600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','532601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','532622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','532624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','532626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˫���ɴ���������','532800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�º���','532822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','532900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','532922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֶ���','532925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͻ�����������','532926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ρɽ�������������','532927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','532928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','532930');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532931');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','532932');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�º���徰����������','533100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','533102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('â��','533103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','533122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӯ����','533123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¤����','533124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ŭ��������������','533300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','533321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','533323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ������ŭ��������','533324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ����������������','533325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','533400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������','533421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','533422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ά��������������','533423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','540000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','540100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','540101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǹ���','540102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','540121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','540122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ��','540123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','540124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','540125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','540126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ī�񹤿���','540127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','542100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','542124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','542128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('â����','542129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡��','542132');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�߰���','542133');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ�ϵ���','542200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˶���','542221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɣ����','542224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӳ���','542229');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','542231');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542232');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˿�����','542233');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�տ������','542300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�տ�����','542301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ����','542322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('лͨ����','542328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʲ���','542330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542332');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ٰ���','542333');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƕ���','542334');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡��','542335');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ľ��','542336');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542337');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڰ���','542338');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','542400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','542427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','542428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','542500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ｊ��','542525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֥����','542600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֥��','542621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','542622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ī����','542624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','542626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','542627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','610000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�³���','610102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','610111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('δ����','610112');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610113');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610114');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610115');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610116');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͭ����','610200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ӡ̨��','610203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ҫ����','610204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˾���','610222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('μ����','610302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','610303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�²���','610304');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ɽ��','610323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ü��','610326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¤��','610327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ǧ����','610328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610329');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610330');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̫����','610331');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ض���','610402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('μ����','610404');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','610422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ǭ��','610424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','610425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610426');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610427');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610428');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ѯ����','610429');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610430');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�书��','610431');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','610481');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('μ����','610500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��μ��','610502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�γ���','610525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ѳ���','610526');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','610527');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƽ��','610528');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610581');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610582');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӱ���','610600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӳ���','610621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӵ���','610622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ӳ���','610623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('־����','610625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','610627');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610628');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�崨��','610629');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˴���','610630');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610631');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610632');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','610702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֣��','610721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǹ���','610722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǿ��','610726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610727');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','610728');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610729');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ��','610730');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ��','610821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','610823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','610826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��֬��','610827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','610828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ɽ��','610829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�彧��','610830');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610831');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','610901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯȪ��','610922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','610924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('᰸���','610925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','610926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ��','610927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ѯ����','610928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׺���','610929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','611000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','611001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','611002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','611021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','611022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','611023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ����','611024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','611025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','611026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ʡ','620000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ǹ���','620102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','620103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','620111');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','620200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','620300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','620302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','620403');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Զ��','620421');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̩��','620423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','620500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','620503');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','620521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ذ���','620522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʹ���','620523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','620524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�żҴ�����������','620525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620601');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620602');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ף����������','620623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ҵ��','620700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620702');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ԣ����������','620721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','620724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɽ����','620725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','620800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','620802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','620822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͤ��','620824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ׯ����','620825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','620900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','620901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620902');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�౱�ɹ���������','620923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������������','620924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','620981');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ػ���','620982');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','621001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','621021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ˮ��','621024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','621027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','621101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͨμ��','621121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¤����','621122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('μԴ��','621123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','621124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���','621126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¤����','621200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','621201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�䶼��','621202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('崲���','621223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����','621227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','621228');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ļ���������','622900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','622901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','622921');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','622922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','622923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','622924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','622925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������','622926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʯɽ�����嶫����������������','622927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ϲ���������','623000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','623001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̶��','623021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('׿����','623022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','623023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','623024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','623025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('µ����','623026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ĺ���','623027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ຣʡ','630000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','630100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','630101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ƕ���','630102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','630103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','630104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ǳ���','630105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ��������������','630121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','630122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','630123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','632100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','632121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͻ�������������','632122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ֶ���','632123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','632126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��¡����������','632127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ѭ��������������','632128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','632200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ����������','632221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ղ���','632224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ϲ���������','632300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͬ����','632321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','632323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����ɹ���������','632324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ϲ���������','632500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͬ����','632522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','632523');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�˺���','632524');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632525');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������������','632600');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632621');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632622');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ʵ���','632623');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632624');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632625');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','632626');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','632700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632721');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ӷ���','632722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ƶ���','632723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ζ���','632724');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǫ��','632725');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','632726');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����ɹ������������','632800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ľ��','632801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','632802');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632821');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','632822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','632823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ļ���������','640000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','640101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','640106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640181');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ��ɽ��','640200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','640201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','640202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ũ��','640205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ƽ����','640221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','640301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͨ��','640302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���±���','640303');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�γ���','640323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͬ����','640324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͭϿ��','640381');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','640400');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','640401');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԭ����','640402');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640422');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('¡����','640423');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','640424');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640425');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640500');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','640501');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ��ͷ��','640502');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','640521');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ԭ��','640522');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�½�ά���������','650000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³ľ����','650100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','650101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ��','650102');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ���Ϳ���','650103');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','650104');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ˮĥ����','650105');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͷ�ͺ���','650106');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','650107');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׶���','650109');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³ľ����','650121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','650200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ͻ��','650201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ɽ����','650202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����������','650203');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�׼�̲��','650204');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ڶ�����','650205');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³������','652100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��³����','652101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('۷����','652122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�п�ѷ��','652123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ܵ���','652200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������������������','652222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������������','652300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652302');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͼ����','652323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����˹��','652324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','652325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ������','652327');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ľ�ݹ�����������','652328');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������ɹ�������','652700');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652701');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652722');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ȫ��','652723');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������ɹ�������','652800');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','652801');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��̨��','652822');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ξ����','652823');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Ǽ��','652824');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ĩ��','652825');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���Ȼ���������','652826');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�;���','652827');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��˶��','652828');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652829');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����յ���','652900');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','652901');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','652922');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�⳵��','652923');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','652924');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�º���','652925');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ݳ���','652926');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʲ��','652927');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','652928');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ƺ��','652929');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������տ¶�����������','653000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ͼʲ��','653001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','653022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','653023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ǡ��','653024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʲ����','653100');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʲ��','653101');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�踽��','653121');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653122');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ӣ��ɳ��','653123');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653124');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɯ����','653125');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ҷ����','653126');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','653127');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���պ���','653128');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('٤ʦ��','653129');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ͳ���','653130');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ʲ�����������������','653131');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','653200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ī����','653222');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('Ƥɽ��','653223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','653226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','653227');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���������������','654000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654021');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�첼�������������','654022');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654023');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654024');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��Դ��','654025');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654026');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�ؿ�˹��','654027');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���տ���','654028');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ǵ���','654200');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654201');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654202');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654221');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ɳ����','654223');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654224');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ԣ����','654225');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�Ͳ��������ɹ�������','654226');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����̩����','654300');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����̩��','654301');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','654321');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654322');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������','654323');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('���ͺ���','654324');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����','654325');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��ľ����','654326');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('������ֱϽ�ؼ���������','659000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ʯ������','659001');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('��������','659002');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('ͼľ�����','659003');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�������','659004');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('̨��ʡ','710000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('����ر�������','810000');
INSERT INTO [dbo].[C_AreaType]([Name],[ID]) VALUES('�����ر�������','820000');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_Department]
DELETE FROM [dbo].[C_Department];
GO

INSERT INTO [dbo].[C_Department]([ID],[Code],[Name],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc],[Enabled]) VALUES('001','X001',N'Ĭ�ϲ���',NULL,NULL,NULL,0,NULL,1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_DeviceType]
DELETE FROM [dbo].[C_DeviceType];
GO

INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('0', N'δ����');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('01', N'��ѹ���');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('02', N'��ѹ�������');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('03', N'��ѹ��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('04', N'��ѹֱ�����');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('05', N'�������');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('06', N'���ص�Դ');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('07', N'Ǧ����');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('08', N'UPS�豸');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('09', N'UPS���');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('11', N'����ר�ÿյ�');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('12', N'����յ�ĩ��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('13', N'����յ�����');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('14', N'�任�豸');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('15', N'��ͨ�յ�');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('16', N'�������̸�');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('17', N'��������');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('18', N'��غ��¹�');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('38', N'FSU');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('68', N'﮵��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('76', N'�������');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('77', N'����ͨ�绻��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('78', N'����豸');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('87', N'��ѹֱ��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('88', N'��ѹֱ����Դ���');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('92', N'���ܵ��');
INSERT INTO [dbo].[C_DeviceType]([ID],[Name]) VALUES('93', N'�����Ž�');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_Duty]
DELETE FROM [dbo].[C_Duty];
GO

INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('001', N'���³�', 1, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('002', N'�ܾ���', 2, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('003', N'���ž���', 3, NULL, 1);
INSERT INTO [dbo].[C_Duty]([ID],[Name],[Level],[Desc],[Enabled]) VALUES('004', N'Ա��', 4, NULL, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_LogicType]
DELETE FROM [dbo].[C_LogicType];
GO

INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0', '0', N'δ����');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0101', '01', N'��ѹ���澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0102', '01', N'��ѹ������Դ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0201', '02', N'��ѹ���澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0202', '02', N'���ݲ����豸�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0203', '02', N'г�������豸�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0301', '03', N'��ѹ���澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0401', '04', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0501', '05', N'������澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0502', '05', N'ȼ�ϵ�ظ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0601', '06', N'��ظ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0602', '06', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0603', '06', N'�任�豸�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0604', '06', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0605', '06', N'����ģ��澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0701', '07', N'��ظ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0801', '08', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0802', '08', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0803', '08', N'��·�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0804', '08', N'�������澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0805', '08', N'������澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0806', '08', N'���������Ʋ����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('0901', '09', N'����жϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1101', '11', N'��·ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1102', '11', N'ѹ�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1103', '11', N'ϵͳ���ϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1201', '12', N'��·ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1202', '12', N'ˮ·ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1203', '12', N'ϵͳ���ϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1301', '13', N'ϵͳ���ϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1302', '13', N'��ع��ϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1303', '13', N'ѹ��������');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1401', '14', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1402', '14', N'������澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1403', '14', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1501', '15', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1502', '15', N'ѹ�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1503', '15', N'�����Դ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1601', '16', N'ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1602', '16', N'�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1701', '17', N'ˮ���澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1702', '17', N'�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1703', '17', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1704', '17', N'�¶ȸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1705', '17', N'ʪ�ȸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1706', '17', N'�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1707', '17', N'���Ƹ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1708', '17', N'�𶯸澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1709', '17', N'�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1710', '17', N'������澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('1801', '18', N'��غ�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('6801', '68', N'�¶ȸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('6802', '68', N'��ѹ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7601', '76', N'�ɼ��豸ͨ���жϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7602', '76', N'ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7603', '76', N'������豸ͨ���жϸ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7701', '77', N'��Դ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7702', '77', N'�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7703', '77', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('7801', '78', N'ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8701', '87', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8702', '87', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8703', '87', N'�任�豸�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8704', '87', N'�����豸�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8705', '87', N'��ظ澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('8801', '88', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9201', '92', N'�е��쳣�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9202', '92', N'ϵͳ�澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9203', '92', N'����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9301', '93', N'�Ƿ�����澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9302', '93', N'�ſ��澯');
INSERT INTO [dbo].[C_LogicType]([ID],[DeviceTypeID],[Name]) VALUES('9303', '93', N'ϵͳ�澯');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_Productor]
DELETE FROM [dbo].[C_Productor];
GO

INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('00', 1, N'Ĭ����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('01', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('03', 1, N'Ħ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('04', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('05', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('06', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('07', 1, N'��Ϊ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('08', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('09', 1, N'���ű���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('10', 1, N'ŵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1001', 1, N'21��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1002', 1, N'TCL');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1003', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1004', 1, N'ATLAS');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1005', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1006', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1007', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1008', 1, N'���ݴ�Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1009', 1, N'���ջ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1010', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1011', 1, N'����ˮ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1012', 1, N'ƽԭ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1013', 1, N'�¹�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1014', 1, N'����ƥ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1015', 1, N'�Ŀ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1016', 1, N'BP SOLAR');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1017', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1018', 1, N'�ؿ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1019', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1020', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1021', 1, N'�����ȴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1022', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1023', 1, N'�����¸���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1024', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1025', 1, N'���������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1026', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1027', 1, N'�����̿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1028', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1029', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1030', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1031', 1, N'�����Ϳ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1032', 1, N'����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1033', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1034', 1, N'��˹��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1035', 1, N'���ܵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1036', 1, N'���ܷ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1037', 1, N'껳϶���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1038', 1, N'�����ƿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1039', 1, N'��ʥ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1040', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1041', 1, N'����ɭԴ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1042', 1, N'��ΰ��ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1043', 1, N'��ҵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1044', 1, N'������ƴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1045', 1, N'���ۻ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1046', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1047', 1, N'���Ϻ�ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1048', 1, N'����Ӣ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1049', 1, N'��Ѷʱ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1050', 1, N'�����װ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1051', 1, N'����ΰҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1052', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1053', 1, N'���ϻ�ƽ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1054', 1, N'³����־');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1055', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1056', 1, N'�����Ŵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1057', 1, N'��Դ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1058', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1059', 1, N'��豴���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1060', 1, N'�ںʹ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1061', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1062', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1063', 1, N'�������Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1064', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1065', 1, N'ɽ����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1066', 1, N'���ͻ�ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1067', 1, N'���Ϳ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1068', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1069', 1, N'�ƹ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1070', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1071', 1, N'ˮľ�ܻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1072', 1, N'˶��ΰҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1073', 1, N'˹̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1074', 1, N'̩������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1075', 1, N'��㻪��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1076', 1, N'ͬ�Ѵ�ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1077', 1, N'ΰ˼����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1078', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1079', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1080', 1, N'��ε�Ѱ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1081', 1, N'������ͨԴ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1082', 1, N'�����Ŵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1083', 1, N'�״���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1084', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1085', 1, N'������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1086', 1, N'�Ѱ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1087', 1, N'ԣԴ��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1088', 1, N'Զ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1089', 1, N'������ά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1090', 1, N'����ͨԴ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1091', 1, N'�����д���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1092', 1, N'�д�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1093', 1, N'�м���Ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1094', 1, N'������ʢ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1095', 1, N'������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1096', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1097', 1, N'���Ⱥϴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1098', 1, N'��Ϫ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1099', 1, N'����һ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1100', 1, N'BORRI');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1101', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1102', 1, N'�׿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1103', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1104', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1105', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1106', 1, N'����˳��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1107', 1, N'����̫ƽ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1108', 1, N'��������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1109', 1, N'����Զ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1110', 1, N'���˻���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1111', 1, N'�궨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1112', 1, N'�ɶ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1113', 1, N'�ɶ��ı�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1114', 1, N'�ɶ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1115', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1116', 1, N'�ɶ���ΰ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1117', 1, N'�ɶ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1118', 1, N'��Ȫ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1119', 1, N'�ɶ��ش�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1120', 1, N'�ɶ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1121', 1, N'��ҵ�װ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1122', 1, N'�ɶ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1123', 1, N'�ɶ�Ӣ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1124', 1, N'�ɶ���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1125', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1126', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1127', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1128', 1, N'���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1129', 1, N'��ʿ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1130', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1131', 1, N'����ɭԴ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1132', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1133', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1134', 1, N'������ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1135', 1, N'��˳����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1136', 1, N'̫�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1137', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1138', 1, N'�¹���ʿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1139', 1, N'�ɱ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1140', 1, N'OBO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1141', 1, N'�¹�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1142', 1, N'DEHN');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1143', 1, N'DEKA');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1144', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1145', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1146', 1, N'��ݸ��ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1147', 1, N'��ݸ��ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1148', 1, N'��ݸ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1149', 1, N'���չ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1150', 1, N'��ݸ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1151', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1152', 1, N'��ݸ÷��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1153', 1, N'��ݸ��Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1154', 1, N'��ݸԣ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1155', 1, N'��ݸ�ϴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1156', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1157', 1, N'�ɴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1158', 1, N'��ë��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1159', 1, N'�Ƿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1160', 1, N'PHOENIX');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1161', 1, N'�緫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1162', 1, N'��ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1163', 1, N'�Ϻ���Ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1164', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1165', 1, N'��ɽ����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1166', 1, N'��ɽ��ʢ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1167', 1, N'��ɽ��ï');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1168', 1, N'��ɽ��Ѹ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1169', 1, N'��ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1170', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1171', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1172', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1173', 1, N'���Ի���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1174', 1, N'�����շ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1175', 1, N'����Ȫ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1176', 1, N'����Խ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1177', 1, N'���ݸ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1178', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1179', 1, N'�����Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1180', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1181', 1, N'����ΰ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1182', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1183', 1, N'��˳����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1184', 1, N'���¶���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1185', 1, N'���ŵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1186', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1187', 1, N'��خ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1188', 1, N'��Ƶ�ٱ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1189', 1, N'�㶫���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1190', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1191', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1192', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1193', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1194', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1195', 1, N'�㶫������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1196', 1, N'������Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1197', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1198', 1, N'�㶫��ӱ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1199', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('12', 1, N'˼��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1200', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1201', 1, N'�Ϻ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1202', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1203', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1204', 1, N'��ͷ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1205', 1, N'��˳����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1206', 1, N'�㶫ʤ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1207', 1, N'��ɽ��ӿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1208', 1, N'˳���ر�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1209', 1, N'�㶫˳��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1210', 1, N'˳�ص���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1211', 1, N'˹�¶���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1212', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1213', 1, N'���ݿ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1214', 1, N'�Ŵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1215', 1, N'�㶫�Ż�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1216', 1, N'�㶫�ž�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1217', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1218', 1, N'�㶫ӯ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1219', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1220', 1, N'�㶫����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1221', 1, N'־�ɹھ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1222', 1, N'־��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1223', 1, N'��ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1224', 1, N'���̹�ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1225', 1, N'�㶫׿��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1226', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1227', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1228', 1, N'���ֿ��س�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1229', 1, N'���Ƶ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1230', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1231', 1, N'���ݴ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1232', 1, N'���ݴ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1233', 1, N'��خ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1234', 1, N'��خ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1235', 1, N'���ݷὭ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1236', 1, N'٤��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1237', 1, N'���ݹ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1238', 1, N'��˳����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1239', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1240', 1, N'���ݻ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1241', 1, N'���ݽ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1242', 1, N'���ݽ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1243', 1, N'���ݽ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1244', 1, N'���ݿ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1245', 1, N'���׹��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1246', 1, N'�����׷�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1247', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1248', 1, N'�����Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1249', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1250', 1, N'��غ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1251', 1, N'���ݺ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1252', 1, N'�꼨��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1253', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1254', 1, N'����ά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1255', 1, N'����ΰ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1256', 1, N'�¿�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1257', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1258', 1, N'������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1259', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1260', 1, N'����̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1261', 1, N'�����춫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1262', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1263', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1264', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1265', 1, N'����Ϊ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1266', 1, N'�糿');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1267', 1, N'�����źͳ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1268', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1269', 1, N'������ʢ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1270', 1, N'������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1271', 1, N'������Զ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1272', 1, N'�齭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1273', 1, N'�����Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1274', 1, N'�����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1275', 1, N'���ʾñ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1276', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1277', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1278', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1279', 1, N'���޵���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1280', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1281', 1, N'��Բ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1282', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1283', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1284', 1, N'����˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1285', 1, N'���Ϻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1286', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1287', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1288', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1289', 1, N'��־');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1290', 1, N'���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1291', 1, N'���ֿƼ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1292', 1, N'���ݰ¿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1293', 1, N'���ݸ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1294', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1295', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1296', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1297', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1298', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1299', 1, N'����ʥ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1300', 1, N'ʩ���ؿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1301', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1302', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1303', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1304', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1305', 1, N'����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1306', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1307', 1, N'�����ſ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1308', 1, N'�����Ҹ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1309', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1310', 1, N'����֮��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1311', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1312', 1, N'����׿��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1313', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1314', 1, N'�Ϸ�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1315', 1, N'�ӱ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1316', 1, N'�ӱ��ƻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1317', 1, N'�ȿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1318', 1, N'�ӱ��Ǻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1319', 1, N'�ӱ��ǰ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1320', 1, N'���ϻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1321', 1, N'���ϻ�Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1322', 1, N'���ϻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1323', 1, N'����﮶�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1324', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1325', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1326', 1, N'��ԭͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1327', 1, N'���ݵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1328', 1, N'��̫��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1329', 1, N'���쿪��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1330', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1331', 1, N'�豦');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1332', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1333', 1, N'�F�ϻ�е');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1334', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1335', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1336', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1337', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1338', 1, N'���Ϸ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1339', 1, N'���Ϲ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1340', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1341', 1, N'�������Ű�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1342', 1, N'��ɳ��ǿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1343', 1, N'���ݳ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1344', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1345', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1346', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1347', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1348', 1, N'���ԿƼ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1349', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1350', 1, N'���ִ�ɭ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1351', 1, N'���ֻ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1352', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1353', 1, N'�����͵�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1354', 1, N'�ò�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1355', 1, N'���ϲ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1356', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1357', 1, N'����־��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1358', 1, N'���˵��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1359', 1, N'�����ʴ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1360', 1, N'�»����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1361', 1, N'���հĻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1362', 1, N'���ձ�ʤ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1363', 1, N'���ձ�ʨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1364', 1, N'���ճ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1365', 1, N'���ճ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1366', 1, N'���յ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1367', 1, N'���յ϶���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1368', 1, N'���շ��ʿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1369', 1, N'���ո�˼��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1370', 1, N'���պ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1371', 1, N'���պ��Ĵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1372', 1, N'���ջ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1373', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1374', 1, N'���ջ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1375', 1, N'���ջ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1376', 1, N'���վ�¡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1377', 1, N'������ʿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1378', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1379', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1380', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1381', 1, N'����ŷ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1382', 1, N'�Ⱥ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1383', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1384', 1, N'����ʿ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1385', 1, N'˫��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1386', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1387', 1, N'�·�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1388', 1, N'��Դ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1389', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1390', 1, N'��ͨͨѶ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1391', 1, N'�������ſ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1392', 1, N'�㽭');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1393', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1394', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1395', 1, N'������Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1396', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1397', 1, N'�����в�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1398', 1, N'�����л�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1399', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1400', 1, N'����ʩά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1401', 1, N'���ݹ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1402', 1, N'����Զ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1403', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1404', 1, N'�Ŵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1405', 1, N'�Ž�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1406', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1407', 1, N'����˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1408', 1, N'���ն�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1409', 1, N'�ư���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1410', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1411', 1, N'�����Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1412', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1413', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1414', 1, N'���ݺ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1415', 1, N'��˹��˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1416', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1417', 1, N'���ǵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1418', 1, N'����ʿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1419', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1420', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1421', 1, N'��Ӻ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1422', 1, N'���˼');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1423', 1, N'���ά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1424', 1, N'÷������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1425', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1426', 1, N'GNB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1427', 1, N'ALLTEC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1428', 1, N'APC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1429', 1, N'GE');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1430', 1, N'CSB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1431', 1, N'ASCO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1432', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1433', 1, N'�ϲ�ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1434', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1435', 1, N'�Ͼ���ȫ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1436', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1437', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1438', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1439', 1, N'����ͼ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1440', 1, N'�Ͼ��װ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1441', 1, N'���ο�˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1442', 1, N'�Ͼ�ŷ½');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1443', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1444', 1, N'�Ͼ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1445', 1, N'�Ͼ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1446', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1447', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1448', 1, N'����ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1449', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1450', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1451', 1, N'�¿�˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1452', 1, N'������¡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1453', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1454', 1, N'����¡��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1455', 1, N'�����ͼ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1456', 1, N'����ŷ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1457', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1458', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1459', 1, N'�����찲');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1460', 1, N'����ʱ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1461', 1, N'ƽ��ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1462', 1, N'ƽ��ɽԥ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1463', 1, N'���˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1464', 1, N'Ǯ���ڽ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1465', 1, N'�ൺ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1466', 1, N'�ຣ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1467', 1, N'�廪����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1468', 1, N'Ȫ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1469', 1, N'����޺�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1470', 1, N'Ȫ��˳��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1471', 1, N'Ȫ����Ȫ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1472', 1, N'�ޱ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1473', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1474', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1475', 1, N'VOLVO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1476', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1477', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1478', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1479', 1, N'�����ع�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1480', 1, N'����Ͽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1481', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1482', 1, N'ɭ�ֺ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1483', 1, N'��Զ��ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1484', 1, N'�ƻ���ʢ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1485', 1, N'��ά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1486', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1487', 1, N'ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1488', 1, N'ɽ���Ի�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1489', 1, N'ɽ��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1490', 1, N'ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1491', 1, N'ɽ����ŵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1492', 1, N'ɽ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1493', 1, N'ɽ��ʥ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1494', 1, N'ɽ��ʱ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1495', 1, N'ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1496', 1, N'��Ǻ격');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1497', 1, N'����ɽ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1498', 1, N'ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1499', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1500', 1, N'ɽ��ŵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1501', 1, N'ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1502', 1, N'ɽ����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1503', 1, N'̫ԭ��е');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1504', 1, N'ͨͬ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1505', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1506', 1, N'������Ծ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1507', 1, N'������ɽ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1508', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1509', 1, N'��ͷ��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1510', 1, N'��ͷ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1511', 1, N'��ͷ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1512', 1, N'��ͷ�Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1513', 1, N'��ͷ��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1514', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1515', 1, N'�Ϻ���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1516', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1517', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1518', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1519', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1520', 1, N'�����޶�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1521', 1, N'�Ϻ���õ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1522', 1, N'�Ϻ���ŵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1523', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1524', 1, N'����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1525', 1, N'�Ϻ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1526', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1527', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1528', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1529', 1, N'�Ϻ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1530', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1531', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1532', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1533', 1, N'���ĺ�ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1534', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1535', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1536', 1, N'�ζ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1537', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1538', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1539', 1, N'�Ϻ���̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1540', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1541', 1, N'�Ϻ���ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1542', 1, N'�Ϻ��ȹ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1543', 1, N'�Ϻ��೽');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1544', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1545', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1546', 1, N'�Ϻ���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1547', 1, N'���յ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1548', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1549', 1, N'�Ϻ���Ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1550', 1, N'�Ϻ�ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1551', 1, N'�Ϻ�ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1552', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1553', 1, N'�Ϻ���᷶�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1554', 1, N'�Ϻ�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1555', 1, N'�Ϻ���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1556', 1, N'�׽���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1557', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1558', 1, N'�Ϻ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1559', 1, N'�Ϻ���ͼ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1560', 1, N'�Ϻ�֯��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1561', 1, N'�Ϻ���Զ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1562', 1, N'�Ϻ��з�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1563', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1564', 1, N'���˳���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1565', 1, N'�����̺�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1566', 1, N'����Ѹ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1567', 1, N'���ڰ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1568', 1, N'�̱ټ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1569', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1570', 1, N'����̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1571', 1, N'���ڶ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1572', 1, N'���ڸ��翵');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1573', 1, N'VAPEL');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1574', 1, N'���ڻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1575', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1576', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1577', 1, N'���ն�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1578', 1, N'��ʿ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1579', 1, N'��ʿ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1580', 1, N'�����պ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1581', 1, N'����ʵ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1582', 1, N'����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1583', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1584', 1, N'���ǵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1585', 1, N'��̩����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1586', 1, N'���ڸ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1587', 1, N'���ڹ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1588', 1, N'���ڹ�ҫ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1589', 1, N'���ں���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1590', 1, N'���ں�ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1591', 1, N'���ڻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1592', 1, N'���ǹ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1593', 1, N'����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1594', 1, N'���ڿ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1595', 1, N'���ڿ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1596', 1, N'����Ψ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1597', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1598', 1, N'�պ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1599', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1600', 1, N'��ҵͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1601', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1602', 1, N'�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1603', 1, N'�ķ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1604', 1, N'�����ط�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1605', 1, N'ΰ�ֽ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1606', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1607', 1, N'���+B884');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1608', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1609', 1, N'�⻪��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1610', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1611', 1, N'Ӣά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1612', 1, N'���������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1613', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1614', 1, N'����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1615', 1, N'��������¶');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1616', 1, N'����Զ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1617', 1, N'������ά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1618', 1, N'��Զͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1619', 1, N'���ݾ޵�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1620', 1, N'������ҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1621', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1622', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1623', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1624', 1, N'�����ƺ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1625', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1626', 1, N'������ά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1627', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1628', 1, N'������˳��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1629', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1630', 1, N'����Զ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1631', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1632', 1, N'ʥ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1633', 1, N'ʢ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1634', 1, N'ʩ�͵�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1635', 1, N'���ֵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1636', 1, N'DANCO');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1637', 1, N'STULZ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1638', 1, N'�Ĵ��￨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1639', 1, N'�Ĵ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1640', 1, N'�Ĵ���ʿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1641', 1, N'�Ĵ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1642', 1, N'�Ĵ���Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1643', 1, N'����ɭ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1644', 1, N'�Ĵ�˹����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1645', 1, N'�Ƚ��γ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1646', 1, N'�����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1647', 1, N'�Ĵ��й�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1648', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1649', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1650', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1651', 1, N'���ݽܳ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1652', 1, N'�º���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1653', 1, N'�º격');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1654', 1, N'����ҫ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1655', 1, N'SOCOMEC');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1656', 1, N'̫ԭ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1657', 1, N'̫ԭΰҵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1658', 1, N'̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1659', 1, N'THOMSON');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1660', 1, N'��ǳ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1661', 1, N'�쿪����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1662', 1, N'�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1663', 1, N'�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1664', 1, N'����ʿ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1665', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1666', 1, N'��򿵵�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1667', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1668', 1, N'�����Ѹ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1669', 1, N'�����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1670', 1, N'�Ӷ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1671', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1672', 1, N'�����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1673', 1, N'�쿪����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1674', 1, N'���Эʢ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1675', 1, N'����ر�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1676', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1677', 1, N'����о�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1678', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1679', 1, N'��ˮ��һ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1680', 1, N'ͨ��ʢ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1681', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1682', 1, N'������¡');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1683', 1, N'Ϋ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1684', 1, N'Ϋ��̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1685', 1, N'ΰ�ܻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1686', 1, N'���ݲ�̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1687', 1, N'���ݴ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1688', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1689', 1, N'���ݿ�Ԫ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1690', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1691', 1, N'�����Ϸ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1692', 1, N'�����ٷ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1693', 1, N'��Դ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1694', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1695', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1696', 1, N'��Ϫ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1697', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1698', 1, N'�ߺ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1699', 1, N'�ߵº���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1700', 1, N'�人��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1701', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1702', 1, N'�人�׹�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1703', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1704', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1705', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1706', 1, N'�人ά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1707', 1, N'�ִ��߿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1708', 1, N'�人�δ�ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1709', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1710', 1, N'�人��̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1711', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1712', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1713', 1, N'�人����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1714', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1715', 1, N'������Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1716', 1, N'����ǰ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1717', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1718', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1719', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1720', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1721', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1722', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1723', 1, N'�½�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1724', 1, N'���ܾ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1725', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1726', 1, N'�½�˫��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1727', 1, N'�½��ر�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1728', 1, N'�½�����Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1729', 1, N'�½�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1730', 1, N'��̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1731', 1, N'���籦��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1732', 1, N'�����д�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1733', 1, N'������¹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1734', 1, N'��֣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1735', 1, N'�˰���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1736', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1737', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1738', 1, N'������˼��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1739', 1, N'�ٺ���ʲ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1740', 1, N'��̨��ԭ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1741', 1, N'��̨����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1742', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1743', 1, N'���ݾ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1744', 1, N'���ݻ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1745', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1746', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1747', 1, N'������˹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1748', 1, N'������ɯ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1749', 1, N'���׿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1750', 1, N'�״�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1751', 1, N'����΢��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1752', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1753', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1754', 1, N'��ʹ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1755', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1756', 1, N'Ӣ�ؼ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1757', 1, N'����ά��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1758', 1, N'��Ҧ��ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1759', 1, N'��Ҧ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1760', 1, N'�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1761', 1, N'���ϰ�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1762', 1, N'��ͨ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1763', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1764', 1, N'�˳�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1765', 1, N'��Ҵ���س�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1766', 1, N'���ݿƻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1767', 1, N'���ǵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1768', 1, N'������ɫ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1769', 1, N'��ɳ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1770', 1, N'ҵͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1771', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1772', 1, N'���컪��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1773', 1, N'����Ļ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1774', 1, N'�����涥');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1775', 1, N'�����߿�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1776', 1, N'�㽭��Ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1777', 1, N'�㽭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1778', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1779', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1780', 1, N'�㽭������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1781', 1, N'�㽭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1782', 1, N'�㽭�ܰ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1783', 1, N'�㽭��Ծ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1784', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1785', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1786', 1, N'�㽭�ѱ�˼');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1787', 1, N'�𻪵翪');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1788', 1, N'�㽭��֮·');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1789', 1, N'�϶�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1790', 1, N'Ǯ���ɷ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1791', 1, N'�㽭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1792', 1, N'�㽭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1793', 1, N'�㽭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1794', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1795', 1, N'�˺���Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1796', 1, N'�㽭��̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1797', 1, N'�㽭�и�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1798', 1, N'�򽭶�ʥ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1799', 1, N'Ĭ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('18', 1, N'��ʿͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1800', 1, N'���Ƕ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1801', 1, N'֣�ݴ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1802', 1, N'���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1803', 1, N'֣������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1804', 1, N'�д�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1805', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1806', 1, N'�й�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1807', 1, N'�п�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1808', 1, N'�й���ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1809', 1, N'�й��ر�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1810', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1811', 1, N'�е���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1812', 1, N'�п�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1813', 1, N'�пƺ�Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1814', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1815', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1816', 1, N'��ɽ�ں�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1817', 1, N'��ɽ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1818', 1, N'��ɽ������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1819', 1, N'������Դ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1820', 1, N'���ʿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1821', 1, N'���ݵ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1822', 1, N'���첩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1823', 1, N'���첩ɭ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1824', 1, N'����ɿ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1825', 1, N'�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1826', 1, N'�����˳');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1827', 1, N'����Ͽ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1828', 1, N'���춫��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1829', 1, N'���춫ŵ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1830', 1, N'���츳��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1831', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1832', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1833', 1, N'�����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1834', 1, N'���쿭����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1835', 1, N'���쿭�׶�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1836', 1, N'���쿵ï');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1837', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1838', 1, N'����¡��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1839', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1840', 1, N'���첩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1841', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1842', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1843', 1, N'����ܵ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1844', 1, N'����˹��ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1845', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1846', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1847', 1, N'������̩');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1848', 1, N'����������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1849', 1, N'����ҫ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1850', 1, N'���쳤��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1851', 1, N'���쳤��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1852', 1, N'������ά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1853', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1854', 1, N'�����д���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1855', 1, N'�����ں�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1856', 1, N'�ر����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1857', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1858', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1859', 1, N'�麣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1860', 1, N'�麣���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1861', 1, N'�Ϸ�����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1862', 1, N'�麣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1863', 1, N'�麣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1864', 1, N'�麣������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1865', 1, N'�麣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1866', 1, N'�麣����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1867', 1, N'�麣̩̹');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1868', 1, N'�麣ӯԴ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1869', 1, N'���շ���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1870', 1, N'פ����ʤ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1871', 1, N'פ���껪��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('1872', 1, N'�Ͳ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('19', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('21', 1, N'��Ĭ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('22', 1, N'������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('23', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('24', 1, N'���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('26', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('27', 1, N'����ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('28', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('29', 1, N'ABB');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('30', 1, N'MTU');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('31', 1, N'���춯��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('32', 1, N'��Ѷ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('36', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('37', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('38', 1, N'����־��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('39', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('41', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('42', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('43', 1, N'����ͨ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('44', 1, N'���ر���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('45', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('46', 1, N'��ά');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('47', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('48', 1, N'�����ĺ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('49', 1, N'��Ԫ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('50', 1, N'������ͨ');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('52', 1, N'�Ĵ����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('53', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('55', 1, N'��ʿ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('57', 1, N'�������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('58', 1, N'Լ��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('59', 1, N'�Ǵ￵');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('60', 1, N'�к�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('61', 1, N'��̫');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('62', 1, N'�첨');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('63', 1, N'�Ѻ�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('64', 1, N'��˹����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('65', 1, N'��������');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('66', 1, N'�׹���');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('80', 1, N'����');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('81', 1, N'�˳�');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('82', 1, N'����̩��');
INSERT INTO [dbo].[C_Productor]([ID],[Enabled],[Name]) VALUES('99', 1, N'�Ͼ���Ԫ');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_Brand]
DELETE FROM [dbo].[C_Brand];
GO

INSERT INTO [dbo].[C_Brand]([ID],[Enabled],[Name],[ProductorID]) VALUES('00', 1, N'Ĭ��Ʒ��', '00');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_RoomType]
DELETE FROM [dbo].[C_RoomType];
GO

INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('01', N'��ۻ���');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('02', N'��վ����');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('11', N'�������');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('12', N'��������');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('13', N'��ػ���');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('14', N'�յ�����');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('51', N'�������');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('52', N'��������');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('53', N'���ݻ���');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('54', N'IDC����');
INSERT INTO [dbo].[C_RoomType]([ID],[Name]) VALUES('55', N'�ۺϻ���');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_SCVendor]
DELETE FROM [dbo].[C_SCVendor]
GO

INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('01', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('02', N'��Ĭ��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('03', N'�д�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('04', N'ӯ��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('05', N'����ķ');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('06', N'��ά��ͨ');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('07', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('08', N'��Ѷ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('09', N'������');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('10', N'��������');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('11', N'�Ϲ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('12', N'ҵͨ��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('13', N'�Ϻ�����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('14', N'��Ѷ');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('15', N'���');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('16', N'��Դ����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('17', N'�����Ŵ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('19', N'��Ѷ');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('20', N'�ǰ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('21', N'���');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('22', N'�Ϸ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('23', N'դ��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('24', N'�������');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('25', N'��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('26', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('27', N'�����Ʋ�');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('28', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('29', N'��Ϊ');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('70', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('71', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('72', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('73', N'���');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('74', N'����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('75', N'ά��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('76', N'������');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('77', N'ά��');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('78', N'������');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('79', N'�����');
INSERT INTO [dbo].[C_SCVendor]([ID],[Name]) VALUES('99', N'�й�����');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_StationType]
DELETE FROM [dbo].[C_StationType];
GO

INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('1', N'��������');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('2', N'ͨ�Ż�¥');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('3', N'����ڵ�');
INSERT INTO [dbo].[C_StationType]([ID],[Name]) VALUES('4', N'ͨ�Ż�վ');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_SubCompany]
DELETE FROM [dbo].[C_SubCompany];
GO

INSERT INTO [dbo].[C_SubCompany]([ID],[Enabled],[Name]) VALUES('00', 1, N'Ĭ�ϴ�ά��˾');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_SubDeviceType]
DELETE FROM [dbo].[C_SubDeviceType];
GO

INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0' ,'0', N'δ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0101' ,'01', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0102' ,'01', N'��ѹ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0103' ,'01', N'��ѹ��������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0104' ,'01', N'��ѹ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0105' ,'01', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0106' ,'01', N'��ѹѹ���');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0107' ,'01', N'��ѹĸ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0108' ,'01', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0109' ,'01', N'��ѹ���ݲ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0110' ,'01', N'��ѹ�ͻ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0111' ,'01', N'��ѹ�л���');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0112' ,'01', N'��ѹ������Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0201' ,'02', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0202' ,'02', N'��ѹ���߼�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0203' ,'02', N'��ѹ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0204' ,'02', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0205' ,'02', N'��ѹ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0206' ,'02', N'��ѹATS�л���');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0207' ,'02', N'��ѹ���ݲ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0208' ,'02', N'��ѹ�ͻ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0209' ,'02', N'Ӧ���ͻ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0210' ,'02', N'Ӧ���ͻ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0211' ,'02', N'��ѹ¥������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0212' ,'02', N'��ѹ��������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0213' ,'02', N'���ص�Դ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0214' ,'02', N'UPS�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0215' ,'02', N'��ѹֱ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0216' ,'02', N'�е罻�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0217' ,'02', N'����������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0218' ,'02', N'��Դ�˲��豸');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0219' ,'02', N'��Դ�˲��豸');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0301' ,'03', N'��ʽ��ѹ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0302' ,'03', N'�ͽ���ѹ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0303' ,'03', N'�Ǿ��Ͻ��ѹ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0304' ,'03', N'��ѹ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0401' ,'04', N'24Vֱ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0402' ,'04', N'-48Vֱ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0403' ,'04', N'-48Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0404' ,'04', N'-48Vֱ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0501' ,'05', N'���ͷ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0502' ,'05', N'ȼ���ַ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0503' ,'05', N'���ͷ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0504' ,'05', N'ȼ�ϵ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0601' ,'06', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0602' ,'06', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0603' ,'06', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0604' ,'06', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0701' ,'07', N'UPSǦ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0702' ,'07', N'���ص�ԴǦ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0703' ,'07', N'��ѹֱ��Ǧ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0704' ,'07', N'������ԴǦ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0801' ,'08', N'��ƵUPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0802' ,'08', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0803' ,'08', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0901' ,'09', N'UPS�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0902' ,'09', N'UPS�����ͷ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('0903' ,'09', N'UPS��������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1101' ,'11', N'����ר�ÿյ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1102' ,'11', N'ˮ��ר�ÿյ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1103' ,'11', N'˫��Դר�ÿյ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1201' ,'12', N'�䶳ˮר�ÿյ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1202' ,'12', N'�ȹܱ���');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1203' ,'12', N'ˮ��ǰ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1204' ,'12', N'ˮ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1205' ,'12', N'�м�յ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1206' ,'12', N'Ƕ��ʽ�յ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1301' ,'13', N'����������յ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1302' ,'13', N'�����ݸ���ˮ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1303' ,'13', N'ˮ���ݸ���ˮ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1304' ,'13', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1305' ,'13', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1306' ,'13', N'������ȴ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1307' ,'13', N'������ȴ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1401' ,'14', N'�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1402' ,'14', N'DC/DC�豸');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1403' ,'14', N'ֱ��Զ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1404' ,'14', N'ֱ��Զ��Զ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1501' ,'15', N'��ʽ�յ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1502' ,'15', N'�ڹ�ʽ�յ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1601' ,'16', N'�������̸�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1701' ,'17', N'�¶�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1702' ,'17', N'ʪ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1703' ,'17', N'ˮ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1704' ,'17', N'�̸�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1705' ,'17', N'����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1706' ,'17', N'����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1707' ,'17', N'�Ŵ�');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1708' ,'17', N'�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('1801' ,'18', N'��غ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6801' ,'68', N'UPS﮵��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6802' ,'68', N'���ص�Դ﮵��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6803' ,'68', N'��ѹֱ��﮵��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('6804' ,'68', N'������Դ﮵��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7601' ,'76', N'CSC');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7602' ,'76', N'LSC');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7603' ,'76', N'FSU');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7604' ,'76', N'���زɼ��豸');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7701' ,'77', N'����ͨ��ϵͳ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7702' ,'77', N'���ܻ���ϵͳ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7801' ,'78', N'����������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7802' ,'78', N'̫���ܿ�����');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('7803' ,'78', N'��⻥��������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8701' ,'87', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8702' ,'87', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8703' ,'87', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8704' ,'87', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8801' ,'88', N'240Vֱ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8802' ,'88', N'240Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8803' ,'88', N'240Vֱ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8804' ,'88', N'336Vֱ������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8805' ,'88', N'336Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('8806' ,'88', N'336Vֱ�������');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9201' ,'92', N'�������ܵ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9202' ,'92', N'ֱ�����ܵ��');
INSERT INTO [dbo].[C_SubDeviceType]([ID],[DeviceTypeID],[Name]) VALUES('9301' ,'93', N'�����Ž�');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_SubLogicType]
DELETE FROM [dbo].[C_SubLogicType];
GO

INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('0' ,'0', N'δ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010101' ,'0101', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010102' ,'0101', N'��ѹ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010103' ,'0101', N'��ѹ��������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010104' ,'0101', N'��ѹ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010105' ,'0101', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010106' ,'0101', N'��ѹѹ���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010107' ,'0101', N'��ѹĸ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010108' ,'0101', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010109' ,'0101', N'��ѹ���ݲ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010110' ,'0101', N'��ѹ�ͻ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010111' ,'0101', N'��ѹ�л���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('010201' ,'0102', N'��ѹ������Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020101' ,'0201', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020102' ,'0201', N'��ѹ���߼�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020103' ,'0201', N'��ѹ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020104' ,'0201', N'��ѹ���߹�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020105' ,'0201', N'��ѹ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020106' ,'0201', N'��ѹATS�л���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020107' ,'0201', N'��ѹ�ͻ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020108' ,'0201', N'Ӧ���ͻ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020109' ,'0201', N'Ӧ���ͻ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020110' ,'0201', N'��ѹ¥������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020111' ,'0201', N'��ѹ��������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020112' ,'0201', N'���ص�Դ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020113' ,'0201', N'UPS�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020114' ,'0201', N'��ѹֱ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020115' ,'0201', N'�е罻�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020116' ,'0201', N'����������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020201' ,'0202', N'��ѹ���ݲ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020301' ,'0203', N'��Դ�˲��豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('020302' ,'0203', N'��Դ�˲��豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030101' ,'0301', N'��ʽ��ѹ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030102' ,'0301', N'�ͽ���ѹ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030103' ,'0301', N'�Ǿ��Ͻ��ѹ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('030104' ,'0301', N'��ѹ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040101' ,'0401', N'24Vֱ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040102' ,'0401', N'-48Vֱ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040103' ,'0401', N'-48Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('040104' ,'0401', N'-48Vֱ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050101' ,'0501', N'���ͷ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050102' ,'0501', N'ȼ���ַ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050103' ,'0501', N'���ͷ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('050201' ,'0502', N'ȼ�ϵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060101' ,'0601', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060102' ,'0601', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060103' ,'0601', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060104' ,'0601', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060201' ,'0602', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060202' ,'0602', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060203' ,'0602', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060204' ,'0602', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060301' ,'0603', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060302' ,'0603', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060303' ,'0603', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060304' ,'0603', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060401' ,'0604', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060402' ,'0604', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060403' ,'0604', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060404' ,'0604', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060501' ,'0605', N'�������ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060502' ,'0605', N'��Ͽ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060503' ,'0605', N'�ڹҿ��ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('060504' ,'0605', N'Ƕ�뿪�ص�Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070101' ,'0701', N'UPSǦ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070102' ,'0701', N'���ص�ԴǦ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070103' ,'0701', N'��ѹֱ��Ǧ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('070104' ,'0701', N'������ԴǦ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080101' ,'0801', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080102' ,'0801', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080103' ,'0801', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080201' ,'0802', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080202' ,'0802', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080203' ,'0802', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080301' ,'0803', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080302' ,'0803', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080303' ,'0803', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080401' ,'0804', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080402' ,'0804', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080403' ,'0804', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080501' ,'0805', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080502' ,'0805', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080503' ,'0805', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080601' ,'0806', N'��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080602' ,'0806', N'һ�廯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('080603' ,'0806', N'ģ�黯��ƵUPS');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090101' ,'0901', N'UPS�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090102' ,'0901', N'UPS�����ͷ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('090103' ,'0901', N'UPS��������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110101' ,'1101', N'����ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110102' ,'1101', N'ˮ��ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110103' ,'1101', N'˫��Դר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110201' ,'1102', N'����ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110202' ,'1102', N'ˮ��ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110203' ,'1102', N'˫��Դר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110301' ,'1103', N'����ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110302' ,'1103', N'ˮ��ר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('110303' ,'1103', N'˫��Դר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120101' ,'1201', N'�䶳ˮר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120102' ,'1201', N'�ȹܱ���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120103' ,'1201', N'ˮ��ǰ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120104' ,'1201', N'ˮ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120105' ,'1201', N'�м�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120106' ,'1201', N'Ƕ��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120201' ,'1202', N'�䶳ˮר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120202' ,'1202', N'�ȹܱ���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120203' ,'1202', N'ˮ��ǰ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120204' ,'1202', N'ˮ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120205' ,'1202', N'�м�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120206' ,'1202', N'Ƕ��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120301' ,'1203', N'�䶳ˮר�ÿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120302' ,'1203', N'�ȹܱ���');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120303' ,'1203', N'ˮ��ǰ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120304' ,'1203', N'ˮ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120305' ,'1203', N'�м�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('120306' ,'1203', N'Ƕ��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130101' ,'1301', N'����������յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130102' ,'1301', N'�����ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130103' ,'1301', N'ˮ���ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130104' ,'1301', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130105' ,'1301', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130106' ,'1301', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130107' ,'1301', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130201' ,'1302', N'����������յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130202' ,'1302', N'�����ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130203' ,'1302', N'ˮ���ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130204' ,'1302', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130205' ,'1302', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130206' ,'1302', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130207' ,'1302', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130301' ,'1303', N'����������յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130302' ,'1303', N'�����ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130303' ,'1303', N'ˮ���ݸ���ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130304' ,'1303', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130305' ,'1303', N'��ѹ������ˮ����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130306' ,'1303', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('130307' ,'1303', N'������ȴ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140101' ,'1401', N'�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140102' ,'1401', N'DC/DC�豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140103' ,'1401', N'ֱ��Զ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140104' ,'1401', N'ֱ��Զ��Զ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140201' ,'1402', N'�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140202' ,'1402', N'DC/DC�豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140203' ,'1402', N'ֱ��Զ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140204' ,'1402', N'ֱ��Զ��Զ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140301' ,'1403', N'�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140302' ,'1403', N'DC/DC�豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140303' ,'1403', N'ֱ��Զ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('140304' ,'1403', N'ֱ��Զ��Զ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150101' ,'1501', N'��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150102' ,'1501', N'�ڹҿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150201' ,'1502', N'��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150202' ,'1502', N'�ڹҿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150301' ,'1503', N'��ʽ�յ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('150302' ,'1503', N'�ڹҿյ�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('160101' ,'1601', N'�������̸�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('160201' ,'1602', N'�������̸�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170101' ,'1701', N'ˮ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170201' ,'1702', N'����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170301' ,'1703', N'����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170401' ,'1704', N'�¶�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170501' ,'1705', N'ʪ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170601' ,'1706', N'����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170701' ,'1707', N'����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170801' ,'1708', N'��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('170901' ,'1709', N'����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('171001' ,'1710', N'�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('180101' ,'1801', N'��غ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680101' ,'6801', N'UPS﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680102' ,'6801', N'���ص�Դ﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680103' ,'6801', N'��ѹֱ��﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680104' ,'6801', N'������Դ﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680201' ,'6802', N'UPS﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680202' ,'6802', N'���ص�Դ﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680203' ,'6802', N'��ѹֱ��﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('680204' ,'6802', N'������Դ﮵��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760101' ,'7601', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760102' ,'7601', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760103' ,'7601', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760104' ,'7601', N'���زɼ��豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760201' ,'7602', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760202' ,'7602', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760203' ,'7602', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760204' ,'7602', N'���زɼ��豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760301' ,'7603', N'CSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760302' ,'7603', N'LSC');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760303' ,'7603', N'FSU');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('760304' ,'7603', N'���زɼ��豸');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770101' ,'7701', N'����ͨ��ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770102' ,'7701', N'���ܻ���ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770201' ,'7702', N'����ͨ��ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770202' ,'7702', N'���ܻ���ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770301' ,'7703', N'����ͨ��ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('770302' ,'7703', N'���ܻ���ϵͳ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780101' ,'7801', N'����������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780102' ,'7801', N'̫���ܿ�����');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('780103' ,'7801', N'��⻥��������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870101' ,'8701', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870102' ,'8701', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870103' ,'8701', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870104' ,'8701', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870201' ,'8702', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870202' ,'8702', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870203' ,'8702', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870204' ,'8702', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870301' ,'8703', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870302' ,'8703', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870303' ,'8703', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870304' ,'8703', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870401' ,'8704', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870402' ,'8704', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870403' ,'8704', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870404' ,'8704', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870501' ,'8705', N'������ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870502' ,'8705', N'��ϸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870503' ,'8705', N'�ڹҸ�ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('870504' ,'8705', N'Ƕ���ѹֱ����Դ');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880101' ,'8801', N'240Vֱ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880102' ,'8801', N'240Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880103' ,'8801', N'240Vֱ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880104' ,'8801', N'336Vֱ������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880105' ,'8801', N'336Vֱ����ͷ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('880106' ,'8801', N'336Vֱ�������');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920101' ,'9201', N'�������ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920102' ,'9201', N'ֱ�����ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920201' ,'9202', N'�������ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920202' ,'9202', N'ֱ�����ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920301' ,'9203', N'�������ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('920302' ,'9203', N'ֱ�����ܵ��');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930101' ,'9301', N'�����Ž�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930201' ,'9302', N'�����Ž�');
INSERT INTO [dbo].[C_SubLogicType]([ID],[LogicTypeID],[Name]) VALUES('930301' ,'9303', N'�����Ž�');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_Supplier]
DELETE FROM [dbo].[C_Supplier];
GO

INSERT INTO [dbo].[C_Supplier]([ID],[Enabled],[Name]) VALUES('00', 1, N'Ĭ�Ϲ�Ӧ��');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[C_EnumMethods]
DELETE FROM [dbo].[C_EnumMethods]
GO

INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1001, 1, 1, N'ʡ', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1002, 2, 1, N'��', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(1003, 3, 1, N'��(��)', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2001, 1, 2, N'����', N'�е����뷽ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2002, 2, 2, N'�ܿ�', N'�е����뷽ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2003, 1, 2, N'ת��', N'��������');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(2004, 2, 2, N'ֱ��', N'��������');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3001, 1, 3, N'�Խ�', N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3002, 2, 3, N'����', N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(3003, 3, 3, N'����', N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4001, 1, 4, N'����', N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4002, 2, 4, N'ֻ��', N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(4003, 3, 4, N'��д', N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5001, 1, 5, N'N+1(N>1)', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5002, 2, 5, N'1+1����', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5003, 3, 5, N'����1+1', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5004, 4, 5, N'����', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(5005, 5, 5, N'˫ĸ��', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(6001, 1, 6, N'��ʽ', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(6002, 2, 6, N'�ͽ�ʽ', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7001, 1, 7, N'�Զ�����', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7002, 2, 7, N'�ֶ�����', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7003, 1, 7, N'����', N'��ȴ��ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(7004, 2, 7, N'ˮ��', N'��ȴ��ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(8001, 1, 8, N'ˮƽ��', N'�������');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(8002, 2, 8, N'��ֱ��', N'�������');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9001, 1, 9, N'����', N'���Ӽ���');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9002, 2, 9, N'����', N'���Ӽ���');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9003, 1, 9, N'ֱ��', N'��ֱ����ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(9004, 2, 9, N'����', N'��ֱ����ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10001, 1, 10, N'�Թ�', N'��Դ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10002, 2, 10, N'����', N'��Դ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10003, 1, 10, N'���ͻ�', N'�ͻ�����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10004, 2, 10, N'���ͻ�', N'�ͻ�����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10005, 1, 10, N'����', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(10006, 2, 10, N'����', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11001, 1, 11, N'����ʽ', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11002, 2, 11, N'����ʽ', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11003, 3, 11, N'���ݸ�ʽ', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(11004, 4, 11, N'˫�ݸ�ʽ', N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(12001, 1, 12, N'�ͻ����е�', N'�л�����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(12002, 2, 12, N'�ͻ����ͻ�', N'�л�����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13001, 0, 13, N'����-����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13002, 1, 13, N'����-����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13003, 2, 13, N'����-����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13004, 3, 13, N'����-����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13005, 4, 13, N'����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13006, 5, 13, N'����', N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13007, 0, 13, N'Ĭ��', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13008, 51, 13, N'��ѹ���ϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13009, 52, 13, N'��ѹ���ϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13010, 53, 13, N'UPSϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13011, 54, 13, N'���ص�Դϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13012, 55, 13, N'��ѹֱ��ϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13013, 56, 13, N'����ϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(13014, 57, 13, N'����յ�ϵͳ', N'ϵͳ����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14001, 1, 14, N'>', N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14002, 2, 14, N'<', N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14003, 3, 14, N'=', N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(14004, 4, 14, N'!=', N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15001, 0, 15, N'����', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15002, 1, 15, N'��ר', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15003, 2, 15, N'����', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15004, 3, 15, N'�о���', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15005, 4, 15, N'��ʿ��ʿ��', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15006, 5, 15, N'����', N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15007, 0, 15, N'δ��', N'����״��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15008, 1, 15, N'�ѻ�', N'����״��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(15009, 2, 15, N'����', N'����״��');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16001, 0, 16, N'�ƶ�B�ӿ�', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16002, 1, 16, N'����B�ӿ�', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16003, 2, 16, N'��ͨB�ӿ�', N'����');
INSERT INTO [dbo].[C_EnumMethods]([ID],[Index],[TypeID],[Name],[Desc]) VALUES(16004, 3, 16, N'���нӿ�', N'����');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[Sys_Menu]
DELETE FROM [dbo].[Sys_Menu]
GO

INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('1', 1, '0', 1, N'�����ʲ�', 'Assets.png', 'Asset_Panel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('2', 1, '0', 2, N'�豸����', 'Tools.png', '', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('3', 1, '0', 3, N'��׼�ֵ�', 'Books.png', '', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('4', 1, '0', 4, N'ϵͳ����', 'Sys.png', '', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('5', 1, '0', 5, N'�Ž�����', 'Door.png', NULL, 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('6', 1, '0', 6, N'��Ƶ����', 'Video.png', 'Video_View', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('100', 1, '1', 1, N'����', 'Area.png', 'Area_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('101', 1, '1', 1, N'վ��', 'Station.png', 'Station_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('102', 1, '1', 1, N'����', 'Room.png', 'Room_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('103', 1, '1', 1, N'�豸', 'Device.png', 'Device_Panel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('104', 1, '1', 1, N'ģ��', 'Protocol.png', 'Protocol_View', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('105', 1, '3', 3, N'��׼�ź�', 'Point.png', 'Point_View', 8, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('106', 1, '1', 1, N'FSU', 'Fsu.png', 'FSU_Panel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('107', 1, '1', 1, N'ͨѶ', 'Bus.png', 'Bus_View', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('108', 1, '107', 1, N'����', 'Driver.png', 'Driver_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('200', 1, '2', 2, N'��������', 'DevTool.png', 'Productor_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('201', 1, '2', 2, N'Ʒ��', 'DevTool.png', 'Brand_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('202', 1, '2', 2, N'��Ӧ��', 'DevTool.png', 'Supplier_GrdPanel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('203', 1, '2', 2, N'��ά��˾', 'DevTool.png', 'SubCompany_GrdPanel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('300', 1, '3', 3, N'վ������', 'Book.png', 'StaType_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('301', 1, '3', 3, N'��������', 'Book.png', 'RoomType_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('302', 1, '3', 3, N'�豸����', 'Book.png', 'DevType_GrdPanel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('303', 1, '3', 3, N'�豸����', 'Book.png', 'SubDevType_GrdPanel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('304', 1, '3', 3, N'�߼�����', 'Book.png', 'LogicType_GrdPanel', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('305', 1, '3', 3, N'�߼�����', 'Book.png', 'SubLogicType_GrdPanel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('306', 1, '3', 3, N'��λ״̬', 'Book.png', 'Unit_GrdPanel', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('307', 1, '3', 3, N'ö������', 'Book.png', 'EnumMethod_GrdPanel', 9, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('308', 1, '3', 3, N'�ɼ���Ⱥ', 'Book.png', 'GroupForm', 10, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('401', 1, '4', 4, N'��Ա', 'Users.png', '', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('402', 1, '4', 4, N'��ɫ', 'Role.png', 'Role_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('403', 1, '4', 4, N'�˺�', 'UID.png', 'User_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('404', 1, '4', 4, N'ά��', 'Maintain.png', '', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('500', 1, '5', 5, N'ʱ��', 'Time.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('501', 1, '5', 5, N'�ſ�', 'Card.png', 'Card_View', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('502', 1, '5', 5, N'������', 'accessControl.png', NULL, 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('600', 1, '6', 6, N'��Ӱ��', 'Camera.png', 'Camera_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10301', 1, '103', 1, N'UPS', 'DevType.png', 'UPS_Panel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10302', 1, '103', 1, N'��ѹ��', 'DevType.png', 'Transformer_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10303', 1, '103', 1, N'��غ�����', 'DevType.png', 'BattTempBox_Panel', 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10304', 1, '103', 1, N'�������', 'DevType.png', 'GeneratorGroup_Panel', 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10305', 1, '103', 1, N'���п��ص�Դϵͳ', 'DevType.png', 'DivSwitElecSour_Panel', 5, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10306', 1, '103', 1, N'��Ͽ��ص�Դϵͳ', 'DevType.png', 'CombSwitElecSour_Panel', 6, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10307', 1, '103', 1, N'��������', 'DevType.png', 'WindPowerCon_Panel', 7, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10308', 1, '103', 1, N'��⻥��������', 'DevType.png', 'WindLightCompCon_Panel', 8, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10309', 1, '103', 1, N'�����豸', 'DevType.png', 'WindEnerEqui_Panel', 9, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10310', 1, '103', 1, N'��ѹ����', 'DevType.png', 'HighVoltDistBox_Panel', 10, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10311', 1, '103', 1, N'����ϵͳ', 'DevType.png', 'ChangeHeat_Panel', 11, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10312', 1, '103', 1, N'���������', 'DevType.png', 'ACDistBox_Panel', 12, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10313', 1, '103', 1, N'������˿', 'DevType.png', 'SwitchFuse_Panel', 24, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10314', 1, '103', 1, N'�����', 'DevType.png', 'Inverter_Panel', 14, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10315', 1, '103', 1, N'��ͨ�յ�', 'DevType.png', 'OrdiAirCond_Panel', 15, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10316', 1, '103', 1, N'��ѹ����', 'DevType.png', 'LowDistCabinet_Panel', 16, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10317', 1, '103', 1, N'̫���ܿ�����', 'DevType.png', 'SolarController_Panel', 17, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10318', 1, '103', 1, N'̫�����豸', 'DevType.png', 'SolarEqui_Panel', 18, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10319', 1, '103', 1, N'ͨ��ϵͳ', 'DevType.png', 'Ventilation_Panel', 19, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10320', 0, '103', 1, N'����豸', 'DevType.png', '', 20, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10321', 1, '103', 1, N'��ѹ��', 'DevType.png', 'Manostat_Panel', 21, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10322', 1, '103', 1, N'������', 'DevType.png', 'BattGroup_Panel', 22, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10323', 1, '103', 1, N'�ƶ������', 'DevType.png', 'MobiGenerator_Panel', 23, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10324', 1, '103', 1, N'ֱ�������', 'DevType.png', 'DCDistBox_Panel', 13, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10325', 1, '103', 1, N'����յ����ϵͳ', 'DevType.png', 'AirCondWindCabi_Panel', 25, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10326', 1, '103', 1, N'����յ�����ȴϵͳ', 'DevType.png', 'AirCondWindCool_Panel', 26, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10327', 1, '103', 1, N'����յ�����ϵͳ', 'DevType.png', 'AirCondHost_Panel', 27, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10328', 1, '103', 1, N'ר�ÿյ�', 'DevType.png', 'SpecAirCond_Panel', 28, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10329', 1, '103', 1, N'�Զ���Դ�л���', 'DevType.png', 'ElecSourCabi_Panel', 29, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('10500', 1, '105', 1, N'����', 'SubPoint.png', 'SubPoint_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40101', 1, '401', 2, N'ְλ', 'Duty.png', 'Duty_GrdPanel', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40102', 1, '401', 2, N'����', 'Department.png', 'Department_GrdPanel', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40103', 1, '401', 2, N'Ա��', 'User.png', 'Employee_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40400', 1, '404', 4, N'����', 'UpGrade.png', 'UpGrade_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40401', 0, '404', 4, N'����', 'Redundancy.png', '', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('40402', 1, '404', 4, N'��־', 'UserLog.png', 'UserLog_Panel', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50001', 1, '500', 5, N'����', 'workTime.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50002', 1, '500', 5, N'����', 'workTime.png', NULL, 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50003', 1, '500', 5, N'����', 'workTime.png', NULL, 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50004', 1, '500', 5, N'�ڼ���', 'workTime.png', NULL, 4, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50005', 1, '500', 5, N'��ĩ', 'workTime.png', NULL, 3, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50100', 1, '501', 5, N'��Э', 'OutEmployee.png', 'OutEmployee_View', 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50101', 1, '501', 5, N'����', 'bindCard.png', 'CardsInEmployee_View', 1, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50102', 1, '501', 5, N'��Ȩ', 'cardGrand.png', 'AuthorizationCard_View', 2, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50201', 1, '502', 5, N'��ʱ', 'TimeGrand.png', NULL, 0, GETDATE(), NULL);
INSERT INTO [dbo].[Sys_Menu]([ID],[Enabled],[ParentID],[Type],[Name],[NameImg],[Url],[Index],[CreateTime],[Desc]) VALUES('50202', 1, '502', 5, N'��Ȩ', 'cardGrand.png', 'DoorAuthorization_View', 1, GETDATE(), NULL);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[U_Employee]
DELETE FROM [dbo].[U_Employee];
GO

INSERT INTO [dbo].[U_Employee]([ID],[Name],[EngName],[UsedName],[EmpNo],[DeptId],[DutyId],[ICardId],[Sex],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving],[EntryTime],[RetireTime],[IsFormal],[Remarks],[Enabled]) 
VALUES('00001', 'Ĭ��Ա��', 'Default Employee', NULL, 'W00001', '001', '001', '310000198501010001', 0, '1985-01-01', 4, 1, N'�й�', N'�Ϻ�', N'����', N'�Ϻ����ֶ�����', '200000', '68120000', '58660000', '13800138000', '13800138000@vip.com', NULL, 0, '2011-01-01', '2099-12-31' , 1, NULL, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[U_User]
DELETE FROM [dbo].[U_User];
GO

INSERT INTO [dbo].[U_User]([ID],[EmpID],[Enabled],[UID],[PWD],[PwdFormat],[PwdSalt],[RoleID],[OnlineTime],[LimitTime],[CreateTime],[LastLoginTime],[LastPwdChangedTime],[FailedPwdAttemptCount],[FailedPwdTime],[IsLockedOut],[LastLockoutTime],[Remark]) 
VALUES ('1', '00', 1, 'PECS', 'PECS@1234', 0, '', 0, GETDATE(), '2099-12-31 23:59:59', GETDATE(), GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE(), '');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[U_Role]
DELETE FROM [dbo].[U_Role];
GO

INSERT INTO [dbo].[U_Role]([ID],[Enabled],[Name],[Authority],[Desc]) VALUES(0,1,'��������Ա','All','ϵͳĬ�Ͻ�ɫ');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--����Ĭ��ֵ[dbo].[U_MenusInRole]
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