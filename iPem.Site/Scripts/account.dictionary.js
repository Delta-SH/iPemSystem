//#region Global

var currentFormulaNode = null;
Ext.apply(Ext.form.field.VTypes, {
    IPv4: function (v) {
        return /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/.test(v);
    },
    IPv4Text: 'IPv4地址格式错误',
    IPv4Mask: /[\d\.]/i
});

//#endregion

//#region Model

Ext.define('ScriptModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'creator', type: 'string' },
        { name: 'createdtime', type: 'string' },
		{ name: 'executor', type: 'string' },
		{ name: 'executedtime', type: 'string' },
		{ name: 'comment', type: 'string' }
    ],
    idProperty: 'id'
});

//#endregion

//#region Store

var rsStore = Ext.create('Ext.data.Store', {
    autoLoad: true,
    pageSize: 20,
    model: 'ScriptModel',
    proxy: {
        type: 'ajax',
        url: '/Account/GetRsScripts',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});
var scStore = Ext.create('Ext.data.Store', {
    autoLoad: true,
    pageSize: 20,
    model: 'ScriptModel',
    proxy: {
        type: 'ajax',
        url: '/Account/GetScScripts',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});
var csStore = Ext.create('Ext.data.Store', {
    autoLoad: true,
    pageSize: 20,
    model: 'ScriptModel',
    proxy: {
        type: 'ajax',
        url: '/Account/GetCsScripts',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});

var rsPagingToolbar = $$iPems.clonePagingToolbar(rsStore);
var scPagingToolbar = $$iPems.clonePagingToolbar(scStore);
var csPagingToolbar = $$iPems.clonePagingToolbar(csStore);

//#endregion

//#region Window

var scriptWin = Ext.create('Ext.window.Window', {
    title: '脚本升级',
    glyph: 0xf060,
    height: 180,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    store: null,
    url: null,
    items: [{
        itemId: 'scriptForm',
        xtype: 'form',
        border: false,
        defaultType: 'textfield',
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            margin: '15 15 15 15',
            anchor: '100%'
        },
        items: [
			{
				id: 'scriptFile',
				xtype: 'filefield',
				allowBlank: false,
				emptyText: '请选择升级脚本文件...',
				fieldLabel: '升级脚本',
				buttonText: '浏览...',
				validator: function (v) {
				    if (!/\.sql/.test(v)) {
				        return '仅支持*.sql脚本文件';
				    }
				    return true;
				},
				listeners: {
				    change: function (me, value) {
				        var newValue = value.replace(/C:\\fakepath\\/g, '');
				        me.setRawValue(newValue);
				    }
				}
			},
			{
				id: 'password',
				name: 'password',
				xtype: 'textfield',
				inputType: 'password',
				fieldLabel: '鉴权密码',
				allowBlank: false
			}
        ]
    }],
    buttonAlign: 'right',
    buttons: [
		{ id: 'scriptResult', xtype: 'iconlabel', text: '' },
		{ xtype: 'tbfill' },
		{
			xtype: 'button',
			text: '升级',
			handler: function (el, e) {
			    var form = scriptWin.getComponent('scriptForm'),
					baseForm = form.getForm(),
					scriptResult = Ext.getCmp('scriptResult');

			    scriptResult.setTextWithIcon('', '');
			    if (baseForm.isValid()) {
			        Ext.Msg.confirm('确认对话框', '您确认要升级吗？', function (buttonId, text) {
			            if (buttonId === 'yes') {
			                scriptResult.setTextWithIcon('正在升级...', 'x-icon-loading');
			                baseForm.submit({
			                    clientValidation: true,
			                    submitEmptyText: false,
			                    preventWindow: true,
			                    url: scriptWin.url,
			                    success: function (form, action) {
			                        scriptResult.setTextWithIcon(action.result.message, 'x-icon-accept');
			                        scriptWin.store.loadPage(1);
			                    },
			                    failure: function (form, action) {
			                        var message = 'undefined error.';
			                        if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
			                            message = action.result.message;

			                        scriptResult.setTextWithIcon(message, 'x-icon-error');
			                    }
			                });
			            }
			        });
			    }
			}
		}, {
			xtype: 'button',
			text: '关闭',
			handler: function (el, e) {
			    scriptWin.hide();
			}
		}
    ]
});

//#endregion

//#region Tabs

var wsConfigTab = Ext.create('Ext.form.Panel', {
    glyph: 0xf047,
    title: '数据管理',
    overflowY: 'auto',
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items: [
        {
            xtype: 'fieldset',
            title: '配置管理',
            margin: '20 20 10 20',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'button',
                    text: '清空所有缓存',
                    cls: 'custom-button custom-danger',
                    margin: 15,
                    handler: function () {
                        Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                            if (buttonId === 'yes') {
                                var cacheResult = Ext.getCmp('cacheResult');
                                cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                Ext.Ajax.request({
                                    url: '/Account/ClearCache',
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                    }
                                });
                            }
                        });
                    }
                },
                {
                    xtype: 'button',
                    text: '清空全局缓存',
                    cls: 'custom-button custom-warning',
                    margin: 15,
                    handler: function () {
                        Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                            if (buttonId === 'yes') {
                                var cacheResult = Ext.getCmp('cacheResult');
                                cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                Ext.Ajax.request({
                                    url: '/Account/ClearGlobalCache',
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                    }
                                });
                            }
                        });
                    }
                },
                {
                    xtype: 'button',
                    text: '清空用户缓存',
                    cls: 'custom-button custom-info',
                    margin: 15,
                    handler: function () {
                        Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                            if (buttonId === 'yes') {
                                var cacheResult = Ext.getCmp('cacheResult');
                                cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                Ext.Ajax.request({
                                    url: '/Account/ClearUserCache',
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                    }
                                });
                            }
                        });
                    }
                },
                {
                    xtype: 'button',
                    text: '更新FSU关联',
                    cls: 'custom-button custom-success',
                    margin: 15,
                    hidden: true,
                    handler: function () {
                        Ext.Msg.confirm('确认对话框', '您确认要更新FSU关联的机房吗？', function (buttonId, text) {
                            if (buttonId === 'yes') {
                                var cacheResult = Ext.getCmp('cacheResult');
                                cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                Ext.Ajax.request({
                                    url: '/Account/UpdateFsuRelation',
                                    method: 'POST',
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                    }
                                });
                            }
                        });
                    }
                },
                {
                    id: 'cacheResult',
                    xtype: 'iconlabel',
                    margin: '21 0 21 0',
                    text: '',
                    flex: 1
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '接口管理',
            margin: '10 20 10 20',
            defaultType: 'textfield',
            layout: 'anchor',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 120,
                labelAlign: 'left',
                margin: 25
            },
            items: [
                {
                    itemId: 'ip',
                    name: 'ip',
                    xtype: 'textfield',
                    fieldLabel: 'FSU 通信地址',
                    emptyText: '示例： 192.168.10.100',
                    vtype: 'IPv4',
                    allowBlank: false
                },
                {
                    itemId: 'port',
                    name: 'port',
                    xtype: 'numberfield',
                    fieldLabel: 'FSU 通信端口',
                    value: 8080,
                    minValue: 1,
                    maxValue: 65535,
                    allowBlank: false
                },
                {
                    itemId: 'fsuPath',
                    name: 'fsuPath',
                    xtype: 'textfield',
                    fieldLabel: 'FSU 虚拟路径',
                    emptyText: '示例： /Services/',
                    allowBlank: true
                },
                {
                    itemId: 'uid',
                    name: 'uid',
                    xtype: 'textfield',
                    fieldLabel: 'FSU 登录帐号',
                    allowBlank: false
                },
                {
                    itemId: 'password',
                    name: 'password',
                    xtype: 'textfield',
                    fieldLabel: 'FSU 登录密码',
                    allowBlank: false
                }
            ]
        }
    ],
    buttonAlign: 'left',
    buttons: [
        {
            xtype: 'button',
            text: '保存当前页',
            cls: 'custom-button custom-success',
            handler: function () {
                var wsbasic = wsConfigTab.getForm(),
                    wsresult = Ext.getCmp('wsresult');

                wsresult.setTextWithIcon('', '');
                if (wsbasic.isValid()) {
                    wsresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                    wsbasic.submit({
                        submitEmptyText: false,
                        clientValidation: true,
                        preventWindow: true,
                        url: '/Account/SaveWs',
                        success: function (form, action) {
                            wsresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                        },
                        failure: function (form, action) {
                            var message = 'undefined error.';
                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                message = action.result.message;

                            wsresult.setTextWithIcon(message, 'x-icon-error');
                        }
                    });
                } else {
                    wsresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                }
            }
        },
        { id: 'wsresult', xtype: 'iconlabel', text: '' }
    ]
});

