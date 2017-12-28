--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--更新[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus] WHERE [Id] = 1005;
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005, N'参数管理', '/content/themes/icons/menu-csgl.png', '/Account/Dictionary', N'参数管理是对系统运行所需的各类参数进行管理维护，用户可根据系统实际的业务逻辑需求配置适合的参数。<br/>能耗公式说明:<br/>1. 公式中以“@机房名称>>设备名称>>信号名称”的形式映射一个具体设备的信号点。<br/>2. 机房名称、设备名称、信号名称中禁止出现“@”、“>>”、“+”、“-”、“*”、“/”、“(”、“)”等符号。<br/>公式示例: ((@机房1>>设备1>>信号1 + @机房2>>设备2>>信号2 - @机房3>>设备3>>信号3) * @机房4>>设备4>>信号4) / @机房5>>设备5>>信号5', 5, 1, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--新增脚本升级日志
DECLARE @Id VARCHAR(100) = 'P2S_V1_004';
DELETE FROM [dbo].[H_DBScript] WHERE [Id] = @Id;
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES(@Id,'更新参数管理菜单','Steven',GETDATE(),NULL,GETDATE(),'更新参数管理菜单帮助说明信息');
GO