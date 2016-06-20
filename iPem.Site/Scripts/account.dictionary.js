﻿(function () {
    Ext.apply(Ext.form.field.VTypes, {
        IPv4: function (v) {
            return /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/.test(v);
        },
        IPv4Text: $$iPems.lang.Dictionary.IPv4Text,
        IPv4Mask: /[\d\.]/i
    });

    var layout = Ext.create('Ext.tab.Panel', {
        region: 'center',
        border: true,
        plain: true,
        items: [
            {
                id: 'wsconfig',
                title: $$iPems.lang.Dictionary.Ws.Title,
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
                    title: $$iPems.lang.Dictionary.Ws.LoginTitle,
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
                            fieldLabel: $$iPems.lang.Dictionary.Ws.LoginItems.IP,
                            emptyText: $$iPems.lang.Dictionary.Ws.LoginItems.IPEmptyText,
                            vtype: 'IPv4',
                            allowBlank: false
                        },
                        {
                            itemId: 'port',
                            name: 'port',
                            xtype: 'numberfield',
                            fieldLabel: $$iPems.lang.Dictionary.Ws.LoginItems.Port,
                            value: 8080,
                            minValue: 1,
                            maxValue: 65535,
                            allowBlank: false
                        },
                        {
                            itemId: 'uid',
                            name: 'uid',
                            xtype: 'textfield',
                            fieldLabel: $$iPems.lang.Dictionary.Ws.LoginItems.Uid,
                            allowBlank: false
                        },
                        {
                            itemId: 'pwd',
                            name: 'pwd',
                            xtype: 'textfield',
                            fieldLabel: $$iPems.lang.Dictionary.Ws.LoginItems.Pwd,
                            allowBlank: false
                        }
                    ]
                }, {
                    itemId: 'path',
                    xtype: 'fieldset',
                    title: $$iPems.lang.Dictionary.Ws.PathTitle,
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
                            fieldLabel: $$iPems.lang.Dictionary.Ws.PathItems.Data,
                            emptyText: $$iPems.lang.Dictionary.Ws.PathItems.DataEmptyText,
                            allowBlank: false
                        },
                        {
                            itemId: 'order',
                            name: 'order',
                            xtype: 'textfield',
                            fieldLabel: $$iPems.lang.Dictionary.Ws.PathItems.Order,
                            emptyText: $$iPems.lang.Dictionary.Ws.PathItems.OrderEmptyText,
                            allowBlank: false
                        }
                    ]
                }],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: $$iPems.lang.Save,
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
                title: $$iPems.lang.Dictionary.Ts.Title,
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
                                title: $$iPems.lang.Dictionary.Ts.BasicTitle,
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
                                        { boxLabel: $$iPems.lang.Dictionary.Ts.BasicItems.Enabled, name: 'basic', inputValue: 1 },
                                        { boxLabel: $$iPems.lang.Dictionary.Ts.BasicItems.Loop, name: 'basic', inputValue: 2 },
                                        { boxLabel: $$iPems.lang.Dictionary.Ts.BasicItems.Project, name: 'basic', inputValue: 3 }
                                    ]
                                }]
                            }, {
                                xtype: 'component',
                                width: 20
                            }, {
                                xtype: 'fieldset',
                                flex: 1,
                                title: $$iPems.lang.Dictionary.Ts.LevelTitle,
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
                                            { boxLabel: $$iPems.lang.Dictionary.Ts.LevelItems.Level1, name: 'level', inputValue: $$iPems.AlmLevel.Level1 },
                                            { boxLabel: $$iPems.lang.Dictionary.Ts.LevelItems.Level2, name: 'level', inputValue: $$iPems.AlmLevel.Level2 },
                                            { boxLabel: $$iPems.lang.Dictionary.Ts.LevelItems.Level3, name: 'level', inputValue: $$iPems.AlmLevel.Level3 },
                                            { boxLabel: $$iPems.lang.Dictionary.Ts.LevelItems.Level4, name: 'level', inputValue: $$iPems.AlmLevel.Level4 }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Ts.ContentTitle,
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
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.Area, name: 'content', inputValue: 1 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.Station, name: 'content', inputValue: 2 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.Room, name: 'content', inputValue: 3 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.Device, name: 'content', inputValue: 4 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.Point, name: 'content', inputValue: 5 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.AlmTime, name: 'content', inputValue: 6 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.AlmLevel, name: 'content', inputValue: 7 },
                                { boxLabel: $$iPems.lang.Dictionary.Ts.ContentItems.AlmComment, name: 'content', inputValue: 8 }
                            ]
                        }]
                    },
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Ts.ConditionTitle,
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
                                                xtype: 'StationTypeMultiCombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'devicetypes',
                                                xtype: 'DeviceTypeMultiCombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'pointtypes',
                                                xtype: 'PointTypeMultiCombo',
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
                                                xtype: 'RoomTypeMultiCombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'logictypes',
                                                xtype: 'LogicTypeMultiCombo',
                                                emptyText: $$iPems.lang.AllEmptyText
                                            },
                                            {
                                                name: 'pointnames',
                                                xtype: 'textfield',
                                                fieldLabel: $$iPems.lang.Dictionary.Ts.ConditionItems.Point,
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
                        text: $$iPems.lang.Save,
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
                title: $$iPems.lang.Dictionary.Pue.Title,
                glyph: 0xf043,
                layout: {
                    type: 'hbox',
                    align: 'stretch'
                }
            },
            {
                id: 'rtconfig',
                title: $$iPems.lang.Dictionary.Report.Title,
                glyph: 0xf044,
                xtype: 'form',
                overflowY: 'auto',
                items: [
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Report.Cache.Title,
                        margin: '10 20 20 20',
                        layout: {
                            type: 'hbox',
                            align: 'stretch'
                        },
                        defaults: {
                            margin: 15
                        },
                        items: [{
                            xtype: 'button',
                            text: $$iPems.lang.Dictionary.Report.Cache.Clear,
                            width: 90,
                            scale: 'medium',
                            handler: function () {
                                Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.Dictionary.Report.Cache.Confirm, function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var cacheResult = Ext.getCmp('cacheResult');
                                        cacheResult.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
                                        Ext.Ajax.request({
                                            url: '/Account/ClearCache',
                                            success: function (response, options) {
                                                var data = Ext.decode(response.responseText, true);
                                                if (data.success)
                                                    cacheResult.setTextWithIcon(data.message, 'x-icon-accept');
                                                else
                                                    cacheResult.setTextWithIcon(data.message, 'x-icon-error');
                                            }
                                        });
                                    }
                                });
                            }
                        },
                        {
                            id: 'cacheResult',
                            xtype: 'iconlabel',
                            text: ''
                        }]
                    },
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Report.Exception.Title,
                        margin: '10 20 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
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
                                        name: 'chaopin',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.Exception.ChaoPin,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.Exception.ChaoPinEmptyText,
                                        value: 1,
                                        minValue: 1
                                    }, {
                                        name: 'chaochang',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.Exception.ChaoChang,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.Exception.ChaoChangEmptyText,
                                        value: 1,
                                        minValue: 1
                                    }]
                                }, {
                                    xtype: 'container',
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield',
                                        value: $$iPems.lang.Dictionary.Report.Exception.CiShu,
                                        margin: '15 15 15 0'
                                    }, {
                                        xtype: 'displayfield',
                                        value: $$iPems.lang.Dictionary.Report.Exception.FenZhong,
                                        margin: '15 15 15 0'
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'chaoduan',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.Exception.ChaoDuan,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.Exception.ChaoDuanEmptyText,
                                        value: 1,
                                        minValue: 1
                                    }]
                                }, {
                                    xtype: 'container',
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield',
                                        value: $$iPems.lang.Dictionary.Report.Exception.FenZhong,
                                        margin: '15 15 15 0'
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Report.TingDian.Title,
                        margin: '10 20 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
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
                                        name: 'weitingdian',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.TingDian.WeiTingDian,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.TingDian.WeiTingDianEmptyText,
                                        value: 0,
                                        minValue: 0
                                    }, {
                                        name: 'tingdianxinhao',
                                        xtype: 'LogicPointMultiPicker',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.TingDian.Point,
                                        allowBlank: false
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'tingdian',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.TingDian.TingDian,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.TingDian.TingDianEmptyText,
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: $$iPems.lang.Dictionary.Report.FaDian.Title,
                        margin: '10 20 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
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
                                        name: 'weifadian',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.FaDian.WeiFaDian,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.FaDian.WeiFaDianEmptyText,
                                        value: 0,
                                        minValue: 0
                                    }, {
                                        name: 'fadianxinhao',
                                        xtype: 'LogicPointMultiPicker',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.FaDian.Point,
                                        allowBlank: false
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'fadian',
                                        xtype: 'numberfield',
                                        fieldLabel: $$iPems.lang.Dictionary.Report.FaDian.FaDian,
                                        allowBlank: false,
                                        emptyText: $$iPems.lang.Dictionary.Report.FaDian.FaDianEmptyText,
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    }
                ],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: $$iPems.lang.Save,
                        width: 90,
                        scale: 'medium',
                        handler: function () {
                            var rtconfig = Ext.getCmp('rtconfig'),
                                rtbasic = rtconfig.getForm(),
                                rtresult = Ext.getCmp('rtresult');

                            rtresult.setTextWithIcon('', '');
                            if (rtbasic.isValid()) {
                                rtresult.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
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
                                rtresult.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
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

            var rtconfig = Ext.getCmp('rtconfig'),
                rtbasic = rtconfig.getForm(),
                rtresult = Ext.getCmp('rtresult');

            rtbasic.load({
                url: '/Account/GetRt',
                waitMsg: $$iPems.lang.AjaxHandling,
                waitTitle: $$iPems.lang.SysTipTitle,
                success: function (form, action) {
                    form.clearInvalid();
                    rtresult.setTextWithIcon('', '');
                }
            });
        }
    });
})();