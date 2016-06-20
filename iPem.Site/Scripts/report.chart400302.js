(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'start', type: 'string' },
            { name: 'end', type: 'string' },
            { name: 'maxvalue', type: 'float' },
            { name: 'maxdisplay', type: 'string' },
            { name: 'maxtime', type: 'string' },
            { name: 'mindisplay', type: 'string' },
			{ name: 'minvalue', type: 'float' },
            { name: 'mintime', type: 'string' },
            { name: 'avgdisplay', type: 'string' },
            { name: 'avgvalue', type: 'float' },
            { name: 'total', type: 'int' }
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
            url: '/Report/DownloadChart400302',
            params: store.proxy.extraParams
        });
    };

    var chartLine = Ext.create('Ext.chart.Chart', {
        xtype: 'chart',
        flex: 1,
        legend: {
            position: 'right',
            itemSpacing: 3,
            boxStrokeWidth: 1,
            boxStroke: '#c0c0c0'
        },
        axes: [{
            type: 'Numeric',
            position: 'left',
            fields: ['maxvalue', 'avgvalue', 'minvalue'],
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
            title: $$iPems.lang.Report400302.Chart.Max,
            smooth: true,
            axis: ['left', 'bottom'],
            xField: 'index',
            yField: 'maxvalue',
            highlightLine: false,
            label: {
                display: 'under',
                field: 'maxdisplay'
            },
            tips: {
                trackMouse: true,
                minWidth: 150,
                minHeight: 50,
                renderer: function (storeItem, item) {
                    this.update(
                        Ext.String.format('{0}: {1} - {2}<br/>{3}: {4}<br/>{5}: {6}<br/>{7}: {8}',
                        $$iPems.lang.Report400302.Chart.Range,
                        storeItem.get('start'),
                        storeItem.get('end'),
                        $$iPems.lang.Report400302.Chart.Value,
                        storeItem.get('maxdisplay'),
                        $$iPems.lang.Report400302.Chart.Time,
                        storeItem.get('maxtime'),
                        $$iPems.lang.Report400302.Chart.Total,
                        storeItem.get('total'))
                    );
                }
            },
            style: {
                fill: '#94AE0A',
                stroke: '#94AE0A',
                'stroke-width': 2,
                opacity: 1
            },
            markerConfig: {
                type: 'circle',
                size: 3,
                radius: 3,
                fill: '#94AE0A',
                stroke: '#94AE0A',
                'stroke-width': 2
            },
            highlight: {
                size: 4,
                radius: 4,
                'stroke-width': 2
            }
        }, {
            type: 'line',
            title: $$iPems.lang.Report400302.Chart.Avg,
            smooth: true,
            axis: ['left', 'bottom'],
            xField: 'index',
            yField: 'avgvalue',
            highlightLine: false,
            label: {
                display: 'under',
                field: 'avgdisplay'
            },
            tips: {
                trackMouse: true,
                minWidth: 150,
                minHeight: 50,
                renderer: function (storeItem, item) {
                    this.update(
                        Ext.String.format('{0}: {1} - {2}<br/>{3}: {4}<br/>{5}: {6}',
                        $$iPems.lang.Report400302.Chart.Range,
                        storeItem.get('start'),
                        storeItem.get('end'),
                        $$iPems.lang.Report400302.Chart.Value,
                        storeItem.get('avgdisplay'),
                        $$iPems.lang.Report400302.Chart.Total,
                        storeItem.get('total'))
                    );
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
                fill: '#157fcc',
                stroke: '#157fcc',
                'stroke-width': 2
            },
            highlight: {
                size: 4,
                radius: 4,
                'stroke-width': 2
            }
        }, {
            type: 'line',
            title: $$iPems.lang.Report400302.Chart.Min,
            smooth: true,
            axis: ['left', 'bottom'],
            xField: 'index',
            yField: 'minvalue',
            highlightLine: false,
            label: {
                display: 'under',
                field: 'mindisplay'
            },
            tips: {
                trackMouse: true,
                minWidth: 150,
                minHeight: 50,
                renderer: function (storeItem, item) {
                    this.update(
                        Ext.String.format('{0}: {1} - {2}<br/>{3}: {4}<br/>{5}: {6}<br/>{7}: {8}',
                        $$iPems.lang.Report400302.Chart.Range,
                        storeItem.get('start'),
                        storeItem.get('end'),
                        $$iPems.lang.Report400302.Chart.Value,
                        storeItem.get('mindisplay'),
                        $$iPems.lang.Report400302.Chart.Time,
                        storeItem.get('mintime'),
                        $$iPems.lang.Report400302.Chart.Total,
                        storeItem.get('total'))
                    );
                }
            },
            style: {
                fill: '#A61120',
                stroke: '#A61120',
                'stroke-width': 2,
                opacity: 1
            },
            markerConfig: {
                type: 'circle',
                size: 3,
                radius: 3,
                fill: '#A61120',
                stroke: '#A61120',
                'stroke-width': 2
            },
            highlight: {
                size: 4,
                radius: 4,
                'stroke-width': 2
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            model: 'PointModel'
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
            url: '/Report/RequestChart400302',
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
                title: $$iPems.lang.Report400302.LineTitle,
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
                title: $$iPems.lang.Report400302.DetailTitle,
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
                        text: $$iPems.lang.Report400302.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.Start,
                        dataIndex: 'start'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.End,
                        dataIndex: 'end'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.MaxValue,
                        dataIndex: 'maxdisplay'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.MaxTime,
                        dataIndex: 'maxtime'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.MinValue,
                        dataIndex: 'mindisplay'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.MinTime,
                        dataIndex: 'mintime'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.AvgValue,
                        dataIndex: 'avgdisplay'
                    },
                    {
                        text: $$iPems.lang.Report400302.Columns.Total,
                        dataIndex: 'total'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400302.ConditionTitle,
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
                                emptyText: $$iPems.lang.Report400302.ToolBar.DeviceEmptyText,
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
                                emptyText: $$iPems.lang.Report400302.ToolBar.PointEmptyText,
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
                                fieldLabel: $$iPems.lang.Report400302.ToolBar.Start,
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.addDays(Ext.ux.DateTime.today(),-1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datetimepicker',
                                fieldLabel: $$iPems.lang.Report400302.ToolBar.End,
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