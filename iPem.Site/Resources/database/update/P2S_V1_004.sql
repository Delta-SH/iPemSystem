--��������������������������������������������������������������������������������������������������������������������������������������
--����[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus] WHERE [Id] = 1005;
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005, N'��������', '/content/themes/icons/menu-csgl.png', '/Account/Dictionary', N'���������Ƕ�ϵͳ��������ĸ���������й���ά�����û��ɸ���ϵͳʵ�ʵ�ҵ���߼����������ʺϵĲ�����<br/>�ܺĹ�ʽ˵��:<br/>1. ��ʽ���ԡ�@��������>>�豸����>>�ź����ơ�����ʽӳ��һ�������豸���źŵ㡣<br/>2. �������ơ��豸���ơ��ź������н�ֹ���֡�@������>>������+������-������*������/������(������)���ȷ��š�<br/>��ʽʾ��: ((@����1>>�豸1>>�ź�1 + @����2>>�豸2>>�ź�2 - @����3>>�豸3>>�ź�3) * @����4>>�豸4>>�ź�4) / @����5>>�豸5>>�ź�5', 5, 1, 1);
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�����ű�������־
DECLARE @Id VARCHAR(100) = 'P2S_V1_004';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'���²��������˵�','Steven',GETDATE(),NULL,GETDATE(),'���²��������˵�����˵����Ϣ');
GO