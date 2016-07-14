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
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
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
                                    '放电测值',
                                    storeItem.get(item.display),
                                    '测值时间',
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
                title: '电池放电曲线',
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
                title: '电池放电信息',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
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
                        return $$iPems.GetPointStatusCls(record.get("state"));
                    }
                },
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: '{name} ({rows.length}条)',
                    hideGroupedHeader: false,
                    startCollapsed: true
                }],
                columns: [
                    {
                        text: '序号',
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: '信号名称',
                        dataIndex: 'point'
                    },
                    {
                        text: '开始时间',
                        dataIndex: 'start'
                    },
                    {
                        text: '放电测值',
                        dataIndex: 'value'
                    },
                    {
                        text: '测值时间',
                        dataIndex: 'time'
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
                                id: 'devicePicker',
                                xtype: 'DevicePicker',
                                allowBlank: false,
                                emptyText: '请选择设备...',
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
                                emptyText: '请选择信号...',
                                labelWidth: 60,
                                width: 280,
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
                                id: 'startField',
                                xtype: 'datetimepicker',
                                fieldLabel: '开始时间',
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.addDays(Ext.ux.DateTime.today(), -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datetimepicker',
                                fieldLabel: '结束时间',
                                labelWidth: 60,
                                width: 280,
                                value: Ext.ux.DateTime.addSeconds(Ext.ux.DateTime.today(), -1),
                                editable: false,
                                allowBlank: false
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