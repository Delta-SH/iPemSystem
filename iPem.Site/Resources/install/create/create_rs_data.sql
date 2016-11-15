/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/11/15
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Department]
DELETE FROM [dbo].[C_Department];
GO

INSERT INTO [dbo].[C_Department]([Id],[Code],[Name],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc],[Enabled]) VALUES('001','X001',N'默认部门',NULL,NULL,NULL,0,NULL,1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_Duty]
DELETE FROM [dbo].[C_Duty];
GO

INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('001', N'董事长', 1, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('002', N'总经理', 2, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('003', N'部门经理', 3, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('004', N'员工', 4, NULL, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[C_EnumMethods]
DELETE FROM [dbo].[C_EnumMethods];
GO

INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1001,N'省',1,1,N'类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1002,N'市',1,2,N'类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1003,N'县',1,3,N'类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2001,N'地埋',2,1,N'市电引入方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2002,N'架空',2,2,N'市电引入方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2003,N'转供',2,1,N'供电性质');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2004,N'直供',2,2,N'供电性质');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3001,N'自建',3,1,N'产权');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3002,N'购买',3,2,N'产权');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3003,N'租用',3,3,N'产权');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4001,N'禁用',4,1,N'权限');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4002,N'只读',4,2,N'权限');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4003,N'读写',4,3,N'权限');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5001,N'N+1(N>1)',5,1,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5002,N'1+1主从',5,2,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5003,N'单机1+1',5,3,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5004,N'均分',5,4,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5005,N'双母线',5,5,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(6001,N'干式',6,1,N'类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(6002,N'油浸式',6,2,N'类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7001,N'自动启动',7,1,N'启动方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7002,N'手动启动',7,2,N'启动方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7003,N'风冷',7,1,N'冷却方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7004,N'水冷',7,2,N'冷却方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(8001,N'水平轴',8,1,N'风机类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(8002,N'垂直轴',8,2,N'风机类型');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9001,N'三相',9,1,N'端子极数');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9002,N'单相',9,2,N'端子极数');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9003,N'直流',9,1,N'交直流方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9004,N'交流',9,2,N'交直流方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10001,N'自购',10,1,N'来源');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10002,N'租用',10,2,N'来源');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10003,N'柴油机',10,1,N'油机种类');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10004,N'汽油机',10,2,N'油机种类');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10005,N'单相',10,1,N'相数');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10006,N'三相',10,2,N'相数');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11001,N'活塞式',11,1,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11002,N'涡旋式',11,2,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11003,N'单螺杆式',11,3,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11004,N'双螺杆式',11,4,N'工作方式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(12001,N'油机与市电',12,1,N'切换对象');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(12002,N'油机与油机',12,2,N'切换对象');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13001,N'在用-良好',13,0,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13002,N'在用-故障',13,1,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13003,N'闲置-良好',13,2,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13004,N'闲置-故障',13,3,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13005,N'返修',13,4,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13006,N'报废',13,5,N'使用状态');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14001,N'>',14,1,N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14002,N'<',14,2,N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14003,N'=',14,3,N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14004,N'!=',14,4,N'触发模式');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15001,N'高中',15,0,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15002,N'大专',15,1,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15003,N'本科',15,2,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15004,N'研究生',15,3,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15005,N'博士或博士后',15,4,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15006,N'其他',15,5,N'学历');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15007,N'否',15,0,N'婚姻状况');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15008,N'已婚',15,1,N'婚姻状况');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15009,N'其他',15,2,N'婚姻状况');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Employee]
DELETE FROM [dbo].[U_Employee];
GO

INSERT INTO [dbo].[U_Employee]([Id],[Name],[EngName],[UsedName],[EmpNo],[DeptId],[DutyId],[ICardId],[Sex],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving],[EntryTime],[RetireTime],[IsFormal],[Remarks],[Enabled]) 
VALUES('00001','默认员工','Default Employee',NULL,'W00001','001','001','310000198501010001',0,'1985-01-01',4,1,N'中国',N'上海',N'江苏',N'上海市浦东新区','200000','68120000','58660000','13800138000','13800138000@vip.com',NULL,0,'2010-01-01','2050-01-01',1,NULL,1);
GO