--��������������������������������������������������������������������������������������������������������������������������������������
--�������ע���¼[dbo].[M_Dictionary]
IF NOT EXISTS (SELECT 1 FROM [dbo].[M_Dictionary] WHERE [Id]=5)
BEGIN
	INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) 
	VALUES(5,N'���ע��',NULL,NULL,GETDATE());
END
GO

--��������������������������������������������������������������������������������������������������������������������������������������
--�����ű�������־
DECLARE @Id VARCHAR(100) = 'P2S_V1_006';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'�������ע�Ṧ��','Steven',GETDATE(),NULL,GETDATE(),'�������ע�Ṧ��');
GO
