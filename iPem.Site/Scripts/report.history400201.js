(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'value', type: 'float' },
            { name: 'unit', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'statusid', type: 'int' }
        ],
        idProperty: 'index'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400201',
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

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var currentLayout = Ext.create('Ext.panel.Panel', {
        id: 'currentLayout',
        region: 'center',
        border: false,
        bodyCls: 'x-border-body-panel',
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        items: [{
            xtype: 'grid',
            glyph: 0xf029,
            title: '测值信息',
            collapsible: true,
            collapseFirst: false,
            margin: '5 0 0 0',
            flex: 2,
            store: currentStore,
            viewConfig: {
                loadMask: true,
                stripeRows: true,
                trackOver: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                getRowClass: function (record, rowIndex, rowParams, store) {
                    return $$iPems.GetStateCls(record.get("statusid"));
                }
            },
            columns: [
                {
                    text: '序号',
                    dataIndex: 'index',
                    width: 60
                },
                {
                    text: '所属区域',
                    dataIndex: 'area'
                },
                {
                    text: '所属站点',
                    dataIndex: 'station'
                },
                {
                    text: '所属机房',
                    dataIndex: 'room'
                },
                {
                    text: '所属设备',
                    dataIndex: 'device'
                },
                {
                    text: '信号名称',
                    dataIndex: 'point',
                    width: 150
                },
                {
                    text: '信号标准化名称',
                    dataIndex: 'name',
                    width: 150
                },
                {
                    text: '信号类型',
                    dataIndex: 'type'
                },
                {
                    text: '信号测值',
                    dataIndex: 'value'
                },
                {
                    text: '单位/描述',
                    dataIndex: 'unit'
                },
                {
                    text: '信号状态',
                    dataIndex: 'status',
                    tdCls: 'x-status-cell'
                },
                {
                    text: '测值时间',
                    dataIndex: 'time',
                    width: 150
                }
            ],
            bbar: currentPagingToolbar,
        }],
        dockedItems: [{
            xtype: 'panel',
            glyph: 0xf034,
            title: '筛选条件',
            collapsible: true,
            collapsed: false,
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
                            width: 220
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
                            id: 'station-type-multicombo',
                            xtype: 'StationTypeMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'room-type-multicombo',
                            xtype: 'RoomTypeMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'device-type-multipicker',
                            xtype: 'SubDeviceTypeMultiPicker',
                            emptyText: '默认全部',
                            width: 220
                        },
                        {
                            id: 'exportButton',
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            disabled: true,
                            handler: function (me, event) {
                                print();
                            }
                        }
                    ]
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'point-multipicker',
                            xtype: 'PointMultiPicker',
                            emptyText: '默认全部',
                            width: 220
                        },
                        {
                            id: 'point-keywords',
                            xtype: 'textfield',
                            fieldLabel: '关键字',
                            emptyText: '多关键字请以;分隔，例: A;B;C',
                            labelWidth: 60,
                            width: 448
                        }
                    ]
                }
            ]
        }]
    });

    var query = function () {
        var parent = Ext.getCmp('rangePicker').getValue(),
            startDate = Ext.getCmp('startField').getRawValue(),
            endDate = Ext.getCmp('endField').getRawValue(),
            statypes = Ext.getCmp('station-type-multicombo').getSelectedValues(),
            roomtypes = Ext.getCmp('room-type-multicombo').getSelectedValues(),
            devtypes = Ext.getCmp('device-type-multipicker').getValue(),
            points = Ext.getCmp('point-multipicker').getValue(),
            keywords = Ext.getCmp('point-keywords').getRawValue(),
            proxy = currentStore.getProxy();

        proxy.extraParams.parent = parent;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.statypes = statypes;
        proxy.extraParams.roomtypes = roomtypes;
        proxy.extraParams.devtypes = devtypes;
        proxy.extraParams.points = points;
        proxy.extraParams.keywords = keywords;
        proxy.extraParams.cache = false;
        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    };

    var print = function () {
        $$iPems.download({
            url: '/Report/DownloadHistory400201',
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