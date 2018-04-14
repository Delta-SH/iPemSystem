--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增软件注册记录[dbo].[M_Dictionary]
IF NOT EXISTS (SELECT 1 FROM [dbo].[M_Dictionary] WHERE [Id]=5)
BEGIN
	INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) 
	VALUES(5,N'软件注册',NULL,NULL,GETDATE());
END
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增脚本升级日志
DECLARE @Id VARCHAR(100) = 'P2S_V1_006';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'新增软件注册功能','Steven',GETDATE(),NULL,GETDATE(),'新增软件注册功能');
GO
