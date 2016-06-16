(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'point', type: 'string' },
            { name: 'start', type: 'string' },
            { name: 'value', type: 'string' },
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
        me.proxy.extraParams.points = point.getValue();
        me.proxy.extraParams.starttime = start.getRawValue();
        me.proxy.extraParams.endtime = end.getRawValue();
        me.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadData400303',
            params: store.proxy.extraParams
        });
    };

    var prepareChart = function(chart, params) {
        // the model and store structure it's only to prevent error at render       
        Ext.define('chart.line.model', {
            extend: 'Ext.data.Model',
            fields: []
        });

        //you can config the store whatever you want 
        var store = Ext.create('Ext.data.Store', {
            model: 'chart.line.model',
            autoLoad: false
        });

        Ext.Ajax.request({
            url: '/Report/RequestChart400303',
            params: params || [],
            success: function (response) {
                var data = Ext.decode(response.responseText, true);
                if (Ext.isEmpty(data))
                    return false;

                if (!data.success) {
                    Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    return false;
                }

                var result = Ext.decode(data.data, true);
                if (result == null)
                    return false;

                var fields = [result.fields.key],
                    series = [];

                removeSeries(chart);
                Ext.Array.each(result.fields.values, function (item) {
                    fields.push(item.data, item.display, item.time);
                    series.push(item.data);

                    chart.series.add({
                        type: 'line',
                        title: item.title,
                        smooth: true,
                        highlightLine: false,
                        axis: ['left', 'bottom'],
                        xField: result.fields.key,
                        yField: item.data,
                        label: {
                            display: 'under',
                            field: item.display
                        },
                        tips: {
                            trackMouse: true,
                            minWidth: 150,
                            minHeight: 50,
                            renderer: function (storeItem) {
                                this.update(
                                    Ext.String.format('{0}: {1}<br/>{2}: {3}',
                                    $$iPems.lang.Report400303.Chart.Value,
                                    storeItem.get(item.display),
                                    $$iPems.lang.Report400303.Chart.Time,
                                    storeItem.get(item.time))
                                );
                            }
                        },
                        style: {
                            'stroke-width': 2,
                            opacity: 1
                        },
                        markerConfig: {
                            type: 'circle',
                            size: 3,
                            radius: 3,
                            'stroke-width': 0
                        },
                        highlight: {
                            size: 4,
                            radius: 4,
                            'stroke-width': 0
                        }
                    });
                });

                store.model.setFields(fields);
                store.loadData(result.data, false);

                var mAxes = chart.axes.items;
                for(var axis in mAxes){
                    if(mAxes[axis].type === "Numeric"){
                        mAxes[axis].fields = series;
                    }

                    if (mAxes[axis].type === "Category") {
                        mAxes[axis].fields = [result.fields.key];
                    }
                }
                chart.axes.items = mAxes;
                chart.bindStore(store);
            }
        });
    };

    var removeSeries = function (chart) {
        var series = chart.series.items, surface = chart.surface,
            length = series.length, len = surface.groups.keys.length,
            array = [];
        for (var i = 0; i < length; i++)
            array = Ext.Array.merge(array, series[i].group.keys);
        chart.series.clear();
        for (var j = 0; j < array.length; j++)
            surface.items.getByKey(array[j]).destroy();
        for (var t = 0; t < len; t++)
            surface.groups.items[t].destroy();
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
            fields: [],
            minorTickSteps: 1,
            title: false,
            grid: true
        }, {
            type: 'Category',
            position: 'bottom',
            fields: [],
            title: false,
            minorTickSteps: 3,
            label: {
                rotate: {
                    degrees: 0
                }
            }
        }],
        series: [],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: []
        })
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        groupField: 'point',
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Report/RequestData400303',
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
                    prepareChart(chartLine, me.proxy.extraParams);
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
                title: $$iPems.lang.Report400303.LineTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                layout: {
                    type: 'hbox',
                    align: 'stretch',
                    pack: 'start'
                },
                items: [chartLine]
            }, {
                xtype: 'grid',
                glyph: 0xf029,
                title: $$iPems.lang.Report400303.DetailTitle,
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
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: $$iPems.lang.Report400303.GroupTpl,
                    hideGroupedHeader: false,
                    startCollapsed: true
                }],
                columns: [
                    {
                        text: $$iPems.lang.Report400303.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400303.Columns.Point,
                        dataIndex: 'point'
                    },
                    {
                        text: $$iPems.lang.Report400303.Columns.Start,
                        dataIndex: 'start'
                    },
                    {
                        text: $$iPems.lang.Report400303.Columns.Value,
                        dataIndex: 'value'
                    },
                    {
                        text: $$iPems.lang.Report400303.Columns.Time,
                        dataIndex: 'time'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400303.ConditionTitle,
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
                                emptyText: $$iPems.lang.Report400303.ToolBar.DeviceEmptyText,
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
                                xtype: 'PointMultiCombo',
                                allowBlank: false,
                                emptyText: $$iPems.lang.Report400303.ToolBar.PointEmptyText,
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
                                fieldLabel: $$iPems.lang.Report400303.ToolBar.Start,
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.todayString(Ext.ux.DateTime.defaultFormat),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datetimepicker',
                                fieldLabel: $$iPems.lang.Report400303.ToolBar.End,
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.nowString(),
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