var tsConfigTab = Ext.create('Ext.form.Panel', {
    glyph: 0xf048,
    title: '语音播报',
    overflowY: 'auto',
    items: [
        {
            xtype: 'container',
            layout: 'hbox',
            margin: '20 20 10 20',
            items: [
                {
                    xtype: 'fieldset',
                    title: '语音播报',
                    layout: 'anchor',
                    flex: 1,
                    defaults: {
                        anchor: '100%',
                        margin: 15
                    },
                    items: [{
                        xtype: 'checkboxgroup',
                        columns: 2,
                        items: [
                            { boxLabel: '启用语音播报', name: 'bases', inputValue: 1 },
                            { boxLabel: '循环播报告警', name: 'bases', inputValue: 2 },
                            { boxLabel: '禁播工程告警', name: 'bases', inputValue: 3 },
                            { boxLabel: '禁播确认告警', name: 'bases', inputValue: 4 }
                        ]
                    }]
                }, {
                    xtype: 'component',
                    width: 20
                }, {
                    xtype: 'fieldset',
                    title: '播报级别',
                    layout: 'anchor',
                    flex: 1,
                    defaults: {
                        anchor: '100%',
                        margin: 15
                    },
                    items: [{
                        xtype: 'checkboxgroup',
                        columns: 2,
                        items: [
                            { boxLabel: '一级告警', name: 'levels', inputValue: $$iPems.Level.Level1 },
                            { boxLabel: '二级告警', name: 'levels', inputValue: $$iPems.Level.Level2 },
                            { boxLabel: '三级告警', name: 'levels', inputValue: $$iPems.Level.Level3 },
                            { boxLabel: '四级告警', name: 'levels', inputValue: $$iPems.Level.Level4 }
                        ]
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '播报内容',
            margin: '10 20 20 20',
            layout: 'anchor',
            defaults: {
                anchor: '100%',
                margin: 15
            },
            items: [{
                xtype: 'checkboxgroup',
                columns: 4,
                items: [
                    { boxLabel: '所属区域', name: 'contents', inputValue: 1 },
                    { boxLabel: '所属站点', name: 'contents', inputValue: 2 },
                    { boxLabel: '所属机房', name: 'contents', inputValue: 3 },
                    { boxLabel: '所属设备', name: 'contents', inputValue: 4 },
                    { boxLabel: '所属信号', name: 'contents', inputValue: 5 },
                    { boxLabel: '告警时间', name: 'contents', inputValue: 6 },
                    { boxLabel: '告警等级', name: 'contents', inputValue: 7 },
                    { boxLabel: '告警描述', name: 'contents', inputValue: 8 }
                ]
            }]
        },
        {
            xtype: 'fieldset',
            title: '播报条件',
            margin: 20,
            fieldDefaults: {
                anchor: '100%',
                margin: 15,
                labelWidth: 60,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    layout: 'hbox',
                    items: [
                        {
                            xtype: 'container',
                            layout: 'anchor',
                            flex: 1,
                            items: [
                                {
                                    name: 'stationTypes',
                                    xtype: 'StationTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    name: 'subDeviceTypes',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    matchFieldWidth: true,
                                    emptyText: '默认全部'
                                },
                                {
                                    name: 'points',
                                    xtype: 'PointMultiPicker',
                                    matchFieldWidth: true,
                                    emptyText: '默认全部'
                                }
                            ]
                        },
                        {
                            xtype: 'container',
                            layout: 'anchor',
                            flex: 1,
                            items: [
                                {
                                    name: 'roomTypes',
                                    xtype: 'RoomTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    name: 'subLogicTypes',
                                    xtype: 'SubLogicTypeMultiPicker',
                                    matchFieldWidth: true,
                                    emptyText: '默认全部'
                                },
                                {
                                    name: 'keywords',
                                    xtype: 'textfield',
                                    fieldLabel: '关键字',
                                    emptyText: '多关键字请以;分隔，例: A;B;C'
                                }
                            ]
                        }
                    ]
                }
            ]
        }
    ],
    buttonAlign: 'left',
    buttons: [
        {
            xtype: 'button',
            text: '保存当前页',
            cls: 'custom-button custom-success',
            handler: function () {
                var tsbasic = tsConfigTab.getForm(),
                    tsresult = Ext.getCmp('tsresult');

                tsresult.setTextWithIcon('', '');
                if (tsbasic.isValid()) {
                    tsresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                    tsbasic.submit({
                        submitEmptyText: false,
                        clientValidation: true,
                        preventWindow: true,
                        url: '/Account/SaveTs',
                        success: function (form, action) {
                            tsresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                        },
                        failure: function (form, action) {
                            var message = 'undefined error.';
                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                message = action.result.message;

                            tsresult.setTextWithIcon(message, 'x-icon-error');
                        }
                    });
                } else {
                    tsresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                }
            }
        },
        { id: 'tsresult', xtype: 'iconlabel', text: '' }
    ]
});

var rtConfigTab = Ext.create('Ext.form.Panel', {
    glyph: 0xf044,
    title: '报表参数',
    overflowY: 'auto',
    items: [
        {
            xtype: 'fieldset',
            title: '异常告警',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'chaoDuan',
                        xtype: 'numberfield',
                        fieldLabel: '超短告警',
                        allowBlank: false,
                        value: 1,
                        minValue: 1,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：超短告警是指告警历时小于该阈值的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'chaoChang',
                        xtype: 'numberfield',
                        fieldLabel: '超长告警',
                        allowBlank: false,
                        value: 1,
                        minValue: 1,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：超长告警是指告警历时大于该阈值的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'chaoPin',
                        xtype: 'numberfield',
                        fieldLabel: '超频告警',
                        allowBlank: false,
                        allowDecimals: false,
                        value: 1,
                        minValue: 1,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '次数（注：超频告警是指告警次数大于该阈值的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '系统设备完好率',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'whlHuLue',
                        xtype: 'numberfield',
                        fieldLabel: '忽略告警',
                        allowBlank: false,
                        value: 0,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '故障处理及时率',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'jslGuiDing',
                        xtype: 'numberfield',
                        fieldLabel: '处理时长',
                        allowBlank: false,
                        value: 0,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：报表统计时将计算超过该规定处理时长的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'jslHuLue',
                        xtype: 'numberfield',
                        fieldLabel: '忽略告警',
                        allowBlank: false,
                        value: 0,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '告警确认及时率',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'jslQueRen',
                        xtype: 'numberfield',
                        fieldLabel: '确认时长',
                        allowBlank: false,
                        value: 0,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：报表统计时将计算超过该规定确认时长的告警）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '直流系统可用度(核心站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'hxzlxtkydLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'hxzlxtkydXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"输出电压过低告警"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '交流不间断系统可用度(核心站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'hxjlxtkydLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"UPS设备"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'hxjlxtkydXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"输出电压过低告警"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'hxjlxtkydPangLuXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '旁路信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"旁路运行告警"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '温控系统可用度(核心站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'hxwkxtkydWenDuXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: true,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: false,
                        matchFieldWidth: true,
                        fieldLabel: '温度信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"温度信号"）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'hxwkxtkydGaoWenXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"高温告警信号"）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '监控可用度(核心站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'hxjkkydXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"监控系统采集设备中断"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'hxjkkydLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"动环监控采集设备"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '关键监控测点接入率(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtgjjkcdjrlXinHao',
                        xtype: 'PointMultiPicker',
                        AI: true,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: false,
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '关键信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源总电压、蓄电池组总电压"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtgjjkcdjrlLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源、蓄电池组"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '开关电源带载合格率(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtkgdydzhglLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }, {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtkgdydzhglztXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: true,
                        DO: false,
                        AL: false,
                        matchFieldWidth: true,
                        fieldLabel: '工作状态'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"工作状态（均充、浮充、放电）"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtkgdydzhglfzXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: true,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: false,
                        matchFieldWidth: true,
                        fieldLabel: '负载电流'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"负载电流"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '蓄电池后备时长合格率(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtxdchbschglXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: true,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: false,
                        matchFieldWidth: true,
                        fieldLabel: '电压信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"蓄电池组总电压"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtxdchbschglDianYa',
                        xtype: 'numberfield',
                        fieldLabel: '合格电压',
                        allowBlank: false,
                        value: 47,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: 'V（注：电池放电结束后蓄电池组总电压大于该值则视为放电合格,建议:47V）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtxdchbschglShiJian',
                        xtype: 'numberfield',
                        fieldLabel: '放电时间',
                        allowBlank: false,
                        value: 15,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：当电池放电时间大于该值则视为放电完成，建议:15分钟）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '温控容量合格率(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtwkrlhglWenDuXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: true,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: false,
                        matchFieldWidth: true,
                        fieldLabel: '温度信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"温度信号"）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }, {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtwkrlhglGaoWenXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"高温告警信号"）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '直流系统可用度(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 0 15',
                    items: [{
                        name: 'qtzlxtkydXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源一次下电告警"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtzlxtkydLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"开关电源"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '监控故障处理及时率(其他站点)',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: '15 15 15 15',
                    items: [{
                        name: 'qtjkgzcljslXinHao',
                        xtype: 'PointMultiPicker',
                        flex: 1,
                        AI: false,
                        AO: false,
                        DI: false,
                        DO: false,
                        AL: true,
                        matchFieldWidth: true,
                        fieldLabel: '告警信号'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"站点动环通信中断告警"信号）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '油机发电统计',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'fdjzLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"发电机组、油机"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '变压器能耗统计',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'byqnhLeiXing',
                        xtype: 'SubDeviceTypeMultiPicker',
                        flex: 1,
                        matchFieldWidth: true,
                        fieldLabel: '设备类型'
                    }, {
                        xtype: 'displayfield',
                        value: '（注：设置"变压器"设备类型）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '实时能耗首页',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'indicator01',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        fieldLabel: '第一指标'
                    }, {
                        name: 'indicator02',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        margin: '0 0 0 15',
                        fieldLabel: '第二指标'
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'indicator03',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        fieldLabel: '第三指标'
                    }, {
                        name: 'indicator04',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        margin: '0 0 0 15',
                        fieldLabel: '第四指标'
                    }]
                },
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'indicator05',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        fieldLabel: '第五指标'
                    }, {
                        name: 'indicator',
                        xtype: 'EnergyComboBox',
                        tt: true,
                        flex: 1,
                        margin: '0 0 0 15',
                        fieldLabel: '统计指标'
                    }]
                }
            ]
        },
        {
            xtype: 'fieldset',
            title: '工程预约审核',
            margin: '10 35 20 20',
            fieldDefaults: {
                anchor: '100%',
                labelWidth: 80,
                labelAlign: 'left'
            },
            items: [
                {
                    xtype: 'container',
                    anchor: '100%',
                    layout: 'hbox',
                    margin: 15,
                    items: [{
                        name: 'gcyyshsxShiChang',
                        xtype: 'numberfield',
                        fieldLabel: '生效时长',
                        allowBlank: false,
                        value: 0,
                        minValue: 0,
                        flex: 1
                    }, {
                        xtype: 'displayfield',
                        value: '分钟（注：使工程预约在该规定时长后生效）',
                        margin: '0 0 0 15',
                        flex: 1
                    }]
                }
            ]
        }
    ],
    buttonAlign: 'left',
    buttons: [
        {
            xtype: 'button',
            text: '保存当前页',
            cls: 'custom-button custom-success',
            handler: function () {
                var rtbasic = rtConfigTab.getForm(),
                    rtresult = Ext.getCmp('rtresult');

                rtresult.setTextWithIcon('', '');
                if (rtbasic.isValid()) {
                    rtresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                    rtbasic.submit({
                        submitEmptyText: false,
                        clientValidation: true,
                        preventWindow: true,
                        url: '/Account/SaveRt',
                        success: function (form, action) {
                            rtresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                        },
                        failure: function (form, action) {
                            var message = 'undefined error.';
                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                message = action.result.message;

                            rtresult.setTextWithIcon(message, 'x-icon-error');
                        }
                    });
                } else {
                    rtresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                }
            }
        },
        { id: 'rtresult', xtype: 'iconlabel', text: '' }
    ]
});

var dbConfigTab = Ext.create('Ext.panel.Panel', {
    glyph: 0xf062,
    title: '脚本升级',
    overflowY: 'auto',
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items: [
        {
            xtype: 'grid',
            glyph: 0xf062,
            title: '资源数据库升级记录',
            margin: '20 35 10 20',
            height: 300,
            store: rsStore,
            columnLines: true,
            disableSelection: false,
            loadMask: true,
            forceFit: false,
            listeners: {},
            viewConfig: {
                forceFit: true,
                trackOver: true,
                stripeRows: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                preserveScrollOnRefresh: true
            },
            columns: [{
                text: '脚本编码',
                dataIndex: 'id',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '脚本名称',
                dataIndex: 'name',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '创建人',
                dataIndex: 'creator',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '创建时间',
                dataIndex: 'createdtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '执行人',
                dataIndex: 'executor',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '执行时间',
                dataIndex: 'executedtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '备注',
                dataIndex: 'comment',
                flex: 1,
                align: 'left',
                sortable: false,
                renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
                    metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
                    return value;
                }
            }],
            dockedItems: [{
                xtype: 'toolbar',
                dock: 'top',
                items: [{
                    xtype: 'button',
                    text: '升级脚本',
                    glyph: 0xf060,
                    handler: function (el, e) {
                        scriptWin.setTitle('资源数据库脚本升级');
                        scriptWin.store = rsStore;
                        scriptWin.url = '/Account/UpdateRsScript';
                        scriptWin.getComponent('scriptForm').getForm().reset();
                        Ext.getCmp('scriptResult').setTextWithIcon('', '');
                        scriptWin.show();
                    }
                }]
            }],
            bbar: rsPagingToolbar
        },
        {
            xtype: 'grid',
            glyph: 0xf062,
            title: '应用数据库升级记录',
            margin: '10 35 10 20',
            height: 300,
            store: scStore,
            columnLines: true,
            disableSelection: false,
            loadMask: true,
            forceFit: false,
            listeners: {},
            viewConfig: {
                forceFit: true,
                trackOver: true,
                stripeRows: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                preserveScrollOnRefresh: true
            },
            columns: [{
                text: '脚本编码',
                dataIndex: 'id',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '脚本名称',
                dataIndex: 'name',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '创建人',
                dataIndex: 'creator',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '创建时间',
                dataIndex: 'createdtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '执行人',
                dataIndex: 'executor',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '执行时间',
                dataIndex: 'executedtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '备注',
                dataIndex: 'comment',
                flex: 1,
                align: 'left',
                sortable: false,
                renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
                    metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
                    return value;
                }
            }],
            dockedItems: [{
                xtype: 'toolbar',
                dock: 'top',
                items: [{
                    xtype: 'button',
                    text: '升级脚本',
                    glyph: 0xf060,
                    handler: function (el, e) {
                        scriptWin.setTitle('应用数据库脚本升级');
                        scriptWin.store = scStore;
                        scriptWin.url = '/Account/UpdateScScript';
                        scriptWin.getComponent('scriptForm').getForm().reset();
                        Ext.getCmp('scriptResult').setTextWithIcon('', '');
                        scriptWin.show();
                    }
                }]
            }],
            bbar: scPagingToolbar
        },
        {
            xtype: 'grid',
            glyph: 0xf062,
            title: '历史数据库升级记录',
            margin: '10 35 20 20',
            height: 300,
            store: csStore,
            columnLines: true,
            disableSelection: false,
            loadMask: true,
            forceFit: false,
            listeners: {},
            viewConfig: {
                forceFit: true,
                trackOver: true,
                stripeRows: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                preserveScrollOnRefresh: true
            },
            columns: [{
                text: '脚本编码',
                dataIndex: 'id',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '脚本名称',
                dataIndex: 'name',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '创建人',
                dataIndex: 'creator',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '创建时间',
                dataIndex: 'createdtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '执行人',
                dataIndex: 'executor',
                width: 120,
                align: 'left',
                sortable: true
            }, {
                text: '执行时间',
                dataIndex: 'executedtime',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '备注',
                dataIndex: 'comment',
                flex: 1,
                align: 'left',
                sortable: false,
                renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
                    metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
                    return value;
                }
            }],
            dockedItems: [{
                xtype: 'toolbar',
                dock: 'top',
                items: [{
                    xtype: 'button',
                    text: '升级脚本',
                    glyph: 0xf060,
                    handler: function (el, e) {
                        scriptWin.setTitle('历史数据库脚本升级');
                        scriptWin.store = csStore;
                        scriptWin.url = '/Account/UpdateCsScript';
                        scriptWin.getComponent('scriptForm').getForm().reset();
                        Ext.getCmp('scriptResult').setTextWithIcon('', '');
                        scriptWin.show();
                    }
                }]
            }],
            bbar: csPagingToolbar
        }
    ]
});

var layout = Ext.create('Ext.tab.Panel', {
    region: 'center',
    border: true,
    plain: true,
    items: [wsConfigTab, tsConfigTab, rtConfigTab, dbConfigTab]
});

//#endregion

//#region onReady

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(layout);

        var wsbasic = wsConfigTab.getForm(),
            wsresult = Ext.getCmp('wsresult');

        wsbasic.load({
            url: '/Account/GetWs',
            waitMsg: '正在处理...',
            waitTitle: '系统提示',
            success: function (form, action) {
                form.clearInvalid();
                wsresult.setTextWithIcon('', '');
            }
        });

        var tsbasic = tsConfigTab.getForm(),
            tsresult = Ext.getCmp('tsresult');

        tsbasic.load({
            url: '/Account/GetTs',
            waitMsg: '正在处理...',
            waitTitle: '系统提示',
            success: function (form, action) {
                form.clearInvalid();
                tsresult.setTextWithIcon('', '');
            }
        });

        var rtbasic = rtConfigTab.getForm(),
            rtresult = Ext.getCmp('rtresult');

        rtbasic.load({
            url: '/Account/GetRt',
            waitMsg: '正在处理...',
            waitTitle: '系统提示',
            success: function (form, action) {
                form.clearInvalid();
                rtresult.setTextWithIcon('', '');
            }
        });
    }
});

//#endregion