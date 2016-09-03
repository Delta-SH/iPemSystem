/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/09/02
*/

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[M_Dictionary]
DELETE FROM [dbo].[M_Dictionary];
GO

INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(1,N'����ͨ��',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(2,N'��������',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(3,N'�ܺķ���',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(4,N'�������',NULL,NULL,GETDATE());
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--���Ĭ��ֵ[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus];
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1,N'ϵͳ����','/content/themes/icons/menu.png',NULL,N'ϵͳ����',1,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2,N'���ù���','/content/themes/icons/menu.png',NULL,N'���ù���',2,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3,N'�������','/content/themes/icons/menu.png',NULL,N'�������',3,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4,N'ϵͳ����','/content/themes/icons/menu.png',NULL,N'ϵͳ����',4,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5,N'ϵͳָ��','/content/themes/icons/menu.png',NULL,N'ϵͳָ��',5,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1001,N'��ɫ����','/content/themes/icons/leaf.png','/Account/Roles',N'��ɫ��ָϵͳ���Ȩ�޵ļ����飬ʹ�ý�ɫ���Լ�Ȩ�޹���ʵ���û�Ȩ�޵�ͳһ���������ɫ������Ҫʵ���˽�ɫ��Ϣ���������༭��ɾ������ɫȨ�޷���ȹ��ܡ�',1,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1002,N'�û�����','/content/themes/icons/leaf.png','/Account/Users',N'�û���ָ��¼ϵͳ������˺�������Ϣ���û�����������ĳ����ɫ���̳иý�ɫ��ӵ�е�ϵͳȨ�ޡ��û�������Ҫʵ�����û���Ϣ���������༭��ɾ�����������õȹ��ܡ�',2,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1003,N'��־����','/content/themes/icons/leaf.png','/Account/Events',N'��־�����Ƕ�ϵͳ�м�¼��ϵͳ�쳣�������¼�����־��Ϣ���й���ά�����û����Բ�ѯ������ϵͳ��־��¼������ɾ�����ڵ���־��¼�Ȳ�����',3,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1004,N'��Ϣ����','/content/themes/icons/leaf.png','/Account/Notice',N'��Ϣ�����Ƕ�վ�ڵĹ�����Ϣ�����桢֪ͨ����ʾ����Ϣ���з��������û����Բ�ѯ�����������¡�ɾ��վ�ڵĹ㲥��Ϣ��',4,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005,N'ϵͳ����','/content/themes/icons/leaf.png','/Account/Dictionary',N'ϵͳ�����Ƕ�ϵͳ��������ĸ���������й���ά�����û��ɸ���ϵͳʵ�ʵ�ҵ���߼����������ʺϵĲ�����',5,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2001,N'ģ�����','/content/themes/icons/leaf.png','/Account/2001',N'ģ�����',1,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2002,N'�������','/content/themes/icons/leaf.png','/Account/2002',N'�������',2,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2003,N'��վ����','/content/themes/icons/leaf.png','/Account/2003',N'��վ����',3,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2004,N'��������','/content/themes/icons/leaf.png','/Account/2004',N'��������',4,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2005,N'�豸����','/content/themes/icons/leaf.png','/Account/2005',N'�豸����',5,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2006,N'���̹���','/content/themes/icons/leaf.png','/Project/Index',N'���̹����Ƕ�ϵͳ���漰�Ĺ�����Ŀ����ά�����������ƺ����ı���ͳ�ƹ��ܡ�ע���򹤳�ԤԼ��Ҫ���ù�����Ϣ��Ϊ�˱�֤����ԤԼ��Ϣ�������ԣ��ݲ��ṩɾ�����̹��ܡ�',6,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2007,N'����ԤԼ','/content/themes/icons/leaf.png','/Project/Appointment',N'����ԤԼ�Ƕ�ʩ����������Ӱ�쵽������վ�㡢�������豸�Ƚ�����ǰԤԼ��ϵͳ�����ԤԼ��Ϣ��������ĸ澯��ʶΪ���̸澯��Ϊ��������ͳ���ṩ����֧�֡�ע����澯��Ϣ��Ҫ���ù���ԤԼ��Ϣ��Ϊ�˱�֤�澯��Ϣ�������ԣ��ݲ��ṩɾ��ԤԼ���ܡ�',7,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3001,N'3D��̬','/content/themes/icons/leaf.png','/Configuration/Index',N'3D��̬',1,3,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3002,N'���ӵ�ͼ','/content/themes/icons/leaf.png','/Configuration/Map',N'���ӵ�ͼ',2,3,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4001,N'��������','/content/themes/icons/menu.png',NULL,N'��������',1,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4002,N'��ʷ����','/content/themes/icons/menu.png',NULL,N'��ʷ����',2,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4003,N'ͼ�α���','/content/themes/icons/menu.png',NULL,N'ͼ�α���',3,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4004,N'���Ʊ���','/content/themes/icons/menu.png',NULL,N'���Ʊ���',4,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5001,N'����ָ��','/content/themes/icons/menu.png',NULL,N'����ָ��',1,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5002,N'����ָ��','/content/themes/icons/menu.png',NULL,N'����ָ��',2,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5003,N'����ָ��','/content/themes/icons/menu.png',NULL,N'����ָ��',3,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400101,N'����ͳ��','/content/themes/icons/leaf.png','/Report/Base',N'����ͳ��',1,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400102,N'վ��ͳ��','/content/themes/icons/leaf.png','/Report/Base',N'վ��ͳ��',2,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400103,N'����ͳ��','/content/themes/icons/leaf.png','/Report/Base',N'����ͳ��',3,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400104,N'�豸ͳ��','/content/themes/icons/leaf.png','/Report/Base',N'�豸ͳ��',4,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400201,N'��ֵ��ѯ','/content/themes/icons/leaf.png','/Report/History',N'��ֵ��ѯ',1,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400202,N'�澯��ѯ','/content/themes/icons/leaf.png','/Report/History',N'�澯��ѯ',2,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400203,N'�澯����ͳ��','/content/themes/icons/leaf.png','/Report/History',N'�澯����ͳ��',3,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400204,N'�豸�澯ͳ��','/content/themes/icons/leaf.png','/Report/History',N'�豸�澯ͳ��',4,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400205,N'������Ŀͳ��','/content/themes/icons/leaf.png','/Report/History',N'������Ŀͳ��',5,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400206,N'����ԤԼͳ��','/content/themes/icons/leaf.png','/Report/History',N'����ԤԼͳ��',6,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400207,N'�е�ͣ��ͳ��','/content/themes/icons/leaf.png','/Report/History',N'�е�ͣ��ͳ��<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�е�ͣ��"��Ĳ�����Ϣ��',7,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400208,N'�ͻ�����ͳ��','/content/themes/icons/leaf.png','/Report/History',N'�ͻ�����ͳ��<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�ͻ�����"��Ĳ�����Ϣ��',8,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400301,N'�źŲ�ֵ����','/content/themes/icons/leaf.png','/Report/Chart',N'�źŲ�ֵ����',1,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400302,N'�ź�ͳ������','/content/themes/icons/leaf.png','/Report/Chart',N'�ź�ͳ������',2,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400303,N'��طŵ�����','/content/themes/icons/leaf.png','/Report/Chart',N'��طŵ�����',3,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400401,N'��Ƶ�澯ͳ��','/content/themes/icons/leaf.png','/Report/Custom',N'��Ƶ�澯ͳ��<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��',1,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400402,N'���̸澯ͳ��','/content/themes/icons/leaf.png','/Report/Custom',N'���̸澯ͳ��<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��',2,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400403,N'�����澯ͳ��','/content/themes/icons/leaf.png','/Report/Custom',N'�����澯ͳ��<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>�쳣�澯"��Ĳ�����Ϣ��',3,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500101,N'ϵͳ�豸�����','/content/themes/icons/leaf.png','/KPI/Base',N'ϵͳ�豸����� = {1���豸�澯��ʱ�� / (�豸���� �� ͳ��ʱ��)} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>ϵͳ�豸�����"��Ĳ�����Ϣ��',1,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500102,N'���ϴ���ʱ��','/content/themes/icons/leaf.png','/KPI/Base',N'���ϴ���ʱ�� = {1�������涨����ʱ�����豸���ϴ��� / �豸�����ܴ���} �� 100%<br/>ע����ʹ��֮ǰ����ȷ����������"ϵͳ����>ϵͳ����>�������>���ϴ���ʱ��"��Ĳ�����Ϣ��',2,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500103,N'�澯ȷ�ϼ�ʱ��','/content/themes/icons/leaf.png','/KPI/Base',N'�澯ȷ�ϼ�ʱ�� = {1�������涨ȷ��ʱ���ĸ澯���� / �澯������} �� 100%',3,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500104,N'�澯����ѹ����','/content/themes/icons/leaf.png','/KPI/Base',N'�澯����ѹ���� = {(���ι����еĴθ澯�� �� ���������е�ԭʼ�澯�� - ���������е������澯��) / 1~3���澯ԭʼ�������(�������澯)} �� 100%',4,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500105,N'��ؿ��ö�','/content/themes/icons/leaf.png','/KPI/Base',N'��ؿ��ö� = {1 - �ɼ��豸�жϸ澯ʱ�� / (�ɼ��豸���� �� ͳ��ʱ��)} �� 100%',5,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500106,N'�е���ö�','/content/themes/icons/leaf.png','/KPI/Base',N'�е���ö� = {1 - �е�ͣ��澯ʱ�� / (�е�·�� �� ͳ��ʱ��)} �� 100%',6,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500107,N'�¿�ϵͳ���ö�','/content/themes/icons/leaf.png','/KPI/Base',N'�¿�ϵͳ���ö� = {1 - ���¸澯ʱ�� / (�¶Ȳ������ �� ͳ��ʱ��)} �� 100%',7,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500108,N'ֱ��ϵͳ���ö�','/content/themes/icons/leaf.png','/KPI/Base',N'ֱ��ϵͳ���ö� = {1 - ���ص�Դ�������ܵ�ѹ�͸澯ʱ�� / (���ص�Դ�������� �� ͳ��ʱ��)} �� 100%',8,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500109,N'���������ϵͳ���ö�','/content/themes/icons/leaf.png','/KPI/Base',N'���������ϵͳ���ö� = {1 - (UPS�������ܵ�ѹ�͸澯ʱ�� + UPS��·����ʱ��) / (UPS�������� �� ͳ��ʱ��)} �� 100%',9,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500201,N'�ܺ��ۺ�ͳ��','/content/themes/icons/leaf.png','/KPI/Performance',N'ϵͳ�ܺ�ָ��',1,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500202,N'�ܺķ���ռ��','/content/themes/icons/leaf.png','/KPI/Performance',N'ϵͳ�ܺ�ռ��',2,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500203,N'ϵͳPUEָ��','/content/themes/icons/leaf.png','/KPI/Performance',N'ϵͳPUEָ��',3,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500204,N'ֱ��ϵͳ������','/content/themes/icons/leaf.png','/KPI/Performance',N'ֱ��ϵͳ������',4,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500205,N'���������ϵͳ������','/content/themes/icons/leaf.png','/KPI/Performance',N'���������ϵͳ������',5,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500206,N'�ߵ�ѹ���ϵͳ������','/content/themes/icons/leaf.png','/KPI/Performance',N'�ߵ�ѹ���ϵͳ������',6,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500301,N'��ظ�����','/content/themes/icons/leaf.png','/KPI/Custom',N'��ظ�����',1,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500302,N'վ���ʶ��','/content/themes/icons/leaf.png','/KPI/Custom',N'վ���ʶ��',2,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500303,N'�¿������ϸ���','/content/themes/icons/leaf.png','/KPI/Custom',N'�¿������ϸ���',3,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500304,N'�ؼ���ز�������','/content/themes/icons/leaf.png','/KPI/Custom',N'�ؼ���ز�������',4,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500305,N'���ص�Դ���غϸ���','/content/themes/icons/leaf.png','/KPI/Custom',N'���ص�Դ���غϸ���',5,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500306,N'���غ�ʱ���ϸ���','/content/themes/icons/leaf.png','/KPI/Custom',N'���غ�ʱ���ϸ���',6,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500307,N'���غ˶��Էŵ缰ʱ��','/content/themes/icons/leaf.png','/KPI/Custom',N'���غ˶��Էŵ缰ʱ��',7,5003,1);
GO