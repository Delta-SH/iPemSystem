--��������������������������������������������������������������������������������������������������������������������������������������
--�����˵����ŵ����ͳ�ơ�
DELETE FROM [dbo].[U_Menus] WHERE [Id] = 400211;
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400211, N'�ŵ����ͳ��', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'�ŵ����ͳ���ṩ��ϵͳ���������صķŵ�������ŵ���̡��ŵ����ߵĲ�ѯͳ�ơ����ݵ����ȹ��ܡ�', 11, 4002, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�����ű�������־
DECLARE @Id VARCHAR(100) = 'P2S_V1_001';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'�������ŵ����ͳ�ơ�����','Steven',GETDATE(),NULL,GETDATE(),'�������ŵ����ͳ�ơ�����');
GO