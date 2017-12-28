--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增菜单「放电次数统计」
DELETE FROM [dbo].[U_Menus] WHERE [Id] = 400211;
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400211, N'放电次数统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'放电次数统计提供了系统中所有蓄电池的放电次数、放电过程、放电曲线的查询统计、数据导出等功能。', 11, 4002, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增脚本升级日志
DECLARE @Id VARCHAR(100) = 'P2S_V1_001';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'新增「放电次数统计」报表','Steven',GETDATE(),NULL,GETDATE(),'新增「放电次数统计」报表');
GO