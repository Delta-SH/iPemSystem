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
                title: $$iPems.lang.Report400201.DetailTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
                store: currentStore,
                loadMask: true,
                tools: [{
                    type: 'print',
                    tooltip: $$iPems.lang.Import,
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    loadMask: false,
                    preserveScrollOnRefresh: true,
                    stripeRows: true,
                    trackOver: true,
                    emptyText: $$iPems.lang.GridEmptyText,
                    getRowClass: function (record, rowIndex, rowParams, store) {
                        return $$iPems.GetPointStatusCls(record.get("status"));
                    }
                },
                columns: [
                    {
                        text: $$iPems.lang.Report400201.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Area,
                        dataIndex: 'area'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Station,
                        dataIndex: 'station'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Room,
                        dataIndex: 'room'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.DevType,
                        dataIndex: 'devType'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.DevName,
                        dataIndex: 'devName'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Logic,
                        dataIndex: 'logic'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Point,
                        dataIndex: 'point'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Type,
                        dataIndex: 'type'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Value,
                        dataIndex: 'value'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Timestamp,
                        dataIndex: 'timestamp'
                    },
                    {
                        text: $$iPems.lang.Report400201.Columns.Status,
                        dataIndex: 'statusDisplay',
                        tdCls: 'x-status-cell'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400201.ConditionTitle,
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
                                fieldLabel: $$iPems.lang.Report400201.ToolBar.Range,
                                emptyText: $$iPems.lang.AllEmptyText,
                                labelWidth: 60,
                                width: 220,
                            },
                            {
                                id: 'startField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400201.ToolBar.Start,
                                labelWidth: 60,
                                width: 220,
                                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400201.ToolBar.End,
                                labelWidth: 60,
                                width: 220,
                                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                xtype: 'button',
                                glyph: 0xf005,
                                text: $$iPems.lang.Query,
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
                                emptyText: $$iPems.lang.AllEmptyText
                            },
                            {
                                id: 'room-type-multicombo',
                                xtype: 'RoomTypeMultiCombo',
                                emptyText: $$iPems.lang.AllEmptyText
                            },
                            {
                                id: 'device-type-multicombo',
                                xtype: 'DeviceTypeMultiCombo',
                                emptyText: $$iPems.lang.AllEmptyText
                            },
                            {
                                xtype: 'button',
                                glyph: 0xf010,
                                text: $$iPems.lang.Import,
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
                                emptyText: $$iPems.lang.AllEmptyText
                            },
                            {
                                id: 'point-name-textfield',
                                xtype: 'textfield',
                                fieldLabel: $$iPems.lang.Report400201.ToolBar.PointName,
                                emptyText: $$iPems.lang.MultiConditionEmptyText,
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