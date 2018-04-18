Ext.define('FsuModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'code', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'vendor', type: 'string' },
        { name: 'ip', type: 'string' },
        { name: 'port', type: 'int' },
        { name: 'change', type: 'string' },
        { name: 'last', type: 'string' },
        { name: 'status', type: 'string' },
        { name: '_status', type: 'boolean' },
        { name: 'comment', type: 'string' },
        { name: 'exestatus', type: 'string' },
        { name: 'execomment', type: 'string' },
        { name: 'exetime', type: 'string' },
        { name: 'exer', type: 'string' }
    ],
    idProperty: 'id'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'FsuModel',
    proxy: {
        type: 'ajax',
        url: '/Fsu/RequestFsu',
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
    },
    listeners: {
        load: function (me, records, successful) {
            if (successful) {
                //$$iPems.Tasks.fsuTask.fireOnStart = false;
                //$$iPems.Tasks.fsuTask.restart();
            }
        }
    }
});
var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentLayout = Ext.create('Ext.grid.Panel', {
    glyph: 0xf029,
    title: 'FSU信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    //selType: 'checkboxmodel',
    viewConfig: {
        loadMask: true,
        forceFit: true,
        trackOver: true,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: '序号',
        dataIndex: 'index',
        width: 60,
        align: 'left',
        locked: true,
        sortable: true
    }, {
        text: '编号',
        dataIndex: 'code',
        align: 'center',
        width: 120,
        locked: true,
        sortable: true
    }, {
        text: '名称',
        dataIndex: 'name',
        align: 'left',
        width: 120,
        locked: true,
        sortable: true
    }, {
        text: '所属区域',
        dataIndex: 'area',
        align: 'left',
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        sortable: true
    }, {
        text: '所属机房',
        dataIndex: 'room',
        align: 'left',
        sortable: true
    }, {
        text: '所属厂家',
        dataIndex: 'vendor',
        align: 'left',
        sortable: true
    }, {
        text: 'IP',
        dataIndex: 'ip',
        align: 'center',
        width: 120,
        sortable: true
    }, {
        text: '端口',
        dataIndex: 'port',
        align: 'center',
        sortable: true
    }, {
        text: '注册时间',
        dataIndex: 'change',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '离线时间',
        dataIndex: 'last',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '状态',
        dataIndex: 'status',
        align: 'center',
        sortable: true
    }, {
        text: '备注',
        dataIndex: 'comment',
        align: 'left',
        sortable: true
    }, {
        text: '执行状态',
        dataIndex: 'exestatus',
        align: 'left',
        sortable: true
    }, {
        text: '执行说明',
        dataIndex: 'execomment',
        align: 'left',
        width: 200,
        sortable: true
    }, {
        text: '执行时间',
        dataIndex: 'exetime',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '执行人',
        dataIndex: 'exer',
        align: 'left',
        sortable: true
    }, {
        text: 'FTP',
        align: 'center',
        dataIndex: 'id',
        width: 200,
        renderer: function (value, p, record) {
            if (Ext.isEmpty(value)) return Ext.emptyString;
            if (record.get('_status') === false) return "--";
            return '<a data="logger" class="grid-link" href="javascript:void(0);">日志</a><span class="grid-link-split">|</span><a data="config" class="grid-link" href="javascript:void(0);">参数</a><span class="grid-link-split">|</span><a data="alarm" class="grid-link" href="javascript:void(0);">告警</a><span class="grid-link-split">|</span><a data="measurement" class="grid-link" href="javascript:void(0);">性能</a>';
        }
    }, {
        text: '操作',
        align: 'center',
        dataIndex: 'id',
        width: 150,
        renderer: function (value, p, record) {
            if (Ext.isEmpty(value)) return Ext.emptyString;
            if (record.get('_status') === false) return "--";
            return '<a data="setting" class="grid-link" href="javascript:void(0);">配置</a><span class="grid-link-split">|</span><a data="upgrade" class="grid-link" href="javascript:void(0);">升级</a><span class="grid-link-split">|</span><a data="reboot" class="grid-link" href="javascript:void(0);">重启</a>';
        }
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var columns = view.getGridColumns();
            if (columns.length == 0 || columns.length <= cellIndex) return false;
            var colname = columns[cellIndex].dataIndex;
            if (colname === 'id') {
                var target = e.getTarget('a.grid-link');
                if (!Ext.isEmpty(target)) {
                    var operate = target.getAttribute("data");
                    if (!Ext.isEmpty(operate)) {
                        var id = record.get('id');
                        if (operate == 'logger') {
                            logger(id);
                        } else if (operate == 'config') {
                            config(id);
                        } else if (operate == 'alarm') {
                            alarm(id);
                        } else if (operate == 'measurement') {
                            measurement(id);
                        } else if (operate == 'setting') {
                            setting(id);
                        } else if (operate == 'upgrade') {
                            upgrade(record);
                        } else if (operate == 'reboot') {
                            reboot(id);
                        }
                    }
                }
            }
        }
    },
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [{
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'rangeField',
                xtype: 'RoomPicker',
                fieldLabel: '查询范围',
                width: 220,
                emptyText: '默认全部'
            }, {
                id: 'statusField',
                xtype: 'multicombo',
                fieldLabel: 'FSU状态',
                valueField: 'id',
                displayField: 'text',
                delimiter: $$iPems.Delimiter,
                queryMode: 'local',
                triggerAction: 'all',
                selectionMode: 'all',
                emptyText: '默认全部',
                forceSelection: true,
                labelWidth: 60,
                width: 220,
                store: Ext.create('Ext.data.Store', {
                    fields: [
                        { name: 'id', type: 'int' },
                        { name: 'text', type: 'string' },
                    ],
                    data: [
                        { id: 1, text: '在线' },
                        { id: 0, text: '离线' },
                    ]
                })
            }, {
                id: 'vendorField',
                xtype: 'VendorMultiCombo',
                emptyText: '默认全部'
            }, {
                xtype: 'button',
                text: '数据查询',
                glyph: 0xf005,
                handler: function (el, e) {
                    query();
                }
            }, {
                xtype: 'button',
                text: '升级文件管理',
                glyph: 0xf060,
                handler: function (el, e) {
                    ftpmgr();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'filterField',
                xtype: 'combobox',
                fieldLabel: '筛选类型',
                labelWidth: 60,
                width: 220,
                store: Ext.create('Ext.data.Store', {
                    fields: [
                         { name: 'id', type: 'int' },
                         { name: 'text', type: 'string' }
                    ],
                    data: [
                        { "id": 1, "text": '按FSU编号' },
                        { "id": 2, "text": '按FSU名称' }
                    ]
                }),
                align: 'center',
                value: 1,
                editable: false,
                displayField: 'text',
                valueField: 'id',
            }, {
                id: 'keywordsField',
                xtype: 'textfield',
                width: 448,
                maxLength: 100,
                emptyText: '多条件请以;分隔，例: A;B;C',
            }, {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (el, e) {
                    print();
                }
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var alarmWin = Ext.create('Ext.window.Window', {
    title: '告警日期框',
    glyph: 0xf045,
    height: 180,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    record: null,
    items: [{
        itemId: 'alarmForm',
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
                itemId: 'date',
                name: 'date',
                xtype: 'datefield',
                fieldLabel: '告警日期',
                format: 'Ymd',
                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                editable: false,
                allowBlank: false
            },
            {
                xtype: 'iconlabel',
                text: '提示：通过FTP可以查看一天的告警记录文件',
                iconCls: 'x-icon-tips',
                style: {
                    marginLeft: '75px'
                }
            }
        ]
    }],
    buttonAlign: 'right',
    buttons: [
        { id: 'alarmResult', xtype: 'iconlabel', text: '' },
        { xtype: 'tbfill' },
        {
            xtype: 'button',
            text: '确定',
            handler: function (el, e) {
                var form = alarmWin.getComponent('alarmForm'),
                    date = form.getComponent('date'),
                    label = Ext.getCmp('alarmResult');

                alarmWin.hide();
                ftpWin.show(null, function () {
                    if (iframe.rendered) {
                        iframe.src = Ext.String.format('/Ftp/FsuAlarm?title={0}&fsu={1}&dt={2}', encodeURI('FSU告警管理'), alarmWin.record, date.getRawValue());
                        iframe.load();
                    }
                });
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                alarmWin.hide();
            }
        }
    ]
});

var upgradeWin = Ext.create('Ext.window.Window', {
    title: 'FSU升级',
    glyph: 0xf060,
    height: 250,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    record: null,
    items: [{
        itemId: 'upgradeForm',
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
                itemId: 'fsuid',
                name: 'fsuid',
                fieldLabel: 'FSU编号',
                allowBlank: false,
                readOnly: true
            },
            {
                itemId: 'fsuname',
                name: 'fsuname',
                fieldLabel: 'FSU名称',
                allowBlank: false,
                readOnly: true
            },
            {
                itemId: 'upgradefile',
                name: 'upgradefile',
                xtype: "combo",
                fieldLabel: '升级文件',
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                selectOnFocus: true,
                forceSelection: true,
                store: Ext.create('Ext.data.Store', {
                    autoLoad: false,
                    pageSize: 1024,
                    fields: [
                        { name: 'id', type: 'string' },
                        { name: 'text', type: 'string' },
                        { name: 'comment', type: 'string' }
                    ],
                    proxy: {
                        type: 'ajax',
                        url: '/Ftp/GetUpgradeFiles',
                        reader: {
                            type: 'json',
                            successProperty: 'success',
                            messageProperty: 'message',
                            totalProperty: 'total',
                            root: 'data'
                        }
                    }
                })
            },
            {
                xtype: 'iconlabel',
                text: '提示：请确认升级版本，升级开始将无法撤销。',
                iconCls: 'x-icon-tips',
                style: {
                    marginLeft: '75px'
                }
            }
        ]
    }],
    buttonAlign: 'right',
    buttons: [
        { id: 'upgradeResult', xtype: 'iconlabel', text: '' },
        { xtype: 'tbfill' },
        {
            xtype: 'button',
            text: '升级',
            handler: function (el, e) {
                var form = upgradeWin.getComponent('upgradeForm'),
                    baseForm = form.getForm(),
                    upgradeResult = Ext.getCmp('upgradeResult');

                upgradeResult.setTextWithIcon('', '');
                if (baseForm.isValid()) {
                    Ext.Msg.confirm('确认对话框', '您确认要升级吗？', function (buttonId, text) {
                        if (buttonId === 'yes') {
                            upgradeResult.setTextWithIcon('正在下发命令...', 'x-icon-loading');
                            baseForm.submit({
                                clientValidation: true,
                                submitEmptyText: false,
                                preventWindow: true,
                                url: '/Fsu/Upgrade',
                                success: function (form, action) {
                                    upgradeResult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    query();
                                },
                                failure: function (form, action) {
                                    var message = '客户端未知错误';
                                    if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                        message = action.result.message;

                                    upgradeResult.setTextWithIcon(message, 'x-icon-error');
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
                upgradeWin.hide();
            }
        }
    ]
});

var settingWin = Ext.create('Ext.window.Window', {
    title: 'FSU配置管理',
    glyph: 0xf059,
    height: 400,
    width: 450,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    record: null,
    layout: {
        type: 'hbox',
        align: 'stretch'
    },
    items: [{
        xtype: "tabpanel",
        flex: 1,
        margin: '5 5 5 5',
        activeTab: 0,
        plain: true,
        defaults: {
            autoScroll: true,
            border: false,
            useArrows: false,
            rootVisible: false,
        },
        items: [
        {
            id: 'fsu_userForm',
            xtype: 'form',
            title: 'FSU用户',
            glyph: 0xf012,
            border: false,
            defaultType: 'textfield',
            defaults: {
                anchor: '100%'
            },
            fieldDefaults: {
                labelWidth: 60,
                labelAlign: "left",
                flex: 1,
                margin: '10 5 10 5'
            },
            items: [
                {
                    id: 'fsu_user',
                    name: 'user',
                    xtype: 'textfield',
                    fieldLabel: '用户名称',
                    allowBlank: false,
                    labelAlign: 'top'
                },
                {
                    id: 'fsu_password',
                    name: 'password',
                    xtype: 'textfield',
                    fieldLabel: '登录密码',
                    allowBlank: false,
                    labelAlign: 'top'
                },
                {
                    id: 'fsu_package',
                    name: 'package',
                    submitValue: false,
                    xtype: 'textareafield',
                    fieldLabel: '原始报文',
                    height: 135,
                    readOnly: true,
                    labelAlign: 'top'
                }
            ],
            buttons: [
                { id: 'fsu_userResult', xtype: 'iconlabel', text: '' },
                { xtype: 'tbfill' },
                {
                    xtype: 'button',
                    text: '读取',
                    handler: function (el, e) {
                        var form = Ext.getCmp('fsu_userForm'),
                            baseForm = form.getForm(),
                            result = Ext.getCmp('fsu_userResult');

                        baseForm.load({
                            url: '/Fsu/GetFsuLogin',
                            params: { fsu: settingWin.record },
                            waitMsg: '正在处理...',
                            waitTitle: '系统提示',
                            preventWindow: true,
                            success: function (form, action) {
                                form.clearInvalid();
                                result.setTextWithIcon(action.result.message, 'x-icon-accept');
                            },
                            failure: function (form, action) {
                                var message = '客户端未知错误';
                                if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                    message = action.result.message;

                                result.setTextWithIcon(message, 'x-icon-error');
                            }
                        });
                    }
                },
                {
                    xtype: 'button',
                    text: '下发',
                    handler: function (el, e) {
                        var form = Ext.getCmp('fsu_userForm'),
                            baseForm = form.getForm(),
                            result = Ext.getCmp('fsu_userResult');

                        result.setTextWithIcon('', '');
                        if (baseForm.isValid()) {
                            Ext.Msg.confirm('确认对话框', '您确认要下发配置吗？', function (buttonId, text) {
                                if (buttonId === 'yes') {
                                    result.setTextWithIcon('正在下发配置...', 'x-icon-loading');
                                    baseForm.submit({
                                        submitEmptyText: false,
                                        clientValidation: true,
                                        preventWindow: true,
                                        url: '/Fsu/SetFsuLogin',
                                        params: {
                                            fsu: settingWin.record
                                        },
                                        success: function (form, action) {
                                            result.setTextWithIcon(action.result.message, 'x-icon-accept');
                                        },
                                        failure: function (form, action) {
                                            var message = '客户端未知错误';
                                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                                message = action.result.message;

                                            result.setTextWithIcon(message, 'x-icon-error');
                                        }
                                    });
                                }
                            });
                        } else {
                            result.setTextWithIcon('表单填写错误', 'x-icon-error');
                        }
                    }
                }
            ]
        },
        {
            id: 'ftp_userForm',
            xtype: 'form',
            title: 'FTP用户',
            glyph: 0xf012,
            border: false,
            defaultType: 'textfield',
            defaults: {
                anchor: '100%'
            },
            fieldDefaults: {
                labelWidth: 60,
                labelAlign: "left",
                flex: 1,
                margin: '10 5 10 5'
            },
            items: [
                {
                    id: 'ftp_user',
                    name: 'user',
                    xtype: 'textfield',
                    fieldLabel: '用户名称',
                    allowBlank: false,
                    labelAlign: 'top'
                },
                {
                    id: 'ftp_password',
                    name: 'password',
                    xtype: 'textfield',
                    fieldLabel: '登录密码',
                    allowBlank: false,
                    labelAlign: 'top'
                },
                {
                    id: 'ftp_package',
                    name: 'package',
                    submitValue: false,
                    xtype: 'textareafield',
                    fieldLabel: '原始报文',
                    height: 135,
                    readOnly: true,
                    labelAlign: 'top'
                }
            ],
            buttons: [
                { id: 'ftp_userResult', xtype: 'iconlabel', text: '' },
                { xtype: 'tbfill' },
                {
                    xtype: 'button',
                    text: '读取',
                    handler: function (el, e) {
                        var form = Ext.getCmp('ftp_userForm'),
                            baseForm = form.getForm(),
                            result = Ext.getCmp('ftp_userResult');

                        baseForm.load({
                            url: '/Fsu/GetFtpLogin',
                            params: { fsu: settingWin.record },
                            waitMsg: '正在处理...',
                            waitTitle: '系统提示',
                            preventWindow: true,
                            success: function (form, action) {
                                form.clearInvalid();
                                result.setTextWithIcon(action.result.message, 'x-icon-accept');
                            },
                            failure: function (form, action) {
                                var message = '客户端未知错误';
                                if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                    message = action.result.message;

                                result.setTextWithIcon(message, 'x-icon-error');
                            }
                        });
                    }
                },
                {
                    xtype: 'button',
                    text: '下发',
                    handler: function (el, e) {
                        var form = Ext.getCmp('ftp_userForm'),
                            baseForm = form.getForm(),
                            result = Ext.getCmp('ftp_userResult');

                        result.setTextWithIcon('', '');
                        if (baseForm.isValid()) {
                            Ext.Msg.confirm('确认对话框', '您确认要下发配置吗？', function (buttonId, text) {
                                if (buttonId === 'yes') {
                                    result.setTextWithIcon('正在下发配置...', 'x-icon-loading');
                                    baseForm.submit({
                                        submitEmptyText: false,
                                        clientValidation: true,
                                        preventWindow: true,
                                        url: '/Fsu/SetFtpLogin',
                                        params: {
                                            fsu: settingWin.record
                                        },
                                        success: function (form, action) {
                                            result.setTextWithIcon(action.result.message, 'x-icon-accept');
                                        },
                                        failure: function (form, action) {
                                            var message = '客户端未知错误';
                                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                                message = action.result.message;

                                            result.setTextWithIcon(message, 'x-icon-error');
                                        }
                                    });
                                }
                            });
                        } else {
                            result.setTextWithIcon('表单填写错误', 'x-icon-error');
                        }
                    }
                }
            ]
        }
        ]
    }]
});

var iframe = Ext.create('Ext.ux.IFrame', {
    flex: 1,
    loadMask: '正在处理...',
    src: '/Ftp'
});

var ftpWin = Ext.create('Ext.window.Window', {
    header:false,
    height: 600,
    width: 850,
    modal: true,
    border: false,
    hidden: true,
    shadow : false,
    closeAction: 'hide',
    resizable: {
        transparent: true
    },
    style: {
        border: 'none'
    },
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items: [iframe]
});

var closeWin = function () {
    ftpWin.hide();
}

var ftpmgr = function () {
    ftpWin.show(null, function () {
        if (iframe.rendered) {
            iframe.src = Ext.String.format('/Ftp/Update?title={0}', encodeURI('升级文件管理'));
            iframe.load();
        }
    });
};

var logger = function (id) {
    if (Ext.isEmpty(id)) return false;
    ftpWin.show(null, function () {
        if (iframe.rendered) {
            iframe.src = Ext.String.format('/Ftp/FsuLog?title={0}&fsu={1}', encodeURI('FSU日志文件管理'), id);
            iframe.load();
        }
    });
};

var config = function (id) {
    if (Ext.isEmpty(id)) return false;
    ftpWin.show(null, function () {
        if (iframe.rendered) {
            iframe.src = Ext.String.format('/Ftp/FsuConfig?title={0}&fsu={1}', encodeURI('FSU参数文件管理'), id);
            iframe.load();
        }
    });
};

var alarm = function (id) {
    if (Ext.isEmpty(id)) return false;
    var form = alarmWin.getComponent('alarmForm'),
        date = form.getComponent('date'),
        label = Ext.getCmp('alarmResult');

    alarmWin.record = id;
    form.getForm().reset();
    label.setTextWithIcon('', '');
    alarmWin.show();
};

var measurement = function (id) {
    if (Ext.isEmpty(id)) return false;
    ftpWin.show(null, function () {
        if (iframe.rendered) {
            iframe.src = Ext.String.format('/Ftp/FsuMeasurement?title={0}&fsu={1}', encodeURI('FSU性能文件管理'), id);
            iframe.load();
        }
    });
}

var setting = function (id) {
    if (Ext.isEmpty(id)) return false;
    var fsu_userForm = Ext.getCmp('fsu_userForm'),
        fsu_userResult = Ext.getCmp('fsu_userResult'),
        ftp_userForm = Ext.getCmp('ftp_userForm'),
        ftp_userResult = Ext.getCmp('ftp_userResult');

    fsu_userForm.getForm().reset();
    fsu_userResult.setTextWithIcon('', '');
    ftp_userForm.getForm().reset();
    ftp_userResult.setTextWithIcon('', '');
    settingWin.record = id;
    settingWin.show();
};

var upgrade = function (record) {
    if (Ext.isEmpty(record)) return false;
    var form = upgradeWin.getComponent('upgradeForm'),
        fsuid = form.getComponent('fsuid'),
        fsuname = form.getComponent('fsuname'),
        files = form.getComponent('upgradefile'),
        label = Ext.getCmp('upgradeResult');

    form.getForm().reset();
    label.setTextWithIcon('', '');

    upgradeWin.record = record;
    fsuid.setValue(record.get('id'));
    fsuname.setValue(record.get('name'));
    files.getStore().loadPage(1, {
        callback: function (records, operation, success) {
            if (success && records.length > 0)
                files.select(records[0]);
        }
    });
    upgradeWin.show();
};

var reboot = function(id) {
    if (Ext.isEmpty(id)) return false;
    Ext.Msg.confirm('确认对话框', '您确认要重启吗？', function (buttonId, text) {
        if (buttonId === 'yes') {
            Ext.Ajax.request({
                url: '/Fsu/Reboot',
                params: { id: id },
                mask: new Ext.LoadMask(currentLayout, { msg: '正在处理...' }),
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                        currentPagingToolbar.doRefresh();
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        }
    });
};

var query = function () {
    var rangeField = Ext.getCmp('rangeField'),
        statusField = Ext.getCmp('statusField'),
        vendorField = Ext.getCmp('vendorField'),
        filterField = Ext.getCmp('filterField'),
        keywordsField = Ext.getCmp('keywordsField'),
        parent = rangeField.getValue(),
        status = statusField.getValue(),
        vendors = vendorField.getValue(),
        filter = filterField.getValue(),
        keywords = keywordsField.getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.status = status;
    proxy.extraParams.vendors = vendors;
    proxy.extraParams.filter = filter;
    proxy.extraParams.keywords = keywords;
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var refresh = function () {
    currentPagingToolbar.doRefresh();
}

var print = function () {
    $$iPems.download({
        url: '/Fsu/DownloadFsu',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);
    }

    $$iPems.Tasks.fsuTask.run = function () {
        refresh();
    };
});