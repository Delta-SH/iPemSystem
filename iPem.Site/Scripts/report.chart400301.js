(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'value', type: 'string' },
			{ name: 'threshold', type: 'string' },
            { name: 'state', type: 'int' },
            { name: 'stateDisplay', type: 'string' },
            { name: 'time', type: 'string' }
        ],
        idProperty: 'index'
    });

    var query = function (pagingtoolbar) {
        var device = Ext.getCmp('devicePicker'),
            point = Ext.getCmp('pointCombo'),
            start = Ext.getCmp('startField'),
            end = Ext.getCmp('endField');

        if (!device.isValid()) return;
        if (!point.isValid()) return;

        var me = pagingtoolbar.store;
        me.proxy.extraParams.device = device.getValue();
        me.proxy.extraParams.point = point.getValue();
        me.proxy.extraParams.starttime = start.getRawValue();
        me.proxy.extraParams.endtime = end.getRawValue();
        me.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadChart400301',
            params: store.proxy.extraParams
        });
    };

    var chartLine = Ext.create('Ext.chart.Chart', {
        xtype: 'chart',
        flex: 1,
        axes: [{
            type: 'Numeric',
            position: 'left',
            fields: ['value'],
            minorTickSteps: 1,
            title: false,
            grid: true
        }, {
            type: 'Category',
            position: 'bottom',
            fields: 'index',
            title: false,
            minorTickSteps: 3,
            label: {
                rotate: {
                    degrees: 0
                }
            }
        }],
        series: [{
            type: 'line',
            smooth: true,
            axis: ['left', 'bottom'],
            xField: 'index',
            yField: 'value',
            highlightLine: false,
            label: {
                display: 'under',
                field: 'comment'
            },
            tips: {
                trackMouse: true,
                minWidth: 150,
                minHeight: 50,
                renderer: function (storeItem, item) {
                    this.setTitle(storeItem.get('name'));
                    this.update(storeItem.get('comment'));
                }
            },
            style: {
                fill: '#157fcc',
                stroke: '#157fcc',
                'stroke-width': 2,
                opacity: 1
            },
            markerConfig: {
                type: 'circle',
                size: 3,
                radius: 3,
                fill: '#fff',
                stroke: '#157fcc',
                'stroke-width': 2
            },
            highlight: {
                size: 5,
                radius: 5,
                'stroke-width': 4
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['index', 'name', 'value', 'comment']
        })
    });

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
            url: '/Report/RequestChart400301',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var data = me.proxy.reader.jsonData,
                        chartData = $$iPems.ChartEmptyDataLine;

                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)) {
                        chartData = data.chart;
                    }

                    chartLine.getStore().loadData(chartData, false);
                }
            }
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
                xtype: 'panel',
                glyph: 0xf031,
                title: $$iPems.lang.Report400301.LineTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                tools: [
                    //{
                    //    type: 'print',
                    //    tooltip: $$iPems.lang.Import,
                    //    handler: function (event, toolEl, panelHeader) {
                    //        Ext.ux.ImageExporter.save([chartPie1, chartPie2, chartPie3]);
                    //    }
                    //}
                ],
                layout: {
                    type: 'hbox',
                    align: 'stretch',
                    pack: 'start'
                },
                items: [chartLine]
            }, {
                xtype: 'grid',
                glyph: 0xf029,
                title: $$iPems.lang.Report400301.DetailTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
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
                        return $$iPems.GetPointStatusCls(record.get("state"));
                    }
                },
                columns: [
                    {
                        text: $$iPems.lang.Report400301.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400301.Columns.Value,
                        dataIndex: 'value'
                    },
                    {
                        text: $$iPems.lang.Report400301.Columns.Time,
                        dataIndex: 'time',
                        width: 150
                    },
                    {
                        text: $$iPems.lang.Report400301.Columns.Threshold,
                        dataIndex: 'threshold'
                    },
                    {
                        text: $$iPems.lang.Report400301.Columns.State,
                        dataIndex: 'stateDisplay',
                        tdCls: 'x-status-cell'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400301.ConditionTitle,
                collapsible: true,
                collapsed: false,
                dock: 'top',
                items: [
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'devicePicker',
                                xtype: 'DevicePicker',
                                allowBlank: false,
                                emptyText: $$iPems.lang.Report400301.ToolBar.DeviceEmptyText,
                                selectOnLeaf: true,
                                selectAll: false,
                                listeners: {
                                    select: function (me, record) {
                                        var keys = $$iPems.SplitKeys(record.data.id);
                                        if (keys.length == 2) {
                                            var pointCombo = Ext.getCmp('pointCombo');
                                            pointCombo.bind(keys[1], true, false, true, false);
                                        }
                                    }
                                }
                            },
                            {
                                id: 'pointCombo',
                                xtype: 'PointCombo',
                                allowBlank: false,
                                emptyText: $$iPems.lang.Report400301.ToolBar.PointEmptyText,
                                labelWidth: 60,
                                width: 280,
                            },
                            {
                                xtype: 'splitbutton',
                                glyph: 0xf005,
                                text: $$iPems.lang.Ok,
                                handler: function (me, event) {
                                    query(currentPagingToolbar);
                                },
                                menu: [{
                                    text: $$iPems.lang.Import,
                                    glyph: 0xf010,
                                    handler: function (me, event) {
                                        print(currentStore);
                                    }
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'startField',
                                xtype: 'datetimepicker',
                                fieldLabel: $$iPems.lang.Report400301.ToolBar.Start,
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.addDays(Ext.ux.DateTime.today(),-1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datetimepicker',
                                fieldLabel: $$iPems.lang.Report400301.ToolBar.End,
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.addSeconds(Ext.ux.DateTime.today(), -1),
                                editable: false,
                                allowBlank: false
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
        }
    });
})();