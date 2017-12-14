(function () {
    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
			{ name: 'level', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'interval', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'supporter', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'device', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'confirmed', type: 'string' },
            { name: 'confirmer', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'reservation', type: 'string' },
            { name: 'primary', type: 'string' },
            { name: 'related', type: 'string' },
            { name: 'filter', type: 'string' },
            { name: 'reversal', type: 'string' },
            { name: 'masked', type: 'string' },
            { name: 'createdtime', type: 'string' },
            { name: 'levelid', type: 'int' }
        ],
        idProperty: 'id'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        downloadURL: '/Maintenance/DownloadRedundantAlarms',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Maintenance/RequestRedundantAlarms',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                baseNode: 'root',
                stationTypes: [],
                roomTypes: [],
                subDeviceTypes: [],
                subLogicTypes: [],
                points: [],
                levels: []
            },
            simpleSortMode: true
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var alarmToolbar = Ext.create('Ext.panel.Panel', {
        dock: 'top',
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'rangePicker',
                        xtype: 'DevicePicker',
                        fieldLabel: '查询范围',
                        emptyText: '默认全部',
                        labelWidth: 60,
                        width: 220,
                    },
                    {
                        id: 'startField',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'level-multicombo',
                        xtype: 'AlarmLevelMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        id: 'type-combobox',
                        xtype: 'combobox',
                        fieldLabel: '筛选类型',
                        labelWidth: 60,
                        width: 220,
                        store: Ext.create('Ext.data.Store', {
                            fields: [
                                 { name: 'id', type: 'int' },
                                 { name: 'name', type: 'string' }
                            ],
                            data: [
                                { "id": 1, "name": '按告警编号' },
                                { "id": 2, "name": '按信号名称' }
                            ]
                        }),
                        align: 'center',
                        value: 1,
                        editable: false,
                        displayField: 'name',
                        valueField: 'id',
                    }, {
                        id: 'keyword-textbox',
                        xtype: 'textfield',
                        labelWidth: 50,
                        labelAlign: 'center',
                        width: 220,
                        maxLength: 500,
                        emptyText: '多条件请以;分隔，例: A;B;C',
                    },
                    {
                        id: 'exportButton',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            download();
                        }
                    }
                ]
            }
        ]
    });

    var currentLayout = Ext.create('Ext.grid.Panel', {
        id: 'currentLayout',
        region: 'center',
        title: '告警维护信息',
        glyph: 0xf029,
        store: currentStore,
        bbar: currentPagingToolbar,
        dockedItems: [alarmToolbar],
        flex:1,
        viewConfig: {
            loadMask: false,
            stripeRows: true,
            trackOver: true,
            preserveScrollOnRefresh: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
            getRowClass: function (record, rowIndex, rowParams, store) {
                return $$iPems.GetLevelCls(record.get("levelid"));
            }
        },
        columns: [
            {
                text: '序号',
                dataIndex: 'index',
                width: 60
            },
            {
                text: '告警编号',
                dataIndex: 'id'
            },
            {
                text: '告警级别',
                dataIndex: 'level',
                align: 'center',
                tdCls: 'x-level-cell'
            },
            {
                text: '告警时间',
                dataIndex: 'time',
                align: 'center',
                width: 150
            },
            {
                text: '告警历时',
                dataIndex: 'interval',
                align: 'center',
                width: 120
            },
            {
                text: '告警描述',
                dataIndex: 'comment'
            },
            {
                text: '触发值',
                dataIndex: 'value',
                align: 'center'
            },
            {
                text: '维护厂家',
                dataIndex: 'supporter'
            },
            {
                text: '信号名称',
                dataIndex: 'point'
            },
            {
                text: '所属设备',
                dataIndex: 'device'
            },
            {
                text: '所属机房',
                dataIndex: 'room'
            },
            {
                text: '所属站点',
                dataIndex: 'station'
            },
            {
                text: '所属区域',
                dataIndex: 'area'
            },
            {
                text: '确认状态',
                dataIndex: 'confirmed',
                align: 'center'
            },
            {
                text: '确认人员',
                dataIndex: 'confirmer',
                align: 'center'
            },
            {
                text: '确认时间',
                dataIndex: 'confirmedtime',
                align: 'center'
            },
            {
                text: '工程状态',
                dataIndex: 'reservation'
            },
            {
                text: '翻转告警',
                dataIndex: 'reversal'
            },
            {
                text: '主次告警',
                dataIndex: 'primary'
            },
            {
                text: '关联告警',
                dataIndex: 'related'
            },
            {
                text: '过滤告警',
                dataIndex: 'filter'
            },
            {
                text: '屏蔽告警',
                dataIndex: 'masked'
            },
            {
                text: '入库时间',
                dataIndex: 'createdtime',
                align: 'center',
                width: 150
            }
        ],
        listeners: {
            itemcontextmenu: function (me, record, item, index, e) {
                e.stopEvent();
                almContextMenu.record = record;
                almContextMenu.showAt(e.getXY());
            }
        }
    });

    var almContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        record: null,
        items: [{
            itemId: 'finish',
            glyph: 0xf003,
            text: '结束告警',
            handler: function () {
                if (almContextMenu.record == null)
                    return false;

                Ext.Msg.confirm('确认对话框', '您确定结束选中的告警吗？', function (buttonId, text) {
                    if (buttonId === 'yes') {
                        Ext.Ajax.request({
                            url: '/Maintenance/FinishRedundantAlarms',
                            params: { id: almContextMenu.record.get('id') },
                            mask: new Ext.LoadMask(currentLayout.getView(), { msg: '正在下发结束告警命令...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    currentPagingToolbar.doRefresh();
                                    Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
                    }
                });
            }
        }, {
            itemId: 'delete',
            glyph: 0xf004,
            text: '删除告警',
            handler: function () {
                if (almContextMenu.record == null)
                    return false;

                Ext.Msg.confirm('确认对话框', '您确定删除选中的告警吗？', function (buttonId, text) {
                    if (buttonId === 'yes') {
                        Ext.Ajax.request({
                            url: '/Maintenance/DeleteRedundantAlarms',
                            params: { id: almContextMenu.record.get('id') },
                            mask: new Ext.LoadMask(currentLayout.getView(), { msg: '正在处理...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    currentPagingToolbar.doRefresh();
                                    Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
                    }
                });
            }
        }, '-', {
            itemId: 'export',
            glyph: 0xf010,
            text: '数据导出',
            handler: function () {
                download();
            }
        }]
    });

    var query = function () {
        var parent = Ext.getCmp('rangePicker').getValue(),
            startDate = Ext.getCmp('startField').getRawValue(),
            endDate = Ext.getCmp('endField').getRawValue(),
            levels = Ext.getCmp('level-multicombo').getSelectedValues(),
            type = Ext.getCmp('type-combobox').getValue(),
            keyWord = Ext.getCmp('keyword-textbox').getRawValue(),
            proxy = currentStore.getProxy();

        proxy.extraParams.parent = parent;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.levels = levels;
        proxy.extraParams.type = type;
        proxy.extraParams.keyWord = keyWord;
        proxy.extraParams.cache = false;

        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    };

    var download = function () {
        $$iPems.download({
            url: currentStore.downloadURL,
            params: currentStore.getProxy().extraParams
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

})();