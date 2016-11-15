/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/11/15
*/

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[C_Department]
DELETE FROM [dbo].[C_Department];
GO

INSERT INTO [dbo].[C_Department]([Id],[Code],[Name],[TypeDesc],[Phone],[PostCode],[ParentId],[Desc],[Enabled]) VALUES('001','X001',N'Ĭ�ϲ���',NULL,NULL,NULL,0,NULL,1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[C_Duty]
DELETE FROM [dbo].[C_Duty];
GO

INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('001', N'���³�', 1, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('002', N'�ܾ���', 2, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('003', N'���ž���', 3, NULL, 1);
INSERT INTO [dbo].[C_Duty]([Id],[Name],[Level],[Desc],[Enabled]) VALUES('004', N'Ա��', 4, NULL, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[C_EnumMethods]
DELETE FROM [dbo].[C_EnumMethods];
GO

INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1001,N'ʡ',1,1,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1002,N'��',1,2,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(1003,N'��',1,3,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2001,N'����',2,1,N'�е����뷽ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2002,N'�ܿ�',2,2,N'�е����뷽ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2003,N'ת��',2,1,N'��������');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(2004,N'ֱ��',2,2,N'��������');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3001,N'�Խ�',3,1,N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3002,N'����',3,2,N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(3003,N'����',3,3,N'��Ȩ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4001,N'����',4,1,N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4002,N'ֻ��',4,2,N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(4003,N'��д',4,3,N'Ȩ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5001,N'N+1(N>1)',5,1,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5002,N'1+1����',5,2,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5003,N'����1+1',5,3,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5004,N'����',5,4,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(5005,N'˫ĸ��',5,5,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(6001,N'��ʽ',6,1,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(6002,N'�ͽ�ʽ',6,2,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7001,N'�Զ�����',7,1,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7002,N'�ֶ�����',7,2,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7003,N'����',7,1,N'��ȴ��ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(7004,N'ˮ��',7,2,N'��ȴ��ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(8001,N'ˮƽ��',8,1,N'�������');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(8002,N'��ֱ��',8,2,N'�������');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9001,N'����',9,1,N'���Ӽ���');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9002,N'����',9,2,N'���Ӽ���');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9003,N'ֱ��',9,1,N'��ֱ����ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(9004,N'����',9,2,N'��ֱ����ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10001,N'�Թ�',10,1,N'��Դ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10002,N'����',10,2,N'��Դ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10003,N'���ͻ�',10,1,N'�ͻ�����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10004,N'���ͻ�',10,2,N'�ͻ�����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10005,N'����',10,1,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(10006,N'����',10,2,N'����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11001,N'����ʽ',11,1,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11002,N'����ʽ',11,2,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11003,N'���ݸ�ʽ',11,3,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(11004,N'˫�ݸ�ʽ',11,4,N'������ʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(12001,N'�ͻ����е�',12,1,N'�л�����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(12002,N'�ͻ����ͻ�',12,2,N'�л�����');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13001,N'����-����',13,0,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13002,N'����-����',13,1,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13003,N'����-����',13,2,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13004,N'����-����',13,3,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13005,N'����',13,4,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(13006,N'����',13,5,N'ʹ��״̬');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14001,N'>',14,1,N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14002,N'<',14,2,N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14003,N'=',14,3,N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(14004,N'!=',14,4,N'����ģʽ');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15001,N'����',15,0,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15002,N'��ר',15,1,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15003,N'����',15,2,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15004,N'�о���',15,3,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15005,N'��ʿ��ʿ��',15,4,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15006,N'����',15,5,N'ѧ��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15007,N'��',15,0,N'����״��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15008,N'�ѻ�',15,1,N'����״��');
INSERT INTO [dbo].[C_EnumMethods]([Id],[Name],[TypeId],[Index],[Desc]) VALUES(15009,N'����',15,2,N'����״��');
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_Employee]
DELETE FROM [dbo].[U_Employee];
GO

INSERT INTO [dbo].[U_Employee]([Id],[Name],[EngName],[UsedName],[EmpNo],[DeptId],[DutyId],[ICardId],[Sex],[Birthday],[Degree],[Marriage],[Nation],[Provinces],[Native],[Address],[PostalCode],[AddrPhone],[WorkPhone],[MobilePhone],[Email],[Photo],[Leaving],[EntryTime],[RetireTime],[IsFormal],[Remarks],[Enabled]) 
VALUES('00001','Ĭ��Ա��','Default Employee',NULL,'W00001','001','001','310000198501010001',0,'1985-01-01',4,1,N'�й�',N'�Ϻ�',N'����',N'�Ϻ����ֶ�����','200000','68120000','58660000','13800138000','13800138000@vip.com',NULL,0,'2010-01-01','2050-01-01',1,NULL,1);
GO