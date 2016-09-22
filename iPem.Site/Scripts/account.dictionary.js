(function () {
    Ext.apply(Ext.form.field.VTypes, {
        IPv4: function (v) {
            return /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/.test(v);
        },
        IPv4Text: 'IPv4地址格式错误',
        IPv4Mask: /[\d\.]/i
    });
    
    var layout = Ext.create('Ext.tab.Panel', {
        region: 'center',
        border: true,
        plain: true,
        items: [
            {
                id: 'wsconfig',
                title: '数据通信',
                glyph: 0xf047,
                xtype: 'form',
                overflowY: 'auto',
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                items: [{
                    itemId: 'login',
                    xtype: 'fieldset',
                    title: 'WebService 登录信息',
                    margin: '20 20 10 20',
                    defaultType: 'textfield',
                    layout: 'anchor',
                    fieldDefaults: {
                        anchor: '100%',
                        labelWidth: 150,
                        labelAlign: 'left',
                        margin: 25
                    },
                    items: [
                        {
                            itemId: 'ip',
                            name:'ip',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 通信地址',
                            emptyText: '示例： 192.168.10.100',
                            vtype: 'IPv4',
                            allowBlank: false
                        },
                        {
                            itemId: 'port',
                            name: 'port',
                            xtype: 'numberfield',
                            fieldLabel: 'WebService 通信端口',
                            value: 8080,
                            minValue: 1,
                            maxValue: 65535,
                            allowBlank: false
                        },
                        {
                            itemId: 'uid',
                            name: 'uid',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 登录帐号',
                            allowBlank: false
                        },
                        {
                            itemId: 'password',
                            name: 'password',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 登录密码',
                            allowBlank: false
                        }
                    ]
                }, {
                    itemId: 'path',
                    xtype: 'fieldset',
                    title: 'WebService 访问路径',
                    margin: '10 20 20 20',
                    defaultType: 'textfield',
                    layout: 'anchor',
                    fieldDefaults: {
                        anchor: '100%',
                        labelWidth: 150,
                        labelAlign: 'left',
                        margin: 25
                    },
                    items: [
                        {
                            itemId: 'dataPath',
                            name: 'dataPath',
                            xtype: 'textfield',
                            fieldLabel: '实时数据 访问路径',
                            emptyText: '示例： /Services/GetData',
                            allowBlank: false
                        },
                        {
                            itemId: 'orderPath',
                            name: 'orderPath',
                            xtype: 'textfield',
                            fieldLabel: '远程控制 访问路径',
                            emptyText: '示例： /Services/SetOrder',
                            allowBlank: false
                        }
                    ]
                }],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        handler: function () {
                            var wsconfig = Ext.getCmp('wsconfig'),
                                wsbasic = wsconfig.getForm(),
                                wsresult = Ext.getCmp('wsresult');

                            wsresult.setTextWithIcon('', '');
                            if (wsbasic.isValid()) {
                                wsresult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
            },
            {
                id: 'tsconfig',
                title: '语音播报',
                glyph: 0xf048,
                xtype: 'form',
                overflowY: 'auto',
                items: [
                    {
                        xtype: 'container',
                        layout: 'hbox',
                        margin: '20 20 10 20',
                        items: [
                            {
                                xtype: 'fieldset',
                                flex: 1,
                                title: '语音播报',
                                defaultType: 'checkbox',
                                layout: 'anchor',
                                defaults: {
                                    anchor: '100%',
                                    margin: 15
                                },
                                items: [{
                                    xtype: 'checkboxgroup',
                                    columns:2,
                                    items: [
                                        { boxLabel: '启用语音播报', name: 'basic', inputValue: 1 },
                                        { boxLabel: '循环播报告警', name: 'basic', inputValue: 2 },
                                        { boxLabel: '禁播工程告警', name: 'basic', inputValue: 3 }
                                    ]
                                }]
                            }, {
                                xtype: 'component',
                                width: 20
                            }, {
                                xtype: 'fieldset',
                                flex: 1,
                                title: '播报级别',
                                defaultType: 'radio',
                                layout: 'anchor',
                                defaults: {
                                    anchor: '100%',
                                    margin: 15
                                },
                                items: [
                                    {
                                        xtype: 'checkboxgroup',
                                        columns:2,
                                        items: [
                                            { boxLabel: '一级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level1 },
                                            { boxLabel: '二级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level2 },
                                            { boxLabel: '三级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level3 },
                                            { boxLabel: '四级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level4 }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '播报内容',
                        margin: '10 20 20 20',
                        defaultType: 'checkbox',
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
                            labelWidth: 60,
                            labelAlign: 'left',
                            margin: 15
                        },
                        items: [
                            {
                                xtype: 'container',
                                layout: 'hbox',
                                items: [
                                    {
                                        xtype: 'container',
                                        flex: 1,
                                        layout: 'anchor',
                                        items: [
                                            {
                                                name: 'stationTypes',
                                                xtype: 'StationTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'deviceTypes',
                                                xtype: 'DeviceTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'pointNames',
                                                xtype: 'textfield',
                                                fieldLabel: '信号名称',
                                                emptyText: '多条件请以;分隔，例: A;B;C'
                                            }
                                        ]
                                    },
                                    {
                                        xtype: 'container',
                                        flex: 1,
                                        layout: 'anchor',
                                        items: [
                                            {
                                                name: 'roomTypes',
                                                xtype: 'RoomTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'logicTypes',
                                                xtype: 'LogicTypeMultiPicker',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'pointExtset',
                                                xtype: 'textfield',
                                                fieldLabel: '信号标识',
                                                emptyText: '多条件请以;分隔，例: A;B;C'
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
                            var tsconfig = Ext.getCmp('tsconfig'),
                                tsbasic = tsconfig.getForm(),
                                tsresult = Ext.getCmp('tsresult');

                            tsresult.setTextWithIcon('', '');
                            if (tsbasic.isValid()) {
                                tsresult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
            },
            {
                title: '能耗分类',
                glyph: 0xf043,
                layout: {
                    type: 'hbox',
                    align: 'stretch'
                }
            },
            {
                id: 'rtconfig',
                title: '报表参数',
                glyph: 0xf044,
                xtype: 'form',
                overflowY: 'auto',
                items: [
                    {
                        xtype: 'fieldset',
                        title: '缓存管理',
                        margin: '10 35 20 20',
                        layout: {
                            type: 'hbox',
                            align: 'stretch'
                        },
                        items: [{
                            xtype: 'button',
                            text: '清空所有缓存',
                            cls: 'custom-button custom-danger',
                            margin: 15,
                            handler: function () {
                                Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var cacheResult = Ext.getCmp('cacheResult');
                                        cacheResult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
                                        cacheResult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
                                        Ext.Ajax.request({
                                            url: '/Account/ClearGlobalCache',
                                            success: function (response, options) {
                                                var data = Ext.decode(response.responseText, true);
                                                if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error' );
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
                                        cacheResult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
                            id: 'cacheResult',
                            xtype: 'iconlabel',
                            margin: '21 0 21 0',
                            text: '',
                            flex: 1
                        }]
                    },
                    {
                        xtype: 'fieldset',
                        title: '异常告警',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
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
                                    value: '次数（注：超频告警是指告警次数小于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '市电停电',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 100,
                            labelAlign: 'left',
                            margin: 15
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                items: [{
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'tingDianXinHao',
                                        xtype: 'LogicPointPicker',
                                        fieldLabel: '市电状态信号',
                                        allowBlank: false,
                                        emptyText: '请选择市电状态信号...'
                                    }, {
                                        name: 'weiTingDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '市电正常测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '市电正常状态时的测值',
                                        value: 0,
                                        minValue: 0
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield'
                                    }, {
                                        name: 'tingDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '市电停电测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '市电停电状态时的测值',
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '油机发电',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 100,
                            labelAlign: 'left',
                            margin: 15
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                items: [{
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'faDianXinHao',
                                        xtype: 'LogicPointPicker',
                                        fieldLabel: '油机状态信号',
                                        allowBlank: false,
                                        emptyText: '请选择油机状态信号...'
                                    }, {
                                        name: 'weiFaDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '油机停机测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '油机停机状态时的测值',
                                        value: 0,
                                        minValue: 0
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield'
                                    }, {
                                        name: 'faDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '油机工作测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '油机工作状态时的测值',
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '系统设备完好率',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
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
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
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
                    }
                ],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        handler: function () {
                            var rtconfig = Ext.getCmp('rtconfig'),
                                rtbasic = rtconfig.getForm(),
                                rtresult = Ext.getCmp('rtresult');

                            rtresult.setTextWithIcon('', '');
                            if (rtbasic.isValid()) {
                                rtresult.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
            }
        ]
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(layout);

            var wsconfig = Ext.getCmp('wsconfig'),
                wsbasic = wsconfig.getForm(),
                wsresult = Ext.getCmp('wsresult');

            wsbasic.load({
                url: '/Account/GetWs',
                waitMsg: '正在处理，请稍后...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    wsresult.setTextWithIcon('', '');
                }
            });

            var tsconfig = Ext.getCmp('tsconfig'),
                tsbasic = tsconfig.getForm(),
                tsresult = Ext.getCmp('tsresult');

            tsbasic.load({
                url: '/Account/GetTs',
                waitMsg: '正在处理，请稍后...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    tsresult.setTextWithIcon('', '');
                }
            });

            var rtconfig = Ext.getCmp('rtconfig'),
                rtbasic = rtconfig.getForm(),
                rtresult = Ext.getCmp('rtresult');

            rtbasic.load({
                url: '/Account/GetRt',
                waitMsg: '正在处理，请稍后...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    rtresult.setTextWithIcon('', '');
                }
            });
        }
    });
})();