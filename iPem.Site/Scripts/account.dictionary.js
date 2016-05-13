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
                            itemId: 'pwd',
                            name: 'pwd',
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
                            itemId: 'data',
                            name: 'data',
                            xtype: 'textfield',
                            fieldLabel: '实时数据 访问路径',
                            emptyText: '示例： /Services/GetData?wsdl',
                            allowBlank: false
                        },
                        {
                            itemId: 'order',
                            name: 'order',
                            xtype: 'textfield',
                            fieldLabel: '远程控制 访问路径',
                            emptyText: '示例： /Services/SetOrder?wsdl',
                            allowBlank: false
                        }
                    ]
                }],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存',
                        width: 90,
                        scale: 'medium',
                        handler: function () {
                            var wsconfig = Ext.getCmp('wsconfig'),
                                wsbasic = wsconfig.getForm(),
                                wsresult = Ext.getCmp('wsresult');

                            wsresult.setTextWithIcon('', '');
                            if (wsbasic.isValid()) {
                                wsresult.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
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
                                wsresult.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
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
                                    columns:1,
                                    items: [
                                        { boxLabel: '启用语音', name: 'basic', inputValue: 1 },
                                        { boxLabel: '循环播报', name: 'basic', inputValue: 2 }
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
                                            { boxLabel: '一级告警', name: 'level', inputValue: $$iPems.AlmLevel.Level1 },
                                            { boxLabel: '二级告警', name: 'level', inputValue: $$iPems.AlmLevel.Level2 },
                                            { boxLabel: '三级告警', name: 'level', inputValue: $$iPems.AlmLevel.Level3 },
                                            { boxLabel: '四级告警', name: 'level', inputValue: $$iPems.AlmLevel.Level4 }
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
                                { boxLabel: '所属区域', name: 'content', inputValue: 1 },
                                { boxLabel: '所属站点', name: 'content', inputValue: 2 },
                                { boxLabel: '所属机房', name: 'content', inputValue: 3 },
                                { boxLabel: '所属设备', name: 'content', inputValue: 4 },
                                { boxLabel: '所属信号', name: 'content', inputValue: 5 },
                                { boxLabel: '告警时间', name: 'content', inputValue: 6 },
                                { boxLabel: '告警等级', name: 'content', inputValue: 7 },
                                { boxLabel: '告警描述', name: 'content', inputValue: 8 }
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
                                                name: 'stationtypes',
                                                xtype: 'station.type.multicombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'devicetypes',
                                                xtype: 'device.type.multicombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'pointtypes',
                                                xtype: 'point.type.multicombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            }
                                        ]
                                    },
                                    {
                                        xtype: 'container',
                                        flex: 1,
                                        layout: 'anchor',
                                        items: [
                                            {
                                                name: 'roomtypes',
                                                xtype: 'room.type.multicombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'logictypes',
                                                xtype: 'logic.type.multicombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'pointnames',
                                                xtype: 'textfield',
                                                fieldLabel: '信号名称',
                                                emptyText: $$iPems.lang.MultiConditionEmptyText
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
                        text: '保存',
                        width: 90,
                        scale: 'medium',
                        handler: function () {
                            var tsconfig = Ext.getCmp('tsconfig'),
                                tsbasic = tsconfig.getForm(),
                                tsresult = Ext.getCmp('tsresult');

                            tsresult.setTextWithIcon('', '');
                            if (tsbasic.isValid()) {
                                tsresult.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
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
                                tsresult.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
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
                title: '报表参数',
                glyph: 0xf044,
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
                waitMsg: $$iPems.lang.AjaxHandling,
                waitTitle: $$iPems.lang.SysTipTitle,
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
                waitMsg: $$iPems.lang.AjaxHandling,
                waitTitle: $$iPems.lang.SysTipTitle,
                success: function (form, action) {
                    form.clearInvalid();
                    tsresult.setTextWithIcon('', '');
                }
            });
        }
    });
})();