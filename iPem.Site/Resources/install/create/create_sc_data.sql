/*
* P2S_V1 Data Script Library v1.3.5
* Copyright 2018, Delta
* Author: GJ
* Date: 2018/06/05
*/

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[M_Dictionary]
DELETE FROM [dbo].[M_Dictionary];
GO

INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(1,N'数据管理',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(2,N'语音播报',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(3,N'能耗分类',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(4,N'报表参数',NULL,NULL,GETDATE());
INSERT INTO [dbo].[M_Dictionary]([Id],[Name],[ValuesJson],[ValuesBinary],[LastUpdatedDate]) VALUES(5,N'软件注册',NULL,NULL,GETDATE());
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Menus]
DELETE FROM [dbo].[U_Menus];
GO

INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1, N'系统管理', '/content/themes/icons/menu-xtgl.png', NULL, N'系统管理', 1, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2, N'配置管理', '/content/themes/icons/menu-pzgl.png', NULL, N'配置管理', 2, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3, N'虚拟界面', '/content/themes/icons/menu-xnjm.png', NULL, N'虚拟界面', 3, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4, N'系统报表', '/content/themes/icons/menu-report.png', NULL, N'系统报表', 4, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5, N'系统指标', '/content/themes/icons/menu-kpi.png', NULL, N'系统指标', 5, 0, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1001, N'角色管理', '/content/themes/icons/menu-jsgl.png', '/Account/Roles', N'角色是指系统相关权限的集合组，使用角色可以简化权限管理，实现用户权限的统一分配管理。角色管理主要实现了角色信息的新增、编辑、删除及角色权限分配等功能。', 1, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1002, N'用户管理', '/content/themes/icons/menu-yhgl.png', '/Account/Users', N'用户是指登录系统所需的账号密码信息，用户必须隶属于某个角色并继承该角色所拥有的系统权限。用户管理主要实现了用户信息的新增、编辑、删除及密码重置等功能。', 2, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1003, N'日志管理', '/content/themes/icons/menu-rzgl.png', '/Account/Events', N'日志管理是对系统中记录的系统异常、操作事件等日志信息进行管理维护。用户可以查询、导出系统日志记录，定期删除过期的日志记录等操作。', 3, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1004, N'消息管理', '/content/themes/icons/menu-xxgl.png', '/Account/Notice', N'消息管理是对站内的公开消息、公告、通知、提示等信息进行发布管理。用户可以查询、发布、更新、删除站内的广播消息。', 4, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(1005, N'参数管理', '/content/themes/icons/menu-csgl.png', '/Account/Dictionary', N'参数管理是对系统运行所需的各类参数进行管理维护，用户可根据系统实际的业务逻辑需求配置适合的参数。', 5, 1, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2001, N'FSU信息管理', '/content/themes/icons/menu-fsu-xx.png', '/Fsu/Index', N'FSU信息查询是指对系统内符合查询条件的FSU进行查询展现，提供FSU的编码、IP、端口、是否离线、离线时间等相关信息。', 1, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2002, N'FSU配置管理', '/content/themes/icons/menu-fsu-pz.png', '/Fsu/Configuration', N'FSU在线管理是指对系统内符合查询条件的FSU进行在线配置管理，提供修改单个或批量修改多个FSU相关配置的能力。', 2, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2003, N'FSU日志管理', '/content/themes/icons/menu-fsu-rz.png', '/Fsu/Event', N'FSU日志是指SC对FSU中的FTP文件下载、解析、操作的日志记录；FSU日志管理提供对FTP日志记录的筛选、查询、导出等功能。', 3, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2004, N'动环参数巡检', '/content/themes/icons/menu-csxj.png', '/Fsu/ParamDiff', N'动环参数巡检是指系统可自动对动环参数进行巡检，包括历史数据存储周期、告警信号的门限值、信号是否被屏蔽等，生成巡检结果，提示不合理的参数设置，以提醒维护人员核对和修正。<br/>巡检结果格式：当前值&标准值', 4, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2005, N'告警维护管理', '/content/themes/icons/menu-gjgl.png', '/Maintenance/Index', N'告警维护管理是指对系统中产生的异常告警进行查询统计、数据导出、维护管理，用户可以根据需要对异常告警进行手动结束、手动删除等操作。', 6, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2006, N'工程信息管理', '/content/themes/icons/menu-gcgl.png', '/Project/Index', N'工程信息管理是对系统所涉及的工程项目进行维护管理，以完善后续的报表统计功能。注：因工程预约需要引用工程信息，为了保证工程预约信息的完整性，暂不提供删除工程功能。', 8, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2007, N'工程预约管理', '/content/themes/icons/menu-gcyy.png', '/Project/Reservation', N'工程预约管理是对施工过程中所影响到的区域、站点、机房、设备等进行提前预约，系统会根据预约信息将其产生的告警标识为工程告警，为后续报表统计提供数据支持。注：因告警信息需要引用工程预约信息，为了保证告警信息的完整性，暂不提供删除预约功能。', 9, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2008, N'告警屏蔽管理', '/content/themes/icons/menu-gjpb.png', '/Maintenance/Masking', N'告警屏蔽管理是对系统中已设置的告警屏蔽规则进行查询统计、数据导出等操作。', 7, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2009, N'信号参数管理', '/content/themes/icons/menu-cscy.png', '/Maintenance/PointParam', N'信号参数管理是对系统中所有的信号参数按不同的参数类型进行查询统计、数据导出等操作，将实际参数与标准参数进行对比分析，以不同颜色显示出存在差异的参数，为用户核查巡检信号参数提供帮助。', 5, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2010, N'能耗公式管理', '/content/themes/icons/menu-nhgl.png', '/Maintenance/EneFormula', N'能耗公式管理是对系统站点、机房、设备的各项能耗指标的运算公式进行配置管理。<br/>能耗公式说明:<br/>1. 公式中以“@机房名称>>设备名称>>信号名称”的形式映射一个具体设备的信号点。<br/>2. 机房、设备、信号名称和编号中禁止出现“@”、“>>”、“+”、“-”、“*”、“/”、“(”、“)”等符号。<br/>公式示例: ((@机房1>>设备1>>信号1 + @机房2>>设备2>>信号2 - @机房3>>设备3>>信号3) * @机房4>>设备4>>信号4) / @机房5>>设备5>>信号5', 10, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(2011, N'虚拟信号管理', '/content/themes/icons/menu-xnxh.png', '/Maintenance/VirtualPoint', N'虚拟信号管理是对系统设备所有的虚拟信号进行查询统计、配置管理。用户可以根据需要新增、编辑、删除某个设备的虚拟信号，通过虚拟信号，可以实现不同信号之间的逻辑运算。<br/>虚拟信号公式说明:<br/>1. 公式中以“@机房名称>>设备名称>>信号名称”的形式映射一个具体设备的信号点。<br/>2. 机房、设备、信号名称和编号中禁止出现“@”、“>>”、“+”、“-”、“*”、“/”、“(”、“)”等符号。<br/>公式示例: ((@机房1>>设备1>>信号1 + @机房2>>设备2>>信号2 - @机房3>>设备3>>信号3) * @机房4>>设备4>>信号4) / @机房5>>设备5>>信号5', 11, 2, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3001, N'图形组态', '/content/themes/icons/menu-txzt.png', '/Configuration/Index', N'图形组态提供了更加灵活多样化、更加直观的图形方式展示系统中设备的物理拓扑图以及设备信号的实时测值。', 1, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(3002, N'电子地图', '/content/themes/icons/menu-dzdt.png', '/Configuration/Map', N'电子地图提供了系统中各站点的地理位置、路线指导、站点概况、实时告警等信息的概览功能。<br/>注：在使用电子地图之前，请为站点设置经纬度信息。', 2, 3, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4001, N'基础报表', '/content/themes/icons/menu-report-jc.png', NULL, N'基础报表', 1, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4002, N'历史报表', '/content/themes/icons/menu-report-ls.png', NULL, N'历史报表', 2, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4003, N'图形报表', '/content/themes/icons/menu-report-qx.png', NULL, N'图形报表', 3, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(4004, N'订制报表', '/content/themes/icons/menu-report-dz.png', NULL, N'订制报表', 4, 4, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5001, N'健康度指标(核心站点)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'健康度指标(核心站点)', 1, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5002, N'健康度指标(其他站点)', '/content/themes/icons/menu-kpi-jkd.png', NULL, N'健康度指标(其他站点)', 2, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5003, N'能耗指标', '/content/themes/icons/menu-kpi-nh.png', NULL, N'能耗指标', 3, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(5004, N'订制指标', '/content/themes/icons/menu-kpi-dz.png', NULL, N'订制指标', 4, 5, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400101, N'区域统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'区域统计是指对系统所有区域按照区域类型进行分类统计、对比分析。', 1, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400102, N'站点统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'站点统计是指对系统所有站点按照站点类型进行分类统计、对比分析。', 2, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400103, N'机房统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'机房统计是指对系统所有机房按照机房类型进行分类统计、对比分析。', 3, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400104, N'设备统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'设备统计是指对系统所有设备按照设备类型进行分类统计、对比分析。', 4, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400105, N'员工统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'员工统计是指对系统内所有正式员工的基本信息及门禁相关信息（持卡情况、授权设备等）进行统计、分析。', 5, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400106, N'外协统计', '/content/themes/icons/menu-report-jc.png', '/Report/Base', N'员工统计是指对系统内所有外协人员的基本信息及门禁相关信息（持卡情况、授权设备等）进行统计、分析。', 6, 4001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400201, N'历史测值查詢', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'历史测值查詢提供了系统所有设备信号点产生的历史数据的查询统计、数据导出等功能。', 1, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400202, N'历史告警查詢', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'历史告警查詢提供了系统所有设备信号点产生的历史告警的查询统计、对比分析、数据导出等功能。', 2, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400203, N'告警分类统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'告警分类统计提供了系统所有设备信号点产生的历史告警的分类统计、对比分析、数据导出等功能。', 3, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400204, N'设备告警统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'设备告警统计提供了系统所有设备信号点产生的历史告警的分类统计、对比分析、数据导出等功能。', 4, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400205, N'工程项目统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'工程项目统计提供了系统中所有工程项目的查询统计、数据导出等功能。', 5, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400206, N'工程预约统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'工程预约统计提供了系统中所有工程预约的查询统计、数据导出等功能。', 6, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400207, N'市电停电统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'市电停电统计提供了系统中所有市电停电站点的查询统计、数据导出等功能。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>市电停电"里的参数信息。', 7, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400208, N'油机发电统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'油机发电统计提供了系统中所有油机发电站点的查询统计、数据导出等功能。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>油机发电统计"里的参数信息。', 8, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400209, N'刷卡记录统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'刷卡记录统计提供了系统中所有正式员工、外协人员的刷卡记录的查询统计、数据导出等功能。', 10, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400210, N'刷卡次数统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'刷卡记录统计提供了系统中所有正式员工、外协人员的刷卡次数的查询统计、数据导出等功能。', 11, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400211, N'电池放电统计', '/content/themes/icons/menu-report-ls.png', '/Report/History', N'电池放电统计提供了系统中所有蓄电池的放电次数、放电过程、放电曲线的查询统计、数据导出等功能。', 9, 4002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400301, N'信号测值曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'信号测值曲线是指以曲线图形的方式展示系统中设备信号产生的历史数据。', 1, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400302, N'信号统计曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'信号统计曲线是指以曲线图形的方式展示系统中设备信号产生的统计数据。', 2, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400303, N'电池放电曲线', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'电池放电曲线是指以曲线图形的方式展示系统中电池放电产生的历史数据。', 3, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400304, N'告警分类图表', '/content/themes/icons/menu-report-qx.png', '/Report/Chart', N'告警分类图表是指以柱状图形、饼状图形的方式根据不同维度分类展示系统中的历史告警信息。', 4, 4003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400401, N'超频告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超频告警是指对在一定时间范围内，告警次数超过规定次数的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 1, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400402, N'超短告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超短告警是指对在一定时间范围内，告警历时小于规定时长的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 2, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400403, N'超长告警', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'超长告警是指对在一定时间范围内，告警历时大于规定时长的告警进行查询统计、对比分析。<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>异常告警"里的参数信息。', 3, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400404, N'列头柜功率', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'列头柜功率是指对列头柜各分路每小时的功率信息进行查询统计、对比分析、数据导出等操作。<br/>注：在使用之前，请确保已在"配置管理>虚拟信号管理"里添加了列头柜设备的分路功率虚拟信号。', 4, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(400405, N'列头柜电量', '/content/themes/icons/menu-report-dz.png', '/Report/Custom', N'列头柜电量是指对列头柜各分路每月的用电量信息进行查询统计、对比分析、数据导出等操作。<br/>注：在使用之前，请确保已在"配置管理>虚拟信号管理"里添加了列头柜设备的分路电量虚拟信号。', 5, 4004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500101, N'直流系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'直流系统可用度 = {1 - 开关电源蓄电池组总电压低告警时长 / (开关电源蓄电池组数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>直流系统可用度(核心站点)"里的参数信息。', 1, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500102, N'交流不间断系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'交流不间断系统可用度 = {1 - (UPS蓄电池组总电压低告警时长 + UPS旁路运行时长) / (UPS蓄电池组数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>交流不间断系统可用度(核心站点)"里的参数信息。', 2, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500103, N'温控系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'温控系统可用度 = {1 - 高温告警时长 / (温度测点总数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>温控系统可用度(核心站点)"里的参数信息。', 3, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500104, N'监控可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控可用度 = {1 - 采集设备中断告警时长 / (采集设备数量 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>监控可用度(核心站点)"里的参数信息。', 4, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500105, N'市电可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'市电可用度 = {1 - 市电停电告警时长 / (市电路数 × 统计时长)} × 100%', 5, 5001, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500201, N'监控覆盖率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控覆盖率 = (本月监控站点总数 / 上月监控站点总数) × 100%', 1, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500202, N'关键监控测点接入率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'关键监控测点接入率 = (本月监控设备数量 / 上月电源设备数量) × 100%<br/>注：<br/>1.监控设备指包含"开关电源总电压"、"蓄电池组总电压"信号的设备。<br/>2.电源设备指设备类型为"开关电源"、"蓄电池组"的设备。<br/>3.在使用之前，请确保已设置了"系统管理>系统参数>报表参数>关键监控测点接入率(其他站点)"里的参数信息。', 2, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500203, N'站点标识率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'站点标识率 = (本月标示站点总数 / 上月站点总数) × 100%', 3, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500204, N'开关电源带载合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'开关电源带载合格率 = 开关电源带载率合格套数 / 开关电源总套数 × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>开关电源带载合格率(其他站点)"里的参数信息。', 4, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500205, N'蓄电池后备时长合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'蓄电池后备时长合格率 = 已完成放电基站蓄电池后备时长合格基站数量 / 已完成放电基站数量 × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>蓄电池后备时长合格率(其他站点)"里的参数信息。', 5, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500206, N'温控容量合格率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'温控容量合格率 = (1 - 高温告警站点总数 / 包含温度测点的站点总数) × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>温控容量合格率(其他站点)"里的参数信息。', 6, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500207, N'直流系统可用度', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'直流系统可用度 = {1 - 开关电源一次下电告警总时长 / (开关电源套数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>直流系统可用度(其他站点)"里的参数信息。', 7, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500208, N'监控故障处理及时率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'监控故障处理及时率 = {1 - 站点通信中断告警总时长 / (系统站点总数 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>监控故障处理及时率(其他站点)"里的参数信息。', 8, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500209, N'蓄电池核对性放电及时率', '/content/themes/icons/menu-kpi-jkd.png', '/KPI/Base', N'蓄电池核对性放电及时率 = 蓄电池1小时以上放电站点总数 / 系统站点总数 × 100%', 9, 5002, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500301, N'能耗分类统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'能耗分类统计是指对站点、机房的能耗信息按照能耗分类进行查询统计、对比分析、数据导出等操作。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 1, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500302, N'站点能耗统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'站点能耗统计是指对站点的能耗信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 2, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500303, N'机房能耗统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'机房能耗统计是指对机房的能耗信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 3, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500304, N'站点PUE统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'站点PUE统计是指对站点的PUE信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 4, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500305, N'机房PUE统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'机房PUE统计是指对机房的PUE信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 5, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500306, N'变压器能耗统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'变压器能耗统计是指对变压器设备的能耗信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 6, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500307, N'变压器损耗统计', '/content/themes/icons/menu-kpi-nh.png', '/KPI/Performance', N'变压器损耗统计是指对变压器设备的线损信息按照时、日、月进行查询统计、对比分析，并生成日报表、月报表、年报表。<br/>注：在使用之前，请确保已设置了"配置管理>能耗公式管理"里的能耗公式参数。', 7, 5003, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500401, N'系统设备完好率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'系统设备完好率 = {1－设备告警总时长 / (设备数量 × 统计时长)} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>系统设备完好率"里的参数信息。', 1, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500402, N'故障处理及时率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'故障处理及时率 = {1－超出规定处理时长的设备故障次数 / 设备故障总次数} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>故障处理及时率"里的参数信息。', 2, 5004, 1);
INSERT INTO [dbo].[U_Menus]([Id],[Name],[Icon],[Url],[Comment],[Index],[LastId],[Enabled]) VALUES(500403, N'告警确认及时率', '/content/themes/icons/menu-kpi-dz.png', '/KPI/Custom', N'告警确认及时率 = {1－超出规定确认时长的告警条数 / 告警总条数} × 100%<br/>注：在使用之前，请确保已设置了"系统管理>系统参数>报表参数>告警确认及时率"里的参数信息。', 3, 5004, 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Roles]
DELETE FROM [dbo].[U_Roles];
GO

INSERT INTO [dbo].[U_Roles]([Id],[Name],[Comment],[Enabled],[Type],[Config],[ValuesJson]) VALUES('a0000000-6000-2000-1000-f00000000000','Administrator','超级管理员',1,0,0,NULL);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_Users]
DELETE FROM [dbo].[U_Users];
GO

INSERT INTO [dbo].[U_Users]([Id],[Uid],[Password],[PasswordFormat],[PasswordSalt],[CreatedDate],[LimitedDate],[LastLoginDate],[LastPasswordChangedDate],[FailedPasswordAttemptCount],[FailedPasswordDate],[IsLockedOut],[LastLockoutDate],[Comment],[EmployeeId],[Enabled]) VALUES('62ab161f-dcbb-633b-b6a0-a9ebf6099862', 'system', 'ynMbt/ns3PKIJvDa/a6UiwDwxrE=', 1, '4RltREDrDBzwvkPj0j5hLg==', GETDATE(), '2099-12-31', GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE(), '默认用户', '00001', 1);
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[U_UsersInRoles]
DELETE FROM [dbo].[U_UsersInRoles];
GO

INSERT INTO [dbo].[U_UsersInRoles]([RoleId],[UserId]) VALUES('a0000000-6000-2000-1000-f00000000000', '62ab161f-dcbb-633b-b6a0-a9ebf6099862');
GO

--■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
--添加默认值[dbo].[H_DBScript]
DELETE FROM [dbo].[H_DBScript];
GO

INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_001','新增「放电次数统计」报表','Steven',GETDATE(),'系统批量',GETDATE(),'新增「放电次数统计」报表');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_002','新增组态功能、角色权限功能','Steven',GETDATE(),'系统批量',GETDATE(),'新增组态功能相关数据表、站点角色权限表、机房角色权限表');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_003','新增菜单列表','Steven',GETDATE(),'系统批量',GETDATE(),'新增信号参数管理菜单、告警维护管理菜单、告警屏蔽管理菜单');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_004','更新参数管理菜单','Steven',GETDATE(),'系统批量',GETDATE(),'更新参数管理菜单帮助说明信息');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_005','优化缓存，新增工程预约审核','Steven',GETDATE(),'系统批量',GETDATE(),'优化缓存，新增工程预约审核');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_006','新增软件注册功能','Steven',GETDATE(),'系统批量',GETDATE(),'优化缓存，新增工程预约审核');
INSERT INTO [dbo].[H_DBScript]([Id],[Name],[CreateUser],[CreateTime],[ExecuteUser],[ExecuteTime],[Desc]) VALUES('P2S_V1_007','角色权限细化到设备,增加短信/语音告警功能','Scorpio',GETDATE(),'系统批量',GETDATE(),'角色权限细化到设备,增加短信/语音告警功能');
GO