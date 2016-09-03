/*
* Web Default Data Sql Script Library v1.0.0
* Copyright 2016, Delta
* Author: Steven
* Date: 2016/09/02
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[M_Dictionary]
DELETE FROM [dbo].[M_Dictionary];
GO

INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(1,N'数据通信',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(2,N'语音播报',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(3,N'能耗分类',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(4,N'报表参数',NULL,NULL,GETDATE());
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus];
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1,N'系统管理','/content/themes/icons/menu.png',NULL,N'系统管理',1,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2,N'配置管理','/content/themes/icons/menu.png',NULL,N'配置管理',2,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3,N'虚拟界面','/content/themes/icons/menu.png',NULL,N'虚拟界面',3,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4,N'系统报表','/content/themes/icons/menu.png',NULL,N'系统报表',4,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5,N'系统指标','/content/themes/icons/menu.png',NULL,N'系统指标',5,0,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1001,N'角色管理','/content/themes/icons/leaf.png','/Account/Roles',N'角色是指系统相关权限的集合组，使用角色可以简化权限管理，实现用户权限的统一分配管理。角色管理主要实现了角色信息的新增、编辑、删除及角色权限分配等功能。',1,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1002,N'用户管理','/content/themes/icons/leaf.png','/Account/Users',N'用户是指登录系统所需的账号密码信息，用户必须隶属于某个角色并继承该角色所拥有的系统权限。用户管理主要实现了用户信息的新增、编辑、删除及密码重置等功能。',2,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1003,N'日志管理','/content/themes/icons/leaf.png','/Account/Events',N'日志管理是对系统中记录的系统异常、操作事件等日志信息进行管理维护。用户可以查询、导出系统日志记录，定期删除过期的日志记录等操作。',3,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1004,N'消息管理','/content/themes/icons/leaf.png','/Account/Notice',N'消息管理是对站内的公开消息、公告、通知、提示等信息进行发布管理。用户可以查询、发布、更新、删除站内的广播消息。',4,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005,N'系统参数','/content/themes/icons/leaf.png','/Account/Dictionary',N'系统参数是对系统运行所需的各类参数进行管理维护。用户可根据系统实际的业务逻辑需求配置适合的参数。',5,1,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2001,N'模版管理','/content/themes/icons/leaf.png','/Account/2001',N'模版管理',1,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2002,N'区域管理','/content/themes/icons/leaf.png','/Account/2002',N'区域管理',2,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2003,N'局站管理','/content/themes/icons/leaf.png','/Account/2003',N'局站管理',3,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2004,N'机房管理','/content/themes/icons/leaf.png','/Account/2004',N'机房管理',4,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2005,N'设备管理','/content/themes/icons/leaf.png','/Account/2005',N'设备管理',5,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2006,N'工程管理','/content/themes/icons/leaf.png','/Project/Index',N'工程管理是对系统所涉及的工程项目进行维护管理，以完善后续的报表统计功能。注：因工程预约需要引用工程信息，为了保证工程预约信息的完整性，暂不提供删除工程功能。',6,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2007,N'工程预约','/content/themes/icons/leaf.png','/Project/Appointment',N'工程预约是对施工过程中所影响到的区域、站点、机房、设备等进行提前预约，系统会根据预约信息将其产生的告警标识为工程告警，为后续报表统计提供数据支持。注：因告警信息需要引用工程预约信息，为了保证告警信息的完整性，暂不提供删除预约功能。',7,2,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3001,N'3D组态','/content/themes/icons/leaf.png','/Configuration/Index',N'3D组态',1,3,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3002,N'电子地图','/content/themes/icons/leaf.png','/Configuration/Map',N'电子地图',2,3,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4001,N'基础报表','/content/themes/icons/menu.png',NULL,N'基础报表',1,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4002,N'历史报表','/content/themes/icons/menu.png',NULL,N'历史报表',2,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4003,N'图形报表','/content/themes/icons/menu.png',NULL,N'图形报表',3,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4004,N'订制报表','/content/themes/icons/menu.png',NULL,N'订制报表',4,4,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5001,N'基础指标','/content/themes/icons/menu.png',NULL,N'基础指标',1,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5002,N'性能指标','/content/themes/icons/menu.png',NULL,N'性能指标',2,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5003,N'订制指标','/content/themes/icons/menu.png',NULL,N'订制指标',3,5,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400101,N'区域统计','/content/themes/icons/leaf.png','/Report/Base',N'区域统计',1,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400102,N'站点统计','/content/themes/icons/leaf.png','/Report/Base',N'站点统计',2,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400103,N'机房统计','/content/themes/icons/leaf.png','/Report/Base',N'机房统计',3,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400104,N'设备统计','/content/themes/icons/leaf.png','/Report/Base',N'设备统计',4,4001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400201,N'测值查询','/content/themes/icons/leaf.png','/Report/History',N'测值查询',1,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400202,N'告警查询','/content/themes/icons/leaf.png','/Report/History',N'告警查询',2,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400203,N'告警分类统计','/content/themes/icons/leaf.png','/Report/History',N'告警分类统计',3,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400204,N'设备告警统计','/content/themes/icons/leaf.png','/Report/History',N'设备告警统计',4,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400205,N'工程项目统计','/content/themes/icons/leaf.png','/Report/History',N'工程项目统计',5,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400206,N'工程预约统计','/content/themes/icons/leaf.png','/Report/History',N'工程预约统计',6,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400207,N'市电停电统计','/content/themes/icons/leaf.png','/Report/History',N'市电停电统计<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>市电停电"里的参数信息。',7,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400208,N'油机发电统计','/content/themes/icons/leaf.png','/Report/History',N'油机发电统计<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>油机发电"里的参数信息。',8,4002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400301,N'信号测值曲线','/content/themes/icons/leaf.png','/Report/Chart',N'信号测值曲线',1,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400302,N'信号统计曲线','/content/themes/icons/leaf.png','/Report/Chart',N'信号统计曲线',2,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400303,N'电池放电曲线','/content/themes/icons/leaf.png','/Report/Chart',N'电池放电曲线',3,4003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400401,N'超频告警统计','/content/themes/icons/leaf.png','/Report/Custom',N'超频告警统计<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。',1,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400402,N'超短告警统计','/content/themes/icons/leaf.png','/Report/Custom',N'超短告警统计<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。',2,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400403,N'超长告警统计','/content/themes/icons/leaf.png','/Report/Custom',N'超长告警统计<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。',3,4004,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500101,N'系统设备完好率','/content/themes/icons/leaf.png','/KPI/Base',N'系统设备完好率 = {1－设备告警总时长 / (设备数量 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>系统设备完好率"里的参数信息。',1,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500102,N'故障处理及时率','/content/themes/icons/leaf.png','/KPI/Base',N'故障处理及时率 = {1－超出规定处理时长的设备故障次数 / 设备故障总次数} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>故障处理及时率"里的参数信息。',2,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500103,N'告警确认及时率','/content/themes/icons/leaf.png','/KPI/Base',N'告警确认及时率 = {1－超出规定确认时长的告警条数 / 告警总条数} × 100%',3,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500104,N'告警整体压缩率','/content/themes/icons/leaf.png','/KPI/Base',N'告警整体压缩率 = {(主次关联中的次告警数 ＋ 衍生关联中的原始告警数 - 衍生关联中的衍生告警数) / 1~3级告警原始入库总数(含衍生告警)} × 100%',4,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500105,N'监控可用度','/content/themes/icons/leaf.png','/KPI/Base',N'监控可用度 = {1 - 采集设备中断告警时长 / (采集设备数量 × 统计时长)} × 100%',5,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500106,N'市电可用度','/content/themes/icons/leaf.png','/KPI/Base',N'市电可用度 = {1 - 市电停电告警时长 / (市电路数 × 统计时长)} × 100%',6,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500107,N'温控系统可用度','/content/themes/icons/leaf.png','/KPI/Base',N'温控系统可用度 = {1 - 高温告警时长 / (温度测点总数 × 统计时长)} × 100%',7,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500108,N'直流系统可用度','/content/themes/icons/leaf.png','/KPI/Base',N'直流系统可用度 = {1 - 开关电源蓄电池组总电压低告警时长 / (开关电源蓄电池组数 × 统计时长)} × 100%',8,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500109,N'交流不间断系统可用度','/content/themes/icons/leaf.png','/KPI/Base',N'交流不间断系统可用度 = {1 - (UPS蓄电池组总电压低告警时长 + UPS旁路运行时长) / (UPS蓄电池组数 × 统计时长)} × 100%',9,5001,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500201,N'能耗综合统计','/content/themes/icons/leaf.png','/KPI/Performance',N'系统能耗指标',1,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500202,N'能耗分类占比','/content/themes/icons/leaf.png','/KPI/Performance',N'系统能耗占比',2,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500203,N'系统PUE指标','/content/themes/icons/leaf.png','/KPI/Performance',N'系统PUE指标',3,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500204,N'直流系统负载率','/content/themes/icons/leaf.png','/KPI/Performance',N'直流系统负载率',4,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500205,N'交流不间断系统负载率','/content/themes/icons/leaf.png','/KPI/Performance',N'交流不间断系统负载率',5,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500206,N'高低压配电系统负载率','/content/themes/icons/leaf.png','/KPI/Performance',N'高低压配电系统负载率',6,5002,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500301,N'监控覆盖率','/content/themes/icons/leaf.png','/KPI/Custom',N'监控覆盖率',1,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500302,N'站点标识率','/content/themes/icons/leaf.png','/KPI/Custom',N'站点标识率',2,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500303,N'温控容量合格率','/content/themes/icons/leaf.png','/KPI/Custom',N'温控容量合格率',3,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500304,N'关键监控测点接入率','/content/themes/icons/leaf.png','/KPI/Custom',N'关键监控测点接入率',4,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500305,N'开关电源带载合格率','/content/themes/icons/leaf.png','/KPI/Custom',N'开关电源带载合格率',5,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500306,N'蓄电池后备时长合格率','/content/themes/icons/leaf.png','/KPI/Custom',N'蓄电池后备时长合格率',6,5003,1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500307,N'蓄电池核对性放电及时率','/content/themes/icons/leaf.png','/KPI/Custom',N'蓄电池核对性放电及时率',7,5003,1);
GO