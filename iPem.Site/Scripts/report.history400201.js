(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'devType', type: 'string' },
            { name: 'devName', type: 'string' },
            { name: 'logic', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'timestamp', type: 'string' },
            { name: 'status', type: 'int' },
            { name: 'statusDisplay', type: 'string' }
        ],
        idProperty: 'index'
    });

    var query = function (pagingtoolbar) {
        var me = pagingtoolbar.store;
        me.proxy.extraParams.parent = Ext.getCmp('rangePicker').getValue();
        me.proxy.extraParams.starttime = Ext.getCmp('startField').getRawValue();
        me.proxy.extraParams.endtime = Ext.getCmp('endField').getRawValue();
        me.proxy.extraParams.statypes = Ext.getCmp('station-type-multicombo').getSelectedValues();
        me.proxy.extraParams.roomtypes = Ext.getCmp('room-type-multicombo').getSelectedValues();
        me.proxy.extraParams.devtypes = Ext.getCmp('device-type-multicombo').getSelectedValues();
        me.proxy.extraParams.logictypes = Ext.getCmp('logic-type-multicombo').getSelectedValues();
        me.proxy.extraParams.point = Ext.getCmp('point-name-textfield').getRawValue();
        me.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadHistory400201',
            params: store.proxy.extraParams
        });
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Report/RequestHistory400201',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    Ext.onReady(function () {
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
                title: '信号测值信息',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
                store: currentStore,
                loadMask: true,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    loadMask: false,
                    preserveScrollOnRefresh: true,
                    stripeRows: true,
                    trackOver: true,
                    emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                    getRowClass: function (record, rowIndex, rowParams, store) {
                        return $$iPems.GetPointStatusCls(record.get("status"));
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
                        text: '设备类型',
                        dataIndex: 'devType'
                    },
                    {
                        text: '设备名称',
                        dataIndex: 'devName'
                    },
                    {
                        text: '逻辑分类',
                        dataIndex: 'logic'
                    },
                    {
                        text: '信号名称',
                        dataIndex: 'point'
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
                        text: '测值时间',
                        dataIndex: 'timestamp'
                    },
                    {
                        text: '信号状态',
                        dataIndex: 'statusDisplay',
                        tdCls: 'x-status-cell'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '信号筛选条件',
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
                                    query(currentPagingToolbar);
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
                                id: 'device-type-multicombo',
                                xtype: 'DeviceTypeMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                xtype: 'button',
                                glyph: 0xf010,
                                text: '数据导出',
                                handler: function (me, event) {
                                    print(currentStore);
                                }
                            }
                        ]
                    },
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'logic-type-multicombo',
                                xtype: 'LogicTypeMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'point-name-textfield',
                                xtype: 'textfield',
                                fieldLabel: '信号名称',
                                emptyText: '多条件请以;分隔，例: A;B;C',
                                labelWidth: 60,
                                width: 450
                            }
                        ]
                    }
                ]
            }]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            query(currentPagingToolbar);
        }
    });
})();